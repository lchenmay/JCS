module TypeSys.Program

open System
open System.IO

open TypeSys.Common

open Util.Db

open Loadcfg

Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s

let target__config target = 

    @"C:\Dev\JCS\BizShared\Types.fs"
    |> TypeSys.FSharp.go output

    let pwd = "jjsjd2VSd$"

    //let cfgPath = loadConfigFilePath "set.json"
    //let cfgData = cfgPath |> readRobotConfigs

    //let mutable target = "0"

    //cfgData.Add(
    //    "15",
    //    {   ns = "Shared"
    //        rdbms = Rdbms.SqlServer
    //        dbName = "J"
    //        donmainName = ""
    //        conn = "server=127.0.0.1; user=sa; database=J"
    //        mainDir = @"C:\Dev\J\Shared"
    //        JsDir = @"C:\Dev\J\vscode\src\lib\shared" })
    //cfgData.Add(
    //    "10", 
    //    {   ns = "Shared"
    //        rdbms = Rdbms.PostgreSql
    //        dbName = "Game"
    //        donmainName = ""
    //        conn = 
    //            [|  "Host=localhost"
    //                ";Username=game"
    //                ";Password=" + pwd
    //                ";Database=JCS" |]
    //            |> String.Concat
    //        mainDir = @"C:\Dev\Game\Shared"
    //        JsDir = @"C:\Dev\Game\vscode\src\lib\shared" })
    //cfgData.Add(
    //    "0",  
    //    {   ns = "Shared"
    //        rdbms = Rdbms.PostgreSql
    //        dbName = "CTC"
    //        donmainName = "cpto.cc"
    //        conn = 
    //            [|  "Host=localhost"
    //                ";Username=postgres"
    //                ";Password=" + pwd
    //                ";Database=CTC" |]
    //            |> String.Concat
    //        mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
    //        JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\ctc" })
    //cfgData.Add(
    //    "5",  
    //    {   ns = "Shared"
    //        rdbms = Rdbms.SqlServer
    //        dbName = "CTC"
    //        donmainName = "cpto.cc"
    //        conn = "server=127.0.0.1; user=sa; database=CTC"
    //        mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
    //        JsDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\vscode\src\lib\shared" })
    //cfgData.Add(
    //    "6",  
    //    {   ns = "Shared"
    //        rdbms = Rdbms.PostgreSql
    //        dbName = "JCS"
    //        donmainName = "jcatsys.com"
    //        conn = 
    //            [|  "Host=localhost"
    //                ";Username=postgres"
    //                ";Password=" + pwd
    //                ";Database=JCS" |]
    //            |> String.Concat
    //        mainDir = @"C:\Dev\JCS\Shared"
    //        JsDir = @"C:\Dev\JCS\vscode\src\lib\shared" })
    //cfgData.Add(
    //    "8",  
    //    {   ns = "Shared"
    //        rdbms = Rdbms.SqlServer
    //        dbName = "GenVI"
    //        donmainName = ""
    //        conn = "server=127.0.0.1; user=sa; database=GenVI"
    //        mainDir = @"C:\Dev\DevCoop\GenVI\Shared"
    //        JsDir = @"C:\Dev\DevCoop\GenVI\vscode\src\lib\shared" })
    //cfgData.Add(
    //    "1",  
    //    {   ns = "Shared"
    //        rdbms = Rdbms.SqlServer
    //        dbName = "GCHAIN"
    //        donmainName = "gcha.in"
    //        conn = "server=127.0.0.1; user=sa; database=GCHAIN"
    //        mainDir = @"C:\Dev\GCHAIN2024\GChainVsOpen\Shared"
    //        JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\gchain" })
    //cfgData.Add(
    //    "2",  
    //    {   ns = "BizType"
    //        rdbms = Rdbms.SqlServer
    //        dbName = "Personal"
    //        donmainName = "sdchen.xyz"
    //        conn = "server=127.0.0.1; user=sa; database=Personal"
    //        mainDir = @"C:\Dev\Personal\VisualStudio\BizType"
    //        JsDir = @"C:\Dev\Personal\VSCode\src" })
    //cfgData.Add(
    //    "_",  
    //    {   ns = "BizShared"
    //        rdbms = Rdbms.SqlServer
    //        dbName = ""
    //        donmainName = "jcatsys.com"
    //        conn = ""
    //        mainDir = @"C:\Dev\JCS\BizShared"
    //        JsDir = @"C:\Dev\JCS\BizShared" })

    //let main =
    //    target <- chooseProject cfgData target
    //    cfgData[target] |> CodeRobot.go output


    match target with
    | 7 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "J7"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=J7"
            mainDir = @"C:\Dev\J-7\Shared"
            JsDir = @"C:\Dev\J-7\vscode\src\lib\shared" }
    | 9 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "Game"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=Game"
            mainDir = @"C:\Dev\Game\Shared"
            JsDir = @"C:\Dev\Game\vscode\src\lib\shared" }
    | 11 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "GNexts"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=GNexts"
            mainDir = @"C:\Dev\GNexts\Shared"
            JsDir = @"C:\Dev\GNexts\vscode\src\lib\shared" }
    | 15 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "J"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=J"
            mainDir = @"C:\Dev\J\Shared"
            JsDir = @"C:\Dev\J\vscode\src\lib\shared" }
    | 16 ->
        {   ns = "Studio.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "Studio"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=Studio"
            mainDir = @"C:\Dev\Studio\Studio.Shared"
            JsDir = @"C:\Dev\Studio\vscode\src\lib\shared" }
    | 17 ->
        {   ns = "J.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "J"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=J"
            mainDir = @"C:\Dev\J\J.Shared"
            JsDir = @"C:\Dev\J\vscode\src\lib\shared" }
    | 18 ->
        {   ns = "FA.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "FA"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=FA"
            mainDir = @"C:\Dev\FA\FA.Shared"
            JsDir = @"C:\Dev\FA\vscode\src\lib\shared" }
    | 19 ->
        {   ns = "JA.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "JA"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=JA"
            mainDir = @"C:\Dev\JA\JA.Shared"
            JsDir = @"C:\Dev\JA\vscode\src\lib\shared" }
    | 10 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "Game"
            donmainName = ""
            conn = 
                [|  "Host=localhost"
                    ";Username=game"
                    ";Password=" + pwd
                    ";Database=JCS" |]
                |> String.Concat
            mainDir = @"C:\Dev\Game\Shared"
            JsDir = @"C:\Dev\Game\vscode\src\lib\shared" }

    | 0 -> 
        {   ns = "Shared"
            rdbms = Rdbms.PostgreSql
            dbName = "CTC"
            donmainName = "cpto.cc"
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
            donmainName = "cpto.cc"
            conn = "server=127.0.0.1; user=sa; database=CTC"
            mainDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\Shared"
            JsDir = @"C:\Dev\GCHAIN2024\CrypTradeClubVsOpen\vscode\src\lib\shared" }
    | 6 ->
        {   ns = "JCS.Shared"
            rdbms = Rdbms.SqlServer
            dbName = "JCS"
            donmainName = "jcatsys.com"
            conn = "server=127.0.0.1; user=sa; database=JCS"
            mainDir = @"C:\Dev\JCS\JCS.Shared"
            JsDir = @"C:\Dev\JCS\vscode\src\lib\shared" }

    | 8 ->
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "GenVI"
            donmainName = ""
            conn = "server=127.0.0.1; user=sa; database=GenVI"
            mainDir = @"C:\Dev\DevCoop\GenVI\Shared"
            JsDir = @"C:\Dev\DevCoop\GenVI\vscode\src\lib\shared" }
    | 1 -> 
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "GCHAIN"
            donmainName = "gcha.in"
            conn = "server=127.0.0.1; user=sa; database=GCHAIN"
            mainDir = @"C:\Dev\GCHAIN2024\GChainVsOpen\Shared"
            JsDir = @"C:\Dev\GCHAIN2024\VsCodeOpen\src\lib\shared\gchain" }
    | 2 -> 
        {   ns = "BizType"
            rdbms = Rdbms.SqlServer
            dbName = "Personal"
            donmainName = "sdchen.xyz"
            conn = "server=127.0.0.1; user=sa; database=Personal"
            mainDir = @"C:\Dev\Personal\VisualStudio\BizType"
            JsDir = @"C:\Dev\Personal\VSCode\src" }
    | _ -> 
        {   ns = "BizShared"
            rdbms = Rdbms.SqlServer
            dbName = ""
            donmainName = "jcatsys.com"
            conn = ""
            mainDir = @"C:\Dev\JCS\BizShared"
            JsDir = @"C:\Dev\JCS\BizShared" }

let run () = 

    [|  
        6 // JCS
        //7 // J-7
        16 // studio
        17 // J
        18 // FA
        19 // JA
        //10 //Game
            |]
    |> Array.map target__config
    |> Array.iter(CodeRobot.go output)

run()
BizLogics.CodeRobot.run()

Util.Runtime.halt output "" ""
