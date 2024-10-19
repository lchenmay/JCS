module Shared.OrmMor

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

open System.Data.SqlClient
open System.Threading
open Util.Bin
open Shared.OrmTypes
open Shared.Types

// [FIELD] Structure


let pFIELD__bin (bb:BytesBuilder) (p:pFIELD) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    let binDesc = p.Desc |> Encoding.UTF8.GetBytes
    binDesc.Length |> BitConverter.GetBytes |> bb.append
    binDesc |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append
    
    p.Table |> BitConverter.GetBytes |> bb.append

let FIELD__bin (bb:BytesBuilder) (v:FIELD) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pFIELD__bin bb v.p

let bin__pFIELD (bi:BinIndexed):pFIELD =
    let bin,index = bi

    let p = pFIELD_empty()
    
    let count_Name = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Name <- Encoding.UTF8.GetString(bin,index.Value,count_Name)
    index.Value <- index.Value + count_Name
    
    let count_Desc = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Desc <- Encoding.UTF8.GetString(bin,index.Value,count_Desc)
    index.Value <- index.Value + count_Desc
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Table <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__FIELD (bi:BinIndexed):FIELD =
    let bin,index = bi

    let ID = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Sort = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Createdat = bin__DateTime bi
    
    let Updatedat = bin__DateTime bi
    
    {
        ID = ID
        Sort = Sort
        Createdat = Createdat
        Updatedat = Updatedat
        p = bin__pFIELD bi }

let pFIELD__json (p:pFIELD) =

    [|
        ("Name",p.Name |> Json.Str)
        ("Desc",p.Desc |> Json.Str)
        ("Project",p.Project.ToString() |> Json.Num)
        ("Table",p.Table.ToString() |> Json.Num) |]
    |> Json.Braket

let FIELD__json (v:FIELD) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pFIELD__json v.p) |]
    |> Json.Braket

let FIELD__jsonTbw (w:TextBlockWriter) (v:FIELD) =
    json__str w (FIELD__json v)

let FIELD__jsonStr (v:FIELD) =
    (FIELD__json v) |> json__strFinal


let json__pFIELDo (json:Json):pFIELD option =
    let fields = json |> json__items

    let p = pFIELD_empty()
    
    p.Name <- checkfieldz fields "Name" 64
    
    p.Desc <- checkfield fields "Desc"
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p.Table <- checkfield fields "Table" |> parse_int64
    
    p |> Some
    

let json__FIELDo (json:Json):FIELD option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pFIELDo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Name <- checkfieldz fields "Name" 64
        
        p.Desc <- checkfield fields "Desc"
        
        p.Project <- checkfield fields "Project" |> parse_int64
        
        p.Table <- checkfield fields "Table" |> parse_int64
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [PROJECT] Structure


let pPROJECT__bin (bb:BytesBuilder) (p:pPROJECT) =

    
    let binCode = p.Code |> Encoding.UTF8.GetBytes
    binCode.Length |> BitConverter.GetBytes |> bb.append
    binCode |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append

let PROJECT__bin (bb:BytesBuilder) (v:PROJECT) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pPROJECT__bin bb v.p

let bin__pPROJECT (bi:BinIndexed):pPROJECT =
    let bin,index = bi

    let p = pPROJECT_empty()
    
    let count_Code = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Code <- Encoding.UTF8.GetString(bin,index.Value,count_Code)
    index.Value <- index.Value + count_Code
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p

let bin__PROJECT (bi:BinIndexed):PROJECT =
    let bin,index = bi

    let ID = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Sort = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Createdat = bin__DateTime bi
    
    let Updatedat = bin__DateTime bi
    
    {
        ID = ID
        Sort = Sort
        Createdat = Createdat
        Updatedat = Updatedat
        p = bin__pPROJECT bi }

let pPROJECT__json (p:pPROJECT) =

    [|
        ("Code",p.Code |> Json.Str)
        ("Caption",p.Caption |> Json.Str) |]
    |> Json.Braket

let PROJECT__json (v:PROJECT) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pPROJECT__json v.p) |]
    |> Json.Braket

let PROJECT__jsonTbw (w:TextBlockWriter) (v:PROJECT) =
    json__str w (PROJECT__json v)

let PROJECT__jsonStr (v:PROJECT) =
    (PROJECT__json v) |> json__strFinal


let json__pPROJECTo (json:Json):pPROJECT option =
    let fields = json |> json__items

    let p = pPROJECT_empty()
    
    p.Code <- checkfieldz fields "Code" 64
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p |> Some
    

let json__PROJECTo (json:Json):PROJECT option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pPROJECTo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Code <- checkfieldz fields "Code" 64
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [TABLE] Structure


let pTABLE__bin (bb:BytesBuilder) (p:pTABLE) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    let binDesc = p.Desc |> Encoding.UTF8.GetBytes
    binDesc.Length |> BitConverter.GetBytes |> bb.append
    binDesc |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append

let TABLE__bin (bb:BytesBuilder) (v:TABLE) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pTABLE__bin bb v.p

let bin__pTABLE (bi:BinIndexed):pTABLE =
    let bin,index = bi

    let p = pTABLE_empty()
    
    let count_Name = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Name <- Encoding.UTF8.GetString(bin,index.Value,count_Name)
    index.Value <- index.Value + count_Name
    
    let count_Desc = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Desc <- Encoding.UTF8.GetString(bin,index.Value,count_Desc)
    index.Value <- index.Value + count_Desc
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__TABLE (bi:BinIndexed):TABLE =
    let bin,index = bi

    let ID = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Sort = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Createdat = bin__DateTime bi
    
    let Updatedat = bin__DateTime bi
    
    {
        ID = ID
        Sort = Sort
        Createdat = Createdat
        Updatedat = Updatedat
        p = bin__pTABLE bi }

let pTABLE__json (p:pTABLE) =

    [|
        ("Name",p.Name |> Json.Str)
        ("Desc",p.Desc |> Json.Str)
        ("Project",p.Project.ToString() |> Json.Num) |]
    |> Json.Braket

let TABLE__json (v:TABLE) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pTABLE__json v.p) |]
    |> Json.Braket

let TABLE__jsonTbw (w:TextBlockWriter) (v:TABLE) =
    json__str w (TABLE__json v)

let TABLE__jsonStr (v:TABLE) =
    (TABLE__json v) |> json__strFinal


let json__pTABLEo (json:Json):pTABLE option =
    let fields = json |> json__items

    let p = pTABLE_empty()
    
    p.Name <- checkfieldz fields "Name" 64
    
    p.Desc <- checkfield fields "Desc"
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p |> Some
    

let json__TABLEo (json:Json):TABLE option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pTABLEo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Name <- checkfieldz fields "Name" 64
        
        p.Desc <- checkfield fields "Desc"
        
        p.Project <- checkfield fields "Project" |> parse_int64
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [COMP] Structure


let pCOMP__bin (bb:BytesBuilder) (p:pCOMP) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append

let COMP__bin (bb:BytesBuilder) (v:COMP) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pCOMP__bin bb v.p

let bin__pCOMP (bi:BinIndexed):pCOMP =
    let bin,index = bi

    let p = pCOMP_empty()
    
    let count_Name = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Name <- Encoding.UTF8.GetString(bin,index.Value,count_Name)
    index.Value <- index.Value + count_Name
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__COMP (bi:BinIndexed):COMP =
    let bin,index = bi

    let ID = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Sort = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Createdat = bin__DateTime bi
    
    let Updatedat = bin__DateTime bi
    
    {
        ID = ID
        Sort = Sort
        Createdat = Createdat
        Updatedat = Updatedat
        p = bin__pCOMP bi }

let pCOMP__json (p:pCOMP) =

    [|
        ("Name",p.Name |> Json.Str)
        ("Caption",p.Caption |> Json.Str)
        ("Project",p.Project.ToString() |> Json.Num) |]
    |> Json.Braket

let COMP__json (v:COMP) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pCOMP__json v.p) |]
    |> Json.Braket

let COMP__jsonTbw (w:TextBlockWriter) (v:COMP) =
    json__str w (COMP__json v)

let COMP__jsonStr (v:COMP) =
    (COMP__json v) |> json__strFinal


let json__pCOMPo (json:Json):pCOMP option =
    let fields = json |> json__items

    let p = pCOMP_empty()
    
    p.Name <- checkfieldz fields "Name" 64
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p |> Some
    

let json__COMPo (json:Json):COMP option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pCOMPo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Name <- checkfieldz fields "Name" 64
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        p.Project <- checkfield fields "Project" |> parse_int64
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [PAGE] Structure


let pPAGE__bin (bb:BytesBuilder) (p:pPAGE) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    let binOgTitle = p.OgTitle |> Encoding.UTF8.GetBytes
    binOgTitle.Length |> BitConverter.GetBytes |> bb.append
    binOgTitle |> bb.append
    
    let binOgDesc = p.OgDesc |> Encoding.UTF8.GetBytes
    binOgDesc.Length |> BitConverter.GetBytes |> bb.append
    binOgDesc |> bb.append
    
    let binOgImage = p.OgImage |> Encoding.UTF8.GetBytes
    binOgImage.Length |> BitConverter.GetBytes |> bb.append
    binOgImage |> bb.append
    
    p.Template |> BitConverter.GetBytes |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append

let PAGE__bin (bb:BytesBuilder) (v:PAGE) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pPAGE__bin bb v.p

let bin__pPAGE (bi:BinIndexed):pPAGE =
    let bin,index = bi

    let p = pPAGE_empty()
    
    let count_Name = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Name <- Encoding.UTF8.GetString(bin,index.Value,count_Name)
    index.Value <- index.Value + count_Name
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    let count_OgTitle = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.OgTitle <- Encoding.UTF8.GetString(bin,index.Value,count_OgTitle)
    index.Value <- index.Value + count_OgTitle
    
    let count_OgDesc = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.OgDesc <- Encoding.UTF8.GetString(bin,index.Value,count_OgDesc)
    index.Value <- index.Value + count_OgDesc
    
    let count_OgImage = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.OgImage <- Encoding.UTF8.GetString(bin,index.Value,count_OgImage)
    index.Value <- index.Value + count_OgImage
    
    p.Template <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__PAGE (bi:BinIndexed):PAGE =
    let bin,index = bi

    let ID = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Sort = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Createdat = bin__DateTime bi
    
    let Updatedat = bin__DateTime bi
    
    {
        ID = ID
        Sort = Sort
        Createdat = Createdat
        Updatedat = Updatedat
        p = bin__pPAGE bi }

let pPAGE__json (p:pPAGE) =

    [|
        ("Name",p.Name |> Json.Str)
        ("Caption",p.Caption |> Json.Str)
        ("OgTitle",p.OgTitle |> Json.Str)
        ("OgDesc",p.OgDesc |> Json.Str)
        ("OgImage",p.OgImage |> Json.Str)
        ("Template",p.Template.ToString() |> Json.Num)
        ("Project",p.Project.ToString() |> Json.Num) |]
    |> Json.Braket

let PAGE__json (v:PAGE) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pPAGE__json v.p) |]
    |> Json.Braket

let PAGE__jsonTbw (w:TextBlockWriter) (v:PAGE) =
    json__str w (PAGE__json v)

let PAGE__jsonStr (v:PAGE) =
    (PAGE__json v) |> json__strFinal


let json__pPAGEo (json:Json):pPAGE option =
    let fields = json |> json__items

    let p = pPAGE_empty()
    
    p.Name <- checkfieldz fields "Name" 64
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.OgTitle <- checkfield fields "OgTitle"
    
    p.OgDesc <- checkfield fields "OgDesc"
    
    p.OgImage <- checkfield fields "OgImage"
    
    p.Template <- checkfield fields "Template" |> parse_int64
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p |> Some
    

let json__PAGEo (json:Json):PAGE option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pPAGEo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Name <- checkfieldz fields "Name" 64
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        p.OgTitle <- checkfield fields "OgTitle"
        
        p.OgDesc <- checkfield fields "OgDesc"
        
        p.OgImage <- checkfield fields "OgImage"
        
        p.Template <- checkfield fields "Template" |> parse_int64
        
        p.Project <- checkfield fields "Project" |> parse_int64
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [TEMPLATE] Structure


let pTEMPLATE__bin (bb:BytesBuilder) (p:pTEMPLATE) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append

let TEMPLATE__bin (bb:BytesBuilder) (v:TEMPLATE) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pTEMPLATE__bin bb v.p

let bin__pTEMPLATE (bi:BinIndexed):pTEMPLATE =
    let bin,index = bi

    let p = pTEMPLATE_empty()
    
    let count_Name = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Name <- Encoding.UTF8.GetString(bin,index.Value,count_Name)
    index.Value <- index.Value + count_Name
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__TEMPLATE (bi:BinIndexed):TEMPLATE =
    let bin,index = bi

    let ID = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Sort = BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let Createdat = bin__DateTime bi
    
    let Updatedat = bin__DateTime bi
    
    {
        ID = ID
        Sort = Sort
        Createdat = Createdat
        Updatedat = Updatedat
        p = bin__pTEMPLATE bi }

let pTEMPLATE__json (p:pTEMPLATE) =

    [|
        ("Name",p.Name |> Json.Str)
        ("Caption",p.Caption |> Json.Str)
        ("Project",p.Project.ToString() |> Json.Num) |]
    |> Json.Braket

let TEMPLATE__json (v:TEMPLATE) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pTEMPLATE__json v.p) |]
    |> Json.Braket

let TEMPLATE__jsonTbw (w:TextBlockWriter) (v:TEMPLATE) =
    json__str w (TEMPLATE__json v)

let TEMPLATE__jsonStr (v:TEMPLATE) =
    (TEMPLATE__json v) |> json__strFinal


let json__pTEMPLATEo (json:Json):pTEMPLATE option =
    let fields = json |> json__items

    let p = pTEMPLATE_empty()
    
    p.Name <- checkfieldz fields "Name" 64
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p |> Some
    

let json__TEMPLATEo (json:Json):TEMPLATE option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pTEMPLATEo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Name <- checkfieldz fields "Name" 64
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        p.Project <- checkfield fields "Project" |> parse_int64
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

let mutable conn = ""

let db__pFIELD(line:Object[]): pFIELD =
    let p = pFIELD_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Desc <- string(line.[5]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64
    p.Table <- if Convert.IsDBNull(line.[7]) then 0L else line.[7] :?> int64

    p

let pFIELD__sps (p:pFIELD) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Desc", p.Desc) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam
            ("Table", p.Table) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("desc", p.Desc) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam
            ("table", p.Table) |> kvp__sqlparam |]

let db__FIELD = db__Rcd db__pFIELD

let FIELD_wrapper item: FIELD =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pFIELD_clone (p:pFIELD): pFIELD = {
    Name = p.Name
    Desc = p.Desc
    Project = p.Project
    Table = p.Table }

let FIELD_update_transaction output (updater,suc,fail) (rcd:FIELD) =
    let rollback_p = rcd.p |> pFIELD_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,FIELD_table,FIELD_sql_update(),pFIELD__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let FIELD_update output (rcd:FIELD) =
    rcd
    |> FIELD_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let FIELD_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment FIELD_id
    let ctime = DateTime.UtcNow
    match create (conn,output,FIELD_table,pFIELD__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> FIELD_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let FIELD_create output p =
    FIELD_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__FIELDo id: FIELD option = id__rcd(conn,FIELD_fieldorders(),FIELD_table,db__FIELD) id

let FIELD_metadata = {
    fieldorders = FIELD_fieldorders
    db__rcd = db__FIELD 
    wrapper = FIELD_wrapper
    sps = pFIELD__sps
    id = FIELD_id
    id__rcdo = id__FIELDo
    clone = pFIELD_clone
    empty__p = pFIELD_empty
    rcd__bin = FIELD__bin
    bin__rcd = bin__FIELD
    sql_update = FIELD_sql_update
    rcd_update = FIELD_update
    table = FIELD_table
    shorthand = "field" }

let FIELDTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Field' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_Field ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Name]
    ,[Desc]
    ,[Project]
    ,[Table])
    END
    """


let db__pPROJECT(line:Object[]): pPROJECT =
    let p = pPROJECT_empty()

    p.Code <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()

    p

let pPROJECT__sps (p:pPROJECT) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Code", p.Code) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("code", p.Code) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam |]

let db__PROJECT = db__Rcd db__pPROJECT

let PROJECT_wrapper item: PROJECT =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pPROJECT_clone (p:pPROJECT): pPROJECT = {
    Code = p.Code
    Caption = p.Caption }

let PROJECT_update_transaction output (updater,suc,fail) (rcd:PROJECT) =
    let rollback_p = rcd.p |> pPROJECT_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,PROJECT_table,PROJECT_sql_update(),pPROJECT__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let PROJECT_update output (rcd:PROJECT) =
    rcd
    |> PROJECT_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let PROJECT_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment PROJECT_id
    let ctime = DateTime.UtcNow
    match create (conn,output,PROJECT_table,pPROJECT__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> PROJECT_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let PROJECT_create output p =
    PROJECT_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__PROJECTo id: PROJECT option = id__rcd(conn,PROJECT_fieldorders(),PROJECT_table,db__PROJECT) id

let PROJECT_metadata = {
    fieldorders = PROJECT_fieldorders
    db__rcd = db__PROJECT 
    wrapper = PROJECT_wrapper
    sps = pPROJECT__sps
    id = PROJECT_id
    id__rcdo = id__PROJECTo
    clone = pPROJECT_clone
    empty__p = pPROJECT_empty
    rcd__bin = PROJECT__bin
    bin__rcd = bin__PROJECT
    sql_update = PROJECT_sql_update
    rcd_update = PROJECT_update
    table = PROJECT_table
    shorthand = "project" }

let PROJECTTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Project' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_Project ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Code]
    ,[Caption])
    END
    """


let db__pTABLE(line:Object[]): pTABLE =
    let p = pTABLE_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Desc <- string(line.[5]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64

    p

let pTABLE__sps (p:pTABLE) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Desc", p.Desc) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("desc", p.Desc) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam |]

let db__TABLE = db__Rcd db__pTABLE

let TABLE_wrapper item: TABLE =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pTABLE_clone (p:pTABLE): pTABLE = {
    Name = p.Name
    Desc = p.Desc
    Project = p.Project }

let TABLE_update_transaction output (updater,suc,fail) (rcd:TABLE) =
    let rollback_p = rcd.p |> pTABLE_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,TABLE_table,TABLE_sql_update(),pTABLE__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let TABLE_update output (rcd:TABLE) =
    rcd
    |> TABLE_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let TABLE_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment TABLE_id
    let ctime = DateTime.UtcNow
    match create (conn,output,TABLE_table,pTABLE__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> TABLE_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let TABLE_create output p =
    TABLE_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__TABLEo id: TABLE option = id__rcd(conn,TABLE_fieldorders(),TABLE_table,db__TABLE) id

let TABLE_metadata = {
    fieldorders = TABLE_fieldorders
    db__rcd = db__TABLE 
    wrapper = TABLE_wrapper
    sps = pTABLE__sps
    id = TABLE_id
    id__rcdo = id__TABLEo
    clone = pTABLE_clone
    empty__p = pTABLE_empty
    rcd__bin = TABLE__bin
    bin__rcd = bin__TABLE
    sql_update = TABLE_sql_update
    rcd_update = TABLE_update
    table = TABLE_table
    shorthand = "table" }

let TABLETxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Table' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_Table ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Name]
    ,[Desc]
    ,[Project])
    END
    """


let db__pCOMP(line:Object[]): pCOMP =
    let p = pCOMP_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64

    p

let pCOMP__sps (p:pCOMP) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam |]

let db__COMP = db__Rcd db__pCOMP

let COMP_wrapper item: COMP =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pCOMP_clone (p:pCOMP): pCOMP = {
    Name = p.Name
    Caption = p.Caption
    Project = p.Project }

let COMP_update_transaction output (updater,suc,fail) (rcd:COMP) =
    let rollback_p = rcd.p |> pCOMP_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,COMP_table,COMP_sql_update(),pCOMP__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let COMP_update output (rcd:COMP) =
    rcd
    |> COMP_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let COMP_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment COMP_id
    let ctime = DateTime.UtcNow
    match create (conn,output,COMP_table,pCOMP__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> COMP_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let COMP_create output p =
    COMP_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__COMPo id: COMP option = id__rcd(conn,COMP_fieldorders(),COMP_table,db__COMP) id

let COMP_metadata = {
    fieldorders = COMP_fieldorders
    db__rcd = db__COMP 
    wrapper = COMP_wrapper
    sps = pCOMP__sps
    id = COMP_id
    id__rcdo = id__COMPo
    clone = pCOMP_clone
    empty__p = pCOMP_empty
    rcd__bin = COMP__bin
    bin__rcd = bin__COMP
    sql_update = COMP_sql_update
    rcd_update = COMP_update
    table = COMP_table
    shorthand = "comp" }

let COMPTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiComponent' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_UiComponent ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Name]
    ,[Caption]
    ,[Project])
    END
    """


let db__pPAGE(line:Object[]): pPAGE =
    let p = pPAGE_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.OgTitle <- string(line.[6]).TrimEnd()
    p.OgDesc <- string(line.[7]).TrimEnd()
    p.OgImage <- string(line.[8]).TrimEnd()
    p.Template <- if Convert.IsDBNull(line.[9]) then 0L else line.[9] :?> int64
    p.Project <- if Convert.IsDBNull(line.[10]) then 0L else line.[10] :?> int64

    p

let pPAGE__sps (p:pPAGE) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam
            ("OgTitle", p.OgTitle) |> kvp__sqlparam
            ("OgDesc", p.OgDesc) |> kvp__sqlparam
            ("OgImage", p.OgImage) |> kvp__sqlparam
            ("Template", p.Template) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam
            ("ogtitle", p.OgTitle) |> kvp__sqlparam
            ("ogdesc", p.OgDesc) |> kvp__sqlparam
            ("ogimage", p.OgImage) |> kvp__sqlparam
            ("template", p.Template) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam |]

let db__PAGE = db__Rcd db__pPAGE

let PAGE_wrapper item: PAGE =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pPAGE_clone (p:pPAGE): pPAGE = {
    Name = p.Name
    Caption = p.Caption
    OgTitle = p.OgTitle
    OgDesc = p.OgDesc
    OgImage = p.OgImage
    Template = p.Template
    Project = p.Project }

let PAGE_update_transaction output (updater,suc,fail) (rcd:PAGE) =
    let rollback_p = rcd.p |> pPAGE_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,PAGE_table,PAGE_sql_update(),pPAGE__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let PAGE_update output (rcd:PAGE) =
    rcd
    |> PAGE_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let PAGE_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment PAGE_id
    let ctime = DateTime.UtcNow
    match create (conn,output,PAGE_table,pPAGE__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> PAGE_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let PAGE_create output p =
    PAGE_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__PAGEo id: PAGE option = id__rcd(conn,PAGE_fieldorders(),PAGE_table,db__PAGE) id

let PAGE_metadata = {
    fieldorders = PAGE_fieldorders
    db__rcd = db__PAGE 
    wrapper = PAGE_wrapper
    sps = pPAGE__sps
    id = PAGE_id
    id__rcdo = id__PAGEo
    clone = pPAGE_clone
    empty__p = pPAGE_empty
    rcd__bin = PAGE__bin
    bin__rcd = bin__PAGE
    sql_update = PAGE_sql_update
    rcd_update = PAGE_update
    table = PAGE_table
    shorthand = "page" }

let PAGETxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiPage' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_UiPage ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Name]
    ,[Caption]
    ,[OgTitle]
    ,[OgDesc]
    ,[OgImage]
    ,[Template]
    ,[Project])
    END
    """


let db__pTEMPLATE(line:Object[]): pTEMPLATE =
    let p = pTEMPLATE_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64

    p

let pTEMPLATE__sps (p:pTEMPLATE) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam |]

let db__TEMPLATE = db__Rcd db__pTEMPLATE

let TEMPLATE_wrapper item: TEMPLATE =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pTEMPLATE_clone (p:pTEMPLATE): pTEMPLATE = {
    Name = p.Name
    Caption = p.Caption
    Project = p.Project }

let TEMPLATE_update_transaction output (updater,suc,fail) (rcd:TEMPLATE) =
    let rollback_p = rcd.p |> pTEMPLATE_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,TEMPLATE_table,TEMPLATE_sql_update(),pTEMPLATE__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let TEMPLATE_update output (rcd:TEMPLATE) =
    rcd
    |> TEMPLATE_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let TEMPLATE_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment TEMPLATE_id
    let ctime = DateTime.UtcNow
    match create (conn,output,TEMPLATE_table,pTEMPLATE__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> TEMPLATE_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let TEMPLATE_create output p =
    TEMPLATE_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__TEMPLATEo id: TEMPLATE option = id__rcd(conn,TEMPLATE_fieldorders(),TEMPLATE_table,db__TEMPLATE) id

let TEMPLATE_metadata = {
    fieldorders = TEMPLATE_fieldorders
    db__rcd = db__TEMPLATE 
    wrapper = TEMPLATE_wrapper
    sps = pTEMPLATE__sps
    id = TEMPLATE_id
    id__rcdo = id__TEMPLATEo
    clone = pTEMPLATE_clone
    empty__p = pTEMPLATE_empty
    rcd__bin = TEMPLATE__bin
    bin__rcd = bin__TEMPLATE
    sql_update = TEMPLATE_sql_update
    rcd_update = TEMPLATE_update
    table = TEMPLATE_table
    shorthand = "template" }

let TEMPLATETxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_UiTemplate' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_UiTemplate ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Name]
    ,[Caption]
    ,[Project])
    END
    """


type MetadataEnum = 
| FIELD = 0
| PROJECT = 1
| TABLE = 2
| COMP = 3
| PAGE = 4
| TEMPLATE = 5

let tablenames = [|
    FIELD_metadata.table
    PROJECT_metadata.table
    TABLE_metadata.table
    COMP_metadata.table
    PAGE_metadata.table
    TEMPLATE_metadata.table |]

let init() =

    let sqlMaxTs_Field, sqlCountTs_Field =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_Field]", "SELECT COUNT(ID) FROM [Ts_Field]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_field", "SELECT COUNT(id) FROM ts_field"
    match singlevalue_query conn (str__sql sqlMaxTs_Field) with
    | Some v ->
        let max = v :?> int64
        if max > FIELD_id.Value then
            FIELD_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_Field) with
    | Some v ->
        FIELD_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxTs_Project, sqlCountTs_Project =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_Project]", "SELECT COUNT(ID) FROM [Ts_Project]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_project", "SELECT COUNT(id) FROM ts_project"
    match singlevalue_query conn (str__sql sqlMaxTs_Project) with
    | Some v ->
        let max = v :?> int64
        if max > PROJECT_id.Value then
            PROJECT_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_Project) with
    | Some v ->
        PROJECT_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxTs_Table, sqlCountTs_Table =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_Table]", "SELECT COUNT(ID) FROM [Ts_Table]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_table", "SELECT COUNT(id) FROM ts_table"
    match singlevalue_query conn (str__sql sqlMaxTs_Table) with
    | Some v ->
        let max = v :?> int64
        if max > TABLE_id.Value then
            TABLE_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_Table) with
    | Some v ->
        TABLE_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxTs_UiComponent, sqlCountTs_UiComponent =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_UiComponent]", "SELECT COUNT(ID) FROM [Ts_UiComponent]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_uicomponent", "SELECT COUNT(id) FROM ts_uicomponent"
    match singlevalue_query conn (str__sql sqlMaxTs_UiComponent) with
    | Some v ->
        let max = v :?> int64
        if max > COMP_id.Value then
            COMP_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_UiComponent) with
    | Some v ->
        COMP_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxTs_UiPage, sqlCountTs_UiPage =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_UiPage]", "SELECT COUNT(ID) FROM [Ts_UiPage]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_uipage", "SELECT COUNT(id) FROM ts_uipage"
    match singlevalue_query conn (str__sql sqlMaxTs_UiPage) with
    | Some v ->
        let max = v :?> int64
        if max > PAGE_id.Value then
            PAGE_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_UiPage) with
    | Some v ->
        PAGE_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxTs_UiTemplate, sqlCountTs_UiTemplate =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_UiTemplate]", "SELECT COUNT(ID) FROM [Ts_UiTemplate]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_uitemplate", "SELECT COUNT(id) FROM ts_uitemplate"
    match singlevalue_query conn (str__sql sqlMaxTs_UiTemplate) with
    | Some v ->
        let max = v :?> int64
        if max > TEMPLATE_id.Value then
            TEMPLATE_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_UiTemplate) with
    | Some v ->
        TEMPLATE_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()
    ()