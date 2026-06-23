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
    RuntimeData_empty()
    |> empty__Runtime<EuComplex,unit,HostData,RuntimeData> 
        "JCS" 
        { version = 0 }

let appendFact runtime fact = 
    lock runtime.facts (fun _ ->
        runtime.facts <- 
            runtime.facts
            |> List.append [fact])

        



