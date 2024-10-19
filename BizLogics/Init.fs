module BizLogics.Init

open System
open System.IO
open System.Collections.Generic
open System.Collections.Concurrent
open System.Threading

open Util.Runtime
open Util.CollectionModDict
open Util.Bin
open Util.Json
open Util.Db
open Util.DbTx
open Util.Orm
open Util.Zmq

open UtilWebServer.Constants
open UtilWebServer.Db
open UtilWebServer.DbLogger
open UtilWebServer.Init
open UtilWebServer.FileSys

open Shared.OrmTypes
open Shared.OrmMor
open Shared.Types
open Shared.CustomMor

open BizLogics.Common
open BizLogics.Project
open BizLogics.Db

let init (runtime:Runtime) = 

    "Init ..."
    |> runtime.output

    conn <- runtime.host.conn

    if runtime.host.updateDatabase then
        updateDbStructure runtime conn

    Shared.OrmMor.init()

    runtime.data.pcs.Reset 4

    let loader metadata = loadAll runtime.output conn metadata

    (fun (i:PROJECT) -> runtime.data.pcs[i.ID] <- i |> project__ProjectComplex) |> loader PROJECT_metadata

    (fun (i:PAGE) -> 
        let pc = runtime.data.pcs[i.p.Project]
        pc.pages[i.ID] <- i) |> loader PAGE_metadata

    let pc = runtime.data.pcs[234346L]

    [|  "Public"
        "CodeRobot" |]
    |> Array.iter(fun name ->
        match pc.templates.ToArray() |> Array.tryFind(fun i -> i.Value.p.Name = name) with
        | Some template -> ()
        | None -> 
            match createTemplate pc.project name with
            | Some template -> pc.templates[template.ID] <- template
            | None -> halt runtime.output ("BizLogics.Init.createTemplate [" + name + "]") "")

    let template = pc.templates.Values |> Array.find(fun i -> i.p.Name = "CodeRobot")

    [|  "/CodeRobot/Projects"
        "/CodeRobot/Project" |]
    |> Array.iter(fun name ->
        match pc.pages.ToArray() |> Array.tryFind(fun i -> i.Value.p.Name = name) with
        | Some page -> ()
        | None -> 
            match createPage pc.project template name with
            | Some page -> pc.pages[page.ID] <- page
            | None -> halt runtime.output ("BizLogics.Init.createPage [" + name + "]") "")





