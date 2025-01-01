module JCS.BizLogics.Branch

open System
open System.Text
open System.Collections.Generic
open System.Threading

open Util.Cat
open Util.Text
open Util.Bin
open Util.CollectionModDict
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
open UtilWebServer.Server.Monitor
open UtilWebServer.Db

open JCS.Shared.OrmTypes
open JCS.Shared.OrmMor
open JCS.Shared.Types
open JCS.Shared.CustomMor

open JCS.BizLogics.Common
open JCS.BizLogics.Db

let branching x = 

    let bindx p = 
        x.proco <- Some p
        Suc x

    match x.service with
    | "public" -> 
        match x.api with
        | "ping" -> bindx apiPing
        | "auth" -> (fun x ->
            let session = (tryFindStrByAtt "session" x.json).Trim()
            if (tryFindStrByAtt "act" x.json).Trim() = "sign-out" then
                if runtime.sessions.ContainsKey session then
                    runtime.sessions.Remove session |> ignore
                [| ok |]
            else
                let key = (tryFindStrByAtt "key" x.json).Trim()
                if key = "080D6FB2-0946-487C-8982-180638AA042D" then

                    let eux = 
                        runtime.users.Values
                        |> Seq.toArray
                        |> Array.find(fun i ->  i.eu.p.AuthType = euAuthTypeEnum.Admin)
                
                    let session = (tryFindStrByAtt "session" x.json).Trim()
                    if runtime.sessions.ContainsKey session then
                        runtime.sessions.Remove session |> ignore

                    let s = UtilWebServer.Session.user__session runtime.sessions eux

                    [|  ok
                        "session", s.session |> str__json
                        "eux", eux |> EuComplex__json |]
                else
                    er Er.Unauthorized) |> bindx
        | "moment" -> (fun x -> 
            let id = (tryFindNumByAtt "id" x.json).Trim() |> parse_int64
            if runtime.data.mxs.ContainsKey id then
                runtime.data.mxs[id] 
                |> MomentComplex__json
                |> wrapOk "data"
            else
                er Er.InvalideParameter) |> bindx
        | "moments" -> (fun x -> 
            runtime.data.mxs.Values
            |> Array.map MomentComplex__json
            |> wrapOkAry) |> bindx
        | "msg" -> (fun x -> 
            let name = (tryFindStrByAtt "name" x.json).Trim()
            let email = (tryFindStrByAtt "email" x.json).Trim()
            let msg = (tryFindStrByAtt "msg" x.json).Trim()
            if name.Length * email.Length * msg.Length > 0 then
                let p = pBOOK_empty()
                p.Caption <- name
                p.Email <- email
                p.Message <- msg
                match p__createRcd p BOOK_metadata dbLoggero "/api/public/msg" conn with
                | Some rcd -> [| ok |]
                | None -> er Er.Internal
            else
                er Er.InvalideParameter) |> bindx
        | _ -> Fail(Er.ApiNotExists,x)
    | "eu" -> 
        match x.api with
        | "moment" -> (fun x -> 
            let id = (tryFindNumByAtt "id" x.json).Trim() |> parse_int64
            let title = (tryFindStrByAtt "title" x.json).Trim()
            let summary = (tryFindStrByAtt "summary" x.json).Trim()
            let content = (tryFindStrByAtt "content" x.json).Trim()
            if runtime.data.mxs.ContainsKey id then
                if updateRcd "/api/eu/moment" conn MOMENT_metadata dbLoggero (fun p -> 
                    p.Title <- title
                    p.Summary <- summary
                    p.FullText <- content) runtime.data.mxs[id].m then
                    [| ok |]
                else
                    er Er.Internal
            else
                let p = pMOMENT_empty()
                p.Title <- title
                p.Summary <- summary
                p.FullText <- content
                p.Type <- momentTypeEnum.Original
                match p__createRcd p MOMENT_metadata dbLoggero "/api/public/msg" conn with
                | Some rcd -> 
                    runtime.data.mxs[rcd.ID] <- {
                        fbxs = [| |]
                        m = rcd }
                    [| ok |]
                | None -> er Er.Internal) |> bindx
        | "files" -> (fun x -> 
            let items = runtime.data.files.Values
            if items.Length > 200 then
                Array.sub items 0 200
            else
                items 
            |> Array.map checkFileThumbnail
            |> Array.map FILE__json
            |> wrapOkAry) |> bindx
        | _ -> Fail(Er.ApiNotExists,x)
    | "admin" -> 
        match x.api with
        | "plogs" -> (fun x -> 
            let metadata = PLOG_metadata
            match 
                "ORDER BY ID DESC"
                |> Util.Orm.loadall conn
                    (metadata.table,metadata.fieldorders(),metadata.db__rcd) with
            | Some items ->
                if items.Length > 200 then
                    Array.sub items 0 200
                else
                    items 
                |> Array.map(fun rcd -> 
                    let clone = PLOG_clone rcd
                    clone.p.Request <- clone.p.Request.Replace(crlf,"<br>")
                    clone)
                |> Array.map PLOG__json
            | None -> [| |]
            |> wrapOkAry) |> bindx
        | "books" -> (fun x -> 
            let metadata = BOOK_metadata
            match 
                "ORDER BY ID DESC"
                |> Util.Orm.loadall conn
                    (metadata.table,metadata.fieldorders(),metadata.db__rcd) with
            | Some items ->
                if items.Length > 200 then
                    Array.sub items 0 200
                else
                    items 
                |> Array.map(fun rcd -> 
                    let clone = BOOK_clone rcd
                    clone.p.Message <- clone.p.Message.Replace(crlf,"<br>")
                    clone)
                |> Array.map BOOK__json
            | None -> [| |]
            |> wrapOkAry) |> bindx
        | "monitorPerf" -> bindx apiMonitorPerf
        | "monitorServer" -> bindx (fun x -> apiMonitor x.runtime)
        | _ -> Fail(Er.ApiNotExists,x)
    | "open" -> Fail(Er.ApiNotExists,x)
    | _ -> Fail(Er.ApiNotExists,x)


let branch service api json = 

    use cw = new CodeWrapper("Server.WebHandler.branch")

    let mutable x = incoming__x runtime service api "" json
    
    //match service with
    //| "eu" ->
    //    x <- 
    //        x 
    //        |> bind checkSession
    //        |> bind checkSessionEu
    //| "admin" ->
    //    x <- 
    //        x 
    //        |> bind checkSession
    //        |> bind checkSessionEu
    //| "open" ->
    //    x <- 
    //        x 
    //        |> bind checkSession
    //        |> bind checkSessionEu

    //| _ -> ()

    runApi branching x
