module BizLogics.SSR

open System
open System.Text
open System.Collections.Generic
open System.Threading

open Util.Cat
open Util.Text
open Util.Bin
open Util.Perf
open Util.Json
open Util.Http
open Util.HttpServer
open Util.Zmq

open Shared.OrmTypes
open Shared.Types
open Shared.OrmMor
open Shared.CustomMor

open UtilWebServer.Common
open UtilWebServer.Api
open UtilWebServer.Json
open UtilWebServer.SSR

open Shared.OrmTypes
open Shared.OrmMor
open Shared.Types
open Shared.CustomMor

open BizLogics.Common
open BizLogics.Branch

let hash1,hash2 = 
    runtime.host.fsDir + "\\" + runtime.host.defaultHtml
    |> vueIndexFile__hashes

let ssrPageHome = {
    title = "cpto.cc"
    desc = "Social trading. Portfolio. Aggregated news and market data."
    image = "https://i.imgur.com/hzWYQow.png"
    url = runtime.host.url
    noscript = "" }

let r1 = str__regex @"\w+"

let echo req = 

    match 
        { req = req; rep = None}
        |> Suc
        //|> bindFail (hHomepage (fun _ -> ssrPageHome |> render((hash1,hash2))))
        |> bind (hapi echoApiHandler branch) with
    | Suc x -> x.rep
    | Fail(x,e) -> None
