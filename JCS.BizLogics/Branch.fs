module JCS.BizLogics.Branch

open System
open System.Text
open System.IO
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

open JCS.Shared.OrmTypes
open JCS.Shared.Types
open JCS.Shared.OrmMor
open JCS.Shared.CustomMor

open UtilKestrel.Types
open UtilKestrel.Ctx
open UtilKestrel.Api
open UtilKestrel.Json
open UtilKestrel.SSR
open UtilKestrel.Server

open JCS.Shared.OrmTypes
open JCS.Shared.OrmMor
open JCS.Shared.Types
open JCS.Shared.CustomMor

open JCS.BizLogics.Common
open JCS.BizLogics.Db

let branching (x:X) = 

    let bindx p = 
        x.Struct.proco <- Some p
        Suc x

    match x.Struct.scheme with
    //| "public" -> 
    //    match x.Struct.api with
    //    | "ping" -> bindx apiPing
    //    | "auth" -> (fun x ->
    //        let session = (tryFindStrByAtt "session" x.json).Trim()
    //        if (tryFindStrByAtt "act" x.Json).Trim() = "sign-out" then
    //            if runtime.sessions.ContainsKey session then
    //                runtime.sessions.Remove session |> ignore
    //            [| ok |]
    //        else
    //            let key = (tryFindStrByAtt "key" x.json).Trim()
    //            if key = "080D6FB2-0946-487C-8982-180638AA042D" then

    //                let eux = 
    //                    runtime.users.Values
    //                    |> Seq.toArray
    //                    |> Array.find(fun i ->  i.eu.p.AuthType = euAuthTypeEnum.Admin)
                
    //                let session = (tryFindStrByAtt "session" x.json).Trim()
    //                if runtime.sessions.ContainsKey session then
    //                    runtime.sessions.Remove session |> ignore

    //                let s = UtilKestrel.Session.user__session runtime.sessions eux

    //                [|  ok
    //                    "session", s.session |> str__json
    //                    "eux", eux |> EuComplex__json |]
    //            else
    //                er Er.Unauthorized) |> bindx
    //    | "moment" -> (fun x -> 
    //        let id = (tryFindNumByAtt "id" x.json).Trim() |> parse_int64
    //        if runtime.data.mxs.ContainsKey id then
    //            runtime.data.mxs[id] 
    //            |> MomentComplex__json
    //            |> wrapOk "data"
    //        else
    //            er Er.InvalideParameter) |> bindx
    //    | "moments" -> (fun x -> 
    //        let mutable items = 
    //            let items = runtime.data.mxs.Values
    //            let tag = (tryFindStrByAtt "tag" x.json).Trim().ToLower()
    //            if tag.Length > 0 then
    //                match tag with
    //                | _ -> items |> Array.filter(fun i -> i.m.p.Tags.ToLower().Contains tag)
    //            else
    //                items

    //        items
    //        |> Array.sortByDescending(fun i -> i.m.Updatedat)
    //        |> Array.map MomentComplex__json
    //        |> wrapOkAry) |> bindx
    //    | "homepage" -> (fun x -> 
    //        let map id = runtime.data.mxs[id] |> MomentComplex__json
    //        [|  ok
    //            "home",map 54864677L
    //            "FP", [| 
    //                map 54864683L |] |> Json.Ary
    //            "IA", [| 
    //                map 54864679L |] |> Json.Ary
    //            "CAT", [| 
    //                map 54864684L
    //                map 54864685L
    //                map 54864686L
    //                map 54864687L
    //                map 54864688L
    //                map 54864689L
    //                map 54864690L
    //                map 54864680L |] |> Json.Ary
    //            "Service", [| 
    //                map 54864678L |] |> Json.Ary |]) |> bindx
    //    | "msg" -> (fun x -> 
    //        let name = (tryFindStrByAtt "name" x.json).Trim()
    //        let email = (tryFindStrByAtt "email" x.json).Trim()
    //        let msg = (tryFindStrByAtt "msg" x.json).Trim()
    //        if name.Length * email.Length * msg.Length > 0 then
    //            let p = pBOOK_empty()
    //            p.Caption <- name
    //            p.Email <- email
    //            p.Message <- msg
    //            match p__createRcd p BOOK_metadata dbLoggero "/api/public/msg" conn with
    //            | Some rcd -> [| ok |]
    //            | None -> er Er.Internal
    //        else
    //            er Er.InvalideParameter) |> bindx
    //    | _ -> Fail(Er.ApiNotExists,x)
    //| "eu" -> 
    //    match x.api with
    //    | "moment" -> 
    //        createUpdateDeleteAct "/api/eu/moment" conn MOMENT_metadata dbLoggero Er.Internal 
    //            runtime.data.mxs.TryGet None (fun mx -> mx.m) json__pMOMENTo
    //            (fun p pIncoming -> 
    //                p.Title <- pIncoming.Title
    //                p.Summary <- pIncoming.Summary
    //                p.PreviewImgUrl <- pIncoming.PreviewImgUrl
    //                p.FullText <- pIncoming.FullText
    //                true)
    //            None 
    //            (Some(fun p pIncoming -> 
    //                p.Title <- pIncoming.Title
    //                p.Summary <- pIncoming.Summary
    //                p.PreviewImgUrl <- pIncoming.PreviewImgUrl
    //                p.FullText <- pIncoming.FullText
    //                p.Type <- momentTypeEnum.Original))
    //            (Some(fun rcd -> runtime.data.mxs[rcd.ID] <- { fbxs = [| |]; m = rcd }))
    //            |> bindx
    //    | "file" ->
    //        createUpdateDeleteAct "/api/eu/file" conn FILE_metadata dbLoggero Er.Internal 
    //            runtime.data.files.TryGet None (fun i -> i) json__pFILEo
    //            (fun p pIncoming -> 
    //                p.Caption <- pIncoming.Caption
    //                p.Desc <- pIncoming.Desc
    //                true)
    //            (Some(fun file -> 
    //                try 
    //                    let filename = buildfilename runtime.host.fsDir file.ID file.p.Suffix
    //                    if File.Exists filename then
    //                        File.Delete filename
    //                with
    //                | ex -> ()
    //                runtime.data.files.Remove file.ID))
    //            (Some(fun p pIncoming -> 
    //                p.Caption <- pIncoming.Caption
    //                p.Desc <- pIncoming.Desc))
    //            (Some(fun rcd -> runtime.data.files[rcd.ID] <- rcd ))
    //            |> bindx
    //    | "files" -> (fun x -> 
    //        let items = runtime.data.files.Values
    //        if items.Length > 200 then
    //            Array.sub items 0 200
    //        else
    //            items 
    //        |> Array.sortByDescending(fun i -> i.ID)
    //        |> Array.map (checkFileThumbnail conn runtime.host.fsDir FILE_metadata dbLoggero
    //                        (fun rcd -> rcd.p.Thumbnail,rcd.p.Suffix,rcd.p.Size)
    //                        (fun file size -> file.p.Size <- size)
    //                        (fun file thumbnail -> file.p.Thumbnail <- thumbnail))
    //        |> Array.map FILE__json
    //        |> wrapOkAry) |> bindx
    //    | _ -> Fail(Er.ApiNotExists,x)
    //    | _ -> Fail(Er.ApiNotExists,x)
    //| "admin" -> 
    //    match x.api with
    //    | "plogs" -> (fun x -> 
    //        let metadata = PLOG_metadata
    //        match "ORDER BY ID DESC" |> Util.Orm.loadall conn (metadata.table,metadata.fieldorders(),metadata.db__rcd) with
    //        | Some items ->
    //            items
    //            |> Array.filter(fun i -> (UtilKestrel.PageLog.req__fromo i.p.Request).IsSome)
    //            |> (fun items -> if items.Length > 200 then Array.sub items 0 200 else items)
    //            |> Array.map(fun rcd -> UtilKestrel.PageLog.req__json (rcd.p.Ip,rcd.Createdat,rcd.p.Request))
    //        | None -> [| |]
    //        |> wrapOkAry) |> bindx
    //    | "books" -> (fun x -> 
    //        let metadata = BOOK_metadata
    //        match 
    //            "ORDER BY ID DESC"
    //            |> Util.Orm.loadall conn
    //                (metadata.table,metadata.fieldorders(),metadata.db__rcd) with
    //        | Some items ->
    //            if items.Length > 200 then
    //                Array.sub items 0 200
    //            else
    //                items 
    //            |> Array.map(fun rcd -> 
    //                let clone = BOOK_clone rcd
    //                clone.p.Message <- clone.p.Message.Replace(crlf,"<br>")
    //                clone)
    //            |> Array.map BOOK__json
    //        | None -> [| |]
    //        |> wrapOkAry) |> bindx
    //    | "monitorPerf" -> bindx apiMonitorPerf
    //    | "monitorServer" -> bindx (fun x -> apiMonitor x.runtime)
    //    | _ -> Fail(Er.ApiNotExists,x)
    //| "open" -> Fail(Er.ApiNotExists,x)
    | _ -> Fail(Er.ApiNotExists,x)


let branch (x:X) = 

    use cw = new CodeWrapper("Server.WebHandler.branch")
    
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

    x.Struct.contentType <- "application/json; charset=utf-8"
    x.Struct.rep <-
        runApi branching x 
        |> Braket
        |> json__strFinal
        |> Encoding.UTF8.GetBytes
