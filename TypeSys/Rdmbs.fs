module TypeSys.Rdmbs

open System
open System.IO
open System.Collections.Generic
open System.Data
open System.Data.SqlClient
open System.Text
open System.Text.RegularExpressions

open Util.Collection

open TypeSys.MetaType

type Rdbms = 
| NotAvailable
| SqlServer
| MySql
| PostgreSql

let sqlField f =
    let sort,fname,def,json = f

    let n = fname

    let s = 
        match def with
        | FieldDef.FK v -> "
        "
        | FieldDef.Caption length -> "NVARCHAR(" + length.ToString() + ") COLLATE Chinese_PRC_CI_AS"
        | FieldDef.Chars length -> "NVARCHAR(" + length.ToString() + ") COLLATE Chinese_PRC_CI_AS"
        | FieldDef.Link length -> "NVARCHAR(" + length.ToString() + ") COLLATE Chinese_PRC_CI_AS"
        | FieldDef.Text -> "NVARCHAR(MAX)"
        | FieldDef.Timestamp
        | FieldDef.Integer -> "BIGINT"
        | FieldDef.Float -> "FLOAT"
        | FieldDef.Boolean -> "BIT"
        | FieldDef.SelectLines v -> "INT"
        | FieldDef.TimeSeries -> "VARBINARY(MAX)"
        | _ -> ""

    "[" + n + "] " + s