module TypeSys.RepositoryPaths

open System
open System.IO

let private hasRepositoryMarkers (directory: DirectoryInfo) =
    let gitMarker = Path.Combine(directory.FullName, ".git")
    (Directory.Exists gitMarker || File.Exists gitMarker)
    && File.Exists(Path.Combine(directory.FullName, "AGENTS.md"))
    && File.Exists(Path.Combine(directory.FullName, "TypeSys", "TypeSys.fsproj"))

let repositoryRootFrom startPath =
    let rec find (directory: DirectoryInfo) =
        if hasRepositoryMarkers directory then
            directory.FullName
        elif isNull directory.Parent then
            invalidOp $"Cannot locate the JCS repository from: {startPath}"
        else
            find directory.Parent

    let resolved = Path.GetFullPath startPath
    let start =
        if File.Exists resolved then FileInfo(resolved).Directory
        else DirectoryInfo(resolved)
    find start

let requireDirectory description path =
    let resolved = Path.GetFullPath path
    if not (Directory.Exists resolved) then
        invalidOp $"{description} directory does not exist: {resolved}"
    resolved

let repositoryDirectory repositoryRoot relativePath =
    Path.Combine(repositoryRoot, relativePath)
    |> requireDirectory relativePath

let environmentDirectory variableName =
    match Environment.GetEnvironmentVariable variableName with
    | value when String.IsNullOrWhiteSpace value ->
        invalidOp $"Environment variable {variableName} must name the explicitly authorized repository root."
    | path -> requireDirectory variableName path
