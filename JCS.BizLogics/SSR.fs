module JCS.BizLogics.SSR

open System
open System.IO
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

open JCS.Shared.OrmTypes
open JCS.Shared.Types
open JCS.Shared.OrmMor
open JCS.Shared.CustomMor

open UtilWebServer.Common
open UtilWebServer.Api
open UtilWebServer.Json
open UtilWebServer.SSR
open UtilWebServer.Server.File

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

let uploader = 
    let postCreateo = 
        Some(fun (rcd:FILE) -> 
        runtime.data.files[rcd.ID] <- rcd)
    
    let setter (p:pFILE) (owner:int64,caption:string,suffix:string,desc:string,size:int) =
        p.Caption <- caption
        p.Owner <- owner
        p.Suffix <- suffix
        p.Desc <- desc
        p.Size <- size

    echoUploadFile 
        uploadBuffer runtime.data.files.TryGet
        (fun rcd -> rcd.p.Suffix)
        runtime.host.fsDir conn FILE_metadata dbLoggero 
        setter postCreateo

let dnloader = 
    echoDownloadFile runtime.host.fsDir FILE_metadata 
        (fun rcd -> rcd.p.Suffix)

let plugin = ""

let x__items (x:ReqRep) = 
    runtime.data.mxs.Values
    |> Array.filter(fun mx -> mx.m.p.Title.Length * mx.m.p.Summary.Length * mx.m.p.FullText.Length > 0)
    |> Array.map(fun mx -> "https://" + x.req.domainname + "/m/" + mx.m.ID.ToString())        

let hMoment vueDeployDir (x:ReqRep) =
    let req = x.req
    if req.path.Length = 2 then
        if req.path[0] = "m" then
            match 
                req.path[1]
                |> parse_int64
                |> runtime.data.mxs.TryGet with
            | Some mx -> 
                x.rep <-
                    {
                        title = "J-CAT SYS LLC - " + mx.m.p.Title 
                        desc = mx.m.p.Summary
                        image = 
                            if mx.m.p.PreviewImgUrl.Length > 0 then
                                mx.m.p.PreviewImgUrl
                            else
                                "https://" + req.domainname + "/file/35461232.png"
                        url = "https://" + req.domainname + "/m/" + mx.m.ID.ToString()
                        noscript = "" }
                    |> render (vueIndexFile__hashes(vueDeployDir + "/index.html")) plugin
                    |> bin__StandardResponse "text/html"
                    |> Some
                Suc x
            | None -> Fail((),x)
        else
            Fail((),x)
    else
        Fail((),x)

let pages = [|
    "admin" |]

let echo (req:HttpRequest) = 
    let ip = req |> remote_ip
    if ip.StartsWith "127.0.0.1" = false then
        let p = pPLOG_empty()
        p.Request <- req.bin |> System.Text.Encoding.ASCII.GetString
        p.Ip <- ip
        UtilWebServer.Db.p__createRcd 
            p PLOG_metadata dbLoggero "echo" conn |> ignore

    let vueDeployDir = runtime.host.req__vueDeployDir req
    
    match 
        { req = req; rep = None}
        |> Suc
        |> bind (homepage runtime.langs pages ssrPageHome vueDeployDir "")
        |> bindFail (hMoment vueDeployDir)
        |> bindFail (hSEO x__items)
        |> bindFail uploader
        |> bindFail dnloader
        |> bindFail (hapi echoApiHandler (branch req)) with
    | Suc x -> x.rep
    | Fail(x,e) -> None



