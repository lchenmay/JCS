module JCS.BizLogics.Db

open System
open System.IO
open System.Threading
open System.Text
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Text
open Util.Perf
open Util.Crypto
open Util.CollectionModDict
open Util.Db
open Util.DbTx
open Util.Orm
open Util.Zmq

open UtilWebServer.DbLogger
open UtilWebServer.Db
open UtilWebServer.Common

open JCS.Shared.OrmTypes
open JCS.Shared.Types
open JCS.Shared.OrmMor
open JCS.Shared.CustomMor
open JCS.Shared.Project

open JCS.BizLogics.Common

let dbLoggero =
    (fun log -> 
        let p = pLOG_empty()
        p.Content <- log.content
        p.Location <- log.location
        p.Sql <- log.sql
        p)
    |> createDbLogger LOG_metadata conn
    |> Some


let creator metadata populate = 
    let p = metadata.empty__p()
    populate p
    p__createRcd p metadata dbLoggero metadata.table conn


let createEU caption auth = 
    (fun (p:pEU) -> 
        p.Caption <- caption
        p.AuthType <- auth) 
    |> creator EU_metadata

let createFILE id (owner,caption,suffix,desc) = 

    let p = FILE_metadata.empty__p()
    p.Caption <- caption
    p.Owner <- owner
    p.Suffix <- suffix
    p.Desc <- desc
    
    let pretx = None |> opctx__pretx

    let rcd = 
        p
        |> id__CreateTx id pretx FILE_metadata

    if pretx |> loggedPipeline dbLoggero "BizLogics.Db" conn then
        Some rcd
    else
        None


let tryCU localizor creator updator ps = 
    match localizor ps with
    | Some v -> updator v ps
    | None -> creator ps

let createProject (code:string) = 
    let code = code.Trim()
    if code.Length > 0 then
        (fun (p:pPROJECT) ->
            p.Code <- code) |> creator PROJECT_metadata
    else
        None

let code__ProjectComplexo ps =
    runtime.data.projectxs.Values
    |> Array.tryFind(fun i -> i.project.p.Code = fst ps)

let createProjectComplex ps = 
    match createProject (fst ps) with
    | Some project -> 
        let projectx = project__ProjectComplex project
        runtime.data.projectxs[project.ID] <- projectx
        Some projectx
    | None -> None

let updateProjectComplex projectx ps = 
    if 
        projectx.project
        |> updateRcd "" conn PROJECT_metadata dbLoggero (fun p -> 
            p.Caption <- snd ps) then
        Some projectx
    else
        None

let tryProjectCU (code:string) = 
    tryCU code__ProjectComplexo createProjectComplex updateProjectComplex

let createPage projectx (name:string) = 
    let name = name.Trim()
    if name.Length > 0 && projectx.pagexs.ContainsKey name = false then
        (fun (p:pPAGE) ->
            p.Project <- projectx.project.ID
            p.Name <- name) |> creator PAGE_metadata
    else
        None

let checkLocalHostConfig projectx = 
    match 
        projectx.hostconfigs.Values 
        |> Array.tryFind(fun i -> i.p.Hostname.ToUpper() = System.Environment.MachineName.ToUpper()) with
    | Some v -> Some v
    | None -> 
        match 
            (fun (p:pHOSTCONFIG) ->
                p.Project <- projectx.project.ID
                p.Hostname <- System.Environment.MachineName.ToUpper()) |> creator HOSTCONFIG_metadata with
            | Some v -> 
                projectx.hostconfigs[v.p.Hostname] <- v
                Some v
            | None -> None

let createComp projectx (name:string) = 
    let name = name.Trim()
    if name.Length > 0 && projectx.compxs.ContainsKey name = false then
        (fun (p:pCOMP) ->
            p.Project <- projectx.project.ID
            p.Name <- name) |> creator COMP_metadata
    else
        None

let createTemplate project name = 
    (fun (p:pTEMPLATE) ->
        p.Project <- project.ID
        p.Name <- name) |> creator TEMPLATE_metadata

let createVarType bindType bind project name t = 
    (fun (p:pVARTYPE) ->
        p.Project <- project.ID
        p.BindType <- bindType
        p.Bind <- bind
        p.Name <- name
        p.Type <- t) |> creator VARTYPE_metadata

let createCompProp projectx (name,t,bind) =
    let compx = projectx.compxs[bind]
    match compx.props.Values |> Array.tryFind(fun i -> i.p.Name = name) with
    | Some prop -> Some prop
    | None -> 
        match 
            createVarType 
                vartypeBindTypeEnum.CompProps compx.comp.ID projectx.project
                name t with
        | Some vt -> 
            compx.props[name] <- vt
            Some vt
        | None -> None

let createCompState projectx (name,t,bind) =
    let compx = projectx.compxs[bind]
    match compx.states.Values |> Array.tryFind(fun i -> i.p.Name = name) with
    | Some prop -> Some prop
    | None -> 
        match 
            createVarType 
                vartypeBindTypeEnum.CompState compx.comp.ID projectx.project
                name t with
        | Some vt -> 
            compx.states[name] <- vt
            Some vt
        | None -> None

let createPageProp projectx (name,t,bind) =
    let pagex = projectx.pagexs[bind]
    match pagex.props.Values |> Array.tryFind(fun i -> i.p.Name = name) with
    | Some prop -> Some prop
    | None -> 
        match 
            createVarType 
                vartypeBindTypeEnum.PageProps pagex.page.ID projectx.project
                name t with
        | Some vt -> 
            pagex.props[name] <- vt
            Some vt
        | None -> None

let createPageState projectx (name,t,bind) =
    let pagex = projectx.pagexs[bind]
    match pagex.states.Values |> Array.tryFind(fun i -> i.p.Name = name) with
    | Some prop -> Some prop
    | None -> 
        match 
            createVarType 
                vartypeBindTypeEnum.CompState pagex.page.ID projectx.project
                name t with
        | Some vt -> 
            pagex.states[name] <- vt
            Some vt
        | None -> None

