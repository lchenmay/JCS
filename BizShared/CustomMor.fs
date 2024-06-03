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
    uint32__bin bb v.color

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
            |> bin__uint32
    }

let Stroke__json (v:Stroke) =

    [|  ("points",
        List__json ((fun v -> 
            let v0,v1 = v
            
            let json0 = float32__json v0
            let json1 = float32__json v1
            [| json0;json1 |] |> Json.Ary)) v.points)
        ("strokeSize",float32__json v.strokeSize)
        ("color",uint32__json v.color)
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
            match v |> json__uint32o with
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