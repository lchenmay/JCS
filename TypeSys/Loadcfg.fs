module Loadcfg

open System
open System.IO

open System.Text.Json
open System.Collections.Generic

open TypeSys.Common

open Util.Db

let output (s:string) = Console.WriteLine s

let loadConfigFilePath defaultpath =
    let args = Environment.GetCommandLineArgs()
    let mutable cfgPath = defaultpath
    if args.Length > 1 then
        match args.[1] with
        | "-f" when args.Length > 2 -> 
            cfgPath <- args.[2]
        | _ -> 
            ()
    $"Load cfg from File: {cfgPath}" |> output
    cfgPath

let parseRdbms (s: int) =
    match s with
    | 0 -> Rdbms.SqlServer
    | 1 -> Rdbms.PostgreSql
    | _ -> Rdbms.SqlServer

let parseRobotConfig (name: string) (jsonElement: JsonElement) : RobotConfig option =
    try
        Some {
            ns = jsonElement.GetProperty("ns").GetString()
            rdbms = jsonElement.GetProperty("rdbms").GetInt32() |> parseRdbms
            dbName = jsonElement.GetProperty("dbName").GetString()
            donmainName = jsonElement.GetProperty("donmainName").GetString()
            conn = jsonElement.GetProperty("conn").GetString()
            mainDir = jsonElement.GetProperty("mainDir").GetString()
            JsDir = jsonElement.GetProperty("JsDir").GetString()
        }
    with
    | :? System.Collections.Generic.KeyNotFoundException
    | :? System.ArgumentNullException -> None
    | :? System.FormatException as ex ->
        printfn "Error parsing config for %s: %s" name ex.Message
        None

let readRobotConfigs (filePath: string): Dictionary<string, RobotConfig> =
    let configs = Dictionary<string, RobotConfig>()
    try
        let jsonString = File.ReadAllText(filePath)
        let jsonDocument = JsonDocument.Parse(jsonString)
        let root = jsonDocument.RootElement

        root.EnumerateObject()
        |> Seq.iter (fun prop ->
            match parseRobotConfig prop.Name prop.Value with
            | Some config -> configs.Add(prop.Name, config)
            | None -> $"Skipping config for {prop.Name} due to errors." |> output
        )
    with
    | ex ->
        $"Error reading file: {ex.Message}" |> output

    configs 

let chooseProject (configs: Dictionary<string, RobotConfig>) target =
    "Available projects:" |> output
    configs.Keys
    |> Seq.iter (fun key -> output $"{key} - {configs[key].dbName} {configs[key].rdbms |> string}")

    $"Please enter the project name (default:{target}):" |> output
    let input = Console.ReadLine()
    if configs.ContainsKey(input) then
        input
    else
        $"Project {input} not found. Use default value {target}"  |> output
        target