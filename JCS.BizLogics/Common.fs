﻿module JCS.BizLogics.Common

open System
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Cat
open Util.Db
open Util.DbTx
open Util.Orm

open UtilWebServer.Common
open UtilWebServer.Db
open UtilWebServer.Runtime

open JCS.Shared.OrmTypes
open JCS.Shared.OrmMor
open JCS.Shared.Types
open JCS.Shared.CustomMor


type Session = SessionTemplate<EuComplex,unit>
type Sessions = SessionsTemplate<EuComplex,unit>

type HostData = unit

type Runtime = RuntimeTemplate<EuComplex,unit,RuntimeData,HostData>

type X = UtilWebServer.Api.ApiCtx<Runtime,Session,Er>

type CtxWrappedX = CtxWrapper<X,Er>

type HostEnum = 
| Dev
| Prod

let runtime = 

    let hostEnum = 
        match Environment.MachineName with
        | "MAIN" -> HostEnum.Dev
        | _ -> HostEnum.Prod

    let h = {
        data = ()
        port = 5045
        conn = ""
        url = ""

        updateDatabase = true

        DiscordAppId = 
            [|  "1254790111"
                "913181274" |]
            |> String.Concat
        DiscordPubKey = 
            [|  "e0300e71e2dc"
                "94ec42425c"
                "eea8faed6b6"
                "172158dbbc1"
                "b882fa2750f"
                "b55dec22a" |]
            |> String.Concat
        DiscordSecret = 
            [|  "YwZeJGUrR"
                "JwL3E7V"
                "cwlgtvJ_"
                "oeT01nom" |]
            |> String.Concat
        DiscordRedirect = ""

        vueDeployDir = @"C:\Dev\JCS\vscode\dist"
        fsDir = @"C:/FsRoot/JCS" }

    match hostEnum with
    | HostEnum.Dev -> 
        Util.Db.rdbms <- Rdbms.SqlServer
        h.conn <- "server=.; database=JCS; Trusted_Connection=True;"
    | HostEnum.Prod ->
        Util.Db.rdbms <- Rdbms.PostgreSql
        h.conn <- "Server=localhost;Username=postgres;Password=Abc123;Database=jcs;"

    RuntimeData_empty()
    |> empty__Runtime<EuComplex,unit,HostData,RuntimeData> h

let appendFact runtime fact = 
    lock runtime.facts (fun _ ->
        runtime.facts <- 
            runtime.facts
            |> List.append [fact])

        



