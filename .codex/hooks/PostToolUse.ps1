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
        $state = [pscustomobject]@{
            dirtyAt = $null
            dirtyTool = $null
            highestRisk = [string] $policy.riskClassification.default
            verifiedAt = $null
            verificationAttemptedAt = $null
            verificationSuccess = $false
        }
    }
    elseif ($null -eq $state.PSObject.Properties['highestRisk']) {
        $state | Add-Member -NotePropertyName highestRisk -NotePropertyValue ([string] $policy.riskClassification.default)
    }

    $now = (Get-Date).ToUniversalTime().ToString('o')
    Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
        timestamp = $now
        event = 'PostToolUse'
        sessionId = $sessionId
        turnId = [string] $eventData.turn_id
        toolName = $toolName
        success = [bool] $succeeded
        inputSha256 = Get-PolicySha256 $toolText
    })
    if (Test-PolicyVerificationCommand -Policy $policy -ToolText $toolText) {
        $state.verificationAttemptedAt = $now
        $state.verificationSuccess = [bool] $succeeded
        if ($succeeded) { $state.verifiedAt = $now }
        Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
        Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
            timestamp = $now
            event = 'Verification'
            sessionId = $sessionId
            riskLevel = [string] $state.highestRisk
            success = [bool] $succeeded
            inputSha256 = Get-PolicySha256 $toolText
        })
        exit 0
    }

    if ($succeeded -and (Test-PolicyMutatingTool -ToolName $toolName -ToolText $toolText)) {
        $state.dirtyAt = $now
        $state.dirtyTool = $toolName
        $toolRisk = Get-PolicyRiskLevelForTool -Policy $policy -ToolName $toolName -ToolText $toolText
        $state.highestRisk = Get-PolicyHigherRisk -Left ([string] $state.highestRisk) -Right $toolRisk
        $state.verificationSuccess = $false
        Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
    }
}
catch {
    Write-PolicyHookJson ([ordered]@{ systemMessage = "Post-tool JCS policy tracking failed: $($_.Exception.Message)"; hookSpecificOutput = [ordered]@{ hookEventName = 'PostToolUse'; additionalContext = 'Treat the JCS session as modified and run its declared verification entry point before completion.' } })
    exit 0
}
