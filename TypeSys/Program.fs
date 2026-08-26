module TypeSys.Program

open System
open System.IO

open TypeSys.Config

open Util.Db
open Util.OrmDb
open Util.Rdbms


Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s

let target__config target = 

    let pwd = "jjsjd2VSd$"

    match target with
    | 7 ->
        {   ns = "J7.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "J7"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=J7"
            mainDir = @"C:\Dev\J7\J7.Shared"
            JsDir = @"C:\Dev\J7\vscode\src\lib\shared" }
    | 9 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "Game"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=Game"
            mainDir = @"C:\Dev\Game\Shared"
            JsDir = @"C:\Dev\Game\vscode\src\lib\shared" }
    | 11 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "GNexts"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=GNexts"
            mainDir = @"C:\Dev\GNexts\Shared"
            JsDir = @"C:\Dev\GNexts\vscode\src\lib\shared" }
    | 15 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "J"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=J"
            mainDir = @"C:\Dev\J\Shared"
            JsDir = @"C:\Dev\J\vscode\src\lib\shared" }
    | 16 ->
        {   ns = "Studio.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "Studio"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=Studio"
            mainDir = @"C:\Dev\Studio\Studio.Shared"
            JsDir = @"C:\Dev\Studio\vscode\src\lib\shared" }
    | 17 ->
        {   ns = "J.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "J"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=J"
            mainDir = @"C:\Dev\J\J.Shared"
            JsDir = @"C:\Dev\J\vscode\src\lib\shared" }
    | 18 ->
        {   ns = "FA.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "FA"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=FA"
            mainDir = @"C:\Dev\FA\FA.Shared"
            JsDir = @"C:\Dev\FA\vscode\src\lib\shared" }
    | 19 ->
        {   ns = "JA.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "JA"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=JA"
            mainDir = @"C:\Dev\JA\JA.Shared"
            JsDir = @"C:\Dev\JA\vscode\src\lib\shared" }
    | 20 ->
        {   ns = "Aiarwa.Shared"
            rdbms = Rdbms.PostgreSql
            dbName = "Aiarwa"
            domainName = "wigaoil.com"
            conn = "Host=localhost;Port=5432;Database=aiarwa;Username=aiarwa;Password=e2TpqcaTEYLfkvFMkc"
            mainDir = @"C:\Dev\Aiarwa\Aiarwa.Shared"
            JsDir = @"C:\Dev\Aiarwa\vscode\src\lib\shared" }
    | 10 ->
        {   ns = "Game.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "Game"
            domainName = ""
            conn = 
                [|  "Host=localhost"
                    ";Username=game"
                    ";Password=" + pwd
                    ";Database=Game" |]
                |> String.Concat
            mainDir = @"C:\Dev\Game\Game.Shared"
            JsDir = @"C:\Dev\Game\vscode\src\lib\shared" }

    | 0 -> 
        {   ns = "Shared"
            rdbms = Rdbms.PostgreSql
            dbName = "CTC"
            domainName = "cpto.cc"
            conn = 
                [|  "Host=localhost"
                    ";Username=postgres"
                    ";Password=" + pwd
                    ";Database=CTC" |]
                |> String.Concat
            mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
            JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\ctc" }
    | 5 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "CTC"
            domainName = "cpto.cc"
            conn = "server=127.0.0.1; user=sa; database=CTC"
            mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
            JsDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\vscode\src\lib\shared" }
    | 6 ->
        {   ns = "JCS.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "JCS"
            domainName = "jcatsys.com"
            conn = "server=127.0.0.1; user=sa; database=JCS"
            mainDir = @"C:\Dev\JCS\JCS.Shared"
            JsDir = @"C:\Dev\JCS\vscode\src\lib\shared" }

    | 8 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "GenVI"
            domainName = ""
            conn = "server=127.0.0.1; user=sa; database=GenVI"
            mainDir = @"C:\Dev\DevCoop\GenVI\Shared"
            JsDir = @"C:\Dev\DevCoop\GenVI\vscode\src\lib\shared" }
    | 1 -> 
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "GCHAIN"
            domainName = "gcha.in"
            conn = "server=127.0.0.1; user=sa; database=GCHAIN"
            mainDir = @"C:\Dev\GCHAIN2024\GChainVsOpen\Shared"
            JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\gchain" }
    | 2 -> 
        {   ns = "BizType"
            rdbms = Rdbms.SqlServer
            dbName = "Personal"
            domainName = "sdchen.xyz"
            conn = "server=127.0.0.1; user=sa; database=Personal"
            mainDir = @"C:\Dev\Personal\VisualStudio\BizType"
            JsDir = @"C:\Dev\Personal\VSCode\src" }
    | _ -> 
        {   ns = "BizShared"
            rdbms = Rdbms.SqlServer
            dbName = ""
            domainName = "jcatsys.com"
            conn = ""
            mainDir = @"C:\Dev\JCS\BizShared"
            JsDir = @"C:\Dev\JCS\BizShared" }

let runMultiple exeDir = 

    [|  
        //6 // JCS
        //7 // J-7
        20 // Aiarwa
        //16 // studio
        //17 // J
        //18 // FA
        //19 // JA
        //10 //Game
            |]
    |> Array.map target__config
    |> Array.iter(CodeRobot.go output exeDir)

System.AppContext.BaseDirectory.TrimEnd('\\','/') |> runMultiple
//Directory.GetCurrentDirectory() |> JCS.BizLogics.CodeRobot.runAll

Util.Runtime.halt output "" ""
