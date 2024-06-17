﻿module TypeSys.CodeRobotIITs

open System
open System.IO
open System.Collections.Generic
open System.Data
open System.Data.SqlClient
open System.Text
open System.Text.RegularExpressions

open Util.Runtime
open Util.Cat
open Util.Collection
open Util.CollectionSortedAccessor
open Util.Bin
open Util.Text
open Util.Json
open Util.FileSys
open Util.Db
open Util.DbQuery
open Util.DbTx

open TypeSys.MetaType
open TypeSys.CodeRobotI

let rec t__binImpl (w:TextBlockWriter) indent t = 

    match t.tEnum with
    | TypeEnum.Primitive -> ()
    | TypeEnum.Structure items ->
        items
        |> Array.iter(fun (name,tt) -> 
            w.newlineBlankIndent (indent + 1)
            t__binCall w (indent + 1) tt
            " (bb) (v." + name + ")" |> w.appendEnd)

    | TypeEnum.Product items -> 
        "let " + (productItems__term "rcd" "" "," items) + " = rcd" |> w.newlineIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let tt = items[i]
            "rcd" + i.ToString() |> w.newlineIndent indent
            t__binCall w (indent + 2) tt)

    | TypeEnum.Sum items -> 
        "int32__bin (bb) (v.e)" |> w.newlineIndent (indent + 1)
        "switch (v.e) {" |> w.newlineIndent (indent + 1)
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let n,o = items[i]
            "case " + i.ToString() + ":" |> w.newlineIndent (indent + 2)

            match o with
            | Some tt -> 
                w.newlineBlankIndent (indent + 3)
                t__binCall w (indent + 3) tt
                " (bb) (v)" |> w.appendEnd
            | None -> ()

            "break" |> w.newlineIndent (indent + 3))
        "}" |> w.newlineIndent (indent + 1)

    | TypeEnum.Enum v -> 

        [|  ""; 
            "///// let p" + t.name + "__bin (bb:BytesBuilder) (p:p" + t.name + ") =" |] 
        |> w.multiLine
        
    | TypeEnum.Orm table ->

        [|  ""
            "export const p" + t.name + "__bin = (bb:BytesBuilder) => (p:p" + t.name + ") => {"
            "" |] 
        |> w.multiLine

        table
        |> table__sortedFields
        |> Array.map (fdef__tbin table ProgrammingLang.TypeScript)
        |> Array.map(fun i -> i.ToArray())
        |> Array.concat
        |> Array.iter (w.newlineIndent (indent + 1))

        "}"
        |> w.newline

        [|  ""
            "export const " + t.name + "__bin = (bb:BytesBuilder) => (v:" + t.name + ") => {"
            "bin.int64__bin (bb) (v.ID)"
            "bin.int64__bin (bb) (v.Sort)"
            "bin.DateTime__bin (bb) (v.Createdat)"
            "bin.DateTime__bin (bb) (v.Updatedat)"
            ""
            "p" + t.name + "__bin (bb) (v.p)" |]
        |> w.multiLine

    | TypeEnum.Option v -> ()
    | TypeEnum.Ary v -> ()
    | TypeEnum.List v -> ()
    | TypeEnum.Dictionary (kType,vType) -> ()

and t__binCall w indent t = 
    
    match t.tEnum with
    | TypeEnum.Primitive -> 
        w.appendEnd "bin."
        match t.name with
        | "string" -> "str__bin"
        | "int" -> "int32__bin"
        | "Boolean" -> "bool__bin"
        | _ -> t.name + "__bin"
        |> w.appendEnd
    | TypeEnum.Structure items -> 
        w.appendEnd "bin."
        t.name + "__bin" |> w.appendEnd
    | TypeEnum.Product items -> 
        "((bb:BytesBuilder) => (v:any) => {" |> w.appendEnd
        w.newlineBlankIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            "let v" + i.ToString() + " = v.v" + i.ToString() |> w.newlineIndent indent
            w.newlineBlankIndent indent
            t__binCall w indent items[i]
            " (bb) (v" + i.ToString() + ")" |> w.appendEnd)
        "})" |> w.appendEnd

    | TypeEnum.Sum v -> 
        w.appendEnd "bin."
        t.name + "__bin" |> w.appendEnd
    | TypeEnum.Enum v -> ()
    | TypeEnum.Orm table -> 
        w.appendEnd "bin."
        t.name + "__bin" |> w.appendEnd
    | TypeEnum.Option tt -> 
        w.appendEnd "bin."
        "Option__bin (" |> w.appendEnd
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Ary tt -> 
        "" |> w.newlineIndent indent
        w.appendEnd "bin."
        "array__bin (" |> w.appendEnd
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.List tt -> 

        "" |> w.newlineIndent indent
        w.appendEnd "bin."
        "List__bin (" |> w.appendEnd
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd

    | TypeEnum.Dictionary (kType,vType) -> 
        w.appendEnd "bin."
        "Dictionary__bin (" |> w.appendEnd
        t__binCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__binCall w (indent + 1) vType
        ")" |> w.appendEnd

let rec bin__tImpl (w:TextBlockWriter) indent t = 

    match t.tEnum with
    | TypeEnum.Primitive -> ()
    | TypeEnum.Structure items ->
        "return {" |> w.newlineIndent (indent + 1)
        items
        |> Array.iter(fun (name,tt) -> 
            name + ": " |> w.newlineIndent (indent + 2)
            bin__tCall w (indent + 3) tt 
            " (bi)," |> w.appendEnd)
        "}" |> w.newlineIndent (indent + 1)

    | TypeEnum.Product items -> 
        let var = productItems__term "rcd" "" "," items 
        "let " + var + " = rcd" |> w.newlineIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let tt = items[i]
            "rcd" + i.ToString() |> w.appendEnd
            bin__tCall w (indent + 2) tt)
        "" |> w.newlineIndent indent
        var |> w.newlineIndent indent

    | TypeEnum.Sum items -> 

        "let v:" + t.name + " = {}" |> w.newlineIndent (indent + 1)
        "v.e = bin__int32 (bi)" |> w.newlineIndent (indent + 1)
        "switch (v.e) {" |> w.newlineIndent (indent + 1)
        [| 0 .. items.Length - 1 |]
        |> Array.rev
        |> Array.iter(fun i -> 
            let n,o = items[i]
            "case " + i.ToString() + ":" |> w.newlineIndent (indent + 2)
            match o with
            | Some tt -> 
                "v.val = " |> w.newlineIndent (indent + 3)
                bin__tCall w (indent + 3) tt
                " (bi) " |> w.appendEnd
            | None -> ()
            "break" |> w.newlineIndent (indent + 3))
        "}" |> w.newlineIndent (indent + 1)
        "return v" |> w.newlineIndent (indent + 1)


    | TypeEnum.Enum v -> ()
    | TypeEnum.Orm table ->

        [|  ""
            "export const bin__p" + t.name + " = (bi:BinIndexed):p" + t.name + " => {"
            tab + "let bin,index = bi"
            "" |] 
        |> w.multiLine

        [|  "let p = p" + t.name + "_empty()" |]
        |> w.multiLineIndent (indent + 1)

        table
        |> table__sortedFields
        |> Array.sortBy(fun i ->
            let sort,fname,def,json = i
            sort)
        |> Array.map (fdef__bint table ProgrammingLang.TypeScript)
        |> Array.map(fun i -> i.ToArray())
        |> Array.concat
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  ""
            "return p" |]
        |> w.multiLineIndent (indent + 1)

        [|  ""; 
            "export const bin__" + t.name + " = (bi:BinIndexed):" + t.name + " => {" 
            tab + "let bin,index = bi"
            "" |] 
        |> w.multiLine

        [|  "let ID = bin__int64 (bi)"
            "index.Value = index.Value + 8"
            ""
            "let Sort = bin__int64 (bi)"
            "index.Value = index.Value + 8"
            ""
            "let Createdat = bin__DateTime (bi)"
            ""
            "let Updatedat = bin__DateTime (bi)"
            ""
            "return {" |]
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  "ID: ID,"
            "Sort: Sort,"
            "Createdat: Createdat,"
            "Updatedat: Updatedat,"
            "p:  bin__p" + t.name + " (bi) }" |]
        |> w.multiLineIndent (indent + 2)

        "}"
        |> w.newlineIndent (indent + 1)

    | TypeEnum.Option tt -> ()
    | TypeEnum.List tt -> ()
    | TypeEnum.Dictionary (kType,vType) -> ()

and bin__tCall w indent t = 

    match t.tEnum with
    | TypeEnum.Primitive ->
        w.appendEnd "bin."
        match t.name with
        | "string" -> "bin__str"
        | "int" -> "bin__int32"
        | "Boolean" -> "bin__bool"
        | _ -> "bin__" + t.name
        |> w.appendEnd
    | TypeEnum.Structure items -> 
        w.appendEnd "bin."
        "bin__" + t.name |> w.appendEnd
    | TypeEnum.Product items -> 
        "((bi:BinIndexed) => {" |> w.appendEnd
        let var = productItems__term "v" "" "," items 
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            w.newlineBlankIndent (indent + 2)
            "let v" + i.ToString() + " = " |> w.appendEnd
            bin__tCall w (indent + 3) items[i]
            "(bi)" |> w.appendEnd)
        "" |> w.newlineIndent indent
        "return {" |> w.newlineIndent(indent + 2)
        [| 0 .. items.Length - 1 |]
        |> Array.map(fun i -> "v" + i.ToString() + ":v" + i.ToString())
        |> String.concat ","
        |> w.appendEnd
        "}})" |> w.appendEnd
    | TypeEnum.Sum v -> 
        w.appendEnd "bin."
        "bin__" + t.name |> w.appendEnd
    | TypeEnum.Enum v -> ()
    | TypeEnum.Orm table -> 
        w.appendEnd "bin."
        "bin__" + t.name |> w.appendEnd
    | TypeEnum.Option tt -> 
        w.appendEnd "bin."
        "bin__Option (" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Ary tt -> 
        w.appendEnd "bin."
        "bin__array (" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.List tt -> 
        w.appendEnd "bin."
        "bin__List (" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Dictionary (kType,vType) -> 
        "(fun bi ->" |> w.appendEnd
        "let v = new Dictionary<" + kType.name + "," + vType.name + ">()" |> w.newlineIndent (indent + 1)
        w.appendEnd "bin."
        "bin__Dictionary (" |> w.newlineIndent (indent + 1)
        bin__tCall w (indent + 2) kType
        ") (" |> w.appendEnd
        bin__tCall w (indent + 2) vType
        ") v bi" |> w.appendEnd
        "v)" |> w.newlineIndent (indent + 1)

let rec t__jsonImpl (w:TextBlockWriter) indent t = 

    match t.tEnum with
    | TypeEnum.Structure items ->

        "[|  " |> w.newlineIndent (indent + 1)

        items
        |> Array.iter(fun (name,tt) -> 
            "(\"" + name + "\"," |> w.appendEnd
            t__jsonCall w (indent + 2) tt
            " v." + name + ")" |> w.appendEnd
            w.newlineBlankIndent (indent + 2))

        " |]" |> w.appendEnd
        "|> Json.Braket" |> w.newlineIndent (indent + 1)

    | TypeEnum.Sum items -> 

        "let items = new List<string * Json>()" |> w.newlineIndent (indent + 1)
        w.newlineBlank()

        "match v with" |> w.newlineIndent (indent + 1)
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i ->
            let n,o = items[i]
            "| " + t.name + "." + n |> w.newlineIndent (indent + 1)
            match o with
            | Some tt -> " v ->" |> w.appendEnd
            | None ->  " ->" |> w.appendEnd
            "(\"e\",int32__json " + i.ToString() + ") |> items.Add" |> w.newlineIndent (indent + 2)
            match o with
            | Some tt -> 
                w.newlineBlankIndent (indent + 2)
                "(\"" + n + "\"," |> w.appendEnd
                t__jsonCall w (indent + 2) tt
                " v) |> items.Add" |> w.appendEnd
            | None -> ())

        w.newlineBlank()
        "items.ToArray() |> Json.Braket" |> w.newlineIndent (indent + 1)

    | TypeEnum.Orm table ->
    
        [|  ""; 
            "export const p" + t.name + "__json = (p:p" + t.name + ") => {"
            "" |] 
        |> w.multiLine


        "[|" |> w.newlineIndent (indent + 1)

        table
        |> table__sortedFields
        |> Array.map(fun f -> 
            let sort,name,def,json = f
            let src = fdef__tjson f
            "(\"" + name + "\"," + src + ")")
        |> Array.iter (w.newlineIndent (indent + 2))

        " |]" |> w.appendEnd
        "|> Json.Braket" |> w.newlineIndent (indent + 1)

        [|  ""; 
            "let " + t.name + "__json (v:" + t.name + ") ="
            "" |] 
        |> w.multiLine

        [|  "let p = v.p"
            "" |]
        |> w.multiLineIndent 1

        "[|  (\"id\",v.ID.ToString() |> Json.Num)" |> w.newlineIndent (indent + 1)
        "(\"sort\",v.Sort.ToString() |> Json.Num)" |> w.newlineIndent (indent + 2)
        "(\"createdat\",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)" |> w.newlineIndent (indent + 2)
        "(\"updatedat\",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)" |> w.newlineIndent (indent + 2)
        
        table
        |> table__sortedFields
        |> Array.map(fun f -> 
            let sort,name,def,json = f
            let src = fdef__tjson f
            "(\"" + name + "\"," + src + ")")
        |> Array.iter (w.newlineIndent (indent + 2))

        " |]" |> w.appendEnd
        "|> Json.Braket" |> w.newlineIndent (indent + 1)

        "}"
        |> w.newline

    | _ -> ()

and t__jsonCall w indent t = 

    match t.tEnum with
    | TypeEnum.Primitive -> 
        match t.name with
        | "string" -> "str__json"
        | "int" -> "int32__json"
        | "Boolean" -> "bool__json"
        | _ -> t.name + "__json"
        |> w.appendEnd
    | TypeEnum.Product items -> 
        "(fun v -> " |> w.appendEnd
        "let " + (productItems__term "v" "" "," items) + " = v" |> w.newlineIndent indent
        w.newlineBlankIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            "let json" + i.ToString() + " = " |> w.newlineIndent indent
            t__jsonCall w indent items[i]
            " v" + i.ToString() |> w.appendEnd)
        "[| " + (productItems__term "json" "" ";" items) + " |] |> Json.Ary)" |> w.newlineIndent indent
    | TypeEnum.Sum items -> t.name + "__json" |> w.appendEnd
    | TypeEnum.Option tt -> 
        "Option__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.List tt -> 
        "List__json (" |> w.newlineIndent indent
        t__jsonCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Dictionary (kType,vType) -> 
        "Dictionary__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__jsonCall w (indent + 1) vType
        ")" |> w.appendEnd
    | _ -> t.name + "__json" |> w.appendEnd

    ()

let rec json__tImpl (w:TextBlockWriter) indent t = 

    match t.tEnum with
    | TypeEnum.Primitive -> ()
    | TypeEnum.Structure items ->

        "let mutable passOptions = true" |> w.newlineIndent (indent + 1) 
        w.newlineBlank()

        items
        |> Array.iter(fun (name,tt) -> 
            "let " + name + "o =" |> w.newlineIndent (indent + 1) 
            "match json__tryFindByName json \"" + name + "\" with" |> w.newlineIndent (indent + 2) 
            "| None ->" |> w.newlineIndent (indent + 2) 
            "passOptions <- false" |> w.newlineIndent (indent + 3) 
            "None" |> w.newlineIndent (indent + 3) 
            "| Some v -> " |> w.newlineIndent (indent + 2) 
            "match v |> " |> w.newlineIndent (indent + 3) 
            (json__tCall w (indent + 3) tt)
            " with" |> w.appendEnd 
            "| Some res -> Some res" |> w.newlineIndent (indent + 3) 
            "| None ->" |> w.newlineIndent (indent + 3) 
            "passOptions <- false" |> w.newlineIndent (indent + 4) 
            "None" |> w.newlineIndent (indent + 4) 
            w.newlineBlank())

        "if passOptions then" |> w.newlineIndent (indent + 1) 

        "{" |> w.newlineIndent (indent + 2)
        items
        |> Array.iter(fun (name,tt) -> 
            name + " = " + name + "o.Value" |> w.newlineIndent (indent + 3) 
            ())
        "} |> Some" |> w.appendEnd

        "else" |> w.newlineIndent (indent + 1) 
        "None" |> w.newlineIndent (indent + 2) 

    | TypeEnum.Product items -> 
        let var = productItems__term "rcd" "" "," items 
        "let " + var + " = rcd" |> w.newlineIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let tt = items[i]
            "rcd" + i.ToString() |> w.appendEnd
            json__tCall w (indent + 2) tt
            ())
        "" |> w.newlineIndent indent
        var |> w.newlineIndent indent
        
    | TypeEnum.Sum items ->

        "match json__tryFindByName json \"e\" with" |> w.newlineIndent (indent + 1)
        "| Some json ->" |> w.newlineIndent (indent + 1)
        "match json__int32o json with" |> w.newlineIndent (indent + 2)
        "| Some i ->" |> w.newlineIndent (indent + 2)
        "match i with" |> w.newlineIndent (indent + 3)
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let n,o = items[i]
            "| " + i.ToString() + " -> " |> w.newlineIndent (indent + 3)
            match o with
            | Some tt -> 
                "match " |> w.newlineIndent (indent + 4)
                json__tCall w (indent + 4) tt
                " json with" |> w.appendEnd
                "| Some v -> v |> " + t.name + "." + n + " |> Some" |> w.newlineIndent (indent + 4)
                "| None -> None" |> w.newlineIndent (indent + 4)
            | None -> t.name + "." + n + " |> Some" |> w.appendEnd)
        "| _ -> None" |> w.newlineIndent (indent + 3)
        "| None -> None" |> w.newlineIndent (indent + 2)
        "| None -> None" |> w.newlineIndent (indent + 1)

    | TypeEnum.Enum v -> ()
    | TypeEnum.Orm table ->
    
        [|  ""
            "let json__p" + t.name + "o (json:Json):p" + t.name + " option =" 
            tab + "let fields = json |> json__items"
            "" |]
        |> w.multiLine

        [|  "let p = p" + t.name + "_empty()" |]
        |> Array.iter (w.newlineIndent (indent + 1))

        table
        |> table__sortedFields
        |> Array.sortBy(fun i ->
            let sort,fname,def,json = i
            sort)
        |> Array.map fdef__jsont
        |> Array.map(fun i -> i.ToArray())
        |> Array.concat
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  ""
            "p |> Some" 
            "" |]
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  ""
            "let json__" + t.name + "o (json:Json):" + t.name + " option =" 
            tab + "let fields = json |> json__items"
            "" |]
        |> w.multiLine

        [|  "let ID = checkfield fields \"id\" |> parse_int64"
            "let Sort = checkfield fields \"sort\" |> parse_int64"
            "let Createdat = checkfield fields \"createdat\" |> parse_int64 |> DateTime.FromBinary"
            "let Updatedat = checkfield fields \"updatedat\" |> parse_int64 |> DateTime.FromBinary"
            ""
            "let p = p" + t.name + "_empty()" |]
        |> Array.iter (w.newlineIndent (indent + 1))

        table
        |> table__sortedFields
        |> Array.sortBy(fun i ->
            let sort,fname,def,json = i
            sort)
        |> Array.map fdef__jsont
        |> Array.map(fun i -> i.ToArray())
        |> Array.concat
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  ""
            "{"
            tab + "ID = ID"
            tab + "Sort = Sort"
            tab + "Createdat = Createdat"
            tab + "Updatedat = Updatedat"
            tab + "p = p } |> Some" 
            "" |]
        |> Array.iter (w.newlineIndent (indent + 1))

    | TypeEnum.Option tt -> ()
    | TypeEnum.Ary tt -> ()
    | TypeEnum.List tt -> ()
    | TypeEnum.Dictionary (kType,vType) -> ()

and json__tCall (w:TextBlockWriter)indent t = 

    match t.tEnum with
    | TypeEnum.Primitive ->
        match t.name with
        | "string" -> "json__stro"
        | "int" -> "json__int32o"
        | "Boolean" -> "json__boolo"
        | _ -> "json__" + t.name + "o"
        |> w.appendEnd
    | TypeEnum.Structure items -> "json__" + t.name + "o" |> w.appendEnd
    | TypeEnum.Product items -> 
        "(fun json ->" |> w.appendEnd
        "match json with" |> w.newlineIndent (indent + 2)
        "| Json.Ary items ->" |> w.newlineIndent (indent + 2)
        "if items.Length = " + items.Length.ToString() + " then" |> w.newlineIndent (indent + 3)
        "let mutable passOptions = true" |> w.newlineIndent (indent + 4)
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            w.newlineBlank()
            w.newlineBlankIndent (indent + 4)
            "let o" + i.ToString() + " = " |> w.appendEnd
            "json" |> w.newlineIndent (indent + 5)
            "|> " |> w.newlineIndent (indent + 5)
            json__tCall w (indent + 5) items[i]
            "if o" + i.ToString() + ".IsNone then" |> w.newlineIndent (indent + 4)
            "passOptions <- false" |> w.newlineIndent (indent + 5)
            ())
        "" |> w.newlineIndent indent
        "if passOptions then" |> w.newlineIndent(indent + 4)
        let var = productItems__term "o" ".Value" "," items 
        "Some(" + var |> w.newlineIndent(indent + 5)
        ")" |> w.appendEnd
        "else" |> w.newlineIndent(indent + 4)
        "None" |> w.newlineIndent(indent + 5)
        "else" |> w.newlineIndent (indent + 3)
        "None" |> w.newlineIndent (indent + 4)
        "| _ -> None)" |> w.newlineIndent(indent + 2)

    | TypeEnum.Sum items -> "json__" + t.name + "o" |> w.appendEnd
    | TypeEnum.Enum v -> ()
    | TypeEnum.Orm table -> "json__" + t.name + "o" |> w.appendEnd
    | TypeEnum.Option tt -> 
        "json__Optiono (" |> w.appendEnd
        json__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Ary tt -> 
        "json__arrayo (" |> w.appendEnd
        json__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.List tt -> 
        "json__Listo (" |> w.appendEnd
        json__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Dictionary (kType,vType) -> 
        "(fun json ->" |> w.appendEnd
        "json__Dictionaryo (" |> w.newlineIndent (indent + 1)
        json__tCall w (indent) kType
        ") (" |> w.appendEnd
        json__tCall w (indent) vType
        ") (new Dictionary<" + kType.name + "," + vType.name + ">()) json)" |> w.appendEnd

