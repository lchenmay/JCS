param([Parameter(Mandatory = $true)] [string] $PolicyPath)

$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'PolicyCommon.ps1')

try {
    $rawInput = [Console]::In.ReadToEnd()
    if ([string]::IsNullOrWhiteSpace($rawInput)) { exit 0 }
    $eventData = $rawInput | ConvertFrom-Json
    $policy = Get-PolicyData -PolicyPath $PolicyPath
    if (-not (Test-PolicyEventInRepository -Policy $policy -EventData $eventData -ToolText '')) { exit 0 }

    $changedFiles = Get-PolicyChangedFiles -Policy $policy
    $risk = Get-PolicyRiskLevelForPaths -Policy $policy -Paths $changedFiles
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
        highestRisk = $risk
        verifiedAt = $null
        verificationAttemptedAt = $null
        verificationSuccess = $false
    }
    Write-PolicySessionState -Policy $policy -SessionId ([string] $eventData.session_id) -State $state
    Write-PolicyAuditRecord -Policy $policy -Record ([ordered]@{
        timestamp = $now
        event = 'SessionStart'
        mode = Get-PolicyMode -Policy $policy
        riskLevel = $risk
        sessionId = [string] $eventData.session_id
        source = [string] $eventData.source
        changedFileCount = $changedFiles.Count
        statusSha256 = $statusHash
    })

    $name = Get-PolicyRepositoryName -Policy $policy
    $mode = Get-PolicyMode -Policy $policy
    $verifyCommand = Get-PolicyVerificationCommand -Policy $policy
    $permissionMode = [string] $eventData.permission_mode
    $context = "$name project policy active: mode=$mode, risk=$risk, changed-files=$($changedFiles.Count), write-scope=repository-only, outside-repository=deny. Existing changed paths are protected from reset/restore/delete. Dependency repositories are read-only. Runtime permission mode '$permissionMode' is not managed by this repository. Verification entry point: $verifyCommand."
    Write-PolicyHookJson ([ordered]@{
        hookSpecificOutput = [ordered]@{
            hookEventName = 'SessionStart'
            additionalContext = $context
        }
    })
}
catch {
    Write-PolicyHookJson ([ordered]@{ systemMessage = "JCS session policy preflight failed: $($_.Exception.Message). Project hooks are not a substitute for the runtime permission boundary." })
    exit 0
}
