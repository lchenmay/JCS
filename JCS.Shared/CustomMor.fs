module JCS.Shared.CustomMor

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
open JCS.Shared.OrmTypes
open JCS.Shared.Types
open JCS.Shared.OrmMor

// [EuComplex] Structure

let EuComplex_empty(): EuComplex =
    {
        eu = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pEU_empty() }
    }

let EuComplex__bin (bb:BytesBuilder) (v:EuComplex) =

    EU__bin bb v.eu

let bin__EuComplex (bi:BinIndexed):EuComplex =
    let bin,index = bi

    {
        eu = 
            bi
            |> bin__EU
    }

let EuComplex__json (v:EuComplex) =

    [|  ("eu",EU__json v.eu)
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
            match v |> json__EUo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            eu = euo.Value }:EuComplex) |> Some
    else
        None

let EuComplex_clone src =
    let bb = new BytesBuilder()
    EuComplex__bin bb src
    bin__EuComplex (bb.bytes(),ref 0)

// [FBindComplex] Structure

let FBindComplex_empty(): FBindComplex =
    {
        file = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pFILE_empty() }
        fbind = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pFBIND_empty() }
    }

let FBindComplex__bin (bb:BytesBuilder) (v:FBindComplex) =

    FILE__bin bb v.file
    FBIND__bin bb v.fbind

let bin__FBindComplex (bi:BinIndexed):FBindComplex =
    let bin,index = bi

    {
        file = 
            bi
            |> bin__FILE
        fbind = 
            bi
            |> bin__FBIND
    }

let FBindComplex__json (v:FBindComplex) =

    [|  ("file",FILE__json v.file)
        ("fbind",FBIND__json v.fbind)
         |]
    |> Json.Braket

let FBindComplex__jsonTbw (w:TextBlockWriter) (v:FBindComplex) =
    json__str w (FBindComplex__json v)

let FBindComplex__jsonStr (v:FBindComplex) =
    (FBindComplex__json v) |> json__strFinal


let json__FBindComplexo (json:Json):FBindComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let fileo =
        match json__tryFindByName json "file" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__FILEo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let fbindo =
        match json__tryFindByName json "fbind" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__FBINDo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            file = fileo.Value
            fbind = fbindo.Value }:FBindComplex) |> Some
    else
        None

let FBindComplex_clone src =
    let bb = new BytesBuilder()
    FBindComplex__bin bb src
    bin__FBindComplex (bb.bytes(),ref 0)

// [MomentComplex] Structure

let MomentComplex_empty(): MomentComplex =
    {
        fbxs = [| |]
        m = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pMOMENT_empty() }
    }

let MomentComplex__bin (bb:BytesBuilder) (v:MomentComplex) =

    
    array__bin (FBindComplex__bin) bb v.fbxs
    MOMENT__bin bb v.m

let bin__MomentComplex (bi:BinIndexed):MomentComplex =
    let bin,index = bi

    {
        fbxs = 
            bi
            |> bin__array (bin__FBindComplex)
        m = 
            bi
            |> bin__MOMENT
    }

let MomentComplex__json (v:MomentComplex) =

    [|  ("fbxs",array__json (FBindComplex__json) v.fbxs)
        ("m",MOMENT__json v.m)
         |]
    |> Json.Braket

let MomentComplex__jsonTbw (w:TextBlockWriter) (v:MomentComplex) =
    json__str w (MomentComplex__json v)

let MomentComplex__jsonStr (v:MomentComplex) =
    (MomentComplex__json v) |> json__strFinal


let json__MomentComplexo (json:Json):MomentComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let fbxso =
        match json__tryFindByName json "fbxs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__arrayo (json__FBindComplexo) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let mo =
        match json__tryFindByName json "m" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__MOMENTo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            fbxs = fbxso.Value
            m = mo.Value }:MomentComplex) |> Some
    else
        None

let MomentComplex_clone src =
    let bb = new BytesBuilder()
    MomentComplex__bin bb src
    bin__MomentComplex (bb.bytes(),ref 0)

// [TableComplex] Structure

let TableComplex_empty(): TableComplex =
    {
        fields = ModDict_empty()
        table = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pTABLE_empty() }
    }

let TableComplex__bin (bb:BytesBuilder) (v:TableComplex) =

    
    ModDictStr__bin (FIELD__bin) bb v.fields
    TABLE__bin bb v.table

let bin__TableComplex (bi:BinIndexed):TableComplex =
    let bin,index = bi

    {
        fields = 
            bi
            |> bin__ModDictStr(bin__FIELD)
        table = 
            bi
            |> bin__TABLE
    }

let TableComplex__json (v:TableComplex) =

    [|  ("fields",ModDictStr__json (FIELD__json) v.fields)
        ("table",TABLE__json v.table)
         |]
    |> Json.Braket

let TableComplex__jsonTbw (w:TextBlockWriter) (v:TableComplex) =
    json__str w (TableComplex__json v)

let TableComplex__jsonStr (v:TableComplex) =
    (TableComplex__json v) |> json__strFinal


let json__TableComplexo (json:Json):TableComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let fieldso =
        match json__tryFindByName json "fields" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__FIELDo) (new Dictionary<string,FIELD>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let tableo =
        match json__tryFindByName json "table" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__TABLEo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            fields = fieldso.Value
            table = tableo.Value }:TableComplex) |> Some
    else
        None

let TableComplex_clone src =
    let bb = new BytesBuilder()
    TableComplex__bin bb src
    bin__TableComplex (bb.bytes(),ref 0)

// [CompComplex] Structure

let CompComplex_empty(): CompComplex =
    {
        states = ModDict_empty()
        props = ModDict_empty()
        comp = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pCOMP_empty() }
    }

let CompComplex__bin (bb:BytesBuilder) (v:CompComplex) =

    
    ModDictStr__bin (VARTYPE__bin) bb v.states
    
    ModDictStr__bin (VARTYPE__bin) bb v.props
    COMP__bin bb v.comp

let bin__CompComplex (bi:BinIndexed):CompComplex =
    let bin,index = bi

    {
        states = 
            bi
            |> bin__ModDictStr(bin__VARTYPE)
        props = 
            bi
            |> bin__ModDictStr(bin__VARTYPE)
        comp = 
            bi
            |> bin__COMP
    }

let CompComplex__json (v:CompComplex) =

    [|  ("states",ModDictStr__json (VARTYPE__json) v.states)
        ("props",ModDictStr__json (VARTYPE__json) v.props)
        ("comp",COMP__json v.comp)
         |]
    |> Json.Braket

let CompComplex__jsonTbw (w:TextBlockWriter) (v:CompComplex) =
    json__str w (CompComplex__json v)

let CompComplex__jsonStr (v:CompComplex) =
    (CompComplex__json v) |> json__strFinal


let json__CompComplexo (json:Json):CompComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let stateso =
        match json__tryFindByName json "states" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__VARTYPEo) (new Dictionary<string,VARTYPE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let propso =
        match json__tryFindByName json "props" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__VARTYPEo) (new Dictionary<string,VARTYPE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let compo =
        match json__tryFindByName json "comp" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__COMPo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            states = stateso.Value
            props = propso.Value
            comp = compo.Value }:CompComplex) |> Some
    else
        None

let CompComplex_clone src =
    let bb = new BytesBuilder()
    CompComplex__bin bb src
    bin__CompComplex (bb.bytes(),ref 0)

// [PageComplex] Structure

let PageComplex_empty(): PageComplex =
    {
        states = ModDict_empty()
        props = ModDict_empty()
        page = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pPAGE_empty() }
    }

let PageComplex__bin (bb:BytesBuilder) (v:PageComplex) =

    
    ModDictStr__bin (VARTYPE__bin) bb v.states
    
    ModDictStr__bin (VARTYPE__bin) bb v.props
    PAGE__bin bb v.page

let bin__PageComplex (bi:BinIndexed):PageComplex =
    let bin,index = bi

    {
        states = 
            bi
            |> bin__ModDictStr(bin__VARTYPE)
        props = 
            bi
            |> bin__ModDictStr(bin__VARTYPE)
        page = 
            bi
            |> bin__PAGE
    }

let PageComplex__json (v:PageComplex) =

    [|  ("states",ModDictStr__json (VARTYPE__json) v.states)
        ("props",ModDictStr__json (VARTYPE__json) v.props)
        ("page",PAGE__json v.page)
         |]
    |> Json.Braket

let PageComplex__jsonTbw (w:TextBlockWriter) (v:PageComplex) =
    json__str w (PageComplex__json v)

let PageComplex__jsonStr (v:PageComplex) =
    (PageComplex__json v) |> json__strFinal


let json__PageComplexo (json:Json):PageComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let stateso =
        match json__tryFindByName json "states" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__VARTYPEo) (new Dictionary<string,VARTYPE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let propso =
        match json__tryFindByName json "props" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__VARTYPEo) (new Dictionary<string,VARTYPE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let pageo =
        match json__tryFindByName json "page" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__PAGEo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            states = stateso.Value
            props = propso.Value
            page = pageo.Value }:PageComplex) |> Some
    else
        None

let PageComplex_clone src =
    let bb = new BytesBuilder()
    PageComplex__bin bb src
    bin__PageComplex (bb.bytes(),ref 0)

// [ApiComplex] Structure

let ApiComplex_empty(): ApiComplex =
    {
        reqs = ModDict_empty()
        reps = ModDict_empty()
        api = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pAPI_empty() }
    }

let ApiComplex__bin (bb:BytesBuilder) (v:ApiComplex) =

    
    ModDictStr__bin (VARTYPE__bin) bb v.reqs
    
    ModDictStr__bin (VARTYPE__bin) bb v.reps
    API__bin bb v.api

let bin__ApiComplex (bi:BinIndexed):ApiComplex =
    let bin,index = bi

    {
        reqs = 
            bi
            |> bin__ModDictStr(bin__VARTYPE)
        reps = 
            bi
            |> bin__ModDictStr(bin__VARTYPE)
        api = 
            bi
            |> bin__API
    }

let ApiComplex__json (v:ApiComplex) =

    [|  ("reqs",ModDictStr__json (VARTYPE__json) v.reqs)
        ("reps",ModDictStr__json (VARTYPE__json) v.reps)
        ("api",API__json v.api)
         |]
    |> Json.Braket

let ApiComplex__jsonTbw (w:TextBlockWriter) (v:ApiComplex) =
    json__str w (ApiComplex__json v)

let ApiComplex__jsonStr (v:ApiComplex) =
    (ApiComplex__json v) |> json__strFinal


let json__ApiComplexo (json:Json):ApiComplex option =
    let fields = json |> json__items

    let mutable passOptions = true

    let reqso =
        match json__tryFindByName json "reqs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__VARTYPEo) (new Dictionary<string,VARTYPE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let repso =
        match json__tryFindByName json "reps" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__VARTYPEo) (new Dictionary<string,VARTYPE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let apio =
        match json__tryFindByName json "api" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__APIo with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            reqs = reqso.Value
            reps = repso.Value
            api = apio.Value }:ApiComplex) |> Some
    else
        None

let ApiComplex_clone src =
    let bb = new BytesBuilder()
    ApiComplex__bin bb src
    bin__ApiComplex (bb.bytes(),ref 0)

// [ProjectComplex] Structure

let ProjectComplex_empty(): ProjectComplex =
    {
        hostconfigs = ModDict_empty()
        tablexs = ModDict_empty()
        compxs = ModDict_empty()
        templatexs = ModDict_empty()
        pagexs = ModDict_empty()
        apixs = ModDict_empty()
        project = { ID = 0L; Sort = 0L; Createdat = DateTime.MinValue; Updatedat = DateTime.MinValue; p = pPROJECT_empty() }
    }

let ProjectComplex__bin (bb:BytesBuilder) (v:ProjectComplex) =

    
    ModDictStr__bin (HOSTCONFIG__bin) bb v.hostconfigs
    
    ModDictStr__bin (TableComplex__bin) bb v.tablexs
    
    ModDictStr__bin (CompComplex__bin) bb v.compxs
    
    ModDictStr__bin (TEMPLATE__bin) bb v.templatexs
    
    ModDictStr__bin (PageComplex__bin) bb v.pagexs
    
    ModDictStr__bin (ApiComplex__bin) bb v.apixs
    PROJECT__bin bb v.project

let bin__ProjectComplex (bi:BinIndexed):ProjectComplex =
    let bin,index = bi

    {
        hostconfigs = 
            bi
            |> bin__ModDictStr(bin__HOSTCONFIG)
        tablexs = 
            bi
            |> bin__ModDictStr(bin__TableComplex)
        compxs = 
            bi
            |> bin__ModDictStr(bin__CompComplex)
        templatexs = 
            bi
            |> bin__ModDictStr(bin__TEMPLATE)
        pagexs = 
            bi
            |> bin__ModDictStr(bin__PageComplex)
        apixs = 
            bi
            |> bin__ModDictStr(bin__ApiComplex)
        project = 
            bi
            |> bin__PROJECT
    }

let ProjectComplex__json (v:ProjectComplex) =

    [|  ("hostconfigs",ModDictStr__json (HOSTCONFIG__json) v.hostconfigs)
        ("tablexs",ModDictStr__json (TableComplex__json) v.tablexs)
        ("compxs",ModDictStr__json (CompComplex__json) v.compxs)
        ("templatexs",ModDictStr__json (TEMPLATE__json) v.templatexs)
        ("pagexs",ModDictStr__json (PageComplex__json) v.pagexs)
        ("apixs",ModDictStr__json (ApiComplex__json) v.apixs)
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

    let hostconfigso =
        match json__tryFindByName json "hostconfigs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__HOSTCONFIGo) (new Dictionary<string,HOSTCONFIG>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let tablexso =
        match json__tryFindByName json "tablexs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__TableComplexo) (new Dictionary<string,TableComplex>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let compxso =
        match json__tryFindByName json "compxs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__CompComplexo) (new Dictionary<string,CompComplex>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let templatexso =
        match json__tryFindByName json "templatexs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__TEMPLATEo) (new Dictionary<string,TEMPLATE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let pagexso =
        match json__tryFindByName json "pagexs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__PageComplexo) (new Dictionary<string,PageComplex>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let apixso =
        match json__tryFindByName json "apixs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictStro (json__ApiComplexo) (new Dictionary<string,ApiComplex>()) json) with
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
        ({
            hostconfigs = hostconfigso.Value
            tablexs = tablexso.Value
            compxs = compxso.Value
            templatexs = templatexso.Value
            pagexs = pagexso.Value
            apixs = apixso.Value
            project = projecto.Value }:ProjectComplex) |> Some
    else
        None

let ProjectComplex_clone src =
    let bb = new BytesBuilder()
    ProjectComplex__bin bb src
    bin__ProjectComplex (bb.bytes(),ref 0)

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

let Fact_clone src =
    let bb = new BytesBuilder()
    Fact__bin bb src
    bin__Fact (bb.bytes(),ref 0)

// [RuntimeData] Structure

let RuntimeData_empty(): RuntimeData =
    {
        facts = []
        projectxs = ModDict_empty()
        files = ModDict_empty()
        mxs = ModDict_empty()
        books = new List<BOOK>()
    }

let RuntimeData__bin (bb:BytesBuilder) (v:RuntimeData) =

    
    ListImmutable__bin (Fact__bin) bb v.facts
    
    ModDictInt64__bin (ProjectComplex__bin) bb v.projectxs
    
    ModDictInt64__bin (FILE__bin) bb v.files
    
    ModDictInt64__bin (MomentComplex__bin) bb v.mxs
    
    List__bin (BOOK__bin) bb v.books

let bin__RuntimeData (bi:BinIndexed):RuntimeData =
    let bin,index = bi

    {
        facts = 
            bi
            |> bin__ListImmutable (bin__Fact)
        projectxs = 
            bi
            |> bin__ModDictInt64(bin__ProjectComplex)
        files = 
            bi
            |> bin__ModDictInt64(bin__FILE)
        mxs = 
            bi
            |> bin__ModDictInt64(bin__MomentComplex)
        books = 
            bi
            |> bin__List (bin__BOOK)
    }

let RuntimeData__json (v:RuntimeData) =

    [|  ("facts",ListImmutable__json (Fact__json) v.facts)
        ("projectxs",ModDictInt64__json (ProjectComplex__json) v.projectxs)
        ("files",ModDictInt64__json (FILE__json) v.files)
        ("mxs",ModDictInt64__json (MomentComplex__json) v.mxs)
        ("books",List__json (BOOK__json) v.books)
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

    let projectxso =
        match json__tryFindByName json "projectxs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictInt64o (json__ProjectComplexo) (new Dictionary<int64,ProjectComplex>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let fileso =
        match json__tryFindByName json "files" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictInt64o (json__FILEo) (new Dictionary<int64,FILE>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let mxso =
        match json__tryFindByName json "mxs" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> (fun json ->json__ModDictInt64o (json__MomentComplexo) (new Dictionary<int64,MomentComplex>()) json) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    let bookso =
        match json__tryFindByName json "books" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__Listo (json__BOOKo) with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            facts = factso.Value
            projectxs = projectxso.Value
            files = fileso.Value
            mxs = mxso.Value
            books = bookso.Value }:RuntimeData) |> Some
    else
        None

let RuntimeData_clone src =
    let bb = new BytesBuilder()
    RuntimeData__bin bb src
    bin__RuntimeData (bb.bytes(),ref 0)

// [ClientRuntime] Structure

let ClientRuntime_empty(): ClientRuntime =
    {
        version = 0
    }

let ClientRuntime__bin (bb:BytesBuilder) (v:ClientRuntime) =

    int32__bin bb v.version

let bin__ClientRuntime (bi:BinIndexed):ClientRuntime =
    let bin,index = bi

    {
        version = 
            bi
            |> bin__int32
    }

let ClientRuntime__json (v:ClientRuntime) =

    [|  ("version",int32__json v.version)
         |]
    |> Json.Braket

let ClientRuntime__jsonTbw (w:TextBlockWriter) (v:ClientRuntime) =
    json__str w (ClientRuntime__json v)

let ClientRuntime__jsonStr (v:ClientRuntime) =
    (ClientRuntime__json v) |> json__strFinal


let json__ClientRuntimeo (json:Json):ClientRuntime option =
    let fields = json |> json__items

    let mutable passOptions = true

    let versiono =
        match json__tryFindByName json "version" with
        | None ->
            passOptions <- false
            None
        | Some v -> 
            match v |> json__int32o with
            | Some res -> Some res
            | None ->
                passOptions <- false
                None

    if passOptions then
        ({
            version = versiono.Value }:ClientRuntime) |> Some
    else
        None

let ClientRuntime_clone src =
    let bb = new BytesBuilder()
    ClientRuntime__bin bb src
    bin__ClientRuntime (bb.bytes(),ref 0)

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

let Msg_clone src =
    let bb = new BytesBuilder()
    Msg__bin bb src
    bin__Msg (bb.bytes(),ref 0)

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

let Er_clone src =
    let bb = new BytesBuilder()
    Er__bin bb src
    bin__Er (bb.bytes(),ref 0)