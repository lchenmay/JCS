module TypeSys.CodeRobotI

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

let fdef__str def = 
    match def with
    | FK t -> "FK -> [" + t.tableName + "]"
    | Caption length -> "Caption Length = " + length.ToString()
    | Chars length -> "Chars Length = " + length.ToString()
    | Link length -> "Link Length = " + length.ToString()
    | Text -> "Text"
    | Bin -> "Bin"
    | Integer -> "Integer"
    | Float -> "Float"
    | Boolean -> "Boolean"
    | SelectLines lines -> "SelectLines"
    | Timestamp -> "Timestamp"
    | TimeSeries -> "TimeSeries"
    | Other -> "N/A"

let fdef__srcTypes (table,name,def) = 
    match def with
    | FK t -> "FK","int64","number"
    | Caption length -> "Caption","string","string"
    | Chars length -> "Chars","string","string"
    | Link length -> "Link","string","string"
    | Text -> "Text","string","string"
    | Bin -> "Bin","byte[]","array"
    | Integer -> "Integer","int64","number"
    | Float -> "Float","double","number"
    | Boolean -> "Boolean","bool","boolean"
    | SelectLines lines -> 
        let enumType = table.typeName.ToLower() + name + "Enum"
        enumType,enumType,enumType
    | Timestamp -> "Timestamp","DateTime","Date"
    | TimeSeries -> "TimeSeries","TimeSpan","Date"
    | Other -> "","Object","any"

let parseSelectLines (items:Dictionary<string,string>) = 
    Util.Text.split "///" (items.["lines"])
    |> Array.map(fun line ->
        let i = line.IndexOf "//"    
        line.Substring(0,i),line.Substring(i + 2))

let items__fieldo items =
    let fieldname = checkfield items "name"
    let e = checkfield items "enum"
    if fieldname.Length * e.Length > 0 then
        match e with
        | "FK" -> (fieldname, FieldDef.Other,items) |> Some
        | "Caption" -> (fieldname, FieldDef.Caption(checkfield items "length" |> parse_int32),items) |> Some
        | "Chars" -> (fieldname, FieldDef.Chars(checkfield items "length" |> parse_int32),items) |> Some
        | "Link" -> (fieldname, FieldDef.Link(checkfield items "length" |> parse_int32),items) |> Some
        | "Text" -> (fieldname, FieldDef.Text,items) |> Some
        | "Bin" -> (fieldname, FieldDef.Bin,items) |> Some
        | "Integer" -> (fieldname, FieldDef.Integer,items) |> Some
        | "Float" -> (fieldname, FieldDef.Float,items) |> Some
        | "Boolean" -> (fieldname, FieldDef.Boolean,items) |> Some
        | "SelectLines" -> (fieldname, FieldDef.SelectLines(parseSelectLines items),items) |> Some
        | "Timestamp" -> (fieldname, FieldDef.Timestamp,items) |> Some
        | "TimeSeries" -> (fieldname, FieldDef.TimeSeries,items) |> Some
        | _ -> None
    else
        None

let handleEnumFields handler = 
    table__sortedFields >> Array.iter(fun (sort,name,def,string) -> 
        match def with
        | FieldDef.SelectLines lines -> handler (name,lines)
        | _ -> ())

let fdef__tbin table l field = 

    let t__bin = new List<string>()

    let sort,name,def,json = field

    match l with
    | ProgrammingLang.FSharp ->

        let boolean() = 
            [|  ""
                "p." + name + " |> BitConverter.GetBytes |> bb.append" |]
            |> t__bin.AddRange

        let int64() = 
            [|  ""
                "p." + name + " |> BitConverter.GetBytes |> bb.append" |]
            |> t__bin.AddRange

        let timestamp() = 
            [|  ""
                "p." + name + ".Ticks |> BitConverter.GetBytes |> bb.append" |]
            |> t__bin.AddRange

        let bytes() = 
            [|  ""
                "p." + name + ".Length |> BitConverter.GetBytes |> bb.append"
                "p." + name + " |> bb.append" |]
            |> t__bin.AddRange

        let float() = 
            [|  ""
                "p." + name + " |> BitConverter.GetBytes |> bb.append" |]
            |> t__bin.AddRange

        let enum() = 
            [|  ""
                "p." + name + " |> EnumToValue |> BitConverter.GetBytes |> bb.append" |]
            |> t__bin.AddRange

        let string() = 
            [|  ""
                "let bin" + name + " = p." + name + " |> Encoding.UTF8.GetBytes"
                "bin" + name + ".Length |> BitConverter.GetBytes |> bb.append"
                "bin" + name + " |> bb.append" |]
            |> t__bin.AddRange

        match def with
        | FK table -> int64()
        | Caption length
        | Chars length
        | Link length -> string()
        | Text -> string()
        | Bin -> bytes()
        | Integer ->  int64()
        | Float -> float()
        | Boolean -> boolean()
        | SelectLines lines -> enum()
        | Timestamp -> timestamp()
        //| TimeSeries -> "TimeSeries","TimeSpan","Date"
        | _ -> ()

    | ProgrammingLang.TypeScript -> 

        let boolean() = 
            [|  ""
                "p." + name + " |> BitConverter.GetBytes |> bb.append" |]
            |> t__bin.AddRange

        let int64() = 
            [|  ""
                "bin.int64__bin (bb) (p." + name + ")" |]
            |> t__bin.AddRange

        let timestamp() = 
            [|  ""
                "bin.int64__bin (bb) (p." + name + ")" |]
            |> t__bin.AddRange

        let bytes() = 
            [|  ""
                "p." + name + ".Length |> BitConverter.GetBytes |> bb.append"
                "p." + name + " |> bb.append" |]
            |> t__bin.AddRange

        let float() = 
            [|  ""
                "bin.float__bin (bb) (p." + name + ")" |]
            |> t__bin.AddRange

        let enum() = 
            [|  ""
                "bin.int32__bin (bb) (" + table.typeName.ToLower() + name + "Enum__int(p." + name + "))" |]
            |> t__bin.AddRange

        let string() = 
            [|  ""
                "bin.str__bin (bb) (p." + name + ")" |]
            |> t__bin.AddRange

        match def with
        | FK table -> int64()
        | Caption length
        | Chars length
        | Link length -> string()
        | Text -> string()
        | Bin -> bytes()
        | Integer ->  int64()
        | Float -> float()
        | Boolean -> boolean()
        | SelectLines lines -> enum()
        | Timestamp -> timestamp()
        //| TimeSeries -> "TimeSeries","TimeSpan","Date"
        | _ -> ()

    | _ -> ()

    t__bin

let fdef__bint table l field = 

    let bin__t = new List<string>()

    let sort,name,def,json = field

    match l with
    | ProgrammingLang.FSharp ->

        let boolean() = 
            [|  ""
                "p." + name + " <- BitConverter.ToBoolean(bin,index.Value)"
                "index.Value <- index.Value + 1" |]
            |> bin__t.AddRange

        let int64() = 
            [|  ""
                "p." + name + " <- BitConverter.ToInt64(bin,index.Value)"
                "index.Value <- index.Value + 8" |]
            |> bin__t.AddRange

        let timestamp() = 
            [|  ""
                "p." + name + " <- BitConverter.ToInt64(bin,index.Value) |> DateTime.FromBinary"
                "index.Value <- index.Value + 8" |]
            |> bin__t.AddRange

        let bytes() = 
            [|  ""
                "let length_" + name + " = BitConverter.ToInt32(bin,index.Value)"
                "index.Value <- index.Value + 4"
                "p." + name + " <- Array.sub bin index.Value length_" + name
                "index.Value <- index.Value + length_" + name + "" |]
            |> bin__t.AddRange

        let float() = 
            [|  ""
                "p." + name + " <- BitConverter.ToDouble(bin,index.Value)"
                "index.Value <- index.Value + 8" |]
            |> bin__t.AddRange

        let enum() = 
            [|  ""
                "p." + name + " <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue"
                "index.Value <- index.Value + 4" |]
            |> bin__t.AddRange

        let string() = 
            [|  ""
                "let count_" + name + " = BitConverter.ToInt32(bin,index.Value)"
                "index.Value <- index.Value + 4"
                "p." + name + " <- Encoding.UTF8.GetString(bin,index.Value,count_" + name + ")"
                "index.Value <- index.Value + count_" + name + "" |]
            |> bin__t.AddRange

        match def with
        | FK table -> int64()
        | Caption length
        | Chars length
        | Link length -> string()
        | Text -> string()
        | Bin -> bytes()
        | Integer ->  int64()
        | Float -> float()
        | Boolean -> boolean()
        | SelectLines lines -> enum()
        | Timestamp -> timestamp()
        //| TimeSeries -> "TimeSeries","TimeSpan","Date"
        | _ -> ()

    | ProgrammingLang.TypeScript -> 

        let boolean() = 
            [|  ""
                "p." + name + " = bin.bin__boolean (bi)"
                "bi.index = bi.index + 1" |]
            |> bin__t.AddRange

        let int64() = 
            [|  ""
                "p." + name + " = bin.bin__int64 (bi)"
                "bi.index = bi.index + 8" |]
            |> bin__t.AddRange

        let timestamp() = 
            [|  ""
                "p." + name + " = bin.bin__int64 (bi)"
                "bi.index = bi.index + 8" |]
            |> bin__t.AddRange

        let bytes() = 
            [|  ""
                "p." + name + ".Length |> BitConverter.GetBytes |> bb.append"
                "p." + name + " |> bb.append" |]
            |> bin__t.AddRange

        let float() = 
            [|  ""
                "p." + name + " = bin.bin__float (bi)"
                "bi.index = bi.index + 8" |]
            |> bin__t.AddRange

        let enum() = 
            [|  ""
                "p." + name + " = bin.int__" + table.typeName.ToLower() + name + "Enum(bin__int32 (bi))"
                "bi.index = bi.index + 4" |]
            |> bin__t.AddRange

        let string() = 
            [|  ""
                "p." + name + " = bin.bin__str (bi)" |]
            |> bin__t.AddRange

        match def with
        | FK table -> int64()
        | Caption length
        | Chars length
        | Link length -> string()
        | Text -> string()
        | Bin -> bytes()
        | Integer ->  int64()
        | Float -> float()
        | Boolean -> boolean()
        | SelectLines lines -> enum()
        | Timestamp -> timestamp()
        //| TimeSeries -> "TimeSeries","TimeSpan","Date"
        | _ -> ()

    | _ -> ()

    bin__t

let fdef__tjson field = 

    let sort,name,def,json = field

    let str() = "p." + name + " |> Json.Str"
    
    let tostr() = "p." + name + ".ToString() |> Json.Num"
    
    let bool() = "if p." + name + " then Json.True else Json.False"

    let timestamp() = "(p." + name + " |> Util.Time.wintime__unixtime).ToString() |> Json.Num"
    
    let bytes() = "p." + name + " |> Convert.ToBase64String |> Json.Str"

    let enum() = "(p." + name + " |> EnumToValue).ToString() |> Json.Num"

    match def with
    | FK table -> tostr()
    | Caption length
    | Chars length
    | Link length -> str()
    | Text -> str()
    | Bin -> bytes()
    | Integer ->  tostr()
    | Float -> tostr()
    | Boolean -> bool()
    | SelectLines lines -> enum()
    | Timestamp -> timestamp()
    //| TimeSeries -> "TimeSeries","TimeSpan","Date"
    | _ -> ""


let fdef__jsont field = 

    let buffer = new List<string>()

    let sort,name,def,json = field

    let boolean() = 
        [|  ""
            "p." + name + " <- checkfield fields \"" + name + "\" = \"true\"" |]
        |> buffer.AddRange

    let int64() = 
        [|  ""
            "p." + name + " <- checkfield fields \"" + name + "\" |> parse_int64" |]
        |> buffer.AddRange
    
    let timestamp() = 
        [|  ""
            "p." + name + " <- checkfield fields \"" + name + "\" |> parse_int64 |> Util.Time.unixtime__wintime" |]
        |> buffer.AddRange
        
    let bytes() = 
        //[|  ""
        //    "let length_" + name + " = BitConverter.ToInt32(bin,index.Value)"
        //    "index.Value <- index.Value + 4"
        //    "p." + name + " <- Array.sub bin index.Value length_" + name
        //    "index.Value <- index.Value + length_" + name + "" |]
        //|> buffer.AddRange
        ()

    let float() = 
        [|  ""
            "p." + name + " <- checkfield fields \"" + name + "\" |> parse_float" |]
        |> buffer.AddRange

    let enum() = 
        [|  ""
            "p." + name + " <- checkfield fields \"" + name + "\" |> parse_int32 |> EnumOfValue" |]
        |> buffer.AddRange

    let string() = 
        [|  ""
            "p." + name + " <- checkfield fields \"" + name + "\"" |]
        |> buffer.AddRange

    let stringLength l = 
        [|  ""
            "p." + name + " <- checkfieldz fields \"" + name + "\" " + l.ToString() |]
        |> buffer.AddRange

    match def with
    | FK table -> int64()
    | Caption length -> stringLength length
    | Chars length -> stringLength length
    | Link length -> stringLength length
    | Text -> string()
    | Bin -> bytes()
    | Integer ->  int64()
    | Float -> float()
    | Boolean -> boolean()
    | SelectLines lines -> enum()
    | Timestamp -> timestamp()
    //| TimeSeries -> "TimeSeries","TimeSpan","Date"
    | _ -> ()

    buffer


let fdef__empty lang t name def = 
    match lang with
    | ProgrammingLang.FSharp -> 
        match def with
        | FK t -> "0L"
        | Caption length
        | Chars length
        | Link length -> "\"\""
        | Text -> "\"\""
        | Bin -> "[||]"
        | Integer -> "0L"
        | Float -> "0.0"
        | Boolean -> "true"
        | SelectLines lines -> "EnumOfValue 0"
        | Timestamp -> "DateTime.MinValue"
        | TimeSeries -> "TimeSpan.MinValue"
        | Other -> ""

    | _ -> 
        match def with
        | FK t -> "0"
        | Caption length
        | Chars length
        | Link length -> "\"\""
        | Text -> "\"\""
        | Bin -> "[]"
        | Integer -> "0"
        | Float -> "0.0"
        | Boolean -> "true"
        | SelectLines lines -> "int__" + t.tableName + name + "Enum(0)"
        | Timestamp -> "DateTime.MinValue"
        | TimeSeries -> "TimeSpan.MinValue"
        | Other -> ""

        