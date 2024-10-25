module BizLogics.Db

open System
open System.Threading
open System.Text
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Text
open Util.Perf
open Util.Crypto
open Util.Db
open Util.DbTx
open Util.Orm
open Util.Zmq

open UtilWebServer.DbLogger
open UtilWebServer.Db
open UtilWebServer.Common

open Shared.OrmTypes
open Shared.Types
open Shared.OrmMor
open Shared.CustomMor

open BizLogics.Common

let creator metadata populate = 
    let p = metadata.empty__p()
    populate p
    p__createRcd p metadata metadata.table conn

let createProject (code:string) = 
    let code = code.Trim()
    if code.Length > 0 then
        (fun (p:pPROJECT) ->
            p.Code <- code) |> creator PROJECT_metadata
    else
        None

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

