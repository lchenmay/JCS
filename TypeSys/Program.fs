module TypeSys.Program

open System
open System.IO

open TypeSys.Common

open Util.Db

open Loadcfg

let run() = 

    Console.OutputEncoding <- System.Text.Encoding.Unicode
    let output (s:string) = Console.WriteLine s

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

    let mutable target = 0

    //target <- 5 // CTC Develop
    //target <- 6 // JCS
    target <- 10 // Game
    //let target = 11 // GNexts
    //target <- 15 // J

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
    | 10 ->
        {   ns = "Shared"
            rdbms = Rdbms.PostgreSql
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
        {   ns = "Shared"
            rdbms = Rdbms.SqlServer
            dbName = "JCS"
            donmainName = "jcatsys.com"
            conn = "server=127.0.0.1; user=sa; database=JCS"
            mainDir = @"C:\Dev\JCS\Shared"
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
    |> CodeRobot.go output

    BizLogics.CodeRobot.run()

run()

Util.Runtime.halt output "" ""
