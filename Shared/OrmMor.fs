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

// [ADDRESS] Structure


let pADDRESS__bin (bb:BytesBuilder) (p:pADDRESS) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Bind |> BitConverter.GetBytes |> bb.append
    
    p.AddressType |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    let binLine1 = p.Line1 |> Encoding.UTF8.GetBytes
    binLine1.Length |> BitConverter.GetBytes |> bb.append
    binLine1 |> bb.append
    
    let binLine2 = p.Line2 |> Encoding.UTF8.GetBytes
    binLine2.Length |> BitConverter.GetBytes |> bb.append
    binLine2 |> bb.append
    
    let binState = p.State |> Encoding.UTF8.GetBytes
    binState.Length |> BitConverter.GetBytes |> bb.append
    binState |> bb.append
    
    let binCounty = p.County |> Encoding.UTF8.GetBytes
    binCounty.Length |> BitConverter.GetBytes |> bb.append
    binCounty |> bb.append
    
    let binTown = p.Town |> Encoding.UTF8.GetBytes
    binTown.Length |> BitConverter.GetBytes |> bb.append
    binTown |> bb.append
    
    let binContact = p.Contact |> Encoding.UTF8.GetBytes
    binContact.Length |> BitConverter.GetBytes |> bb.append
    binContact |> bb.append
    
    let binTel = p.Tel |> Encoding.UTF8.GetBytes
    binTel.Length |> BitConverter.GetBytes |> bb.append
    binTel |> bb.append
    
    let binEmail = p.Email |> Encoding.UTF8.GetBytes
    binEmail.Length |> BitConverter.GetBytes |> bb.append
    binEmail |> bb.append
    
    let binZip = p.Zip |> Encoding.UTF8.GetBytes
    binZip.Length |> BitConverter.GetBytes |> bb.append
    binZip |> bb.append
    
    p.City |> BitConverter.GetBytes |> bb.append
    
    p.Country |> BitConverter.GetBytes |> bb.append
    
    let binRemarks = p.Remarks |> Encoding.UTF8.GetBytes
    binRemarks.Length |> BitConverter.GetBytes |> bb.append
    binRemarks |> bb.append

let ADDRESS__bin (bb:BytesBuilder) (v:ADDRESS) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pADDRESS__bin bb v.p

let bin__pADDRESS (bi:BinIndexed):pADDRESS =
    let bin,index = bi

    let p = pADDRESS_empty()
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Bind <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.AddressType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    let count_Line1 = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Line1 <- Encoding.UTF8.GetString(bin,index.Value,count_Line1)
    index.Value <- index.Value + count_Line1
    
    let count_Line2 = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Line2 <- Encoding.UTF8.GetString(bin,index.Value,count_Line2)
    index.Value <- index.Value + count_Line2
    
    let count_State = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.State <- Encoding.UTF8.GetString(bin,index.Value,count_State)
    index.Value <- index.Value + count_State
    
    let count_County = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.County <- Encoding.UTF8.GetString(bin,index.Value,count_County)
    index.Value <- index.Value + count_County
    
    let count_Town = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Town <- Encoding.UTF8.GetString(bin,index.Value,count_Town)
    index.Value <- index.Value + count_Town
    
    let count_Contact = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Contact <- Encoding.UTF8.GetString(bin,index.Value,count_Contact)
    index.Value <- index.Value + count_Contact
    
    let count_Tel = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Tel <- Encoding.UTF8.GetString(bin,index.Value,count_Tel)
    index.Value <- index.Value + count_Tel
    
    let count_Email = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Email <- Encoding.UTF8.GetString(bin,index.Value,count_Email)
    index.Value <- index.Value + count_Email
    
    let count_Zip = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Zip <- Encoding.UTF8.GetString(bin,index.Value,count_Zip)
    index.Value <- index.Value + count_Zip
    
    p.City <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Country <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_Remarks = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Remarks <- Encoding.UTF8.GetString(bin,index.Value,count_Remarks)
    index.Value <- index.Value + count_Remarks
    
    p

let bin__ADDRESS (bi:BinIndexed):ADDRESS =
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
        p = bin__pADDRESS bi }

let pADDRESS__json (p:pADDRESS) =

    [|
        ("Caption",p.Caption |> Json.Str)
        ("Bind",p.Bind.ToString() |> Json.Num)
        ("AddressType",(p.AddressType |> EnumToValue).ToString() |> Json.Num)
        ("Line1",p.Line1 |> Json.Str)
        ("Line2",p.Line2 |> Json.Str)
        ("State",p.State |> Json.Str)
        ("County",p.County |> Json.Str)
        ("Town",p.Town |> Json.Str)
        ("Contact",p.Contact |> Json.Str)
        ("Tel",p.Tel |> Json.Str)
        ("Email",p.Email |> Json.Str)
        ("Zip",p.Zip |> Json.Str)
        ("City",p.City.ToString() |> Json.Num)
        ("Country",p.Country.ToString() |> Json.Num)
        ("Remarks",p.Remarks |> Json.Str) |]
    |> Json.Braket

let ADDRESS__json (v:ADDRESS) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pADDRESS__json v.p) |]
    |> Json.Braket

let ADDRESS__jsonTbw (w:TextBlockWriter) (v:ADDRESS) =
    json__str w (ADDRESS__json v)

let ADDRESS__jsonStr (v:ADDRESS) =
    (ADDRESS__json v) |> json__strFinal


let json__pADDRESSo (json:Json):pADDRESS option =
    let fields = json |> json__items

    let p = pADDRESS_empty()
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.Bind <- checkfield fields "Bind" |> parse_int64
    
    p.AddressType <- checkfield fields "AddressType" |> parse_int32 |> EnumOfValue
    
    p.Line1 <- checkfieldz fields "Line1" 300
    
    p.Line2 <- checkfieldz fields "Line2" 300
    
    p.State <- checkfieldz fields "State" 16
    
    p.County <- checkfieldz fields "County" 16
    
    p.Town <- checkfieldz fields "Town" 16
    
    p.Contact <- checkfieldz fields "Contact" 64
    
    p.Tel <- checkfieldz fields "Tel" 20
    
    p.Email <- checkfieldz fields "Email" 256
    
    p.Zip <- checkfieldz fields "Zip" 16
    
    p.City <- checkfield fields "City" |> parse_int64
    
    p.Country <- checkfield fields "Country" |> parse_int64
    
    p.Remarks <- checkfield fields "Remarks"
    
    p |> Some
    

let json__ADDRESSo (json:Json):ADDRESS option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pADDRESSo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [BIZ] Structure


let pBIZ__bin (bb:BytesBuilder) (p:pBIZ) =

    
    let binCode = p.Code |> Encoding.UTF8.GetBytes
    binCode.Length |> BitConverter.GetBytes |> bb.append
    binCode |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Parent |> BitConverter.GetBytes |> bb.append
    
    p.BasicAcct |> BitConverter.GetBytes |> bb.append
    
    let binDescTxt = p.DescTxt |> Encoding.UTF8.GetBytes
    binDescTxt.Length |> BitConverter.GetBytes |> bb.append
    binDescTxt |> bb.append
    
    let binWebsite = p.Website |> Encoding.UTF8.GetBytes
    binWebsite.Length |> BitConverter.GetBytes |> bb.append
    binWebsite |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    p.City |> BitConverter.GetBytes |> bb.append
    
    p.Country |> BitConverter.GetBytes |> bb.append
    
    p.Lang |> BitConverter.GetBytes |> bb.append
    
    p.IsSocialPlatform |> BitConverter.GetBytes |> bb.append
    
    p.IsCmsSource |> BitConverter.GetBytes |> bb.append
    
    p.IsPayGateway |> BitConverter.GetBytes |> bb.append

let BIZ__bin (bb:BytesBuilder) (v:BIZ) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pBIZ__bin bb v.p

let bin__pBIZ (bi:BinIndexed):pBIZ =
    let bin,index = bi

    let p = pBIZ_empty()
    
    let count_Code = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Code <- Encoding.UTF8.GetString(bin,index.Value,count_Code)
    index.Value <- index.Value + count_Code
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Parent <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.BasicAcct <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_DescTxt = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.DescTxt <- Encoding.UTF8.GetString(bin,index.Value,count_DescTxt)
    index.Value <- index.Value + count_DescTxt
    
    let count_Website = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Website <- Encoding.UTF8.GetString(bin,index.Value,count_Website)
    index.Value <- index.Value + count_Website
    
    let count_Icon = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Icon <- Encoding.UTF8.GetString(bin,index.Value,count_Icon)
    index.Value <- index.Value + count_Icon
    
    p.City <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Country <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Lang <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.IsSocialPlatform <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsCmsSource <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsPayGateway <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p

let bin__BIZ (bi:BinIndexed):BIZ =
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
        p = bin__pBIZ bi }

let pBIZ__json (p:pBIZ) =

    [|
        ("Code",p.Code |> Json.Str)
        ("Caption",p.Caption |> Json.Str)
        ("Parent",p.Parent.ToString() |> Json.Num)
        ("BasicAcct",p.BasicAcct.ToString() |> Json.Num)
        ("DescTxt",p.DescTxt |> Json.Str)
        ("Website",p.Website |> Json.Str)
        ("Icon",p.Icon |> Json.Str)
        ("City",p.City.ToString() |> Json.Num)
        ("Country",p.Country.ToString() |> Json.Num)
        ("Lang",p.Lang.ToString() |> Json.Num)
        ("IsSocialPlatform",if p.IsSocialPlatform then Json.True else Json.False)
        ("IsCmsSource",if p.IsCmsSource then Json.True else Json.False)
        ("IsPayGateway",if p.IsPayGateway then Json.True else Json.False) |]
    |> Json.Braket

let BIZ__json (v:BIZ) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pBIZ__json v.p) |]
    |> Json.Braket

let BIZ__jsonTbw (w:TextBlockWriter) (v:BIZ) =
    json__str w (BIZ__json v)

let BIZ__jsonStr (v:BIZ) =
    (BIZ__json v) |> json__strFinal


let json__pBIZo (json:Json):pBIZ option =
    let fields = json |> json__items

    let p = pBIZ_empty()
    
    p.Code <- checkfieldz fields "Code" 64
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.Parent <- checkfield fields "Parent" |> parse_int64
    
    p.BasicAcct <- checkfield fields "BasicAcct" |> parse_int64
    
    p.DescTxt <- checkfield fields "DescTxt"
    
    p.Website <- checkfieldz fields "Website" 256
    
    p.Icon <- checkfieldz fields "Icon" 256
    
    p.City <- checkfield fields "City" |> parse_int64
    
    p.Country <- checkfield fields "Country" |> parse_int64
    
    p.Lang <- checkfield fields "Lang" |> parse_int64
    
    p.IsSocialPlatform <- checkfield fields "IsSocialPlatform" = "true"
    
    p.IsCmsSource <- checkfield fields "IsCmsSource" = "true"
    
    p.IsPayGateway <- checkfield fields "IsPayGateway" = "true"
    
    p |> Some
    

let json__BIZo (json:Json):BIZ option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pBIZo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [CAT] Structure


let pCAT__bin (bb:BytesBuilder) (p:pCAT) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Lang |> BitConverter.GetBytes |> bb.append
    
    p.Zh |> BitConverter.GetBytes |> bb.append
    
    p.Parent |> BitConverter.GetBytes |> bb.append
    
    p.CatState |> EnumToValue |> BitConverter.GetBytes |> bb.append

let CAT__bin (bb:BytesBuilder) (v:CAT) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pCAT__bin bb v.p

let bin__pCAT (bi:BinIndexed):pCAT =
    let bin,index = bi

    let p = pCAT_empty()
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Lang <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Zh <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Parent <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CatState <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p

let bin__CAT (bi:BinIndexed):CAT =
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
        p = bin__pCAT bi }

let pCAT__json (p:pCAT) =

    [|
        ("Caption",p.Caption |> Json.Str)
        ("Lang",p.Lang.ToString() |> Json.Num)
        ("Zh",p.Zh.ToString() |> Json.Num)
        ("Parent",p.Parent.ToString() |> Json.Num)
        ("CatState",(p.CatState |> EnumToValue).ToString() |> Json.Num) |]
    |> Json.Braket

let CAT__json (v:CAT) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pCAT__json v.p) |]
    |> Json.Braket

let CAT__jsonTbw (w:TextBlockWriter) (v:CAT) =
    json__str w (CAT__json v)

let CAT__jsonStr (v:CAT) =
    (CAT__json v) |> json__strFinal


let json__pCATo (json:Json):pCAT option =
    let fields = json |> json__items

    let p = pCAT_empty()
    
    p.Caption <- checkfieldz fields "Caption" 64
    
    p.Lang <- checkfield fields "Lang" |> parse_int64
    
    p.Zh <- checkfield fields "Zh" |> parse_int64
    
    p.Parent <- checkfield fields "Parent" |> parse_int64
    
    p.CatState <- checkfield fields "CatState" |> parse_int32 |> EnumOfValue
    
    p |> Some
    

let json__CATo (json:Json):CAT option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pCATo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [CITY] Structure


let pCITY__bin (bb:BytesBuilder) (p:pCITY) =

    
    let binFullname = p.Fullname |> Encoding.UTF8.GetBytes
    binFullname.Length |> BitConverter.GetBytes |> bb.append
    binFullname |> bb.append
    
    let binMetropolitanCode3IATA = p.MetropolitanCode3IATA |> Encoding.UTF8.GetBytes
    binMetropolitanCode3IATA.Length |> BitConverter.GetBytes |> bb.append
    binMetropolitanCode3IATA |> bb.append
    
    let binNameEn = p.NameEn |> Encoding.UTF8.GetBytes
    binNameEn.Length |> BitConverter.GetBytes |> bb.append
    binNameEn |> bb.append
    
    p.Country |> BitConverter.GetBytes |> bb.append
    
    p.Place |> BitConverter.GetBytes |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    let binTel = p.Tel |> Encoding.UTF8.GetBytes
    binTel.Length |> BitConverter.GetBytes |> bb.append
    binTel |> bb.append

let CITY__bin (bb:BytesBuilder) (v:CITY) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pCITY__bin bb v.p

let bin__pCITY (bi:BinIndexed):pCITY =
    let bin,index = bi

    let p = pCITY_empty()
    
    let count_Fullname = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Fullname <- Encoding.UTF8.GetString(bin,index.Value,count_Fullname)
    index.Value <- index.Value + count_Fullname
    
    let count_MetropolitanCode3IATA = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.MetropolitanCode3IATA <- Encoding.UTF8.GetString(bin,index.Value,count_MetropolitanCode3IATA)
    index.Value <- index.Value + count_MetropolitanCode3IATA
    
    let count_NameEn = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.NameEn <- Encoding.UTF8.GetString(bin,index.Value,count_NameEn)
    index.Value <- index.Value + count_NameEn
    
    p.Country <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Place <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_Icon = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Icon <- Encoding.UTF8.GetString(bin,index.Value,count_Icon)
    index.Value <- index.Value + count_Icon
    
    let count_Tel = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Tel <- Encoding.UTF8.GetString(bin,index.Value,count_Tel)
    index.Value <- index.Value + count_Tel
    
    p

let bin__CITY (bi:BinIndexed):CITY =
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
        p = bin__pCITY bi }

let pCITY__json (p:pCITY) =

    [|
        ("Fullname",p.Fullname |> Json.Str)
        ("MetropolitanCode3IATA",p.MetropolitanCode3IATA |> Json.Str)
        ("NameEn",p.NameEn |> Json.Str)
        ("Country",p.Country.ToString() |> Json.Num)
        ("Place",p.Place.ToString() |> Json.Num)
        ("Icon",p.Icon |> Json.Str)
        ("Tel",p.Tel |> Json.Str) |]
    |> Json.Braket

let CITY__json (v:CITY) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pCITY__json v.p) |]
    |> Json.Braket

let CITY__jsonTbw (w:TextBlockWriter) (v:CITY) =
    json__str w (CITY__json v)

let CITY__jsonStr (v:CITY) =
    (CITY__json v) |> json__strFinal


let json__pCITYo (json:Json):pCITY option =
    let fields = json |> json__items

    let p = pCITY_empty()
    
    p.Fullname <- checkfieldz fields "Fullname" 64
    
    p.MetropolitanCode3IATA <- checkfieldz fields "MetropolitanCode3IATA" 3
    
    p.NameEn <- checkfieldz fields "NameEn" 64
    
    p.Country <- checkfield fields "Country" |> parse_int64
    
    p.Place <- checkfield fields "Place" |> parse_int64
    
    p.Icon <- checkfieldz fields "Icon" 256
    
    p.Tel <- checkfieldz fields "Tel" 4
    
    p |> Some
    

let json__CITYo (json:Json):CITY option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pCITYo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [CRY] Structure


let pCRY__bin (bb:BytesBuilder) (p:pCRY) =

    
    let binCode2 = p.Code2 |> Encoding.UTF8.GetBytes
    binCode2.Length |> BitConverter.GetBytes |> bb.append
    binCode2 |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    let binFullname = p.Fullname |> Encoding.UTF8.GetBytes
    binFullname.Length |> BitConverter.GetBytes |> bb.append
    binFullname |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    let binTel = p.Tel |> Encoding.UTF8.GetBytes
    binTel.Length |> BitConverter.GetBytes |> bb.append
    binTel |> bb.append
    
    p.Cur |> BitConverter.GetBytes |> bb.append
    
    p.Capital |> BitConverter.GetBytes |> bb.append
    
    p.Place |> BitConverter.GetBytes |> bb.append
    
    p.Lang |> BitConverter.GetBytes |> bb.append

let CRY__bin (bb:BytesBuilder) (v:CRY) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pCRY__bin bb v.p

let bin__pCRY (bi:BinIndexed):pCRY =
    let bin,index = bi

    let p = pCRY_empty()
    
    let count_Code2 = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Code2 <- Encoding.UTF8.GetString(bin,index.Value,count_Code2)
    index.Value <- index.Value + count_Code2
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    let count_Fullname = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Fullname <- Encoding.UTF8.GetString(bin,index.Value,count_Fullname)
    index.Value <- index.Value + count_Fullname
    
    let count_Icon = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Icon <- Encoding.UTF8.GetString(bin,index.Value,count_Icon)
    index.Value <- index.Value + count_Icon
    
    let count_Tel = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Tel <- Encoding.UTF8.GetString(bin,index.Value,count_Tel)
    index.Value <- index.Value + count_Tel
    
    p.Cur <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Capital <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Place <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Lang <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__CRY (bi:BinIndexed):CRY =
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
        p = bin__pCRY bi }

let pCRY__json (p:pCRY) =

    [|
        ("Code2",p.Code2 |> Json.Str)
        ("Caption",p.Caption |> Json.Str)
        ("Fullname",p.Fullname |> Json.Str)
        ("Icon",p.Icon |> Json.Str)
        ("Tel",p.Tel |> Json.Str)
        ("Cur",p.Cur.ToString() |> Json.Num)
        ("Capital",p.Capital.ToString() |> Json.Num)
        ("Place",p.Place.ToString() |> Json.Num)
        ("Lang",p.Lang.ToString() |> Json.Num) |]
    |> Json.Braket

let CRY__json (v:CRY) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pCRY__json v.p) |]
    |> Json.Braket

let CRY__jsonTbw (w:TextBlockWriter) (v:CRY) =
    json__str w (CRY__json v)

let CRY__jsonStr (v:CRY) =
    (CRY__json v) |> json__strFinal


let json__pCRYo (json:Json):pCRY option =
    let fields = json |> json__items

    let p = pCRY_empty()
    
    p.Code2 <- checkfieldz fields "Code2" 2
    
    p.Caption <- checkfieldz fields "Caption" 64
    
    p.Fullname <- checkfieldz fields "Fullname" 256
    
    p.Icon <- checkfieldz fields "Icon" 256
    
    p.Tel <- checkfieldz fields "Tel" 4
    
    p.Cur <- checkfield fields "Cur" |> parse_int64
    
    p.Capital <- checkfield fields "Capital" |> parse_int64
    
    p.Place <- checkfield fields "Place" |> parse_int64
    
    p.Lang <- checkfield fields "Lang" |> parse_int64
    
    p |> Some
    

let json__CRYo (json:Json):CRY option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pCRYo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [EU] Structure


let pEU__bin (bb:BytesBuilder) (p:pEU) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    let binUsername = p.Username |> Encoding.UTF8.GetBytes
    binUsername.Length |> BitConverter.GetBytes |> bb.append
    binUsername |> bb.append
    
    p.SocialAuthBiz |> BitConverter.GetBytes |> bb.append
    
    let binSocialAuthId = p.SocialAuthId |> Encoding.UTF8.GetBytes
    binSocialAuthId.Length |> BitConverter.GetBytes |> bb.append
    binSocialAuthId |> bb.append
    
    let binSocialAuthAvatar = p.SocialAuthAvatar |> Encoding.UTF8.GetBytes
    binSocialAuthAvatar.Length |> BitConverter.GetBytes |> bb.append
    binSocialAuthAvatar |> bb.append
    
    let binEmail = p.Email |> Encoding.UTF8.GetBytes
    binEmail.Length |> BitConverter.GetBytes |> bb.append
    binEmail |> bb.append
    
    let binTel = p.Tel |> Encoding.UTF8.GetBytes
    binTel.Length |> BitConverter.GetBytes |> bb.append
    binTel |> bb.append
    
    p.Gender |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Status |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Admin |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.BizPartner |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Privilege |> BitConverter.GetBytes |> bb.append
    
    p.Verify |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    let binPwd = p.Pwd |> Encoding.UTF8.GetBytes
    binPwd.Length |> BitConverter.GetBytes |> bb.append
    binPwd |> bb.append
    
    p.Online |> BitConverter.GetBytes |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    let binBackground = p.Background |> Encoding.UTF8.GetBytes
    binBackground.Length |> BitConverter.GetBytes |> bb.append
    binBackground |> bb.append
    
    p.BasicAcct |> BitConverter.GetBytes |> bb.append
    
    p.Citizen |> BitConverter.GetBytes |> bb.append
    
    let binRefer = p.Refer |> Encoding.UTF8.GetBytes
    binRefer.Length |> BitConverter.GetBytes |> bb.append
    binRefer |> bb.append
    
    p.Referer |> BitConverter.GetBytes |> bb.append
    
    let binUrl = p.Url |> Encoding.UTF8.GetBytes
    binUrl.Length |> BitConverter.GetBytes |> bb.append
    binUrl |> bb.append
    
    let binAbout = p.About |> Encoding.UTF8.GetBytes
    binAbout.Length |> BitConverter.GetBytes |> bb.append
    binAbout |> bb.append

let EU__bin (bb:BytesBuilder) (v:EU) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pEU__bin bb v.p

let bin__pEU (bi:BinIndexed):pEU =
    let bin,index = bi

    let p = pEU_empty()
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    let count_Username = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Username <- Encoding.UTF8.GetString(bin,index.Value,count_Username)
    index.Value <- index.Value + count_Username
    
    p.SocialAuthBiz <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_SocialAuthId = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.SocialAuthId <- Encoding.UTF8.GetString(bin,index.Value,count_SocialAuthId)
    index.Value <- index.Value + count_SocialAuthId
    
    let count_SocialAuthAvatar = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.SocialAuthAvatar <- Encoding.UTF8.GetString(bin,index.Value,count_SocialAuthAvatar)
    index.Value <- index.Value + count_SocialAuthAvatar
    
    let count_Email = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Email <- Encoding.UTF8.GetString(bin,index.Value,count_Email)
    index.Value <- index.Value + count_Email
    
    let count_Tel = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Tel <- Encoding.UTF8.GetString(bin,index.Value,count_Tel)
    index.Value <- index.Value + count_Tel
    
    p.Gender <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Status <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Admin <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.BizPartner <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Privilege <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Verify <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    let count_Pwd = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Pwd <- Encoding.UTF8.GetString(bin,index.Value,count_Pwd)
    index.Value <- index.Value + count_Pwd
    
    p.Online <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    let count_Icon = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Icon <- Encoding.UTF8.GetString(bin,index.Value,count_Icon)
    index.Value <- index.Value + count_Icon
    
    let count_Background = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Background <- Encoding.UTF8.GetString(bin,index.Value,count_Background)
    index.Value <- index.Value + count_Background
    
    p.BasicAcct <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Citizen <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_Refer = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Refer <- Encoding.UTF8.GetString(bin,index.Value,count_Refer)
    index.Value <- index.Value + count_Refer
    
    p.Referer <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_Url = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Url <- Encoding.UTF8.GetString(bin,index.Value,count_Url)
    index.Value <- index.Value + count_Url
    
    let count_About = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.About <- Encoding.UTF8.GetString(bin,index.Value,count_About)
    index.Value <- index.Value + count_About
    
    p

let bin__EU (bi:BinIndexed):EU =
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
        p = bin__pEU bi }

let pEU__json (p:pEU) =

    [|
        ("Caption",p.Caption |> Json.Str)
        ("Username",p.Username |> Json.Str)
        ("SocialAuthBiz",p.SocialAuthBiz.ToString() |> Json.Num)
        ("SocialAuthId",p.SocialAuthId |> Json.Str)
        ("SocialAuthAvatar",p.SocialAuthAvatar |> Json.Str)
        ("Email",p.Email |> Json.Str)
        ("Tel",p.Tel |> Json.Str)
        ("Gender",(p.Gender |> EnumToValue).ToString() |> Json.Num)
        ("Status",(p.Status |> EnumToValue).ToString() |> Json.Num)
        ("Admin",(p.Admin |> EnumToValue).ToString() |> Json.Num)
        ("BizPartner",(p.BizPartner |> EnumToValue).ToString() |> Json.Num)
        ("Privilege",p.Privilege.ToString() |> Json.Num)
        ("Verify",(p.Verify |> EnumToValue).ToString() |> Json.Num)
        ("Pwd",p.Pwd |> Json.Str)
        ("Online",if p.Online then Json.True else Json.False)
        ("Icon",p.Icon |> Json.Str)
        ("Background",p.Background |> Json.Str)
        ("BasicAcct",p.BasicAcct.ToString() |> Json.Num)
        ("Citizen",p.Citizen.ToString() |> Json.Num)
        ("Refer",p.Refer |> Json.Str)
        ("Referer",p.Referer.ToString() |> Json.Num)
        ("Url",p.Url |> Json.Str)
        ("About",p.About |> Json.Str) |]
    |> Json.Braket

let EU__json (v:EU) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pEU__json v.p) |]
    |> Json.Braket

let EU__jsonTbw (w:TextBlockWriter) (v:EU) =
    json__str w (EU__json v)

let EU__jsonStr (v:EU) =
    (EU__json v) |> json__strFinal


let json__pEUo (json:Json):pEU option =
    let fields = json |> json__items

    let p = pEU_empty()
    
    p.Caption <- checkfieldz fields "Caption" 64
    
    p.Username <- checkfieldz fields "Username" 64
    
    p.SocialAuthBiz <- checkfield fields "SocialAuthBiz" |> parse_int64
    
    p.SocialAuthId <- checkfield fields "SocialAuthId"
    
    p.SocialAuthAvatar <- checkfield fields "SocialAuthAvatar"
    
    p.Email <- checkfieldz fields "Email" 256
    
    p.Tel <- checkfieldz fields "Tel" 32
    
    p.Gender <- checkfield fields "Gender" |> parse_int32 |> EnumOfValue
    
    p.Status <- checkfield fields "Status" |> parse_int32 |> EnumOfValue
    
    p.Admin <- checkfield fields "Admin" |> parse_int32 |> EnumOfValue
    
    p.BizPartner <- checkfield fields "BizPartner" |> parse_int32 |> EnumOfValue
    
    p.Privilege <- checkfield fields "Privilege" |> parse_int64
    
    p.Verify <- checkfield fields "Verify" |> parse_int32 |> EnumOfValue
    
    p.Pwd <- checkfieldz fields "Pwd" 16
    
    p.Online <- checkfield fields "Online" = "true"
    
    p.Icon <- checkfieldz fields "Icon" 256
    
    p.Background <- checkfieldz fields "Background" 256
    
    p.BasicAcct <- checkfield fields "BasicAcct" |> parse_int64
    
    p.Citizen <- checkfield fields "Citizen" |> parse_int64
    
    p.Refer <- checkfieldz fields "Refer" 9
    
    p.Referer <- checkfield fields "Referer" |> parse_int64
    
    p.Url <- checkfield fields "Url"
    
    p.About <- checkfield fields "About"
    
    p |> Some
    

let json__EUo (json:Json):EU option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pEUo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [CSI] Structure


let pCSI__bin (bb:BytesBuilder) (p:pCSI) =

    
    p.Type |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Lang |> BitConverter.GetBytes |> bb.append
    
    p.Bind |> BitConverter.GetBytes |> bb.append

let CSI__bin (bb:BytesBuilder) (v:CSI) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pCSI__bin bb v.p

let bin__pCSI (bi:BinIndexed):pCSI =
    let bin,index = bi

    let p = pCSI_empty()
    
    p.Type <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Lang <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Bind <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__CSI (bi:BinIndexed):CSI =
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
        p = bin__pCSI bi }

let pCSI__json (p:pCSI) =

    [|
        ("Type",(p.Type |> EnumToValue).ToString() |> Json.Num)
        ("Lang",p.Lang.ToString() |> Json.Num)
        ("Bind",p.Bind.ToString() |> Json.Num) |]
    |> Json.Braket

let CSI__json (v:CSI) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pCSI__json v.p) |]
    |> Json.Braket

let CSI__jsonTbw (w:TextBlockWriter) (v:CSI) =
    json__str w (CSI__json v)

let CSI__jsonStr (v:CSI) =
    (CSI__json v) |> json__strFinal


let json__pCSIo (json:Json):pCSI option =
    let fields = json |> json__items

    let p = pCSI_empty()
    
    p.Type <- checkfield fields "Type" |> parse_int32 |> EnumOfValue
    
    p.Lang <- checkfield fields "Lang" |> parse_int64
    
    p.Bind <- checkfield fields "Bind" |> parse_int64
    
    p |> Some
    

let json__CSIo (json:Json):CSI option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pCSIo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [CWC] Structure


let pCWC__bin (bb:BytesBuilder) (p:pCWC) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.ExternalId |> BitConverter.GetBytes |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    p.EU |> BitConverter.GetBytes |> bb.append
    
    p.Biz |> BitConverter.GetBytes |> bb.append
    
    let binJson = p.Json |> Encoding.UTF8.GetBytes
    binJson.Length |> BitConverter.GetBytes |> bb.append
    binJson |> bb.append

let CWC__bin (bb:BytesBuilder) (v:CWC) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pCWC__bin bb v.p

let bin__pCWC (bi:BinIndexed):pCWC =
    let bin,index = bi

    let p = pCWC_empty()
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.ExternalId <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_Icon = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Icon <- Encoding.UTF8.GetString(bin,index.Value,count_Icon)
    index.Value <- index.Value + count_Icon
    
    p.EU <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Biz <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_Json = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Json <- Encoding.UTF8.GetString(bin,index.Value,count_Json)
    index.Value <- index.Value + count_Json
    
    p

let bin__CWC (bi:BinIndexed):CWC =
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
        p = bin__pCWC bi }

let pCWC__json (p:pCWC) =

    [|
        ("Caption",p.Caption |> Json.Str)
        ("ExternalId",p.ExternalId.ToString() |> Json.Num)
        ("Icon",p.Icon |> Json.Str)
        ("EU",p.EU.ToString() |> Json.Num)
        ("Biz",p.Biz.ToString() |> Json.Num)
        ("Json",p.Json |> Json.Str) |]
    |> Json.Braket

let CWC__json (v:CWC) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pCWC__json v.p) |]
    |> Json.Braket

let CWC__jsonTbw (w:TextBlockWriter) (v:CWC) =
    json__str w (CWC__json v)

let CWC__jsonStr (v:CWC) =
    (CWC__json v) |> json__strFinal


let json__pCWCo (json:Json):pCWC option =
    let fields = json |> json__items

    let p = pCWC_empty()
    
    p.Caption <- checkfieldz fields "Caption" 64
    
    p.ExternalId <- checkfield fields "ExternalId" |> parse_int64
    
    p.Icon <- checkfieldz fields "Icon" 256
    
    p.EU <- checkfield fields "EU" |> parse_int64
    
    p.Biz <- checkfield fields "Biz" |> parse_int64
    
    p.Json <- checkfield fields "Json"
    
    p |> Some
    

let json__CWCo (json:Json):CWC option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pCWCo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [API] Structure


let pAPI__bin (bb:BytesBuilder) (p:pAPI) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append

let API__bin (bb:BytesBuilder) (v:API) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pAPI__bin bb v.p

let bin__pAPI (bi:BinIndexed):pAPI =
    let bin,index = bi

    let p = pAPI_empty()
    
    let count_Name = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Name <- Encoding.UTF8.GetString(bin,index.Value,count_Name)
    index.Value <- index.Value + count_Name
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__API (bi:BinIndexed):API =
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
        p = bin__pAPI bi }

let pAPI__json (p:pAPI) =

    [|
        ("Name",p.Name |> Json.Str)
        ("Project",p.Project.ToString() |> Json.Num) |]
    |> Json.Braket

let API__json (v:API) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pAPI__json v.p) |]
    |> Json.Braket

let API__jsonTbw (w:TextBlockWriter) (v:API) =
    json__str w (API__json v)

let API__jsonStr (v:API) =
    (API__json v) |> json__strFinal


let json__pAPIo (json:Json):pAPI option =
    let fields = json |> json__items

    let p = pAPI_empty()
    
    p.Name <- checkfieldz fields "Name" 64
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p |> Some
    

let json__APIo (json:Json):API option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pAPIo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [FIELD] Structure


let pFIELD__bin (bb:BytesBuilder) (p:pFIELD) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    let binDesc = p.Desc |> Encoding.UTF8.GetBytes
    binDesc.Length |> BitConverter.GetBytes |> bb.append
    binDesc |> bb.append
    
    p.FieldType |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Length |> BitConverter.GetBytes |> bb.append
    
    let binSelectLines = p.SelectLines |> Encoding.UTF8.GetBytes
    binSelectLines.Length |> BitConverter.GetBytes |> bb.append
    binSelectLines |> bb.append
    
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
    
    p.FieldType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Length <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_SelectLines = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.SelectLines <- Encoding.UTF8.GetString(bin,index.Value,count_SelectLines)
    index.Value <- index.Value + count_SelectLines
    
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
        ("FieldType",(p.FieldType |> EnumToValue).ToString() |> Json.Num)
        ("Length",p.Length.ToString() |> Json.Num)
        ("SelectLines",p.SelectLines |> Json.Str)
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
    
    p.FieldType <- checkfield fields "FieldType" |> parse_int32 |> EnumOfValue
    
    p.Length <- checkfield fields "Length" |> parse_int64
    
    p.SelectLines <- checkfield fields "SelectLines"
    
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
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [HOSTCONFIG] Structure


let pHOSTCONFIG__bin (bb:BytesBuilder) (p:pHOSTCONFIG) =

    
    let binHostname = p.Hostname |> Encoding.UTF8.GetBytes
    binHostname.Length |> BitConverter.GetBytes |> bb.append
    binHostname |> bb.append
    
    p.Database |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    let binDatabaseName = p.DatabaseName |> Encoding.UTF8.GetBytes
    binDatabaseName.Length |> BitConverter.GetBytes |> bb.append
    binDatabaseName |> bb.append
    
    let binDatabaseConn = p.DatabaseConn |> Encoding.UTF8.GetBytes
    binDatabaseConn.Length |> BitConverter.GetBytes |> bb.append
    binDatabaseConn |> bb.append
    
    let binDirVs = p.DirVs |> Encoding.UTF8.GetBytes
    binDirVs.Length |> BitConverter.GetBytes |> bb.append
    binDirVs |> bb.append
    
    let binDirVsCodeWeb = p.DirVsCodeWeb |> Encoding.UTF8.GetBytes
    binDirVsCodeWeb.Length |> BitConverter.GetBytes |> bb.append
    binDirVsCodeWeb |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append

let HOSTCONFIG__bin (bb:BytesBuilder) (v:HOSTCONFIG) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pHOSTCONFIG__bin bb v.p

let bin__pHOSTCONFIG (bi:BinIndexed):pHOSTCONFIG =
    let bin,index = bi

    let p = pHOSTCONFIG_empty()
    
    let count_Hostname = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Hostname <- Encoding.UTF8.GetString(bin,index.Value,count_Hostname)
    index.Value <- index.Value + count_Hostname
    
    p.Database <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    let count_DatabaseName = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.DatabaseName <- Encoding.UTF8.GetString(bin,index.Value,count_DatabaseName)
    index.Value <- index.Value + count_DatabaseName
    
    let count_DatabaseConn = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.DatabaseConn <- Encoding.UTF8.GetString(bin,index.Value,count_DatabaseConn)
    index.Value <- index.Value + count_DatabaseConn
    
    let count_DirVs = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.DirVs <- Encoding.UTF8.GetString(bin,index.Value,count_DirVs)
    index.Value <- index.Value + count_DirVs
    
    let count_DirVsCodeWeb = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.DirVsCodeWeb <- Encoding.UTF8.GetString(bin,index.Value,count_DirVsCodeWeb)
    index.Value <- index.Value + count_DirVsCodeWeb
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__HOSTCONFIG (bi:BinIndexed):HOSTCONFIG =
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
        p = bin__pHOSTCONFIG bi }

let pHOSTCONFIG__json (p:pHOSTCONFIG) =

    [|
        ("Hostname",p.Hostname |> Json.Str)
        ("Database",(p.Database |> EnumToValue).ToString() |> Json.Num)
        ("DatabaseName",p.DatabaseName |> Json.Str)
        ("DatabaseConn",p.DatabaseConn |> Json.Str)
        ("DirVs",p.DirVs |> Json.Str)
        ("DirVsCodeWeb",p.DirVsCodeWeb |> Json.Str)
        ("Project",p.Project.ToString() |> Json.Num) |]
    |> Json.Braket

let HOSTCONFIG__json (v:HOSTCONFIG) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pHOSTCONFIG__json v.p) |]
    |> Json.Braket

let HOSTCONFIG__jsonTbw (w:TextBlockWriter) (v:HOSTCONFIG) =
    json__str w (HOSTCONFIG__json v)

let HOSTCONFIG__jsonStr (v:HOSTCONFIG) =
    (HOSTCONFIG__json v) |> json__strFinal


let json__pHOSTCONFIGo (json:Json):pHOSTCONFIG option =
    let fields = json |> json__items

    let p = pHOSTCONFIG_empty()
    
    p.Hostname <- checkfieldz fields "Hostname" 64
    
    p.Database <- checkfield fields "Database" |> parse_int32 |> EnumOfValue
    
    p.DatabaseName <- checkfieldz fields "DatabaseName" 64
    
    p.DatabaseConn <- checkfieldz fields "DatabaseConn" 64
    
    p.DirVs <- checkfieldz fields "DirVs" 64
    
    p.DirVsCodeWeb <- checkfieldz fields "DirVsCodeWeb" 64
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p |> Some
    

let json__HOSTCONFIGo (json:Json):HOSTCONFIG option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pHOSTCONFIGo v
        | None -> None
    
    match o with
    | Some p ->
        
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
    
    let binTypeSessionUser = p.TypeSessionUser |> Encoding.UTF8.GetBytes
    binTypeSessionUser.Length |> BitConverter.GetBytes |> bb.append
    binTypeSessionUser |> bb.append

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
    
    let count_TypeSessionUser = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.TypeSessionUser <- Encoding.UTF8.GetString(bin,index.Value,count_TypeSessionUser)
    index.Value <- index.Value + count_TypeSessionUser
    
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
        ("Caption",p.Caption |> Json.Str)
        ("TypeSessionUser",p.TypeSessionUser |> Json.Str) |]
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
    
    p.TypeSessionUser <- checkfieldz fields "TypeSessionUser" 64
    
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
    
    let binRoute = p.Route |> Encoding.UTF8.GetBytes
    binRoute.Length |> BitConverter.GetBytes |> bb.append
    binRoute |> bb.append
    
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
    
    let count_Route = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Route <- Encoding.UTF8.GetString(bin,index.Value,count_Route)
    index.Value <- index.Value + count_Route
    
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
        ("Route",p.Route |> Json.Str)
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
    
    p.Route <- checkfield fields "Route"
    
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
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [VARTYPE] Structure


let pVARTYPE__bin (bb:BytesBuilder) (p:pVARTYPE) =

    
    let binName = p.Name |> Encoding.UTF8.GetBytes
    binName.Length |> BitConverter.GetBytes |> bb.append
    binName |> bb.append
    
    let binType = p.Type |> Encoding.UTF8.GetBytes
    binType.Length |> BitConverter.GetBytes |> bb.append
    binType |> bb.append
    
    let binVal = p.Val |> Encoding.UTF8.GetBytes
    binVal.Length |> BitConverter.GetBytes |> bb.append
    binVal |> bb.append
    
    p.BindType |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Bind |> BitConverter.GetBytes |> bb.append
    
    p.Project |> BitConverter.GetBytes |> bb.append

let VARTYPE__bin (bb:BytesBuilder) (v:VARTYPE) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pVARTYPE__bin bb v.p

let bin__pVARTYPE (bi:BinIndexed):pVARTYPE =
    let bin,index = bi

    let p = pVARTYPE_empty()
    
    let count_Name = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Name <- Encoding.UTF8.GetString(bin,index.Value,count_Name)
    index.Value <- index.Value + count_Name
    
    let count_Type = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Type <- Encoding.UTF8.GetString(bin,index.Value,count_Type)
    index.Value <- index.Value + count_Type
    
    let count_Val = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Val <- Encoding.UTF8.GetString(bin,index.Value,count_Val)
    index.Value <- index.Value + count_Val
    
    p.BindType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Bind <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Project <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__VARTYPE (bi:BinIndexed):VARTYPE =
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
        p = bin__pVARTYPE bi }

let pVARTYPE__json (p:pVARTYPE) =

    [|
        ("Name",p.Name |> Json.Str)
        ("Type",p.Type |> Json.Str)
        ("Val",p.Val |> Json.Str)
        ("BindType",(p.BindType |> EnumToValue).ToString() |> Json.Num)
        ("Bind",p.Bind.ToString() |> Json.Num)
        ("Project",p.Project.ToString() |> Json.Num) |]
    |> Json.Braket

let VARTYPE__json (v:VARTYPE) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pVARTYPE__json v.p) |]
    |> Json.Braket

let VARTYPE__jsonTbw (w:TextBlockWriter) (v:VARTYPE) =
    json__str w (VARTYPE__json v)

let VARTYPE__jsonStr (v:VARTYPE) =
    (VARTYPE__json v) |> json__strFinal


let json__pVARTYPEo (json:Json):pVARTYPE option =
    let fields = json |> json__items

    let p = pVARTYPE_empty()
    
    p.Name <- checkfieldz fields "Name" 64
    
    p.Type <- checkfieldz fields "Type" 64
    
    p.Val <- checkfield fields "Val"
    
    p.BindType <- checkfield fields "BindType" |> parse_int32 |> EnumOfValue
    
    p.Bind <- checkfield fields "Bind" |> parse_int64
    
    p.Project <- checkfield fields "Project" |> parse_int64
    
    p |> Some
    

let json__VARTYPEo (json:Json):VARTYPE option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pVARTYPEo v
        | None -> None
    
    match o with
    | Some p ->
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

let mutable conn = ""

let db__pADDRESS(line:Object[]): pADDRESS =
    let p = pADDRESS_empty()

    p.Caption <- string(line.[4]).TrimEnd()
    p.Bind <- if Convert.IsDBNull(line.[5]) then 0L else line.[5] :?> int64
    p.AddressType <- EnumOfValue(if Convert.IsDBNull(line.[6]) then 0 else line.[6] :?> int)
    p.Line1 <- string(line.[7]).TrimEnd()
    p.Line2 <- string(line.[8]).TrimEnd()
    p.State <- string(line.[9]).TrimEnd()
    p.County <- string(line.[10]).TrimEnd()
    p.Town <- string(line.[11]).TrimEnd()
    p.Contact <- string(line.[12]).TrimEnd()
    p.Tel <- string(line.[13]).TrimEnd()
    p.Email <- string(line.[14]).TrimEnd()
    p.Zip <- string(line.[15]).TrimEnd()
    p.City <- if Convert.IsDBNull(line.[16]) then 0L else line.[16] :?> int64
    p.Country <- if Convert.IsDBNull(line.[17]) then 0L else line.[17] :?> int64
    p.Remarks <- string(line.[18]).TrimEnd()

    p

let pADDRESS__sps (p:pADDRESS) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Bind", p.Bind) |> kvp__sqlparam
            ("AddressType", p.AddressType) |> kvp__sqlparam
            ("Line1", p.Line1) |> kvp__sqlparam
            ("Line2", p.Line2) |> kvp__sqlparam
            ("State", p.State) |> kvp__sqlparam
            ("County", p.County) |> kvp__sqlparam
            ("Town", p.Town) |> kvp__sqlparam
            ("Contact", p.Contact) |> kvp__sqlparam
            ("Tel", p.Tel) |> kvp__sqlparam
            ("Email", p.Email) |> kvp__sqlparam
            ("Zip", p.Zip) |> kvp__sqlparam
            ("City", p.City) |> kvp__sqlparam
            ("Country", p.Country) |> kvp__sqlparam
            ("Remarks", p.Remarks) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("caption", p.Caption) |> kvp__sqlparam
            ("bind", p.Bind) |> kvp__sqlparam
            ("addresstype", p.AddressType) |> kvp__sqlparam
            ("line1", p.Line1) |> kvp__sqlparam
            ("line2", p.Line2) |> kvp__sqlparam
            ("state", p.State) |> kvp__sqlparam
            ("county", p.County) |> kvp__sqlparam
            ("town", p.Town) |> kvp__sqlparam
            ("contact", p.Contact) |> kvp__sqlparam
            ("tel", p.Tel) |> kvp__sqlparam
            ("email", p.Email) |> kvp__sqlparam
            ("zip", p.Zip) |> kvp__sqlparam
            ("city", p.City) |> kvp__sqlparam
            ("country", p.Country) |> kvp__sqlparam
            ("remarks", p.Remarks) |> kvp__sqlparam |]

let db__ADDRESS = db__Rcd db__pADDRESS

let ADDRESS_wrapper item: ADDRESS =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pADDRESS_clone (p:pADDRESS): pADDRESS = {
    Caption = p.Caption
    Bind = p.Bind
    AddressType = p.AddressType
    Line1 = p.Line1
    Line2 = p.Line2
    State = p.State
    County = p.County
    Town = p.Town
    Contact = p.Contact
    Tel = p.Tel
    Email = p.Email
    Zip = p.Zip
    City = p.City
    Country = p.Country
    Remarks = p.Remarks }

let ADDRESS_update_transaction output (updater,suc,fail) (rcd:ADDRESS) =
    let rollback_p = rcd.p |> pADDRESS_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,ADDRESS_table,ADDRESS_sql_update(),pADDRESS__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let ADDRESS_update output (rcd:ADDRESS) =
    rcd
    |> ADDRESS_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let ADDRESS_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment ADDRESS_id
    let ctime = DateTime.UtcNow
    match create (conn,output,ADDRESS_table,pADDRESS__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> ADDRESS_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let ADDRESS_create output p =
    ADDRESS_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__ADDRESSo id: ADDRESS option = id__rcd(conn,ADDRESS_fieldorders(),ADDRESS_table,db__ADDRESS) id

let ADDRESS_metadata = {
    fieldorders = ADDRESS_fieldorders
    db__rcd = db__ADDRESS 
    wrapper = ADDRESS_wrapper
    sps = pADDRESS__sps
    id = ADDRESS_id
    id__rcdo = id__ADDRESSo
    clone = pADDRESS_clone
    empty__p = pADDRESS_empty
    rcd__bin = ADDRESS__bin
    bin__rcd = bin__ADDRESS
    sql_update = ADDRESS_sql_update
    rcd_update = ADDRESS_update
    table = ADDRESS_table
    shorthand = "address" }

let ADDRESSTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Address' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Address ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Caption]
    ,[Bind]
    ,[AddressType]
    ,[Line1]
    ,[Line2]
    ,[State]
    ,[County]
    ,[Town]
    ,[Contact]
    ,[Tel]
    ,[Email]
    ,[Zip]
    ,[City]
    ,[Country]
    ,[Remarks])
    END
    """


let db__pBIZ(line:Object[]): pBIZ =
    let p = pBIZ_empty()

    p.Code <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.Parent <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64
    p.BasicAcct <- if Convert.IsDBNull(line.[7]) then 0L else line.[7] :?> int64
    p.DescTxt <- string(line.[8]).TrimEnd()
    p.Website <- string(line.[9]).TrimEnd()
    p.Icon <- string(line.[10]).TrimEnd()
    p.City <- if Convert.IsDBNull(line.[11]) then 0L else line.[11] :?> int64
    p.Country <- if Convert.IsDBNull(line.[12]) then 0L else line.[12] :?> int64
    p.Lang <- if Convert.IsDBNull(line.[13]) then 0L else line.[13] :?> int64
    p.IsSocialPlatform <- if Convert.IsDBNull(line.[14]) then false else line.[14] :?> bool
    p.IsCmsSource <- if Convert.IsDBNull(line.[15]) then false else line.[15] :?> bool
    p.IsPayGateway <- if Convert.IsDBNull(line.[16]) then false else line.[16] :?> bool

    p

let pBIZ__sps (p:pBIZ) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Code", p.Code) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Parent", p.Parent) |> kvp__sqlparam
            ("BasicAcct", p.BasicAcct) |> kvp__sqlparam
            ("DescTxt", p.DescTxt) |> kvp__sqlparam
            ("Website", p.Website) |> kvp__sqlparam
            ("Icon", p.Icon) |> kvp__sqlparam
            ("City", p.City) |> kvp__sqlparam
            ("Country", p.Country) |> kvp__sqlparam
            ("Lang", p.Lang) |> kvp__sqlparam
            ("IsSocialPlatform", p.IsSocialPlatform) |> kvp__sqlparam
            ("IsCmsSource", p.IsCmsSource) |> kvp__sqlparam
            ("IsPayGateway", p.IsPayGateway) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("code", p.Code) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam
            ("parent", p.Parent) |> kvp__sqlparam
            ("basicacct", p.BasicAcct) |> kvp__sqlparam
            ("desctxt", p.DescTxt) |> kvp__sqlparam
            ("website", p.Website) |> kvp__sqlparam
            ("icon", p.Icon) |> kvp__sqlparam
            ("city", p.City) |> kvp__sqlparam
            ("country", p.Country) |> kvp__sqlparam
            ("lang", p.Lang) |> kvp__sqlparam
            ("issocialplatform", p.IsSocialPlatform) |> kvp__sqlparam
            ("iscmssource", p.IsCmsSource) |> kvp__sqlparam
            ("ispaygateway", p.IsPayGateway) |> kvp__sqlparam |]

let db__BIZ = db__Rcd db__pBIZ

let BIZ_wrapper item: BIZ =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pBIZ_clone (p:pBIZ): pBIZ = {
    Code = p.Code
    Caption = p.Caption
    Parent = p.Parent
    BasicAcct = p.BasicAcct
    DescTxt = p.DescTxt
    Website = p.Website
    Icon = p.Icon
    City = p.City
    Country = p.Country
    Lang = p.Lang
    IsSocialPlatform = p.IsSocialPlatform
    IsCmsSource = p.IsCmsSource
    IsPayGateway = p.IsPayGateway }

let BIZ_update_transaction output (updater,suc,fail) (rcd:BIZ) =
    let rollback_p = rcd.p |> pBIZ_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,BIZ_table,BIZ_sql_update(),pBIZ__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let BIZ_update output (rcd:BIZ) =
    rcd
    |> BIZ_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let BIZ_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment BIZ_id
    let ctime = DateTime.UtcNow
    match create (conn,output,BIZ_table,pBIZ__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> BIZ_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let BIZ_create output p =
    BIZ_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__BIZo id: BIZ option = id__rcd(conn,BIZ_fieldorders(),BIZ_table,db__BIZ) id

let BIZ_metadata = {
    fieldorders = BIZ_fieldorders
    db__rcd = db__BIZ 
    wrapper = BIZ_wrapper
    sps = pBIZ__sps
    id = BIZ_id
    id__rcdo = id__BIZo
    clone = pBIZ_clone
    empty__p = pBIZ_empty
    rcd__bin = BIZ__bin
    bin__rcd = bin__BIZ
    sql_update = BIZ_sql_update
    rcd_update = BIZ_update
    table = BIZ_table
    shorthand = "biz" }

let BIZTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Biz' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Biz ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Code]
    ,[Caption]
    ,[Parent]
    ,[BasicAcct]
    ,[DescTxt]
    ,[Website]
    ,[Icon]
    ,[City]
    ,[Country]
    ,[Lang]
    ,[IsSocialPlatform]
    ,[IsCmsSource]
    ,[IsPayGateway])
    END
    """


let db__pCAT(line:Object[]): pCAT =
    let p = pCAT_empty()

    p.Caption <- string(line.[4]).TrimEnd()
    p.Lang <- if Convert.IsDBNull(line.[5]) then 0L else line.[5] :?> int64
    p.Zh <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64
    p.Parent <- if Convert.IsDBNull(line.[7]) then 0L else line.[7] :?> int64
    p.CatState <- EnumOfValue(if Convert.IsDBNull(line.[8]) then 0 else line.[8] :?> int)

    p

let pCAT__sps (p:pCAT) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Lang", p.Lang) |> kvp__sqlparam
            ("Zh", p.Zh) |> kvp__sqlparam
            ("Parent", p.Parent) |> kvp__sqlparam
            ("CatState", p.CatState) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("caption", p.Caption) |> kvp__sqlparam
            ("lang", p.Lang) |> kvp__sqlparam
            ("zh", p.Zh) |> kvp__sqlparam
            ("parent", p.Parent) |> kvp__sqlparam
            ("catstate", p.CatState) |> kvp__sqlparam |]

let db__CAT = db__Rcd db__pCAT

let CAT_wrapper item: CAT =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pCAT_clone (p:pCAT): pCAT = {
    Caption = p.Caption
    Lang = p.Lang
    Zh = p.Zh
    Parent = p.Parent
    CatState = p.CatState }

let CAT_update_transaction output (updater,suc,fail) (rcd:CAT) =
    let rollback_p = rcd.p |> pCAT_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,CAT_table,CAT_sql_update(),pCAT__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let CAT_update output (rcd:CAT) =
    rcd
    |> CAT_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let CAT_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment CAT_id
    let ctime = DateTime.UtcNow
    match create (conn,output,CAT_table,pCAT__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> CAT_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let CAT_create output p =
    CAT_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__CATo id: CAT option = id__rcd(conn,CAT_fieldorders(),CAT_table,db__CAT) id

let CAT_metadata = {
    fieldorders = CAT_fieldorders
    db__rcd = db__CAT 
    wrapper = CAT_wrapper
    sps = pCAT__sps
    id = CAT_id
    id__rcdo = id__CATo
    clone = pCAT_clone
    empty__p = pCAT_empty
    rcd__bin = CAT__bin
    bin__rcd = bin__CAT
    sql_update = CAT_sql_update
    rcd_update = CAT_update
    table = CAT_table
    shorthand = "cat" }

let CATTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Cat' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Cat ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Caption]
    ,[Lang]
    ,[Zh]
    ,[Parent]
    ,[CatState])
    END
    """


let db__pCITY(line:Object[]): pCITY =
    let p = pCITY_empty()

    p.Fullname <- string(line.[4]).TrimEnd()
    p.MetropolitanCode3IATA <- string(line.[5]).TrimEnd()
    p.NameEn <- string(line.[6]).TrimEnd()
    p.Country <- if Convert.IsDBNull(line.[7]) then 0L else line.[7] :?> int64
    p.Place <- if Convert.IsDBNull(line.[8]) then 0L else line.[8] :?> int64
    p.Icon <- string(line.[9]).TrimEnd()
    p.Tel <- string(line.[10]).TrimEnd()

    p

let pCITY__sps (p:pCITY) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Fullname", p.Fullname) |> kvp__sqlparam
            ("MetropolitanCode3IATA", p.MetropolitanCode3IATA) |> kvp__sqlparam
            ("NameEn", p.NameEn) |> kvp__sqlparam
            ("Country", p.Country) |> kvp__sqlparam
            ("Place", p.Place) |> kvp__sqlparam
            ("Icon", p.Icon) |> kvp__sqlparam
            ("Tel", p.Tel) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("fullname", p.Fullname) |> kvp__sqlparam
            ("metropolitancode3iata", p.MetropolitanCode3IATA) |> kvp__sqlparam
            ("nameen", p.NameEn) |> kvp__sqlparam
            ("country", p.Country) |> kvp__sqlparam
            ("place", p.Place) |> kvp__sqlparam
            ("icon", p.Icon) |> kvp__sqlparam
            ("tel", p.Tel) |> kvp__sqlparam |]

let db__CITY = db__Rcd db__pCITY

let CITY_wrapper item: CITY =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pCITY_clone (p:pCITY): pCITY = {
    Fullname = p.Fullname
    MetropolitanCode3IATA = p.MetropolitanCode3IATA
    NameEn = p.NameEn
    Country = p.Country
    Place = p.Place
    Icon = p.Icon
    Tel = p.Tel }

let CITY_update_transaction output (updater,suc,fail) (rcd:CITY) =
    let rollback_p = rcd.p |> pCITY_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,CITY_table,CITY_sql_update(),pCITY__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let CITY_update output (rcd:CITY) =
    rcd
    |> CITY_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let CITY_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment CITY_id
    let ctime = DateTime.UtcNow
    match create (conn,output,CITY_table,pCITY__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> CITY_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let CITY_create output p =
    CITY_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__CITYo id: CITY option = id__rcd(conn,CITY_fieldorders(),CITY_table,db__CITY) id

let CITY_metadata = {
    fieldorders = CITY_fieldorders
    db__rcd = db__CITY 
    wrapper = CITY_wrapper
    sps = pCITY__sps
    id = CITY_id
    id__rcdo = id__CITYo
    clone = pCITY_clone
    empty__p = pCITY_empty
    rcd__bin = CITY__bin
    bin__rcd = bin__CITY
    sql_update = CITY_sql_update
    rcd_update = CITY_update
    table = CITY_table
    shorthand = "city" }

let CITYTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_City' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_City ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Fullname]
    ,[MetropolitanCode3IATA]
    ,[NameEn]
    ,[Country]
    ,[Place]
    ,[Icon]
    ,[Tel])
    END
    """


let db__pCRY(line:Object[]): pCRY =
    let p = pCRY_empty()

    p.Code2 <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.Fullname <- string(line.[6]).TrimEnd()
    p.Icon <- string(line.[7]).TrimEnd()
    p.Tel <- string(line.[8]).TrimEnd()
    p.Cur <- if Convert.IsDBNull(line.[9]) then 0L else line.[9] :?> int64
    p.Capital <- if Convert.IsDBNull(line.[10]) then 0L else line.[10] :?> int64
    p.Place <- if Convert.IsDBNull(line.[11]) then 0L else line.[11] :?> int64
    p.Lang <- if Convert.IsDBNull(line.[12]) then 0L else line.[12] :?> int64

    p

let pCRY__sps (p:pCRY) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Code2", p.Code2) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Fullname", p.Fullname) |> kvp__sqlparam
            ("Icon", p.Icon) |> kvp__sqlparam
            ("Tel", p.Tel) |> kvp__sqlparam
            ("Cur", p.Cur) |> kvp__sqlparam
            ("Capital", p.Capital) |> kvp__sqlparam
            ("Place", p.Place) |> kvp__sqlparam
            ("Lang", p.Lang) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("code2", p.Code2) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam
            ("fullname", p.Fullname) |> kvp__sqlparam
            ("icon", p.Icon) |> kvp__sqlparam
            ("tel", p.Tel) |> kvp__sqlparam
            ("cur", p.Cur) |> kvp__sqlparam
            ("capital", p.Capital) |> kvp__sqlparam
            ("place", p.Place) |> kvp__sqlparam
            ("lang", p.Lang) |> kvp__sqlparam |]

let db__CRY = db__Rcd db__pCRY

let CRY_wrapper item: CRY =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pCRY_clone (p:pCRY): pCRY = {
    Code2 = p.Code2
    Caption = p.Caption
    Fullname = p.Fullname
    Icon = p.Icon
    Tel = p.Tel
    Cur = p.Cur
    Capital = p.Capital
    Place = p.Place
    Lang = p.Lang }

let CRY_update_transaction output (updater,suc,fail) (rcd:CRY) =
    let rollback_p = rcd.p |> pCRY_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,CRY_table,CRY_sql_update(),pCRY__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let CRY_update output (rcd:CRY) =
    rcd
    |> CRY_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let CRY_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment CRY_id
    let ctime = DateTime.UtcNow
    match create (conn,output,CRY_table,pCRY__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> CRY_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let CRY_create output p =
    CRY_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__CRYo id: CRY option = id__rcd(conn,CRY_fieldorders(),CRY_table,db__CRY) id

let CRY_metadata = {
    fieldorders = CRY_fieldorders
    db__rcd = db__CRY 
    wrapper = CRY_wrapper
    sps = pCRY__sps
    id = CRY_id
    id__rcdo = id__CRYo
    clone = pCRY_clone
    empty__p = pCRY_empty
    rcd__bin = CRY__bin
    bin__rcd = bin__CRY
    sql_update = CRY_sql_update
    rcd_update = CRY_update
    table = CRY_table
    shorthand = "cry" }

let CRYTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Country' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Country ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Code2]
    ,[Caption]
    ,[Fullname]
    ,[Icon]
    ,[Tel]
    ,[Cur]
    ,[Capital]
    ,[Place]
    ,[Lang])
    END
    """


let db__pEU(line:Object[]): pEU =
    let p = pEU_empty()

    p.Caption <- string(line.[4]).TrimEnd()
    p.Username <- string(line.[5]).TrimEnd()
    p.SocialAuthBiz <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64
    p.SocialAuthId <- string(line.[7]).TrimEnd()
    p.SocialAuthAvatar <- string(line.[8]).TrimEnd()
    p.Email <- string(line.[9]).TrimEnd()
    p.Tel <- string(line.[10]).TrimEnd()
    p.Gender <- EnumOfValue(if Convert.IsDBNull(line.[11]) then 0 else line.[11] :?> int)
    p.Status <- EnumOfValue(if Convert.IsDBNull(line.[12]) then 0 else line.[12] :?> int)
    p.Admin <- EnumOfValue(if Convert.IsDBNull(line.[13]) then 0 else line.[13] :?> int)
    p.BizPartner <- EnumOfValue(if Convert.IsDBNull(line.[14]) then 0 else line.[14] :?> int)
    p.Privilege <- if Convert.IsDBNull(line.[15]) then 0L else line.[15] :?> int64
    p.Verify <- EnumOfValue(if Convert.IsDBNull(line.[16]) then 0 else line.[16] :?> int)
    p.Pwd <- string(line.[17]).TrimEnd()
    p.Online <- if Convert.IsDBNull(line.[18]) then false else line.[18] :?> bool
    p.Icon <- string(line.[19]).TrimEnd()
    p.Background <- string(line.[20]).TrimEnd()
    p.BasicAcct <- if Convert.IsDBNull(line.[21]) then 0L else line.[21] :?> int64
    p.Citizen <- if Convert.IsDBNull(line.[22]) then 0L else line.[22] :?> int64
    p.Refer <- string(line.[23]).TrimEnd()
    p.Referer <- if Convert.IsDBNull(line.[24]) then 0L else line.[24] :?> int64
    p.Url <- string(line.[25]).TrimEnd()
    p.About <- string(line.[26]).TrimEnd()

    p

let pEU__sps (p:pEU) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Username", p.Username) |> kvp__sqlparam
            ("SocialAuthBiz", p.SocialAuthBiz) |> kvp__sqlparam
            ("SocialAuthId", p.SocialAuthId) |> kvp__sqlparam
            ("SocialAuthAvatar", p.SocialAuthAvatar) |> kvp__sqlparam
            ("Email", p.Email) |> kvp__sqlparam
            ("Tel", p.Tel) |> kvp__sqlparam
            ("Gender", p.Gender) |> kvp__sqlparam
            ("Status", p.Status) |> kvp__sqlparam
            ("Admin", p.Admin) |> kvp__sqlparam
            ("BizPartner", p.BizPartner) |> kvp__sqlparam
            ("Privilege", p.Privilege) |> kvp__sqlparam
            ("Verify", p.Verify) |> kvp__sqlparam
            ("Pwd", p.Pwd) |> kvp__sqlparam
            ("Online", p.Online) |> kvp__sqlparam
            ("Icon", p.Icon) |> kvp__sqlparam
            ("Background", p.Background) |> kvp__sqlparam
            ("BasicAcct", p.BasicAcct) |> kvp__sqlparam
            ("Citizen", p.Citizen) |> kvp__sqlparam
            ("Refer", p.Refer) |> kvp__sqlparam
            ("Referer", p.Referer) |> kvp__sqlparam
            ("Url", p.Url) |> kvp__sqlparam
            ("About", p.About) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("caption", p.Caption) |> kvp__sqlparam
            ("username", p.Username) |> kvp__sqlparam
            ("socialauthbiz", p.SocialAuthBiz) |> kvp__sqlparam
            ("socialauthid", p.SocialAuthId) |> kvp__sqlparam
            ("socialauthavatar", p.SocialAuthAvatar) |> kvp__sqlparam
            ("email", p.Email) |> kvp__sqlparam
            ("tel", p.Tel) |> kvp__sqlparam
            ("gender", p.Gender) |> kvp__sqlparam
            ("status", p.Status) |> kvp__sqlparam
            ("admin", p.Admin) |> kvp__sqlparam
            ("bizpartner", p.BizPartner) |> kvp__sqlparam
            ("privilege", p.Privilege) |> kvp__sqlparam
            ("verify", p.Verify) |> kvp__sqlparam
            ("pwd", p.Pwd) |> kvp__sqlparam
            ("online", p.Online) |> kvp__sqlparam
            ("icon", p.Icon) |> kvp__sqlparam
            ("background", p.Background) |> kvp__sqlparam
            ("basicacct", p.BasicAcct) |> kvp__sqlparam
            ("citizen", p.Citizen) |> kvp__sqlparam
            ("refer", p.Refer) |> kvp__sqlparam
            ("referer", p.Referer) |> kvp__sqlparam
            ("url", p.Url) |> kvp__sqlparam
            ("about", p.About) |> kvp__sqlparam |]

let db__EU = db__Rcd db__pEU

let EU_wrapper item: EU =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pEU_clone (p:pEU): pEU = {
    Caption = p.Caption
    Username = p.Username
    SocialAuthBiz = p.SocialAuthBiz
    SocialAuthId = p.SocialAuthId
    SocialAuthAvatar = p.SocialAuthAvatar
    Email = p.Email
    Tel = p.Tel
    Gender = p.Gender
    Status = p.Status
    Admin = p.Admin
    BizPartner = p.BizPartner
    Privilege = p.Privilege
    Verify = p.Verify
    Pwd = p.Pwd
    Online = p.Online
    Icon = p.Icon
    Background = p.Background
    BasicAcct = p.BasicAcct
    Citizen = p.Citizen
    Refer = p.Refer
    Referer = p.Referer
    Url = p.Url
    About = p.About }

let EU_update_transaction output (updater,suc,fail) (rcd:EU) =
    let rollback_p = rcd.p |> pEU_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,EU_table,EU_sql_update(),pEU__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let EU_update output (rcd:EU) =
    rcd
    |> EU_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let EU_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment EU_id
    let ctime = DateTime.UtcNow
    match create (conn,output,EU_table,pEU__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> EU_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let EU_create output p =
    EU_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__EUo id: EU option = id__rcd(conn,EU_fieldorders(),EU_table,db__EU) id

let EU_metadata = {
    fieldorders = EU_fieldorders
    db__rcd = db__EU 
    wrapper = EU_wrapper
    sps = pEU__sps
    id = EU_id
    id__rcdo = id__EUo
    clone = pEU_clone
    empty__p = pEU_empty
    rcd__bin = EU__bin
    bin__rcd = bin__EU
    sql_update = EU_sql_update
    rcd_update = EU_update
    table = EU_table
    shorthand = "eu" }

let EUTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_EndUser' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_EndUser ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Caption]
    ,[Username]
    ,[SocialAuthBiz]
    ,[SocialAuthId]
    ,[SocialAuthAvatar]
    ,[Email]
    ,[Tel]
    ,[Gender]
    ,[Status]
    ,[Admin]
    ,[BizPartner]
    ,[Privilege]
    ,[Verify]
    ,[Pwd]
    ,[Online]
    ,[Icon]
    ,[Background]
    ,[BasicAcct]
    ,[Citizen]
    ,[Refer]
    ,[Referer]
    ,[Url]
    ,[About])
    END
    """


let db__pCSI(line:Object[]): pCSI =
    let p = pCSI_empty()

    p.Type <- EnumOfValue(if Convert.IsDBNull(line.[4]) then 0 else line.[4] :?> int)
    p.Lang <- if Convert.IsDBNull(line.[5]) then 0L else line.[5] :?> int64
    p.Bind <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64

    p

let pCSI__sps (p:pCSI) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Type", p.Type) |> kvp__sqlparam
            ("Lang", p.Lang) |> kvp__sqlparam
            ("Bind", p.Bind) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("type", p.Type) |> kvp__sqlparam
            ("lang", p.Lang) |> kvp__sqlparam
            ("bind", p.Bind) |> kvp__sqlparam |]

let db__CSI = db__Rcd db__pCSI

let CSI_wrapper item: CSI =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pCSI_clone (p:pCSI): pCSI = {
    Type = p.Type
    Lang = p.Lang
    Bind = p.Bind }

let CSI_update_transaction output (updater,suc,fail) (rcd:CSI) =
    let rollback_p = rcd.p |> pCSI_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,CSI_table,CSI_sql_update(),pCSI__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let CSI_update output (rcd:CSI) =
    rcd
    |> CSI_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let CSI_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment CSI_id
    let ctime = DateTime.UtcNow
    match create (conn,output,CSI_table,pCSI__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> CSI_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let CSI_create output p =
    CSI_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__CSIo id: CSI option = id__rcd(conn,CSI_fieldorders(),CSI_table,db__CSI) id

let CSI_metadata = {
    fieldorders = CSI_fieldorders
    db__rcd = db__CSI 
    wrapper = CSI_wrapper
    sps = pCSI__sps
    id = CSI_id
    id__rcdo = id__CSIo
    clone = pCSI_clone
    empty__p = pCSI_empty
    rcd__bin = CSI__bin
    bin__rcd = bin__CSI
    sql_update = CSI_sql_update
    rcd_update = CSI_update
    table = CSI_table
    shorthand = "csi" }

let CSITxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_SpecialItem' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_SpecialItem ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Type]
    ,[Lang]
    ,[Bind])
    END
    """


let db__pCWC(line:Object[]): pCWC =
    let p = pCWC_empty()

    p.Caption <- string(line.[4]).TrimEnd()
    p.ExternalId <- if Convert.IsDBNull(line.[5]) then 0L else line.[5] :?> int64
    p.Icon <- string(line.[6]).TrimEnd()
    p.EU <- if Convert.IsDBNull(line.[7]) then 0L else line.[7] :?> int64
    p.Biz <- if Convert.IsDBNull(line.[8]) then 0L else line.[8] :?> int64
    p.Json <- string(line.[9]).TrimEnd()

    p

let pCWC__sps (p:pCWC) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Caption", p.Caption) |> kvp__sqlparam
            ("ExternalId", p.ExternalId) |> kvp__sqlparam
            ("Icon", p.Icon) |> kvp__sqlparam
            ("EU", p.EU) |> kvp__sqlparam
            ("Biz", p.Biz) |> kvp__sqlparam
            ("Json", p.Json) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("caption", p.Caption) |> kvp__sqlparam
            ("externalid", p.ExternalId) |> kvp__sqlparam
            ("icon", p.Icon) |> kvp__sqlparam
            ("eu", p.EU) |> kvp__sqlparam
            ("biz", p.Biz) |> kvp__sqlparam
            ("json", p.Json) |> kvp__sqlparam |]

let db__CWC = db__Rcd db__pCWC

let CWC_wrapper item: CWC =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pCWC_clone (p:pCWC): pCWC = {
    Caption = p.Caption
    ExternalId = p.ExternalId
    Icon = p.Icon
    EU = p.EU
    Biz = p.Biz
    Json = p.Json }

let CWC_update_transaction output (updater,suc,fail) (rcd:CWC) =
    let rollback_p = rcd.p |> pCWC_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,CWC_table,CWC_sql_update(),pCWC__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let CWC_update output (rcd:CWC) =
    rcd
    |> CWC_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let CWC_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment CWC_id
    let ctime = DateTime.UtcNow
    match create (conn,output,CWC_table,pCWC__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> CWC_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let CWC_create output p =
    CWC_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__CWCo id: CWC option = id__rcd(conn,CWC_fieldorders(),CWC_table,db__CWC) id

let CWC_metadata = {
    fieldorders = CWC_fieldorders
    db__rcd = db__CWC 
    wrapper = CWC_wrapper
    sps = pCWC__sps
    id = CWC_id
    id__rcdo = id__CWCo
    clone = pCWC_clone
    empty__p = pCWC_empty
    rcd__bin = CWC__bin
    bin__rcd = bin__CWC
    sql_update = CWC_sql_update
    rcd_update = CWC_update
    table = CWC_table
    shorthand = "cwc" }

let CWCTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_WebCredential' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_WebCredential ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Caption]
    ,[ExternalId]
    ,[Icon]
    ,[EU]
    ,[Biz]
    ,[Json])
    END
    """


let db__pAPI(line:Object[]): pAPI =
    let p = pAPI_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line.[5]) then 0L else line.[5] :?> int64

    p

let pAPI__sps (p:pAPI) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam |]

let db__API = db__Rcd db__pAPI

let API_wrapper item: API =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pAPI_clone (p:pAPI): pAPI = {
    Name = p.Name
    Project = p.Project }

let API_update_transaction output (updater,suc,fail) (rcd:API) =
    let rollback_p = rcd.p |> pAPI_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,API_table,API_sql_update(),pAPI__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let API_update output (rcd:API) =
    rcd
    |> API_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let API_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment API_id
    let ctime = DateTime.UtcNow
    match create (conn,output,API_table,pAPI__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> API_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let API_create output p =
    API_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__APIo id: API option = id__rcd(conn,API_fieldorders(),API_table,db__API) id

let API_metadata = {
    fieldorders = API_fieldorders
    db__rcd = db__API 
    wrapper = API_wrapper
    sps = pAPI__sps
    id = API_id
    id__rcdo = id__APIo
    clone = pAPI_clone
    empty__p = pAPI_empty
    rcd__bin = API__bin
    bin__rcd = bin__API
    sql_update = API_sql_update
    rcd_update = API_update
    table = API_table
    shorthand = "api" }

let APITxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_Api' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_Api ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Name]
    ,[Project])
    END
    """


let db__pFIELD(line:Object[]): pFIELD =
    let p = pFIELD_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Desc <- string(line.[5]).TrimEnd()
    p.FieldType <- EnumOfValue(if Convert.IsDBNull(line.[6]) then 0 else line.[6] :?> int)
    p.Length <- if Convert.IsDBNull(line.[7]) then 0L else line.[7] :?> int64
    p.SelectLines <- string(line.[8]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line.[9]) then 0L else line.[9] :?> int64
    p.Table <- if Convert.IsDBNull(line.[10]) then 0L else line.[10] :?> int64

    p

let pFIELD__sps (p:pFIELD) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Desc", p.Desc) |> kvp__sqlparam
            ("FieldType", p.FieldType) |> kvp__sqlparam
            ("Length", p.Length) |> kvp__sqlparam
            ("SelectLines", p.SelectLines) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam
            ("Table", p.Table) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("desc", p.Desc) |> kvp__sqlparam
            ("fieldtype", p.FieldType) |> kvp__sqlparam
            ("length", p.Length) |> kvp__sqlparam
            ("selectlines", p.SelectLines) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam
            ("table", p.Table) |> kvp__sqlparam |]

let db__FIELD = db__Rcd db__pFIELD

let FIELD_wrapper item: FIELD =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pFIELD_clone (p:pFIELD): pFIELD = {
    Name = p.Name
    Desc = p.Desc
    FieldType = p.FieldType
    Length = p.Length
    SelectLines = p.SelectLines
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
    ,[FieldType]
    ,[Length]
    ,[SelectLines]
    ,[Project]
    ,[Table])
    END
    """


let db__pHOSTCONFIG(line:Object[]): pHOSTCONFIG =
    let p = pHOSTCONFIG_empty()

    p.Hostname <- string(line.[4]).TrimEnd()
    p.Database <- EnumOfValue(if Convert.IsDBNull(line.[5]) then 0 else line.[5] :?> int)
    p.DatabaseName <- string(line.[6]).TrimEnd()
    p.DatabaseConn <- string(line.[7]).TrimEnd()
    p.DirVs <- string(line.[8]).TrimEnd()
    p.DirVsCodeWeb <- string(line.[9]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line.[10]) then 0L else line.[10] :?> int64

    p

let pHOSTCONFIG__sps (p:pHOSTCONFIG) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Hostname", p.Hostname) |> kvp__sqlparam
            ("Database", p.Database) |> kvp__sqlparam
            ("DatabaseName", p.DatabaseName) |> kvp__sqlparam
            ("DatabaseConn", p.DatabaseConn) |> kvp__sqlparam
            ("DirVs", p.DirVs) |> kvp__sqlparam
            ("DirVsCodeWeb", p.DirVsCodeWeb) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("hostname", p.Hostname) |> kvp__sqlparam
            ("database", p.Database) |> kvp__sqlparam
            ("databasename", p.DatabaseName) |> kvp__sqlparam
            ("databaseconn", p.DatabaseConn) |> kvp__sqlparam
            ("dirvs", p.DirVs) |> kvp__sqlparam
            ("dirvscodeweb", p.DirVsCodeWeb) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam |]

let db__HOSTCONFIG = db__Rcd db__pHOSTCONFIG

let HOSTCONFIG_wrapper item: HOSTCONFIG =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pHOSTCONFIG_clone (p:pHOSTCONFIG): pHOSTCONFIG = {
    Hostname = p.Hostname
    Database = p.Database
    DatabaseName = p.DatabaseName
    DatabaseConn = p.DatabaseConn
    DirVs = p.DirVs
    DirVsCodeWeb = p.DirVsCodeWeb
    Project = p.Project }

let HOSTCONFIG_update_transaction output (updater,suc,fail) (rcd:HOSTCONFIG) =
    let rollback_p = rcd.p |> pHOSTCONFIG_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,HOSTCONFIG_table,HOSTCONFIG_sql_update(),pHOSTCONFIG__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let HOSTCONFIG_update output (rcd:HOSTCONFIG) =
    rcd
    |> HOSTCONFIG_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let HOSTCONFIG_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment HOSTCONFIG_id
    let ctime = DateTime.UtcNow
    match create (conn,output,HOSTCONFIG_table,pHOSTCONFIG__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> HOSTCONFIG_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let HOSTCONFIG_create output p =
    HOSTCONFIG_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__HOSTCONFIGo id: HOSTCONFIG option = id__rcd(conn,HOSTCONFIG_fieldorders(),HOSTCONFIG_table,db__HOSTCONFIG) id

let HOSTCONFIG_metadata = {
    fieldorders = HOSTCONFIG_fieldorders
    db__rcd = db__HOSTCONFIG 
    wrapper = HOSTCONFIG_wrapper
    sps = pHOSTCONFIG__sps
    id = HOSTCONFIG_id
    id__rcdo = id__HOSTCONFIGo
    clone = pHOSTCONFIG_clone
    empty__p = pHOSTCONFIG_empty
    rcd__bin = HOSTCONFIG__bin
    bin__rcd = bin__HOSTCONFIG
    sql_update = HOSTCONFIG_sql_update
    rcd_update = HOSTCONFIG_update
    table = HOSTCONFIG_table
    shorthand = "hostconfig" }

let HOSTCONFIGTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_HostConfig' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_HostConfig ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Hostname]
    ,[Database]
    ,[DatabaseName]
    ,[DatabaseConn]
    ,[DirVs]
    ,[DirVsCodeWeb]
    ,[Project])
    END
    """


let db__pPROJECT(line:Object[]): pPROJECT =
    let p = pPROJECT_empty()

    p.Code <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.TypeSessionUser <- string(line.[6]).TrimEnd()

    p

let pPROJECT__sps (p:pPROJECT) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Code", p.Code) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam
            ("TypeSessionUser", p.TypeSessionUser) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("code", p.Code) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam
            ("typesessionuser", p.TypeSessionUser) |> kvp__sqlparam |]

let db__PROJECT = db__Rcd db__pPROJECT

let PROJECT_wrapper item: PROJECT =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pPROJECT_clone (p:pPROJECT): pPROJECT = {
    Code = p.Code
    Caption = p.Caption
    TypeSessionUser = p.TypeSessionUser }

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
    ,[Caption]
    ,[TypeSessionUser])
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
    p.Route <- string(line.[6]).TrimEnd()
    p.OgTitle <- string(line.[7]).TrimEnd()
    p.OgDesc <- string(line.[8]).TrimEnd()
    p.OgImage <- string(line.[9]).TrimEnd()
    p.Template <- if Convert.IsDBNull(line.[10]) then 0L else line.[10] :?> int64
    p.Project <- if Convert.IsDBNull(line.[11]) then 0L else line.[11] :?> int64

    p

let pPAGE__sps (p:pPAGE) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Route", p.Route) |> kvp__sqlparam
            ("OgTitle", p.OgTitle) |> kvp__sqlparam
            ("OgDesc", p.OgDesc) |> kvp__sqlparam
            ("OgImage", p.OgImage) |> kvp__sqlparam
            ("Template", p.Template) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam
            ("route", p.Route) |> kvp__sqlparam
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
    Route = p.Route
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
    ,[Route]
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


let db__pVARTYPE(line:Object[]): pVARTYPE =
    let p = pVARTYPE_empty()

    p.Name <- string(line.[4]).TrimEnd()
    p.Type <- string(line.[5]).TrimEnd()
    p.Val <- string(line.[6]).TrimEnd()
    p.BindType <- EnumOfValue(if Convert.IsDBNull(line.[7]) then 0 else line.[7] :?> int)
    p.Bind <- if Convert.IsDBNull(line.[8]) then 0L else line.[8] :?> int64
    p.Project <- if Convert.IsDBNull(line.[9]) then 0L else line.[9] :?> int64

    p

let pVARTYPE__sps (p:pVARTYPE) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Name", p.Name) |> kvp__sqlparam
            ("Type", p.Type) |> kvp__sqlparam
            ("Val", p.Val) |> kvp__sqlparam
            ("BindType", p.BindType) |> kvp__sqlparam
            ("Bind", p.Bind) |> kvp__sqlparam
            ("Project", p.Project) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("name", p.Name) |> kvp__sqlparam
            ("type", p.Type) |> kvp__sqlparam
            ("val", p.Val) |> kvp__sqlparam
            ("bindtype", p.BindType) |> kvp__sqlparam
            ("bind", p.Bind) |> kvp__sqlparam
            ("project", p.Project) |> kvp__sqlparam |]

let db__VARTYPE = db__Rcd db__pVARTYPE

let VARTYPE_wrapper item: VARTYPE =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pVARTYPE_clone (p:pVARTYPE): pVARTYPE = {
    Name = p.Name
    Type = p.Type
    Val = p.Val
    BindType = p.BindType
    Bind = p.Bind
    Project = p.Project }

let VARTYPE_update_transaction output (updater,suc,fail) (rcd:VARTYPE) =
    let rollback_p = rcd.p |> pVARTYPE_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,VARTYPE_table,VARTYPE_sql_update(),pVARTYPE__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let VARTYPE_update output (rcd:VARTYPE) =
    rcd
    |> VARTYPE_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let VARTYPE_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment VARTYPE_id
    let ctime = DateTime.UtcNow
    match create (conn,output,VARTYPE_table,pVARTYPE__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> VARTYPE_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let VARTYPE_create output p =
    VARTYPE_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__VARTYPEo id: VARTYPE option = id__rcd(conn,VARTYPE_fieldorders(),VARTYPE_table,db__VARTYPE) id

let VARTYPE_metadata = {
    fieldorders = VARTYPE_fieldorders
    db__rcd = db__VARTYPE 
    wrapper = VARTYPE_wrapper
    sps = pVARTYPE__sps
    id = VARTYPE_id
    id__rcdo = id__VARTYPEo
    clone = pVARTYPE_clone
    empty__p = pVARTYPE_empty
    rcd__bin = VARTYPE__bin
    bin__rcd = bin__VARTYPE
    sql_update = VARTYPE_sql_update
    rcd_update = VARTYPE_update
    table = VARTYPE_table
    shorthand = "vartype" }

let VARTYPETxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ts_VarType' AND xtype='U')
    BEGIN

        CREATE TABLE Ts_VarType ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Name]
    ,[Type]
    ,[Val]
    ,[BindType]
    ,[Bind]
    ,[Project])
    END
    """


type MetadataEnum = 
| ADDRESS = 0
| BIZ = 1
| CAT = 2
| CITY = 3
| CRY = 4
| EU = 5
| CSI = 6
| CWC = 7
| API = 8
| FIELD = 9
| HOSTCONFIG = 10
| PROJECT = 11
| TABLE = 12
| COMP = 13
| PAGE = 14
| TEMPLATE = 15
| VARTYPE = 16

let tablenames = [|
    ADDRESS_metadata.table
    BIZ_metadata.table
    CAT_metadata.table
    CITY_metadata.table
    CRY_metadata.table
    EU_metadata.table
    CSI_metadata.table
    CWC_metadata.table
    API_metadata.table
    FIELD_metadata.table
    HOSTCONFIG_metadata.table
    PROJECT_metadata.table
    TABLE_metadata.table
    COMP_metadata.table
    PAGE_metadata.table
    TEMPLATE_metadata.table
    VARTYPE_metadata.table |]

let init() =

    let sqlMaxCa_Address, sqlCountCa_Address =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_Address]", "SELECT COUNT(ID) FROM [Ca_Address]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_address", "SELECT COUNT(id) FROM ca_address"
    match singlevalue_query conn (str__sql sqlMaxCa_Address) with
    | Some v ->
        let max = v :?> int64
        if max > ADDRESS_id.Value then
            ADDRESS_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_Address) with
    | Some v ->
        ADDRESS_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxCa_Biz, sqlCountCa_Biz =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_Biz]", "SELECT COUNT(ID) FROM [Ca_Biz]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_biz", "SELECT COUNT(id) FROM ca_biz"
    match singlevalue_query conn (str__sql sqlMaxCa_Biz) with
    | Some v ->
        let max = v :?> int64
        if max > BIZ_id.Value then
            BIZ_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_Biz) with
    | Some v ->
        BIZ_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxCa_Cat, sqlCountCa_Cat =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_Cat]", "SELECT COUNT(ID) FROM [Ca_Cat]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_cat", "SELECT COUNT(id) FROM ca_cat"
    match singlevalue_query conn (str__sql sqlMaxCa_Cat) with
    | Some v ->
        let max = v :?> int64
        if max > CAT_id.Value then
            CAT_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_Cat) with
    | Some v ->
        CAT_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxCa_City, sqlCountCa_City =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_City]", "SELECT COUNT(ID) FROM [Ca_City]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_city", "SELECT COUNT(id) FROM ca_city"
    match singlevalue_query conn (str__sql sqlMaxCa_City) with
    | Some v ->
        let max = v :?> int64
        if max > CITY_id.Value then
            CITY_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_City) with
    | Some v ->
        CITY_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxCa_Country, sqlCountCa_Country =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_Country]", "SELECT COUNT(ID) FROM [Ca_Country]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_country", "SELECT COUNT(id) FROM ca_country"
    match singlevalue_query conn (str__sql sqlMaxCa_Country) with
    | Some v ->
        let max = v :?> int64
        if max > CRY_id.Value then
            CRY_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_Country) with
    | Some v ->
        CRY_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxCa_EndUser, sqlCountCa_EndUser =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_EndUser]", "SELECT COUNT(ID) FROM [Ca_EndUser]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_enduser", "SELECT COUNT(id) FROM ca_enduser"
    match singlevalue_query conn (str__sql sqlMaxCa_EndUser) with
    | Some v ->
        let max = v :?> int64
        if max > EU_id.Value then
            EU_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_EndUser) with
    | Some v ->
        EU_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxCa_SpecialItem, sqlCountCa_SpecialItem =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_SpecialItem]", "SELECT COUNT(ID) FROM [Ca_SpecialItem]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_specialitem", "SELECT COUNT(id) FROM ca_specialitem"
    match singlevalue_query conn (str__sql sqlMaxCa_SpecialItem) with
    | Some v ->
        let max = v :?> int64
        if max > CSI_id.Value then
            CSI_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_SpecialItem) with
    | Some v ->
        CSI_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxCa_WebCredential, sqlCountCa_WebCredential =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_WebCredential]", "SELECT COUNT(ID) FROM [Ca_WebCredential]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_webcredential", "SELECT COUNT(id) FROM ca_webcredential"
    match singlevalue_query conn (str__sql sqlMaxCa_WebCredential) with
    | Some v ->
        let max = v :?> int64
        if max > CWC_id.Value then
            CWC_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_WebCredential) with
    | Some v ->
        CWC_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxTs_Api, sqlCountTs_Api =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_Api]", "SELECT COUNT(ID) FROM [Ts_Api]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_api", "SELECT COUNT(id) FROM ts_api"
    match singlevalue_query conn (str__sql sqlMaxTs_Api) with
    | Some v ->
        let max = v :?> int64
        if max > API_id.Value then
            API_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_Api) with
    | Some v ->
        API_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

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

    let sqlMaxTs_HostConfig, sqlCountTs_HostConfig =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_HostConfig]", "SELECT COUNT(ID) FROM [Ts_HostConfig]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_hostconfig", "SELECT COUNT(id) FROM ts_hostconfig"
    match singlevalue_query conn (str__sql sqlMaxTs_HostConfig) with
    | Some v ->
        let max = v :?> int64
        if max > HOSTCONFIG_id.Value then
            HOSTCONFIG_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_HostConfig) with
    | Some v ->
        HOSTCONFIG_count.Value <-
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

    let sqlMaxTs_VarType, sqlCountTs_VarType =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_VarType]", "SELECT COUNT(ID) FROM [Ts_VarType]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_vartype", "SELECT COUNT(id) FROM ts_vartype"
    match singlevalue_query conn (str__sql sqlMaxTs_VarType) with
    | Some v ->
        let max = v :?> int64
        if max > VARTYPE_id.Value then
            VARTYPE_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_VarType) with
    | Some v ->
        VARTYPE_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()
    ()