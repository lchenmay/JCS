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

open TypeSys.Rdmbs
open TypeSys.MetaType
open TypeSys.CodeRobotI
open TypeSys.CodeRobotIIFs
open TypeSys.CodeRobotIITs

let table__sql (w:TextBlockWriter) table = 

    "-- [" + table.tableName + "] ----------------------"
    |> w.newline

    let sb = new StringBuilder()
    [|  "IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + table.tableName + "' AND xtype='U')" + crlf
        "BEGIN" + crlf
        tab + "CREATE TABLE " + table.tableName + " ([ID] BIGINT NOT NULL"
        tab + tab + ",[Createdat] BIGINT NOT NULL"
        tab + tab + ",[Updatedat] BIGINT NOT NULL"
        tab + tab + ",[Sort] BIGINT NOT NULL,"
        tab + tab + "" |]
    |> w.multiLine

    table.fields.Values
    |> Seq.toArray
    |> Array.map sqlField
    |> String.concat(crlf + tab + tab + ",")
    |> w.appendEnd

    [|  ", CONSTRAINT [PK_" + table.tableName + "] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]"
        "END"
        "" |]
    |> w.multiLine  

    table.fields.Values
    |> Seq.toArray
    |> Array.iter(fun f ->

        let sort,fname,def,json = f

        let t = sqlField f

        [|  ""
            "IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('" + table.tableName + "') AND name='" + fname + "')"
            "BEGIN"
            " ALTER TABLE " + table.tableName + " ALTER COLUMN " + t
            "END"
            "ELSE"
            "BEGIN"
            " ALTER TABLE " + table.tableName + " ADD " + t
            "END"
            "" |]
        |> w.multiLine

        //sqlIndexSb.Append("UPDATE "+transobj.name+" SET ["+fname+"]='-' WHERE (["+fname+"] IS NULL)" + crlf) |> ignore
        //sqlIndexSb.Append("IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_SrOrm_"+fullname+"')" + crlf) |> ignore
        //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
        //sqlIndexSb.Append(" ALTER TABLE Ca_Staff DROP  CONSTRAINT [Constraint_SrOrm_"+fullname+"]" + crlf) |> ignore
        //sqlIndexSb.Append("END" + crlf) |> ignore

        //sqlIndexSb.Append("IF NOT EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_"+fullname+"')" + crlf) |> ignore
        //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
        //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" ADD  CONSTRAINT [Constraint_"+fullname+"] DEFAULT('-') FOR ["+fname+"]" + crlf) |> ignore
        //sqlIndexSb.Append("END" + crlf) |> ignore
        //sqlIndexSb.Append("" + crlf) |> ignore
        //sqlIndexSb.Append("IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_"+fullname+"')" + crlf) |> ignore
        //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
        //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" DROP  CONSTRAINT [UniqueNonclustered_"+fullname+"]" + crlf) |> ignore
        //sqlIndexSb.Append("END" + crlf) |> ignore

        //let s = " ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf
        //sb.Append(" ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf) |> ignore

        ())

let updateDatabase output rdbms (conn:string) tables = 

    let sqls = new List<Sql>()
    
    match rdbms with
    | Rdbms.SqlServer ->

        let mutable dbName = conn.ToUpper() |> regex_match(string__regex("(?<=DATABASE=)[^;]+"))
        if dbName.Length = 0 then
            dbName <- conn.ToUpper() |> regex_match(string__regex("(?<=CATALOG=)[^;]+"))
                    
        match 
            [|  { text = "USE [" + dbName + "]"; ps = [||] }    |]
            |> tx conn output with
        | Suc x ->
            tables
            |> Array.iter(fun table ->

                match 
                    "SELECT [name] FROM SYSCOLUMNS WHERE id=object_id('" + table.tableName + "')"
                    |> str__sql
                    |> multiline_query conn with
                | Suc x ->

                    let cols = x.lines.ToArray() |> Array.map(fun item -> string item.[0])

                    let sqlcreatetable =
                        let sb = new StringBuilder()
                        sb.Append("IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + table.tableName + "' AND xtype='U')" + crlf) |> ignore
                        sb.Append("BEGIN" + crlf) |> ignore
                        sb.Append("    CREATE TABLE " + table.tableName + " ([ID] BIGINT NOT NULL") |> ignore
                        sb.Append(",[Createdat] BIGINT NOT NULL") |> ignore
                        sb.Append(",[Updatedat] BIGINT NOT NULL") |> ignore
                        sb.Append(",[Sort] BIGINT NOT NULL,") |> ignore

                        table 
                        |> table__fieldKeys
                        |> Array.map(fun i -> table.fields[i])
                        |> Array.map sqlField
                        |> String.concat ","
                        |> sb.Append
                        |> ignore

                        sb.Append(", CONSTRAINT [PK_" + table.tableName + "] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]" + crlf) |> ignore
                        sb.Append("END" + crlf + crlf) |> ignore

                        sb.Append("IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='PK_" + table.tableName + "')" + crlf) |> ignore
                        sb.Append("BEGIN" + crlf) |> ignore
                        sb.Append(" ALTER TABLE [" + table.tableName + "] DROP CONSTRAINT [PK_" + table.tableName + "] WITH ( ONLINE = OFF )" + crlf) |> ignore
                        sb.Append("END" + crlf + crlf) |> ignore

                        sb.Append("IF NOT EXISTS(SELECT * FROM SYSINDEXES WHERE name='PK_" + table.tableName + "_ID')" + crlf) |> ignore
                        sb.Append("BEGIN" + crlf) |> ignore
                        sb.Append(" CREATE CLUSTERED INDEX [PK_" + table.tableName + "_ID] ON [" + table.tableName + "](ID)" + crlf) |> ignore
                        sb.Append("END" + crlf) |> ignore

                        { text = sb.ToString(); ps = [||]}

                    match tx conn output [|sqlcreatetable|] with
                    | Suc x -> output("Create [" + table.tableName + "] OK")
                    | Fail(exn,so) ->
                        output("Failed: " + sqlcreatetable.text + ": " + exn.ToString())

                    cols
                    |> Array.filter(fun col -> col <> "ID" && col <> "Createdat" && col <> "Updatedat" && col <> "Sort")
                    |> Array.iter(fun col ->
                        match
                            table 
                            |> table__fieldKeys
                            |> Array.tryFind(fun f -> f = col) with
                        | Some f ->
                            [|  "ALTER TABLE "
                                table.tableName
                                " DROP COLUMN [" + col + "]" |]
                            |> String.Concat
                            |> str__sql
                            |> sqls.Add
                        | None -> ())

                    table 
                    |> table__fieldKeys
                    |> Array.iter(fun f ->
                        match cols |> Seq.tryFind(fun c -> c = f) with
                        | Some f -> ()
                        | None ->

                            let s = 
                                table.fields[f]
                                |> sqlField

                            [|  " ALTER TABLE "
                                table.tableName
                                " ADD "
                                s |]
                            |> String.Concat
                            |> str__sql
                            |> sqls.Add)

                | _ -> ())
        | Fail(b,a) -> 
            if b.exno.IsSome then
                b.exno.Value.ToString() |> output
                "CONN: " + conn |> output

        tables
        |> Array.iter(fun table ->
            let sb = new StringBuilder()
            sb.Append("IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='" + table.tableName + "' AND xtype='U')" + Util.Text.crlf) |> ignore
            sb.Append("BEGIN" + Util.Text.crlf) |> ignore
            sb.Append("    CREATE TABLE " + table.tableName + " ([ID] BIGINT NOT NULL") |> ignore
            sb.Append(",[Createdat] BIGINT NOT NULL") |> ignore
            sb.Append(",[Updatedat] BIGINT NOT NULL") |> ignore
            sb.Append(",[Sort] BIGINT NOT NULL,") |> ignore

            table.fields.Values
            |> Seq.toArray
            |> Array.map sqlField
            |> String.concat(",")
            |> sb.Append
            |> ignore

            sb.Append(", CONSTRAINT [PK_" + table.tableName + "] PRIMARY KEY CLUSTERED ([ID] ASC)) ON [PRIMARY]" + crlf) |> ignore
            sb.Append("END" + Util.Text.crlf) |> ignore
            sqls.Add({ text = sb.ToString(); ps = [||]})

            table.fields.Values
            |> Seq.toArray
            |> Array.iter(fun f ->

                let sort,fname,def,json = f

                sb.Append("--[" + table.tableName + "]--------------------" + crlf) |> ignore

                sb.Append("IF EXISTS(SELECT * FROM SYSCOLUMNS WHERE id=object_id('" + table.tableName + "') AND name='"+fname+"')" + crlf) |> ignore
                sb.Append("BEGIN" + crlf) |> ignore
                sb.Append(" ALTER TABLE " + table.tableName + " ALTER COLUMN ["+fname+"] NCHAR(64) COLLATE Chinese_PRC_CI_AS " + crlf) |> ignore
                sb.Append("END" + crlf) |> ignore
                sb.Append("ELSE" + crlf) |> ignore
                sb.Append("BEGIN" + crlf) |> ignore
                sb.Append(" ALTER TABLE " + table.tableName + " ADD ["+fname+"] NCHAR(64) COLLATE Chinese_PRC_CI_AS " + crlf) |> ignore
                sb.Append("END" + crlf) |> ignore

                //sqlIndexSb.Append("UPDATE "+transobj.name+" SET ["+fname+"]='-' WHERE (["+fname+"] IS NULL)" + crlf) |> ignore
                //sqlIndexSb.Append("IF EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_SrOrm_"+fullname+"')" + crlf) |> ignore
                //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
                //sqlIndexSb.Append(" ALTER TABLE Ca_Staff DROP  CONSTRAINT [Constraint_SrOrm_"+fullname+"]" + crlf) |> ignore
                //sqlIndexSb.Append("END" + crlf) |> ignore

                //sqlIndexSb.Append("IF NOT EXISTS(SELECT object_id FROM [sys].[objects] WHERE name='Constraint_"+fullname+"')" + crlf) |> ignore
                //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
                //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" ADD  CONSTRAINT [Constraint_"+fullname+"] DEFAULT('-') FOR ["+fname+"]" + crlf) |> ignore
                //sqlIndexSb.Append("END" + crlf) |> ignore
                //sqlIndexSb.Append("" + crlf) |> ignore
                //sqlIndexSb.Append("IF EXISTS(SELECT * FROM SYSINDEXES WHERE name='UniqueNonclustered_"+fullname+"')" + crlf) |> ignore
                //sqlIndexSb.Append("BEGIN" + crlf) |> ignore
                //sqlIndexSb.Append(" ALTER TABLE "+transobj.name+" DROP  CONSTRAINT [UniqueNonclustered_"+fullname+"]" + crlf) |> ignore
                //sqlIndexSb.Append("END" + crlf) |> ignore

                //let s = " ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf
                //sb.Append(" ALTER TABLE " + transobj.name + " DROP  CONSTRAINT [Constraint_" + fullname + "]" + crlf) |> ignore

                ()))

    | _ -> ()

    sqls

    ()