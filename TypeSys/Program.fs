module TypeSys.Program

open System
open System.IO

open TypeSys.Config
open Util.Rdbms

let private output (text: string) =
    Console.WriteLine text

let private usage =
    """
TypeSys explicit generation entry

Required:
  --execute
  --namespace <F# namespace>
  --rdbms <postgresql|sqlserver>
  --database <database name>
  --main-dir <existing shared-source directory>
  --js-dir <existing TypeScript output directory>

Optional:
  --domain <domain name>
  --connection-env <environment variable containing a connection string>

Example:
  dotnet run --project TypeSys.fsproj -- --execute --namespace Example.Shared --rdbms postgresql --database example --main-dir <path> --js-dir <path>
"""

let private parseArguments (args: string array) =
    let rec loop index execute options =
        if index >= args.Length then
            execute, options
        else
            match args[index] with
            | "--execute" -> loop (index + 1) true options
            | key when key.StartsWith("--", StringComparison.Ordinal) ->
                if index + 1 >= args.Length then
                    failwith $"Missing value for {key}."
                loop (index + 2) execute (Map.add key args[index + 1] options)
            | value ->
                failwith $"Unexpected argument: {value}."
    loop 0 false Map.empty

let private requireOption (name: string) (options: Map<string, string>) =
    match Map.tryFind name options with
    | Some value when not (String.IsNullOrWhiteSpace value) -> value.Trim()
    | _ -> failwith $"Required option {name} is missing."

let private existingDirectory (optionName: string) (value: string) =
    let path = Path.GetFullPath value
    if not (Directory.Exists path) then
        failwith $"{optionName} does not resolve to an existing directory: {path}"
    path

let private parseRdbms (value: string) =
    match value.Trim().ToLowerInvariant() with
    | "postgresql" | "postgres" -> Rdbms.PostgreSql
    | "sqlserver" | "mssql" -> Rdbms.SqlServer
    | _ -> failwith "--rdbms must be postgresql or sqlserver."

let private connectionString (options: Map<string, string>) =
    match Map.tryFind "--connection-env" options with
    | None -> ""
    | Some variableName ->
        match Environment.GetEnvironmentVariable(variableName.Trim()) with
        | value when not (String.IsNullOrWhiteSpace value) -> value.Trim()
        | _ -> failwith $"Connection-string environment variable {variableName} is missing."

[<EntryPoint>]
let main argv =
    Console.OutputEncoding <- Text.Encoding.UTF8
    try
        let execute, options = parseArguments argv
        if not execute then
            output usage
            2
        else
            let config: RobotConfig =
                { ns = requireOption "--namespace" options
                  rdbms = requireOption "--rdbms" options |> parseRdbms
                  dbName = requireOption "--database" options
                  domainName = Map.tryFind "--domain" options |> Option.defaultValue ""
                  conn = connectionString options
                  mainDir = requireOption "--main-dir" options |> existingDirectory "--main-dir"
                  JsDir = requireOption "--js-dir" options |> existingDirectory "--js-dir" }

            let executableDirectory =
                AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            CodeRobot.go output executableDirectory config
            0
    with ex ->
        Console.Error.WriteLine ex.Message
        Console.Error.WriteLine usage
        1
