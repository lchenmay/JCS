param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
$policy = Get-Content -Raw -LiteralPath $PolicyPath | ConvertFrom-Json
$testRoot = Join-Path ([System.IO.Path]::GetTempPath()) ("codex-policy-test-" + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $testRoot | Out-Null
$oldStateRoot = $env:CODEX_POLICY_STATE_ROOT
$oldAuditRoot = $env:CODEX_POLICY_AUDIT_ROOT
$oldMode = $env:CODEX_POLICY_MODE_OVERRIDE
$env:CODEX_POLICY_STATE_ROOT = Join-Path $testRoot 'state'
$env:CODEX_POLICY_AUDIT_ROOT = Join-Path $testRoot 'audit'
$env:CODEX_POLICY_MODE_OVERRIDE = 'enforce'
$script:suiteResults = New-Object System.Collections.Generic.List[object]

function Invoke-Hook {
    param([string] $ScriptName, [object] $Event, [string] $EffectivePolicyPath = $script:testPolicyPath)
    $json = $Event | ConvertTo-Json -Depth 24 -Compress
    $output = $json | & pwsh.exe -NoProfile -File (Join-Path $PSScriptRoot $ScriptName) -PolicyPath $EffectivePolicyPath
    if ($LASTEXITCODE -ne 0) { throw "$ScriptName exited with $LASTEXITCODE" }
    return ($output | Out-String).Trim()
}

function Assert-Condition {
    param([bool] $Condition, [string] $Message)
    if (-not $Condition) { throw $Message }
}

function Invoke-TestSuite {
    param([int] $Number, [string] $Name, [scriptblock] $Body)
    try {
        & $Body
        $script:suiteResults.Add([pscustomobject]@{ number = $Number; name = $Name; passed = $true; error = '' })
        Write-Output ("PASS {0:D2}: {1}" -f $Number, $Name)
    }
    catch {
        $script:suiteResults.Add([pscustomobject]@{ number = $Number; name = $Name; passed = $false; error = $_.Exception.Message })
        Write-Output ("FAIL {0:D2}: {1} -- {2}" -f $Number, $Name, $_.Exception.Message)
    }
}

function New-BaseEvent {
    param([string] $SessionId)
    return @{ session_id = $SessionId; turn_id = 'turn-1'; cwd = [string] $policy.repository.root; permission_mode = 'default' }
}

try {
    $policy.mode = 'enforce'
    $policy.repository.root = $testRoot
    $policy.generatedPaths = @('generated/output.txt')
    $policy.generators.direct = @('unsafe-generator.exe')
    $policy.generators.trusted = @('scripts/regenerate-types.ps1')
    $policy.completionGate.enabled = $true
    $policy.verification.entryPoint = 'scripts/codex-verify.ps1'
    $testPolicyDirectory = Join-Path $testRoot '.codex'
    New-Item -ItemType Directory -Path $testPolicyDirectory | Out-Null
    $script:testPolicyPath = Join-Path $testPolicyDirectory 'policy.json'
    $policy | ConvertTo-Json -Depth 24 | Set-Content -LiteralPath $script:testPolicyPath -Encoding UTF8
    New-Item -ItemType Directory -Path (Join-Path $testRoot 'src') | Out-Null
    Set-Content -LiteralPath (Join-Path $testRoot 'src\tracked.txt') -Value 'tracked' -Encoding UTF8
    Set-Content -LiteralPath (Join-Path $testRoot 'src\user-work.txt') -Value 'baseline' -Encoding UTF8
    & git -C $testRoot init --quiet
    if ($LASTEXITCODE -ne 0) { throw 'Unable to initialize the isolated policy test repository.' }
    & git -C $testRoot add --all
    & git -C $testRoot -c user.name='Codex Policy Test' -c user.email='policy-test@example.invalid' commit --quiet -m 'Create clean policy fixture'
    if ($LASTEXITCODE -ne 0) { throw 'Unable to commit the clean policy test fixture.' }
    . (Join-Path $PSScriptRoot 'PolicyCommon.ps1')

    Invoke-TestSuite 1 'safe repository-local edit' {
        $base = New-BaseEvent 'suite-01'
        $start = $base + @{ hook_event_name = 'SessionStart'; source = 'startup'; model = 'test-model' }
        $startResult = Invoke-Hook 'SessionStart.ps1' $start | ConvertFrom-Json
        Assert-Condition ([string] $startResult.hookSpecificOutput.additionalContext -match 'write-scope=repository-only') 'SessionStart did not load the project-local scope.'
        Assert-Condition ([string] $startResult.hookSpecificOutput.additionalContext -match 'not managed by this repository') 'SessionStart did not separate runtime permissions from project policy.'
        $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: src/safe.txt' } }
        Assert-Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $event))) 'A safe local patch was not allowed silently.'
    }

    Invoke-TestSuite 2 'generated artifact protection' {
        $base = New-BaseEvent 'suite-02'
        $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: generated/output.txt' } }
        $result = Invoke-Hook 'PreToolUse.ps1' $event | ConvertFrom-Json
        Assert-Condition ($result.hookSpecificOutput.permissionDecision -eq 'deny') 'A direct generated-file edit was not denied.'
        Assert-Condition ([string] $result.hookSpecificOutput.permissionDecisionReason -match 'L2') 'Generated-file denial did not carry L2 risk.'
        $loadedPolicy = Get-PolicyData -PolicyPath $script:testPolicyPath
        $fallbackState = Read-PolicySessionState -Policy $loadedPolicy -SessionId 'suite-02'
        Assert-Condition ($null -ne $fallbackState) 'PreToolUse did not bootstrap session state when SessionStart was absent.'
        Assert-Condition ($null -ne $fallbackState.PSObject.Properties['baselineStatusSha256']) 'Fallback session state did not preserve a baseline hash.'
        $shellEvent = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = "Set-Content -LiteralPath generated/output.txt -Value 'unsafe'" } }
        $shellResult = Invoke-Hook 'PreToolUse.ps1' $shellEvent | ConvertFrom-Json
        Assert-Condition ($shellResult.hookSpecificOutput.permissionDecision -eq 'deny') 'A shell write to a generated file was not denied.'
    }

    Invoke-TestSuite 3 'controlled generator entry point' {
        $base = New-BaseEvent 'suite-03'
        $direct = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'unsafe-generator.exe --write' } }
        $directResult = Invoke-Hook 'PreToolUse.ps1' $direct | ConvertFrom-Json
        Assert-Condition ($directResult.hookSpecificOutput.permissionDecision -eq 'deny') 'Direct generator execution was not denied.'
        $trusted = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'pwsh scripts/regenerate-types.ps1 -Execute' } }
        Assert-Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $trusted))) 'The trusted generation wrapper was not allowed.'
    }

    Invoke-TestSuite 4 'destructive Git history operations' {
        $base = New-BaseEvent 'suite-04'
        foreach ($command in @('git reset --hard HEAD~1', 'git clean -fdx', 'git push origin main --force-with-lease')) {
            $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = $command } }
            $result = Invoke-Hook 'PreToolUse.ps1' $event | ConvertFrom-Json
            Assert-Condition ($result.hookSpecificOutput.permissionDecision -eq 'deny') "Destructive Git command was not denied: $command"
        }
    }

    Invoke-TestSuite 5 'recursive delete and forced process termination' {
        $base = New-BaseEvent 'suite-05'
        foreach ($command in @('Remove-Item -LiteralPath .\cache -Recurse -Force', 'Stop-Process -Id 1234 -Force', 'taskkill.exe /PID 1234 /F')) {
            $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = $command } }
            $result = Invoke-Hook 'PreToolUse.ps1' $event | ConvertFrom-Json
            Assert-Condition ($result.hookSpecificOutput.permissionDecision -eq 'deny') "Destructive local command was not denied: $command"
        }
    }

    Invoke-TestSuite 6 'secret and production path protection' {
        $base = New-BaseEvent 'suite-06'
        foreach ($path in @('.env.production', 'certs/server.pem', 'deploy/prod/settings.json')) {
            $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = "*** Update File: $path" } }
            $result = Invoke-Hook 'PreToolUse.ps1' $event | ConvertFrom-Json
            Assert-Condition ($result.hookSpecificOutput.permissionDecision -eq 'deny') "Protected path was not denied: $path"
        }
        $shellEvent = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = "Set-Content -LiteralPath .env.production -Value 'unsafe'" } }
        $shellResult = Invoke-Hook 'PreToolUse.ps1' $shellEvent | ConvertFrom-Json
        Assert-Condition ($shellResult.hookSpecificOutput.permissionDecision -eq 'deny') 'A shell write to a protected path was not denied.'
    }

    Invoke-TestSuite 7 'external side effects require context, not implicit execution' {
        $base = New-BaseEvent 'suite-07'
        foreach ($command in @('ssh production.example', 'dotnet ef database update', 'deploy production')) {
            $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = $command } }
            $result = Invoke-Hook 'PreToolUse.ps1' $event | ConvertFrom-Json
            Assert-Condition ([string]::IsNullOrWhiteSpace([string] $result.hookSpecificOutput.permissionDecision)) 'A context-only external rule unexpectedly denied the command.'
            Assert-Condition (-not [string]::IsNullOrWhiteSpace([string] $result.hookSpecificOutput.additionalContext)) 'External action did not produce authorization context.'
        }
    }

    Invoke-TestSuite 8 'outside-repository isolation' {
        $base = New-BaseEvent 'suite-08'
        $outsidePatch = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: D:\DEV\Sibling\src\x.txt' } }
        $outsidePatchResult = Invoke-Hook 'PreToolUse.ps1' $outsidePatch | ConvertFrom-Json
        Assert-Condition ($outsidePatchResult.hookSpecificOutput.permissionDecision -eq 'deny') 'An outside-repository patch was not denied.'
        $outsideDelete = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'Remove-Item -LiteralPath C:\Temp\outside -Recurse' } }
        $outsideDeleteResult = Invoke-Hook 'PreToolUse.ps1' $outsideDelete | ConvertFrom-Json
        Assert-Condition ($outsideDeleteResult.hookSpecificOutput.permissionDecision -eq 'deny') 'An outside-repository delete was not denied.'
        $outsideRead = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'Get-Content -LiteralPath C:\Temp\outside.txt' } }
        Assert-Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $outsideRead))) 'A read-only outside path was incorrectly denied.'
        $hookDefinition = Get-Content -Raw -LiteralPath (Join-Path $PSScriptRoot '..\hooks.json')
        Assert-Condition ($hookDefinition -notmatch 'D:\\\\DEV\\\\JCS') 'Hook definitions still contain a hard-coded JCS root.'
        Assert-Condition ($hookDefinition -match 'git rev-parse --show-toplevel') 'Hook definitions do not resolve scripts from the active Git root.'
        foreach ($check in @($policy.verification.checks | Where-Object { [string] $_.executable -eq 'dotnet' -and @($_.arguments) -contains 'build' })) {
            Assert-Condition (@($check.arguments) -contains '--no-dependencies') "Dotnet check '$($check.name)' can write through sibling project references."
        }
        foreach ($check in @($policy.verification.checks | Where-Object { [string] $_.category -eq 'frontend' })) {
            Assert-Condition (@($check.preconditions).Count -gt 0) "Frontend check '$($check.name)' can mutate generated routes before detecting missing dependencies."
        }
    }

    Invoke-TestSuite 9 'path-based risk classification' {
        Assert-Condition ((Get-PolicyRiskLevelForPaths -Policy $policy -Paths @('src/safe.fs')) -eq 'L1') 'Ordinary source path was not classified L1.'
        Assert-Condition ((Get-PolicyRiskLevelForPaths -Policy $policy -Paths @('.codex/policy.json')) -eq 'L2') 'Policy path was not classified L2.'
        Assert-Condition ((Get-PolicyRiskLevelForPaths -Policy $policy -Paths @('JCS.Shared/Design-main.json')) -eq 'L2') 'Shared schema path was not classified L2.'
        Assert-Condition ((Get-PolicyRiskLevelForPaths -Policy $policy -Paths @('deploy/prod/app.json')) -eq 'L3') 'Production path was not classified L3.'
        $invalidPolicy = Get-Content -Raw -LiteralPath $script:testPolicyPath | ConvertFrom-Json
        $invalidPolicy.runtimePermissions.managedByRepository = $true
        $invalidPolicyPath = Join-Path $testPolicyDirectory 'invalid-policy.json'
        $invalidPolicy | ConvertTo-Json -Depth 24 | Set-Content -LiteralPath $invalidPolicyPath -Encoding UTF8
        $rejected = $false
        try { [void] (Get-PolicyData -PolicyPath $invalidPolicyPath) }
        catch { $rejected = $true }
        Assert-Condition $rejected 'Policy validation allowed repository-managed runtime permissions.'
        $pathRejected = $false
        try { Assert-PolicyRelativePath -Path '..\outside.json' -Field 'test' }
        catch { $pathRejected = $true }
        Assert-Condition $pathRejected 'Policy validation allowed a parent-traversing receipt path.'
        . (Join-Path $PSScriptRoot 'VerificationCommon.ps1')
        Reset-CodexVerificationResults
        Add-CodexVerificationResult -Name 'missing-tool' -Success:$false -ExitCode 1 -Output 'error: Script not found "vue-tsc"'
        Assert-Condition ($script:CodexVerificationResults[0].failureKind -eq 'tooling-prerequisite') 'Verification failure classification did not identify a missing tool.'
    }

    Invoke-TestSuite 10 'audit mode never denies' {
        $env:CODEX_POLICY_MODE_OVERRIDE = 'audit'
        try {
            $base = New-BaseEvent 'suite-10'
            foreach ($command in @('*** Update File: generated/output.txt', 'git reset --hard HEAD~1')) {
                $tool = if ($command.StartsWith('***')) { 'apply_patch' } else { 'Bash' }
                $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = $tool; tool_input = @{ command = $command } }
                $result = Invoke-Hook 'PreToolUse.ps1' $event | ConvertFrom-Json
                Assert-Condition ([string]::IsNullOrWhiteSpace([string] $result.hookSpecificOutput.permissionDecision)) 'Audit mode issued a denial.'
                Assert-Condition (-not [string]::IsNullOrWhiteSpace([string] $result.hookSpecificOutput.additionalContext)) 'Audit mode did not report its finding.'
            }
        }
        finally { $env:CODEX_POLICY_MODE_OVERRIDE = 'enforce' }
    }

    Invoke-TestSuite 11 'completion gate after successful verification' {
        $base = New-BaseEvent 'suite-11'
        $write = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: src/safe.txt' }; tool_response = @{ exitCode = 0 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $write)
        $stop = $base + @{ hook_event_name = 'Stop'; stop_hook_active = $false; last_assistant_message = 'done' }
        $blocked = Invoke-Hook 'Stop.ps1' $stop | ConvertFrom-Json
        Assert-Condition ($blocked.decision -eq 'block') 'Completion was not blocked after an unverified write.'
        $verify = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'pwsh -File scripts/codex-verify.ps1' }; tool_response = @{ exitCode = 0 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $verify)
        $stopOutput = Invoke-Hook 'Stop.ps1' $stop
        if (-not [string]::IsNullOrWhiteSpace($stopOutput)) {
            $stopResult = $stopOutput | ConvertFrom-Json
            Assert-Condition ([string]::IsNullOrWhiteSpace([string] $stopResult.decision)) 'Successful verification did not open the completion gate.'
            Assert-Condition ([string] $stopResult.systemMessage -match 'confidence=unknown') 'Missing verification receipt did not downgrade confidence.'
        }
    }

    Invoke-TestSuite 12 'failed verification and bounded stop recovery' {
        $base = New-BaseEvent 'suite-12'
        $write = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: JCS.Shared/Manual.fs' }; tool_response = @{ exitCode = 0 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $write)
        $verify = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'pwsh -File scripts/codex-verify.ps1' }; tool_response = @{ exitCode = 1 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $verify)
        $firstStop = $base + @{ hook_event_name = 'Stop'; stop_hook_active = $false; last_assistant_message = 'done' }
        $firstResult = Invoke-Hook 'Stop.ps1' $firstStop | ConvertFrom-Json
        Assert-Condition ($firstResult.decision -eq 'block') 'A failed verification did not block the first completion attempt.'
        Assert-Condition ([string] $firstResult.reason -match 'L2') 'Completion block did not retain the highest risk level.'
        $secondStop = $base + @{ hook_event_name = 'Stop'; stop_hook_active = $true; last_assistant_message = 'blocked' }
        $secondResult = Invoke-Hook 'Stop.ps1' $secondStop | ConvertFrom-Json
        Assert-Condition ([string]::IsNullOrWhiteSpace([string] $secondResult.decision)) 'The second stop created an infinite blocking loop.'
        Assert-Condition (-not [string]::IsNullOrWhiteSpace([string] $secondResult.systemMessage)) 'The second stop did not report the unresolved verification failure.'
    }

    Invoke-TestSuite 13 'outside write path normalization' {
        $base = New-BaseEvent 'suite-13'
        $parentPatch = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: ..\sibling\x.txt' } }
        $parentResult = Invoke-Hook 'PreToolUse.ps1' $parentPatch | ConvertFrom-Json
        Assert-Condition ($parentResult.hookSpecificOutput.permissionDecision -eq 'deny') 'A parent-traversing patch was not denied.'
        $outsidePath = Join-Path (Split-Path -Parent $testRoot) 'sibling\x.txt'
        $shellWrite = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = "Set-Content -LiteralPath '$outsidePath' -Value unsafe" } }
        $shellResult = Invoke-Hook 'PreToolUse.ps1' $shellWrite | ConvertFrom-Json
        Assert-Condition ($shellResult.hookSpecificOutput.permissionDecision -eq 'deny') 'A shell write to an absolute outside path was not denied.'
    }

    Invoke-TestSuite 14 'baseline overwrite prevention and completion gate' {
        $userPath = Join-Path $testRoot 'src\user-work.txt'
        Set-Content -LiteralPath $userPath -Value 'user-owned-change' -Encoding UTF8
        try {
            $base = New-BaseEvent 'suite-14'
            $start = $base + @{ hook_event_name = 'SessionStart'; source = 'startup'; model = 'test-model' }
            [void] (Invoke-Hook 'SessionStart.ps1' $start)
            foreach ($command in @('git restore -- src/user-work.txt', 'git checkout -- src/user-work.txt')) {
                $event = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = $command } }
                $result = Invoke-Hook 'PreToolUse.ps1' $event | ConvertFrom-Json
                Assert-Condition ($result.hookSpecificOutput.permissionDecision -eq 'deny') "A baseline-overwriting command was not denied: $command"
            }

            $postBase = New-BaseEvent 'suite-14-post'
            $postStart = $postBase + @{ hook_event_name = 'SessionStart'; source = 'startup'; model = 'test-model' }
            [void] (Invoke-Hook 'SessionStart.ps1' $postStart)
            & git -C $testRoot restore -- src/user-work.txt
            $post = $postBase + @{ hook_event_name = 'PostToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'git restore -- src/user-work.txt' }; tool_response = @{ exitCode = 0 } }
            $postResult = Invoke-Hook 'PostToolUse.ps1' $post | ConvertFrom-Json
            Assert-Condition ([string] $postResult.hookSpecificOutput.additionalContext -match 'pre-task changed path') 'PostToolUse did not report the disappeared baseline.'
            $stop = $postBase + @{ hook_event_name = 'Stop'; stop_hook_active = $false; last_assistant_message = 'done' }
            $stopResult = Invoke-Hook 'Stop.ps1' $stop | ConvertFrom-Json
            Assert-Condition ($stopResult.decision -eq 'block') 'Completion was not blocked after baseline loss.'
        }
        finally { & git -C $testRoot restore -- src/user-work.txt }
    }

    Invoke-TestSuite 15 'tracked deletion target protection' {
        $base = New-BaseEvent 'suite-15'
        $patchDelete = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = "*** Begin Patch`n*** Delete File: src/tracked.txt`n*** End Patch" } }
        $patchResult = Invoke-Hook 'PreToolUse.ps1' $patchDelete | ConvertFrom-Json
        Assert-Condition ($patchResult.hookSpecificOutput.permissionDecision -eq 'deny') 'An apply_patch tracked-file deletion was not denied.'
        $shellDelete = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'Remove-Item -LiteralPath src/tracked.txt -Force' } }
        $shellResult = Invoke-Hook 'PreToolUse.ps1' $shellDelete | ConvertFrom-Json
        Assert-Condition ($shellResult.hookSpecificOutput.permissionDecision -eq 'deny') 'A shell tracked-file deletion was not denied.'
        Set-Content -LiteralPath (Join-Path $testRoot 'untracked.tmp') -Value 'temporary' -Encoding UTF8
        $untrackedDelete = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'Remove-Item -LiteralPath untracked.tmp -Force' } }
        $untrackedResult = Invoke-Hook 'PreToolUse.ps1' $untrackedDelete
        Assert-Condition ([string]::IsNullOrWhiteSpace($untrackedResult)) "A non-baseline untracked-file deletion was incorrectly denied: $untrackedResult"
    }

    Invoke-TestSuite 16 'adaptive verification selection and coverage' {
        . (Join-Path $PSScriptRoot 'VerificationSelection.ps1')
        . (Join-Path $PSScriptRoot 'TaskRouting.ps1')
        $loadedPolicy = Get-PolicyData -PolicyPath $script:testPolicyPath
        $l1Plan = @(Get-CodexVerificationPlan -Policy $loadedPolicy -ChangedFiles @('src/safe.fs') -RiskLevel 'L1')
        Assert-Condition (@($l1Plan | Where-Object { $_.selected }).Count -eq 0) 'An unmatched L1 path selected heavyweight checks.'
        $l1Profile = Get-CodexTaskProfile -Policy $loadedPolicy -ChangedFiles @('src/safe.fs')
        $l1Coverage = Get-CodexVerificationCoverage -Policy $loadedPolicy -ChangedFiles @('src/safe.fs') -Plan $l1Plan -TaskProfile $l1Profile
        Assert-Condition ([string] $l1Coverage.confidence -eq 'unknown') 'An unmatched L1 path did not report unknown coverage.'

        $typeProfile = Get-CodexTaskProfile -Policy $loadedPolicy -ChangedFiles @('TypeSys/sample.fs')
        $typePlan = @(Get-CodexVerificationPlan -Policy $loadedPolicy -ChangedFiles @('TypeSys/sample.fs') -RiskLevel 'L2' -TaskTypes @($typeProfile.taskTypes))
        $selected = @($typePlan | Where-Object { $_.selected } | ForEach-Object { [string] $_.name })
        foreach ($name in @('typesys-release-build', 'jcs-shared-release-build', 'jcs-bizlogics-release-build', 'dependency-impact-readonly')) {
            Assert-Condition ($name -in $selected) "TypeSys change did not select '$name'."
        }
        Assert-Condition ('portal-typecheck' -notin $selected) 'TypeSys change unnecessarily selected portal typecheck.'
        $mockResults = @($typePlan | Where-Object { $_.selected } | ForEach-Object { [pscustomobject]@{ name = $_.name; success = $true; evidenceLevels = @($_.evidenceLevels) } })
        $mockResults += [pscustomobject]@{ name = 'generated-file-consistency'; success = $true; evidenceLevels = @('generation') }
        $typeCoverage = Get-CodexVerificationCoverage -Policy $loadedPolicy -ChangedFiles @('TypeSys/sample.fs') -Plan $typePlan -TaskProfile $typeProfile -Results $mockResults
        Assert-Condition ([string] $typeCoverage.confidence -eq 'structural') 'Declared TypeSys verification did not report structural coverage.'
    }

    Invoke-TestSuite 17 'partial confidence and repeated failure circuit breaker' {
        $loadedPolicy = Get-PolicyData -PolicyPath $script:testPolicyPath
        $receiptPath = Join-Path (Get-PolicyRepositoryRoot -Policy $loadedPolicy) ([string] $loadedPolicy.verification.receiptPath)
        New-Item -ItemType Directory -Force -Path (Split-Path -Parent $receiptPath) | Out-Null

        $base = New-BaseEvent 'suite-17-partial'
        $write = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: src/safe.txt' }; tool_response = @{ exitCode = 0 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $write)
        @{ verificationConfidence = 'partial'; recommendedActions = @('Run a focused check.'); checks = @() } | ConvertTo-Json -Depth 5 | Set-Content -LiteralPath $receiptPath -Encoding UTF8
        $verify = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'pwsh -File scripts/codex-verify.ps1' }; tool_response = @{ exitCode = 0 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $verify)
        $stop = $base + @{ hook_event_name = 'Stop'; stop_hook_active = $false; last_assistant_message = 'done' }
        $partialResult = Invoke-Hook 'Stop.ps1' $stop | ConvertFrom-Json
        Assert-Condition ([string]::IsNullOrWhiteSpace([string] $partialResult.decision)) 'Partial coverage incorrectly blocked completion.'
        Assert-Condition ([string] $partialResult.systemMessage -match 'confidence=partial') 'Partial coverage did not require an explicit caveat.'

        $repeatBase = New-BaseEvent 'suite-17-repeat'
        $repeatWrite = $repeatBase + @{ hook_event_name = 'PostToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: src/safe.txt' }; tool_response = @{ exitCode = 0 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $repeatWrite)
        @{ verificationConfidence = 'declared'; recommendedActions = @(); checks = @(@{ name = 'focused'; passed = $false; failureKind = 'command-failure' }) } | ConvertTo-Json -Depth 5 | Set-Content -LiteralPath $receiptPath -Encoding UTF8
        $failedVerify = $repeatBase + @{ hook_event_name = 'PostToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'pwsh -File scripts/codex-verify.ps1' }; tool_response = @{ exitCode = 1 } }
        [void] (Invoke-Hook 'PostToolUse.ps1' $failedVerify)
        [void] (Invoke-Hook 'PostToolUse.ps1' $failedVerify)
        $repeatStop = $repeatBase + @{ hook_event_name = 'Stop'; stop_hook_active = $false; last_assistant_message = 'done' }
        $repeatResult = Invoke-Hook 'Stop.ps1' $repeatStop | ConvertFrom-Json
        Assert-Condition ([string]::IsNullOrWhiteSpace([string] $repeatResult.decision)) 'Repeated identical failure kept the completion loop blocked.'
        Assert-Condition ([string] $repeatResult.systemMessage -match 'repeated 2 times') 'Repeated failure did not activate the circuit breaker.'
    }

    Invoke-TestSuite 18 'prompt routing and unknown-intent fallback' {
        . (Join-Path $PSScriptRoot 'TaskRouting.ps1')
        $loadedPolicy = Get-PolicyData -PolicyPath $script:testPolicyPath
        $profile = Get-CodexTaskProfile -Policy $loadedPolicy -Prompt '订单金额算错了，修复后端业务逻辑'
        Assert-Condition ('bugfix' -in @($profile.taskTypes)) 'A defect prompt did not select the bugfix rule pack.'
        Assert-Condition ('backend' -in @($profile.taskTypes)) 'A backend prompt did not select the backend rule pack.'
        Assert-Condition ('behavioral' -in @($profile.requiredEvidence)) 'A bugfix did not require behavioral evidence.'
        Assert-Condition ([string] $profile.status -eq 'known') 'A recognized task was marked unknown.'

        $unknown = Get-CodexTaskProfile -Policy $loadedPolicy -Prompt '帮我处理一下这个'
        Assert-Condition ([string] $unknown.status -eq 'U') 'An unrecognized prompt did not enter U state.'
        $base = New-BaseEvent 'suite-18'
        [void] (Invoke-Hook 'SessionStart.ps1' ($base + @{ hook_event_name = 'SessionStart'; source = 'startup'; model = 'test-model' }))
        $promptOutput = Invoke-Hook 'UserPromptSubmit.ps1' ($base + @{ hook_event_name = 'UserPromptSubmit'; prompt = '修复后端错误' }) | ConvertFrom-Json
        Assert-Condition ([string] $promptOutput.hookSpecificOutput.additionalContext -match "-SessionId 'suite-18'") 'Prompt routing did not provide a session-aware verification command.'
        $state = Read-PolicySessionState -Policy $loadedPolicy -SessionId 'suite-18'
        Assert-Condition ('bugfix' -in @($state.taskTypes)) 'Prompt routing did not persist the selected task type.'
    }

    Invoke-TestSuite 19 'behavioral evidence is distinct from structural success' {
        . (Join-Path $PSScriptRoot 'TaskRouting.ps1')
        . (Join-Path $PSScriptRoot 'VerificationSelection.ps1')
        $loadedPolicy = Get-PolicyData -PolicyPath $script:testPolicyPath
        $profile = Get-CodexTaskProfile -Policy $loadedPolicy -Prompt '修复后端错误' -ChangedFiles @('JCS.BizLogics/Logic.fs')
        $plan = @(Get-CodexVerificationPlan -Policy $loadedPolicy -ChangedFiles @('JCS.BizLogics/Logic.fs') -RiskLevel 'L1' -TaskTypes @($profile.taskTypes))
        $structural = @([pscustomobject]@{ name = 'jcs-bizlogics-release-build'; success = $true; evidenceLevels = @('structural') })
        $partial = Get-CodexVerificationCoverage -Policy $loadedPolicy -ChangedFiles @('JCS.BizLogics/Logic.fs') -Plan $plan -TaskProfile $profile -Results $structural
        Assert-Condition ([string] $partial.confidence -eq 'partial') 'Structural success incorrectly claimed behavioral coverage.'
        Assert-Condition ('behavioral' -in @($partial.missingEvidence)) 'Missing behavioral evidence was not reported.'

        $behavioral = @($structural) + [pscustomobject]@{ name = 'focused-regression'; success = $true; evidenceLevels = @('behavioral') }
        $complete = Get-CodexVerificationCoverage -Policy $loadedPolicy -ChangedFiles @('JCS.BizLogics/Logic.fs') -Plan $plan -TaskProfile $profile -Results $behavioral
        Assert-Condition ([string] $complete.confidence -eq 'behavioral') 'Focused behavior evidence did not raise confidence to behavioral.'
    }

    $failed = @($script:suiteResults | Where-Object { -not $_.passed })
    $totalSuites = 19
    Write-Output ("Policy suites: {0}/{1} passed" -f ($totalSuites - $failed.Count), $totalSuites)
    if ($failed.Count -gt 0) { throw "$($failed.Count) policy test suite(s) failed." }
}
finally {
    $env:CODEX_POLICY_STATE_ROOT = $oldStateRoot
    $env:CODEX_POLICY_AUDIT_ROOT = $oldAuditRoot
    $env:CODEX_POLICY_MODE_OVERRIDE = $oldMode
    $resolvedTestRoot = [System.IO.Path]::GetFullPath($testRoot)
    $resolvedTemp = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
    if ($resolvedTestRoot.StartsWith($resolvedTemp, [StringComparison]::OrdinalIgnoreCase) -and (Test-Path -LiteralPath $resolvedTestRoot)) {
        Remove-Item -LiteralPath $resolvedTestRoot -Recurse -Force
    }
}
