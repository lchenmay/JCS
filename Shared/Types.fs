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

type TableComplex = {
fields: ModDictStr<FIELD>
table: TABLE }

type CompComplex = {
states: ModDictStr<VARTYPE>
props: ModDictStr<VARTYPE>
comp: COMP }

type PageComplex = {
states: ModDictStr<VARTYPE>
props: ModDictStr<VARTYPE>
page: PAGE }

type ProjectComplex = {
hostconfigs: ModDictStr<HOSTCONFIG>
tables: ModDictStr<TableComplex>
comps: ModDictInt64<CompComplex>
templates: ModDictInt64<TEMPLATE>
pages: ModDictInt64<PageComplex>
project: PROJECT }

type Fact = 
| Undefined

type RuntimeData = {
mutable facts: Fact list
pcs: ModDictInt64<ProjectComplex> }

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



