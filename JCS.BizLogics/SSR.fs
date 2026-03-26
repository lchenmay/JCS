module JCS.BizLogics.SSR

open System
open System.IO
open System.Text
open System.Collections.Generic
open System.Threading

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Http

open Util.Cat
open Util.Text
open Util.Bin
open Util.Perf
open Util.Json
open Util.Http
open Util.HttpServer

open JCS.Shared.OrmTypes
open JCS.Shared.Types
open JCS.Shared.OrmMor
open JCS.Shared.CustomMor

open UtilKestrel.Types
open UtilKestrel.Api
open UtilKestrel.Json
open UtilKestrel.SSR
open UtilKestrel.File

open JCS.BizLogics.Common
open JCS.BizLogics.Branch
open JCS.BizLogics.Db

let ssrPageHome = {
    title = "J-Cat Sys"
    desc = "Cross platform code automation solution"
    image = "https://jcatsys.com/file/35461232.png"
    url = runtime.host.url
    noscript = "" }

let r1 = str__regex @"\w+"

let uploadBuffer = new Dictionary<int64,SortedDictionary<int,byte[]>>()

let plugin = ""

let pages = [|
    "/m"
    "/moments"
    "/admin" |]

let echo (scheme,api,reqBodyBin): byte[] = [||]
//let echo (hx:HttpContext) = 
//    let ip = req |> remote_ip
//    if ip.StartsWith "127.0.0.1" = false then
//        let p = pPLOG_empty()
//        p.Request <- req.bin |> System.Text.Encoding.ASCII.GetString
//        p.Ip <- ip
//        UtilKestrel.Db.p__createRcd 
//            p PLOG_metadata dbLoggero "echo" conn |> ignore
    
//    let vueDeployDir = runtime.host.req__vueDeployDir req
    
//    match 
//        { req = req; rep = None}
//        |> Suc
//        |> bind (homepage runtime.langs pages ssrPageHome vueDeployDir "")
//        |> bindFail (hMoment vueDeployDir)
//        |> bindFail (hSEO x__items "")
//        |> bindFail uploader
//        |> bindFail dnloader
//        |> bindFail (hapi echoApiHandler (branch req)) with
//    | Suc x -> x.rep
//    | Fail(x,e) -> None



