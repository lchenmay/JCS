module JCS.Shared.Project

open System
open System.Threading
open System.Text
open System.Collections.Generic
open System.Collections.Concurrent

open Util.Text
open Util.Perf
open Util.Crypto
open Util.CollectionModDict
open Util.Db
open Util.DbTx
open Util.Orm
open Util.Zmq

open JCS.Shared.OrmTypes
open JCS.Shared.Types
open JCS.Shared.OrmMor
open JCS.Shared.CustomMor

let project__ProjectComplex project = 
    {   hostconfigs = createModDictStr 4    
        tablexs = createModDictStr 4
        compxs = createModDictStr 4
        templatexs = createModDictStr 4
        pagexs = createModDictStr 4
        apixs = createModDictStr 4
        project = project }

let comp__CompComplex comp = 
    {
        states = createModDictStr 4
        props = createModDictStr 4
        comp = comp }

let page__CompComplex page = 
    {
        states = createModDictStr 4
        props = createModDictStr 4
        page = page }

let api__ApiComplex api = 
    {
        reqs = createModDictStr 4
        reps = createModDictStr 4
        api = api }

let projx__lines projx = 

    let res = new List<string>()
    
    [|  projx.project.ID.ToString()
        projx.project.p.Code    |]
    |> res.AddRange

    projx.compxs.Values
    |> Array.iter(fun x -> 

        x.comp.p.Name |> res.Add

        x.props.Values
        |> Array.iter(fun xx -> 
            "Prop: " + xx.p.Name + ":" + xx.p.Type |> res.Add))

    res.ToArray()