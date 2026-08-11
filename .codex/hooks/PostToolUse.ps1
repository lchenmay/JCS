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
        $state = New-PolicySessionState -Policy $policy -ChangedFiles @(Get-PolicyChangedFiles -Policy $policy)
    }
    elseif ($null -eq $state.PSObject.Properties['highestRisk']) {
        $state | Add-Member -NotePropertyName highestRisk -NotePropertyValue ([string] $policy.riskClassification.default)
    }
    if ($null -eq $state.PSObject.Properties['baselineChangedFiles']) { $state | Add-Member -NotePropertyName baselineChangedFiles -NotePropertyValue @() }
    if ($null -eq $state.PSObject.Properties['baselinePreserved']) { $state | Add-Member -NotePropertyName baselinePreserved -NotePropertyValue $true }
    if ($null -eq $state.PSObject.Properties['verificationConfidence']) { $state | Add-Member -NotePropertyName verificationConfidence -NotePropertyValue 'not-run' }
    if ($null -eq $state.PSObject.Properties['verificationFailureSignature']) { $state | Add-Member -NotePropertyName verificationFailureSignature -NotePropertyValue $null }
    if ($null -eq $state.PSObject.Properties['repeatedVerificationFailures']) { $state | Add-Member -NotePropertyName repeatedVerificationFailures -NotePropertyValue 0 }
    if ($null -eq $state.PSObject.Properties['verificationRecommendedActions']) { $state | Add-Member -NotePropertyName verificationRecommendedActions -NotePropertyValue @() }
    if ($null -eq $state.PSObject.Properties['taskProfileStatus']) { $state | Add-Member -NotePropertyName taskProfileStatus -NotePropertyValue ([string] $policy.taskRouting.unknownStatus) }
    if ($null -eq $state.PSObject.Properties['taskTypes']) { $state | Add-Member -NotePropertyName taskTypes -NotePropertyValue @() }
    if ($null -eq $state.PSObject.Properties['selectedRulePacks']) { $state | Add-Member -NotePropertyName selectedRulePacks -NotePropertyValue @('core') }
    if ($null -eq $state.PSObject.Properties['requiredEvidence']) { $state | Add-Member -NotePropertyName requiredEvidence -NotePropertyValue @() }
    if ($null -eq $state.PSObject.Properties['focusedDiagnosticAttempts']) { $state | Add-Member -NotePropertyName focusedDiagnosticAttempts -NotePropertyValue 0 }

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
        $receipt = $null
        $receiptRelativePath = Get-PolicyVerificationReceiptRelativePath -Policy $policy -SessionId $sessionId
        $receiptPath = Join-Path (Get-PolicyRepositoryRoot -Policy $policy) $receiptRelativePath
        if (-not (Test-Path -LiteralPath $receiptPath)) {
            $receiptPath = Join-Path (Get-PolicyRepositoryRoot -Policy $policy) ([string] $policy.verification.receiptPath)
        }
        if (Test-Path -LiteralPath $receiptPath) {
            try { $receipt = Get-Content -Raw -LiteralPath $receiptPath | ConvertFrom-Json } catch { $receipt = $null }
        }
        $expectedTaskTypes = @($state.taskTypes | ForEach-Object { ([string] $_).ToLowerInvariant() } | Sort-Object -Unique)
        $receiptTaskTypes = if ($null -ne $receipt) { @($receipt.taskProfile.taskTypes | ForEach-Object { ([string] $_).ToLowerInvariant() } | Sort-Object -Unique) } else { @() }
        $missingTaskTypes = @($expectedTaskTypes | Where-Object { $_ -notin $receiptTaskTypes })
        $profileMatches = $expectedTaskTypes.Count -eq 0 -or $missingTaskTypes.Count -eq 0
        $state.verificationSuccess = [bool] $succeeded -and $profileMatches
        if ($state.verificationSuccess) { $state.verifiedAt = $now }
        $state.verificationConfidence = if ($null -ne $receipt -and -not [string]::IsNullOrWhiteSpace([string] $receipt.verificationConfidence)) { [string] $receipt.verificationConfidence } else { 'unknown' }
        $state.verificationRecommendedActions = if ($null -ne $receipt) { @($receipt.recommendedActions | ForEach-Object { [string] $_ }) } else { @('Verification produced no readable receipt; report confidence as unknown.') }
        if (-not $profileMatches) {
            $state.verificationConfidence = 'partial'
            $state.verificationRecommendedActions = @($state.verificationRecommendedActions) + "Re-run verification with the task profile types: $($expectedTaskTypes -join ',')."
        }
        if ($state.verificationSuccess) {
            $state.verificationFailureSignature = $null
            $state.repeatedVerificationFailures = 0
        }
        else {
            $failureText = if ($null -ne $receipt) {
                (@($receipt.checks | Where-Object { -not [bool] $_.passed } | ForEach-Object { "$($_.name):$($_.failureKind)" }) -join "`n")
            }
            else { 'verification-command-failed' }
            if ([string]::IsNullOrWhiteSpace($failureText)) { $failureText = 'verification-command-failed' }
            $signature = Get-PolicySha256 $failureText
            if ([string] $state.verificationFailureSignature -eq $signature) {
                $state.repeatedVerificationFailures = [int] $state.repeatedVerificationFailures + 1
            }
            else {
                $state.verificationFailureSignature = $signature
                $state.repeatedVerificationFailures = 1
            }
            if ([int] $state.repeatedVerificationFailures -ge [int] $policy.taskRouting.maxFocusedDiagnosticAttempts) {
                $state.taskProfileStatus = [string] $policy.taskRouting.unknownStatus
            }
        }
        Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
        Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
            timestamp = $now
            event = 'Verification'
            sessionId = $sessionId
            riskLevel = [string] $state.highestRisk
            success = [bool] $state.verificationSuccess
            confidence = [string] $state.verificationConfidence
            taskProfileMatched = [bool] $profileMatches
            missingTaskTypes = @($missingTaskTypes)
            repeatedFailureCount = [int] $state.repeatedVerificationFailures
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
        $state.verificationConfidence = 'stale'
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
