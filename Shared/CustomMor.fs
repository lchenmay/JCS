module Shared.CustomMor

open LanguagePrimitives

open System
open System.Collections.Generic
open System.Collections.Concurrent
open System.Text

open Util.Cat
open Util.Perf
open Util.Measures
open Util.CollectionModDict
open Util.Collection
open Util.Db
open Util.DbQuery
open Util.DbTx
open Util.Bin
open Util.Text
open Util.Json
open Util.Orm
open Util.Stat

open PreOrm

open Util.Bin
open Shared.OrmTypes
open Shared.Types
open Shared.OrmMor

// [EuComplex] Structure

let EuComplex_empty(): EuComplex =
    {
        eu = 0L
    }

let EuComplex__bin (bb:BytesBuilder) (v:EuComplex) =

    int64__bin bb v.eu

let bin__EuComplex (bi:BinIndexed):EuComplex =
    let bin,index = bi

    {
        eu = 
            bi
            |> bin__int64
    }

let EuComplex__json (v:EuComplex) =

    [|  ("eu",int64__json v.eu)
         |]
    |> Json.Braket

let EuComplex__jsonTbw (w:TextBlockWriter) (v:EuComplex) =
    json__str w (EuComplex__json v)

let EuComplex__jsonStr (v:EuComplex) =
    (EuComplex__json v) |> json__strFinal


let json__EuComplexo (json:Json):EuComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let euo =
        match json__tryFindByName json "eu" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__int64o with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        {
            eu = euo.Value } |> Some
    else
        None

// [ProjectComplex] Structure

let ProjectComplex_empty(): ProjectComplex =
    {
        comps = ModDict_empty()
        templates = ModDict_empty()
        pages = ModDict_empty()
        project = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pPROJECT_empty() }
    }

let ProjectComplex__bin (bb:BytesBuilder) (v:ProjectComplex) =

    
    ModDictInt64__bin (COMP__bin) bb v.comps
    
    ModDictInt64__bin (TEMPLATE__bin) bb v.templates
    
    ModDictInt64__bin (PAGE__bin) bb v.pages
    PROJECT__bin bb v.project

let bin__ProjectComplex (bi:BinIndexed):ProjectComplex =
    let bin,index = bi

    {
        comps = 
            bi
            |> bin__ModDictInt64(bin__COMP)
        templates = 
            bi
            |> bin__ModDictInt64(bin__TEMPLATE)
        pages = 
            bi
            |> bin__ModDictInt64(bin__PAGE)
        project = 
            bi
            |> bin__PROJECT
    }

let ProjectComplex__json (v:ProjectComplex) =

    [|  ("comps",ModDictInt64__json (COMP__json) v.comps)
        ("templates",ModDictInt64__json (TEMPLATE__json) v.templates)
        ("pages",ModDictInt64__json (PAGE__json) v.pages)
        ("project",PROJECT__json v.project)
         |]
    |> Json.Braket

let ProjectComplex__jsonTbw (w:TextBlockWriter) (v:ProjectComplex) =
    json__str w (ProjectComplex__json v)

let ProjectComplex__jsonStr (v:ProjectComplex) =
    (ProjectComplex__json v) |> json__strFinal


let json__ProjectComplexo (json:Json):ProjectComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let compso =
        match json__tryFindByName json "comps" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictInt64o (json__COMPo) (new Dictionary<int64,COMP>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let templateso =
        match json__tryFindByName json "templates" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictInt64o (json__TEMPLATEo) (new Dictionary<int64,TEMPLATE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let pageso =
        match json__tryFindByName json "pages" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictInt64o (json__PAGEo) (new Dictionary<int64,PAGE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let projecto =
        match json__tryFindByName json "project" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__PROJECTo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        {
            comps = compso.Value
            templates = templateso.Value
            pages = pageso.Value
            project = projecto.Value } |> Some
    else
        None

// [Fact] Structure

let Fact_empty(): Fact =Fact.Undefined

let Fact__bin (bb:BytesBuilder) (v:Fact) =

    match v with
    | Fact.Undefined ->
        int32__bin bb 0

let bin__Fact (bi:BinIndexed):Fact =
    let bin,index = bi

    match bin__int32 bi with
    | _ -> Fact.Undefined

let Fact__json (v:Fact) =

    let items = new List<string * Json>()

    match v with
    | Fact.Undefined ->
        ("e",int32__json 0) |> items.Add

    items.ToArray() |> Json.Braket

let Fact__jsonTbw (w:TextBlockWriter) (v:Fact) =
    json__str w (Fact__json v)

let Fact__jsonStr (v:Fact) =
    (Fact__json v) |> json__strFinal


let json__Facto (json:Json):Fact option =
    let fields = json |> json__items

    match json__tryFindByName json "e" with
    | Some e ->
        match json__int32o e with
        | Some i ->
            match i with
            | 0 -> Fact.Undefined |> Some
            | _ -> None
        | None -> None
    | None -> None

// [RuntimeData] Structure

let RuntimeData_empty(): RuntimeData =
    {
        facts = []
        pcs = ModDict_empty()
    }

let RuntimeData__bin (bb:BytesBuilder) (v:RuntimeData) =

    
    ListImmutable__bin (Fact__bin) bb v.facts
    
    ModDictInt64__bin (ProjectComplex__bin) bb v.pcs

let bin__RuntimeData (bi:BinIndexed):RuntimeData =
    let bin,index = bi

    {
        facts = 
            bi
            |> bin__ListImmutable (bin__Fact)
        pcs = 
            bi
            |> bin__ModDictInt64(bin__ProjectComplex)
    }

let RuntimeData__json (v:RuntimeData) =

    [|  ("facts",ListImmutable__json (Fact__json) v.facts)
        ("pcs",ModDictInt64__json (ProjectComplex__json) v.pcs)
         |]
    |> Json.Braket

let RuntimeData__jsonTbw (w:TextBlockWriter) (v:RuntimeData) =
    json__str w (RuntimeData__json v)

let RuntimeData__jsonStr (v:RuntimeData) =
    (RuntimeData__json v) |> json__strFinal


let json__RuntimeDatao (json:Json):RuntimeData option =
    let fields = json |> json__items

    let mutable passOptions = true

    let factso =
        match json__tryFindByName json "facts" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__ListImmutableo (json__Facto) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let pcso =
        match json__tryFindByName json "pcs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictInt64o (json__ProjectComplexo) (new Dictionary<int64,ProjectComplex>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        {
            facts = factso.Value
            pcs = pcso.Value } |> Some
    else
        None

// [Msg] Structure

let Msg_empty(): Msg =Msg.Heartbeat

let Msg__bin (bb:BytesBuilder) (v:Msg) =

    match v with
    | Msg.Heartbeat ->
        int32__bin bb 0
    | Msg.ApiRequest v ->
        int32__bin bb 1
        json__bin bb v
    | Msg.ApiResponse v ->
        int32__bin bb 2
        json__bin bb v
    | Msg.SingleFact v ->
        int32__bin bb 3
        Fact__bin bb v
    | Msg.MultiFact v ->
        int32__bin bb 4
        
        array__bin (Fact__bin) bb v

let bin__Msg (bi:BinIndexed):Msg =
    let bin,index = bi

    match bin__int32 bi with
    | 4 -> bin__array (bin__Fact) bi |> Msg.MultiFact
    | 3 -> bin__Fact bi |> Msg.SingleFact
    | 2 -> bin__json bi |> Msg.ApiResponse
    | 1 -> bin__json bi |> Msg.ApiRequest
    | _ -> Msg.Heartbeat

let Msg__json (v:Msg) =

    let items = new List<string * Json>()

    match v with
    | Msg.Heartbeat ->
        ("e",int32__json 0) |> items.Add
    | Msg.ApiRequest v ->
        ("e",int32__json 1) |> items.Add
        ("val", v) |> items.Add
    | Msg.ApiResponse v ->
        ("e",int32__json 2) |> items.Add
        ("val", v) |> items.Add
    | Msg.SingleFact v ->
        ("e",int32__json 3) |> items.Add
        ("val",Fact__json v) |> items.Add
    | Msg.MultiFact v ->
        ("e",int32__json 4) |> items.Add
        ("val",array__json (Fact__json) v) |> items.Add

    items.ToArray() |> Json.Braket

let Msg__jsonTbw (w:TextBlockWriter) (v:Msg) =
    json__str w (Msg__json v)

let Msg__jsonStr (v:Msg) =
    (Msg__json v) |> json__strFinal


let json__Msgo (json:Json):Msg option =
    let fields = json |> json__items

    match json__tryFindByName json "e" with
    | Some e ->
        match json__int32o e with
        | Some i ->
            match i with
            | 0 -> Msg.Heartbeat |> Some
            | 1 -> 
                match json__tryFindByName json "val" with 
                | Some v ->
                    match Some v with
                    | Some vv -> vv |> Msg.ApiRequest |> Some
                    | None -> None
                | None -> None
            | 2 -> 
                match json__tryFindByName json "val" with 
                | Some v ->
                    match Some v with
                    | Some vv -> vv |> Msg.ApiResponse |> Some
                    | None -> None
                | None -> None
            | 3 -> 
                match json__tryFindByName json "val" with 
                | Some v ->
                    match json__Facto v with
                    | Some vv -> vv |> Msg.SingleFact |> Some
                    | None -> None
                | None -> None
            | 4 -> 
                match json__tryFindByName json "val" with 
                | Some v ->
                    match json__arrayo (json__Facto) v with
                    | Some vv -> vv |> Msg.MultiFact |> Some
                    | None -> None
                | None -> None
            | _ -> None
        | None -> None
    | None -> None

// [Er] Structure

let Er_empty(): Er =Er.ApiNotExists

let Er__bin (bb:BytesBuilder) (v:Er) =

    match v with
    | Er.ApiNotExists ->
        int32__bin bb 0
    | Er.InvalideParameter ->
        int32__bin bb 1
    | Er.Unauthorized ->
        int32__bin bb 2
    | Er.NotAvailable ->
        int32__bin bb 3
    | Er.Internal ->
        int32__bin bb 4

let bin__Er (bi:BinIndexed):Er =
    let bin,index = bi

    match bin__int32 bi with
    | 4 -> Er.Internal
    | 3 -> Er.NotAvailable
    | 2 -> Er.Unauthorized
    | 1 -> Er.InvalideParameter
    | _ -> Er.ApiNotExists

let Er__json (v:Er) =

    let items = new List<string * Json>()

    match v with
    | Er.ApiNotExists ->
        ("e",int32__json 0) |> items.Add
    | Er.InvalideParameter ->
        ("e",int32__json 1) |> items.Add
    | Er.Unauthorized ->
        ("e",int32__json 2) |> items.Add
    | Er.NotAvailable ->
        ("e",int32__json 3) |> items.Add
    | Er.Internal ->
        ("e",int32__json 4) |> items.Add

    items.ToArray() |> Json.Braket

let Er__jsonTbw (w:TextBlockWriter) (v:Er) =
    json__str w (Er__json v)

let Er__jsonStr (v:Er) =
    (Er__json v) |> json__strFinal


let json__Ero (json:Json):Er option =
    let fields = json |> json__items

    match json__tryFindByName json "e" with
    | Some e ->
        match json__int32o e with
        | Some i ->
            match i with
            | 0 -> Er.ApiNotExists |> Some
            | 1 -> Er.InvalideParameter |> Some
            | 2 -> Er.Unauthorized |> Some
            | 3 -> Er.NotAvailable |> Some
            | 4 -> Er.Internal |> Some
            | _ -> None
        | None -> None
    | None -> None