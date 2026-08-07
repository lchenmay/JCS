param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
$policy = Get-Content -Raw -LiteralPath $PolicyPath | ConvertFrom-Json
$testRoot = Join-Path ([System.IO.Path]::GetTempPath()) ("codex-policy-test-" + [guid]::NewGuid().ToString('N'))
New-Item -ItemType Directory -Path $testRoot | Out-Null
$oldStateRoot = $env:CODEX_POLICY_STATE_ROOT
$oldMode = $env:CODEX_POLICY_MODE_OVERRIDE
$env:CODEX_POLICY_STATE_ROOT = Join-Path $testRoot 'state'
$env:CODEX_POLICY_MODE_OVERRIDE = 'enforce'

function Invoke-Hook {
    param([string] $ScriptName, [object] $Event)
    $json = $Event | ConvertTo-Json -Depth 16 -Compress
    $output = $json | & powershell.exe -NoProfile -ExecutionPolicy Bypass -File (Join-Path $PSScriptRoot $ScriptName) -PolicyPath $script:testPolicyPath
    if ($LASTEXITCODE -ne 0) { throw "$ScriptName exited with $LASTEXITCODE" }
    return ($output | Out-String).Trim()
}

function Assert-True {
    param([bool] $Condition, [string] $Name)
    if (-not $Condition) { throw "FAILED: $Name" }
    Write-Output "PASS: $Name"
}

try {
    $policy.mode = 'enforce'
    $policy.generatedPathFragments = @('generated/output.txt')
    $policy.siblingRepositoryRoots = @('D:\DEV\Sibling')
    $policy.directGeneratorFragments = @('unsafe-generator.exe')
    $policy.trustedGeneratorFragments = @('scripts/regenerate-types.ps1')
    $policy.completionGate.enabled = $true
    $policy.verificationCommand = 'scripts/codex-verify.ps1'
    $script:testPolicyPath = Join-Path $testRoot 'policy.json'
    $policy | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath $script:testPolicyPath -Encoding UTF8

    $base = @{ session_id = 'test-session'; turn_id = 'turn-1'; cwd = [string] $policy.repositoryRoot; permission_mode = 'default' }
    $safe = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: src/safe.txt' } }
    Assert-True -Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $safe))) -Name 'safe patch is allowed'

    $generated = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: generated/output.txt' } }
    $generatedResult = Invoke-Hook 'PreToolUse.ps1' $generated | ConvertFrom-Json
    Assert-True -Condition ($generatedResult.hookSpecificOutput.permissionDecision -eq 'deny') -Name 'generated file edit is denied'

    $hardReset = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'git reset --hard HEAD~1' } }
    $hardResetResult = Invoke-Hook 'PreToolUse.ps1' $hardReset | ConvertFrom-Json
    Assert-True -Condition ($hardResetResult.hookSpecificOutput.permissionDecision -eq 'deny') -Name 'hard reset is denied'

    $sibling = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: D:\DEV\Sibling\src\x.txt' } }
    Assert-True -Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $sibling))) -Name 'outside-repository patch is not governed'

    $outsideDelete = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'Remove-Item -LiteralPath C:\Temp\outside -Recurse' } }
    Assert-True -Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'PreToolUse.ps1' $outsideDelete))) -Name 'outside-repository command is not governed'

    $directGenerator = $base + @{ hook_event_name = 'PreToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'unsafe-generator.exe --write' } }
    $generatorResult = Invoke-Hook 'PreToolUse.ps1' $directGenerator | ConvertFrom-Json
    Assert-True -Condition ($generatorResult.hookSpecificOutput.permissionDecision -eq 'deny') -Name 'direct generator execution is denied'

    $postWrite = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: src/safe.txt' }; tool_response = @{ exitCode = 0 } }
    [void] (Invoke-Hook 'PostToolUse.ps1' $postWrite)
    $stop = $base + @{ hook_event_name = 'Stop'; stop_hook_active = $false; last_assistant_message = 'done' }
    $stopResult = Invoke-Hook 'Stop.ps1' $stop | ConvertFrom-Json
    Assert-True -Condition ($stopResult.decision -eq 'block') -Name 'stop is blocked after an unverified write'

    $postVerify = $base + @{ hook_event_name = 'PostToolUse'; tool_name = 'Bash'; tool_input = @{ command = 'powershell -File scripts/codex-verify.ps1' }; tool_response = @{ exitCode = 0 } }
    [void] (Invoke-Hook 'PostToolUse.ps1' $postVerify)
    Assert-True -Condition ([string]::IsNullOrWhiteSpace((Invoke-Hook 'Stop.ps1' $stop))) -Name 'successful verification opens completion gate'

    $postWrite.turn_id = 'turn-2'
    [void] (Invoke-Hook 'PostToolUse.ps1' $postWrite)
    $postVerify.tool_response = @{ exitCode = 1 }
    [void] (Invoke-Hook 'PostToolUse.ps1' $postVerify)
    $secondStop = $base + @{ hook_event_name = 'Stop'; stop_hook_active = $true; last_assistant_message = 'blocked' }
    $secondStopResult = Invoke-Hook 'Stop.ps1' $secondStop | ConvertFrom-Json
    Assert-True -Condition ([string]::IsNullOrWhiteSpace([string] $secondStopResult.decision)) -Name 'second stop can report a genuine verification blocker'

    $env:CODEX_POLICY_MODE_OVERRIDE = 'audit'
    $auditBase = @{ session_id = 'audit-session'; turn_id = 'turn-1'; cwd = [string] $policy.repositoryRoot; permission_mode = 'default' }
    $auditWrite = $auditBase + @{ hook_event_name = 'PostToolUse'; tool_name = 'apply_patch'; tool_input = @{ command = '*** Update File: src/audit.txt' }; tool_response = @{ exitCode = 0 } }
    [void] (Invoke-Hook 'PostToolUse.ps1' $auditWrite)
    $auditStop = $auditBase + @{ hook_event_name = 'Stop'; stop_hook_active = $false; last_assistant_message = 'done' }
    $auditStopResult = Invoke-Hook 'Stop.ps1' $auditStop | ConvertFrom-Json
    Assert-True -Condition ([string]::IsNullOrWhiteSpace([string] $auditStopResult.decision)) -Name 'audit mode never blocks completion'

    Write-Output 'Policy tests passed: 10'
}
finally {
    $env:CODEX_POLICY_STATE_ROOT = $oldStateRoot
    $env:CODEX_POLICY_MODE_OVERRIDE = $oldMode
    $resolvedTestRoot = [System.IO.Path]::GetFullPath($testRoot)
    $resolvedTemp = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
    if ($resolvedTestRoot.StartsWith($resolvedTemp, [StringComparison]::OrdinalIgnoreCase) -and (Test-Path -LiteralPath $resolvedTestRoot)) {
        Remove-Item -LiteralPath $resolvedTestRoot -Recurse -Force
    }
}
