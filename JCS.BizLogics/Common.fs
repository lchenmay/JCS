module JCS.BizLogics.Common

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
            [|  "129810910"
                "2488158259" |]
            |> String.Concat
        DiscordPubKey = 
            [|  "d172c3fd453"
                "a101e18078f"
                "e35908a2a75"
                "8403a6a57b3"
                "b74925ded0c"
                "5734c7668" |]
            |> String.Concat
        DiscordSecret = 
            [|  "ZyLGxD37M9Z"
                "IbjBOENj4-y"
                "PJr03SGgib" |]
            |> String.Concat

        VsDirSolution = @"C:/Dev/JCS"
        req__vueDeployDir = (fun _ -> @"C:/Dev/JCS/vscode/dist")
        fsDir = @"C:/FsRoot/JCS" }

    match hostEnum with
    | HostEnum.Dev -> 
        Util.Db.rdbms <- Rdbms.SqlServer
        h.conn <- "server=.; database=JCS; Trusted_Connection=True;"
    | HostEnum.Prod ->
        Util.Db.rdbms <- Rdbms.PostgreSql
        h.conn <- "Server=localhost;Username=postgres;Password=Abc123;Database=jcs;"

    RuntimeData_empty()
    |> empty__Runtime<EuComplex,unit,HostData,RuntimeData> "JCS" h

let appendFact runtime fact = 
    lock runtime.facts (fun _ ->
        runtime.facts <- 
            runtime.facts
            |> List.append [fact])

        



