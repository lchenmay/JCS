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
            baselineChangedFiles = @()
            baselinePreserved = $true
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
    if ($null -eq $state.PSObject.Properties['baselineChangedFiles']) { $state | Add-Member -NotePropertyName baselineChangedFiles -NotePropertyValue @() }
    if ($null -eq $state.PSObject.Properties['baselinePreserved']) { $state | Add-Member -NotePropertyName baselinePreserved -NotePropertyValue $true }

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
        $currentChangedFiles = @(Get-PolicyChangedFiles -Policy $policy)
        $missingBaseline = @($state.baselineChangedFiles | Where-Object { $_ -notin $currentChangedFiles })
        if ($missingBaseline.Count -gt 0) {
            $state.baselinePreserved = $false
            $state.highestRisk = Get-PolicyHigherRisk -Left ([string] $state.highestRisk) -Right 'L3'
            Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
                timestamp = $now
                event = 'BaselineViolation'
                sessionId = $sessionId
                riskLevel = 'L3'
                missingPathCount = $missingBaseline.Count
                missingPathsSha256 = Get-PolicySha256 ($missingBaseline -join "`n")
            })
        }
        Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
        if (-not [bool] $state.baselinePreserved) {
            Write-PolicyHookJson ([ordered]@{
                systemMessage = 'JCS baseline protection detected that pre-existing user changes disappeared. Stop work and report the affected paths; do not claim completion.'
                hookSpecificOutput = [ordered]@{
                    hookEventName = 'PostToolUse'
                    additionalContext = 'A pre-task changed path is no longer present in Git status. Completion is blocked even if verification later passes.'
                }
            })
        }
    }
}
catch {
    Write-PolicyHookJson ([ordered]@{ systemMessage = "Post-tool JCS policy tracking failed: $($_.Exception.Message)"; hookSpecificOutput = [ordered]@{ hookEventName = 'PostToolUse'; additionalContext = 'Treat the JCS session as modified and run its declared verification entry point before completion.' } })
    exit 0
}
