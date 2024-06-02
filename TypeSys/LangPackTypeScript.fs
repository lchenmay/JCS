﻿module TypeSys.LangPackTypeScript

open System
open System.Text

open Util.Text

open TypeSys.MetaType

let rec type__annotation tc t = 
    match t.tEnum with
    | TypeEnum.Primitive ->
        match t.name with
        | "int64"
        | "int"
        | "float" -> "number"
        | "bool" -> "boolean"
        | _ -> t.name
    | TypeEnum.Orm v -> t.name
    | TypeEnum.List v -> "any[]"
    | TypeEnum.Dictionary (k,v) -> "{[key:any]: any}"
    | TypeEnum.Option tt -> tt |> type__annotation tc
    | _ -> 
        (str__type tc t.name).name

let type__TypeScript tc (w:TextBlockWriter) t = 

    "export type " + t.name + " = {" |> w.newline

    match t.tEnum with
    | TypeEnum.Structure items -> 
        items
        |> Array.map(fun (s,t) -> 
            s + ":" + (t |> type__annotation tc))

    | TypeEnum.Product items -> 
        [| 0 .. items.Length - 1 |]
        |> Array.map(fun i -> 
            let t = items[i]
            "v" + i.ToString() + ":" + (t |> type__annotation tc))

    | TypeEnum.Sum items ->
        [|  "enum: number"
            "val: any" |]

    | TypeEnum.Enum items -> 
        [|  "enum: number" |]

    | _ -> [| |]
    |> w.multiLineConcate ("," + crlf)


    "}" |> w.newline
