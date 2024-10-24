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

let checkLocalHostConfig projectx = 
    match 
        projectx.hostconfigs.Values 
        |> Array.tryFind(fun i -> i.p.Hostname.ToUpper() = System.Environment.MachineName.ToUpper()) with
    | Some v -> Some v
    | None -> 
        (fun (p:pHOSTCONFIG) ->
            p.Project <- projectx.project.ID
            p.Hostname <- System.Environment.MachineName.ToUpper()) |> creator HOSTCONFIG_metadata

let createComp project name = 
    (fun (p:pCOMP) ->
        p.Project <- project.ID
        p.Name <- name) |> creator COMP_metadata

let createTemplate project name = 
    (fun (p:pTEMPLATE) ->
        p.Project <- project.ID
        p.Name <- name) |> creator TEMPLATE_metadata

let createPage project template name = 
    (fun (p:pPAGE) ->
        p.Project <- project.ID
        p.Template <- template.ID
        p.Name <- name) |> creator PAGE_metadata


let createCompProp project comp name t = 
    (fun (p:pVARTYPE) ->
        p.Project <- project.ID
        p.BindType <- vartypeBindTypeEnum.CompProps
        p.Bind <- comp.ID
        p.Name <- name
        p.Type <- t) |> creator VARTYPE_metadata

let createCompState project comp name v = 
    (fun (p:pVARTYPE) ->
        p.Project <- project.ID
        p.BindType <- vartypeBindTypeEnum.CompState
        p.Bind <- comp.ID
        p.Name <- name
        p.Val <- v) |> creator VARTYPE_metadata

let createPageProp project page name t = 
    (fun (p:pVARTYPE) ->
        p.Project <- project.ID
        p.BindType <- vartypeBindTypeEnum.PageProps
        p.Bind <- page.ID
        p.Name <- name
        p.Type <- t) |> creator VARTYPE_metadata
