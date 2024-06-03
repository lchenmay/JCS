module TypeSys.Program

open System

open TypeSys.CodeRobot

Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s

@"C:\Dev\JCS\BizShared\Types.fs"
|> TypeSys.FSharp.go output

let target = 1

match target with
| 0 -> 
    {   ns = "BizType"
        mainDir = @"C:\Dev\GECO2024\WebService\BizType"
        JsDir = @"C:\Dev\GECO2024\WebFrontend\Vue\src\lib\gfuns\robot" }
| _ -> 
    {   ns = "BizShared"
        mainDir = @"C:\Dev\JCS\BizShared"
        JsDir = @"C:\Dev\JCS\BizShared" }
|> CodeRobot.go output

Util.Runtime.halt output "" ""
