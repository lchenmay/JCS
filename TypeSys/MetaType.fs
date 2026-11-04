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
open Util.Text
open Util.Json
open Util.FileSys
open Util.Db
open Util.Rdbms
open Util.DbQuery
open Util.DbTx
open Util.Orm
open Util.OrmDb

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
| Integer of string * string
| Float of string * string
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
| OrmRcd of Table
| Ormp of Table
| Option of Type
| Ary of Type
| List of Type
| Dictionary of Type * Type
| SortedDictionary of Type * Type
| ConcurrentDictionary of Type * Type
| ModDictInt64 of Type
| ModDictStr of Type
| ListImmutable of Type
| Fun of Type * Type
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
| OrmRcd = 5
| Ormp = 6
| Option = 7
| Ary = 8
| List = 9
| Dictionary = 10
| SortedDictionary = 11
| ConcurrentDictionary = 12
| ModDictInt64 = 13
| ModDictStr = 14
| ListImmutable = 15
| Fun = 16

let typeEnum__plain e = 
    match e with
    | TypeEnum.Primitive -> TypeEnumPlain.Primitive
    | TypeEnum.Structure v -> TypeEnumPlain.Structure
    | TypeEnum.Product v -> TypeEnumPlain.Product
    | TypeEnum.Sum v -> TypeEnumPlain.Sum
    | TypeEnum.Enum v -> TypeEnumPlain.Enum
    | TypeEnum.OrmRcd v -> TypeEnumPlain.OrmRcd
    | TypeEnum.Ormp v -> TypeEnumPlain.Ormp
    | TypeEnum.Option v -> TypeEnumPlain.Option
    | TypeEnum.Ary v -> TypeEnumPlain.Ary
    | TypeEnum.List v -> TypeEnumPlain.List
    | TypeEnum.Dictionary (kType,vType) -> TypeEnumPlain.Dictionary
    | TypeEnum.SortedDictionary (kType,vType) -> TypeEnumPlain.SortedDictionary
    | TypeEnum.ConcurrentDictionary (kType,vType) -> TypeEnumPlain.ConcurrentDictionary
    | TypeEnum.ModDictInt64 v -> TypeEnumPlain.ModDictInt64
    | TypeEnum.ModDictStr v -> TypeEnumPlain.ModDictStr
    | TypeEnum.ListImmutable v -> TypeEnumPlain.ListImmutable
    | TypeEnum.Fun (src,dst) -> TypeEnumPlain.Fun

let type__supportMarshall t = 
    match t.tEnum with
    | TypeEnum.Fun (src,dst) -> false
    | _ -> true

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
    | TypeEnum.OrmRcd v -> ()
    | TypeEnum.Option v ->
        tabs[indent + 1] + "Option of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.Ary v ->
        tabs[indent + 1] + "array of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.List v ->
        tabs[indent + 1] + "List of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.ListImmutable v ->
        tabs[indent + 1] + "ListImmutable of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.Dictionary (kType,vType) ->
        tabs[indent + 1] + "Key:" |> output
        type__str output (indent + 1) kType
        tabs[indent + 1] + "Val:" |> output
        type__str output (indent + 1) vType
    | TypeEnum.SortedDictionary (kType,vType) ->
        tabs[indent + 1] + "Key:" |> output
        type__str output (indent + 1) kType
        tabs[indent + 1] + "Val:" |> output
        type__str output (indent + 1) vType
    | TypeEnum.ConcurrentDictionary (kType,vType) ->
        tabs[indent + 1] + "Key:" |> output
        type__str output (indent + 1) kType
        tabs[indent + 1] + "Val:" |> output
        type__str output (indent + 1) vType
    | TypeEnum.ModDictInt64 v ->
        tabs[indent + 1] + "ModDict Int64 of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.ModDictStr v ->
        tabs[indent + 1] + "ModDict Str of:" |> output
        type__str output (indent + 1) v
    | TypeEnum.Fun (src, dst) ->
        tabs[indent + 1] + " function:" |> output
        type__str output (indent + 1) src
        tabs[indent + 1] + " -> " |> output
        type__str output (indent + 1) dst
    | _ -> ()

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
    | TypeEnum.OrmRcd t -> Some t 
    | _ -> None

let tc__sorted tc = 
    tc.types.Values
    |> Seq.toArray
    |> Array.sortBy(fun t -> t.sort)

let appendType tc t = 
    t.sort <- tc.types.Count
    tc.types.Add(t.name,t)
    t

// §13 量纲泛型支持：判定自定义类型是否为“量纲泛型”类型
// 量纲参数（如 'Measure）：仅出现在基元量纲字符串内（float<'Measure> 的 < > 里）；
// 值参数（如 'Ctx）：作为字段自身类型出现 → 返回 None（值泛型属 Phase 2，本阶段不处理）
let measureGparamOf (t:Type) : string option =
    if t.src.Length = 0 then None
    else
        let head = t.src[0].Trim()
        let i = head.IndexOf '<'
        if i < 0 then None
        else
            let close = head.IndexOf('>', i)
            if close < 0 then None
            else
                let g = head.Substring(i + 1, close - i - 1).Trim()
                if g.Length = 0 || g.[0] <> ''' then None
                else
                    let body =
                        t.src
                        |> Array.skip 1
                        |> Array.fold (fun s l -> s + " " + l) ""
                    let inMeasure =
                        body.Contains("float<" + g + ">")
                        || body.Contains("float32<" + g + ">")
                        || body.Contains("int<" + g + ">")
                        || body.Contains("int32<" + g + ">")
                        || body.Contains("int64<" + g + ">")
                    let asValue = body.Contains(" " + g)
                    if inMeasure && not asValue then Some g else None

// 从首个 '<' 起，按括号深度找到与之配平的 '>'，返回内部内容（不含两端括号）
// 处理形如 List<float<b/a>> 中尾部的 '>>'：取最外层的配平 '>'，避免量纲的 '>' 被误当泛型闭合
let genericInner (s:string) =
    let i = s.IndexOf '<'
    if i < 0 then s
    else
        let rec loop j depth =
            if j >= s.Length then s.Substring(i + 1)
            elif s.[j] = '<' then loop (j + 1) (depth + 1)
            elif s.[j] = '>' then
                if depth = 1 then s.Substring(i + 1, j - i - 1)
                else loop (j + 1) (depth - 1)
            else loop (j + 1) depth
        loop (i + 1) 1

// 在 '<>' 深度为 0 的位置按分隔符切分（用于 Dictionary 的 k,v，避免量纲括号干扰）
let splitTopLevel (sep:char) (s:string) =
    let parts = ResizeArray<string>()
    let mutable depth = 0
    let mutable start = 0
    for i in 0 .. s.Length - 1 do
        if s.[i] = '<' then depth <- depth + 1
        elif s.[i] = '>' then depth <- depth - 1
        elif s.[i] = sep && depth = 0 then
            parts.Add(s.Substring(start, i - start))
            start <- i + 1
    parts.Add(s.Substring(start))
    parts.ToArray()

let rec str__type tc s = 
    if tc.types.ContainsKey s then
        tc.types[s]
    else
        let t = 
            if s.Contains "->" then
                let index = s.IndexOf "->"
                let src = s.Substring(0,index).Trim() |> str__type tc
                let dst = s.Substring(index + "->".Length).Trim() |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Fun (src,dst)
                    custom =  false
                    src = [| |] }
            else if s.EndsWith " option" then
                let tt = s.Substring(0,s.Length - " option".Length) |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Option tt
                    custom =  false
                    src = [| |] }
            else if s.EndsWith " list" then
                let tt = s.Substring(0,s.Length - " list".Length) |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.ListImmutable tt
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
                let tt = genericInner s |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.List tt
                    custom =  false
                    src = [| |] }
            else if s.StartsWith "ConcurrentDictionary<" then
                let parts = genericInner s |> splitTopLevel ','
                let k = str__type tc (parts.[0].Trim())
                let v = str__type tc (parts.[1].Trim())
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.ConcurrentDictionary(k,v)
                    custom =  false
                    src = [| |] }
            else if s.StartsWith "SortedDictionary<" then
                let parts = genericInner s |> splitTopLevel ','
                let k = str__type tc (parts.[0].Trim())
                let v = str__type tc (parts.[1].Trim())
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.SortedDictionary(k,v)
                    custom =  false
                    src = [| |] }
            else if s.StartsWith "Dictionary<" then
                let parts = genericInner s |> splitTopLevel ','
                let k = str__type tc (parts.[0].Trim())
                let v = str__type tc (parts.[1].Trim())
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.Dictionary(k,v)
                    custom =  false
                    src = [| |] }
            else if s.StartsWith "ModDictInt64<" then
                let tt = genericInner s |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.ModDictInt64 tt
                    custom =  false
                    src = [| |] }
            else if s.StartsWith "ModDictStr<" then
                let tt = genericInner s |> str__type tc
                { 
                    name = s 
                    sort = 0
                    tEnum = TypeEnum.ModDictStr tt
                    custom =  false
                    src = [| |] }
            else if s.Contains "*" then
                let mutable ss = s.Trim()
                if ss.StartsWith "(" && ss.EndsWith ")" then
                    ss <- s.Substring(1,ss.Length - 2)
                { 
                    name = ss 
                    sort = 0
                    tEnum = 
                        split "*" ss
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

let parseCustomTypes 
    (modulenames:List<string>)
    (lines:string[]) = 

    let rx1 = str__regex @"(?<=type\s+)\w+"
    let rx2 = str__regex @"(?<=module\s+?)\w+(?=\s*?=)"

    let buffer = new List<string * string * List<string>>()

    let mutable modulename = ""

    [| 0 .. lines.Length - 1 |]
    |> Array.iter(fun i -> 
        let line = lines[i].TrimEnd()

        let mn = regex_match rx2 line
        if mn.Length > 0 then
            modulename <- mn
            modulenames.Add modulename

        let m = regex_match rx1 line
        if m.Length > 0 then
            let srcLines = new List<string>()
            srcLines.Add line
            let fullname = 
                if line.StartsWith tab then
                    modulename + "." + m
                else
                    m
            buffer.Add(m,fullname,srcLines)
        else
            if buffer.Count > 0 then
                let m,fullname,srcLines = buffer[buffer.Count - 1]
                srcLines.Add line)

    let trimWith (src:List<string>) = 

        let mutable index = -1

        [| 0.. src.Count - 1 |]
        |> Array.iter(fun i -> 
            let s = src[i]
            if index = -1 then
                let i1 = s.LastIndexOf "with"
                let i2 = s.LastIndexOf "}"
                if i2 < i1 then
                    index <- i
                    src[i] <- s.Substring(0,i2 + 1))

        if index > 0 then
            src.RemoveRange(index + 1,src.Count - index - 1)

    [| 0.. buffer.Count - 1 |]
    |> Array.map(fun i -> 
        let name,fullname,src = buffer[i]

        trimWith src

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

        let r = str__regex (@"type\s+" + name)
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

