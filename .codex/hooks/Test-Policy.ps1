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
        Assert-Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $outsidePatch))) 'An outside-repository patch was governed by JCS policy.'
        $outsideDelete = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'Remove-Item -LiteralPath C:\Temp\outside -Recurse' } }
        Assert-Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $outsideDelete))) 'An outside-repository delete was governed by JCS policy.'
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
        Assert-Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'Stop.ps1' $stop))) 'Successful verification did not open the completion gate.'
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

    $failed = @($script:suiteResults | Where-Object { -not $_.passed })
    Write-Output ("Policy suites: {0}/12 passed" -f (12 - $failed.Count))
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
