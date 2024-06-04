module BizShared.CustomMor

open LanguagePrimitives

open System
open System.Collections.Generic
open System.Text

open Util.Cat
open Util.Perf
open Util.Measures
open Util.Db
open Util.DbQuery
open Util.DbTx
open Util.Bin
open Util.Text
open Util.Json
open Util.Orm

open PreOrm

open Util.Bin
open BizShared.OrmTypes
open BizShared.Types
open BizShared.OrmMor

// [Stroke] Structure

let Stroke__bin (bb:BytesBuilder) (v:Stroke) =

    
    List__bin ((fun bb v -> 
        let v0,v1 = v
        
        float32__bin bb v0
        float32__bin bb v1)) bb v.points
    float32__bin bb v.strokeSize
    str__bin bb v.color

let bin__Stroke (bi:BinIndexed):Stroke =
    let bin,index = bi

    {
        points =
            bi
            |> bin__List ((fun bi ->
                        let v0 = 
                            bi
                            |> bin__float32
                        let v1 = 
                            bi
                            |> bin__float32
                
                        v0,v1))
        strokeSize =
            bi
            |> bin__float32
        color =
            bi
            |> bin__str
    }

let Stroke__json (v:Stroke) =

    [|  ("points",
        List__json ((fun v -> 
            let v0,v1 = v
            
            let json0 = float32__json v0
            let json1 = float32__json v1
            [| json0;json1 |] |> Json.Ary)) v.points)
        ("strokeSize",float32__json v.strokeSize)
        ("color",str__json v.color)
         |]
    |> Json.Braket

let Stroke__jsonTbw (w:TextBlockWriter) (v:Stroke) =
    json__str w (Stroke__json v)

let Stroke__jsonStr (v:Stroke) =
    (Stroke__json v) |> json__strFinal


let json__Strokeo (json:Json):Stroke option =
    let fields = json |> json__items

    let mutable passOptions = true

    let pointso =
        match json__tryFindByName json "points" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__Listo ((fun json ->
                        match json with
                        | Json.Ary items ->
                            if items.Length = 2 then
                                let mutable passOptions = true

                                let o0 = 
                                    json
                                    |> json__float32o
                                if o0.IsNone then
                                    passOptions <- false

                                let o1 = 
                                    json
                                    |> json__float32o
                                if o1.IsNone then
                                    passOptions <- false
                
                                if passOptions then
                                    Some(o0.Value,o1.Value)
                                else
                                    None
                            else
                                None
                        | _ -> None)) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let strokeSizeo =
        match json__tryFindByName json "strokeSize" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__float32o with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let coloro =
        match json__tryFindByName json "color" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__stro with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        {
            points = pointso.Value
            strokeSize = strokeSizeo.Value
            color = coloro.Value} |> Some
    else
        None

// [ActionWhiteboard] Structure

let ActionWhiteboard__bin (bb:BytesBuilder) (v:ActionWhiteboard) =

    match v with
    | ActionWhiteboard.Stroke v ->
        int32__bin bb 0
        Stroke__bin bb v
    | ActionWhiteboard.Clear v ->
        int32__bin bb 1
        uint32__bin bb v
    | ActionWhiteboard.Msg v ->
        int32__bin bb 2
        str__bin bb v

let bin__ActionWhiteboard (bi:BinIndexed):ActionWhiteboard =
    let bin,index = bi

    match bin__int32 bi with
    | 2 -> bin__str bi |> ActionWhiteboard.Msg
    | 1 -> bin__uint32 bi |> ActionWhiteboard.Clear
    | _ -> bin__Stroke bi |> ActionWhiteboard.Stroke

let ActionWhiteboard__json (v:ActionWhiteboard) =

    let items = new List<string * Json>()

    match v with
    | ActionWhiteboard.Stroke v ->
        ("enum",int32__json 0) |> items.Add
        ("Stroke",Stroke__json v) |> items.Add
    | ActionWhiteboard.Clear v ->
        ("enum",int32__json 1) |> items.Add
        ("Clear",uint32__json v) |> items.Add
    | ActionWhiteboard.Msg v ->
        ("enum",int32__json 2) |> items.Add
        ("Msg",str__json v) |> items.Add

    items.ToArray() |> Json.Braket

let ActionWhiteboard__jsonTbw (w:TextBlockWriter) (v:ActionWhiteboard) =
    json__str w (ActionWhiteboard__json v)

let ActionWhiteboard__jsonStr (v:ActionWhiteboard) =
    (ActionWhiteboard__json v) |> json__strFinal


let json__ActionWhiteboardo (json:Json):ActionWhiteboard option =
    let fields = json |> json__items

    match json__tryFindByName json "enum" with
    | Some json ->
        match json__int32o json with
        | Some i ->
            match i with
            | 0 -> 
                match json__Strokeo json with
                | Some v -> v |> ActionWhiteboard.Stroke |> Some
                | None -> None
            | 1 -> 
                match json__uint32o json with
                | Some v -> v |> ActionWhiteboard.Clear |> Some
                | None -> None
            | 2 -> 
                match json__stro json with
                | Some v -> v |> ActionWhiteboard.Msg |> Some
                | None -> None
            | _ -> None
        | None -> None
    | None -> None

// [FactWhiteboard] Structure

let FactWhiteboard__bin (bb:BytesBuilder) (v:FactWhiteboard) =

    ActionWhiteboard__bin bb v.action
    str__bin bb v.actor
    int64__bin bb v.clientId
    int64__bin bb v.serverId
    DateTime__bin bb v.clientTimestamp
    DateTime__bin bb v.serverTimestamp

let bin__FactWhiteboard (bi:BinIndexed):FactWhiteboard =
    let bin,index = bi

    {
        action =
            bi
            |> bin__ActionWhiteboard
        actor =
            bi
            |> bin__str
        clientId =
            bi
            |> bin__int64
        serverId =
            bi
            |> bin__int64
        clientTimestamp =
            bi
            |> bin__DateTime
        serverTimestamp =
            bi
            |> bin__DateTime
    }

let FactWhiteboard__json (v:FactWhiteboard) =

    [|  ("action",ActionWhiteboard__json v.action)
        ("actor",str__json v.actor)
        ("clientId",int64__json v.clientId)
        ("serverId",int64__json v.serverId)
        ("clientTimestamp",DateTime__json v.clientTimestamp)
        ("serverTimestamp",DateTime__json v.serverTimestamp)
         |]
    |> Json.Braket

let FactWhiteboard__jsonTbw (w:TextBlockWriter) (v:FactWhiteboard) =
    json__str w (FactWhiteboard__json v)

let FactWhiteboard__jsonStr (v:FactWhiteboard) =
    (FactWhiteboard__json v) |> json__strFinal


let json__FactWhiteboardo (json:Json):FactWhiteboard option =
    let fields = json |> json__items

    let mutable passOptions = true

    let actiono =
        match json__tryFindByName json "action" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__ActionWhiteboardo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let actoro =
        match json__tryFindByName json "actor" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__stro with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let clientIdo =
        match json__tryFindByName json "clientId" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__int64o with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let serverIdo =
        match json__tryFindByName json "serverId" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__int64o with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let clientTimestampo =
        match json__tryFindByName json "clientTimestamp" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__DateTimeo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let serverTimestampo =
        match json__tryFindByName json "serverTimestamp" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__DateTimeo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        {
            action = actiono.Value
            actor = actoro.Value
            clientId = clientIdo.Value
            serverId = serverIdo.Value
            clientTimestamp = clientTimestampo.Value
            serverTimestamp = serverTimestampo.Value} |> Some
    else
        None

// [FactBroadcast] Structure

let FactBroadcast__bin (bb:BytesBuilder) (v:FactBroadcast) =

    match v with
    | FactBroadcast.Whiteboard v ->
        int32__bin bb 0
        FactWhiteboard__bin bb v
    | FactBroadcast.Undefined ->
        int32__bin bb 1

let bin__FactBroadcast (bi:BinIndexed):FactBroadcast =
    let bin,index = bi

    match bin__int32 bi with
    | 1 -> FactBroadcast.Undefined
    | _ -> bin__FactWhiteboard bi |> FactBroadcast.Whiteboard

let FactBroadcast__json (v:FactBroadcast) =

    let items = new List<string * Json>()

    match v with
    | FactBroadcast.Whiteboard v ->
        ("enum",int32__json 0) |> items.Add
        ("Whiteboard",FactWhiteboard__json v) |> items.Add
    | FactBroadcast.Undefined ->
        ("enum",int32__json 1) |> items.Add

    items.ToArray() |> Json.Braket

let FactBroadcast__jsonTbw (w:TextBlockWriter) (v:FactBroadcast) =
    json__str w (FactBroadcast__json v)

let FactBroadcast__jsonStr (v:FactBroadcast) =
    (FactBroadcast__json v) |> json__strFinal


let json__FactBroadcasto (json:Json):FactBroadcast option =
    let fields = json |> json__items

    match json__tryFindByName json "enum" with
    | Some json ->
        match json__int32o json with
        | Some i ->
            match i with
            | 0 -> 
                match json__FactWhiteboardo json with
                | Some v -> v |> FactBroadcast.Whiteboard |> Some
                | None -> None
            | 1 -> FactBroadcast.Undefined |> Some
            | _ -> None
        | None -> None
    | None -> None