module TypeSys.Program

open System

open TypeSys.CodeRobot

Console.OutputEncoding <- System.Text.Encoding.Unicode
let output (s:string) = Console.WriteLine s

@"C:\Dev\GECO2024\WebService\BizType\Types.fs"
|> TypeSys.FSharp.go output

{   mainDir = @"C:\Dev\GECO2024\WebService\BizType"
    JsDir = @"C:\Dev\GECO2024\WebFrontend\Vue\src\lib\gfuns\robot" }
|> CodeRobot.go output

Util.Runtime.halt output "" ""
