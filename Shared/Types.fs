module Shared.Types

open System
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Text
open Util.Json
open Util.Stat
open Util.CollectionModDict

open Shared.OrmTypes

//[TypeManaged]{

type EuComplex = {
eu: int64 }

type ProjectComplex = {
project: PROJECT }

type Fact = 
| Undefined

type RuntimeData = {
mutable facts: Fact list
pcs: ModDictStr<ProjectComplex> }

type Msg = 
| Heartbeat
| ApiRequest of Json
| ApiResponse of Json
| SingleFact of Fact
| MultiFact of Fact[]

type Er = 
| ApiNotExists
| InvalideParameter
| Unauthorized
| NotAvailable
| Internal


//}



