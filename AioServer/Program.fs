open System
open System.Collections.Generic
open System.Text
open System.Text.Json
open System.Threading

open Util.Cat
open Util.Text
open Util.HttpClient
open Util.Console
open Util.Zmq

open GNodeServiceSDK.Types
open GNodeServiceSDK.IPC

open BizType.OrmTypes
open BizType.Types
open BizType.CustomMor


Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s

let mutable appRuntimeo = None

[<EntryPoint>]
let main args =

    appRuntimeo <- createIPC output args

    let port = 12077

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

        //wsp.bin |> binPushWsToAll zweb

        ">> Server Broadcast >> " + wsp.bin.Length.ToString() + " bytes"
        |> output

        None

    zweb.disconnector.Add(fun bin -> ())
    lauchWebServer output (httpHandler (httpEcho () branch)) wsHandler zweb

    Util.Runtime.halt output "" ""

    0
