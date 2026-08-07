param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'PolicyCommon.ps1')

try {
    $rawInput = [Console]::In.ReadToEnd()
    if ([string]::IsNullOrWhiteSpace($rawInput)) { exit 0 }
    $eventData = $rawInput | ConvertFrom-Json
    $policy = Get-PolicyData -PolicyPath $PolicyPath
    $mode = Get-PolicyMode -Policy $policy
    if (-not (Test-PolicyEventInRepository -Policy $policy -EventData $eventData -ToolText '')) { exit 0 }
    if (-not [bool] $policy.completionGate.enabled) { exit 0 }

    $state = Read-PolicySessionState -Policy $policy -SessionId ([string] $eventData.session_id)
    if ($null -eq $state -or [string]::IsNullOrWhiteSpace([string] $state.dirtyAt)) { exit 0 }

    $verifiedAfterWrite = $false
    if ([bool] $state.verificationSuccess -and -not [string]::IsNullOrWhiteSpace([string] $state.verifiedAt)) {
        $verifiedAfterWrite = ([DateTimeOffset]::Parse([string] $state.verifiedAt) -ge [DateTimeOffset]::Parse([string] $state.dirtyAt))
    }
    if ($verifiedAfterWrite) { exit 0 }

    $verifyCommand = [string] $policy.verificationCommand
    if ($mode -ne 'enforce') {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "$($policy.repositoryName) policy audit: repository changes are not yet verified. Suggested command: $verifyCommand" })
        exit 0
    }
    if ([bool] $eventData.stop_hook_active) {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "Verification remains missing or failed for $($policy.repositoryName). The final response must state this explicitly and must not claim successful completion." })
        exit 0
    }

    $reason = "Repository changes were detected after the last successful verification. Run $verifyCommand. Fix failures where safe; if a genuine blocker remains, report the exact failed check and stop again without claiming success."
    Write-PolicyHookJson ([ordered]@{ decision = 'block'; reason = $reason })
}
catch {
    if ($mode -eq 'enforce') {
        Write-PolicyHookJson ([ordered]@{ decision = 'block'; reason = "Completion gate failed closed: $($_.Exception.Message). Repair the hook or explicitly report that verification enforcement is unavailable." })
    }
    else {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "Completion audit is unavailable: $($_.Exception.Message)" })
    }
    exit 0
}
