module TypeSys.CodeRobotIIFs

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
open Util.Bin
open Util.Text
open Util.Json
open Util.FileSys
open Util.Db
open Util.DbQuery
open Util.DbTx

open TypeSys.MetaType
open TypeSys.Common
open TypeSys.CodeRobotI


let rec t__binImpl (w:TextBlockWriter) indent t = 

    match t.tEnum with
    | TypeEnum.Primitive -> ()
    | TypeEnum.Structure items ->
        items
        |> Array.filter(snd >> type__supportMarshall)
        |> Array.iter(fun (name,tt) -> 
            w.newlineBlankIndent (indent + 1)
            t__binCall w (indent + 1) tt
            " bb v." + name |> w.appendEnd)

    | TypeEnum.Product items -> 
        let items = items |> Array.filter(type__supportMarshall)
        "let " + (productItems__term "rcd" "" "," items) + " = rcd" |> w.newlineIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let tt = items[i]
            "rcd" + i.ToString() |> w.newlineIndent indent
            t__binCall w (indent + 2) tt)

    | TypeEnum.Sum items -> 
        "match v with" |> w.newlineIndent (indent + 1)
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let n,o = items[i]
            "| " + t.name + "." + n |> w.newlineIndent (indent + 1)
            match o with
            | Some tt -> " v ->" |> w.appendEnd
            | None -> " ->" |> w.appendEnd
            
            "int32__bin bb " + i.ToString() |> w.newlineIndent (indent + 2)

            match o with
            | Some tt -> 
                w.newlineBlankIndent (indent + 2)
                t__binCall w (indent + 2) tt
                " bb v" |> w.appendEnd
            | None -> ()

            ())

    | TypeEnum.Enum v -> 

        [|  ""; 
            "///// let p" + t.name + "__bin (bb:BytesBuilder) (p:p" + t.name + ") =" |] 
        |> w.multiLine
        
    | TypeEnum.OrmRcd table ->

        [|  ""
            "let p" + t.name + "__bin (bb:BytesBuilder) (p:p" + t.name + ") ="
            "" |] 
        |> w.multiLine

        table
        |> table__sortedFields
        |> Array.map (fdef__tbin table ProgrammingLang.FSharp)
        |> Array.map(fun i -> i.ToArray())
        |> Array.concat
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  "" 
            "let " + t.name + "__bin (bb:BytesBuilder) (v:" + t.name + ") =" |]
        |> w.multiLine

        [|  "v.ID |> BitConverter.GetBytes |> bb.append"
            "v.Sort |> BitConverter.GetBytes |> bb.append"
            "DateTime__bin bb v.Createdat"
            "DateTime__bin bb v.Updatedat"
            ""
            "p" + t.name + "__bin bb v.p" |]
        |> w.multiLineIndent 1

    | TypeEnum.Option v -> ()
    | TypeEnum.Ary v -> ()
    | TypeEnum.List v -> ()
    | TypeEnum.ListImmutable v -> ()
    | TypeEnum.Dictionary (kType,vType) -> ()
    | TypeEnum.SortedDictionary (kType,vType) -> ()
    | TypeEnum.ConcurrentDictionary (kType,vType) -> ()
    | TypeEnum.ModDictInt64 v -> ()
    | TypeEnum.ModDictStr v -> ()
    | TypeEnum.Fun (src,dst) -> ()

and t__binCall w indent t = 
    
    match t.tEnum with
    | TypeEnum.Primitive -> 
        match t.name with
        | "string" -> "str__bin"
        | "int" -> "int32__bin"
        | "Boolean" -> "bool__bin"
        | "Json" -> "json__bin"
        | _ -> t.name + "__bin"
        |> w.appendEnd
    | TypeEnum.Structure items -> t.name + "__bin" |> w.appendEnd
    | TypeEnum.Product items -> 
        "(fun bb v -> " |> w.appendEnd
        "let " + (productItems__term "v" "" "," items) + " = v" |> w.newlineIndent indent
        w.newlineBlankIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            w.newlineBlankIndent indent
            t__binCall w indent items[i]
            " bb v" + i.ToString() |> w.appendEnd)
        ")" |> w.appendEnd

    | TypeEnum.Sum v -> t.name + "__bin" |> w.appendEnd
    | TypeEnum.Enum v -> ()
    | TypeEnum.OrmRcd table -> t.name + "__bin" |> w.appendEnd
    | TypeEnum.Ormp table -> t.name + "__bin" |> w.appendEnd
    | TypeEnum.Option tt -> 
        "Option__bin (" |> w.appendEnd
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Ary tt ->
        "array__bin (" |> w.newlineIndent indent
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.ListImmutable tt -> 
        "ListImmutable__bin (" |> w.newlineIndent indent
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.List tt -> 
        "List__bin (" |> w.newlineIndent indent
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Dictionary (kType,vType) -> 
        "Dictionary__bin (" |> w.appendEnd
        t__binCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__binCall w (indent + 1) vType
        ")" |> w.appendEnd
    | TypeEnum.SortedDictionary (kType,vType) -> 
        "SortedDictionary__bin (" |> w.appendEnd
        t__binCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__binCall w (indent + 1) vType
        ")" |> w.appendEnd
    | TypeEnum.ConcurrentDictionary (kType,vType) -> 
        "ConcurrentDictionary__bin (" |> w.appendEnd
        t__binCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__binCall w (indent + 1) vType
        ")" |> w.appendEnd
    | TypeEnum.ModDictInt64 tt -> 
        "ModDictInt64__bin (" |> w.newlineIndent indent
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.ModDictStr tt -> 
        "ModDictStr__bin (" |> w.newlineIndent indent
        t__binCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Fun (src,dst) -> ()

let rec bin__tImpl (w:TextBlockWriter) indent t = 

    match t.tEnum with
    | TypeEnum.Primitive -> ()
    | TypeEnum.Structure items ->
        "{" |> w.newlineIndent (indent + 1)
        items
        |> Array.iter(fun (name,tt) -> 
            name + " = " |> w.newlineIndent (indent + 2)
            if type__supportMarshall tt then
                "bi" |> w.newlineIndent (indent + 3)
                "|> " |> w.newlineIndent (indent + 3)
                (bin__tCall w (indent + 3) tt)
             else
                t__emptyCall w indent tt)
        "}" |> w.newlineIndent (indent + 1)

    | TypeEnum.Product items -> 
        let items = items |> Array.filter(type__supportMarshall)
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

        "match bin__int32 bi with" |> w.newlineIndent (indent + 1)
        [| 0 .. items.Length - 1 |]
        |> Array.rev
        |> Array.iter(fun i -> 
            let n,o = items[i]
            if i = 0 then
                "| _ -> " |> w.newlineIndent (indent + 1)
            else
                "| " + i.ToString() + " -> " |> w.newlineIndent (indent + 1)
            match o with
            | Some tt -> 
                bin__tCall w (indent + 2) tt
                " bi |> " + t.name + "." + n |> w.appendEnd
            | None -> t.name + "." + n |> w.appendEnd)


    | TypeEnum.Enum v -> ()
    | TypeEnum.OrmRcd table ->

        [|  ""
            "let bin__p" + t.name + " (bi:BinIndexed):p" + t.name + " ="
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
        |> Array.map (fdef__bint table ProgrammingLang.FSharp)
        |> Array.map(fun i -> i.ToArray())
        |> Array.concat
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  ""
            "p"  |]
        |> w.multiLineIndent (indent + 1)

        [|  ""; 
            "let bin__" + t.name + " (bi:BinIndexed):" + t.name + " =" 
            tab + "let bin,index = bi"
            "" |] 
        |> w.multiLine

        [|  "let ID = BitConverter.ToInt64(bin,index.Value)"
            "index.Value <- index.Value + 8"
            ""
            "let Sort = BitConverter.ToInt64(bin,index.Value)"
            "index.Value <- index.Value + 8"
            ""
            "let Createdat = bin__DateTime bi"
            ""
            "let Updatedat = bin__DateTime bi"
            ""
            "{" |]
        |> Array.iter (w.newlineIndent (indent + 1))

        [|  "ID = ID"
            "Sort = Sort"
            "Createdat = Createdat"
            "Updatedat = Updatedat"
            "p = bin__p" + t.name + " bi }" |]
        |> w.multiLineIndent (indent + 2)

    | TypeEnum.Option tt -> ()
    | TypeEnum.Ary tt -> ()
    | TypeEnum.List tt -> ()
    | TypeEnum.ListImmutable tt -> ()
    | TypeEnum.Dictionary (kType,vType) -> ()
    | TypeEnum.SortedDictionary (kType,vType) -> ()
    | TypeEnum.ConcurrentDictionary(kType,vType) -> ()
    | TypeEnum.ModDictInt64 tt -> ()
    | TypeEnum.ModDictStr tt -> ()

and bin__tCall w indent t = 

    match t.tEnum with
    | TypeEnum.Primitive ->
        match t.name with
        | "string" -> "bin__str"
        | "int" -> "bin__int32"
        | "Boolean" -> "bin__bool"
        | "Json" -> "bin__json"
        | _ -> "bin__" + t.name
        |> w.appendEnd
    | TypeEnum.Structure items -> "bin__" + t.name |> w.appendEnd
    | TypeEnum.Product items -> 
        "(fun bi ->" |> w.appendEnd
        let var = productItems__term "v" "" "," items 
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            w.newlineBlankIndent (indent + 2)
            "let v" + i.ToString() + " = " |> w.appendEnd
            "bi" |> w.newlineIndent (indent + 3)
            "|> " |> w.newlineIndent (indent + 3)
            bin__tCall w (indent + 3) items[i])
        "" |> w.newlineIndent indent
        var |> w.newlineIndent(indent + 2)
        ")" |> w.appendEnd
    | TypeEnum.Sum v -> "bin__" + t.name |> w.appendEnd
    | TypeEnum.Enum v -> ()
    | TypeEnum.OrmRcd table -> "bin__" + t.name |> w.appendEnd
    | TypeEnum.Ormp table -> "bin__" + t.name |> w.appendEnd
    | TypeEnum.Option tt -> 
        "bin__Option (" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Ary tt -> 
        "bin__array (" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.List tt -> 
        "bin__List (" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.ListImmutable tt -> 
        "bin__ListImmutable (" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Dictionary (kType,vType) ->
        "(fun bi ->" |> w.appendEnd
        "let v = new Dictionary<" + kType.name + "," + vType.name + ">()" |> w.newlineIndent (indent + 1)
        "bin__Dictionary (" |> w.newlineIndent (indent + 1)
        bin__tCall w (indent + 2) kType
        ") (" |> w.appendEnd
        bin__tCall w (indent + 2) vType
        ") v bi" |> w.appendEnd
        "v)" |> w.newlineIndent (indent + 1)
    | TypeEnum.SortedDictionary (kType,vType) ->
        "(fun bi ->" |> w.appendEnd
        "let v = new SortedDictionary<" + kType.name + "," + vType.name + ">()" |> w.newlineIndent (indent + 1)
        "bin__SortedDictionary (" |> w.newlineIndent (indent + 1)
        bin__tCall w (indent + 2) kType
        ") (" |> w.appendEnd
        bin__tCall w (indent + 2) vType
        ") v bi" |> w.appendEnd
        "v)" |> w.newlineIndent (indent + 1)
    | TypeEnum.ConcurrentDictionary (kType,vType) -> 
        "(fun bi ->" |> w.appendEnd
        "let v = new ConcurrentDictionary<" + kType.name + "," + vType.name + ">()" |> w.newlineIndent (indent + 1)
        "bin__ConcurrentDictionary (" |> w.newlineIndent (indent + 1)
        bin__tCall w (indent + 2) kType
        ") (" |> w.appendEnd
        bin__tCall w (indent + 2) vType
        ") v bi" |> w.appendEnd
        "v)" |> w.newlineIndent (indent + 1)
    | TypeEnum.ModDictInt64 tt -> 
        "bin__ModDictInt64(" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.ModDictStr tt -> 
        "bin__ModDictStr(" |> w.appendEnd
        bin__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Fun (kType,vType) -> ()

and t__emptyImpl (w:TextBlockWriter) indent t = 

    "let " + t.name + "_empty(): " + t.name + " ="
    |> w.newline

    match t.tEnum with
    | TypeEnum.Primitive -> ()
    | TypeEnum.Structure items ->
        "{" |> w.newlineIndent (indent + 1)
        items
        |> Array.iter(fun (name,tt) -> 
            name + " = " |> w.newlineIndent (indent + 2)
            (t__emptyCall w (indent + 3) tt))
        "}" |> w.newlineIndent (indent + 1)

    | TypeEnum.Product items -> 
        let var = productItems__term "rcd" "" "," items 
        "let " + var + " = rcd" |> w.newlineIndent indent
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let tt = items[i]
            "rcd" + i.ToString() |> w.appendEnd
            t__emptyCall w (indent + 2) tt)
        "" |> w.newlineIndent indent
        var |> w.newlineIndent indent

    | TypeEnum.Sum items -> 
        let n,o = items[0]
        match o with
        | Some tt -> 
            " " |> w.appendEnd
            t__emptyCall w (indent + 2) tt
            " |> " + t.name + "." + n |> w.appendEnd
        | None -> t.name + "." + n |> w.appendEnd

    | TypeEnum.Enum v -> ()
    | TypeEnum.OrmRcd table ->
        ()

    | TypeEnum.Option tt -> ()
    | TypeEnum.Ary tt -> ()
    | TypeEnum.List tt -> ()
    | TypeEnum.ListImmutable tt -> ()
    | TypeEnum.Dictionary (kType,vType) -> ()
    | TypeEnum.SortedDictionary (kType,vType) -> ()
    | TypeEnum.ConcurrentDictionary (kType,vType) -> ()
    | TypeEnum.ModDictInt64 tt -> ()
    | TypeEnum.ModDictStr tt -> ()

and t__emptyCall w indent t = 

    match t.tEnum with
    | TypeEnum.Primitive ->
        match t.name with
        | "string" -> "\"\""
        | "float" -> "0.0"
        | "int" -> "0"
        | "int32" -> "0"
        | "int64" -> "0L"
        | "bool" -> "true"
        | "DateTime" -> "DateTime.MinValue"
        | "Json" -> "bin__json"
        | "Stat" -> "Stat_empty()"
        | "SpotInStat" -> "SpotInStat_empty()"
        | _ -> "bin__" + t.name
        |> w.appendEnd
    | TypeEnum.Structure items -> t.name + "_empty()" |> w.appendEnd
    | TypeEnum.Product items -> 
        "(fun bi ->" |> w.appendEnd
        let var = productItems__term "v" "" "," items 
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            w.newlineBlankIndent (indent + 2)
            "let v" + i.ToString() + " = " |> w.appendEnd
            "bi" |> w.newlineIndent (indent + 3)
            "|> " |> w.newlineIndent (indent + 3)
            t__emptyCall w (indent + 3) items[i])
        "" |> w.newlineIndent indent
        var |> w.newlineIndent(indent + 2)
        ")" |> w.appendEnd
    | TypeEnum.Sum v -> t.name + "_empty()" |> w.appendEnd
    | TypeEnum.Enum v -> ()
    | TypeEnum.OrmRcd table -> 
        "{ ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = p" + t.name.ToUpper() + "_empty() }" |> w.appendEnd
    | TypeEnum.Ormp table -> 
        t.name + "_empty()" |> w.appendEnd
    | TypeEnum.Option tt -> 
        "None" |> w.appendEnd
        //"bin__Option (" |> w.appendEnd
        //t__emptyCall w (indent + 1) tt
        //")" |> w.appendEnd
    | TypeEnum.Ary tt -> 
        "[| |]" |> w.appendEnd
    | TypeEnum.List tt -> 
        "new List<" + tt.name + ">()" |> w.appendEnd
    | TypeEnum.ListImmutable tt -> 
        "[]" |> w.appendEnd
    | TypeEnum.Dictionary (kType,vType) -> 
        "new Dictionary<" + kType.name + "," + vType.name + ">()" |> w.appendEnd
    | TypeEnum.SortedDictionary (kType,vType) -> 
        "new SortedDictionary<" + kType.name + "," + vType.name + ">()" |> w.appendEnd
    | TypeEnum.ConcurrentDictionary (kType,vType) -> 
        "new ConcurrentDictionary<" + kType.name + "," + vType.name + ">()" |> w.appendEnd
    | TypeEnum.ModDictInt64 tt -> 
        "ModDict_empty()" |> w.appendEnd
    | TypeEnum.ModDictStr tt -> 
        "ModDict_empty()" |> w.appendEnd
    | TypeEnum.Fun (src,dst) -> 
        "(fun _ -> " |> w.appendEnd
        match dst.tEnum with
        | TypeEnum.Primitive -> t__emptyCall w indent dst
        | _ -> dst.name + "_empty()" |> w.appendEnd
        ")" |> w.appendEnd

let rec t__jsonImpl (w:TextBlockWriter) indent t = 

    match t.tEnum with
    | TypeEnum.Structure items ->
        let items = items |> Array.filter(snd >> type__supportMarshall)

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
                "(\"val\"," |> w.appendEnd
                t__jsonCall w (indent + 2) tt
                " v) |> items.Add" |> w.appendEnd
            | None -> ())

        w.newlineBlank()
        "items.ToArray() |> Json.Braket" |> w.newlineIndent (indent + 1)

    | TypeEnum.OrmRcd table ->
    
        [|  ""; 
            "let p" + t.name + "__json (p:p" + t.name + ") ="
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
        
        "(\"p\",p" + t.name + "__json v.p)" |> w.newlineIndent (indent + 2)
        
        //table
        //|> table__sortedFields
        //|> Array.map(fun f -> 
        //    let sort,name,def,json = f
        //    let src = fdef__tjson f
        //    "(\"" + name + "\"," + src + ")")
        //|> Array.iter (w.newlineIndent (indent + 2))

        " |]" |> w.appendEnd
        "|> Json.Braket" |> w.newlineIndent (indent + 1)

    | _ -> ()

and t__jsonCall w indent t = 

    match t.tEnum with
    | TypeEnum.Primitive -> 
        match t.name with
        | "string" -> "str__json"
        | "int" -> "int32__json"
        | "Boolean" -> "bool__json"
        | "Json" -> ""
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
    | TypeEnum.Ary tt -> 
        "array__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.ListImmutable tt -> 
        "ListImmutable__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.List tt -> 
        "List__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Dictionary (kType,vType) -> 
        "Dictionary__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__jsonCall w (indent + 1) vType
        ")" |> w.appendEnd
    | TypeEnum.SortedDictionary (kType,vType) -> 
        "SortedDictionary__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__jsonCall w (indent + 1) vType
        ")" |> w.appendEnd
    | TypeEnum.ConcurrentDictionary (kType,vType) -> 
        "ConcurrentDictionary__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) kType
        ") (" |> w.appendEnd
        t__jsonCall w (indent + 1) vType
        ")" |> w.appendEnd
    | TypeEnum.ModDictInt64 tt -> 
        "ModDictInt64__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.ModDictStr tt -> 
        "ModDictStr__json (" |> w.appendEnd
        t__jsonCall w (indent + 1) tt
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
            if type__supportMarshall tt then
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
            else
                " " |> w.appendEnd
                t__emptyCall w indent tt
                " |> Some" |> w.appendEnd
            w.newlineBlank())

        "if passOptions then" |> w.newlineIndent (indent + 1) 

        "{" |> w.newlineIndent (indent + 2)
        items
        |> Array.iter(fun (name,tt) -> 
            name + " = " + name + "o.Value" |> w.newlineIndent (indent + 3))
        " } |> Some" |> w.appendEnd

        "else" |> w.newlineIndent (indent + 1) 
        "None" |> w.newlineIndent (indent + 2) 

    | TypeEnum.Product items -> 
        let items = items |> Array.filter(type__supportMarshall)
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
        "| Some e ->" |> w.newlineIndent (indent + 1)
        "match json__int32o e with" |> w.newlineIndent (indent + 2)
        "| Some i ->" |> w.newlineIndent (indent + 2)
        "match i with" |> w.newlineIndent (indent + 3)
        [| 0 .. items.Length - 1 |]
        |> Array.iter(fun i -> 
            let n,o = items[i]
            "| " + i.ToString() + " -> " |> w.newlineIndent (indent + 3)
            match o with
            | Some tt -> 
                "match json__tryFindByName json \"val\" with " |> w.newlineIndent (indent + 4)
                "| Some v ->" |> w.newlineIndent (indent + 4)
                "match " |> w.newlineIndent (indent + 5)
                json__tCall w (indent + 5) tt
                " v with" |> w.appendEnd
                "| Some vv -> vv |> " + t.name + "." + n + " |> Some" |> w.newlineIndent (indent + 5)
                "| None -> None" |> w.newlineIndent (indent + 5)
                "| None -> None" |> w.newlineIndent (indent + 4)
            | None -> t.name + "." + n + " |> Some" |> w.appendEnd)
        "| _ -> None" |> w.newlineIndent (indent + 3)
        "| None -> None" |> w.newlineIndent (indent + 2)
        "| None -> None" |> w.newlineIndent (indent + 1)

    | TypeEnum.Enum v -> ()
    | TypeEnum.OrmRcd table ->
    
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
            "let o  ="
            tab + "match"
            tab + tab + "json"
            tab + tab + "|> tryFindByAtt \"p\" with"
            tab + "| Some (s,v) -> json__p" + t.name + "o v"
            tab + "| None -> None"
            ""
            "match o with"
            "| Some p ->" |]
        |> Array.iter (w.newlineIndent (indent + 1))

        //table
        //|> table__sortedFields
        //|> Array.sortBy(fun i ->
        //    let sort,fname,def,json = i
        //    sort)
        //|> Array.map fdef__jsont
        //|> Array.map(fun i -> i.ToArray())
        //|> Array.concat
        //|> Array.iter (w.newlineIndent (indent + 2))

        [|  ""
            "{"
            tab + "ID = ID"
            tab + "Sort = Sort"
            tab + "Createdat = Createdat"
            tab + "Updatedat = Updatedat"
            tab + "p = p } |> Some" 
            "" |]
        |> Array.iter (w.newlineIndent (indent + 2))

        "| None -> None" |> w.newlineIndent (indent + 1)

    | TypeEnum.Option tt -> ()
    | TypeEnum.Ary tt -> ()
    | TypeEnum.List tt -> ()
    | TypeEnum.ListImmutable tt -> ()
    | TypeEnum.Dictionary (kType,vType) -> ()
    | TypeEnum.SortedDictionary (kType,vType) -> ()
    | TypeEnum.ModDictInt64 tt -> ()
    | TypeEnum.ModDictStr tt -> ()

and json__tCall (w:TextBlockWriter)indent t = 

    match t.tEnum with
    | TypeEnum.Primitive ->
        match t.name with
        | "string" -> "json__stro"
        | "int" -> "json__int32o"
        | "Boolean" -> "json__boolo"
        | "Json" -> "Some"
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
    | TypeEnum.OrmRcd table -> "json__" + t.name + "o" |> w.appendEnd
    | TypeEnum.Ormp table -> "json__" + t.name + "o" |> w.appendEnd
    | TypeEnum.Option tt -> 
        "json__Optiono (" |> w.appendEnd
        json__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.Ary tt -> 
        "json__arrayo (" |> w.appendEnd
        json__tCall w (indent + 1) tt
        ")" |> w.appendEnd
    | TypeEnum.ListImmutable tt -> 
        "json__ListImmutableo (" |> w.appendEnd
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
    | TypeEnum.SortedDictionary (kType,vType) -> 
        "(fun json ->" |> w.appendEnd
        "json__SortedDictionaryo (" |> w.newlineIndent (indent + 1)
        json__tCall w (indent) kType
        ") (" |> w.appendEnd
        json__tCall w (indent) vType
        ") (new SortedDictionary<" + kType.name + "," + vType.name + ">()) json)" |> w.appendEnd
    | TypeEnum.ConcurrentDictionary (kType,vType) -> 
        "(fun json ->" |> w.appendEnd
        "json__ConcurrentDictionaryo (" |> w.newlineIndent (indent + 1)
        json__tCall w (indent) kType
        ") (" |> w.appendEnd
        json__tCall w (indent) vType
        ") (new ConcurrentDictionary<" + kType.name + "," + vType.name + ">()) json)" |> w.appendEnd
    | TypeEnum.ModDictInt64 tt -> 
        "(fun json ->" |> w.appendEnd
        "json__ModDictInt64o (" |> w.appendEnd
        json__tCall w (indent + 1) tt
        ") (new Dictionary<int64," + tt.name + ">()) json)" |> w.appendEnd
    | TypeEnum.ModDictStr tt -> 
        "(fun json ->" |> w.appendEnd
        "json__ModDictStro (" |> w.appendEnd
        json__tCall w (indent + 1) tt
        ") (new Dictionary<string," + tt.name + ">()) json)" |> w.appendEnd
    | TypeEnum.Fun (kType,vType) -> ()

let clone (w:TextBlockWriter) indent (t:Type) = 
    [|  ""
        "let " + t.name + "_clone src ="
        tab + "let bb = new BytesBuilder()"
        tab + t.name + "__bin bb src"
        tab + "bin__" + t.name + " (bb.bytes(),ref 0)" |]
    |> w.multiLine

