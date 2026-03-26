module JCS.BizLogics.Common

open System
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Cat
open Util.Db
open Util.DbTx
open Util.Orm

open UtilKestrel.Types
open UtilKestrel.Ctx
open UtilKestrel.DbLogger
open UtilKestrel.Runtime

open JCS.Shared.OrmTypes
open JCS.Shared.OrmMor
open JCS.Shared.Types
open JCS.Shared.CustomMor


type Session = SessionTemplate<EuComplex,unit>
type Sessions = SessionsTemplate<EuComplex,unit>

type HostData = unit

type Runtime = RuntimeTemplate<EuComplex,unit,RuntimeData,HostData>

type X = EchoCtx<Runtime,Session,Er>
type WrapX = CtxWrapper<X,Er>

type HostEnum = 
| Dev
| Kamatera

let runtime = 

    let h = {
        data = ()
        port = 5045
        rdbms = Util.Db.Rdbms.SqlServer
        conn = "server=.; database=JCS; Trusted_Connection=True;"
        url = ""
        cert = ""
        certpwd = ""

        updateDatabase = true

        VsDirSolution = @"C:/Dev/JCS"
        req__vueDeployDir = @"C:/Dev/JCS/vscode/dist"
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

        



