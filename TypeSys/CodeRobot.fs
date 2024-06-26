module TypeSys.CodeRobot

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
open TypeSys.Common
open TypeSys.RDBMS
open TypeSys.CodeRobotI
open TypeSys.LangPackTypeScript
open TypeSys.CodeRobotIIFs
open TypeSys.CodeRobotIITs

type RobotConfig = {
ns: string
dbName: string
conn: string
mainDir: string
JsDir: string }

type Robot = {
srcs: Src[]
sql: Src
ot: Src
otTypeScript: Src
om: Src
omTypeScript: Src
cm: Src
typeTypeScript: Src
cmTypeScript: Src
config: RobotConfig
output: string -> unit }

let robot__srcs robot = 
    robot.srcs,robot.sql,robot.ot,robot.otTypeScript,robot.om,robot.omTypeScript,robot.cm,robot.typeTypeScript,robot.cmTypeScript

let create__Src (pattern:string) = 
    let buffer = new List<string>()
    
    {
        lang = 
            if pattern.EndsWith ".ts" then
                ProgrammingLang.TypeScript
            else if pattern.EndsWith ".cs" then
                ProgrammingLang.CSharp
            else if pattern.EndsWith ".fs" then
                ProgrammingLang.FSharp
            else
                ProgrammingLang.SQL
        buffer = buffer
        w = new TextBlockWriter(buffer)
        filename = pattern }

let buildTypeCat output
    (tables:SortedDictionary<string,Table>) 
    (cTypes:Dictionary<string,Type>) = 

    let tc = { types = new Dictionary<string,Type>() }

    tables.Keys
    |> Seq.iter(fun n -> 
        let table = tables[n]
        {   name = table.typeName
            sort = 0
            tEnum = Orm table
            custom =  false
            src = [||] } 
        |> appendType tc
        |> ignore)

    cTypes.Keys
    |> Seq.iter(fun n -> appendType tc cTypes[n] |> ignore)

    let cTypesSorted = 
        cTypes.Values
        |> Seq.toArray
        |> Array.sortBy(fun t -> t.sort)
    
    cTypesSorted
    |> Array.iter(fun c -> 

        let lines__items (lines:string[]) = 
            [| 1 .. lines.Length - 1 |]
            |> Array.map(fun i -> lines[i].TrimStart())
            |> Array.filter(fun line -> line.StartsWith "|")
            |> Array.map(fun line -> line.Substring(1).Trim())

        match c.tEnum with
        | TypeEnum.Structure items ->
            c.tEnum <- 
                [| 1 .. c.src.Length - 1 |]
                |> Array.map(fun i -> 
                    let line = c.src[i].Replace("{","").Replace("}","").Trim()
                    let index = line.IndexOf ":"
                    line,index)
                |> Array.filter(fun (line,index) -> index > 0)
                |> Array.map(fun (line,index) ->
                    let varName = line.Substring(0,index).Replace("mutable","").Trim()
                    let typeNname = line.Substring(index + 1).Trim()
                    varName,str__type tc typeNname)
                |> TypeEnum.Structure

        | TypeEnum.Enum items ->
            c.tEnum <- 
                c.src
                |> lines__items
                |> Array.map(fun s -> 
                    let index = s.IndexOf " = "
                    if index > 0  then
                        let name = s.Substring(0,index).Trim()
                        let v = s.Substring(index + 4).Trim()
                        name,v |> parse_int32
                    else
                        s, 0)
                |> TypeEnum.Enum
        | TypeEnum.Sum items ->
            c.tEnum <- 
                c.src
                |> lines__items
                |> Array.map(fun s -> 
                    let index = s.IndexOf " of "
                    if index > 0  then
                        let name = s.Substring(0,index).Trim()
                        let tt = s.Substring(index + 4).Trim()
                        name,str__type tc tt |> Some
                    else
                        s, None)
                |> TypeEnum.Sum
        | _ ->  c.tEnum <- TypeEnum.Product [||]
        ())

    cTypesSorted
    |> Array.iter(type__str output 0)

    tc

let load robot =

    let tables = new SortedDictionary<string,Table>()

    let dict__shorthand (dict:Dictionary<string,string>) (name:string) =
        if dict.ContainsKey "shorthand" then
            dict["shorthand"]
        else
            let cs = 
                name.ToCharArray()
                |> Array.filter(fun c ->
                    let a = string c
                    let b = a.ToUpper()
                    a = b && a <> "_")
            new string(cs)

    let jsonTables = 

        let jsontxt = 
            robot.config.mainDir
            |> Directory.GetFiles
            |> Array.filter(fun i -> i.Contains "Design-" && i.EndsWith ".json")
            |> Array.map (Util.FileSys.try_read_string >> snd)
            |> Array.map (findBidirectional ("[","]"))
            |> String.concat ","

        let json = "{\"tables\":[" + jsontxt + "]}" |> str__root

        match json |> tryFindByAtt "tables" with
        | Some (n,v) -> 
            match v with
            | Json.Ary items -> items
            | _ -> [| |]
        | None -> [| |]

    jsonTables
    |> Array.iter(fun item ->

        let dict = Util.Json.json__items item

        let name = dict.["name"]

        let t = {
            tableName = name
            fields = new Dictionary<string,Field>();
            fkins = new List<Table * string>();
            fkouts = new List<string * Table>();
            idstarting = 
                if dict.ContainsKey "id" then 
                    Util.Text.parse_int64 dict["id"]
                else 
                    0L;
            typeName = 
                let shorthand = (dict__shorthand dict name).ToUpper()
                if shorthand.Length > 0 then
                    shorthand
                else
                    let cs = 
                        let a = name.ToUpper()
                        [| 0 .. name.Length - 1|]
                        |> Array.filter(fun i -> name[i] = a[i])
                        |> Array.map(fun i -> name[i])
                    new string(cs) }

        let items = name__array item "fields"
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i ->
            items[i]
            |> Util.Json.json__items
            |> items__fieldo
            |> Util.Cat.processOption (fun (fname,fdef,items) -> 
                t.fields.Add(fname,(i,fname,fdef,items))))

        tables.Add(name,t))

    let tableArray = tables.Values |> Seq.toArray

    tableArray
    |> Array.iter(fun table -> 

        table
        |> table__fieldKeys
        |> Array.iter(fun k -> 
            let sort,fname,def,items = table.fields[k]

            match def with
            | FieldDef.Other -> 
                if checkfield items "enum" = "FK" then
                    let mutable fk = checkfield items "ref"
                    if fk.Length = 0 then
                        //fk <- (table.tableName.Substring(0,(table.tableName.IndexOf "_") + 1)) + fname
                        fk <- fname
                    if fk.Length > 0 then
                        match
                            tables.Keys
                            |> Seq.tryFind(fun tt -> 
                                let table = tables[tt]
                                table.tableName = fk 
                                || table.typeName = fk
                                || table.tableName.EndsWith("_" + fk)) with
                        | Some tt -> table.fields[fname] <- sort,fname,FK tables[tt],items
                        | None -> table.fields[fname] <- sort,fname,FK table,items
            | _ -> ())

        table
        |> table__fieldKeys
        |> Array.filter(fun k -> 
            let sort,fname,def,json = table.fields[k]
            match def with
            | FieldDef.Other -> true
            | _ -> false)
        |> Array.map table.fields.Remove
        |> ignore

        ())

    let cTypes = 
        let dict = new Dictionary<string,Type>()
        robot.config.mainDir + @"\Types.fs"
        |> Util.FileSys.filename__lines
        |> findInLines("//[TypeManaged]{","//}")
        |> Array.map(fun line -> 
            let index = line.IndexOf "//"
            if index >= 0 then
                line.Substring(0,index)
            else
                line)
        |> parseCustomTypes 
        |> Array.iter(fun i -> dict.Add(i.name,i))
        dict

    cTypes,buildTypeCat robot.output tables cTypes,tableArray

let save srcs = 
    srcs
    |> Array.iter(fun src ->
        src
        |> src__txt crlf
        |> try_write_text (src.filename)
        |> ignore)

let addMulti line = Array.iter(fun src -> src.buffer.Add line)

let fSharpHeader src m opens = 
    [|  "module " + m
        ""
        "open LanguagePrimitives"
        ""
        "open System"
        "open System.Collections.Generic"
        "open System.Text"
        ""
        "open Util.Cat"
        "open Util.Perf"
        "open Util.Measures"
        "open Util.Db"
        "open Util.DbQuery"
        "open Util.DbTx"
        "open Util.Bin"
        "open Util.Text"
        "open Util.Json"
        "open Util.Orm"
        ""
        "open PreOrm"
        "" |]
    |> src.w.multiLine

    opens |> src.w.multiLine

let buildTableEnums robot (t:Table) (name,lines:(string * string)[]) =

    let srcs,sql,ot,otTypeScript,om,omTypeScript,cm,typeTypeScript,cmTypeScript =
        robot__srcs robot

    let enumName = t.typeName.ToLower() + name + "Enum"

    "type " + enumName + " = " |> ot.w.newline

    [| 0 .. lines.Length - 1 |]
    |> Array.iter(fun i -> 
        let a,b = lines[i]
        "| " + a + " = " + i.ToString() + " // " + b |> ot.w.newline)

    ot.w.newlineBlank()
    [|  "let " + enumName + "s = [| "
        [| 0 .. lines.Length - 1 |]
        |> Array.map(fun i -> 
            let a,b = lines[i]
            enumName + "." + a)
        |> String.concat "; "
        " |]" |]
    |> String.Concat
    |> ot.w.newline

    [|  "let " + enumName + "strs = [| "
        [| 0 .. lines.Length - 1 |]
        |> Array.map(fun i -> 
            let a,b = lines[i]
            "\"" + enumName + "\"")
        |> String.concat "; "
        " |]" |]
    |> String.Concat
    |> ot.w.newline

    "let " + t.typeName.ToLower() + name + "Num" + " = " + lines.Length.ToString() |> ot.w.newline

    ot.w.newlineBlank()
    "let int__" + enumName + " v =" |> ot.w.newline
    "match v with" |> ot.w.newlineIndent 1
    [| 0 .. lines.Length - 1 |]
    |> Array.map(fun i -> 
        let a,b = lines[i]
        "| " + i.ToString() + " -> Some " + enumName + "." + a)
    |> Array.iter (ot.w.newlineIndent 1)
    "| _ -> None" |> ot.w.newlineIndent 1

    ot.w.newlineBlank()
    "let str__" + enumName + " s =" |> ot.w.newline
    "match s with" |> ot.w.newlineIndent 1
    [| 0 .. lines.Length - 1 |]
    |> Array.map(fun i -> 
        let a,b = lines[i]
        "| \"" + a + "\" -> Some " + enumName + "." + a)
    |> Array.iter (ot.w.newlineIndent 1)
    "| _ -> None" |> ot.w.newlineIndent 1

    ot.w.newlineBlank()
    "let " + enumName + "__caption e =" |> ot.w.newline
    "match e with" |> ot.w.newlineIndent 1
    [| 0 .. lines.Length - 1 |]
    |> Array.map(fun i -> 
        let a,b = lines[i]
        "| " + enumName + "." + a + " -> \"" + b + "\"")
    |> Array.iter (ot.w.newlineIndent 1)
    "| _ -> \"\"" |> ot.w.newlineIndent 1


    (fun (w:TextBlockWriter) -> 

        [| 0 .. lines.Length - 1 |]
        |> Array.iter(fun i -> 
            let a,b = lines[i]
            "const " + enumName + "_" + a + " = " + i.ToString() + " // " + b |> w.newline)

        //"const enum " + enumName + " {" |> w.newline
        //[| 0 .. lines.Length - 1 |]
        //|> Array.iter(fun i -> 
        //    let a,b = lines[i]
        //    a + " = " + i.ToString() + ", // " + b |> otTypeScript.w.newlineIndent 1)
        //"}" |> w.newline

    //    w.newlineBlank()
    //    "export const int__" + enumName + " = (v:number):" + enumName + " => {" |> w.newline
    //    "switch (v) {" |> w.newlineIndent 1
    //    [| 0 .. lines.Length - 2 |]
    //    |> Array.map(fun i -> 
    //        let a,b = lines[i]
    //        "case " + i.ToString() + ": return " + enumName + "." + a)
    //    |> Array.iter (w.newlineIndent 1)
    //    "default: return " + enumName + "." + (fst lines[lines.Length - 1]) + " }" |> w.newlineIndent 1
    //    "}" |> w.newline

    //    w.newlineBlank()
    //    "export const " + enumName + "__int = (e:" + enumName + "number):int => {" |> w.newline
    //    "switch (e) {" |> w.newlineIndent 1
    //    [| 0 .. lines.Length - 2 |]
    //    |> Array.map(fun i -> 
    //        let a,b = lines[i]
    //        "case " + enumName + "." + a + ": return " + i.ToString())
    //    |> Array.iter (w.newlineIndent 1)
    //    "default: return 0 }" |> w.newlineIndent 1
    //    "}" |> w.newline
        ()) otTypeScript.w

    addMulti "" [| ot; otTypeScript |]

let buildTableType robot (t:Table) (fieldNames:string[],fields) =

    let srcs,sql,ot,otTypeScript,om,omTypeScript,cm,typeTypeScript,cmTypeScript =
        robot__srcs robot

    fieldNames
    |> Array.map(fun i -> 
        let sort,name,def,json = t.fields[i]
        name,fdef__srcTypes(t,name,def))
    |> Array.iter (fun (name,(fsType,csType,tsType)) -> 
        "mutable " + name + ": " + fsType |> ot.w.newline
        name + ": " + tsType |> otTypeScript.w.newlineIndent 1)
    "}" |> ot.w.appendEnd
    "}" |> otTypeScript.w.newline
    addMulti "" [| ot; otTypeScript |]

    [|  "export type " + t.typeName + " = {"
        "id:number"
        "createdat:Date"
        "updatedat:Date"
        "sort:number"
        "p:p" + t.typeName
        "}"
        "" |]
    |> otTypeScript.w.multiLine

    ot.w.newlineBlank()
    "type " + t.typeName + " = Rcd<p" + t.typeName + ">" |> ot.w.newline

    ot.w.newlineBlank()
    [|  "let " + t.typeName + "_fieldorders = \"[ID],[Createdat],[Updatedat],[Sort],"
        fieldNames
        |> Array.map(fun i -> "[" + i + "]")
        |> String.concat ","
        "\"" |]
    |> String.Concat
    |> ot.w.newline

    ot.w.newlineBlank()
    "let p" + t.typeName + "_fieldordersArray = [|" |> ot.w.newline
    fieldNames
    |> Array.map(fun i -> "\"" + i + "\"")
    |> Array.iter (ot.w.newlineIndent 1)
    " |]" |> ot.w.appendEnd

    ot.w.newlineBlank()
    [|  "let " + t.typeName + "_sql_update = \"[Updatedat]=@Updatedat,"
        fieldNames
        |> Array.map(fun i -> "[" + i + "]=@" + i)
        |> String.concat ","
        "\"" |]
    |> String.Concat
    |> ot.w.newline

    ot.w.newlineBlank()
    "let p" + t.typeName + "_fields = [|" |> ot.w.newline
    fields
    |> Array.map(fun (sort,name,def,json) -> 
        match def with
        | FK v -> "FK" + "(\"" + name + "\")"
        | Caption v -> "Caption" + "(\"" + name + "\", " + v.ToString() + ")"
        | Chars v -> "Chars" + "(\"" + name + "\", " + v.ToString() + ")"
        | Link v -> "Link" + "(\"" + name + "\", " + v.ToString() + ")"
        | Text -> "Text" + "(\"" + name + "\")"
        | Bin -> "Bin" + "(\"" + name + "\")"
        | Integer -> "Integer" + "(\"" + name + "\")"
        | Float -> "Float" + "(\"" + name + "\")"
        | Boolean -> "Boolean" + "(\"" + name + "\")"
        | SelectLines lines -> 
            let a = 
                lines
                |> Array.map(fun (p,q) -> "(\"" + p + "\",\"" + q + "\")")
                |> String.concat ";"
            "SelectLines" + "(\"" + name + "\", [| " + a + " |])"
        | Timestamp -> "Timestamp" + "(\"" + name + "\")"
        | TimeSeries -> "TimeSeries" + "(\"" + name + "\")"
        | Other -> "")
    |> Array.iter (ot.w.newlineIndent 1)
    " |]" |> ot.w.appendEnd

    ot.w.newlineBlank()
    "let p" + t.typeName + "_empty(): p" + t.typeName + " = {" |> ot.w.newline
    fieldNames
    |> Array.iter(fun i -> 
        let sort,name,def,json = t.fields[i]
        name + " = " + (fdef__empty ProgrammingLang.FSharp t name def)  |> ot.w.newlineIndent 1)
    " }" |> ot.w.appendEnd

    builderEmpty 
        (robot.config.dbName.ToLower())
        t 
        fieldNames 
        omTypeScript

    ot.w.newlineBlank()
    "let " + t.typeName + "_id = ref " + t.idstarting.ToString() + "L" |> ot.w.newline
    "let " + t.typeName + "_count = ref 0" |> ot.w.newline
    "let " + t.typeName + "_table = \"" + t.tableName + "\"" |> ot.w.newline

    ot.w.newlineBlank()

let buildTableMor om (t:Table) (fieldNames:string[],fields) =

    om.w.newlineBlank()
    "let db__p" + t.typeName + "(line:Object[]): p" + t.typeName + " =" |> om.w.newline
    "let p = p" + t.typeName + "_empty()" |> om.w.newlineIndent 1

    om.w.newlineBlank()
    [| 0 .. fieldNames.Length - 1 |]
    |> Array.iter(fun i -> 
        let sort,name,def,json = t.fields[fieldNames[i]]
        "p." + name + " <- " |> om.w.newlineIndent 1

        let item = "line.[" + (i + 4).ToString() + "]"

        match def with
        | FK v -> "if Convert.IsDBNull(" + item + ") then 0L else " + item + " :?> int64"
        | Caption v -> "string(" + item + ").TrimEnd()"
        | Chars v -> "string(" + item + ").TrimEnd()"
        | Link v -> "string(" + item + ").TrimEnd()"
        | Text -> "string(" + item + ").TrimEnd()"
        | Bin -> item + " :?> byte[]"
        | Integer -> "if Convert.IsDBNull(" + item + ") then 0L else " + item + " :?> int64"
        | Float -> "if Convert.IsDBNull(" + item + ") then 0.0 else " + item + " :?> float"
        | Boolean -> "if Convert.IsDBNull(" + item + ") then false else " + item + " :?> bool"
        | SelectLines lines -> "EnumOfValue(if Convert.IsDBNull(" + item + ") then 0 else " + item + " :?> int)"
        | Timestamp -> "DateTime.FromBinary(if Convert.IsDBNull(" + item + ") then DateTime.MinValue.Ticks else " + item + " :?> int64)"
        | TimeSeries -> "TimeSeries" + "(\"" + name + "\")"
        | Other -> ""
        |> om.w.appendEnd)
    om.w.newlineBlank()
    "p" |> om.w.newlineIndent 1

    om.w.newlineBlank()
    "let p" + t.typeName + "__sps (p:p" + t.typeName + ") = [|" |> om.w.newline
    fieldNames
    |> Array.iter(fun i -> 
        let sort,name,def,json = t.fields[i]
        "new SqlParameter(\"" + name + "\", " |> om.w.newlineIndent 1
        match def with
        | FieldDef.Timestamp -> "p." + name + ".Ticks"
        | _ -> "p." + name 
        |> om.w.appendEnd
        ")" |> om.w.appendEnd)
    " |]" |> om.w.appendEnd

    om.w.newlineBlank()
    "let db__" + t.typeName + " = db__Rcd db__p" + t.typeName |> om.w.newline

    om.w.newlineBlank()
    "let " + t.typeName + "_wrapper item: " + t.typeName + " =" |> om.w.newline
    "let (i,c,u,s),p = item" |> om.w.newlineIndent 1
    "{ ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }" |> om.w.newlineIndent 1

    om.w.newlineBlank()
    "let p" + t.typeName + "_clone (p:p" + t.typeName + "): p" + t.typeName + " = {" |> om.w.newline
    fieldNames
    |> Array.iter(fun i -> 
        let sort,name,def,json = t.fields[i]
        name + " = p." + name |> om.w.newlineIndent 1)
    " }" |> om.w.appendEnd

    om.w.newlineBlank()
    "let " + t.typeName + "_update_transaction output (updater,suc,fail) (rcd:" + t.typeName + ") =" |> om.w.newline
    "let rollback_p = rcd.p |> p" + t.typeName + "_clone" |> om.w.newlineIndent 1
    "let rollback_updatedat = rcd.Updatedat" |> om.w.newlineIndent 1
    "updater rcd.p" |> om.w.newlineIndent 1
    "let ctime,res =" |> om.w.newlineIndent 1
    "(rcd.ID,rcd.p,rollback_p,rollback_updatedat)" |> om.w.newlineIndent 2
    "|> update (conn,output," + t.typeName + "_table," + t.typeName + "_sql_update,p" + t.typeName + "__sps,suc,fail)" |> om.w.newlineIndent 2
    "match res with" |> om.w.newlineIndent 1
    "| Suc ctx ->" |> om.w.newlineIndent 1
    "rcd.Updatedat <- ctime" |> om.w.newlineIndent 2
    "suc(ctime,ctx)" |> om.w.newlineIndent 2
    "| Fail(eso,ctx) ->" |> om.w.newlineIndent 1
    "rcd.p <- rollback_p" |> om.w.newlineIndent 2
    "rcd.Updatedat <- rollback_updatedat" |> om.w.newlineIndent 2
    "fail eso" |> om.w.newlineIndent 2

    om.w.newlineBlank()
    "let " + t.typeName + "_update output (rcd:" + t.typeName + ") =" |> om.w.newline
    "rcd" |> om.w.newlineIndent 1
    "|> " + t.typeName + "_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))" |> om.w.newlineIndent 1

    om.w.newlineBlank()
    "let " + t.typeName + "_create_incremental_transaction output (suc,fail) p =" |> om.w.newline
    "let cid = Interlocked.Increment " + t.typeName + "_id" |> om.w.newlineIndent 1
    "let ctime = DateTime.UtcNow" |> om.w.newlineIndent 1
    "match create (conn,output," + t.typeName + "_table,p" + t.typeName + "__sps) (cid,ctime,p) with" |> om.w.newlineIndent 1
    "| Suc ctx -> ((cid,ctime,ctime,cid),p) |> " + t.typeName + "_wrapper |> suc" |> om.w.newlineIndent 1
    "| Fail(eso,ctx) -> fail(eso,ctx)" |> om.w.newlineIndent 1

    om.w.newlineBlank()
    "let " + t.typeName + "_create output p =" |> om.w.newline
    t.typeName + "_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p" |> om.w.newlineIndent 1
    "" |> om.w.newlineIndent 1

    om.w.newlineBlank()
    "let id__" + t.typeName + "o id: " + t.typeName + " option = id__rcd(conn," + t.typeName + "_fieldorders," + t.typeName + "_table,db__" + t.typeName + ") id" |> om.w.newline

    om.w.newlineBlank()
    "let " + t.typeName + "_metadata = {" |> om.w.newline
    "fieldorders = " + t.typeName + "_fieldorders" |> om.w.newlineIndent 1
    "db__rcd = db__" + t.typeName + " " |> om.w.newlineIndent 1
    "wrapper = " + t.typeName + "_wrapper" |> om.w.newlineIndent 1
    "sps = p" + t.typeName + "__sps" |> om.w.newlineIndent 1
    "id = " + t.typeName + "_id" |> om.w.newlineIndent 1
    "id__rcdo = id__" + t.typeName + "o" |> om.w.newlineIndent 1
    "clone = p" + t.typeName + "_clone" |> om.w.newlineIndent 1
    "empty__p = p" + t.typeName + "_empty" |> om.w.newlineIndent 1
    "rcd__bin = " + t.typeName + "__bin" |> om.w.newlineIndent 1
    "bin__rcd = bin__" + t.typeName |> om.w.newlineIndent 1
    "sql_update = " + t.typeName + "_sql_update" |> om.w.newlineIndent 1
    "rcd_update = " + t.typeName + "_update" |> om.w.newlineIndent 1
    "table = " + t.typeName + "_table" |> om.w.newlineIndent 1
    "shorthand = \"" + t.typeName.ToLower() + "\"" |> om.w.newlineIndent 1
    " }" |> om.w.appendEnd

    om.w.newlineBlank()
    "let " + t.typeName + "TxSqlServer =" |> om.w.newline
    "\"\"\"" |> om.w.newlineIndent 1
    "IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + t.tableName + "' AND xtype='U')" |> om.w.newlineIndent 1
    "BEGIN" + crlf |> om.w.newlineIndent 1
    "    CREATE TABLE " + t.tableName + " ([ID] BIGINT NOT NULL" |> om.w.newlineIndent 1
    ",[Createdat] BIGINT NOT NULL" |> om.w.newlineIndent 1
    ",[Updatedat] BIGINT NOT NULL" |> om.w.newlineIndent 1
    ",[Sort] BIGINT NOT NULL," |> om.w.newlineIndent 1

    fieldNames
    |> Array.iter(fun i -> 
        let sort,name,def,json = t.fields[i]
        ",[" + name + "]" |> om.w.newlineIndent 1)

    ")" |> om.w.appendEnd
    "END" |> om.w.newlineIndent 1
    "\"\"\"" |> om.w.newlineIndent 1


    om.w.newlineBlank()

let buildTable robot (t:Table) =

    let srcs,sql,ot,otTypeScript,om,omTypeScript,cm,typeTypeScript,cmTypeScript =
        robot__srcs robot

    "// [" + t.tableName + "] (" + t.typeName + ")" |> addMulti <| [| ot; otTypeScript |]
    addMulti "" [| ot; otTypeScript |]

    handleEnumFields (buildTableEnums robot t) t

    "type p" + t.typeName + " = {" |> ot.w.newline
    "export type p" + t.typeName + " = {" |> otTypeScript.w.newline

    let fieldNames = t |> table__fieldKeys
    let fields = fieldNames |> Array.map(fun i -> t.fields[i])

    buildTableType robot t (fieldNames,fields)
    buildTableMor om t (fieldNames,fields)

let buildTables robot tables =

    let srcs,sql,ot,otTypeScript,om,omTypeScript,cm,typeTypeScript,cmTypeScript =
        robot__srcs robot

    om.w.newlineBlank()
    "let mutable conn = \"\"" |> om.w.newline

    tables
    |> Array.iter (buildTable robot)

    om.w.newlineBlank()
    if tables.Length > 0 then
        "type MetadataEnum = " |> om.w.newline
        [| 0 .. tables.Length - 1 |]
        |> Array.map(fun i -> "| " + tables[i].typeName + " = " + i.ToString())
        |> Array.iter om.w.newline

    om.w.newlineBlank()
    "let tablenames = [|" |> om.w.newline
    tables
    |> Array.map(fun t -> t.typeName + "_metadata.table")
    |> Array.iter(om.w.newlineIndent 1)
    " |]" |> om.w.appendEnd

    om.w.newlineBlank()
    "let init() =" |> om.w.newline
    tables
    |> Array.iter(fun t -> 
        om.w.newlineBlank()
        "match singlevalue_query conn (str__sql \"SELECT MAX(ID) FROM [" + t.tableName + "]\") with" |> om.w.newlineIndent 1
        "| Some v ->" |> om.w.newlineIndent 1
        "let max = v :?> int64" |> om.w.newlineIndent 2
        "if max > " + t.typeName + "_id.Value then" |> om.w.newlineIndent 2
        t.typeName + "_id.Value <- max" |> om.w.newlineIndent 3
        "| None -> ()" |> om.w.newlineIndent 1
        om.w.newlineBlank()
        "match singlevalue_query conn (str__sql \"SELECT COUNT(ID) FROM [" + t.tableName + "]\") with" |> om.w.newlineIndent 1
        "| Some v -> " + t.typeName + "_count.Value <- v :?> int32" |> om.w.newlineIndent 1
        "| None -> ()" |> om.w.newlineIndent 1)
    "()" |> om.w.newlineIndent 1

let buildType ns src t = 

    let tbw = new TextBlockWriter(src.buffer)

    tbw.newlineBlank()
    "// [" + t.name + "] Structure" |> tbw.newline

    match t.tEnum with
    | TypeEnum.Orm table -> ()
    | _ ->
        [|  ""; 
            match src.lang with
            | ProgrammingLang.FSharp -> "let " + t.name + "__bin (bb:BytesBuilder) (v:" + t.name + ") ="
            | ProgrammingLang.TypeScript -> 
                //"export const " + t.name + "__bin = (bb:BytesBuilder) => (v:" + t.name + ") => {"
                "export const " + t.name + "__bin = (bb:BytesBuilder) => (v:any) => {"
            | _ -> ()
            "" |] 
        |> tbw.multiLine

    match src.lang with
    | ProgrammingLang.FSharp -> 
        CodeRobotIIFs.t__binImpl tbw 0 t
    | ProgrammingLang.TypeScript -> 
        CodeRobotIITs.t__binImpl ns tbw 0 t
        "}" |> tbw.newline
    | _ -> ()

    match t.tEnum with
    | TypeEnum.Orm table -> ()
    | _ ->
        match src.lang with
        | ProgrammingLang.FSharp -> 
            [|  ""
                "let bin__" + t.name + " (bi:BinIndexed):" + t.name + " =" 
                tab + "let bin,index = bi"
                "" |]
        | ProgrammingLang.TypeScript -> 
            [|  ""
                "export const bin__" + t.name + " = (bi:BinIndexed):" + t.name + " => {"
                "" |]
        | _ -> [| |]
        |> tbw.multiLine


    match src.lang with
    | ProgrammingLang.FSharp -> 
        CodeRobotIIFs.bin__tImpl tbw 0 t
    | ProgrammingLang.TypeScript -> 
        CodeRobotIITs.bin__tImpl ns tbw 0 t
        "}" |> tbw.newline
    | _ -> ()

    match t.tEnum with
    | TypeEnum.Orm table -> ()
    | _ ->
        match src.lang with
        | ProgrammingLang.FSharp -> 
            [|  ""; 
                "let " + t.name + "__json (v:" + t.name + ") ="
                "" |] 
            |> tbw.multiLine
        | _ -> ()

    match src.lang with
    | ProgrammingLang.FSharp -> 
        CodeRobotIIFs.t__jsonImpl tbw 0 t
    | ProgrammingLang.TypeScript -> 
        ()
        //CodeRobotIITs.t__jsonImpl tbw 0 t
    | _ -> ()

    match src.lang with
    | ProgrammingLang.FSharp -> 
        [|  ""; 
            "let " + t.name + "__jsonTbw (w:TextBlockWriter) (v:" + t.name + ") ="
            tab + "json__str w (" + t.name + "__json v)"
            ""
            "let " + t.name + "__jsonStr (v:" + t.name + ") ="
            tab + "(" + t.name + "__json v) |> json__strFinal"
            "" |] 
        |> tbw.multiLine
    | _ -> ()

    match t.tEnum with
    | TypeEnum.Orm table -> ()
    | _ ->
        match src.lang with
        | ProgrammingLang.FSharp -> 
            [|  ""
                "let json__" + t.name + "o (json:Json):" + t.name + " option =" 
                tab + "let fields = json |> json__items"
                "" |]
            |> tbw.multiLine
        | _ -> ()

    match src.lang with
    | ProgrammingLang.FSharp -> 
        CodeRobotIIFs.json__tImpl tbw 0 t
    | ProgrammingLang.TypeScript -> 
        ()
        //CodeRobotIITs.json__tImpl tbw 0 t
    | _ -> ()

let buildCustomTypes config tc src srcTypeScript (cTypes:Dictionary<string,Type>) = 

    [|  "declare global {"
        ""
        "namespace " + config.dbName.ToLower() + " {"
        ""
        "" |]
    |> src.w.multiLine

    cTypes.Values
    |> Seq.iter(fun t -> 
    
        [|  ""
            "// [" + t.name + "]" |]
        |> src.w.multiLine

        LangPackTypeScript.type__TypeScript tc src srcTypeScript t)

    [|  ""
        "}"
        ""
        "}"
        ""
        "export {}"
        "" |]
    |> src.w.multiLine

let prepareRobot output config= 

    let sql =
        config.mainDir + "\OrmTypes.sql"
        |> create__Src
    let ot =
        config.mainDir + "\OrmTypes.fs"
        |> create__Src
    let otTypeScript = 
        config.JsDir + "\OrmTypes.d.ts"
        |> create__Src

    let om = 
        config.mainDir + "\OrmMor.fs"
        |> create__Src
    let omTypeScript = 
        config.JsDir + "\OrmMor.ts"
        |> create__Src

    let cm = 
        config.mainDir + "\CustomMor.fs"
        |> create__Src
    let typeTypeScript = 
        config.JsDir + "\Types.d.ts"
        |> create__Src
    let cmTypeScript = 
        config.JsDir + "\CustomMor.ts"
        |> create__Src

    {
        srcs =
            [|  sql
                ot 
                otTypeScript
                om
                omTypeScript
                cm
                typeTypeScript
                cmTypeScript |]
        sql = sql
        ot = ot
        otTypeScript = otTypeScript
        om = om
        omTypeScript = omTypeScript
        cm = cm
        typeTypeScript = typeTypeScript
        cmTypeScript = cmTypeScript
        config = config
        output = output }


let go output config = 

    let robot = prepareRobot output config

    let cTypes,tc,tables  = load robot

    let srcs,sql,ot,otTypeScript,om,omTypeScript,cm,typeTypeScript,cmTypeScript =
        robot__srcs robot

    [|  "USE [" + config.dbName + "]"
        "" |]
    |> sql.w.multiLine

    tables
    |> Array.iter (table__sql sql.w)

    [|  "// OrmMor.ts"
        "import { BytesBuilder } from \"~/lib/util/bin\""
        "import * as binCommon from '~/lib/util/bin'"
        "const marshall = {...binCommon }"
        "" |]
    |> omTypeScript.w.multiLine

    [|  "// OrmMor.ts"
        "import { BytesBuilder } from \"~/lib/util/bin\""
        "import * as binCommon from '~/lib/util/bin'"
        "import * as binOrm from './OrmMor'"
        "const marshall = {...binCommon, ...binOrm }"
        "" |]
    |> cmTypeScript.w.multiLine

    [|  "declare global {"
        ""
        "namespace " + config.dbName.ToLower() + " {"
        "" |]
    |> otTypeScript.w.multiLine
    
    fSharpHeader ot (config.ns + ".OrmTypes") [||]
    [|  "open System.Data.SqlClient"
        "open System.Threading"
        "open Util.Bin"
        "open " + config.ns + ".OrmTypes"
        "open " + config.ns + ".Types" |]
    |> fSharpHeader om (config.ns + ".OrmMor")
    [|  "open Util.Bin"
        "open " + config.ns + ".OrmTypes"
        "open " + config.ns + ".Types"
        "open " + config.ns + ".OrmMor" |]
    |> fSharpHeader cm (config.ns + ".CustomMor")

    let sorted = tc |> tc__sorted

    buildCustomTypes config tc typeTypeScript cmTypeScript cTypes

    let ns = config.dbName.ToLower()

    sorted
    |> Array.iter(fun t ->
        match t.tEnum |> typeEnum__plain with
        | TypeEnumPlain.Orm -> 
            buildType ns om t
            buildType ns omTypeScript t
        | TypeEnumPlain.Structure
        | TypeEnumPlain.Sum -> 
            buildType ns cm t
            buildType ns cmTypeScript t
        | _ -> ())
    
    sorted
    |> filter<Type,Table> matchOrm
    |> buildTables robot

    [|  ""
        "}"
        ""
        "}"
        ""
        "export {}"
        "" |]
    |> otTypeScript.w.multiLine

    save srcs

    "Done" |> output
        