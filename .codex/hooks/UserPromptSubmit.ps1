param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
[Console]::InputEncoding = [System.Text.UTF8Encoding]::new($false)
. (Join-Path $PSScriptRoot 'PolicyCommon.ps1')
. (Join-Path $PSScriptRoot 'TaskRouting.ps1')

try {
    $rawInput = [Console]::In.ReadToEnd()
    if ([string]::IsNullOrWhiteSpace($rawInput)) { exit 0 }
    $eventData = $rawInput | ConvertFrom-Json
    $policy = Get-PolicyData -PolicyPath $PolicyPath
    if (-not [bool] $policy.taskRouting.enabled) { exit 0 }
    if (-not (Test-PolicyEventInRepository -Policy $policy -EventData $eventData -ToolText '')) { exit 0 }

    $sessionId = [string] $eventData.session_id
    $prompt = [string] $eventData.prompt
    $profile = Get-CodexTaskProfile -Policy $policy -Prompt $prompt
    $state = Read-PolicySessionState -Policy $policy -SessionId $sessionId
    if ($null -eq $state) {
        $changedFiles = Get-PolicyChangedFiles -Policy $policy
        $state = New-PolicySessionState -Policy $policy -ChangedFiles $changedFiles
    }
    foreach ($property in @{
        taskProfileStatus = [string] $profile.status
        taskTypes = @($profile.taskTypes)
        selectedRulePacks = @($profile.selectedRulePacks)
        requiredEvidence = @($profile.requiredEvidence)
        taskPromptSha256 = Get-PolicySha256 $prompt
        focusedDiagnosticAttempts = 0
    }.GetEnumerator()) {
        if ($null -eq $state.PSObject.Properties[$property.Key]) {
            $state | Add-Member -NotePropertyName $property.Key -NotePropertyValue $property.Value
        }
        else { $state.($property.Key) = $property.Value }
    }
    $state.highestRisk = Get-PolicyHigherRisk -Left ([string] $state.highestRisk) -Right ([string] $profile.inferredRisk)
    Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
    Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
        timestamp = (Get-Date).ToUniversalTime().ToString('o')
        event = 'UserPromptSubmit'
        sessionId = $sessionId
        promptSha256 = Get-PolicySha256 $prompt
        profileStatus = [string] $profile.status
        taskTypes = @($profile.taskTypes)
        selectedRulePacks = @($profile.selectedRulePacks)
        requiredEvidence = @($profile.requiredEvidence)
        riskLevel = [string] $profile.inferredRisk
    })

    $taskText = if (@($profile.taskTypes).Count -eq 0) { 'unknown' } else { @($profile.taskTypes) -join ',' }
    $evidenceText = if (@($profile.requiredEvidence).Count -eq 0) { 'none-declared' } else { @($profile.requiredEvidence) -join ',' }
    $verifyCommand = "scripts/codex-verify.ps1 -SessionId '$sessionId'$(ConvertTo-CodexTaskTypeArgument -TaskTypes @($profile.taskTypes))"
    $unknownText = if ([string] $profile.status -eq [string] $policy.taskRouting.unknownStatus) { ' Intent status=U: do not claim behavioral coverage without a focused check.' } else { '' }
    Write-PolicyHookJson ([ordered]@{
        hookSpecificOutput = [ordered]@{
            hookEventName = 'UserPromptSubmit'
            additionalContext = "JCS task route: types=$taskText; packs=$(@($profile.selectedRulePacks) -join ','); risk=$($profile.inferredRisk); required-evidence=$evidenceText.$unknownText Verify with $verifyCommand"
        }
    })
}
catch {
    Write-PolicyHookJson ([ordered]@{
        hookSpecificOutput = [ordered]@{
            hookEventName = 'UserPromptSubmit'
            additionalContext = "JCS task routing is unavailable: $($_.Exception.Message). Treat intent as U, keep the repository boundary, and use focused evidence instead of broad retries."
        }
    })
    exit 0
}
