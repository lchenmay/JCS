module TypeSys.SrOrm

open System
open System.Collections.Generic
open System.Data
open System.Data.SqlClient
open System.Text
open System.Text.RegularExpressions

open Util.Runtime
open Util.Cat
open Util.CollectionSortedAccessor
open Util.Text
open Util.Db
open Util.DbQuery
open Util.DbTx

open TypeSys.Orm


type ProgrammingLang = 
| FSharp
| Elixir

type Rdbms = 
| NotAvailable
| SqlServer
| MySql
| PostgreSql

type TransObj = {
    fields: Dictionary<string,string>[];
    fkins: List<TransObj*string>;
    fkouts: List<string*TransObj>;
    idstarting: int64;
    shorthand:string;
    name:string }

//let load designfile =

//    let root = designfile |> Util.Json.parser

//    let transobjects =
//        root.children.[0].children.[0].children.ToArray()
//        |> Array.map(fun item ->
//            let dict = Util.Json.json__items item.content
//            let mutable index = 4
//            let fields =
//                item.children.[0].children.ToArray()
//                |> Array.map(fun c ->
//                    let items = c.content |> Util.Json.json__items
//                    items.Add("index", index.ToString())
//                    index <- index + 1
//                    items)

//            let name = dict.["name"]
//            let shorthand =
//                if(dict.ContainsKey("shorthand")) then
//                    dict.["shorthand"]
//                else
//                    let cs = 
//                        name.ToCharArray()
//                        |> Array.filter(fun c ->
//                            let a = string(c)
//                            let b = a.ToUpper()
//                            a = b && a <> "_")
//                    new string(cs)

//            let transobj = {
//                fields = fields;
//                fkins = new ResizeArray<TransObj*string>();
//                fkouts = new ResizeArray<string*TransObj>();
//                idstarting = 
//                    if dict.ContainsKey "id" then 
//                        Util.Text.parse_int64 dict.["id"]
//                    else 
//                        0L;
//                shorthand = shorthand.ToLower();
//                name = name }
//            name, transobj)
//        |> Util.Collection.array__sorteddict fst

//    transobjects
//    |> Seq.iter(fun item ->
//        let transobj = snd(item.Value)
//        transobj.fields
//        |> Seq.iter(fun field ->
//            if(field.["enum"] = "FK") then
//                let fieldname = field.["name"]
//                let ref =
//                    if(field.ContainsKey("ref")) then
//                        field.["ref"]
//                    else
//                        match (transobjects.Keys |> Seq.tryFind(fun k -> k.EndsWith(fieldname))) with
//                        | Some(v) -> v
//                        | None -> ""
//                if(ref.Length > 0 ) then //&& transobjects.ContainsKey ref) then
//                    let referece = snd(transobjects.[ref])
//                    referece.fkins.Add(transobj, fieldname)
//                    transobj.fkouts.Add(fieldname,referece)
//                ())

//        ())

//    let dict = Util.Json.json__items(root.children.[0].content)
//    dict.["project"], transobjects

let tab = "    "

//===============================================================================

// wT.Rcd
let wRcd(transobj:TransObj)(sb:StringBuilder)(field:Dictionary<string,string>) =

    let fieldname, fieldenum = field.["name"], field.["enum"]

    let cmt = 
        let s = Util.Json.checkfield(field)("cmt")
        if(s.Length>0)then
            "  // "+s
        else
            ""

    if field.ContainsKey("type") then
        sb.Append(crlf + tab + tab + "mutable " + fieldname + ": " + field.["type"] + ";" + cmt) |> ignore
    else if(fieldenum = "SelectLines") then
        sb.Append(crlf + tab + tab + "mutable " + fieldname + ": " + transobj.shorthand + fieldname + "Enum;" + cmt) |> ignore
    else
        sb.Append(crlf + tab + tab + "mutable " + fieldname + ": " + fieldenum + ";" + cmt) |> ignore

let w1m(transobj:TransObj)(sb:StringBuilder)(field:Dictionary<string,string>) =
    let fieldname, fieldenum = field.["name"], field.["enum"]
    if field.ContainsKey("type") then
        sb.Append(crlf + tab + tab + "mutable " + fieldname + ": " + field.["type"] + ";") |> ignore
    else if(fieldenum = "SelectLines") then
        sb.Append(crlf + tab + tab + "mutable " + fieldname + ": " + transobj.shorthand + fieldname + "Enum;") |> ignore
    else
        sb.Append(crlf + tab + tab + "mutable " + fieldname + ": " + fieldenum + ";") |> ignore

// wT.fields__rcd params
let wfields__rcdParams(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(field.["name"].ToLower() + "_,") |> ignore

// wT.fields__rcd assigns
let wfields__rcdAssigns(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(crlf + tab + tab + field.["name"] + " = " + field.["name"].ToLower() + "_;") |> ignore

// wT.sqlparams
let wsqlparams(sb:StringBuilder)(field:Dictionary<string,string>) =
    match field.["enum"] with
    | "Timestamp" -> sb.Append(crlf + tab + tab + "new SqlParameter(\"" + field.["name"] + "\", p." + field.["name"] + ".Ticks);") |> ignore
    | "TimeSeries" -> sb.Append(crlf + tab + tab + "new SqlParameter(\"" + field.["name"] + "\", TimeSeries__bytes p." + field.["name"] + ");") |> ignore
    | _ -> sb.Append(crlf + tab + tab + "new SqlParameter(\"" + field.["name"] + "\", p." + field.["name"] + ");") |> ignore

// wT.fieldorders
let wfieldorders(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append("[" + field.["name"] + "],") |> ignore

// wT.db__rcd
let wdb__rcd(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(crlf + tab + tab + "p." + field.["name"] + " <- ") |> ignore
    let l = "line.[" + field.["index"] + "]"
    let wrap =
        if(field.ContainsKey("type"))then
            field.["wrap"]
        else
            ""
    match field.["enum"] with
        | "FK" ->
            sb.Append(crlf + tab + tab + tab + "if(System.Convert.IsDBNull(" + l + ")) then") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + "0L") |> ignore
            sb.Append(crlf + tab + tab + tab + "else") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + l + " :?> int64") |> ignore
        | "Integer" ->
            sb.Append(crlf + tab + tab + tab + "if(System.Convert.IsDBNull(" + l + ")) then") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + "0L") |> ignore
            sb.Append(crlf + tab + tab + tab + "else") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + l + " :?> int64") |> ignore
            if(wrap.Length > 0) then
                sb.Append(crlf + tab + tab + tab + "|> " + wrap) |> ignore
        | "Money"
        | "Float" ->
            sb.Append(crlf + tab + tab + tab + "if(System.Convert.IsDBNull(" + l + ")) then") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + "0.0") |> ignore
            sb.Append(crlf + tab + tab + tab + "else") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + l + " :?> float") |> ignore
            if(wrap.Length > 0) then
                sb.Append(crlf + tab + tab + tab + "|> " + wrap) |> ignore

        | "SelectLines" ->
            sb.Append("LanguagePrimitives.EnumOfValue(if(System.Convert.IsDBNull(" + l + ")) then 0 else " + l + " :?> int)") |> ignore
        | "Caption"
        | "Chars"
        | "Link"
        | "Text"
        | "RictText"
        | "Pwd" ->
            sb.Append("string(" + l + ").TrimEnd()") |> ignore
        | "Timestamp" ->
            sb.Append("DateTime.FromBinary(if(System.Convert.IsDBNull(" + l + ")) then System.DateTime.MinValue.Ticks else " + l + " :?> int64)") |> ignore
        | "Boolean" ->
            sb.Append(crlf + tab + tab + tab + "if(System.Convert.IsDBNull(" + l + ")) then") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + "false") |> ignore
            sb.Append(crlf + tab + tab + tab + "else") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + l + " :?> bool") |> ignore
        | "Bin" ->
            sb.Append(l + " :?> byte[]") |> ignore
        | "TimeSeries" -> 
            sb.Append(crlf + tab + tab + tab + "if(System.Convert.IsDBNull " + l + ") then") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + "new List<DateTime * byte[]>()") |> ignore
            sb.Append(crlf + tab + tab + tab + "else") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + "let index = ref 0") |> ignore
            sb.Append(crlf + tab + tab + tab + tab + "bin__TimeSeries(" + l + " :?> byte[],index)") |> ignore
        | _ -> ()
    sb.Append(crlf) |> ignore

let w13(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(crlf + tab + tab + "\"" + field.["name"] + "\";") |> ignore

// wT.fields
let wfields(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(crlf +  tab + tab) |> ignore
    match field.["enum"] with
    | "Caption"
    | "Chars"
    | "Link" ->
        sb.Append(field.["enum"] + "(\"" + field.["name"] + "\", " + field.["length"] + ");") |> ignore
    | "SelectLines" ->
        sb.Append(field.["enum"] + "(\"" + field.["name"] + "\", [|(\"" + field.["lines"].Replace("///", "\");(\"").Replace("//", "\",\"") + "\")|]);") |> ignore
    | _ ->
        sb.Append(field.["enum"] + "(\"" + field.["name"] + "\");") |> ignore

let w8(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(crlf + tab + tab + field.["name"] + " = p." + field.["name"] + ";" + tab + tab) |> ignore

let w9(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(crlf + tab + tab + field.["name"] + " = m." + field.["name"] + ";" + tab + tab) |> ignore

let wa(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append("[" + field.["name"] + "]=@"+field.["name"]+",") |> ignore

let wb(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(crlf +  tab + tab + tab) |> ignore
    let wrap =
        if(field.ContainsKey("type"))then
            field.["wrap"]
        else
            ""

    match field.["enum"] with
    | "Caption"
    | "Text"
    | "Pwd"
    | "Chars"
    | "Link" ->
        sb.Append(field.["name"] + " = \"\";") |> ignore
    | "SelectLines" ->
        sb.Append(field.["name"] + " = EnumOfValue(0);") |> ignore
    | "Float" ->
        if(wrap.Length>0)then
            sb.Append(field.["name"] + " = 0.0 |> "+wrap+";") |> ignore
        else
            sb.Append(field.["name"] + " = 0.0;") |> ignore
    | "FK"
    | "Integer" ->
        if(wrap.Length>0)then
            sb.Append(field.["name"] + " = 0L |> "+wrap+";") |> ignore
        else
            sb.Append(field.["name"] + " = 0L;") |> ignore
    | "Timestamp" ->
        sb.Append(field.["name"] + " = DateTime.UtcNow;") |> ignore
    | "Boolean" -> sb.Append(field.["name"] + " = true;") |> ignore
    | "Bin" -> sb.Append(field.["name"] + " = [||];") |> ignore
    | "TimeSeries" -> sb.Append(field.["name"] + " = new List<DateTime * byte[]>();") |> ignore
    | _ -> ()

let wc(sb:StringBuilder)(field:Dictionary<string,string>) =
    match field.["enum"] with
    | "Boolean" ->
        sb.Append(crlf + tab + tab + tab + "\"\\\"" + field.["name"] + "\\\": \" + (if p." + field.["name"] + " then \"true\" else \"false\");") |> ignore
    | "Timestamp" ->
        sb.Append(crlf + tab + tab + tab + "\"\\\"" + field.["name"] + "\\\": \" + (Util.Time.wintime__unixtime(p." + field.["name"] + ")).ToString();") |> ignore
        sb.Append(crlf + tab + tab + tab + "\"\\\"" + field.["name"] + "Text\\\": \\\"\" + Util.Text.timestamp__userfriendly(p." + field.["name"] + ") + \"\\\"\";") |> ignore
    | "Pwd" -> ()
    | "Caption"
    | "Text"
    | "Chars"
    | "Link" ->
        sb.Append(crlf + tab + tab + tab + "\"\\\"" + field.["name"] + "\\\": \" + \"\\\"\" + Util.Json.str__escape(p." + field.["name"] + ") + \"\\\"\";") |> ignore
    | "SelectLines" ->
        sb.Append(crlf + tab + tab + tab + "\"\\\"" + field.["name"] + "\\\": \" + EnumToValue(p." + field.["name"] + ").ToString();") |> ignore
    | "Bin" ->
        sb.Append(crlf + tab + tab + tab + "\"\\\"" + field.["name"] + "\\\": \" + \"\\\"\" + p." + field.["name"] + ".Length.ToString() + \"\\\"\";") |> ignore
    | _ -> sb.Append(crlf + tab + tab + tab + "\"\\\"" + field.["name"] + "\\\": \" + p." + field.["name"] + ".ToString();") |> ignore


// sT.rcd__dict
let s1(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(tab + tab + tab + "dict.Add(\"" + field.["name"] + "\", rcd.weak." + field.["name"]) |> ignore
    match field.["enum"] with
    | "Timestamp" ->
        sb.Append(".Ticks.ToString()") |> ignore
    | "FK"
    | "Integer"
    | "Float"
    | "Money"
    | "SelectLines"
    | "Boolean" ->
        sb.Append(".ToString()") |> ignore
    | "TimeSeries" ->
        sb.Append(".Count.ToString()") |> ignore
    | _ -> ()
    sb.Append(")" + crlf) |> ignore

// sT.rcd__dict
let s2(sb:StringBuilder)(field:Dictionary<string,string>) =
    sb.Append(tab + tab + "dict.Add(\"" + field.["name"] + "\", rcd.p." + field.["name"]) |> ignore
    match field.["enum"] with
    | "Timestamp" ->
        sb.Append(".Ticks.ToString()") |> ignore
    | "FK"
    | "Integer"
    | "Float"
    | "Money"
    | "SelectLines"
    | "Boolean" ->
        sb.Append(".ToString()") |> ignore
    | "Bin" ->
        sb.Append(".Length.ToString()") |> ignore
    | "TimeSeries" ->
        sb.Append(".Count.ToString()") |> ignore
    | _ -> ()
    sb.Append(")" + crlf) |> ignore

// create
let db1(f:Dictionary<string,string>) =
    let n = f.["name"]
    match f.["enum"] with
    | "FK" ->
        "[" + n + "] BIGINT"
    | "Caption" ->
        "[" + n + "] NVARCHAR(" + f.["length"] + ") COLLATE Chinese_PRC_CI_AS"
    | "Chars" ->
        "[" + n + "] NVARCHAR(" + f.["length"] + ") COLLATE Chinese_PRC_CI_AS"
    | "Link" ->
        "[" + n + "] NVARCHAR(" + f.["length"] + ") COLLATE Chinese_PRC_CI_AS"
    | "Text" ->
        "[" + n + "] NVARCHAR(MAX)"
    | "RichText" ->
        "[" + n + "] NVARCHAR(MAX)"
    | "Integer" ->
        "[" + n + "] BIGINT"
    | "Money" ->
        "[" + n + "] FLOAT"
    | "Float" ->
        "[" + n + "] FLOAT"
    | "Boolean" ->
        "[" + n + "] BIT"
    | "Pwd" ->
        "[" + n + "] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS"
    | "SelectLines" ->
        "[" + n + "] INT"
    | "Timestamp" ->
        "[" + n + "] BIGINT"
    | "Bin"
    | "TimeSeries" ->
        "[" + n + "] VARBINARY(MAX)"
    | _ -> ""

//===============================================================================


let sb_type = new StringBuilder()
let sb_mor = new StringBuilder()

let append_type(line:string) =
    sb_type.Append(line) |> ignore

let append_mor(line:string) =
    sb_mor.Append(line) |> ignore

let append(line:string) =
    sb_type.Append(line) |> ignore
    sb_mor.Append(line) |> ignore

//let build output (designfile, typefile, morfile,(conn:string),rdbms) =

//    let reserved_type =
//        Util.FileSys.try_read_string(typefile)
//        |> snd
//        |> Util.Text.regex_match(new Regex("//<Reserved>.*?//</Reserved>",Util.Text.regex_options))
//    let reserved_mor =
//        Util.FileSys.try_read_string(morfile)
//        |> snd
//        |> Util.Text.regex_match(new Regex("//<Reserved>.*?//</Reserved>",Util.Text.regex_options))

//    let project,transobjects = load(designfile)

//    let tab = "    "
//    append_type("module MetadataType" + crlf + crlf)
//    append_mor("module MetadataMor" + crlf + crlf)
//    //append("namespace SrOrm" + crlf)
//    append(crlf)
//    append(tab + "open LanguagePrimitives" + crlf)
//    append(tab + "open System" + crlf)
//    append(tab + "open System.Collections.Generic" + crlf)
//    append(tab + "open System.Data.SqlClient" + crlf)
//    append(tab + "open System.Text" + crlf)
//    append(tab + "open Util.Cat" + crlf)
//    append(tab + "open Util.Perf" + crlf)
//    append(tab + "open Util.Measures" + crlf)
//    append(tab + "open Util.Db" + crlf)
//    append(tab + "open Util.DbQuery" + crlf)
//    append(tab + "open Util.DbTx" + crlf)
//    append(tab + "open Util.Bin" + crlf + crlf + crlf)
//    append(tab + "open Util.Orm" + crlf + crlf + crlf)
//    if(reserved_type.Length > 0)then
//        append_type(reserved_type + crlf)
//    else
//        append_type("//<Reserved>" + crlf + "//</Reserved>" + crlf)
//    if(reserved_mor.Length > 0)then
//        append_mor(reserved_mor + crlf)
//    else
//        append_mor("//<Reserved>" + crlf + "//</Reserved>" + crlf)
//    append_mor(tab + "open MetadataType" + crlf + crlf)
//    append_type(crlf)

//    let fields__builder(builder)(fields) =
//        let sb = new StringBuilder()
//        fields |> Seq.iter(fun field -> field |> builder(sb))
//        sb.ToString()

//    let fields__builder_cuttail(builder)(fields) =
//        let sb = new StringBuilder()
//        fields |> Seq.iter(fun field -> field |> builder(sb))
//        let s = sb.ToString()
//        s.Substring(0, s.Length - 1)

//    let coding transobj =

//        append(tab + "// ["+transobj.name+"]" + crlf + crlf)

//        transobj.fields
//        |> Seq.filter(fun f -> f.["enum"] = "SelectLines")
//        |> Seq.iter(fun f ->
//            append_type(tab + "type " + transobj.shorthand + f.["name"] + "Enum =" + crlf)
//            let lines = Util.Text.split "///" (f.["lines"])
//            [0..lines.Length-1]
//            |> Seq.iter(fun i ->
//                let line = lines.[i].Replace("//", " = " + i.ToString() + " //")
//                tab + tab + "| " + line + crlf |> append_type)
//            append_type(crlf)

//            append_type(tab + "let " + transobj.shorthand + f.["name"] + "Enums = [|")
//            [0..lines.Length-1]
//            |> Seq.map(fun i ->
//                let line = lines.[i]
//                let text = line.Substring(0,line.IndexOf("//"))
//                transobj.shorthand + f.["name"] + "Enum" + "." + text)
//            |> String.concat("; ")
//            |> append_type
//            append_type("|]" + crlf)
//            append_type(crlf)

//            append_type(tab + "let " + transobj.shorthand + f.["name"] + "Enumstrs = [|")
//            [0..lines.Length-1]
//            |> Seq.map(fun i ->
//                let line = lines.[i]
//                let text = line.Substring(0,line.IndexOf("//"))
//                "\"" + text + "\""
//            )
//            |> String.concat("; ")
//            |> append_type
//            append_type("|]" + crlf)
//            append_type(crlf)
                
//            let e = transobj.shorthand + f.["name"] + "Enumstrs"
//            append_type(tab + (sprintf "str__enums.Add((\"%s\",\"%s\"),%s)" transobj.shorthand f.["name"] e) + crlf)

//            append_type(tab + "let " + transobj.shorthand + f.["name"] + "Num = " + lines.Length.ToString() + crlf)
//            append_type(crlf)

//            append_type(tab + "let int__" + transobj.shorthand + f.["name"] + "Enum(v) = " + crlf)
//            append_type(tab + tab + "match v with" + crlf)
//            [0..lines.Length-1]
//            |> Seq.iter(fun i ->
//                let line = lines.[i]
//                let text = line.Substring(0,line.IndexOf("//"))
//                append_type(tab + tab + "| " + i.ToString() + " -> Some("+ transobj.shorthand + f.["name"] + "Enum" + "."+text+")" + crlf))
//            append_type(tab + tab + "| _ -> None" + crlf)

//            append_type(tab + "let str__" + transobj.shorthand + f.["name"] + "Enum(s) = " + crlf)
//            append_type(tab + tab + "match s with" + crlf)
//            [0..lines.Length-1]
//            |> Seq.iter(fun i ->
//                let line = lines.[i]
//                let text = line.Substring(0,line.IndexOf("//"))
//                append_type(tab + tab + "| \"" + text + "\" -> Some("+ transobj.shorthand + f.["name"] + "Enum" + "."+text+")" + crlf))
//            append_type(tab + tab + "| _ -> None" + crlf)

//            append_type(crlf)
//            append_type(tab + crlf)
//            append_type(tab + "let " + transobj.shorthand + f.["name"] + "Enum__caption(e) = " + crlf)
//            append_type(tab + tab + "match e with" + crlf)
//            [| 0..lines.Length-1 |]
//            |> Array.iter(fun i ->
//                let line = lines.[i]
//                let text = line.Substring(0,line.IndexOf("//"))
//                let caption = line.Substring(line.IndexOf("//") + 2)
//                append_type(tab + tab + "| " + transobj.shorthand + f.["name"] + "Enum" + "." + text + " -> \"" + caption + "\"" + crlf))
//            let line = lines.[0]
//            let caption = line.Substring(line.IndexOf("//") + 2)
//            append_type(tab + tab + "| _ -> \"" + caption + "\"" + crlf)
//            append_type(tab + crlf)
//            append_type(crlf)

//            ())

//        transobj.fields
//        |> Seq.iter(fun f ->
//            match f.["enum"] with
//            | "Caption"
//            | "Chars" 
//            | "Link" -> 
//                append_type(tab + "let " + transobj.shorthand + "_" + f.["name"]+"_length = "+f.["length"]+crlf)
//                append_type(crlf)
//            | _ -> ())


//        let shorthand = transobj.shorthand.ToUpper()
//        let ptype = "p" + shorthand
//        let t = transobj.shorthand.ToUpper()

//        append_type(tab + "type "+ptype+" = {" + fields__builder_cuttail(wRcd(transobj))(transobj.fields) + crlf + tab + " }" + crlf + crlf)

//        //append_type(tab + "type m"+ptype+" = {" + crlf + tab + tab + "mutable muted: bool;" + fields__builder_cuttail(w1m(transobj))(transobj.fields) + " }" + crlf + crlf)

//        append_mor(tab + "let rcd__" + shorthand + "_ID (rcd:" + shorthand + ") = rcd.ID" + crlf)
//        append_mor(tab + "let rcd__" + shorthand + "_Createdat (rcd:" + shorthand + ") = rcd.Createdat" + crlf)
//        append_mor(tab + "let rcd__" + shorthand + "_Updatedat (rcd:" + shorthand + ") = rcd.Updatedat" + crlf)
//        append_mor(tab + "let rcd__" + shorthand + "_Sort (rcd:" + shorthand + ") = rcd.Sort" + crlf)
//        append_mor crlf
//        transobj.fields
//        |> Seq.iter(fun f ->
//            let fname = f.["name"]
//            append_mor(tab + "let rcd__" + shorthand + "_" + fname + " (rcd:" + shorthand + ") = rcd.p." + fname + crlf))
//        append_mor crlf

//        append_mor(tab + "let fields__"+ptype+"(" + fields__builder_cuttail(wfields__rcdParams)(transobj.fields) + ") = {")
//        append_mor(fields__builder_cuttail(wfields__rcdAssigns)(transobj.fields) + " }" + crlf + crlf)

//        append_mor(tab + "let "+ptype+"__sps(p:"+ptype+") = [|")
//        append_mor(fields__builder_cuttail(wsqlparams)(transobj.fields) + " |]" + crlf + crlf)

//        append_type(tab + "let "+t+"_fieldorders = \"[ID],[Createdat],[Updatedat],[Sort]," + fields__builder_cuttail(wfieldorders)(transobj.fields) + "\"" + crlf + crlf)

//        append_type(tab + "let " + ptype + "_fieldordersArray = [|" + fields__builder_cuttail(w13)(transobj.fields) + " |]" + crlf + crlf)
            
//        append_type(tab + "let "+t+"_sql_update = \"[Updatedat]=@Updatedat," + fields__builder_cuttail(wa)(transobj.fields) + "\"" + crlf + crlf)

//        append_mor(tab + "let db__"+ptype+"(line:Object[]):"+ptype+" = " + crlf)
//        append_mor(tab + tab + "let p = "+ptype+"_empty()")
//        append_mor(tab + tab + tab + crlf + fields__builder(wdb__rcd)(transobj.fields) + crlf)
//        append_mor(tab + tab + "p" + crlf + crlf)

//        append_mor(tab + "let "+ptype+"_clone(p:"+ptype+"):"+ptype+" = {" + fields__builder_cuttail(w8)(transobj.fields) + " }" + crlf + crlf)
//        //append_mor(tab + "let "+ptype+"__m"+ptype+"(p:"+ptype+"):m"+ptype+" = {" + crlf + tab + tab + "muted = false;" + fields__builder_cuttail(w8)(transobj.fields) + " }" + crlf + crlf)
//        //append_mor(tab + "let m"+ptype+"__"+ptype+"(m:m"+ptype+"):"+ptype+" = {" + fields__builder_cuttail(w9)(transobj.fields) + " }" + crlf + crlf)

//        append_type(tab + "let "+ptype+"_fields = [|" + fields__builder_cuttail(wfields)(transobj.fields) + " |]" + crlf + crlf)

//        append_type(tab + "let p" + t + "_empty_CallCount = ref 0L" + crlf + crlf)

//        append_type(tab + "let p"+t+"_empty():p"+t+" = " + crlf + crlf + tab + tab + "System.Threading.Interlocked.Increment(p" + t + "_empty_CallCount) |> ignore" + crlf + crlf)
//        append_type(tab + tab + "{" + fields__builder_cuttail(wb)(transobj.fields) + " }" + crlf + crlf)
//        append_type(tab + crlf + crlf)

//        // append_type(tab + "let p" + t + "_empty():p" + t + " = {" + fields__builder_cuttail(wb)(transobj.fields) + " }" + crlf + crlf)

//        append_type(tab + "let " + t+"_id = ref " + transobj.idstarting.ToString() + "L" + crlf)
//        append_type(tab + "let " + t+"_count = ref 0" + crlf)
//        append_type(tab + "let " + t+"_table = \"" + transobj.name + "\"" + crlf + crlf)

//        append_type(tab + "type "+t+" = Rcd<p"+t+">" + crlf + crlf)

//        append_mor(tab + "let db__"+t+"(line):"+t+" = db__Rcd(db__p"+t+")(line)" + crlf + crlf)

//        append_mor(tab + "let bin__"+t+"(bi:BinIndexed):"+t+" = " + crlf)
            
//        append_mor(tab + tab + "let bin,index = bi" + crlf)
//        append_mor(tab + tab + "let ID = BitConverter.ToInt64(bin,index.Value)" + crlf)
//        append_mor(tab + tab + "index.Value <- index.Value + 8" + crlf)
//        append_mor(tab + tab + "let Sort = BitConverter.ToInt64(bin,index.Value)" + crlf)
//        append_mor(tab + tab + "index.Value <- index.Value + 8" + crlf)
//        append_mor(tab + tab + "let Createdat = BitConverter.ToInt64(bin,index.Value) |> DateTime.FromBinary" + crlf)
//        append_mor(tab + tab + "index.Value <- index.Value + 8" + crlf)
//        append_mor(tab + tab + "let Updatedat = BitConverter.ToInt64(bin,index.Value) |> DateTime.FromBinary" + crlf)
//        append_mor(tab + tab + "index.Value <- index.Value + 8" + crlf)
            
//        append_mor(tab + tab + "let p = p"+t+"_empty()" + crlf)

//        transobj.fields
//        |> Array.map(fun f ->
//            let mutable s = ""
//            let length,method = 
//                match f.["enum"] with
//                | "Caption"
//                | "Chars"
//                | "Link"
//                | "Text"
//                | "RichText"
//                | "Pwd" ->
//                    s <- s + tab + tab + "let count_" + f.["name"] + " = BitConverter.ToInt32(bin,index.Value)" + crlf
//                    s <- s + tab + tab + "index.Value <- index.Value + 4" + crlf
//                    "count_" + f.["name"], "Encoding.UTF8.GetString(bin,index.Value,count_" + f.["name"] + ")"
//                | "Bin" ->
//                    s <- s + tab + tab + "let count_" + f.["name"] + " = BitConverter.ToInt32(bin,index.Value)" + crlf
//                    s <- s + tab + tab + "index.Value <- index.Value + 4" + crlf
//                    "count_" + f.["name"], "[|index.Value..index.Value+count_" + f.["name"] + "|] |> Array.map(fun i -> bin.[i])"
//                | "FK"
//                | "Integer" -> 
//                    if(f.ContainsKey("wrap"))then
//                        "8", "BitConverter.ToInt64(bin,index.Value) |> " + f.["wrap"]
//                    else
//                        "8", "BitConverter.ToInt64(bin,index.Value)"
//                | "Money"
//                | "Float" -> 
//                    if(f.ContainsKey("wrap"))then
//                        "8", "BitConverter.ToDouble(bin,index.Value) |> " + f.["wrap"]
//                    else
//                        "8", "BitConverter.ToDouble(bin,index.Value)"
//                | "Timestamp" -> 
//                    "8", "BitConverter.ToInt64(bin,index.Value) |> DateTime.FromBinary"
//                | "Boolean" -> 
//                    "1", "BitConverter.ToBoolean(bin,index.Value)"
//                | "SelectLines" -> 
//                    "4", "BitConverter.ToInt32(bin,index.Value) |> EnumOfValue"
//                | "TimeSeries" -> 
//                    "0", "bin__TimeSeries(bin,index)"
//                | _ -> 
//                    "0", ""
//            s <- s + tab + tab + "p." + f.["name"] + " <- " + method + crlf
//            s <- s + tab + tab + "index.Value <- index.Value + " + length + crlf
//            s)
//        |> String.Concat
//        |> append_mor
//        append_mor(tab + tab + "{ID=ID;Createdat=Createdat;Updatedat=Updatedat;Sort=Sort;p=p}" + crlf + crlf)

//        //append_mor(tab + "let "+t+"__bin(rcd:"+t+")(bb:BytesBuilder) = " + crlf)
//        append_mor(tab + "let "+t+"__bin(bb:BytesBuilder)(rcd:"+t+") = " + crlf)

//        append_mor(tab + tab + "rcd.ID |> BitConverter.GetBytes |> bb.append" + crlf)
//        append_mor(tab + tab + "rcd.Sort |> BitConverter.GetBytes |> bb.append" + crlf)
//        append_mor(tab + tab + "rcd.Createdat.Ticks |> BitConverter.GetBytes |> bb.append" + crlf)
//        append_mor(tab + tab + "rcd.Updatedat.Ticks |> BitConverter.GetBytes |> bb.append" + crlf)
//        append_mor(tab + tab + "let p = rcd.p" + crlf)

//        transobj.fields
//        |> Seq.iter(fun f ->
//            match f.["enum"] with
//            | "Caption"
//            | "Chars"
//            | "Link"
//            | "Text"
//            | "RichText"
//            | "Pwd" ->
//                append_mor(tab + tab + "let bin" + f.["name"] + " = p." + f.["name"] + " |> Encoding.UTF8.GetBytes" + crlf)
//                append_mor(tab + tab + "bin" + f.["name"] + ".Length |> BitConverter.GetBytes |> bb.append" + crlf)
//                append_mor(tab + tab + "bin" + f.["name"] + " |> bb.append" + crlf)
//            | "Bin" ->
//                append_mor(tab + tab + "p." + f.["name"] + ".Length |> BitConverter.GetBytes |> bb.append" + crlf)
//                append_mor(tab + tab + "p." + f.["name"] + " |> bb.append" + crlf)
//            | "FK"
//            | "Integer" -> 
//                if(f.ContainsKey("unwrap"))then
//                    append_mor(tab + tab + "p." + f.["name"] + " |> " + f.["unwrap"] + " |> BitConverter.GetBytes |> bb.append" + crlf)
//                else
//                    append_mor(tab + tab + "p." + f.["name"] + " |> BitConverter.GetBytes |> bb.append" + crlf)
//            | "Money"
//            | "Float" -> 
//                if(f.ContainsKey("unwrap"))then
//                    append_mor(tab + tab + "p." + f.["name"] + " |> " + f.["unwrap"] + " |> BitConverter.GetBytes |> bb.append" + crlf)
//                else
//                    append_mor(tab + tab + "p." + f.["name"] + " |> BitConverter.GetBytes |> bb.append" + crlf)
//            | "Boolean" -> 
//                append_mor(tab + tab + "p." + f.["name"] + " |> BitConverter.GetBytes |> bb.append" + crlf)
//            | "Timestamp" -> 
//                append_mor(tab + tab + "p." + f.["name"] + ".Ticks |> BitConverter.GetBytes |> bb.append" + crlf)
//            | "SelectLines" -> 
//                append_mor(tab + tab + "p." + f.["name"] + " |> EnumToValue |> BitConverter.GetBytes |> bb.append" + crlf)
//            | _ -> ())

//        append_mor(tab + tab + "()" + crlf)

//        append_mor(tab + crlf + crlf)

//        append_mor(tab + "let "+t+"_wrapper(item):"+t+" =" + crlf)
//        append_mor(tab + tab + "let (i,c,u,s),p = item" + crlf)
//        append_mor(tab + tab + "{ID=i;Createdat=c;Updatedat=u;Sort=s;p=p}" + crlf + crlf)

//        let p__mp = "p"+t+"__mp"+t
//        let mp__p = "mp"+t+"__p"+t
//        append_mor(tab + "let "+t+"_update_transaction output (updater,suc,fail)(rcd:"+t+") =" + crlf)
//        append_mor(tab + tab + "let rollback_p = rcd.p |> p"+t+"_clone" + crlf)
//        append_mor(tab + tab + "let rollback_updatedat = rcd.Updatedat" + crlf)
//        append_mor(tab + tab + "updater(rcd.p)" + crlf)
//        append_mor(tab + tab + "let ctime,res =" + crlf)
//        append_mor(tab + tab + tab + "(rcd.ID,rcd.p,rollback_p,rollback_updatedat)" + crlf)
//        append_mor(tab + tab + tab + "|> update(conn,output,"+t+"_table,"+t+"_sql_update,p"+t+"__sps,suc,fail)" + crlf)
//        append_mor(tab + tab + "match(res) with" + crlf)
//        append_mor(tab + tab + "| Suc(ctx) ->" + crlf)
//        append_mor(tab + tab + tab + "rcd.Updatedat <- ctime" + crlf)
//        append_mor(tab + tab + tab + "suc(ctime,ctx)" + crlf)
//        append_mor(tab + tab + "| Fail(eso,ctx) ->" + crlf)
//        append_mor(tab + tab + tab + "rcd.p <- rollback_p" + crlf)
//        append_mor(tab + tab + tab + "rcd.Updatedat <- rollback_updatedat" + crlf)
//        append_mor(tab + tab + tab + "fail(eso)" + crlf + crlf)

//        append_mor(tab + "let "+t+"_update output (rcd:"+t+") =" + crlf)
//        append_mor(tab + tab + "rcd" + crlf)
//        append_mor(tab + tab + "|> " + t + "_update_transaction output (" + crlf)
//        append_mor(tab + tab + tab + "(fun p -> ())," + crlf)
//        append_mor(tab + tab + tab + "(fun (ctime,ctx) -> ())," + crlf)
//        append_mor(tab + tab + tab + "(fun dte -> ()))" + crlf + crlf)

//        append_mor(tab + "let "+t+"_create_incremental_transaction output (suc,fail) p =" + crlf)
//        append_mor(tab + tab + "let cid = System.Threading.Interlocked.Increment("+t+"_id)" + crlf)
//        append_mor(tab + tab + "let ctime = DateTime.UtcNow" + crlf)
//        append_mor(tab + tab + "match create(conn,output,"+t+"_table,p"+t+"__sps)(cid,ctime,p) with" + crlf)
//        append_mor(tab + tab + "| Suc(ctx) -> suc(((cid,ctime,ctime,cid),p)|>"+t+"_wrapper)" + crlf)
//        append_mor(tab + tab + "| Fail(eso,ctx) -> fail(eso,ctx)" + crlf + crlf)

//        append_mor(tab + "let " + t + "_create output p =" + crlf)
//        append_mor(tab + tab + "let suc(rcd) = ()" + crlf)
//        append_mor(tab + tab + "let fail(eso,ctx) = ()" + crlf)
//        append_mor(tab + tab + t + "_create_incremental_transaction output (suc,fail) p" + crlf + crlf)

//        append_mor(tab + "let id__"+t+"o(id):"+t+" option = " + "id__rcd(conn,"+t+"_fieldorders,"+t+"_table,db__"+t+")(id)" + crlf + crlf)

//        append_mor(tab + "let "+t+"_metadata =" + crlf)
//        append_mor(tab + tab + "{" + crlf)
//        append_mor(tab + tab + tab + "fieldorders = "+t+"_fieldorders;" + crlf)
//        append_mor(tab + tab + tab + "db__rcd = db__"+t+";" + crlf)
//        append_mor(tab + tab + tab + "wrapper = "+t+"_wrapper;" + crlf)
//        append_mor(tab + tab + tab + "sps = p"+t+"__sps;" + crlf)
//        append_mor(tab + tab + tab + "id = "+t+"_id;" + crlf)
//        append_mor(tab + tab + tab + "id__rcdo = id__" + t + "o;" + crlf)
//        append_mor(tab + tab + tab + "clone = p" + t + "_clone;" + crlf)
//        append_mor(tab + tab + tab + "empty__p = p"+ t + "_empty;" + crlf)
//        append_mor(tab + tab + tab + "rcd__bin = "+ t + "__bin;" + crlf)
//        append_mor(tab + tab + tab + "bin__rcd = bin__"+ t + ";" + crlf)
//        append_mor(tab + tab + tab + "sql_update = "+t+"_sql_update;" + crlf)
//        append_mor(tab + tab + tab + "rcd_update = " + t + "_update;" + crlf)
//        append_mor(tab + tab + tab + "table = "+t+"_table;" + crlf)
//        append_mor(tab + tab + tab + "shorthand = \"" + transobj.shorthand + "\" }" + crlf + crlf)

//        append_mor(tab + "let "+t+"__jsonRaw(sb:StringBuilder)(rcd:" + t + ") =" + crlf)
//        append_mor(tab + tab + "let p = rcd.p" + crlf)
//        append_mor(tab + tab + "sb.Append(\"\\\"id\\\":\" + rcd.ID.ToString()) |> ignore" + crlf)
//        append_mor(tab + tab + "sb.Append(\",\\\"sort\\\":\" + rcd.Sort.ToString()) |> ignore" + crlf)
//        append_mor(tab + tab + "sb.Append(\",\\\"createdat\\\":\" + (rcd.Createdat |> Util.Time.wintime__unixtime).ToString()) |> ignore" + crlf)
//        append_mor(tab + tab + "sb.Append(\",\\\"updatedat\\\":\" + (rcd.Updatedat |> Util.Time.wintime__unixtime).ToString()) |> ignore" + crlf)
//        transobj.fields
//        |> Seq.iter(fun field -> 
                
//            let prefix = 
//                if field.ContainsKey "exclude" then
//                    match field.["exclude"] with
//                    | "JSON" -> "//"
//                    | _ -> ""
//                else
//                    ""

//            match field.["enum"] with
//            | "Boolean" ->
//                tab + tab + prefix + "sb.Append(\",\\\"" + field.["name"] + "\\\":\" + p." + field.["name"] + ".ToString().ToLower()) |> ignore" + crlf
//            | "FK" ->
//                tab + tab + prefix + "sb.Append(\",\\\"" + field.["name"] + "\\\":\" + p." + field.["name"] + ".ToString()) |> ignore" + crlf
//            | "Timestamp" ->
//                tab + tab + prefix + "sb.Append(\",\\\"" + field.["name"] + "\\\":\" + (p." + field.["name"] + " |> Util.Time.wintime__unixtime).ToString()) |> ignore" + crlf
//            | "Float"
//            | "Integer" ->
//                tab + tab + prefix + "sb.Append(\",\\\"" + field.["name"] + "\\\":\" + p." + field.["name"] + ".ToString()) |> ignore" + crlf
//            | "Caption"
//            | "Chars"
//            | "Link"
//            | "Text" ->
//                tab + tab + prefix + "sb.Append(\",\\\"" + field.["name"] + "\\\":\\\"\" + p." + field.["name"] + " + \"\\\"\") |> ignore" + crlf
//            | "SelectLines" ->
//                [|  tab + tab + prefix + "sb.Append(\",\\\"" + field.["name"] + "\\\":\" + (p." + field.["name"] + " |> EnumToValue).ToString()) |> ignore" + crlf;
//                    tab + tab + prefix + "sb.Append(\",\\\"" + field.["name"] + "Caption\\\":\\\"\" + " + transobj.shorthand + "" + field.["name"] + "Enum__caption(p." + field.["name"] + ") + \"\\\"\") |> ignore" + crlf |]
//                |> linesConcat
//            | _ -> ""
//            |> append_mor)
//        append_mor(crlf)

//        append_mor(tab + "let "+t+"__json(sb:StringBuilder)(rcd:" + t + ") =" + crlf)
//        append_mor(tab + tab + "sb.Append(\"{\") |> ignore" + crlf)
//        append_mor(tab + tab + t + "__jsonRaw(sb)(rcd)" + crlf)
//        append_mor(tab + tab + "sb.Append(\"}\") |> ignore" + crlf)

//        append_mor(crlf + crlf)

//        ()


//    let transobjs = 
//        transobjects.Values
//        |> Seq.toArray
//        |> Array.map snd

//    append_mor(tab+"let mutable conn = \"\"" + crlf + crlf)

//    transobjs
//    |> Array.iter(fun transobj ->
//        try
//            coding transobj
//        with
//        | ex -> 
//            output transobj.name
//            output (ex.ToString())
//            System.Console.ReadLine() |> ignore)


//    transobjects
//    |> Seq.iter(fun kvp ->

//        let a,transobj = kvp.Key, snd(kvp.Value)

//        let ptype = "p"+transobj.shorthand.ToUpper()
//        let t = transobj.shorthand.ToUpper()

//        append_mor(tab + "// ["+transobj.name+"]" + crlf)

//        transobj.fkouts
//        |> Seq.iter(fun pk ->
//            let field,ref = pk
//            append_mor(tab + "let " + t + "__" + field + "(rcd:"+transobj.shorthand.ToUpper()+") = id__rcd(conn," + t+"_fieldorders,"+t+"_table,db__"+ptype+")(rcd.p."+ field + ")" + crlf))

//        transobj.fkins
//        |> Seq.iter(fun pk ->
//            let ref,field = pk
//            append_mor(tab + "let "+transobj.shorthand.ToUpper() + "__" + ref.shorthand+ field+"(rcd:"+transobj.shorthand.ToUpper()+")" + " = refin(conn," + ref.shorthand.ToUpper()+"_fieldorders,"+ref.shorthand.ToUpper()+"_table,db__p"+ref.shorthand.ToUpper()+",\""+field+"\")(rcd.ID)" + crlf))

//        append_mor(crlf + tab + "let "+t+"__dict(rcd:"+t+") = " + crlf)
//        append_mor(tab + tab + "let dict = new Dictionary<string,string>()" + crlf)
//        append_mor(tab + tab + "dict.Add(\"ID\", rcd.ID.ToString())" + crlf)
//        append_mor(tab + tab + "dict.Add(\"Createdat\", rcd.Createdat.Ticks.ToString())" + crlf)
//        append_mor(tab + tab + "dict.Add(\"Updatedat\", rcd.Updatedat.Ticks.ToString())" + crlf)
//        append_mor(tab + tab + "dict.Add(\"Sort\", rcd.ID.ToString())" + crlf)
//        append_mor(fields__builder(s2)(transobj.fields))
//        append_mor(tab + tab + "dict" + crlf + crlf)

//        ())

//    let tos = 
//        transobjects 
//        |> Seq.toArray 
//        |> Array.sortBy(fun i -> snd(i.Value).shorthand.ToUpper())

//    append_mor(tab + "type MetadataEnum =" + crlf)
//    [| 0..tos.Length - 1 |]
//    |> Array.map(fun i ->
//        let kvp = tos.[i]
//        let a,transobj = kvp.Key, snd(kvp.Value)
//        let t = transobj.shorthand.ToUpper()
//        tab + "| " + t + " = " + i.ToString() + crlf)
//    |> String.Concat
//    |> append_mor
//    append_mor(crlf + crlf)

//    append_mor(tab + "let tablenames = [|" + crlf)
//    tos
//    |> Array.map(fun kvp ->
//        let a,transobj = kvp.Key, snd(kvp.Value)
//        let t = transobj.shorthand.ToUpper()
//        tab + tab + t + "_metadata.table;" + crlf)
//    |> String.Concat
//    |> append_mor
//    append_mor(tab + tab + "|]" + crlf + crlf)

//    append_mor(tab + "let shorthands = [|" + crlf)
//    tos
//    |> Array.map(fun kvp ->
//        let a,transobj = kvp.Key, snd(kvp.Value)
//        tab + tab + "\"" + transobj.shorthand + "\"" + crlf)
//    |> String.Concat
//    |> append_mor
//    append_mor(tab + tab + "|]" + crlf + crlf)
        
//    tos
//    |> Array.map(fun kvp ->
//        let a,transobj = kvp.Key, snd(kvp.Value)
//        let t = transobj.shorthand.ToUpper()
//        let code = sprintf "str__metadata.Add( %s,(%s,%s))" (t + "_metadata.table") (t + "_metadata.shorthand") (t + "_metadata.fieldorders")
//        tab + code + crlf
//    )
//    |> String.Concat
//    |> append_mor

//    append_mor(tab + "let dbObjs__rcds(tablename)(top,where) =" + crlf)
//    append_mor(tab + tab + "match tablename with" + crlf)
//    tos
//    |> Array.map(fun kvp ->
//        let a,transobj = kvp.Key, snd(kvp.Value)
//        let t = transobj.shorthand.ToUpper()
//        tab + tab + "| \"" + transobj.name + "\" -> rcds__bin(conn)(top,where)(" + t + "_metadata)" + crlf)
//    |> String.Concat
//    |> append_mor
//    append_mor(tab + tab + "| _ -> None" + crlf + crlf)

//    append_mor(tab + "let rcds__dbObjs(tablename)(bin:byte[]) =" + crlf)
//    append_mor(tab + tab + "match tablename with" + crlf)
//    tos
//    |> Array.map(fun kvp ->
//        let a,transobj = kvp.Key, snd(kvp.Value)
//        let t = transobj.shorthand.ToUpper()
//        tab + tab + "| \"" + transobj.name + "\" -> bin__rcds(" + t + "_metadata)(bin) |> Array.map(fun rcd -> (rcd.ID,rcd.Createdat,rcd.Updatedat,rcd.ID,rcd.p) |> build_create_sql(" + t + "_metadata))" + crlf)
//    |> String.Concat
//    |> append_mor
//    append_mor(tab + tab + "| _ -> [| |]" + crlf + crlf)

//    append_mor(tab + "let init() = " + crlf + crlf + crlf)
//    append_mor(tab + tab + "use cw = new CodeWrapper(\"SrOrm.MetadataMor.init\")" + crlf + crlf + crlf)
//    tos
//    |> Array.iter(fun kvp ->

//        let a,transobj = kvp.Key, snd(kvp.Value)

//        let idname = transobj.shorthand.ToUpper() + "_id.Value"
//        let countname = transobj.shorthand.ToUpper() + "_count.Value"

//        append_mor(tab + tab + "match singlevalue_query(conn)(str__sql(\"SELECT MAX(ID) FROM ["+ transobj.name + "]\")) with" + crlf)
//        append_mor(tab + tab + "| Some(v) ->" + crlf)
//        append_mor(tab + tab + tab + "let max = v :?> int64" + crlf)
//        append_mor(tab + tab + tab + "if(max > " + idname + ") then" + crlf)
//        append_mor(tab + tab + tab + tab + idname + " <- max" + crlf)
//        append_mor(tab + tab + "| None -> ()" + crlf + crlf)

//        append_mor(tab + tab + "match singlevalue_query(conn)(str__sql(\"SELECT COUNT(ID) FROM ["+ transobj.name + "]\")) with" + crlf)
//        append_mor(tab + tab + "| Some(v) ->" + countname + " <- v :?> int32" + crlf)
//        append_mor(tab + tab + "| None -> ()" + crlf + crlf)

//        ())

//    sb_type.ToString() |> Util.FileSys.try_write_text(typefile) |> ignore
//    sb_mor.ToString() |> Util.FileSys.try_write_text(morfile) |> ignore

//    let sqls = new ResizeArray<Sql>()

//    let sqlIndexSb = new StringBuilder()

//    if(rdbms = Rdbms.SqlServer) then
//        let mutable dbName = conn.ToUpper() |> regex_match(string__regex("(?<=DATABASE=)[^;]+"))
//        if dbName.Length = 0 then
//            dbName <- conn.ToUpper() |> regex_match(string__regex("(?<=CATALOG=)[^;]+"))
                    
//        match tx conn output [|{ text = "USE [" + dbName + "]"; ps = [||] }|] with
//        | Cat.Suc(x) ->
//            transobjects.Values |> Seq.iter(fun item ->
//                let table, transobj = item
//                match ("SELECT [name] FROM SYSCOLUMNS WHERE id=object_id('"+transobj.name+"')")
//                    |> str__sql
//                    |> multiline_query conn with
//                | Suc(x) ->
//                    let cols = x.lines |> Seq.map(fun item -> string(item.[0]))

//                    let sqlcreatetable =
//                        let sb = new StringBuilder()
//                        sb.Append("IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + table + "' AND xtype='U')" + Util.Text.crlf) |> ignore
//                        sb.Append("BEGIN" + Util.Text.crlf) |> ignore
//                        sb.Append("    CREATE TABLE " + table + " ([ID] BIGINT NOT NULL") |> ignore
//                        sb.Append(",[Createdat] BIGINT NOT NULL") |> ignore
//                        sb.Append(",[Updatedat] BIGINT NOT NULL") |> ignore
//                        sb.Append(",[Sort] BIGINT NOT NULL,") |> ignore

//                        transobj.fields
//                        |> Seq.map(fun f -> f |> db1)
//                        |> Seq.toArray
//                        |> String.concat(",")
//                        |> sb.Append
//                        |> ignore

//                        sb.Append(", CONSTRAINT [PK_" + table + "] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]" + crlf) |> ignore
//                        sb.Append("END" + crlf + crlf) |> ignore

//                        sb.Append("IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='PK_" + table + "')" + crlf) |> ignore
//                        sb.Append("BEGIN" + crlf) |> ignore
//                        sb.Append(" ALTER TABLE [" + table + "] DROP CONSTRAINT [PK_" + table + "] WITH ( ONLINE = OFF )" + crlf) |> ignore
//                        sb.Append("END" + crlf + crlf) |> ignore

//                        sb.Append("IF NOT EXISTS(SELECT * FROM SYSINDEXES WHERE name='PK_" + table + "_ID')" + crlf) |> ignore
//                        sb.Append("BEGIN" + crlf) |> ignore
//                        sb.Append(" CREATE CLUSTERED INDEX [PK_" + table + "_ID] ON [" + table + "](ID)" + crlf) |> ignore
//                        sb.Append("END" + crlf) |> ignore

//                        { text = sb.ToString(); ps = [||]}

//                    match tx conn output [|sqlcreatetable|] with
//                    | Suc x -> output("Create ["+transobj.name+"] OK")
//                    | Fail(exn,so) ->
//                        output("Failed: "+sqlcreatetable.text+": "+exn.ToString())

//                    cols
//                    |> Seq.iter(fun col ->
//                        let find = transobj.fields |> Seq.tryFind(fun f -> f.["name"] = col)
//                        if(find.IsNone && col <> "ID" && col <> "Createdat" && col <> "Updatedat" && col <> "Sort") then
//                            output("Droping ["+transobj.name+"].["+col+"], CONFIRM...")
//                            //System.Console.ReadLine() |> ignore
//                            match tx conn output [|str__sql("ALTER TABLE "+transobj.name+" DROP COLUMN ["+col+"]")|] with
//                            | Suc x -> output("OK")
//                            | Fail(exn,so) ->
//                                output("Failed: "+exn.ToString()))

//                    transobj.fields
//                    |> Seq.iter(fun f ->
//                        let fname = f.["name"]
//                        let find = cols |> Seq.tryFind(fun c -> c = fname)
//                        if(find.IsNone) then
//                            output("Adding ["+transobj.name+"].["+fname+"], CONFIRM...")
//                            //System.Console.ReadLine() |> ignore
//                            match tx conn output [|str__sql(" ALTER TABLE "+transobj.name+" ADD "+db1(f))|] with
//                            | Suc x -> output("OK")
//                            | Fail(exn,so) ->
//                                output("Failed: "+exn.ToString()))

//                | _ -> ())
//        | Cat.Fail(b,a) -> 
//            if(b.exno.IsSome) then
//                output(b.exno.Value.ToString())
//                output("CONN: " + conn)

//        transobjects.Values |> Seq.iter(fun item ->
//            let table, transobj = item
//            let sb = new StringBuilder()
//            sb.Append("IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + table + "' AND xtype='U')" + Util.Text.crlf) |> ignore
//            sb.Append("BEGIN" + Util.Text.crlf) |> ignore
//            sb.Append("    CREATE TABLE " + table + " ([ID] BIGINT NOT NULL") |> ignore
//            sb.Append(",[Createdat] BIGINT NOT NULL") |> ignore
//            sb.Append(",[Updatedat] BIGINT NOT NULL") |> ignore
//            sb.Append(",[Sort] BIGINT NOT NULL,") |> ignore

//            transobj.fields
//            |> Seq.map(fun f -> f |> db1)
//            |> Seq.toArray
//            |> String.concat(",")
//            |> sb.Append
//            |> ignore

//            sb.Append(", CONSTRAINT [PK_" + table + "] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]" + crlf) |> ignore
//            sb.Append("END" + Util.Text.crlf) |> ignore
//            sqls.Add({ text = sb.ToString(); ps = [||]})

//            transobj.fields
//            |> Seq.iter(fun f ->

//                let fname = f.["name"]
//                let fullname = transobj.name+"_"+fname

//                sb.Append("--["+transobj.name+"].["+fname+"]--------------------" + crlf) |> ignore

//                sb.Append("IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('"+transobj.name+"') AND name='"+fname+"')" + crlf) |> ignore
//                sb.Append("BEGIN" + crlf) |> ignore
//                sb.Append(" ALTER TABLE "+transobj.name+" ALTER COLUMN ["+fname+"] NCHAR(64) COLLATE Chinese_PRC_CI_AS " + crlf) |> ignore
//                sb.Append("END" + crlf) |> ignore
//                sb.Append("ELSE" + crlf) |> ignore
//                sb.Append("BEGIN" + crlf) |> ignore
//                sb.Append(" ALTER TABLE "+transobj.name+" ADD ["+fname+"] NCHAR(64) COLLATE Chinese_PRC_CI_AS " + crlf) |> ignore
//                sb.Append("END" + crlf) |> ignore

//                //sqlIndexSb.Append("UPDATE "+transobj.name+" SET ["+fname+"]='-' WHERE (["+fname+"] IS NULL)" + crlf) |> ignore
//                //sqlIndexSb.Append("IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_SrOrm_"+fullname+"')" + crlf) |> ignore
//                //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
//                //sqlIndexSb.Append(" ALTER TABLE Ca_Staff DROP  CONSTRAINT [Constraint_SrOrm_"+fullname+"]" + crlf) |> ignore
//                //sqlIndexSb.Append("END" + crlf) |> ignore

//                //sqlIndexSb.Append("IF NOT EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_"+fullname+"')" + crlf) |> ignore
//                //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
//                //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" ADD  CONSTRAINT [Constraint_"+fullname+"] DEFAULT('-') FOR ["+fname+"]" + crlf) |> ignore
//                //sqlIndexSb.Append("END" + crlf) |> ignore
//                //sqlIndexSb.Append("" + crlf) |> ignore
//                //sqlIndexSb.Append("IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_"+fullname+"')" + crlf) |> ignore
//                //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
//                //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" DROP  CONSTRAINT [UniqueNonclustered_"+fullname+"]" + crlf) |> ignore
//                //sqlIndexSb.Append("END" + crlf) |> ignore

//                //let s = " ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf
//                //sb.Append(" ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf) |> ignore

//                ()))

//    project,transobjects,sqls,sqlIndexSb.ToString()

let updateDatabase conn output sqls =

    match tx conn output sqls with
    | Suc x -> output("OK: update database")
    | Fail(dte, x) ->
        if(dte.sqlo.IsSome) then
            output(dte.exno.Value.ToString() + " >> " + dte.sqlo.Value.text)
        else
            output(dte.exno.Value.ToString())

//let bootstraping output (conn,rdbms,working) =

//    let working =
//        if(working <> "") then
//            working
//        else
//            let f = 
//                Environment.CurrentDirectory
//                |> Util.FileSys.dir_parent
//                |> Util.FileSys.dir_parent
//                |> Util.FileSys.dir_parent
//                |> Util.FileSys.dir_parent
//            f + @"/SrOrm"

//    //let files = System.IO.Directory.GetFiles(working)

//    let inner = 
//        System.IO.Directory.GetFiles(working)
//        |> Array.filter(fun f -> (f.StartsWith(working + "\\Design-")||f.StartsWith(working + "/Design-")) && f.EndsWith(".json"))
//        |> Array.map(fun f ->
//            let mutable text = Util.FileSys.try_read_string(f) |> snd
//            text <- text.Trim()
//            if(text.StartsWith("[") && text.EndsWith("]")) then
//                text <- text.Substring(1,text.Length-2)
//            text)
//        |> String.concat(",")

//    let error,content = Util.FileSys.try_read_string(working + "/Design.json")

//    (content.Replace("\"transobjs\": []","\"transobjs\": ["+inner+"]"),
//        working + "/MetadataType.fs",
//        working + "/MetadataMor.fs",
//        conn,
//        rdbms)
//    |> build output

//    ()

let sordedaccessor_builder
    (conn,output)
    (metadata,where,keying:('p->string))
    (ps) = 

    match 
        where
        |> Util.Orm.loadall
            (conn)
            (metadata.table,metadata.fieldorders,metadata.db__rcd) with
    | None -> 
        halt output "sordedaccessor_builder/A" ""
        new SortedAccessor<string,Rcd<'p>>(0)
    | Some(v) ->
        let ka = create_sortedaccessor(fun () -> 
            v 
            |> Array.map(fun item -> 
                let id = item.ID
                let k = item.p |> keying
                id,k,item))(8)

        ps
        |> Array.iter(fun p ->
            let key = keying(p)
            if(ka.trygetkey(key).IsNone)then
                let cid,ctime,res = create_incremental(conn,output,metadata.table,metadata.sps)(metadata.id,p)
                match res with
                | Suc(x) -> 
                    ((cid,ctime,ctime,cid),p)
                    |> metadata.wrapper
                    |> ka.append(cid,key)
                | Fail(a,b) -> 
                    halt(output)("sordedaccessor_builder/B")(a.sqlo.Value.text + " -> " + a.exno.Value.ToString()))

        ka

let integrity_check(conn,output)(table,field,ids,coding:int64->string,handlero:(int64[] -> unit)option) = 

    let mutable stop = false

    ids
    |> Seq.iter(fun id ->
        let code = id |> coding
        match 
            ("SELECT COUNT(ID) FROM "+table+" WHERE "+field+"=N'"+code+"'")
            |> str__sql
            |> singlevalue_query(conn) with
        | Some(c) -> 
            let count = (c :?> int32)
            if(count <> 1)then
                stop <- true
                output("L-95: ["+table+"] Integrity Check Failed, ["+field+"]='"+code+"', Count="+count.ToString())
                match
                    ("SELECT ID FROM "+table+" WHERE "+field+"='"+code+"'")
                    |> str__sql
                    |> multiline_query(conn) with
                | Suc(ctx) -> 
                    let ids = 
                        ctx.lines
                        |> Seq.map(fun line -> (line.[0] :?> int64))
                        |> Seq.toArray

                    if(handlero.IsSome)then
                        handlero.Value(ids)

                    ids
                    |> Array.map(fun id -> id.ToString())
                    |> Array.iter output
                | Fail(a,b) -> ()
        | None -> halt output "integrity_check/A" table)

    if(stop) then
        halt output "integrity_check/B" table

// ====================== VUE =================================

type SrOrmProject = 
    { 
        obs: SortedDictionary<string,TransObj>; }

let empty__SrOrmProject() = 
    {   obs = new SortedDictionary<string,TransObj>(); }

let transobj__vue project (ob:TransObj) = 
        
    let sb = new StringBuilder()

    let shorthanding(item:Dictionary<string,string>) = 
        if(item.ContainsKey("ref")) then
            item.["ref"]
        else
            item.["name"]

    let dsts = 
        ob.fields
        |> Array.filter(fun item -> item.["enum"] = "FK")
        |> Array.map shorthanding
        |> Array.distinct

    sb.Append(crlf + "<template>") |> ignore
    sb.Append(crlf + tab + "<div class='panel'>") |> ignore
    sb.Append(crlf + tab + tab + "<crudPanel") |> ignore
    sb.Append(crlf + tab + tab + tab + "apiList='/api//'") |> ignore
    sb.Append(crlf + tab + tab + tab + "apiUpdate='/api//'") |> ignore
    sb.Append(crlf + tab + tab + tab + "v-bind:fieldKeys='this.fieldKeys'") |> ignore
    sb.Append(crlf + tab + tab + tab + "v-bind:fieldCaptions='this.fieldCaptions'") |> ignore
    sb.Append(crlf + tab + tab + tab + "v-bind:buttons='this.buttons'") |> ignore
    sb.Append(crlf + tab + tab + tab + "v-bind:buttonClicks='this.buttonClicks'") |> ignore
    sb.Append(crlf + tab + tab + tab + ":populate='populate'") |> ignore
    sb.Append(crlf + tab + tab + tab + ":selected='selected'") |> ignore
    sb.Append(crlf + tab + tab + tab + ":commit='commit'") |> ignore
    sb.Append(crlf + tab + tab + tab + ":clear='clear'>") |> ignore
    sb.Append(crlf + tab + tab + tab + tab + "<template #caption>编辑区</template>") |> ignore
    sb.Append(crlf + tab + tab + tab + tab + "<template #detail>") |> ignore
    sb.Append(crlf + tab + tab + tab + tab + tab + "<table>") |> ignore

    ob.fields
    |> Array.iter(fun item -> 
        let name = item.["name"]
        sb.Append(crlf + tab + tab + tab + tab + tab + tab + "<tr>") |> ignore
        sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + "<td>" + name + "</td>") |> ignore
        sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + "<td>") |> ignore

        let enum = item.["enum"]
        let txt = 
            match enum with
            | "FK" -> 
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + "<select v-model='" + name + "'>") |> ignore
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + tab + "<option") |> ignore
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + tab + tab + "v-for='(item,index) in this." + shorthanding(item) + "s'") |> ignore
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + tab + tab + ":key='index'") |> ignore
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + tab + tab + ":text='item.Name'") |> ignore
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + tab + tab + ":value='item.id'></option>") |> ignore
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + "</select>") |> ignore
            | "SelectLines" -> 
                let mutable index = 0
                item.["lines"]
                |> regex_matches(string__regex("[^/]+//[^//]+"))
                |> Array.iter(fun line -> 
                    let a = findTo "//" line
                    let b = findFrom "//" line
                    sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + "<input type=\"radio\" value=\"" + index.ToString() + "\" v-model=\"" + name + "\" /><label for=\"" + index.ToString() + "\">" + b + "</label>") |> ignore
                    index <- index + 1)
            | _ -> 
                sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + tab + "<input v-model='" + name + "' />") |> ignore
        sb.Append(crlf + tab + tab + tab + tab + tab + tab + tab + "</td>") |> ignore
        sb.Append(crlf + tab + tab + tab + tab + tab + tab + "</tr>") |> ignore)

    sb.Append(crlf + tab + tab + tab + tab + tab + "</table>") |> ignore
    sb.Append(crlf + tab + tab + tab + tab + "</template>") |> ignore
    sb.Append(crlf + tab + tab + "</crudPanel>") |> ignore
    sb.Append(crlf + tab + "</div>") |> ignore
    sb.Append(crlf + "</template>") |> ignore
    sb.Append(crlf) |> ignore
    sb.Append(crlf + "<script>") |> ignore
    sb.Append(crlf) |> ignore
    sb.Append(crlf + "import axios from 'axios'") |> ignore
    sb.Append(crlf + "import crudPanel from '../components/CrudPanel.vue'") |> ignore
    sb.Append(crlf) |> ignore
    sb.Append(crlf + "export default {") |> ignore
    sb.Append(crlf + tab + "name: 'ComponentName',") |> ignore
    sb.Append(crlf + tab + "components: {") |> ignore
    sb.Append(crlf + tab + tab + "crudPanel,") |> ignore
    sb.Append(crlf + tab + "},") |> ignore
    sb.Append(crlf + tab + "data(){") |> ignore
    sb.Append(crlf + tab + tab + "return {") |> ignore
    sb.Append(crlf + tab + tab + tab + "id: 0,") |> ignore

    dsts
    |> Array.iter(fun dst ->
        sb.Append(crlf + tab + tab + tab + dst + "s: [],") |> ignore)

    ob.fields
    |> Array.iter(fun item -> 
        let name = item.["name"]
        let enum = item.["enum"]
        let defaultVal = 
            match enum with
            | "Caption"
            | "Text" -> "''"
            | "Float"
            | "Integer" -> "0"
            | _ -> "null"
        sb.Append(crlf + tab + tab + tab + name + ": " + defaultVal + ",") |> ignore)

    let keys = 
        ob.fields 
        |> Array.map(fun item -> 
            "'" + item.["name"] + "'") 
        |> String.concat(",")

    sb.Append(crlf + tab + tab + tab + "fieldKeys: [" + keys + "],") |> ignore
    sb.Append(crlf + tab + tab + tab + "fieldCaptions: [('<div style=\"display: flex;\">'") |> ignore

    ob.fields
    |> Array.iter(fun item -> 
        let name = item.["name"]
        let enum = item.["enum"]
        let defaultVal = 
            match enum with
            | _ -> "null"
        sb.Append(crlf + tab + tab + tab + tab + "+ '<div style=\"width: 60px;\">" + name + "</div>',") |> ignore)

    sb.Append(crlf + tab + tab + tab + tab + "+ '</div>')],") |> ignore
    sb.Append(crlf + tab + tab + tab + "buttons: [],") |> ignore
    sb.Append(crlf + tab + tab + "}") |> ignore
    sb.Append(crlf + tab + "},") |> ignore
    sb.Append(crlf + tab + "mounted(){") |> ignore

    dsts
    |> Array.iter(fun dst ->
        sb.Append(crlf + tab + tab + "axios.post('/api//', { session: window.enduser.session }).then(res => { this." + dst + "s = res.data.list })") |> ignore)

    sb.Append(crlf + tab + "},") |> ignore
    sb.Append(crlf + tab + "methods: {") |> ignore
    sb.Append(crlf + tab + tab + "populate(item,field){") |> ignore
    sb.Append(crlf + tab + tab + tab + "//let html = []") |> ignore
    sb.Append(crlf + tab + tab + tab + "//html.push('<div style=\"display: flex;\">')") |> ignore
    sb.Append(crlf + tab + tab + tab + "return item[field]") |> ignore

    //ob.fields
    //|> Array.iter(fun item -> 
    //    let name = item.["name"]
    //    let enum = item.["enum"]
    //    match enum with
    //    | _ -> 
    //        sb.Append(crlf + tab + tab + tab + "html.push(item[field])") |> ignore)

    sb.Append(crlf + tab + tab + tab + "//html.push('</div>')") |> ignore
    sb.Append(crlf + tab + tab + tab + "//return html.join('')") |> ignore
    sb.Append(crlf + tab + tab + "},") |> ignore
    sb.Append(crlf + tab + tab + "selected(item){") |> ignore
    sb.Append(crlf + tab + tab + tab + "this.id = item['id']") |> ignore

    ob.fields
    |> Array.iter(fun item -> 
        let name = item.["name"]
        sb.Append(crlf + tab + tab + tab + "this." + name + " = item['" + name + "']") |> ignore)

    sb.Append(crlf + tab + tab + "},") |> ignore
    sb.Append(crlf + tab + tab + "commit(){") |> ignore
    sb.Append(crlf + tab + tab + tab + "return {") |> ignore
    sb.Append(crlf + tab + tab + tab + tab + "id: this.id,") |> ignore

    ob.fields
    |> Array.iter(fun item -> 
        let name = item.["name"]
        sb.Append(crlf + tab + tab + tab + tab + name + ": this." + name + ",") |> ignore)

    sb.Append(crlf + tab + tab + tab + "}") |> ignore
    sb.Append(crlf + tab + tab + "},") |> ignore
    sb.Append(crlf + tab + tab + "clear(){") |> ignore
    sb.Append(crlf + tab + tab + tab + "this.id = 0") |> ignore

    ob.fields
    |> Array.iter(fun item -> 
        let name = item.["name"]
        let enum = item.["enum"]
        let defaultVal = 
            match enum with
            | _ -> "null"
        sb.Append(crlf + tab + tab + tab + "this." + name + " = " + defaultVal) |> ignore)

    sb.Append(crlf + tab + tab + "},") |> ignore
    sb.Append(crlf + tab + tab + "//buttonClicks(button,item){") |> ignore
    sb.Append(crlf + tab + tab + "buttonClicks(button){") |> ignore
    sb.Append(crlf + tab + tab + tab + "switch(button){") |> ignore
    sb.Append(crlf + tab + tab + tab + tab + "case '':") |> ignore
    sb.Append(crlf + tab + tab + tab + tab + tab + "break") |> ignore
    sb.Append(crlf + tab + tab + tab + "}") |> ignore
    sb.Append(crlf + tab + tab + "}") |> ignore
    sb.Append(crlf + tab + "}") |> ignore
    sb.Append(crlf + "}") |> ignore
    sb.Append(crlf) |> ignore
    sb.Append(crlf + "</script>") |> ignore
    sb.Append(crlf) |> ignore
    sb.Append(crlf + "<style scoped>") |> ignore
    sb.Append(crlf + "</style>") |> ignore
    sb.Append(crlf) |> ignore

    sb.ToString()