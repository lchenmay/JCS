module TypeSys.Program

open System

open TypeSys.CodeRobot

open Npgsql

Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s

@"C:\Dev\JCS\BizShared\Types.fs"
|> TypeSys.FSharp.go output


(fun _ -> 

    let pwd = "jjsjd2VSd$"

    let connStr =
        [|  "Host=localhost"
            ";Username=postgres"
            ";Password=" + pwd
            ";Database=CTC" |]
        |> String.Concat

    let conn = new NpgsqlConnection(connStr)

    try
        conn.Open()
    with
    | ex -> 
        let s = ex.ToString()
        ()
    
    ()) ()




let target = 1

match target with
| 0 -> 
    {   ns = "Shared"
        dbName = "CTC"
        conn = "server=127.0.0.1; user=sa; database=CTC"
        mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
        JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\ctc" }
| 1 -> 
    {   ns = "Shared"
        dbName = "GCHAIN"
        conn = "server=127.0.0.1; user=sa; database=GCHAIN"
        mainDir = @"C:\Dev\GCHAIN2024\GChainVsOpen\Shared"
        JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\gchain" }
| 2 -> 
    {   ns = "BizType"
        dbName = "Personal"
        conn = "server=127.0.0.1; user=sa; database=Personal"
        mainDir = @"C:\Dev\Personal\VisualStudio\BizType"
        JsDir = @"C:\Dev\Personal\VSCode\src" }
| _ -> 
    {   ns = "BizShared"
        dbName = ""
        conn = ""
        mainDir = @"C:\Dev\JCS\BizShared"
        JsDir = @"C:\Dev\JCS\BizShared" }
|> CodeRobot.go output

Util.Runtime.halt output "" ""
