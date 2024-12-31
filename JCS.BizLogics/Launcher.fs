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
open Util.WebServer

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

    init runtime

    runtime.echo <- echo
    runtime.h404o <- (fun _ ->  
        ssrPageHome 
        |> render (hash1,hash2) ""
        |> bin__StandardResponse "text/html") |> Some
    runtime.wsHandler <- fun json ->
        match
            json
            |> json__Msgo with
        | Some msg ->
            match msg with
            | ApiRequest json ->

                let service = tryFindStrByAtt "service" json
                let api = tryFindStrByAtt "api" json
                
                branch service api json
                |> Json.Braket
                |> Some

            | Heartbeat -> None
            | _ -> Some empty
        | None -> Some empty
    
    startEngine runtime

