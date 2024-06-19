module TypeSys.LangPackTypeScript

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
        | "float"
        | "float32" -> "number"
        | "bool" -> "boolean"
        | "DateTime" -> "Date"
        | _ -> t.name
    | TypeEnum.Orm v -> t.name
    | TypeEnum.List v -> "any[]"
    | TypeEnum.Dictionary (k,v) -> "{[key:any]: any}"
    | TypeEnum.Option tt -> tt |> type__annotation tc
    | _ -> 
        (str__type tc t.name).name

let type__TypeScript tc (w:TextBlockWriter) t = 

    match t.tEnum with
    | TypeEnum.Sum items ->
        "const enum " + t.name + "Enum {" |> w.newline
        [| 0 .. items.Length - 1 |]
        |> Array.map(fun i -> 
            let n,o = items[i]
            n + " = " + i.ToString() + ",//" + n)
        |> w.multiLineIndent 1
        "}" |> w.newline
        |> w.newlineBlank
    | _ -> ()    

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
        [|  "e: number"
            "val: any" |]

    | TypeEnum.Enum items -> 
        [|  "e: number" |]

    | _ -> [| |]
    |> w.multiLineConcate ("," + crlf)


    "}" |> w.newline
