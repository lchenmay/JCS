module Server.Launching

open System
open System.Text
open System.IO
open System.Diagnostics

open Util.Cat
open Util.Perf
open Util.Runtime
open Util.Json
open Util.Http
open Util.HttpServer
open Util.Zmq
open Util.WebServer

open Shared.Types
open Shared.CustomMor

open UtilWebServer.Common
open UtilWebServer.SSR

open BizLogics.Common
open BizLogics.Init
open BizLogics.Branch
open BizLogics.SSR

open UtilWebServer.Server.Service

[<EntryPoint>]
let main argv =

    //let content,embedding = "content","embedding"
    //UtilWebServer.Open.Discord.sendMsg
    //    (fun (s:string) -> Console.WriteLine s)
    //    BizLogics.Discord.glitchClient 
    //    BizLogics.Discord.guildId 
    //    BizLogics.Discord.glitchReviewChannelId 
    //    //BizLogics.Discord.glitchVerifiedChannelId
    //    (content,embedding)

    init runtime

    runtime.echo <- echo
    runtime.h404o <- (fun _ ->  
        ssrPageHome 
        |> render (hash1,hash2) 
        |> bin__StandardResponse "text/html") |> Some
    runtime.wsHandler <- fun json ->
        match
            json
            |> json__Msgo with
        | Some msg ->
            match msg with
            | ApiRequest json ->

                let service = tryFindStrByAtt "service" json
                let api = tryFindStrByAtt "api" json
                
                branch service api json
                |> Json.Braket
                |> Some

            | Heartbeat -> None
            | _ -> Some empty
        | None -> Some empty
    
    startEngine runtime

    Util.Runtime.halt output "" ""

    0