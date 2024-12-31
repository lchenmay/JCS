module Server.Launching

open System
open System.Text
open System.IO
open System.Diagnostics

open UtilWebServer.Common

[<EntryPoint>]
let main argv =

    JCS.BizLogics.Launcher.launch()

    Util.Runtime.halt output "" ""

    0
