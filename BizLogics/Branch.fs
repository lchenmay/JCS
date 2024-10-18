module BizLogics.Branch

open System
open System.Text
open System.Collections.Generic
open System.Threading

open Util.Cat
open Util.Text
open Util.Bin
open Util.Perf
open Util.Json
open Util.Http
open Util.HttpServer
open Util.Zmq

open Shared.OrmTypes
open Shared.Types
open Shared.OrmMor
open Shared.CustomMor

open UtilWebServer.Common
open UtilWebServer.Api
open UtilWebServer.Json
open UtilWebServer.SSR
open UtilWebServer.Server.Monitor

open Shared.OrmTypes
open Shared.OrmMor
open Shared.Types
open Shared.CustomMor

open BizLogics.Common


let branching x = 

    let bindx p = 
        x.proco <- Some p
        Suc x

    match x.service with
    | "public" -> 
        match x.api with
        | "monitorPerf" -> bindx (fun x -> apiMonitorPerf())
        | "monitorServer" -> bindx (fun x -> apiMonitor x.runtime)
        | _ -> Fail(Er.ApiNotExists,x)
    | "eu" -> 
        match x.api with
        | _ -> Fail(Er.ApiNotExists,x)
    | "admin" -> Fail(Er.ApiNotExists,x)
    | "open" -> Fail(Er.ApiNotExists,x)
    | _ -> Fail(Er.ApiNotExists,x)


let branch service api json = 

    use cw = new CodeWrapper("Server.WebHandler.branch")

    let mutable x = incoming__x runtime service api "" json
    
    match service with
    | _ -> ()

    runApi branching x
