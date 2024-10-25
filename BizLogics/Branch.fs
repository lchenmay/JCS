module BizLogics.Branch

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

open Shared.OrmTypes
open Shared.Types
open Shared.OrmMor
open Shared.CustomMor
open Shared.Project

open UtilWebServer.Common
open UtilWebServer.Api
open UtilWebServer.Json
open UtilWebServer.SSR
open UtilWebServer.Server.Monitor

open Shared.OrmTypes
open Shared.OrmMor
open Shared.Types
open Shared.CustomMor

open BizLogics.Common
open BizLogics.Db

let branching x = 

    let bindx p = 
        x.proco <- Some p
        Suc x

    match x.service with
    | "public" -> 
        match x.api with
        | "ping" -> apiPing |> bindx
        | "perf" -> apiMonitorPerf |> bindx
        | "reloadProjects" -> (fun x -> runtime.data.projectxs.Values |> apiList ProjectComplex__json) |> bindx
        | "createProject" -> (fun x ->
            match tryFindStrByAtt "code" x.json |> createProject with
            | Some project -> 
                let projectx = project |> project__ProjectComplex
                runtime.data.projectxs[project.ID] <- projectx
                projectx |> ProjectComplex__json |> wrapOk "projectx"
            | None -> er Er.InvalideParameter) |> bindx
        | "createComp" -> (fun x ->
            match 
                tryFindNumByAtt "project" x.json
                |> parse_int64
                |> runtime.data.projectxs.TryGet with
            | Some projectx ->
                match tryFindStrByAtt "name" x.json |> createComp projectx with
                | Some comp -> 
                    let compx = comp |> comp__CompComplex
                    projectx.compxs[comp.p.Name] <- compx
                    compx |> CompComplex__json |> wrapOk "compx"
                | None -> er Er.InvalideParameter
            | None -> er Er.InvalideParameter) |> bindx
        | "createPage" -> (fun x ->
            match 
                tryFindNumByAtt "project" x.json
                |> parse_int64
                |> runtime.data.projectxs.TryGet with
            | Some projectx ->
                match tryFindStrByAtt "name" x.json |> createPage projectx with
                | Some page -> 
                    let pagex = page |> page__CompComplex
                    projectx.pagexs[page.p.Name] <- pagex
                    pagex |> PageComplex__json |> wrapOk "pagex"
                | None -> er Er.InvalideParameter
            | None -> er Er.InvalideParameter) |> bindx
        | "createVarType" -> (fun x ->
            let bindTypeo = tryFindNumByAtt "bindType" x.json |> parse_int32 |> int__vartypeBindTypeEnum
            let projectxo = tryFindNumByAtt "project" x.json |> parse_int64|> runtime.data.projectxs.TryGet
            let bind = tryFindStrByAtt "bind" x.json
            let n = tryFindStrByAtt "n" x.json
            let t = tryFindStrByAtt "type" x.json
            match (bindTypeo,projectxo) with
            | (Some bindType), (Some projectx) ->
                let ps = n,t,bind
                match 
                    match bindType with
                    | vartypeBindTypeEnum.CompProps -> createCompProp projectx ps
                    | vartypeBindTypeEnum.CompState -> createCompState projectx ps
                    | vartypeBindTypeEnum.PageProps -> createPageProp projectx ps
                    | vartypeBindTypeEnum.PageState -> createPageState projectx ps
                    | _ -> None with
                | Some vt -> vt |> VARTYPE__json |> wrapOk "vt"
                | None -> er Er.InvalideParameter
            | _ -> er Er.InvalideParameter) |> bindx
        | _ -> Fail(Er.ApiNotExists,x)
    | _ -> Fail(Er.ApiNotExists,x)


let branch service api json = 

    use cw = new CodeWrapper("Server.WebHandler.branch")

    let mutable x = incoming__x runtime service api "" json
    
    match service with

    | _ -> ()

    runApi branching x
