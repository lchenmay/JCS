module TypeSys.CodeRobotIII

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

open TypeSys.TypeSys
open TypeSys.CodeRobotI
open TypeSys.CodeRobotIIFs
open TypeSys.CodeRobotIITs

let updateDatabase output rdbms (conn:string) tables = 

    //let sqls = empty__TextBlockWriter()
    
    //match rdbms with
    //| Rdbms.SqlServer ->

    //     create
    //    let db1(f:Dictionary<string,string>) =
    //        let n = f.["name"]
    //        match f.["enum"] with
    //        | "FK" ->
    //            "[" + n + "] BIGINT"
    //        | "Caption" ->
    //            "[" + n + "] NVARCHAR(" + f.["length"] + ") COLLATE Chinese_PRC_CI_AS"
    //        | "Chars" ->
    //            "[" + n + "] NVARCHAR(" + f.["length"] + ") COLLATE Chinese_PRC_CI_AS"
    //        | "Link" ->
    //            "[" + n + "] NVARCHAR(" + f.["length"] + ") COLLATE Chinese_PRC_CI_AS"
    //        | "Text" ->
    //            "[" + n + "] NVARCHAR(MAX)"
    //        | "RichText" ->
    //            "[" + n + "] NVARCHAR(MAX)"
    //        | "Integer" ->
    //            "[" + n + "] BIGINT"
    //        | "Money" ->
    //            "[" + n + "] FLOAT"
    //        | "Float" ->
    //            "[" + n + "] FLOAT"
    //        | "Boolean" ->
    //            "[" + n + "] BIT"
    //        | "Pwd" ->
    //            "[" + n + "] NVARCHAR(64) COLLATE Chinese_PRC_CI_AS"
    //        | "SelectLines" ->
    //            "[" + n + "] INT"
    //        | "Timestamp" ->
    //            "[" + n + "] BIGINT"
    //        | "Bin"
    //        | "TimeSeries" ->
    //            "[" + n + "] VARBINARY(MAX)"
    //        | _ -> ""

    //    let mutable dbName = conn.ToUpper() |> regex_match(string__regex("(?<=DATABASE=)[^;]+"))
    //    if dbName.Length = 0 then
    //        dbName <- conn.ToUpper() |> regex_match(string__regex("(?<=CATALOG=)[^;]+"))
                    
    //    match tx conn output [|{ text = "USE [" + dbName + "]"; ps = [||] }|] with
    //    | Cat.Suc x ->
    //        tables
    //        |> Array.iter(fun table ->

    //            match 
    //                "SELECT [name] FROM SYSCOLUMNS WHERE id=object_id('" + table.tableName + "')"
    //                |> str__sql
    //                |> multiline_query conn with
    //            | Suc x ->

    //                let cols = x.lines.ToArray() |> Array.map(fun item -> string item.[0])

    //                let sqlcreatetable =
    //                    let sb = new StringBuilder()
    //                    sb.Append("IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + table.tableName + "' AND xtype='U')" + crlf) |> ignore
    //                    sb.Append("BEGIN" + crlf) |> ignore
    //                    sb.Append("    CREATE TABLE " + table.tableName + " ([ID] BIGINT NOT NULL") |> ignore
    //                    sb.Append(",[Createdat] BIGINT NOT NULL") |> ignore
    //                    sb.Append(",[Updatedat] BIGINT NOT NULL") |> ignore
    //                    sb.Append(",[Sort] BIGINT NOT NULL,") |> ignore

    //                    table 
    //                    |> table__fieldKeys
    //                    |> Array.map(fun i -> table.fields[i])
    //                    |> Array.map(fun f ->
    //                        let sort,name,def,json = f
    //                        json |> db1)
    //                    |> Seq.toArray
    //                    |> String.concat ","
    //                    |> sb.Append
    //                    |> ignore

    //                    sb.Append(", CONSTRAINT [PK_" + table.tableName + "] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]" + crlf) |> ignore
    //                    sb.Append("END" + crlf + crlf) |> ignore

    //                    sb.Append("IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='PK_" + table.tableName + "')" + crlf) |> ignore
    //                    sb.Append("BEGIN" + crlf) |> ignore
    //                    sb.Append(" ALTER TABLE [" + table.tableName + "] DROP CONSTRAINT [PK_" + table.tableName + "] WITH ( ONLINE = OFF )" + crlf) |> ignore
    //                    sb.Append("END" + crlf + crlf) |> ignore

    //                    sb.Append("IF NOT EXISTS(SELECT * FROM SYSINDEXES WHERE name='PK_" + table.tableName + "_ID')" + crlf) |> ignore
    //                    sb.Append("BEGIN" + crlf) |> ignore
    //                    sb.Append(" CREATE CLUSTERED INDEX [PK_" + table.tableName + "_ID] ON [" + table.tableName + "](ID)" + crlf) |> ignore
    //                    sb.Append("END" + crlf) |> ignore

    //                    { text = sb.ToString(); ps = [||]}

    //                match tx conn output [|sqlcreatetable|] with
    //                | Suc x -> output("Create [" + table.tableName + "] OK")
    //                | Fail(exn,so) ->
    //                    output("Failed: " + sqlcreatetable.text + ": " + exn.ToString())

    //                cols
    //                |> Seq.iter(fun col ->
    //                    let find = transobj.fields |> Seq.tryFind(fun f -> f.["name"] = col)
    //                    if(find.IsNone && col <> "ID" && col <> "Createdat" && col <> "Updatedat" && col <> "Sort") then
    //                        output("Droping ["+transobj.name+"].["+col+"], CONFIRM...")
    //                        //System.Console.ReadLine() |> ignore
    //                        match tx conn output [|str__sql("ALTER TABLE "+transobj.name+" DROP COLUMN ["+col+"]")|] with
    //                        | Suc x -> output("OK")
    //                        | Fail(exn,so) ->
    //                            output("Failed: "+exn.ToString()))

    //                transobj.fields
    //                |> Seq.iter(fun f ->
    //                    let fname = f.["name"]
    //                    let find = cols |> Seq.tryFind(fun c -> c = fname)
    //                    if(find.IsNone) then
    //                        output("Adding ["+transobj.name+"].["+fname+"], CONFIRM...")
    //                        //System.Console.ReadLine() |> ignore
    //                        match tx conn output [|str__sql(" ALTER TABLE "+transobj.name+" ADD "+db1(f))|] with
    //                        | Suc x -> output("OK")
    //                        | Fail(exn,so) ->
    //                            output("Failed: "+exn.ToString()))

    //            | _ -> ())
    //    | Cat.Fail(b,a) -> 
    //        if(b.exno.IsSome) then
    //            output(b.exno.Value.ToString())
    //            output("CONN: " + conn)

    //    transobjects.Values |> Seq.iter(fun item ->
    //        let table, transobj = item
    //        let sb = new StringBuilder()
    //        sb.Append("IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + table + "' AND xtype='U')" + Util.Text.crlf) |> ignore
    //        sb.Append("BEGIN" + Util.Text.crlf) |> ignore
    //        sb.Append("    CREATE TABLE " + table + " ([ID] BIGINT NOT NULL") |> ignore
    //        sb.Append(",[Createdat] BIGINT NOT NULL") |> ignore
    //        sb.Append(",[Updatedat] BIGINT NOT NULL") |> ignore
    //        sb.Append(",[Sort] BIGINT NOT NULL,") |> ignore

    //        transobj.fields
    //        |> Seq.map(fun f -> f |> db1)
    //        |> Seq.toArray
    //        |> String.concat(",")
    //        |> sb.Append
    //        |> ignore

    //        sb.Append(", CONSTRAINT [PK_" + table + "] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]" + crlf) |> ignore
    //        sb.Append("END" + Util.Text.crlf) |> ignore
    //        sqls.Add({ text = sb.ToString(); ps = [||]})

    //        transobj.fields
    //        |> Seq.iter(fun f ->

    //            let fname = f.["name"]
    //            let fullname = transobj.name+"_"+fname

    //            sb.Append("--["+transobj.name+"].["+fname+"]--------------------" + crlf) |> ignore

    //            sb.Append("IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('"+transobj.name+"') AND name='"+fname+"')" + crlf) |> ignore
    //            sb.Append("BEGIN" + crlf) |> ignore
    //            sb.Append(" ALTER TABLE "+transobj.name+" ALTER COLUMN ["+fname+"] NCHAR(64) COLLATE Chinese_PRC_CI_AS " + crlf) |> ignore
    //            sb.Append("END" + crlf) |> ignore
    //            sb.Append("ELSE" + crlf) |> ignore
    //            sb.Append("BEGIN" + crlf) |> ignore
    //            sb.Append(" ALTER TABLE "+transobj.name+" ADD ["+fname+"] NCHAR(64) COLLATE Chinese_PRC_CI_AS " + crlf) |> ignore
    //            sb.Append("END" + crlf) |> ignore

    //            //sqlIndexSb.Append("UPDATE "+transobj.name+" SET ["+fname+"]='-' WHERE (["+fname+"] IS NULL)" + crlf) |> ignore
    //            //sqlIndexSb.Append("IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_SrOrm_"+fullname+"')" + crlf) |> ignore
    //            //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
    //            //sqlIndexSb.Append(" ALTER TABLE Ca_Staff DROP  CONSTRAINT [Constraint_SrOrm_"+fullname+"]" + crlf) |> ignore
    //            //sqlIndexSb.Append("END" + crlf) |> ignore

    //            //sqlIndexSb.Append("IF NOT EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_"+fullname+"')" + crlf) |> ignore
    //            //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
    //            //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" ADD  CONSTRAINT [Constraint_"+fullname+"] DEFAULT('-') FOR ["+fname+"]" + crlf) |> ignore
    //            //sqlIndexSb.Append("END" + crlf) |> ignore
    //            //sqlIndexSb.Append("" + crlf) |> ignore
    //            //sqlIndexSb.Append("IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_"+fullname+"')" + crlf) |> ignore
    //            //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
    //            //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" DROP  CONSTRAINT [UniqueNonclustered_"+fullname+"]" + crlf) |> ignore
    //            //sqlIndexSb.Append("END" + crlf) |> ignore

    //            //let s = " ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf
    //            //sb.Append(" ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf) |> ignore

    //            ()))

    //| _ -> ()

    //sqls

    ()