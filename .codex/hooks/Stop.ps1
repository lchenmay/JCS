param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
$mode = 'audit'
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
    Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
        timestamp = (Get-Date).ToUniversalTime().ToString('o')
        event = 'Stop'
        mode = $mode
        sessionId = [string] $eventData.session_id
        hasState = $null -ne $state
        stopHookActive = [bool] $eventData.stop_hook_active
    })
    if ($null -eq $state -or [string]::IsNullOrWhiteSpace([string] $state.dirtyAt)) { exit 0 }
    $verifiedAfterWrite = $false
    if ([bool] $state.verificationSuccess -and -not [string]::IsNullOrWhiteSpace([string] $state.verifiedAt)) {
        $verifiedAfterWrite = ([DateTimeOffset]::Parse([string] $state.verifiedAt) -ge [DateTimeOffset]::Parse([string] $state.dirtyAt))
    }
    if ($verifiedAfterWrite) { exit 0 }

    $name = Get-PolicyRepositoryName -Policy $policy
    $verifyCommand = Get-PolicyVerificationCommand -Policy $policy
    $risk = if ([string]::IsNullOrWhiteSpace([string] $state.highestRisk)) { [string] $policy.riskClassification.default } else { [string] $state.highestRisk }
    if ($mode -ne [string] $policy.completionGate.blockingMode) {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "$name policy audit ($risk): repository changes are not yet verified. Suggested command: $verifyCommand" })
        exit 0
    }
    if ([bool] $eventData.stop_hook_active) {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "Verification remains missing or failed for $name ($risk). The final response must state this explicitly and must not claim successful validation." })
        exit 0
    }

    $reason = "Repository changes at risk $risk were detected after the last successful verification. Run $verifyCommand. Fix safe failures or report the exact unresolved check without claiming validation success."
    Write-PolicyHookJson ([ordered]@{ decision = 'block'; reason = $reason })
}
catch {
    if ($mode -eq 'enforce') {
        Write-PolicyHookJson ([ordered]@{ decision = 'block'; reason = "JCS completion gate failed closed: $($_.Exception.Message). Repair the project hook or explicitly report that verification enforcement is unavailable." })
    }
    else {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "JCS completion audit is unavailable: $($_.Exception.Message)" })
    }
    exit 0
}
