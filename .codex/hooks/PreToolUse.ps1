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
    $toolName = [string] $eventData.tool_name
    $toolText = Get-PolicyToolText -EventData $eventData
    if (-not (Test-PolicyEventInRepository -Policy $policy -EventData $eventData -ToolText $toolText)) { exit 0 }

    $sessionId = [string] $eventData.session_id
    $state = Read-PolicySessionState -Policy $policy -SessionId $sessionId
    if ($null -eq $state) {
        $changedFiles = @(Get-PolicyChangedFiles -Policy $policy)
        $baselineRisk = Get-PolicyRiskLevelForPaths -Policy $policy -Paths $changedFiles
        $now = (Get-Date).ToUniversalTime().ToString('o')
        $statusHash = Get-PolicySha256 ($changedFiles -join "`n")
        $state = [pscustomobject]@{
            baselineAt = $now
            baselineChangedFileCount = $changedFiles.Count
            baselineChangedFiles = @($changedFiles)
            baselineStatusSha256 = $statusHash
            baselinePreserved = $true
            dirtyAt = $null
            dirtyTool = $null
            highestRisk = $baselineRisk
            verifiedAt = $null
            verificationAttemptedAt = $null
            verificationSuccess = $false
        }
        Write-PolicySessionState -Policy $policy -SessionId $sessionId -State $state
        Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
            timestamp = $now
            event = 'SessionBootstrap'
            mode = $mode
            riskLevel = $baselineRisk
            sessionId = $sessionId
            source = 'pre-tool-fallback'
            changedFileCount = $changedFiles.Count
            statusSha256 = $statusHash
        })
    }

    $findings = New-Object System.Collections.Generic.List[object]
    $risk = [string] $policy.riskClassification.default
    foreach ($rule in @($policy.rules)) {
        if (Test-PolicyRuleMatch -Policy $policy -Rule $rule -ToolName $toolName -ToolText $toolText) {
            $findings.Add([pscustomobject]@{
                rule = [string] $rule.name
                severity = [string] $rule.severity
                risk = [string] $rule.risk
                detail = [string] $rule.message
            })
            $risk = Get-PolicyHigherRisk -Left $risk -Right ([string] $rule.risk)
        }
    }
    $baselinePaths = if ($null -ne $state.PSObject.Properties['baselineChangedFiles']) { @($state.baselineChangedFiles) } else { @() }
    if (Test-PolicyBaselineDestructiveCommand -Policy $policy -ToolText $toolText -BaselinePaths $baselinePaths) {
        $findings.Add([pscustomobject]@{
            rule = 'baseline-preservation'
            severity = 'deny'
            risk = 'L3'
            detail = 'This command can discard paths that were already modified when the task started.'
        })
        $risk = Get-PolicyHigherRisk -Left $risk -Right 'L3'
    }
    Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
        timestamp = (Get-Date).ToUniversalTime().ToString('o')
        event = 'PreToolUse'
        mode = $mode
        riskLevel = $risk
        sessionId = [string] $eventData.session_id
        turnId = [string] $eventData.turn_id
        toolName = $toolName
        inputSha256 = Get-PolicySha256 $toolText
        findings = @($findings | ForEach-Object { $_.rule })
    })

    if ($findings.Count -eq 0) { exit 0 }

    $deny = @($findings | Where-Object { $_.severity -eq 'deny' })
    $summary = ($findings | ForEach-Object { $_.detail } | Select-Object -Unique) -join ' '
    if ($mode -eq [string] $policy.completionGate.blockingMode -and $deny.Count -gt 0) {
        Write-PolicyHookJson ([ordered]@{
            hookSpecificOutput = [ordered]@{
                hookEventName = 'PreToolUse'
                permissionDecision = 'deny'
                permissionDecisionReason = "$(Get-PolicyRepositoryName -Policy $policy) policy blocked this $risk operation. $summary"
            }
        })
        exit 0
    }

    Write-PolicyHookJson ([ordered]@{
        hookSpecificOutput = [ordered]@{
            hookEventName = 'PreToolUse'
            additionalContext = "$(Get-PolicyRepositoryName -Policy $policy) policy ($mode, $risk): $summary"
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
                permissionDecisionReason = 'The JCS enforcement hook failed closed. Repair or disable the reviewed project hook before retrying.'
            }
        })
    }
    else {
        Write-PolicyHookJson ([ordered]@{ systemMessage = $message; hookSpecificOutput = [ordered]@{ hookEventName = 'PreToolUse'; additionalContext = 'JCS policy auditing is unavailable; avoid protected writes.' } })
    }
    exit 0
}
