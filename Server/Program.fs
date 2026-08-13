module Server.Launching

open System
open System.Text
open System.IO
open System.Diagnostics

open UtilKestrel.Common

let private output (text: string) = Console.WriteLine text

[<EntryPoint>]
let main argv =

    JCS.BizLogics.Launcher.launch()

    Util.Runtime.halt output "" ""

    0
