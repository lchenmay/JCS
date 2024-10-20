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

    runtime.data.projectxs.Reset 1

    let loader metadata = loadAll runtime.output conn metadata

    (fun (i:PROJECT) -> runtime.data.projectxs[i.ID] <- i |> project__ProjectComplex) |> loader PROJECT_metadata

    let pc = 

        let self = "JCS"
        match runtime.data.projectxs.Values |> Array.tryFind(fun i -> i.project.p.Code = self) with
        | Some pc -> ()
        | None -> 
            match createProject self with
            | Some project -> runtime.data.projectxs[project.ID] <- project |> project__ProjectComplex
            | None -> halt runtime.output ("BizLogics.Init.createComp [" + self + "]") ""

        runtime.data.projectxs.Values |> Array.find(fun i -> i.project.p.Code = self)

    (fun (i:HOSTCONFIG) -> runtime.data.projectxs[i.p.Project].hostconfigs[i.p.Hostname] <- i) |> loader HOSTCONFIG_metadata
    checkLocalHostConfig pc |> ignore

    (fun (i:TABLE) -> runtime.data.projectxs[i.p.Project].tablexs[i.p.Name] <- {
        fields = createModDictStr 4
        table = i }) |> loader TABLE_metadata
    (fun (i:FIELD) -> 
        let pc = runtime.data.projectxs[i.p.Project]
        let tc = pc.tablexs.Values |> Array.find(fun t -> t.table.ID = i.p.Table)
        tc.fields[i.p.Name] <- i) |> loader FIELD_metadata

    (fun (i:COMP) -> runtime.data.projectxs[i.p.Project].compxs[i.p.Name] <- i |> comp__CompComplex) |> loader COMP_metadata
    (fun (i:TEMPLATE) -> runtime.data.projectxs[i.p.Project].templatexs[i.p.Name] <- i) |> loader TEMPLATE_metadata
    (fun (i:PAGE) -> runtime.data.projectxs[i.p.Project].pagexs[i.p.Name] <- i |> page__CompComplex) |> loader PAGE_metadata

    (fun (i:API) -> runtime.data.projectxs[i.p.Project].apixs[i.p.Name] <- i |> api__ApiComplex) |> loader API_metadata

    [|  "/Common/Project"
        "/Common/Table"
        "/Common/Field"
        "/Common/Api"
        "/Common/Template"
        "/Common/VarType"
        "/Common/Comp"
        "/Common/Page" |]
    |> Array.iter(fun name ->
        match pc.compxs.Values |> Array.tryFind(fun i -> i.comp.p.Name = name) with
        | Some comp -> ()
        | None -> 
            match createComp pc.project name with
            | Some comp -> pc.compxs[comp.p.Name] <- comp |> comp__CompComplex
            | None -> halt runtime.output ("BizLogics.Init.createComp [" + name + "]") "")

    [|  "Public"
        "CodeRobot" |]
    |> Array.iter(fun name ->
        match pc.templatexs.Values |> Array.tryFind(fun i -> i.p.Name = name) with
        | Some template -> ()
        | None -> 
            match createTemplate pc.project name with
            | Some template -> pc.templatexs[template.p.Name] <- template
            | None -> halt runtime.output ("BizLogics.Init.createTemplate [" + name + "]") "")

    let template = pc.templatexs.Values |> Array.find(fun i -> i.p.Name = "CodeRobot")

    [|  "/Public/HomePage"
        "/CodeRobot/Projects"
        "/CodeRobot/Project" |]
    |> Array.iter(fun name ->
        match pc.pagexs.Values |> Array.tryFind(fun i -> i.page.p.Name = name) with
        | Some page -> ()
        | None -> 
            match createPage pc.project template name with
            | Some page -> pc.pagexs[page.p.Name] <- page |> page__CompComplex
            | None -> halt runtime.output ("BizLogics.Init.createPage [" + name + "]") "")


    (fun (i:VARTYPE) -> 
        let pc = runtime.data.projectxs[i.p.Project]
        match i.p.BindType with
        | vartypeBindTypeEnum.CompState -> 
            let comp = pc.compxs.Values |> Array.find(fun ii-> ii.comp.ID = i.p.Bind)
            comp.states[i.p.Name] <- i
        | vartypeBindTypeEnum.CompProps -> 
            let comp = pc.compxs.Values |> Array.find(fun ii-> ii.comp.ID = i.p.Bind)
            comp.props[i.p.Name] <- i

            pc
            |> projx__lines
            |> Array.iter runtime.output

            ()

        | vartypeBindTypeEnum.PageState -> 
            let page = pc.pagexs.Values |> Array.find(fun ii-> ii.page.ID = i.p.Bind)
            page.states[i.p.Name] <- i
        | vartypeBindTypeEnum.PageProps -> 
            let page = pc.pagexs.Values |> Array.find(fun ii-> ii.page.ID = i.p.Bind)
            page.props[i.p.Name] <- i
        | _ -> ()) |> loader VARTYPE_metadata

    pc
    |> projx__lines
    |> Array.iter runtime.output

    [|  ("projectx","ProjectComplex","/Common/Project") |]
    |> Array.iter(fun (propName,propType,comp) ->
        let compx = pc.compxs[comp]
        match compx.props.Values |> Array.tryFind(fun i -> i.p.Name = propName) with
        | Some prop -> ()
        | None -> 
            match createCompProp pc.project compx.comp propName propType with
            | Some vt -> compx.props[propName] <- vt
            | None -> halt runtime.output ("BizLogics.Init.createPageProp") "")



