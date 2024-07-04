module TypeSys.MetaType

open System
open System.IO
open System.Collections.Generic
open System.Data
open System.Data.SqlClient
open System.Text
open System.Text.RegularExpressions

open Util.Runtime
open Util.Cat
open Util.CollectionSortedAccessor
open Util.Text
open Util.Json
open Util.FileSys
open Util.Db
open Util.DbQuery
open Util.DbTx
open Util.Orm

type ProgrammingLang = 
| FSharp
| CSharp
| TypeScript
| SQL

type FieldDef =
| FK of Table
| Caption of int
| Chars of int
| Link of int
| Text
| Bin
| Integer
| Float
| Boolean
| SelectLines of (string * string)[]
| Timestamp
| TimeSeries
| Other

// Sort, Name, Def, Json
and Field = int * string * FieldDef * Dictionary<string,string>

and Table = {
tableName: string
fields: Dictionary<string,Field>
fkins: List<Table * string>
fkouts: List<string * Table>
idstarting: int64
typeName:string }

with override this.ToString() = 
            "[" + this.tableName + "] " + this.typeName
            

let table__sortedFields table = 
    table.fields.Values
    |> Seq.toArray
    |> Array.sortBy(fun i ->
        let a,b,c,d = i
        a)

type TypeEnum = 
| Primitive
| Structure of (string * Type)[]
| Product of Type[]
| Sum of (string * Type option)[]
| Enum of (string * int)[]
| Orm of Table
| Option of Type
| Ary of Type
| List of Type
| Dictionary of Type * Type
and Type = {
name: string
mutable sort: int
mutable tEnum: TypeEnum
mutable custom: bool
src: string[] }

with override this.ToString() = 
            "[" + this.name + "] "

type TypeEnumPlain = 
| Primitive = 0
| Structure = 1
| Product = 2
| Sum = 3
| Enum = 4
| Orm = 5
| Option = 6
| Ary = 7
| List = 8
| Dictionary = 9

let typeEnum__plain e = 
    match e with
    | TypeEnum.Primitive -> TypeEnumPlain.Primitive
    | TypeEnum.Structure v -> TypeEnumPlain.Structure
    | TypeEnum.Product v -> TypeEnumPlain.Product
    | TypeEnum.Sum v -> TypeEnumPlain.Sum
    | TypeEnum.Enum v -> TypeEnumPlain.Enum
    | TypeEnum.Orm v -> TypeEnumPlain.Orm
    | TypeEnum.Option v -> TypeEnumPlain.Option
    | TypeEnum.Ary v -> TypeEnumPlain.Ary
    | TypeEnum.List v -> TypeEnumPlain.List
    | TypeEnum.Dictionary (kType,vType) -> TypeEnumPlain.Dictionary

let rec type__str output indent t = 

    tabs[indent] + "[" + t.name + "] (" + (typeEnum__plain t.tEnum).ToString() + ")" |> output

    match t.tEnum with
    | TypeEnum.Primitive -> ()
    | TypeEnum.Structure items -> 
        items
        |> Array.iter(fun i -> 
            let n,tt = i
            tabs[indent + 1] + "\"" + n + "\":" |> output
            type__str output (indent + 1) tt)
    | TypeEnum.Product items -> 
        items
        |> Array.iter(type__str output (indent + 1))
    | TypeEnum.Sum items -> 
        items
        |> Array.iter(fun item ->
            let n,o = item
            tabs[indent + 1] + "| \"" + n + "\" of" |> output
            match o with
            | Some tt -> type__str output (indent + 1) tt
            | None -> ())
            
    | TypeEnum.Enum v -> ()
    | TypeEnum.Orm v -> ()
    | TypeEnum.Option v ->
        tabs[indent + 1] + "Option of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.Ary v ->
        tabs[indent + 1] + "array of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.List v ->
        tabs[indent + 1] + "List of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.Dictionary (kType,vType) ->
        tabs[indent + 1] + "Key:" |> output
        type__str output (indent + 1) kType
        tabs[indent + 1] + "Val:" |> output
        type__str output (indent + 1) vType

let type__strFinal t = 
    let w = empty__TextBlockWriter()
    type__str w.newline 0 t
    w.text()

type TypeCat = {
types: Dictionary<string,Type> }

with override this.ToString() = 
        this.types.Count.ToString() + " types"

let matchOrm (t:Type) =
    match t.tEnum with
    | TypeEnum.Orm t -> Some t 
    | _ -> None

let tc__sorted tc = 
    tc.types.Values
    |> Seq.toArray
    |> Array.sortBy(fun t -> t.sort)

let appendType tc t = 
    t.sort <- tc.types.Count
    tc.types.Add(t.name,t)
    t

let rec str__type tc s = 
    if tc.types.ContainsKey s then
        tc.types[s]
    else
        let t = 
            if s.EndsWith " option" then
                let tt = s.Substring(0,s.Length - " option".Length) |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Option tt
                    custom =  false
                    src = [| |] }
            else if s.EndsWith "[]" then
                let tt = s.Substring(0,s.Length - 2).Trim() |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Ary tt
                    custom =  false
                    src = [| |] }
            else if s.EndsWith " array" then
                let tt = s.Substring(0,s.Length - 6).Trim() |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Ary tt
                    custom =  false
                    src = [| |] }
            else if s.StartsWith "List<" then
                let tt = (find ("<",">") s).Trim() |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.List tt
                    custom =  false
                    src = [| |] }
            else if s.StartsWith "Dictionary<" then
                let k = find ("<",",") s |> str__type tc
                let v = (findBidirectional (",",">") s).Trim() |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Dictionary(k,v)
                    custom =  false
                    src = [| |] }
            else if s.Contains "*" then
                { 
                    name = s 
                    sort = 0
                    tEnum = 
                        split "*" s
                        |> Array.map(fun i -> i.Trim())
                        |> Array.filter(fun i -> i.Length > 0)
                        |> Array.map(str__type tc)
                        |> TypeEnum.Product
                    custom =  true
                    src = [| |] }
            else
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Primitive
                    custom =  false
                    src = [| |] }

        if tc.types.ContainsKey s = false then
            tc.types.Add(s,t)
        t

let parseCustomTypes (lines:string[]) = 

    let rx = string__regex @"(?<=type\s+)\w+"

    let buffer = new List<string * List<string>>()

    [| 0 .. lines.Length - 1 |]
    |> Array.iter(fun i -> 
        let line = lines[i]
        let m = regex_match rx line
        if m.Length > 0 then
            let l = new List<string>()
            l.Add line
            buffer.Add(m,l)
        else
            if buffer.Count > 0 then
                let m,l = buffer[buffer.Count - 1]
                l.Add line)

    [| 0.. buffer.Count - 1 |]
    |> Array.map(fun i -> 
        let name,src = buffer[i]
        let lines = src.ToArray() |> Array.filter(fun i -> i.Length > 0)

        let mutable s  = src.ToArray() |> String.concat crlf
        s <- s.Substring((s.IndexOf name) + name.Length).Trim()

        let e = 
            if s.EndsWith "}" then
                TypeEnum.Structure [||]
            else if s.Contains "|" then
                if s.Contains " = " then
                    TypeEnum.Enum [||]
                else
                    TypeEnum.Sum [||]
            else
                TypeEnum.Structure [||]

        let r = string__regex (@"type\s+" + name)
        let head = 
            lines 
            |> Array.findIndex(fun line ->
                let s = regex_match r line
                s.Length > 0)

        {   name = name
            sort = i
            tEnum = e
            custom =  true
            src = 
                src.ToArray()
                |> Array.filter(fun i -> i.Length > 0) })

