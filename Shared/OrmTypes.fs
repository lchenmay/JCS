module Shared.OrmTypes

open LanguagePrimitives

open System
open System.Collections.Generic
open System.Collections.Concurrent
open System.Text

open Util.Cat
open Util.Perf
open Util.Measures
open Util.CollectionModDict
open Util.Collection
open Util.Db
open Util.DbQuery
open Util.DbTx
open Util.Bin
open Util.Text
open Util.Json
open Util.Orm
open Util.Stat

open PreOrm

// [Ts_Field] (FIELD)

type pFIELD = {
mutable Name: Chars
mutable Desc: Text}


type FIELD = Rcd<pFIELD>

let FIELD_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Desc]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","desc" """

let pFIELD_fieldordersArray = [|
    "Name"
    "Desc" |]

let FIELD_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Desc]=@Desc"
    | Rdbms.PostgreSql -> "name=@name,desc=@desc"

let pFIELD_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Text("Desc") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Text("desc") |]

let pFIELD_empty(): pFIELD = {
    Name = ""
    Desc = "" }

let FIELD_id = ref 7523431L
let FIELD_count = ref 0
let FIELD_table = "Ts_Field"

// [Ts_Project] (PROJECT)

type pPROJECT = {
mutable Code: Chars
mutable Caption: Chars}


type PROJECT = Rcd<pPROJECT>

let PROJECT_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code","caption" """

let pPROJECT_fieldordersArray = [|
    "Code"
    "Caption" |]

let PROJECT_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code]=@Code,[Caption]=@Caption"
    | Rdbms.PostgreSql -> "code=@code,caption=@caption"

let pPROJECT_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code", 64)
            Chars("Caption", 256) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code", 64)
            Chars("caption", 256) |]

let pPROJECT_empty(): pPROJECT = {
    Code = ""
    Caption = "" }

let PROJECT_id = ref 234345L
let PROJECT_count = ref 0
let PROJECT_table = "Ts_Project"

// [Ts_Table] (TABLE)

type pTABLE = {
mutable Name: Chars
mutable Desc: Text}


type TABLE = Rcd<pTABLE>

let TABLE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Desc]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","desc" """

let pTABLE_fieldordersArray = [|
    "Name"
    "Desc" |]

let TABLE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Desc]=@Desc"
    | Rdbms.PostgreSql -> "name=@name,desc=@desc"

let pTABLE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Text("Desc") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Text("desc") |]

let pTABLE_empty(): pTABLE = {
    Name = ""
    Desc = "" }

let TABLE_id = ref 7523431L
let TABLE_count = ref 0
let TABLE_table = "Ts_Table"

// [Ts_UiComponent] (COMP)

type pCOMP = {
mutable Code: Chars
mutable Caption: Chars}


type COMP = Rcd<pCOMP>

let COMP_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code","caption" """

let pCOMP_fieldordersArray = [|
    "Code"
    "Caption" |]

let COMP_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code]=@Code,[Caption]=@Caption"
    | Rdbms.PostgreSql -> "code=@code,caption=@caption"

let pCOMP_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code", 64)
            Chars("Caption", 256) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code", 64)
            Chars("caption", 256) |]

let pCOMP_empty(): pCOMP = {
    Code = ""
    Caption = "" }

let COMP_id = ref 6723431L
let COMP_count = ref 0
let COMP_table = "Ts_UiComponent"

// [Ts_UiPage] (PAGE)

type pPAGE = {
mutable Code: Chars
mutable Caption: Chars}


type PAGE = Rcd<pPAGE>

let PAGE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code","caption" """

let pPAGE_fieldordersArray = [|
    "Code"
    "Caption" |]

let PAGE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code]=@Code,[Caption]=@Caption"
    | Rdbms.PostgreSql -> "code=@code,caption=@caption"

let pPAGE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code", 64)
            Chars("Caption", 256) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code", 64)
            Chars("caption", 256) |]

let pPAGE_empty(): pPAGE = {
    Code = ""
    Caption = "" }

let PAGE_id = ref 6723431L
let PAGE_count = ref 0
let PAGE_table = "Ts_UiPage"
