param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'PolicyCommon.ps1')

try {
    $rawInput = [Console]::In.ReadToEnd()
    if ([string]::IsNullOrWhiteSpace($rawInput)) { exit 0 }
    $eventData = $rawInput | ConvertFrom-Json
    $policy = Get-PolicyData -PolicyPath $PolicyPath
    if (-not [bool] $policy.completionGate.enabled) { exit 0 }

    $sessionId = [string] $eventData.session_id
    $toolName = [string] $eventData.tool_name
    $toolText = Get-PolicyToolText -EventData $eventData
    if (-not (Test-PolicyEventInRepository -Policy $policy -EventData $eventData -ToolText $toolText)) { exit 0 }
    $succeeded = Test-PolicyToolSucceeded -ToolResponse $eventData.tool_response
    $state = Read-PolicySessionState -Policy $policy -SessionId $sessionId
    if ($null -eq $state) {
        $state = [pscustomobject]@{ dirtyAt = $null; dirtyTool = $null; verifiedAt = $null; verificationAttemptedAt = $null; verificationSuccess = $false }
    }

    $now = (Get-Date).ToUniversalTime().ToString('o')
    if (Test-PolicyVerificationCommand -Policy $policy -ToolText $toolText) {
        $state.verificationAttemptedAt = $now
        $state.verificationSuccess = [bool] $succeeded
        if ($succeeded) { $state.verifiedAt = $now }
        Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
        Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{ timestamp = $now; event = 'Verification'; sessionId = $sessionId; success = [bool] $succeeded; inputSha256 = Get-PolicySha256 $toolText })
        exit 0
    }

    if ($succeeded -and (Test-PolicyMutatingTool -ToolName $toolName -ToolText $toolText)) {
        $state.dirtyAt = $now
        $state.dirtyTool = $toolName
        $state.verificationSuccess = $false
        Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
    }
}
catch {
    Write-PolicyHookJson ([ordered]@{ systemMessage = "Post-tool policy tracking failed: $($_.Exception.Message)"; hookSpecificOutput = [ordered]@{ hookEventName = 'PostToolUse'; additionalContext = 'Treat the session as modified and run the repository verification command before completion.' } })
    exit 0
}
