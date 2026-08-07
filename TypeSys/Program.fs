module TypeSys.Program

open System
open System.IO

open TypeSys.Common

open Util.Db

Console.OutputEncoding <- Text.Encoding.UTF8

let output (text: string) = Console.WriteLine text

let private optionalEnvironment name =
    match Environment.GetEnvironmentVariable name with
    | null -> ""
    | value -> value

let private requireDirectory expectedPath =
    let resolved = Path.GetFullPath expectedPath
    if not (Directory.Exists resolved) then
        invalidOp $"TypeSys target directory does not exist: {resolved}"
    resolved

let targetConfig target =
    match target with
    | 6 ->
        { ns = "JCS.Shared"
          rdbms = Rdbms.SqlServer
          dbName = "JCS"
          donmainName = "jcatsys.com"
          conn = optionalEnvironment "TYPESYS_CONNECTION_JCS"
          mainDir = requireDirectory @"D:\DEV\JCS\JCS.Shared"
          JsDir = requireDirectory @"D:\DEV\JCS\vscode\src\lib\shared" }
    | 20 ->
        { ns = "Aiarwa.Shared"
          rdbms = Rdbms.PostgreSql
          dbName = "Aiarwa"
          donmainName = "wigaoil.com"
          conn = optionalEnvironment "TYPESYS_CONNECTION_AIARWA"
          mainDir = requireDirectory @"D:\DEV\Aiarwa\Aiarwa.Shared"
          JsDir = requireDirectory @"D:\DEV\Aiarwa\vscode\src\lib\shared" }
    | _ ->
        invalidArg (nameof target) $"Unsupported D:\DEV TypeSys target: {target}. Supported targets are 6 (JCS) and 20 (Aiarwa)."

let runTarget exeDir target =
    let config = targetConfig target
    output $"TypeSys target {target}: {config.mainDir}"
    config |> CodeRobot.go output exeDir

let args = Environment.GetCommandLineArgs() |> Array.skip 1
let exeDir = AppContext.BaseDirectory.TrimEnd('\\', '/')

match args with
| [| "--target"; value |] ->
    match Int32.TryParse value with
    | true, target -> runTarget exeDir target
    | _ -> invalidArg (nameof value) "--target must be an integer."
| [| "--list-targets" |] ->
    output "6 JCS"
    output "20 Aiarwa"
| _ ->
    invalidOp "TypeSys has no default target. Use a repository controlled-generation wrapper with --target 6 or --target 20."
