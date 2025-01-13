module JCS.BizLogics.Launcher

open System
open System.Text
open System.IO
open System.Diagnostics

open Util.Cat
open Util.Perf
open Util.Runtime
open Util.Json
open Util.Http
open Util.HttpServer
open Util.Zmq

open UtilWebServer.Common
open UtilWebServer.SSR
open UtilWebServer.Server.Service

open JCS.Shared.Types
open JCS.Shared.CustomMor

open JCS.BizLogics.Common
open JCS.BizLogics.Init
open JCS.BizLogics.Branch
open JCS.BizLogics.SSR


let launch() =

    init runtime |> ignore

    runtime.listener.echo <- echo
    runtime.listener.h404o <- Some(fun _ -> 
        "404"
        |> System.Text.Encoding.ASCII.GetBytes
        |> bin__StandardResponse "text/html")
    runtime.listener.wsHandler <- fun json -> None
    
    startEngine runtime.listener

