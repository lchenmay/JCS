module TypeSys.LangPackTypeScript

open System
open System.Text

open Util.Text

open TypeSys.MetaType
open TypeSys.Common
open TypeSys.CodeRobotI

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

let type__TypeScript tc srcType srcMor t = 

    match t.tEnum with
    | TypeEnum.Sum items ->
        "const enum " + t.name + "Enum {" |> srcMor.w.newline
        [| 0 .. items.Length - 1 |]
        |> Array.map(fun i -> 
            let n,o = items[i]
            n + " = " + i.ToString() + ",//" + n)
        |> srcMor.w.multiLineIndent 1
        "}" |> srcMor.w.newline
        |> srcMor.w.newlineBlank
    | _ -> ()    

    let w = srcType.w

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

let builderEmpty ns t fieldNames src = 

    "export const p" + t.typeName + "_empty = (): " + ns + ".p" + t.typeName + " => {" |> src.w.newline
    "return {" |> src.w.newlineIndent 1
    fieldNames
    |> Array.map(fun i -> 
        let sort,name,def,json = t.fields[i]
        name + ": " + (fdef__empty ProgrammingLang.TypeScript t name def))
    |> src.w.multiLineConcateIndent "," 2
    " }" |> src.w.appendEnd
    "}" |> src.w.newline
    src.w.newlineBlank()

    "export const " + t.typeName + "_empty = (): " + ns + "." + t.typeName + " => {" |> src.w.newline
    "return {" |> src.w.newlineIndent 1
    "id: 0," |> src.w.newlineIndent 2
    "createdat: new Date()," |> src.w.newlineIndent 2
    "updatedat: new Date()," |> src.w.newlineIndent 2
    "sort: 0," |> src.w.newlineIndent 2
    "p: p" + t.typeName + "_empty() }" |> src.w.newlineIndent 2
    "}" |> src.w.newline
    src.w.newlineBlank()
