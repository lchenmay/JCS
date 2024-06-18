module TypeSys.Program

open System

open TypeSys.CodeRobot

Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s


if false then

    let wsHandler incoming =

        None

    let engine = 
        Util.WebServer.prepEngine 
            output
            None
            ""
            ""
            ()
            wsHandler
            8000
    Util.WebServer.startEngine engine

    (fun _ -> engine.ToString() |> output)
    |> Util.Concurrent.asyncCyclerInterval 1000

    Util.Runtime.halt output "" ""




@"C:\Dev\JCS\BizShared\Types.fs"
|> TypeSys.FSharp.go output

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
