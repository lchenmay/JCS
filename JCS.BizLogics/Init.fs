﻿module JCS.BizLogics.Init

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

open JCS.Shared.OrmTypes
open JCS.Shared.OrmMor
open JCS.Shared.Types
open JCS.Shared.CustomMor
open JCS.Shared.Project

open JCS.BizLogics.Common
open JCS.BizLogics.Db

let init (runtime:Runtime) = 

    "Init ..."
    |> runtime.output

    conn <- runtime.host.conn

    if runtime.host.updateDatabase then
        updateDbStructure runtime conn

    JCS.Shared.OrmMor.init()

    let loader metadata = loadAll runtime.output conn metadata

    (fun (i:EU) -> runtime.users[i.ID] <- {
        eu = i })
    |> loadAll runtime.output conn EU_metadata

    let users = runtime.users.Values |> Seq.toArray
    match users |> Array.tryFind(fun i -> i.eu.p.AuthType = euAuthTypeEnum.Admin) with
    | Some rcd -> ()
    | None -> 
        match createEU "sa" euAuthTypeEnum.Admin with
        | Some rcd -> runtime.users[rcd.ID] <- { eu = rcd }
        | None -> halt runtime.output ("BizLogics.Init.createEU") ""

    (fun (i:FILE) -> runtime.data.files[i.ID] <- i)
    |> loadAll runtime.output conn FILE_metadata

    let fbxs = 
        let res = new List<FBindComplex>()
        (fun (i:FBIND) -> res.Add {
            file = runtime.data.files[i.p.File]
            fbind = i })
        |> loadAll runtime.output conn FBIND_metadata
        res.ToArray()

    let moments = 
        let res = new List<MOMENT>()
        (fun (i:MOMENT) -> res.Add i)
        |> loadAll runtime.output conn MOMENT_metadata
        res.ToArray()

    moments
    |> Array.iter(fun m -> 
        runtime.data.mxs[m.ID] <- {
            fbxs = 
                fbxs
                |> Array.filter(fun i -> i.fbind.p.Moment = m.ID)
            m = m })

    (fun (i:BOOK) -> runtime.data.books.Add i)
    |> loadAll runtime.output conn BOOK_metadata

    (fun (i:PROJECT) -> runtime.data.projectxs[i.ID] <- i |> project__ProjectComplex) |> loader PROJECT_metadata
    (fun (i:HOSTCONFIG) -> runtime.data.projectxs[i.p.Project].hostconfigs[i.p.Hostname] <- i) |> loader HOSTCONFIG_metadata
    runtime.data.projectxs.Values
    |> Array.map checkLocalHostConfig
    |> ignore

    let projectx = 

        let self = "JCS"
        match runtime.data.projectxs.Values |> Array.tryFind(fun i -> i.project.p.Code = self) with
        | Some pc -> ()
        | None -> 
            match createProject self with
            | Some project -> runtime.data.projectxs[project.ID] <- project |> project__ProjectComplex
            | None -> halt runtime.output ("BizLogics.Init.createComp [" + self + "]") ""

        runtime.data.projectxs.Values |> Array.find(fun i -> i.project.p.Code = self)

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
        "/Common/HostConfig"
        "/Common/Table"
        "/Common/Field"
        "/Common/Api"
        "/Common/Template"
        "/Common/VarType"
        "/Common/Comp"
        "/Common/Page" |]
    |> Array.iter(fun name ->
        match projectx.compxs.Values |> Array.tryFind(fun i -> i.comp.p.Name = name) with
        | Some comp -> ()
        | None -> 
            match createComp projectx name with
            | Some comp -> projectx.compxs[comp.p.Name] <- comp |> comp__CompComplex
            | None -> halt runtime.output ("BizLogics.Init.createComp [" + name + "]") "")

    [|  "Public"
        "CodeRobot" |]
    |> Array.iter(fun name ->
        match projectx.templatexs.Values |> Array.tryFind(fun i -> i.p.Name = name) with
        | Some template -> ()
        | None -> 
            match createTemplate projectx.project name with
            | Some template -> projectx.templatexs[template.p.Name] <- template
            | None -> halt runtime.output ("BizLogics.Init.createTemplate [" + name + "]") "")

    let template = projectx.templatexs.Values |> Array.find(fun i -> i.p.Name = "CodeRobot")

    [|  "/Public/HomePage"
        "/CodeRobot/Projects"
        "/CodeRobot/Project" |]
    |> Array.iter(fun name ->
        match projectx.pagexs.Values |> Array.tryFind(fun i -> i.page.p.Name = name) with
        | Some page -> ()
        | None -> 
            match createPage projectx name with
            | Some page -> projectx.pagexs[page.p.Name] <- page |> page__CompComplex
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

    projectx
    |> projx__lines
    |> Array.iter runtime.output

    // Comp Props
    [|  ("projectx","ProjectComplex","/Common/Project")

        ("projectx","ProjectComplex","/Common/HostConfig")
        ("hostconfig","HOSTCONFIG","/Common/HostConfig")

        ("tablex","TableComplex","/Common/Table")
        ("field","FIELD","/Common/Field")

        ("projectx","ProjectComplex","/Common/Comp")
        ("compx","CompComplex","/Common/Comp")

        ("projectx","ProjectComplex","/Common/Page")
        ("pagex","PageComplex","/Common/Page")

        ("projectx","ProjectComplex","/Common/VarType")
        ("bindType","number","/Common/VarType")
        ("bind","string","/Common/VarType")
        ("vt","VARTYPE","/Common/VarType") |]
    |> Array.map(createCompProp projectx) |> ignore

    // Comp States
    [|  ("expand","false","/Common/Project") |]
    |> Array.map(createCompState projectx) |> ignore

    // Page Props
    [|  ("projectx","ProjectComplex","/CodeRobot/Project") |]
    |> Array.map(createPageProp projectx) |> ignore


