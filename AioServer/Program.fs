open System
open System.Collections.Generic
open System.IO
open System.Text
open System.Text.Json
open System.Threading

open Util.Cat
open Util.Text
open Util.HttpClient
open Util.Console
open Util.Zmq

open BizShared.PreOrm
open BizShared.OrmTypes
open BizShared.Types
open BizShared.CustomMor


Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = 
    [|  Util.Text.now_time() 
        " - "
        s|]
    |> String.Concat
    |> Console.WriteLine

let outputRawStr (s:string) = Console.WriteLine s

let rec private repositoryWebRoot (directory: DirectoryInfo) =
    let gitMarker = Path.Combine(directory.FullName, ".git")
    if Directory.Exists gitMarker || File.Exists gitMarker then
        Path.Combine(directory.FullName, "WebDeploy", "wwwroot")
    elif isNull directory.Parent then
        Path.Combine(AppContext.BaseDirectory, "wwwroot")
    else
        repositoryWebRoot directory.Parent

let webRoot =
    match Environment.GetEnvironmentVariable "JCS_AIOSERVER_WEB_ROOT" with
    | configured when String.IsNullOrWhiteSpace configured ->
        repositoryWebRoot (DirectoryInfo AppContext.BaseDirectory)
    | configured -> Path.GetFullPath configured

[<EntryPoint>]
let main args =

    let zweb = create__ZWeb 2 port LogLevel.All false [||]

    let branch x =
        match x.service with
        | "public" -> 
            match x.api with
            | _ -> Fail(Error.ApiNotExists, x)
        | "admin" -> 
            match x.api with
            //| "sRK" -> setRolfKey |> bindOK x 
            | _ -> Fail(Error.ApiNotExists, x)
        | _ -> Fail(Error.ApiNotExists, x)

    let wsHandler (zweb:ZmqWeb) (wsp:WsPacket) =

        "<< Client: " + wsp.client.ToString() + " << incoming " + wsp.bin.Length.ToString() + " bytes"
        |> output

        wsp.bin
        |> Encoding.UTF8.GetString
        |> outputRawStr

        //wsp.bin |> binPushWsToAll zweb

        ">> Server Broadcast >> " + wsp.bin.Length.ToString() + " bytes"
        |> output

        None

    zweb.disconnector.Add(fun bin -> ())
    lauchWebServer output (httpHandler (httpEcho webRoot () branch)) wsHandler zweb

    Util.Runtime.halt output "" ""

    0
