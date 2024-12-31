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

open JCS.BizLogics.Common
open JCS.BizLogics.Branch
open JCS.BizLogics.Db


let hash1,hash2 = 
    runtime.host.vueDeployDir + "/index.html"
    |> vueIndexFile__hashes

let ssrPageHome = {
    title = "J-Cat Sys"
    desc = "Cross platform code automation solution"
    image = "https://jcatsys.com/file/35461232.png"
    url = runtime.host.url
    noscript = "" }

let r1 = str__regex @"\w+"

let echoUploadFile x =
    let req = x.req
    if req.path.Length = 1 then
        if req.path[0] = "upload" then
            
            let id = Interlocked.Increment FILE_metadata.id

            let owner = 0L
            let caption = 
                if req.headers.ContainsKey "Filename" then
                    req.headers["Filename"].Trim()
                else
                    ""
            let suffix = 
                let index = caption.LastIndexOf "."
                if index > 0 then
                    let s = caption.Substring (index + 1)
                    if s.Length <= 4 then
                        s.ToLower()
                    else
                        ""
                else
                    ""
            let desc = 
                if req.headers.ContainsKey "Desc" then
                    req.headers["Desc"].Trim()
                else
                    ""

            let suc =
                try
                    let filename = buildfilename id suffix
                    System.IO.File.WriteAllBytes(filename,req.body)
                    if System.IO.File.Exists filename then
                        match createFILE id (owner,caption,suffix,desc) with
                        | Some rcd -> 
                            runtime.data.files[rcd.ID] <- rcd
                            true
                        | None -> false
                    else
                        false
                with
                | ex -> false

            Suc x
        else 
            Fail((),x)
    else
        Fail((),x)

let echoDownloadFile x =
    let req = x.req
    if req.path.Length = 2 then
        if req.path[0] = "file" then
            match 
                req.path[1]
                |> regex_match (str__regex "\d+")
                |> parse_int64
                |> id__FILEo with
            | Some file -> 
                try 
                    let filename = buildfilename file.ID file.p.Suffix
                    x.rep <-
                        if File.Exists filename then
                            File.ReadAllBytes filename
                        else
                            [| |]
                        |> bin__StandardResponse ""
                        |> Some
                with
                | ex -> ()
            | None -> ()

            Suc x
        else 
            Fail((),x)
    else
        Fail((),x)


let echo (req:HttpRequest) = 
    let ip = req |> remote_ip
    if ip.StartsWith "127.0.0.1" = false then
        let p = pPLOG_empty()
        p.Request <- req.bin |> System.Text.Encoding.ASCII.GetString
        p.Ip <- ip
        UtilWebServer.Db.p__createRcd 
            p PLOG_metadata "echo" conn |> ignore

    match 
        { req = req; rep = None}
        |> Suc
        |> bind (hHomepage (fun _ -> ssrPageHome |> render (hash1,hash2) ""))
        |> bindFail echoUploadFile
        |> bindFail echoDownloadFile
        |> bindFail (hapi echoApiHandler branch) with
    | Suc x -> x.rep
    | Fail(x,e) -> None



