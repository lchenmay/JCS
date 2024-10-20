﻿module BizLogics.Db

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

let createProject code = 
    (fun (p:pPROJECT) ->
        p.Code <- code) |> creator PROJECT_metadata

let createLocalHostConfig project = 
    (fun (p:pHOSTCONFIG) ->
        p.Project <- project.ID
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
