param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
$mode = 'enforce'
. (Join-Path $PSScriptRoot 'PolicyCommon.ps1')

try {
    $rawInput = [Console]::In.ReadToEnd()
    if ([string]::IsNullOrWhiteSpace($rawInput)) { exit 0 }
    $eventData = $rawInput | ConvertFrom-Json
    $policy = Get-PolicyData -PolicyPath $PolicyPath
    $mode = Get-PolicyMode -Policy $policy
    $toolName = [string] $eventData.tool_name
    $toolText = Get-PolicyToolText -EventData $eventData
    if (-not (Test-PolicyEventInRepository -Policy $policy -EventData $eventData -ToolText $toolText)) { exit 0 }
    $normalized = ConvertTo-PolicyNormalizedText $toolText
    $findings = New-Object System.Collections.Generic.List[object]
    $isFileEdit = $toolName -in @('apply_patch', 'Edit', 'Write')

    if ($isFileEdit) {
        foreach ($fragment in @($policy.generatedPathFragments)) {
            $needle = ConvertTo-PolicyNormalizedText ([string] $fragment)
            if (-not [string]::IsNullOrWhiteSpace($needle) -and $normalized.Contains($needle)) {
                $findings.Add([pscustomobject]@{ rule = 'generated-file-direct-edit'; severity = 'deny'; detail = "Use the controlled generator instead of editing $fragment directly." })
            }
        }
        if ($normalized -match '(?i)(^|[\s/\\])(?:\.env(?:\.|$)|[^\s]+\.(?:pem|key|pfx|p12))(?:[\s/\\]|$)') {
            $findings.Add([pscustomobject]@{ rule = 'secret-file-edit'; severity = 'deny'; detail = 'Editing secret-bearing files through Codex is blocked.' })
        }
    }

    $destructivePatterns = @(
        @{ Name = 'git-hard-reset'; Pattern = '(?i)\bgit(?:\.exe)?\s+(?:-c\s+\S+\s+)?reset\s+--hard\b'; Detail = 'Hard reset can discard user work.' },
        @{ Name = 'git-destructive-clean'; Pattern = '(?i)\bgit(?:\.exe)?\s+(?:-c\s+\S+\s+)?clean\s+[^\r\n]*(?:-fd|-df|-fdx|-dfx|--force)'; Detail = 'Destructive git clean can remove untracked work.' },
        @{ Name = 'git-force-push'; Pattern = '(?i)\bgit(?:\.exe)?\s+(?:-c\s+\S+\s+)?push\s+[^\r\n]*(?:--force(?:-with-lease)?|-f)\b'; Detail = 'Force push is forbidden.' },
        @{ Name = 'recursive-delete'; Pattern = '(?i)\b(?:remove-item|rm|rmdir|del)\b[^\r\n]*(?:-recurse|-rf|/s)\b'; Detail = 'Recursive delete is blocked; use a reviewed, recoverable operation.' },
        @{ Name = 'forceful-process-kill'; Pattern = '(?i)\b(?:taskkill(?:\.exe)?\b[^\r\n]*/f|stop-process\b[^\r\n]*-force)'; Detail = 'Forceful process termination is blocked.' }
    )
    foreach ($item in $destructivePatterns) {
        if ($toolText -match $item.Pattern) {
            $findings.Add([pscustomobject]@{ rule = $item.Name; severity = 'deny'; detail = $item.Detail })
        }
    }

    $trustedGenerator = $false
    foreach ($fragment in @($policy.trustedGeneratorFragments)) {
        if ($normalized.Contains((ConvertTo-PolicyNormalizedText ([string] $fragment)))) { $trustedGenerator = $true }
    }
    if (-not $trustedGenerator) {
        foreach ($fragment in @($policy.directGeneratorFragments)) {
            $needle = ConvertTo-PolicyNormalizedText ([string] $fragment)
            if (-not [string]::IsNullOrWhiteSpace($needle) -and $normalized.Contains($needle)) {
                $findings.Add([pscustomobject]@{ rule = 'direct-generator-execution'; severity = 'deny'; detail = 'Run the repository controlled-generation wrapper instead.' })
            }
        }
    }

    $externalPattern = '(?i)\b(ssh|scp|rsync|systemctl|restart-service|stop-service|psql)\b|\bdotnet\s+ef\s+database\s+update\b|\bdeploy(?:ment)?\b'
    if ($toolText -match $externalPattern) {
        $findings.Add([pscustomobject]@{ rule = 'external-side-effect'; severity = 'context'; detail = 'External, deployment, service, or database action requires an explicit current user request and a scoped approval.' })
    }

    if ($findings.Count -eq 0) { exit 0 }

    Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
        timestamp = (Get-Date).ToUniversalTime().ToString('o')
        event = 'PreToolUse'
        mode = $mode
        sessionId = [string] $eventData.session_id
        turnId = [string] $eventData.turn_id
        toolName = $toolName
        inputSha256 = Get-PolicySha256 $toolText
        findings = @($findings | ForEach-Object { $_.rule })
    })

    $deny = @($findings | Where-Object { $_.severity -eq 'deny' })
    $summary = ($findings | ForEach-Object { $_.detail } | Select-Object -Unique) -join ' '
    if ($mode -eq 'enforce' -and $deny.Count -gt 0) {
        Write-PolicyHookJson ([ordered]@{
            hookSpecificOutput = [ordered]@{
                hookEventName = 'PreToolUse'
                permissionDecision = 'deny'
                permissionDecisionReason = "$($policy.repositoryName) policy blocked this operation. $summary"
            }
        })
        exit 0
    }

    Write-PolicyHookJson ([ordered]@{
        hookSpecificOutput = [ordered]@{
            hookEventName = 'PreToolUse'
            additionalContext = "$($policy.repositoryName) policy ($mode): $summary"
        }
    })
}
catch {
    $message = "Policy hook failure: $($_.Exception.Message)"
    if ($mode -eq 'enforce') {
        Write-PolicyHookJson ([ordered]@{
            systemMessage = $message
            hookSpecificOutput = [ordered]@{
                hookEventName = 'PreToolUse'
                permissionDecision = 'deny'
                permissionDecisionReason = 'The enforcement hook failed closed. Repair or disable the reviewed hook before retrying.'
            }
        })
    }
    else {
        Write-PolicyHookJson ([ordered]@{ systemMessage = $message; hookSpecificOutput = [ordered]@{ hookEventName = 'PreToolUse'; additionalContext = 'Policy enforcement is unavailable; avoid protected writes.' } })
    }
    exit 0
}
