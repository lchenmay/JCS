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
open Shared.Project

open BizLogics.Common
open BizLogics.Db

let init (runtime:Runtime) = 

    "Init ..."
    |> runtime.output

    conn <- runtime.host.conn

    if runtime.host.updateDatabase then
        updateDbStructure runtime conn

    Shared.OrmMor.init()

    runtime.data.pcs.Reset 1

    let loader metadata = loadAll runtime.output conn metadata

    let pc = 

        let self = "JCS"
        match runtime.data.pcs.Values |> Array.tryFind(fun i -> i.project.p.Code = self) with
        | Some pc -> ()
        | None -> 
            match createProject self with
            | Some project -> runtime.data.pcs[project.ID] <- project |> project__ProjectComplex
            | None -> halt runtime.output ("BizLogics.Init.createComp [" + self + "]") ""

        runtime.data.pcs.Values |> Array.find(fun i -> i.project.p.Code = self)

    (fun (i:PROJECT) -> runtime.data.pcs[i.ID] <- i |> project__ProjectComplex) |> loader PROJECT_metadata
    (fun (i:HOSTCONFIG) -> runtime.data.pcs[i.p.Project].hostconfigs[i.p.Hostname] <- i) |> loader HOSTCONFIG_metadata

    (fun (i:TABLE) -> runtime.data.pcs[i.p.Project].tables[i.p.Name] <- {
        fields = createModDictStr 4
        table = i }) |> loader TABLE_metadata
    (fun (i:FIELD) -> 
        let pc = runtime.data.pcs[i.p.Project]
        let tc = pc.tables.Values |> Array.find(fun t -> t.table.ID = i.p.Table)
        tc.fields[i.p.Name] <- i) |> loader FIELD_metadata

    (fun (i:COMP) -> runtime.data.pcs[i.p.Project].comps[i.ID] <- i |> comp__CompComplex) |> loader COMP_metadata
    (fun (i:TEMPLATE) -> runtime.data.pcs[i.p.Project].templates[i.ID] <- i) |> loader TEMPLATE_metadata
    (fun (i:PAGE) -> runtime.data.pcs[i.p.Project].pages[i.ID] <- i |> page__CompComplex) |> loader PAGE_metadata

    (fun (i:API) -> runtime.data.pcs[i.p.Project].apis[i.ID] <- i |> api__ApiComplex) |> loader API_metadata

    (fun (i:VARTYPE) -> 
        let pc = runtime.data.pcs[i.p.Project]
        match i.p.BindType with
        | vartypeBindTypeEnum.CompState -> 
            let comp = pc.comps.Values |> Array.find(fun ii-> ii.comp.ID = i.p.Bind)
            comp.states[i.p.Name] <- i
        | vartypeBindTypeEnum.CompProps -> 
            let comp = pc.comps.Values |> Array.find(fun ii-> ii.comp.ID = i.p.Bind)
            comp.props[i.p.Name] <- i
        | vartypeBindTypeEnum.PageState -> 
            let page = pc.pages.Values |> Array.find(fun ii-> ii.page.ID = i.p.Bind)
            page.states[i.p.Name] <- i
        | vartypeBindTypeEnum.PageProps -> 
            let page = pc.pages.Values |> Array.find(fun ii-> ii.page.ID = i.p.Bind)
            page.props[i.p.Name] <- i
        | _ -> ()) |> loader VARTYPE_metadata

    [|  "/Common/Project"
        "/Common/Table"
        "/Common/Field"
        "/Common/Api"
        "/Common/Template"
        "/Common/VarType"
        "/Common/Comp"
        "/Common/Page" |]
    |> Array.iter(fun name ->
        match pc.comps.Values |> Array.tryFind(fun i -> i.comp.p.Name = name) with
        | Some comp -> ()
        | None -> 
            match createComp pc.project name with
            | Some comp -> pc.comps[comp.ID] <- comp |> comp__CompComplex
            | None -> halt runtime.output ("BizLogics.Init.createComp [" + name + "]") "")

    [|  "Public"
        "CodeRobot" |]
    |> Array.iter(fun name ->
        match pc.templates.Values |> Array.tryFind(fun i -> i.p.Name = name) with
        | Some template -> ()
        | None -> 
            match createTemplate pc.project name with
            | Some template -> pc.templates[template.ID] <- template
            | None -> halt runtime.output ("BizLogics.Init.createTemplate [" + name + "]") "")

    let template = pc.templates.Values |> Array.find(fun i -> i.p.Name = "CodeRobot")

    [|  "/Public/HomePage"
        "/CodeRobot/Projects"
        "/CodeRobot/Project" |]
    |> Array.iter(fun name ->
        match pc.pages.Values |> Array.tryFind(fun i -> i.page.p.Name = name) with
        | Some page -> ()
        | None -> 
            match createPage pc.project template name with
            | Some page -> pc.pages[page.ID] <- page |> page__CompComplex
            | None -> halt runtime.output ("BizLogics.Init.createPage [" + name + "]") "")





