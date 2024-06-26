module TypeSys.Common

open System
open System.IO
open System.Collections.Generic
open System.Data
open System.Data.SqlClient
open System.Text
open System.Text.RegularExpressions

open Util.Runtime
open Util.Cat
open Util.Collection
open Util.CollectionSortedAccessor
open Util.Text
open Util.Json
open Util.FileSys
open Util.Db
open Util.DbQuery
open Util.DbTx
open Util.Orm

open TypeSys.MetaType


type Src = {
lang: ProgrammingLang
buffer: List<string>
w: TextBlockWriter
filename: string }

let src__txt newline src = src.buffer.ToArray() |> String.concat newline

let productItems__term prefix suffix delim (items:Type[]) =
    [| 0 .. items.Length - 1 |]
    |> Array.map(fun i -> prefix + i.ToString() + suffix)
    |> String.concat delim


let table__fieldKeys = 
    table__sortedFields
    >> Array.map(fun i ->
        let a,b,c,d = i
        b)

let table__typeName t = t.shorthand.ToUpper()