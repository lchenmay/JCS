function Get-CodexVerificationChangedFiles {
    param([Parameter(Mandatory = $true)] [object] $Policy)
    $ignored = @($Policy.verification.ignoredPaths | ForEach-Object { [string] $_ })
    return @(Get-PolicyChangedFiles -Policy $Policy | Where-Object {
        -not (Test-PolicyPathMatchesAnyPattern -Path ([string] $_) -Patterns $ignored)
    })
}

function Get-CodexVerificationPlan {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [string[]] $ChangedFiles = @(),
        [Parameter(Mandatory = $true)] [string] $RiskLevel,
        [string[]] $TaskTypes = @(),
        [string[]] $CheckNames = @(),
        [switch] $Full,
        [switch] $PolicyOnly
    )
    $riskRank = Get-PolicyRiskRank $RiskLevel
    $normalizedTaskTypes = @($TaskTypes | ForEach-Object { ([string] $_).ToLowerInvariant() })
    $normalizedCheckNames = @($CheckNames | ForEach-Object { ([string] $_).ToLowerInvariant() })
    $plan = @()
    foreach ($check in @($Policy.verification.checks)) {
        $reasons = New-Object System.Collections.Generic.List[string]
        $pathMatched = $false
        foreach ($changedFile in $ChangedFiles) {
            if (Test-PolicyPathMatchesAnyPattern -Path $changedFile -Patterns @($check.whenPaths)) {
                $pathMatched = $true
            }
        }
        $riskMatched = $false
        if ([bool] $check.runAtOrAbove -and -not [string]::IsNullOrWhiteSpace([string] $check.minimumRisk)) {
            $minimumRiskRank = Get-PolicyRiskRank ([string] $check.minimumRisk)
            $riskMatched = $riskRank -ge $minimumRiskRank
        }
        $taskMatched = @($check.whenTaskTypes | Where-Object { ([string] $_).ToLowerInvariant() -in $normalizedTaskTypes }).Count -gt 0

        $selected = $false
        if ($PolicyOnly) {
            $selected = [string] $check.category -eq 'policy'
            if ($selected) { $reasons.Add('policy-only') }
        }
        elseif ($Full) {
            $selected = $true
            $reasons.Add('full')
        }
        elseif ($normalizedCheckNames.Count -gt 0) {
            $selected = ([string] $check.name).ToLowerInvariant() -in $normalizedCheckNames
            if ($selected) { $reasons.Add('focused-check') }
        }
        else {
            if ([bool] $check.always) { $selected = $true; $reasons.Add('always') }
            if ($pathMatched) { $selected = $true; $reasons.Add('changed-path') }
            if ($riskMatched) { $selected = $true; $reasons.Add("risk-$RiskLevel") }
            if ($taskMatched) { $selected = $true; $reasons.Add('task-type') }
        }
        $plan += [pscustomobject]@{
            name = [string] $check.name
            category = [string] $check.category
            evidenceLevels = @($check.evidenceLevels | ForEach-Object { [string] $_ })
            selected = $selected
            reasons = @($reasons)
            check = $check
        }
    }
    return $plan
}

function Get-CodexVerificationCoverage {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [string[]] $ChangedFiles = @(),
        [object[]] $Plan = @(),
        [object] $TaskProfile = $null,
        [object[]] $Results = @()
    )
    $selectedNames = @($Plan | Where-Object { $_.selected } | ForEach-Object { [string] $_.name })
    $passedNames = if ($Results.Count -gt 0) {
        @($Results | Where-Object { [bool] $_.success } | ForEach-Object { [string] $_.name })
    }
    else { $selectedNames }
    $availableEvidence = if ($Results.Count -gt 0) {
        @($Results | Where-Object { [bool] $_.success } | ForEach-Object { @($_.evidenceLevels) } | ForEach-Object { [string] $_ } | Sort-Object -Unique)
    }
    else {
        @($Plan | Where-Object { $_.selected } | ForEach-Object { @($_.evidenceLevels) } | ForEach-Object { [string] $_ } | Sort-Object -Unique)
    }

    $files = @()
    foreach ($changedFile in $ChangedFiles) {
        $matchingRules = @($Policy.verification.coverageRules | Where-Object {
            Test-PolicyPathMatchesAnyPattern -Path ([string] $changedFile) -Patterns @($_.patterns)
        })
        $expectedChecks = @($matchingRules | ForEach-Object { @($_.checkNames) } | ForEach-Object { [string] $_ } | Sort-Object -Unique)
        $missingChecks = @($expectedChecks | Where-Object { $_ -notin $passedNames })
        $fileConfidence = if ($matchingRules.Count -eq 0) { 'none' } elseif ($missingChecks.Count -eq 0) { 'declared' } else { 'partial' }
        $files += [pscustomobject]@{
            path = $changedFile
            confidence = $fileConfidence
            coverageRules = @($matchingRules | ForEach-Object { [string] $_.name })
            expectedChecks = $expectedChecks
            missingChecks = $missingChecks
        }
    }

    $pathConfidence = if ($ChangedFiles.Count -eq 0) {
        'not-applicable'
    }
    elseif (@($files | Where-Object { $_.confidence -eq 'none' }).Count -gt 0) {
        if (@($files | Where-Object { $_.confidence -eq 'declared' }).Count -gt 0) { 'partial' } else { 'none' }
    }
    elseif (@($files | Where-Object { $_.confidence -eq 'partial' }).Count -gt 0) { 'partial' }
    else { 'declared' }

    $profileStatus = if ($null -eq $TaskProfile -or [string]::IsNullOrWhiteSpace([string] $TaskProfile.status)) { [string] $Policy.taskRouting.unknownStatus } else { [string] $TaskProfile.status }
    $requiredEvidence = if ($null -eq $TaskProfile) { @() } else { @($TaskProfile.requiredEvidence | ForEach-Object { [string] $_ } | Sort-Object -Unique) }
    $missingEvidence = @($requiredEvidence | Where-Object { $_ -notin $availableEvidence })
    $confidence = if ($ChangedFiles.Count -eq 0) {
        'not-applicable'
    }
    elseif ($profileStatus -eq [string] $Policy.taskRouting.unknownStatus -or $pathConfidence -eq 'none') {
        'unknown'
    }
    elseif ($pathConfidence -eq 'partial' -or $missingEvidence.Count -gt 0) {
        'partial'
    }
    elseif ('behavioral' -in $requiredEvidence) {
        'behavioral'
    }
    else { 'structural' }

    $uncovered = @($files | Where-Object { $_.confidence -ne 'declared' } | ForEach-Object { $_.path })
    $actions = @()
    if ($profileStatus -eq [string] $Policy.taskRouting.unknownStatus) {
        $actions += 'Task intent is U (unknown). Run one focused diagnostic or obtain a clearer acceptance behavior; do not repeat broad verification.'
    }
    if ($uncovered.Count -gt 0) {
        $actions += 'Add or run a focused check for uncovered paths, or report path coverage as partial.'
    }
    if ($missingEvidence.Count -gt 0) {
        $actions += "Missing required evidence: $($missingEvidence -join ', '). Passing structural checks alone is not complete behavioral validation."
    }
    return [pscustomobject]@{
        confidence = $confidence
        pathConfidence = $pathConfidence
        taskProfileStatus = $profileStatus
        requiredEvidence = @($requiredEvidence)
        availableEvidence = @($availableEvidence)
        missingEvidence = @($missingEvidence)
        changedFileCount = $ChangedFiles.Count
        coveredFileCount = @($files | Where-Object { $_.confidence -eq 'declared' }).Count
        uncoveredPaths = $uncovered
        files = $files
        recommendedActions = $actions
    }
}
