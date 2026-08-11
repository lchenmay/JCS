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
    if ($null -ne $state -and $null -ne $state.PSObject.Properties['baselinePreserved'] -and -not [bool] $state.baselinePreserved) {
        if ($mode -ne [string] $policy.completionGate.blockingMode -or [bool] $eventData.stop_hook_active) {
            Write-PolicyHookJson ([ordered]@{ systemMessage = 'JCS baseline protection failed: pre-existing user changes disappeared. The final response must identify this failure and must not claim successful completion.' })
            exit 0
        }
        Write-PolicyHookJson ([ordered]@{ decision = 'block'; reason = 'Pre-existing user changes disappeared during the task. Completion is blocked regardless of later verification.' })
        exit 0
    }
    if ($null -eq $state -or [string]::IsNullOrWhiteSpace([string] $state.dirtyAt)) { exit 0 }
    $verifiedAfterWrite = $false
    if ([bool] $state.verificationSuccess -and -not [string]::IsNullOrWhiteSpace([string] $state.verifiedAt)) {
        $verifiedAfterWrite = ([DateTimeOffset]::Parse([string] $state.verifiedAt) -ge [DateTimeOffset]::Parse([string] $state.dirtyAt))
    }
    if ($verifiedAfterWrite) {
        $confidence = if ($null -eq $state.PSObject.Properties['verificationConfidence']) { 'unknown' } else { [string] $state.verificationConfidence }
        $profileStatus = if ($null -eq $state.PSObject.Properties['taskProfileStatus']) { [string] $policy.taskRouting.unknownStatus } else { [string] $state.taskProfileStatus }
        if ($confidence -in @('none', 'partial', 'unknown') -or $profileStatus -eq [string] $policy.taskRouting.unknownStatus) {
            $actions = if ($null -ne $state.PSObject.Properties['verificationRecommendedActions']) { @($state.verificationRecommendedActions) -join ' ' } else { '' }
            Write-PolicyHookJson ([ordered]@{ systemMessage = "Verification completed with confidence=$confidence and task-status=$profileStatus. Do not claim complete behavioral validation. $actions" })
        }
        exit 0
    }

    $name = Get-PolicyRepositoryName -Policy $policy
    $verifyCommand = Get-PolicyVerificationCommand -Policy $policy
    $risk = if ([string]::IsNullOrWhiteSpace([string] $state.highestRisk)) { [string] $policy.riskClassification.default } else { [string] $state.highestRisk }
    $repeatCount = if ($null -eq $state.PSObject.Properties['repeatedVerificationFailures']) { 0 } else { [int] $state.repeatedVerificationFailures }
    $maxRepeats = [int] $policy.completionGate.maxRepeatedVerificationFailures
    if ($repeatCount -ge $maxRepeats) {
        $signature = if ($null -eq $state.PSObject.Properties['verificationFailureSignature']) { 'unknown' } else { [string] $state.verificationFailureSignature }
        Write-PolicyHookJson ([ordered]@{ systemMessage = "The same verification failure repeated $repeatCount times (signature=$signature). Stop broad retries, report the unresolved failure, and request focused evidence or user direction." })
        exit 0
    }
    if ($mode -ne [string] $policy.completionGate.blockingMode) {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "$name policy audit ($risk): repository changes are not yet verified. Suggested command: $verifyCommand" })
        exit 0
    }
    if ([bool] $eventData.stop_hook_active) {
        Write-PolicyHookJson ([ordered]@{ systemMessage = "Verification remains missing or failed for $name ($risk). The final response must state this explicitly and must not claim successful validation." })
        exit 0
    }

    $profileStatus = if ($null -eq $state.PSObject.Properties['taskProfileStatus']) { [string] $policy.taskRouting.unknownStatus } else { [string] $state.taskProfileStatus }
    $diagnostic = if ($profileStatus -eq [string] $policy.taskRouting.unknownStatus -or $repeatCount -gt 0) {
        'Use at most one hypothesis-driven -FocusedCheck diagnostic; do not repeat the unchanged broad verifier.'
    }
    else { 'Run the task-profile verification command before completion.' }
    $reason = "Repository changes at risk $risk were detected after the last successful verification. Run $verifyCommand. $diagnostic Fix safe failures or report the exact unresolved check without claiming validation success."
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
