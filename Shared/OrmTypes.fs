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

// [Ts_Project] (PROJ)

type pPROJ = {
mutable Code: Chars
mutable Caption: Chars}


type PROJ = Rcd<pPROJ>

let PROJ_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code","caption" """

let pPROJ_fieldordersArray = [|
    "Code"
    "Caption" |]

let PROJ_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code]=@Code,[Caption]=@Caption"
    | Rdbms.PostgreSql -> "code=@code,caption=@caption"

let pPROJ_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code", 64)
            Chars("Caption", 256) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code", 64)
            Chars("caption", 256) |]

let pPROJ_empty(): pPROJ = {
    Code = ""
    Caption = "" }

let PROJ_id = ref 234345L
let PROJ_count = ref 0
let PROJ_table = "Ts_Project"
