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
eu: EU }

type FBindComplex = {
file: FILE
fbind: FBIND }

type MomentComplex = {
fbxs: FBindComplex[]
m: MOMENT }

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

type ApiComplex = {
reqs: ModDictStr<VARTYPE>
reps: ModDictStr<VARTYPE>
api: API }

type ProjectComplex = {
hostconfigs: ModDictStr<HOSTCONFIG>
tablexs: ModDictStr<TableComplex>
compxs: ModDictStr<CompComplex>
templatexs: ModDictStr<TEMPLATE>
pagexs: ModDictStr<PageComplex>
apixs: ModDictStr<ApiComplex>
project: PROJECT }

type Fact = 
| Undefined

type RuntimeData = {
mutable facts: Fact list
projectxs: ModDictInt64<ProjectComplex> 
files: ModDictInt64<FILE>
mxs: ModDictInt64<MomentComplex>
books: List<BOOK> }


type ClientRuntime = {
mutable version: int }

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



