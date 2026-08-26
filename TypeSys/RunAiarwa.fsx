#I @"c:\Dev\JCS\TypeSys\bin\Debug\net10.0"
#r "Util.dll"
#r "UtilCrossPlatform.dll"
#r "TypeSys.dll"
open System
open TypeSys.Config
open TypeSys.CodeRobot
open Util.Rdbms

let sharedDir = @"c:\Dev\Aiarwa\Aiarwa.Shared"
let jsDir = @"c:\Dev\Aiarwa\vscode\src\lib\shared"

let config: RobotConfig =
    { ns = "Aiarwa.Shared"
      rdbms = Rdbms.PostgreSql
      dbName = "aiarwa"
      domainName = ""
      conn = ""
      mainDir = sharedDir
      JsDir = jsDir }

printfn "Generating Aiarwa ORM into %s" sharedDir
go (fun t -> printfn "%s" t) @"c:\Dev\JCS\TypeSys\bin\Debug\net10.0" config
printfn "Done."
