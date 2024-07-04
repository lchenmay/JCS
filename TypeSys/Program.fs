﻿module TypeSys.Program

open System

open TypeSys.Common

open Util.Db

Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s

@"C:\Dev\JCS\BizShared\Types.fs"
|> TypeSys.FSharp.go output


let postgresqlconn = 

    let db = "CTC"
    let pwd = "jjsjd2VSd$"

    [|  "Host=localhost"
        ";Username=postgres"
        ";Password=" + pwd
        ";Database=" + db |]
    |> String.Concat

let target = 0

match target with
| 0 -> 
    {   ns = "Shared"
        rdbms = Rdbms.PostgreSql
        dbName = "CTC"
        conn = postgresqlconn
        mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
        JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\ctc" }
    //{   ns = "Shared"
    //    rdbms = Rdbms.SqlServer
    //    dbName = "CTC"
    //    conn = "server=127.0.0.1; user=sa; database=CTC"
    //    mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
    //    JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\ctc" }
| 1 -> 
    {   ns = "Shared"
        rdbms = Rdbms.SqlServer
        dbName = "GCHAIN"
        conn = "server=127.0.0.1; user=sa; database=GCHAIN"
        mainDir = @"C:\Dev\GCHAIN2024\GChainVsOpen\Shared"
        JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\gchain" }
| 2 -> 
    {   ns = "BizType"
        rdbms = Rdbms.SqlServer
        dbName = "Personal"
        conn = "server=127.0.0.1; user=sa; database=Personal"
        mainDir = @"C:\Dev\Personal\VisualStudio\BizType"
        JsDir = @"C:\Dev\Personal\VSCode\src" }
| _ -> 
    {   ns = "BizShared"
        rdbms = Rdbms.SqlServer
        dbName = ""
        conn = ""
        mainDir = @"C:\Dev\JCS\BizShared"
        JsDir = @"C:\Dev\JCS\BizShared" }
|> CodeRobot.go output

Util.Runtime.halt output "" ""
