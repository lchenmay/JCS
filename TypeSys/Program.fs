module TypeSys.Program

open System
open System.IO

open TypeSys.Common
open TypeSys.RepositoryPaths

open Util.Db

Console.OutputEncoding <- Text.Encoding.UTF8

let output (text: string) = Console.WriteLine text

let private optionalEnvironment name =
    match Environment.GetEnvironmentVariable name with
    | null -> ""
    | value -> value

let targetConfig repositoryRoot target =
    match target with
    | 6 ->
        { ns = "JCS.Shared"
          rdbms = Rdbms.SqlServer
          dbName = "JCS"
          donmainName = "jcatsys.com"
          conn = optionalEnvironment "TYPESYS_CONNECTION_JCS"
          mainDir = repositoryDirectory repositoryRoot "JCS.Shared"
          JsDir = repositoryDirectory repositoryRoot (Path.Combine("vscode", "src", "lib", "shared")) }
    | 20 ->
        let targetRoot = environmentDirectory "TYPESYS_AIARWA_ROOT"
        { ns = "Aiarwa.Shared"
          rdbms = Rdbms.PostgreSql
          dbName = "Aiarwa"
          donmainName = "wigaoil.com"
          conn = optionalEnvironment "TYPESYS_CONNECTION_AIARWA"
          mainDir = repositoryDirectory targetRoot "Aiarwa.Shared"
          JsDir = repositoryDirectory targetRoot (Path.Combine("vscode", "src", "lib", "shared")) }
    | _ ->
        invalidArg (nameof target) $"Unsupported TypeSys target: {target}. Supported targets are 6 (JCS) and 20 (Aiarwa)."

let runTarget repositoryRoot target =
    let config = targetConfig repositoryRoot target
    output $"TypeSys target {target}: {config.mainDir}"
    config |> CodeRobot.go output

let args = Environment.GetCommandLineArgs() |> Array.skip 1
let repositoryRoot = repositoryRootFrom AppContext.BaseDirectory

match args with
| [| "--target"; value |] ->
    match Int32.TryParse value with
    | true, target -> runTarget repositoryRoot target
    | _ -> invalidArg (nameof value) "--target must be an integer."
| [| "--list-targets" |] ->
    output "6 JCS"
    output "20 Aiarwa"
| _ ->
    invalidOp "TypeSys has no default target. Use a repository controlled-generation wrapper with --target 6 or --target 20."
