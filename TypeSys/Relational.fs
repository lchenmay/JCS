module TypeSys.Relational

open System
open System.IO

open System.Text.Json
open System.Collections.Generic

open UtilWebServer.Common
open UtilWebServer.Init

open Shared.OrmTypes
open Shared.OrmMor
open Shared.Types
open Shared.CustomMor
open Shared.Project

open TypeSys.Common

let conn = "server=127.0.0.1; user=sa; database=JCS"
let mainDir = @"C:\Dev\JCS\Shared"
let JsDir = @"C:\Dev\JCS\vscode\src\lib\shared"

let run() =

    let h = {
        data = ()
        zmq = false
        port = 5045
        conn = ""
        defaultHtml = "index.html"
        url = ""

        updateDatabase = true

        DiscordAppId = ""
        DiscordPubKey = ""
        DiscordSecret = ""
        DiscordRedirect = ""

        fsDir = @"C:\Dev\JCS\vscode\dist" }


    let runtime = BizLogics.Common.runtime

    BizLogics.Init.init runtime

    let pc = runtime.data.pcs[234346L]

    ()

