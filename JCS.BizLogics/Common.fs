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
| Kamatera

let runtime = 

    let h = {
        data = ()
        port = 5045
        conn = "server=.; database=JCS; Trusted_Connection=True;"
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

    let hostEnum = 
        let machinename = Environment.MachineName
        match Environment.MachineName with
        | "MAIN" -> HostEnum.Dev
        | "PTNHKDIE15IJZN" -> HostEnum.Kamatera
        | _ -> HostEnum.Dev

    match hostEnum with
    | HostEnum.Kamatera -> 
        h.conn <- "server=localhost\MSSQLSERVER01; database=JCS; Trusted_Connection=True;"
    | _ -> ()

    RuntimeData_empty()
    |> empty__Runtime<EuComplex,unit,HostData,RuntimeData> "JCS" h

let appendFact runtime fact = 
    lock runtime.facts (fun _ ->
        runtime.facts <- 
            runtime.facts
            |> List.append [fact])

        



