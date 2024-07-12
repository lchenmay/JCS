module Shared.OrmMor

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
    
    p.Type |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
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
    
    p.Type <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
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
        ("Type",(p.Type |> EnumToValue).ToString() |> Json.Num)
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
    
    p.Type <- checkfield fields "Type" |> parse_int32 |> EnumOfValue
    
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
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        p.Bind <- checkfield fields "Bind" |> parse_int64
        
        p.Type <- checkfield fields "Type" |> parse_int32 |> EnumOfValue
        
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
    
    let binDesc = p.Desc |> Encoding.UTF8.GetBytes
    binDesc.Length |> BitConverter.GetBytes |> bb.append
    binDesc |> bb.append
    
    let binWebsite = p.Website |> Encoding.UTF8.GetBytes
    binWebsite.Length |> BitConverter.GetBytes |> bb.append
    binWebsite |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    p.City |> BitConverter.GetBytes |> bb.append
    
    p.Country |> BitConverter.GetBytes |> bb.append
    
    p.Lang |> BitConverter.GetBytes |> bb.append
    
    p.IsSocial |> BitConverter.GetBytes |> bb.append
    
    p.IsCmsSource |> BitConverter.GetBytes |> bb.append
    
    p.IsPay |> BitConverter.GetBytes |> bb.append
    
    p.MomentLatest |> BitConverter.GetBytes |> bb.append
    
    p.CountFollowers |> BitConverter.GetBytes |> bb.append
    
    p.CountFollows |> BitConverter.GetBytes |> bb.append
    
    p.CountMoments |> BitConverter.GetBytes |> bb.append
    
    p.CountViews |> BitConverter.GetBytes |> bb.append
    
    p.CountComments |> BitConverter.GetBytes |> bb.append
    
    p.CountThumbUps |> BitConverter.GetBytes |> bb.append
    
    p.CountThumbDns |> BitConverter.GetBytes |> bb.append
    
    p.IsCrawling |> BitConverter.GetBytes |> bb.append
    
    p.IsGSeries |> BitConverter.GetBytes |> bb.append
    
    let binRemarksCentral = p.RemarksCentral |> Encoding.UTF8.GetBytes
    binRemarksCentral.Length |> BitConverter.GetBytes |> bb.append
    binRemarksCentral |> bb.append
    
    p.Agent |> BitConverter.GetBytes |> bb.append
    
    let binSiteCats = p.SiteCats |> Encoding.UTF8.GetBytes
    binSiteCats.Length |> BitConverter.GetBytes |> bb.append
    binSiteCats |> bb.append
    
    let binConfiguredCats = p.ConfiguredCats |> Encoding.UTF8.GetBytes
    binConfiguredCats.Length |> BitConverter.GetBytes |> bb.append
    binConfiguredCats |> bb.append

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
    
    let count_Desc = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Desc <- Encoding.UTF8.GetString(bin,index.Value,count_Desc)
    index.Value <- index.Value + count_Desc
    
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
    
    p.IsSocial <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsCmsSource <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsPay <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.MomentLatest <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CountFollowers <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CountFollows <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CountMoments <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CountViews <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CountComments <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CountThumbUps <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.CountThumbDns <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.IsCrawling <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsGSeries <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    let count_RemarksCentral = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.RemarksCentral <- Encoding.UTF8.GetString(bin,index.Value,count_RemarksCentral)
    index.Value <- index.Value + count_RemarksCentral
    
    p.Agent <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_SiteCats = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.SiteCats <- Encoding.UTF8.GetString(bin,index.Value,count_SiteCats)
    index.Value <- index.Value + count_SiteCats
    
    let count_ConfiguredCats = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.ConfiguredCats <- Encoding.UTF8.GetString(bin,index.Value,count_ConfiguredCats)
    index.Value <- index.Value + count_ConfiguredCats
    
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
        ("Desc",p.Desc |> Json.Str)
        ("Website",p.Website |> Json.Str)
        ("Icon",p.Icon |> Json.Str)
        ("City",p.City.ToString() |> Json.Num)
        ("Country",p.Country.ToString() |> Json.Num)
        ("Lang",p.Lang.ToString() |> Json.Num)
        ("IsSocial",if p.IsSocial then Json.True else Json.False)
        ("IsCmsSource",if p.IsCmsSource then Json.True else Json.False)
        ("IsPay",if p.IsPay then Json.True else Json.False)
        ("MomentLatest",p.MomentLatest.ToString() |> Json.Num)
        ("CountFollowers",p.CountFollowers.ToString() |> Json.Num)
        ("CountFollows",p.CountFollows.ToString() |> Json.Num)
        ("CountMoments",p.CountMoments.ToString() |> Json.Num)
        ("CountViews",p.CountViews.ToString() |> Json.Num)
        ("CountComments",p.CountComments.ToString() |> Json.Num)
        ("CountThumbUps",p.CountThumbUps.ToString() |> Json.Num)
        ("CountThumbDns",p.CountThumbDns.ToString() |> Json.Num)
        ("IsCrawling",if p.IsCrawling then Json.True else Json.False)
        ("IsGSeries",if p.IsGSeries then Json.True else Json.False)
        ("RemarksCentral",p.RemarksCentral |> Json.Str)
        ("Agent",p.Agent.ToString() |> Json.Num)
        ("SiteCats",p.SiteCats |> Json.Str)
        ("ConfiguredCats",p.ConfiguredCats |> Json.Str) |]
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
    
    p.Code <- checkfieldz fields "Code" 256
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.Parent <- checkfield fields "Parent" |> parse_int64
    
    p.BasicAcct <- checkfield fields "BasicAcct" |> parse_int64
    
    p.Desc <- checkfield fields "Desc"
    
    p.Website <- checkfieldz fields "Website" 256
    
    p.Icon <- checkfieldz fields "Icon" 256
    
    p.City <- checkfield fields "City" |> parse_int64
    
    p.Country <- checkfield fields "Country" |> parse_int64
    
    p.Lang <- checkfield fields "Lang" |> parse_int64
    
    p.IsSocial <- checkfield fields "IsSocial" = "true"
    
    p.IsCmsSource <- checkfield fields "IsCmsSource" = "true"
    
    p.IsPay <- checkfield fields "IsPay" = "true"
    
    p.MomentLatest <- checkfield fields "MomentLatest" |> parse_int64
    
    p.CountFollowers <- checkfield fields "CountFollowers" |> parse_int64
    
    p.CountFollows <- checkfield fields "CountFollows" |> parse_int64
    
    p.CountMoments <- checkfield fields "CountMoments" |> parse_int64
    
    p.CountViews <- checkfield fields "CountViews" |> parse_int64
    
    p.CountComments <- checkfield fields "CountComments" |> parse_int64
    
    p.CountThumbUps <- checkfield fields "CountThumbUps" |> parse_int64
    
    p.CountThumbDns <- checkfield fields "CountThumbDns" |> parse_int64
    
    p.IsCrawling <- checkfield fields "IsCrawling" = "true"
    
    p.IsGSeries <- checkfield fields "IsGSeries" = "true"
    
    p.RemarksCentral <- checkfield fields "RemarksCentral"
    
    p.Agent <- checkfield fields "Agent" |> parse_int64
    
    p.SiteCats <- checkfield fields "SiteCats"
    
    p.ConfiguredCats <- checkfield fields "ConfiguredCats"
    
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
        
        p.Code <- checkfieldz fields "Code" 256
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        p.Parent <- checkfield fields "Parent" |> parse_int64
        
        p.BasicAcct <- checkfield fields "BasicAcct" |> parse_int64
        
        p.Desc <- checkfield fields "Desc"
        
        p.Website <- checkfieldz fields "Website" 256
        
        p.Icon <- checkfieldz fields "Icon" 256
        
        p.City <- checkfield fields "City" |> parse_int64
        
        p.Country <- checkfield fields "Country" |> parse_int64
        
        p.Lang <- checkfield fields "Lang" |> parse_int64
        
        p.IsSocial <- checkfield fields "IsSocial" = "true"
        
        p.IsCmsSource <- checkfield fields "IsCmsSource" = "true"
        
        p.IsPay <- checkfield fields "IsPay" = "true"
        
        p.MomentLatest <- checkfield fields "MomentLatest" |> parse_int64
        
        p.CountFollowers <- checkfield fields "CountFollowers" |> parse_int64
        
        p.CountFollows <- checkfield fields "CountFollows" |> parse_int64
        
        p.CountMoments <- checkfield fields "CountMoments" |> parse_int64
        
        p.CountViews <- checkfield fields "CountViews" |> parse_int64
        
        p.CountComments <- checkfield fields "CountComments" |> parse_int64
        
        p.CountThumbUps <- checkfield fields "CountThumbUps" |> parse_int64
        
        p.CountThumbDns <- checkfield fields "CountThumbDns" |> parse_int64
        
        p.IsCrawling <- checkfield fields "IsCrawling" = "true"
        
        p.IsGSeries <- checkfield fields "IsGSeries" = "true"
        
        p.RemarksCentral <- checkfield fields "RemarksCentral"
        
        p.Agent <- checkfield fields "Agent" |> parse_int64
        
        p.SiteCats <- checkfield fields "SiteCats"
        
        p.ConfiguredCats <- checkfield fields "ConfiguredCats"
        
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
        
        p.Code2 <- checkfieldz fields "Code2" 2
        
        p.Caption <- checkfieldz fields "Caption" 64
        
        p.Fullname <- checkfieldz fields "Fullname" 256
        
        p.Icon <- checkfieldz fields "Icon" 256
        
        p.Tel <- checkfieldz fields "Tel" 4
        
        p.Cur <- checkfield fields "Cur" |> parse_int64
        
        p.Capital <- checkfield fields "Capital" |> parse_int64
        
        p.Place <- checkfield fields "Place" |> parse_int64
        
        p.Lang <- checkfield fields "Lang" |> parse_int64
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [CUR] Structure

let pCUR__bin (bb:BytesBuilder) (p:pCUR) =

    
    let binCode = p.Code |> Encoding.UTF8.GetBytes
    binCode.Length |> BitConverter.GetBytes |> bb.append
    binCode |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Hidden |> BitConverter.GetBytes |> bb.append
    
    p.IsSac |> BitConverter.GetBytes |> bb.append
    
    p.IsTransfer |> BitConverter.GetBytes |> bb.append
    
    p.IsCash |> BitConverter.GetBytes |> bb.append
    
    p.EnableReward |> BitConverter.GetBytes |> bb.append
    
    p.EnableOTC |> BitConverter.GetBytes |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    p.CurType |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Dec |> BitConverter.GetBytes |> bb.append
    
    p.AnchorRate |> BitConverter.GetBytes |> bb.append
    
    p.Freezable |> BitConverter.GetBytes |> bb.append
    
    p.Authorizable |> BitConverter.GetBytes |> bb.append
    
    let binChaninID = p.ChaninID |> Encoding.UTF8.GetBytes
    binChaninID.Length |> BitConverter.GetBytes |> bb.append
    binChaninID |> bb.append
    
    let binChaninName = p.ChaninName |> Encoding.UTF8.GetBytes
    binChaninName.Length |> BitConverter.GetBytes |> bb.append
    binChaninName |> bb.append
    
    let binContractAddress = p.ContractAddress |> Encoding.UTF8.GetBytes
    binContractAddress.Length |> BitConverter.GetBytes |> bb.append
    binContractAddress |> bb.append
    
    let binWalletAddress = p.WalletAddress |> Encoding.UTF8.GetBytes
    binWalletAddress.Length |> BitConverter.GetBytes |> bb.append
    binWalletAddress |> bb.append
    
    p.BaseRate |> BitConverter.GetBytes |> bb.append

let CUR__bin (bb:BytesBuilder) (v:CUR) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pCUR__bin bb v.p

let bin__pCUR (bi:BinIndexed):pCUR =
    let bin,index = bi

    let p = pCUR_empty()
    
    let count_Code = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Code <- Encoding.UTF8.GetString(bin,index.Value,count_Code)
    index.Value <- index.Value + count_Code
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Hidden <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsSac <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsTransfer <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsCash <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.EnableReward <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.EnableOTC <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    let count_Icon = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Icon <- Encoding.UTF8.GetString(bin,index.Value,count_Icon)
    index.Value <- index.Value + count_Icon
    
    p.CurType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Dec <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.AnchorRate <- BitConverter.ToDouble(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Freezable <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.Authorizable <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    let count_ChaninID = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.ChaninID <- Encoding.UTF8.GetString(bin,index.Value,count_ChaninID)
    index.Value <- index.Value + count_ChaninID
    
    let count_ChaninName = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.ChaninName <- Encoding.UTF8.GetString(bin,index.Value,count_ChaninName)
    index.Value <- index.Value + count_ChaninName
    
    let count_ContractAddress = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.ContractAddress <- Encoding.UTF8.GetString(bin,index.Value,count_ContractAddress)
    index.Value <- index.Value + count_ContractAddress
    
    let count_WalletAddress = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.WalletAddress <- Encoding.UTF8.GetString(bin,index.Value,count_WalletAddress)
    index.Value <- index.Value + count_WalletAddress
    
    p.BaseRate <- BitConverter.ToDouble(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__CUR (bi:BinIndexed):CUR =
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
        p = bin__pCUR bi }

let pCUR__json (p:pCUR) =

    [|
        ("Code",p.Code |> Json.Str)
        ("Caption",p.Caption |> Json.Str)
        ("Hidden",if p.Hidden then Json.True else Json.False)
        ("IsSac",if p.IsSac then Json.True else Json.False)
        ("IsTransfer",if p.IsTransfer then Json.True else Json.False)
        ("IsCash",if p.IsCash then Json.True else Json.False)
        ("EnableReward",if p.EnableReward then Json.True else Json.False)
        ("EnableOTC",if p.EnableOTC then Json.True else Json.False)
        ("Icon",p.Icon |> Json.Str)
        ("CurType",(p.CurType |> EnumToValue).ToString() |> Json.Num)
        ("Dec",p.Dec.ToString() |> Json.Num)
        ("AnchorRate",p.AnchorRate.ToString() |> Json.Num)
        ("Freezable",if p.Freezable then Json.True else Json.False)
        ("Authorizable",if p.Authorizable then Json.True else Json.False)
        ("ChaninID",p.ChaninID |> Json.Str)
        ("ChaninName",p.ChaninName |> Json.Str)
        ("ContractAddress",p.ContractAddress |> Json.Str)
        ("WalletAddress",p.WalletAddress |> Json.Str)
        ("BaseRate",p.BaseRate.ToString() |> Json.Num) |]
    |> Json.Braket

let CUR__json (v:CUR) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pCUR__json v.p) |]
    |> Json.Braket

let CUR__jsonTbw (w:TextBlockWriter) (v:CUR) =
    json__str w (CUR__json v)

let CUR__jsonStr (v:CUR) =
    (CUR__json v) |> json__strFinal


let json__pCURo (json:Json):pCUR option =
    let fields = json |> json__items

    let p = pCUR_empty()
    
    p.Code <- checkfieldz fields "Code" 16
    
    p.Caption <- checkfieldz fields "Caption" 64
    
    p.Hidden <- checkfield fields "Hidden" = "true"
    
    p.IsSac <- checkfield fields "IsSac" = "true"
    
    p.IsTransfer <- checkfield fields "IsTransfer" = "true"
    
    p.IsCash <- checkfield fields "IsCash" = "true"
    
    p.EnableReward <- checkfield fields "EnableReward" = "true"
    
    p.EnableOTC <- checkfield fields "EnableOTC" = "true"
    
    p.Icon <- checkfieldz fields "Icon" 512
    
    p.CurType <- checkfield fields "CurType" |> parse_int32 |> EnumOfValue
    
    p.Dec <- checkfield fields "Dec" |> parse_int64
    
    p.AnchorRate <- checkfield fields "AnchorRate" |> parse_float
    
    p.Freezable <- checkfield fields "Freezable" = "true"
    
    p.Authorizable <- checkfield fields "Authorizable" = "true"
    
    p.ChaninID <- checkfieldz fields "ChaninID" 256
    
    p.ChaninName <- checkfieldz fields "ChaninName" 256
    
    p.ContractAddress <- checkfieldz fields "ContractAddress" 256
    
    p.WalletAddress <- checkfieldz fields "WalletAddress" 256
    
    p.BaseRate <- checkfield fields "BaseRate" |> parse_float
    
    p |> Some
    

let json__CURo (json:Json):CUR option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pCURo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Code <- checkfieldz fields "Code" 16
        
        p.Caption <- checkfieldz fields "Caption" 64
        
        p.Hidden <- checkfield fields "Hidden" = "true"
        
        p.IsSac <- checkfield fields "IsSac" = "true"
        
        p.IsTransfer <- checkfield fields "IsTransfer" = "true"
        
        p.IsCash <- checkfield fields "IsCash" = "true"
        
        p.EnableReward <- checkfield fields "EnableReward" = "true"
        
        p.EnableOTC <- checkfield fields "EnableOTC" = "true"
        
        p.Icon <- checkfieldz fields "Icon" 512
        
        p.CurType <- checkfield fields "CurType" |> parse_int32 |> EnumOfValue
        
        p.Dec <- checkfield fields "Dec" |> parse_int64
        
        p.AnchorRate <- checkfield fields "AnchorRate" |> parse_float
        
        p.Freezable <- checkfield fields "Freezable" = "true"
        
        p.Authorizable <- checkfield fields "Authorizable" = "true"
        
        p.ChaninID <- checkfieldz fields "ChaninID" 256
        
        p.ChaninName <- checkfieldz fields "ChaninName" 256
        
        p.ContractAddress <- checkfieldz fields "ContractAddress" 256
        
        p.WalletAddress <- checkfieldz fields "WalletAddress" 256
        
        p.BaseRate <- checkfield fields "BaseRate" |> parse_float
        
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
    
    p.Refer <- checkfieldz fields "Refer" 7
    
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
        
        p.Refer <- checkfieldz fields "Refer" 7
        
        p.Referer <- checkfield fields "Referer" |> parse_int64
        
        p.Url <- checkfield fields "Url"
        
        p.About <- checkfield fields "About"
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [FILE] Structure

let pFILE__bin (bb:BytesBuilder) (p:pFILE) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Encrypt |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    let binSHA256 = p.SHA256 |> Encoding.UTF8.GetBytes
    binSHA256.Length |> BitConverter.GetBytes |> bb.append
    binSHA256 |> bb.append
    
    p.Size |> BitConverter.GetBytes |> bb.append
    
    p.Bind |> BitConverter.GetBytes |> bb.append
    
    p.BindType |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.State |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Folder |> BitConverter.GetBytes |> bb.append
    
    p.FileType |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    let binJSON = p.JSON |> Encoding.UTF8.GetBytes
    binJSON.Length |> BitConverter.GetBytes |> bb.append
    binJSON |> bb.append

let FILE__bin (bb:BytesBuilder) (v:FILE) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pFILE__bin bb v.p

let bin__pFILE (bi:BinIndexed):pFILE =
    let bin,index = bi

    let p = pFILE_empty()
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Encrypt <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    let count_SHA256 = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.SHA256 <- Encoding.UTF8.GetString(bin,index.Value,count_SHA256)
    index.Value <- index.Value + count_SHA256
    
    p.Size <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Bind <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.BindType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.State <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Folder <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.FileType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    let count_JSON = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.JSON <- Encoding.UTF8.GetString(bin,index.Value,count_JSON)
    index.Value <- index.Value + count_JSON
    
    p

let bin__FILE (bi:BinIndexed):FILE =
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
        p = bin__pFILE bi }

let pFILE__json (p:pFILE) =

    [|
        ("Caption",p.Caption |> Json.Str)
        ("Encrypt",(p.Encrypt |> EnumToValue).ToString() |> Json.Num)
        ("SHA256",p.SHA256 |> Json.Str)
        ("Size",p.Size.ToString() |> Json.Num)
        ("Bind",p.Bind.ToString() |> Json.Num)
        ("BindType",(p.BindType |> EnumToValue).ToString() |> Json.Num)
        ("State",(p.State |> EnumToValue).ToString() |> Json.Num)
        ("Folder",p.Folder.ToString() |> Json.Num)
        ("FileType",(p.FileType |> EnumToValue).ToString() |> Json.Num)
        ("JSON",p.JSON |> Json.Str) |]
    |> Json.Braket

let FILE__json (v:FILE) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pFILE__json v.p) |]
    |> Json.Braket

let FILE__jsonTbw (w:TextBlockWriter) (v:FILE) =
    json__str w (FILE__json v)

let FILE__jsonStr (v:FILE) =
    (FILE__json v) |> json__strFinal


let json__pFILEo (json:Json):pFILE option =
    let fields = json |> json__items

    let p = pFILE_empty()
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.Encrypt <- checkfield fields "Encrypt" |> parse_int32 |> EnumOfValue
    
    p.SHA256 <- checkfield fields "SHA256"
    
    p.Size <- checkfield fields "Size" |> parse_int64
    
    p.Bind <- checkfield fields "Bind" |> parse_int64
    
    p.BindType <- checkfield fields "BindType" |> parse_int32 |> EnumOfValue
    
    p.State <- checkfield fields "State" |> parse_int32 |> EnumOfValue
    
    p.Folder <- checkfield fields "Folder" |> parse_int64
    
    p.FileType <- checkfield fields "FileType" |> parse_int32 |> EnumOfValue
    
    p.JSON <- checkfield fields "JSON"
    
    p |> Some
    

let json__FILEo (json:Json):FILE option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pFILEo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        p.Encrypt <- checkfield fields "Encrypt" |> parse_int32 |> EnumOfValue
        
        p.SHA256 <- checkfield fields "SHA256"
        
        p.Size <- checkfield fields "Size" |> parse_int64
        
        p.Bind <- checkfield fields "Bind" |> parse_int64
        
        p.BindType <- checkfield fields "BindType" |> parse_int32 |> EnumOfValue
        
        p.State <- checkfield fields "State" |> parse_int32 |> EnumOfValue
        
        p.Folder <- checkfield fields "Folder" |> parse_int64
        
        p.FileType <- checkfield fields "FileType" |> parse_int32 |> EnumOfValue
        
        p.JSON <- checkfield fields "JSON"
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [FOLDER] Structure

let pFOLDER__bin (bb:BytesBuilder) (p:pFOLDER) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.Encrypt |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Bind |> BitConverter.GetBytes |> bb.append
    
    p.BindType |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.Parent |> BitConverter.GetBytes |> bb.append

let FOLDER__bin (bb:BytesBuilder) (v:FOLDER) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pFOLDER__bin bb v.p

let bin__pFOLDER (bi:BinIndexed):pFOLDER =
    let bin,index = bi

    let p = pFOLDER_empty()
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p.Encrypt <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Bind <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.BindType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.Parent <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p

let bin__FOLDER (bi:BinIndexed):FOLDER =
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
        p = bin__pFOLDER bi }

let pFOLDER__json (p:pFOLDER) =

    [|
        ("Caption",p.Caption |> Json.Str)
        ("Encrypt",(p.Encrypt |> EnumToValue).ToString() |> Json.Num)
        ("Bind",p.Bind.ToString() |> Json.Num)
        ("BindType",(p.BindType |> EnumToValue).ToString() |> Json.Num)
        ("Parent",p.Parent.ToString() |> Json.Num) |]
    |> Json.Braket

let FOLDER__json (v:FOLDER) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pFOLDER__json v.p) |]
    |> Json.Braket

let FOLDER__jsonTbw (w:TextBlockWriter) (v:FOLDER) =
    json__str w (FOLDER__json v)

let FOLDER__jsonStr (v:FOLDER) =
    (FOLDER__json v) |> json__strFinal


let json__pFOLDERo (json:Json):pFOLDER option =
    let fields = json |> json__items

    let p = pFOLDER_empty()
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p.Encrypt <- checkfield fields "Encrypt" |> parse_int32 |> EnumOfValue
    
    p.Bind <- checkfield fields "Bind" |> parse_int64
    
    p.BindType <- checkfield fields "BindType" |> parse_int32 |> EnumOfValue
    
    p.Parent <- checkfield fields "Parent" |> parse_int64
    
    p |> Some
    

let json__FOLDERo (json:Json):FOLDER option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pFOLDERo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Caption <- checkfieldz fields "Caption" 256
        
        p.Encrypt <- checkfield fields "Encrypt" |> parse_int32 |> EnumOfValue
        
        p.Bind <- checkfield fields "Bind" |> parse_int64
        
        p.BindType <- checkfield fields "BindType" |> parse_int32 |> EnumOfValue
        
        p.Parent <- checkfield fields "Parent" |> parse_int64
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

// [LANG] Structure

let pLANG__bin (bb:BytesBuilder) (p:pLANG) =

    
    let binCode2 = p.Code2 |> Encoding.UTF8.GetBytes
    binCode2.Length |> BitConverter.GetBytes |> bb.append
    binCode2 |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    let binNative = p.Native |> Encoding.UTF8.GetBytes
    binNative.Length |> BitConverter.GetBytes |> bb.append
    binNative |> bb.append
    
    let binIcon = p.Icon |> Encoding.UTF8.GetBytes
    binIcon.Length |> BitConverter.GetBytes |> bb.append
    binIcon |> bb.append
    
    p.IsBlank |> BitConverter.GetBytes |> bb.append
    
    p.IsLocale |> BitConverter.GetBytes |> bb.append
    
    p.IsContent |> BitConverter.GetBytes |> bb.append
    
    p.IsAutoTranslate |> BitConverter.GetBytes |> bb.append
    
    p.TextDirection |> EnumToValue |> BitConverter.GetBytes |> bb.append

let LANG__bin (bb:BytesBuilder) (v:LANG) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pLANG__bin bb v.p

let bin__pLANG (bi:BinIndexed):pLANG =
    let bin,index = bi

    let p = pLANG_empty()
    
    let count_Code2 = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Code2 <- Encoding.UTF8.GetString(bin,index.Value,count_Code2)
    index.Value <- index.Value + count_Code2
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    let count_Native = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Native <- Encoding.UTF8.GetString(bin,index.Value,count_Native)
    index.Value <- index.Value + count_Native
    
    let count_Icon = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Icon <- Encoding.UTF8.GetString(bin,index.Value,count_Icon)
    index.Value <- index.Value + count_Icon
    
    p.IsBlank <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsLocale <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsContent <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.IsAutoTranslate <- BitConverter.ToBoolean(bin,index.Value)
    index.Value <- index.Value + 1
    
    p.TextDirection <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p

let bin__LANG (bi:BinIndexed):LANG =
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
        p = bin__pLANG bi }

let pLANG__json (p:pLANG) =

    [|
        ("Code2",p.Code2 |> Json.Str)
        ("Caption",p.Caption |> Json.Str)
        ("Native",p.Native |> Json.Str)
        ("Icon",p.Icon |> Json.Str)
        ("IsBlank",if p.IsBlank then Json.True else Json.False)
        ("IsLocale",if p.IsLocale then Json.True else Json.False)
        ("IsContent",if p.IsContent then Json.True else Json.False)
        ("IsAutoTranslate",if p.IsAutoTranslate then Json.True else Json.False)
        ("TextDirection",(p.TextDirection |> EnumToValue).ToString() |> Json.Num) |]
    |> Json.Braket

let LANG__json (v:LANG) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pLANG__json v.p) |]
    |> Json.Braket

let LANG__jsonTbw (w:TextBlockWriter) (v:LANG) =
    json__str w (LANG__json v)

let LANG__jsonStr (v:LANG) =
    (LANG__json v) |> json__strFinal


let json__pLANGo (json:Json):pLANG option =
    let fields = json |> json__items

    let p = pLANG_empty()
    
    p.Code2 <- checkfieldz fields "Code2" 2
    
    p.Caption <- checkfieldz fields "Caption" 64
    
    p.Native <- checkfieldz fields "Native" 64
    
    p.Icon <- checkfieldz fields "Icon" 256
    
    p.IsBlank <- checkfield fields "IsBlank" = "true"
    
    p.IsLocale <- checkfield fields "IsLocale" = "true"
    
    p.IsContent <- checkfield fields "IsContent" = "true"
    
    p.IsAutoTranslate <- checkfield fields "IsAutoTranslate" = "true"
    
    p.TextDirection <- checkfield fields "TextDirection" |> parse_int32 |> EnumOfValue
    
    p |> Some
    

let json__LANGo (json:Json):LANG option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pLANGo v
        | None -> None
    
    match o with
    | Some p ->
        
        p.Code2 <- checkfieldz fields "Code2" 2
        
        p.Caption <- checkfieldz fields "Caption" 64
        
        p.Native <- checkfieldz fields "Native" 64
        
        p.Icon <- checkfieldz fields "Icon" 256
        
        p.IsBlank <- checkfield fields "IsBlank" = "true"
        
        p.IsLocale <- checkfield fields "IsLocale" = "true"
        
        p.IsContent <- checkfield fields "IsContent" = "true"
        
        p.IsAutoTranslate <- checkfield fields "IsAutoTranslate" = "true"
        
        p.TextDirection <- checkfield fields "TextDirection" |> parse_int32 |> EnumOfValue
        
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
        
        p.Caption <- checkfieldz fields "Caption" 64
        
        p.ExternalId <- checkfield fields "ExternalId" |> parse_int64
        
        p.Icon <- checkfieldz fields "Icon" 256
        
        p.EU <- checkfield fields "EU" |> parse_int64
        
        p.Biz <- checkfield fields "Biz" |> parse_int64
        
        p.Json <- checkfield fields "Json"
        
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
    p.Type <- EnumOfValue(if Convert.IsDBNull(line.[6]) then 0 else line.[6] :?> int)
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
    [|
        ("Caption", p.Caption) |> kvp__sqlparam
        ("Bind", p.Bind) |> kvp__sqlparam
        ("Type", p.Type) |> kvp__sqlparam
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

let db__ADDRESS = db__Rcd db__pADDRESS

let ADDRESS_wrapper item: ADDRESS =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pADDRESS_clone (p:pADDRESS): pADDRESS = {
    Caption = p.Caption
    Bind = p.Bind
    Type = p.Type
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
        |> update (conn,output,ADDRESS_table,ADDRESS_sql_update,pADDRESS__sps,suc,fail)
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
    

let id__ADDRESSo id: ADDRESS option = id__rcd(conn,ADDRESS_fieldorders,ADDRESS_table,db__ADDRESS) id

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
    ,[Type]
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
    p.Desc <- string(line.[8]).TrimEnd()
    p.Website <- string(line.[9]).TrimEnd()
    p.Icon <- string(line.[10]).TrimEnd()
    p.City <- if Convert.IsDBNull(line.[11]) then 0L else line.[11] :?> int64
    p.Country <- if Convert.IsDBNull(line.[12]) then 0L else line.[12] :?> int64
    p.Lang <- if Convert.IsDBNull(line.[13]) then 0L else line.[13] :?> int64
    p.IsSocial <- if Convert.IsDBNull(line.[14]) then false else line.[14] :?> bool
    p.IsCmsSource <- if Convert.IsDBNull(line.[15]) then false else line.[15] :?> bool
    p.IsPay <- if Convert.IsDBNull(line.[16]) then false else line.[16] :?> bool
    p.MomentLatest <- if Convert.IsDBNull(line.[17]) then 0L else line.[17] :?> int64
    p.CountFollowers <- if Convert.IsDBNull(line.[18]) then 0L else line.[18] :?> int64
    p.CountFollows <- if Convert.IsDBNull(line.[19]) then 0L else line.[19] :?> int64
    p.CountMoments <- if Convert.IsDBNull(line.[20]) then 0L else line.[20] :?> int64
    p.CountViews <- if Convert.IsDBNull(line.[21]) then 0L else line.[21] :?> int64
    p.CountComments <- if Convert.IsDBNull(line.[22]) then 0L else line.[22] :?> int64
    p.CountThumbUps <- if Convert.IsDBNull(line.[23]) then 0L else line.[23] :?> int64
    p.CountThumbDns <- if Convert.IsDBNull(line.[24]) then 0L else line.[24] :?> int64
    p.IsCrawling <- if Convert.IsDBNull(line.[25]) then false else line.[25] :?> bool
    p.IsGSeries <- if Convert.IsDBNull(line.[26]) then false else line.[26] :?> bool
    p.RemarksCentral <- string(line.[27]).TrimEnd()
    p.Agent <- if Convert.IsDBNull(line.[28]) then 0L else line.[28] :?> int64
    p.SiteCats <- string(line.[29]).TrimEnd()
    p.ConfiguredCats <- string(line.[30]).TrimEnd()

    p

let pBIZ__sps (p:pBIZ) =
    [|
        ("Code", p.Code) |> kvp__sqlparam
        ("Caption", p.Caption) |> kvp__sqlparam
        ("Parent", p.Parent) |> kvp__sqlparam
        ("BasicAcct", p.BasicAcct) |> kvp__sqlparam
        ("Desc", p.Desc) |> kvp__sqlparam
        ("Website", p.Website) |> kvp__sqlparam
        ("Icon", p.Icon) |> kvp__sqlparam
        ("City", p.City) |> kvp__sqlparam
        ("Country", p.Country) |> kvp__sqlparam
        ("Lang", p.Lang) |> kvp__sqlparam
        ("IsSocial", p.IsSocial) |> kvp__sqlparam
        ("IsCmsSource", p.IsCmsSource) |> kvp__sqlparam
        ("IsPay", p.IsPay) |> kvp__sqlparam
        ("MomentLatest", p.MomentLatest) |> kvp__sqlparam
        ("CountFollowers", p.CountFollowers) |> kvp__sqlparam
        ("CountFollows", p.CountFollows) |> kvp__sqlparam
        ("CountMoments", p.CountMoments) |> kvp__sqlparam
        ("CountViews", p.CountViews) |> kvp__sqlparam
        ("CountComments", p.CountComments) |> kvp__sqlparam
        ("CountThumbUps", p.CountThumbUps) |> kvp__sqlparam
        ("CountThumbDns", p.CountThumbDns) |> kvp__sqlparam
        ("IsCrawling", p.IsCrawling) |> kvp__sqlparam
        ("IsGSeries", p.IsGSeries) |> kvp__sqlparam
        ("RemarksCentral", p.RemarksCentral) |> kvp__sqlparam
        ("Agent", p.Agent) |> kvp__sqlparam
        ("SiteCats", p.SiteCats) |> kvp__sqlparam
        ("ConfiguredCats", p.ConfiguredCats) |> kvp__sqlparam |]

let db__BIZ = db__Rcd db__pBIZ

let BIZ_wrapper item: BIZ =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pBIZ_clone (p:pBIZ): pBIZ = {
    Code = p.Code
    Caption = p.Caption
    Parent = p.Parent
    BasicAcct = p.BasicAcct
    Desc = p.Desc
    Website = p.Website
    Icon = p.Icon
    City = p.City
    Country = p.Country
    Lang = p.Lang
    IsSocial = p.IsSocial
    IsCmsSource = p.IsCmsSource
    IsPay = p.IsPay
    MomentLatest = p.MomentLatest
    CountFollowers = p.CountFollowers
    CountFollows = p.CountFollows
    CountMoments = p.CountMoments
    CountViews = p.CountViews
    CountComments = p.CountComments
    CountThumbUps = p.CountThumbUps
    CountThumbDns = p.CountThumbDns
    IsCrawling = p.IsCrawling
    IsGSeries = p.IsGSeries
    RemarksCentral = p.RemarksCentral
    Agent = p.Agent
    SiteCats = p.SiteCats
    ConfiguredCats = p.ConfiguredCats }

let BIZ_update_transaction output (updater,suc,fail) (rcd:BIZ) =
    let rollback_p = rcd.p |> pBIZ_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,BIZ_table,BIZ_sql_update,pBIZ__sps,suc,fail)
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
    

let id__BIZo id: BIZ option = id__rcd(conn,BIZ_fieldorders,BIZ_table,db__BIZ) id

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
    ,[Desc]
    ,[Website]
    ,[Icon]
    ,[City]
    ,[Country]
    ,[Lang]
    ,[IsSocial]
    ,[IsCmsSource]
    ,[IsPay]
    ,[MomentLatest]
    ,[CountFollowers]
    ,[CountFollows]
    ,[CountMoments]
    ,[CountViews]
    ,[CountComments]
    ,[CountThumbUps]
    ,[CountThumbDns]
    ,[IsCrawling]
    ,[IsGSeries]
    ,[RemarksCentral]
    ,[Agent]
    ,[SiteCats]
    ,[ConfiguredCats])
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
        |> update (conn,output,CRY_table,CRY_sql_update,pCRY__sps,suc,fail)
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
    

let id__CRYo id: CRY option = id__rcd(conn,CRY_fieldorders,CRY_table,db__CRY) id

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


let db__pCUR(line:Object[]): pCUR =
    let p = pCUR_empty()

    p.Code <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.Hidden <- if Convert.IsDBNull(line.[6]) then false else line.[6] :?> bool
    p.IsSac <- if Convert.IsDBNull(line.[7]) then false else line.[7] :?> bool
    p.IsTransfer <- if Convert.IsDBNull(line.[8]) then false else line.[8] :?> bool
    p.IsCash <- if Convert.IsDBNull(line.[9]) then false else line.[9] :?> bool
    p.EnableReward <- if Convert.IsDBNull(line.[10]) then false else line.[10] :?> bool
    p.EnableOTC <- if Convert.IsDBNull(line.[11]) then false else line.[11] :?> bool
    p.Icon <- string(line.[12]).TrimEnd()
    p.CurType <- EnumOfValue(if Convert.IsDBNull(line.[13]) then 0 else line.[13] :?> int)
    p.Dec <- if Convert.IsDBNull(line.[14]) then 0L else line.[14] :?> int64
    p.AnchorRate <- if Convert.IsDBNull(line.[15]) then 0.0 else line.[15] :?> float
    p.Freezable <- if Convert.IsDBNull(line.[16]) then false else line.[16] :?> bool
    p.Authorizable <- if Convert.IsDBNull(line.[17]) then false else line.[17] :?> bool
    p.ChaninID <- string(line.[18]).TrimEnd()
    p.ChaninName <- string(line.[19]).TrimEnd()
    p.ContractAddress <- string(line.[20]).TrimEnd()
    p.WalletAddress <- string(line.[21]).TrimEnd()
    p.BaseRate <- if Convert.IsDBNull(line.[22]) then 0.0 else line.[22] :?> float

    p

let pCUR__sps (p:pCUR) =
    [|
        ("Code", p.Code) |> kvp__sqlparam
        ("Caption", p.Caption) |> kvp__sqlparam
        ("Hidden", p.Hidden) |> kvp__sqlparam
        ("IsSac", p.IsSac) |> kvp__sqlparam
        ("IsTransfer", p.IsTransfer) |> kvp__sqlparam
        ("IsCash", p.IsCash) |> kvp__sqlparam
        ("EnableReward", p.EnableReward) |> kvp__sqlparam
        ("EnableOTC", p.EnableOTC) |> kvp__sqlparam
        ("Icon", p.Icon) |> kvp__sqlparam
        ("CurType", p.CurType) |> kvp__sqlparam
        ("Dec", p.Dec) |> kvp__sqlparam
        ("AnchorRate", p.AnchorRate) |> kvp__sqlparam
        ("Freezable", p.Freezable) |> kvp__sqlparam
        ("Authorizable", p.Authorizable) |> kvp__sqlparam
        ("ChaninID", p.ChaninID) |> kvp__sqlparam
        ("ChaninName", p.ChaninName) |> kvp__sqlparam
        ("ContractAddress", p.ContractAddress) |> kvp__sqlparam
        ("WalletAddress", p.WalletAddress) |> kvp__sqlparam
        ("BaseRate", p.BaseRate) |> kvp__sqlparam |]

let db__CUR = db__Rcd db__pCUR

let CUR_wrapper item: CUR =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pCUR_clone (p:pCUR): pCUR = {
    Code = p.Code
    Caption = p.Caption
    Hidden = p.Hidden
    IsSac = p.IsSac
    IsTransfer = p.IsTransfer
    IsCash = p.IsCash
    EnableReward = p.EnableReward
    EnableOTC = p.EnableOTC
    Icon = p.Icon
    CurType = p.CurType
    Dec = p.Dec
    AnchorRate = p.AnchorRate
    Freezable = p.Freezable
    Authorizable = p.Authorizable
    ChaninID = p.ChaninID
    ChaninName = p.ChaninName
    ContractAddress = p.ContractAddress
    WalletAddress = p.WalletAddress
    BaseRate = p.BaseRate }

let CUR_update_transaction output (updater,suc,fail) (rcd:CUR) =
    let rollback_p = rcd.p |> pCUR_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,CUR_table,CUR_sql_update,pCUR__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let CUR_update output (rcd:CUR) =
    rcd
    |> CUR_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let CUR_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment CUR_id
    let ctime = DateTime.UtcNow
    match create (conn,output,CUR_table,pCUR__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> CUR_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let CUR_create output p =
    CUR_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__CURo id: CUR option = id__rcd(conn,CUR_fieldorders,CUR_table,db__CUR) id

let CUR_metadata = {
    fieldorders = CUR_fieldorders
    db__rcd = db__CUR 
    wrapper = CUR_wrapper
    sps = pCUR__sps
    id = CUR_id
    id__rcdo = id__CURo
    clone = pCUR_clone
    empty__p = pCUR_empty
    rcd__bin = CUR__bin
    bin__rcd = bin__CUR
    sql_update = CUR_sql_update
    rcd_update = CUR_update
    table = CUR_table
    shorthand = "cur" }

let CURTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Cur' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Cur ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Code]
    ,[Caption]
    ,[Hidden]
    ,[IsSac]
    ,[IsTransfer]
    ,[IsCash]
    ,[EnableReward]
    ,[EnableOTC]
    ,[Icon]
    ,[CurType]
    ,[Dec]
    ,[AnchorRate]
    ,[Freezable]
    ,[Authorizable]
    ,[ChaninID]
    ,[ChaninName]
    ,[ContractAddress]
    ,[WalletAddress]
    ,[BaseRate])
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
    p.Refer <- string(line.[22]).TrimEnd()
    p.Referer <- if Convert.IsDBNull(line.[23]) then 0L else line.[23] :?> int64
    p.Url <- string(line.[24]).TrimEnd()
    p.About <- string(line.[25]).TrimEnd()

    p

let pEU__sps (p:pEU) =
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
        ("Refer", p.Refer) |> kvp__sqlparam
        ("Referer", p.Referer) |> kvp__sqlparam
        ("Url", p.Url) |> kvp__sqlparam
        ("About", p.About) |> kvp__sqlparam |]

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
        |> update (conn,output,EU_table,EU_sql_update,pEU__sps,suc,fail)
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
    

let id__EUo id: EU option = id__rcd(conn,EU_fieldorders,EU_table,db__EU) id

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
    ,[Refer]
    ,[Referer]
    ,[Url]
    ,[About])
    END
    """


let db__pFILE(line:Object[]): pFILE =
    let p = pFILE_empty()

    p.Caption <- string(line.[4]).TrimEnd()
    p.Encrypt <- EnumOfValue(if Convert.IsDBNull(line.[5]) then 0 else line.[5] :?> int)
    p.SHA256 <- string(line.[6]).TrimEnd()
    p.Size <- if Convert.IsDBNull(line.[7]) then 0L else line.[7] :?> int64
    p.Bind <- if Convert.IsDBNull(line.[8]) then 0L else line.[8] :?> int64
    p.BindType <- EnumOfValue(if Convert.IsDBNull(line.[9]) then 0 else line.[9] :?> int)
    p.State <- EnumOfValue(if Convert.IsDBNull(line.[10]) then 0 else line.[10] :?> int)
    p.Folder <- if Convert.IsDBNull(line.[11]) then 0L else line.[11] :?> int64
    p.FileType <- EnumOfValue(if Convert.IsDBNull(line.[12]) then 0 else line.[12] :?> int)
    p.JSON <- string(line.[13]).TrimEnd()

    p

let pFILE__sps (p:pFILE) =
    [|
        ("Caption", p.Caption) |> kvp__sqlparam
        ("Encrypt", p.Encrypt) |> kvp__sqlparam
        ("SHA256", p.SHA256) |> kvp__sqlparam
        ("Size", p.Size) |> kvp__sqlparam
        ("Bind", p.Bind) |> kvp__sqlparam
        ("BindType", p.BindType) |> kvp__sqlparam
        ("State", p.State) |> kvp__sqlparam
        ("Folder", p.Folder) |> kvp__sqlparam
        ("FileType", p.FileType) |> kvp__sqlparam
        ("JSON", p.JSON) |> kvp__sqlparam |]

let db__FILE = db__Rcd db__pFILE

let FILE_wrapper item: FILE =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pFILE_clone (p:pFILE): pFILE = {
    Caption = p.Caption
    Encrypt = p.Encrypt
    SHA256 = p.SHA256
    Size = p.Size
    Bind = p.Bind
    BindType = p.BindType
    State = p.State
    Folder = p.Folder
    FileType = p.FileType
    JSON = p.JSON }

let FILE_update_transaction output (updater,suc,fail) (rcd:FILE) =
    let rollback_p = rcd.p |> pFILE_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,FILE_table,FILE_sql_update,pFILE__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let FILE_update output (rcd:FILE) =
    rcd
    |> FILE_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let FILE_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment FILE_id
    let ctime = DateTime.UtcNow
    match create (conn,output,FILE_table,pFILE__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> FILE_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let FILE_create output p =
    FILE_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__FILEo id: FILE option = id__rcd(conn,FILE_fieldorders,FILE_table,db__FILE) id

let FILE_metadata = {
    fieldorders = FILE_fieldorders
    db__rcd = db__FILE 
    wrapper = FILE_wrapper
    sps = pFILE__sps
    id = FILE_id
    id__rcdo = id__FILEo
    clone = pFILE_clone
    empty__p = pFILE_empty
    rcd__bin = FILE__bin
    bin__rcd = bin__FILE
    sql_update = FILE_sql_update
    rcd_update = FILE_update
    table = FILE_table
    shorthand = "file" }

let FILETxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_File' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_File ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Caption]
    ,[Encrypt]
    ,[SHA256]
    ,[Size]
    ,[Bind]
    ,[BindType]
    ,[State]
    ,[Folder]
    ,[FileType]
    ,[JSON])
    END
    """


let db__pFOLDER(line:Object[]): pFOLDER =
    let p = pFOLDER_empty()

    p.Caption <- string(line.[4]).TrimEnd()
    p.Encrypt <- EnumOfValue(if Convert.IsDBNull(line.[5]) then 0 else line.[5] :?> int)
    p.Bind <- if Convert.IsDBNull(line.[6]) then 0L else line.[6] :?> int64
    p.BindType <- EnumOfValue(if Convert.IsDBNull(line.[7]) then 0 else line.[7] :?> int)
    p.Parent <- if Convert.IsDBNull(line.[8]) then 0L else line.[8] :?> int64

    p

let pFOLDER__sps (p:pFOLDER) =
    [|
        ("Caption", p.Caption) |> kvp__sqlparam
        ("Encrypt", p.Encrypt) |> kvp__sqlparam
        ("Bind", p.Bind) |> kvp__sqlparam
        ("BindType", p.BindType) |> kvp__sqlparam
        ("Parent", p.Parent) |> kvp__sqlparam |]

let db__FOLDER = db__Rcd db__pFOLDER

let FOLDER_wrapper item: FOLDER =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pFOLDER_clone (p:pFOLDER): pFOLDER = {
    Caption = p.Caption
    Encrypt = p.Encrypt
    Bind = p.Bind
    BindType = p.BindType
    Parent = p.Parent }

let FOLDER_update_transaction output (updater,suc,fail) (rcd:FOLDER) =
    let rollback_p = rcd.p |> pFOLDER_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,FOLDER_table,FOLDER_sql_update,pFOLDER__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let FOLDER_update output (rcd:FOLDER) =
    rcd
    |> FOLDER_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let FOLDER_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment FOLDER_id
    let ctime = DateTime.UtcNow
    match create (conn,output,FOLDER_table,pFOLDER__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> FOLDER_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let FOLDER_create output p =
    FOLDER_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__FOLDERo id: FOLDER option = id__rcd(conn,FOLDER_fieldorders,FOLDER_table,db__FOLDER) id

let FOLDER_metadata = {
    fieldorders = FOLDER_fieldorders
    db__rcd = db__FOLDER 
    wrapper = FOLDER_wrapper
    sps = pFOLDER__sps
    id = FOLDER_id
    id__rcdo = id__FOLDERo
    clone = pFOLDER_clone
    empty__p = pFOLDER_empty
    rcd__bin = FOLDER__bin
    bin__rcd = bin__FOLDER
    sql_update = FOLDER_sql_update
    rcd_update = FOLDER_update
    table = FOLDER_table
    shorthand = "folder" }

let FOLDERTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Folder' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Folder ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Caption]
    ,[Encrypt]
    ,[Bind]
    ,[BindType]
    ,[Parent])
    END
    """


let db__pLANG(line:Object[]): pLANG =
    let p = pLANG_empty()

    p.Code2 <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()
    p.Native <- string(line.[6]).TrimEnd()
    p.Icon <- string(line.[7]).TrimEnd()
    p.IsBlank <- if Convert.IsDBNull(line.[8]) then false else line.[8] :?> bool
    p.IsLocale <- if Convert.IsDBNull(line.[9]) then false else line.[9] :?> bool
    p.IsContent <- if Convert.IsDBNull(line.[10]) then false else line.[10] :?> bool
    p.IsAutoTranslate <- if Convert.IsDBNull(line.[11]) then false else line.[11] :?> bool
    p.TextDirection <- EnumOfValue(if Convert.IsDBNull(line.[12]) then 0 else line.[12] :?> int)

    p

let pLANG__sps (p:pLANG) =
    [|
        ("Code2", p.Code2) |> kvp__sqlparam
        ("Caption", p.Caption) |> kvp__sqlparam
        ("Native", p.Native) |> kvp__sqlparam
        ("Icon", p.Icon) |> kvp__sqlparam
        ("IsBlank", p.IsBlank) |> kvp__sqlparam
        ("IsLocale", p.IsLocale) |> kvp__sqlparam
        ("IsContent", p.IsContent) |> kvp__sqlparam
        ("IsAutoTranslate", p.IsAutoTranslate) |> kvp__sqlparam
        ("TextDirection", p.TextDirection) |> kvp__sqlparam |]

let db__LANG = db__Rcd db__pLANG

let LANG_wrapper item: LANG =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pLANG_clone (p:pLANG): pLANG = {
    Code2 = p.Code2
    Caption = p.Caption
    Native = p.Native
    Icon = p.Icon
    IsBlank = p.IsBlank
    IsLocale = p.IsLocale
    IsContent = p.IsContent
    IsAutoTranslate = p.IsAutoTranslate
    TextDirection = p.TextDirection }

let LANG_update_transaction output (updater,suc,fail) (rcd:LANG) =
    let rollback_p = rcd.p |> pLANG_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,LANG_table,LANG_sql_update,pLANG__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let LANG_update output (rcd:LANG) =
    rcd
    |> LANG_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let LANG_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment LANG_id
    let ctime = DateTime.UtcNow
    match create (conn,output,LANG_table,pLANG__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> LANG_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let LANG_create output p =
    LANG_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__LANGo id: LANG option = id__rcd(conn,LANG_fieldorders,LANG_table,db__LANG) id

let LANG_metadata = {
    fieldorders = LANG_fieldorders
    db__rcd = db__LANG 
    wrapper = LANG_wrapper
    sps = pLANG__sps
    id = LANG_id
    id__rcdo = id__LANGo
    clone = pLANG_clone
    empty__p = pLANG_empty
    rcd__bin = LANG__bin
    bin__rcd = bin__LANG
    sql_update = LANG_sql_update
    rcd_update = LANG_update
    table = LANG_table
    shorthand = "lang" }

let LANGTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Lang' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Lang ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Code2]
    ,[Caption]
    ,[Native]
    ,[Icon]
    ,[IsBlank]
    ,[IsLocale]
    ,[IsContent]
    ,[IsAutoTranslate]
    ,[TextDirection])
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
    [|
        ("Caption", p.Caption) |> kvp__sqlparam
        ("ExternalId", p.ExternalId) |> kvp__sqlparam
        ("Icon", p.Icon) |> kvp__sqlparam
        ("EU", p.EU) |> kvp__sqlparam
        ("Biz", p.Biz) |> kvp__sqlparam
        ("Json", p.Json) |> kvp__sqlparam |]

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
        |> update (conn,output,CWC_table,CWC_sql_update,pCWC__sps,suc,fail)
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
    

let id__CWCo id: CWC option = id__rcd(conn,CWC_fieldorders,CWC_table,db__CWC) id

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


type MetadataEnum = 
| ADDRESS = 0
| BIZ = 1
| CRY = 2
| CUR = 3
| EU = 4
| FILE = 5
| FOLDER = 6
| LANG = 7
| CWC = 8

let tablenames = [|
    ADDRESS_metadata.table
    BIZ_metadata.table
    CRY_metadata.table
    CUR_metadata.table
    EU_metadata.table
    FILE_metadata.table
    FOLDER_metadata.table
    LANG_metadata.table
    CWC_metadata.table |]

let init() =

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_Address]") with
    | Some v ->
        let max = v :?> int64
        if max > ADDRESS_id.Value then
            ADDRESS_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_Address]") with
    | Some v -> ADDRESS_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_Biz]") with
    | Some v ->
        let max = v :?> int64
        if max > BIZ_id.Value then
            BIZ_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_Biz]") with
    | Some v -> BIZ_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_Country]") with
    | Some v ->
        let max = v :?> int64
        if max > CRY_id.Value then
            CRY_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_Country]") with
    | Some v -> CRY_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_Cur]") with
    | Some v ->
        let max = v :?> int64
        if max > CUR_id.Value then
            CUR_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_Cur]") with
    | Some v -> CUR_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_EndUser]") with
    | Some v ->
        let max = v :?> int64
        if max > EU_id.Value then
            EU_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_EndUser]") with
    | Some v -> EU_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_File]") with
    | Some v ->
        let max = v :?> int64
        if max > FILE_id.Value then
            FILE_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_File]") with
    | Some v -> FILE_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_Folder]") with
    | Some v ->
        let max = v :?> int64
        if max > FOLDER_id.Value then
            FOLDER_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_Folder]") with
    | Some v -> FOLDER_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_Lang]") with
    | Some v ->
        let max = v :?> int64
        if max > LANG_id.Value then
            LANG_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_Lang]") with
    | Some v -> LANG_count.Value <- v :?> int32
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT MAX(ID) FROM [Ca_WebCredential]") with
    | Some v ->
        let max = v :?> int64
        if max > CWC_id.Value then
            CWC_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql "SELECT COUNT(ID) FROM [Ca_WebCredential]") with
    | Some v -> CWC_count.Value <- v :?> int32
    | None -> ()
    ()