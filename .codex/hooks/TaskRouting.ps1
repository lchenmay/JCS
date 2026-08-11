function Get-CodexTaskProfile {
    param(
        [Parameter(Mandatory = $true)] [object] $Policy,
        [AllowEmptyString()] [string] $Prompt = '',
        [string[]] $ChangedFiles = @(),
        [string[]] $ExplicitTaskTypes = @()
    )

    $selected = New-Object System.Collections.Generic.List[object]
    $normalizedExplicit = @($ExplicitTaskTypes | ForEach-Object { ([string] $_).Trim().ToLowerInvariant() } | Where-Object { -not [string]::IsNullOrWhiteSpace($_) } | Sort-Object -Unique)
    foreach ($pack in @($Policy.taskRouting.rulePacks)) {
        $matchedBy = New-Object System.Collections.Generic.List[string]
        if ([bool] $pack.always) { $matchedBy.Add('always') }
        if (([string] $pack.name).ToLowerInvariant() -in $normalizedExplicit) { $matchedBy.Add('explicit') }

        if (-not [string]::IsNullOrWhiteSpace($Prompt)) {
            foreach ($pattern in @($pack.promptPatterns)) {
                if ($Prompt -match ([string] $pattern)) { $matchedBy.Add('prompt'); break }
            }
        }
        foreach ($path in $ChangedFiles) {
            if (Test-PolicyPathMatchesAnyPattern -Path ([string] $path) -Patterns @($pack.pathPatterns)) {
                $matchedBy.Add('path')
                break
            }
        }
        if ($matchedBy.Count -gt 0) {
            $selected.Add([pscustomobject]@{
                name = [string] $pack.name
                summary = [string] $pack.summary
                minimumRisk = [string] $pack.minimumRisk
                requiredEvidence = @($pack.requiredEvidence | ForEach-Object { [string] $_ })
                matchedBy = @($matchedBy | Sort-Object -Unique)
                always = [bool] $pack.always
            })
        }
    }

    $taskTypes = @($selected | Where-Object { -not $_.always } | ForEach-Object { [string] $_.name } | Sort-Object -Unique)
    $unknownExplicit = @($normalizedExplicit | Where-Object { $_ -notin @($Policy.taskRouting.rulePacks | ForEach-Object { ([string] $_.name).ToLowerInvariant() }) })
    $uncertainty = if ($taskTypes.Count -eq 0 -or $unknownExplicit.Count -gt 0) { [string] $Policy.taskRouting.unknownStatus } else { 'known' }
    $risk = [string] $Policy.riskClassification.default
    foreach ($pack in $selected) {
        $risk = Get-PolicyHigherRisk -Left $risk -Right ([string] $pack.minimumRisk)
    }
    $evidence = @($selected | ForEach-Object { @($_.requiredEvidence) } | ForEach-Object { [string] $_ } | Sort-Object -Unique)
    $actions = @()
    if ($uncertainty -eq [string] $Policy.taskRouting.unknownStatus) {
        $actions += 'Task intent is not covered by a known rule pack. Preserve scope, perform one focused diagnostic when practical, and report the evidence gap instead of repeating broad verification.'
    }

    return [pscustomobject]@{
        status = $uncertainty
        taskTypes = $taskTypes
        selectedRulePacks = @($selected | ForEach-Object { [string] $_.name })
        inferredRisk = $risk
        requiredEvidence = $evidence
        unknownExplicitTaskTypes = $unknownExplicit
        recommendedActions = $actions
        matches = @($selected | ForEach-Object { $_ })
    }
}

function ConvertTo-CodexTaskTypeArgument {
    param([string[]] $TaskTypes = @())
    if ($TaskTypes.Count -eq 0) { return '' }
    return " -TaskType $($TaskTypes -join ',')"
}
