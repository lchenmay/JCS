module JCS.Shared.OrmMor

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
open JCS.Shared.OrmTypes
open JCS.Shared.Types

// [BOOK] Structure


let pBOOK__bin (bb:BytesBuilder) (p:pBOOK) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    let binEmail = p.Email |> Encoding.UTF8.GetBytes
    binEmail.Length |> BitConverter.GetBytes |> bb.append
    binEmail |> bb.append
    
    let binMessage = p.Message |> Encoding.UTF8.GetBytes
    binMessage.Length |> BitConverter.GetBytes |> bb.append
    binMessage |> bb.append

let BOOK__bin (bb:BytesBuilder) (v:BOOK) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pBOOK__bin bb v.p

let bin__pBOOK (bi:BinIndexed):pBOOK =
    let bin,index = bi

    let p = pBOOK_empty()
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    let count_Email = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Email <- Encoding.UTF8.GetString(bin,index.Value,count_Email)
    index.Value <- index.Value + count_Email
    
    let count_Message = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Message <- Encoding.UTF8.GetString(bin,index.Value,count_Message)
    index.Value <- index.Value + count_Message
    
    p

let bin__BOOK (bi:BinIndexed):BOOK =
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
        p = bin__pBOOK bi }

let pBOOK__json (p:pBOOK) =

    [|
        ("Caption",p.Caption |> Json.Str)
        ("Email",p.Email |> Json.Str)
        ("Message",p.Message |> Json.Str) |]
    |> Json.Braket

let BOOK__json (v:BOOK) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pBOOK__json v.p) |]
    |> Json.Braket

let BOOK__jsonTbw (w:TextBlockWriter) (v:BOOK) =
    json__str w (BOOK__json v)

let BOOK__jsonStr (v:BOOK) =
    (BOOK__json v) |> json__strFinal


let json__pBOOKo (json:Json):pBOOK option =
    let fields = json |> json__items

    let p = pBOOK_empty()
    
    p.Caption <- checkfieldz fields "Caption" 64
    
    p.Email <- checkfieldz fields "Email" 64
    
    p.Message <- checkfield fields "Message"
    
    p |> Some
    

let json__BOOKo (json:Json):BOOK option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pBOOKo v
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

let BOOK_clone src =
    let bb = new BytesBuilder()
    BOOK__bin bb src
    bin__BOOK (bb.bytes(),ref 0)

// [EU] Structure


let pEU__bin (bb:BytesBuilder) (p:pEU) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    p.AuthType |> EnumToValue |> BitConverter.GetBytes |> bb.append

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
    
    p.AuthType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
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
        ("AuthType",(p.AuthType |> EnumToValue).ToString() |> Json.Num) |]
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
    
    p.AuthType <- checkfield fields "AuthType" |> parse_int32 |> EnumOfValue
    
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

let EU_clone src =
    let bb = new BytesBuilder()
    EU__bin bb src
    bin__EU (bb.bytes(),ref 0)

// [FILE] Structure


let pFILE__bin (bb:BytesBuilder) (p:pFILE) =

    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append
    
    let binDesc = p.Desc |> Encoding.UTF8.GetBytes
    binDesc.Length |> BitConverter.GetBytes |> bb.append
    binDesc |> bb.append
    
    let binSuffix = p.Suffix |> Encoding.UTF8.GetBytes
    binSuffix.Length |> BitConverter.GetBytes |> bb.append
    binSuffix |> bb.append
    
    p.Size |> BitConverter.GetBytes |> bb.append
    
    p.Thumbnail.Length |> BitConverter.GetBytes |> bb.append
    p.Thumbnail |> bb.append
    
    p.Owner |> BitConverter.GetBytes |> bb.append

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
    
    let count_Desc = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Desc <- Encoding.UTF8.GetString(bin,index.Value,count_Desc)
    index.Value <- index.Value + count_Desc
    
    let count_Suffix = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Suffix <- Encoding.UTF8.GetString(bin,index.Value,count_Suffix)
    index.Value <- index.Value + count_Suffix
    
    p.Size <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let length_Thumbnail = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Thumbnail <- Array.sub bin index.Value length_Thumbnail
    index.Value <- index.Value + length_Thumbnail
    
    p.Owner <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
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
        ("Desc",p.Desc |> Json.Str)
        ("Suffix",p.Suffix |> Json.Str)
        ("Size",p.Size.ToString() |> Json.Num)
        ("Thumbnail",p.Thumbnail |> Convert.ToBase64String |> Json.Str)
        ("Owner",p.Owner.ToString() |> Json.Num) |]
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
    
    p.Caption <- checkfield fields "Caption"
    
    p.Desc <- checkfield fields "Desc"
    
    p.Suffix <- checkfieldz fields "Suffix" 4
    
    p.Size <- checkfield fields "Size" |> parse_int64
    
    p.Owner <- checkfield fields "Owner" |> parse_int64
    
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
        
        {
            ID = ID
            Sort = Sort
            Createdat = Createdat
            Updatedat = Updatedat
            p = p } |> Some
        
    | None -> None

let FILE_clone src =
    let bb = new BytesBuilder()
    FILE__bin bb src
    bin__FILE (bb.bytes(),ref 0)

// [FBIND] Structure


let pFBIND__bin (bb:BytesBuilder) (p:pFBIND) =

    
    p.File |> BitConverter.GetBytes |> bb.append
    
    p.Moment |> BitConverter.GetBytes |> bb.append
    
    let binDesc = p.Desc |> Encoding.UTF8.GetBytes
    binDesc.Length |> BitConverter.GetBytes |> bb.append
    binDesc |> bb.append

let FBIND__bin (bb:BytesBuilder) (v:FBIND) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pFBIND__bin bb v.p

let bin__pFBIND (bi:BinIndexed):pFBIND =
    let bin,index = bi

    let p = pFBIND_empty()
    
    p.File <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    p.Moment <- BitConverter.ToInt64(bin,index.Value)
    index.Value <- index.Value + 8
    
    let count_Desc = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Desc <- Encoding.UTF8.GetString(bin,index.Value,count_Desc)
    index.Value <- index.Value + count_Desc
    
    p

let bin__FBIND (bi:BinIndexed):FBIND =
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
        p = bin__pFBIND bi }

let pFBIND__json (p:pFBIND) =

    [|
        ("File",p.File.ToString() |> Json.Num)
        ("Moment",p.Moment.ToString() |> Json.Num)
        ("Desc",p.Desc |> Json.Str) |]
    |> Json.Braket

let FBIND__json (v:FBIND) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pFBIND__json v.p) |]
    |> Json.Braket

let FBIND__jsonTbw (w:TextBlockWriter) (v:FBIND) =
    json__str w (FBIND__json v)

let FBIND__jsonStr (v:FBIND) =
    (FBIND__json v) |> json__strFinal


let json__pFBINDo (json:Json):pFBIND option =
    let fields = json |> json__items

    let p = pFBIND_empty()
    
    p.File <- checkfield fields "File" |> parse_int64
    
    p.Moment <- checkfield fields "Moment" |> parse_int64
    
    p.Desc <- checkfield fields "Desc"
    
    p |> Some
    

let json__FBINDo (json:Json):FBIND option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pFBINDo v
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

let FBIND_clone src =
    let bb = new BytesBuilder()
    FBIND__bin bb src
    bin__FBIND (bb.bytes(),ref 0)

// [MOMENT] Structure


let pMOMENT__bin (bb:BytesBuilder) (p:pMOMENT) =

    
    let binTitle = p.Title |> Encoding.UTF8.GetBytes
    binTitle.Length |> BitConverter.GetBytes |> bb.append
    binTitle |> bb.append
    
    let binSummary = p.Summary |> Encoding.UTF8.GetBytes
    binSummary.Length |> BitConverter.GetBytes |> bb.append
    binSummary |> bb.append
    
    let binFullText = p.FullText |> Encoding.UTF8.GetBytes
    binFullText.Length |> BitConverter.GetBytes |> bb.append
    binFullText |> bb.append
    
    let binPreviewImgUrl = p.PreviewImgUrl |> Encoding.UTF8.GetBytes
    binPreviewImgUrl.Length |> BitConverter.GetBytes |> bb.append
    binPreviewImgUrl |> bb.append
    
    let binLink = p.Link |> Encoding.UTF8.GetBytes
    binLink.Length |> BitConverter.GetBytes |> bb.append
    binLink |> bb.append
    
    p.Type |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.State |> EnumToValue |> BitConverter.GetBytes |> bb.append
    
    p.MediaType |> EnumToValue |> BitConverter.GetBytes |> bb.append

let MOMENT__bin (bb:BytesBuilder) (v:MOMENT) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pMOMENT__bin bb v.p

let bin__pMOMENT (bi:BinIndexed):pMOMENT =
    let bin,index = bi

    let p = pMOMENT_empty()
    
    let count_Title = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Title <- Encoding.UTF8.GetString(bin,index.Value,count_Title)
    index.Value <- index.Value + count_Title
    
    let count_Summary = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Summary <- Encoding.UTF8.GetString(bin,index.Value,count_Summary)
    index.Value <- index.Value + count_Summary
    
    let count_FullText = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.FullText <- Encoding.UTF8.GetString(bin,index.Value,count_FullText)
    index.Value <- index.Value + count_FullText
    
    let count_PreviewImgUrl = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.PreviewImgUrl <- Encoding.UTF8.GetString(bin,index.Value,count_PreviewImgUrl)
    index.Value <- index.Value + count_PreviewImgUrl
    
    let count_Link = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Link <- Encoding.UTF8.GetString(bin,index.Value,count_Link)
    index.Value <- index.Value + count_Link
    
    p.Type <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.State <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p.MediaType <- BitConverter.ToInt32(bin,index.Value) |> EnumOfValue
    index.Value <- index.Value + 4
    
    p

let bin__MOMENT (bi:BinIndexed):MOMENT =
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
        p = bin__pMOMENT bi }

let pMOMENT__json (p:pMOMENT) =

    [|
        ("Title",p.Title |> Json.Str)
        ("Summary",p.Summary |> Json.Str)
        ("FullText",p.FullText |> Json.Str)
        ("PreviewImgUrl",p.PreviewImgUrl |> Json.Str)
        ("Link",p.Link |> Json.Str)
        ("Type",(p.Type |> EnumToValue).ToString() |> Json.Num)
        ("State",(p.State |> EnumToValue).ToString() |> Json.Num)
        ("MediaType",(p.MediaType |> EnumToValue).ToString() |> Json.Num) |]
    |> Json.Braket

let MOMENT__json (v:MOMENT) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pMOMENT__json v.p) |]
    |> Json.Braket

let MOMENT__jsonTbw (w:TextBlockWriter) (v:MOMENT) =
    json__str w (MOMENT__json v)

let MOMENT__jsonStr (v:MOMENT) =
    (MOMENT__json v) |> json__strFinal


let json__pMOMENTo (json:Json):pMOMENT option =
    let fields = json |> json__items

    let p = pMOMENT_empty()
    
    p.Title <- checkfield fields "Title"
    
    p.Summary <- checkfield fields "Summary"
    
    p.FullText <- checkfield fields "FullText"
    
    p.PreviewImgUrl <- checkfield fields "PreviewImgUrl"
    
    p.Link <- checkfield fields "Link"
    
    p.Type <- checkfield fields "Type" |> parse_int32 |> EnumOfValue
    
    p.State <- checkfield fields "State" |> parse_int32 |> EnumOfValue
    
    p.MediaType <- checkfield fields "MediaType" |> parse_int32 |> EnumOfValue
    
    p |> Some
    

let json__MOMENTo (json:Json):MOMENT option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pMOMENTo v
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

let MOMENT_clone src =
    let bb = new BytesBuilder()
    MOMENT__bin bb src
    bin__MOMENT (bb.bytes(),ref 0)

// [LOG] Structure


let pLOG__bin (bb:BytesBuilder) (p:pLOG) =

    
    let binLocation = p.Location |> Encoding.UTF8.GetBytes
    binLocation.Length |> BitConverter.GetBytes |> bb.append
    binLocation |> bb.append
    
    let binContent = p.Content |> Encoding.UTF8.GetBytes
    binContent.Length |> BitConverter.GetBytes |> bb.append
    binContent |> bb.append
    
    let binSql = p.Sql |> Encoding.UTF8.GetBytes
    binSql.Length |> BitConverter.GetBytes |> bb.append
    binSql |> bb.append

let LOG__bin (bb:BytesBuilder) (v:LOG) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pLOG__bin bb v.p

let bin__pLOG (bi:BinIndexed):pLOG =
    let bin,index = bi

    let p = pLOG_empty()
    
    let count_Location = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Location <- Encoding.UTF8.GetString(bin,index.Value,count_Location)
    index.Value <- index.Value + count_Location
    
    let count_Content = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Content <- Encoding.UTF8.GetString(bin,index.Value,count_Content)
    index.Value <- index.Value + count_Content
    
    let count_Sql = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Sql <- Encoding.UTF8.GetString(bin,index.Value,count_Sql)
    index.Value <- index.Value + count_Sql
    
    p

let bin__LOG (bi:BinIndexed):LOG =
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
        p = bin__pLOG bi }

let pLOG__json (p:pLOG) =

    [|
        ("Location",p.Location |> Json.Str)
        ("Content",p.Content |> Json.Str)
        ("Sql",p.Sql |> Json.Str) |]
    |> Json.Braket

let LOG__json (v:LOG) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pLOG__json v.p) |]
    |> Json.Braket

let LOG__jsonTbw (w:TextBlockWriter) (v:LOG) =
    json__str w (LOG__json v)

let LOG__jsonStr (v:LOG) =
    (LOG__json v) |> json__strFinal


let json__pLOGo (json:Json):pLOG option =
    let fields = json |> json__items

    let p = pLOG_empty()
    
    p.Location <- checkfield fields "Location"
    
    p.Content <- checkfield fields "Content"
    
    p.Sql <- checkfield fields "Sql"
    
    p |> Some
    

let json__LOGo (json:Json):LOG option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pLOGo v
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

let LOG_clone src =
    let bb = new BytesBuilder()
    LOG__bin bb src
    bin__LOG (bb.bytes(),ref 0)

// [PLOG] Structure


let pPLOG__bin (bb:BytesBuilder) (p:pPLOG) =

    
    let binIp = p.Ip |> Encoding.UTF8.GetBytes
    binIp.Length |> BitConverter.GetBytes |> bb.append
    binIp |> bb.append
    
    let binRequest = p.Request |> Encoding.UTF8.GetBytes
    binRequest.Length |> BitConverter.GetBytes |> bb.append
    binRequest |> bb.append

let PLOG__bin (bb:BytesBuilder) (v:PLOG) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pPLOG__bin bb v.p

let bin__pPLOG (bi:BinIndexed):pPLOG =
    let bin,index = bi

    let p = pPLOG_empty()
    
    let count_Ip = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Ip <- Encoding.UTF8.GetString(bin,index.Value,count_Ip)
    index.Value <- index.Value + count_Ip
    
    let count_Request = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Request <- Encoding.UTF8.GetString(bin,index.Value,count_Request)
    index.Value <- index.Value + count_Request
    
    p

let bin__PLOG (bi:BinIndexed):PLOG =
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
        p = bin__pPLOG bi }

let pPLOG__json (p:pPLOG) =

    [|
        ("Ip",p.Ip |> Json.Str)
        ("Request",p.Request |> Json.Str) |]
    |> Json.Braket

let PLOG__json (v:PLOG) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pPLOG__json v.p) |]
    |> Json.Braket

let PLOG__jsonTbw (w:TextBlockWriter) (v:PLOG) =
    json__str w (PLOG__json v)

let PLOG__jsonStr (v:PLOG) =
    (PLOG__json v) |> json__strFinal


let json__pPLOGo (json:Json):pPLOG option =
    let fields = json |> json__items

    let p = pPLOG_empty()
    
    p.Ip <- checkfieldz fields "Ip" 64
    
    p.Request <- checkfield fields "Request"
    
    p |> Some
    

let json__PLOGo (json:Json):PLOG option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pPLOGo v
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

let PLOG_clone src =
    let bb = new BytesBuilder()
    PLOG__bin bb src
    bin__PLOG (bb.bytes(),ref 0)

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

let API_clone src =
    let bb = new BytesBuilder()
    API__bin bb src
    bin__API (bb.bytes(),ref 0)

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

let FIELD_clone src =
    let bb = new BytesBuilder()
    FIELD__bin bb src
    bin__FIELD (bb.bytes(),ref 0)

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

let HOSTCONFIG_clone src =
    let bb = new BytesBuilder()
    HOSTCONFIG__bin bb src
    bin__HOSTCONFIG (bb.bytes(),ref 0)

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

let PROJECT_clone src =
    let bb = new BytesBuilder()
    PROJECT__bin bb src
    bin__PROJECT (bb.bytes(),ref 0)

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

let TABLE_clone src =
    let bb = new BytesBuilder()
    TABLE__bin bb src
    bin__TABLE (bb.bytes(),ref 0)

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

let COMP_clone src =
    let bb = new BytesBuilder()
    COMP__bin bb src
    bin__COMP (bb.bytes(),ref 0)

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

let PAGE_clone src =
    let bb = new BytesBuilder()
    PAGE__bin bb src
    bin__PAGE (bb.bytes(),ref 0)

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

let TEMPLATE_clone src =
    let bb = new BytesBuilder()
    TEMPLATE__bin bb src
    bin__TEMPLATE (bb.bytes(),ref 0)

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

let VARTYPE_clone src =
    let bb = new BytesBuilder()
    VARTYPE__bin bb src
    bin__VARTYPE (bb.bytes(),ref 0)

let mutable conn = ""

let db__pBOOK(line:Object[]): pBOOK =
    let p = pBOOK_empty()

    p.Caption <- string(line[4]).TrimEnd()
    p.Email <- string(line[5]).TrimEnd()
    p.Message <- string(line[6]).TrimEnd()

    p

let pBOOK__sps (p:pBOOK) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Email", p.Email) |> kvp__sqlparam
            ("Message", p.Message) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("caption", p.Caption) |> kvp__sqlparam
            ("email", p.Email) |> kvp__sqlparam
            ("message", p.Message) |> kvp__sqlparam |]

let db__BOOK = db__Rcd db__pBOOK

let BOOK_wrapper item: BOOK =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pBOOK_clone (p:pBOOK): pBOOK = {
    Caption = p.Caption
    Email = p.Email
    Message = p.Message }

let BOOK_update_transaction output (updater,suc,fail) (rcd:BOOK) =
    let rollback_p = rcd.p |> pBOOK_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,BOOK_table,BOOK_sql_update(),pBOOK__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let BOOK_update output (rcd:BOOK) =
    rcd
    |> BOOK_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let BOOK_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment BOOK_id
    let ctime = DateTime.UtcNow
    match create (conn,output,BOOK_table,pBOOK__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> BOOK_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let BOOK_create output p =
    BOOK_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__BOOKo id: BOOK option = id__rcd(conn,BOOK_fieldorders(),BOOK_table,db__BOOK) id

let BOOK_metadata = {
    fieldorders = BOOK_fieldorders
    db__rcd = db__BOOK 
    wrapper = BOOK_wrapper
    sps = pBOOK__sps
    id = BOOK_id
    id__rcdo = id__BOOKo
    clone = pBOOK_clone
    empty__p = pBOOK_empty
    rcd__bin = BOOK__bin
    bin__rcd = bin__BOOK
    p__json = pBOOK__json
    json__po = json__pBOOKo
    rcd__json = BOOK__json
    json__rcdo = json__BOOKo
    sql_update = BOOK_sql_update
    rcd_update = BOOK_update
    table = BOOK_table
    shorthand = "book" }

let BOOKTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Ca_Book' AND xtype='U')
    BEGIN

        CREATE TABLE Ca_Book ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Caption]
    ,[Email]
    ,[Message])
    END
    """


let db__pEU(line:Object[]): pEU =
    let p = pEU_empty()

    p.Caption <- string(line[4]).TrimEnd()
    p.AuthType <- EnumOfValue(if Convert.IsDBNull(line[5]) then 0 else line[5] :?> int)

    p

let pEU__sps (p:pEU) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Caption", p.Caption) |> kvp__sqlparam
            ("AuthType", p.AuthType) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("caption", p.Caption) |> kvp__sqlparam
            ("authtype", p.AuthType) |> kvp__sqlparam |]

let db__EU = db__Rcd db__pEU

let EU_wrapper item: EU =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pEU_clone (p:pEU): pEU = {
    Caption = p.Caption
    AuthType = p.AuthType }

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
    p__json = pEU__json
    json__po = json__pEUo
    rcd__json = EU__json
    json__rcdo = json__EUo
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
    ,[AuthType])
    END
    """


let db__pFILE(line:Object[]): pFILE =
    let p = pFILE_empty()

    p.Caption <- string(line[4]).TrimEnd()
    p.Desc <- string(line[5]).TrimEnd()
    p.Suffix <- string(line[6]).TrimEnd()
    p.Size <- if Convert.IsDBNull(line[7]) then 0L else line[7] :?> int64
    p.Thumbnail <- if Convert.IsDBNull(line[8]) then [| |] else line[8] :?> byte[]
    p.Owner <- if Convert.IsDBNull(line[9]) then 0L else line[9] :?> int64

    p

let pFILE__sps (p:pFILE) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Caption", p.Caption) |> kvp__sqlparam
            ("Desc", p.Desc) |> kvp__sqlparam
            ("Suffix", p.Suffix) |> kvp__sqlparam
            ("Size", p.Size) |> kvp__sqlparam
            ("Thumbnail", p.Thumbnail) |> kvp__sqlparam
            ("Owner", p.Owner) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("caption", p.Caption) |> kvp__sqlparam
            ("desc", p.Desc) |> kvp__sqlparam
            ("suffix", p.Suffix) |> kvp__sqlparam
            ("size", p.Size) |> kvp__sqlparam
            ("thumbnail", p.Thumbnail) |> kvp__sqlparam
            ("owner", p.Owner) |> kvp__sqlparam |]

let db__FILE = db__Rcd db__pFILE

let FILE_wrapper item: FILE =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pFILE_clone (p:pFILE): pFILE = {
    Caption = p.Caption
    Desc = p.Desc
    Suffix = p.Suffix
    Size = p.Size
    Thumbnail = p.Thumbnail
    Owner = p.Owner }

let FILE_update_transaction output (updater,suc,fail) (rcd:FILE) =
    let rollback_p = rcd.p |> pFILE_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,FILE_table,FILE_sql_update(),pFILE__sps,suc,fail)
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
    

let id__FILEo id: FILE option = id__rcd(conn,FILE_fieldorders(),FILE_table,db__FILE) id

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
    p__json = pFILE__json
    json__po = json__pFILEo
    rcd__json = FILE__json
    json__rcdo = json__FILEo
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
    ,[Desc]
    ,[Suffix]
    ,[Size]
    ,[Thumbnail]
    ,[Owner])
    END
    """


let db__pFBIND(line:Object[]): pFBIND =
    let p = pFBIND_empty()

    p.File <- if Convert.IsDBNull(line[4]) then 0L else line[4] :?> int64
    p.Moment <- if Convert.IsDBNull(line[5]) then 0L else line[5] :?> int64
    p.Desc <- string(line[6]).TrimEnd()

    p

let pFBIND__sps (p:pFBIND) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("File", p.File) |> kvp__sqlparam
            ("Moment", p.Moment) |> kvp__sqlparam
            ("Desc", p.Desc) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("file", p.File) |> kvp__sqlparam
            ("moment", p.Moment) |> kvp__sqlparam
            ("desc", p.Desc) |> kvp__sqlparam |]

let db__FBIND = db__Rcd db__pFBIND

let FBIND_wrapper item: FBIND =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pFBIND_clone (p:pFBIND): pFBIND = {
    File = p.File
    Moment = p.Moment
    Desc = p.Desc }

let FBIND_update_transaction output (updater,suc,fail) (rcd:FBIND) =
    let rollback_p = rcd.p |> pFBIND_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,FBIND_table,FBIND_sql_update(),pFBIND__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let FBIND_update output (rcd:FBIND) =
    rcd
    |> FBIND_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let FBIND_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment FBIND_id
    let ctime = DateTime.UtcNow
    match create (conn,output,FBIND_table,pFBIND__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> FBIND_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let FBIND_create output p =
    FBIND_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__FBINDo id: FBIND option = id__rcd(conn,FBIND_fieldorders(),FBIND_table,db__FBIND) id

let FBIND_metadata = {
    fieldorders = FBIND_fieldorders
    db__rcd = db__FBIND 
    wrapper = FBIND_wrapper
    sps = pFBIND__sps
    id = FBIND_id
    id__rcdo = id__FBINDo
    clone = pFBIND_clone
    empty__p = pFBIND_empty
    rcd__bin = FBIND__bin
    bin__rcd = bin__FBIND
    p__json = pFBIND__json
    json__po = json__pFBINDo
    rcd__json = FBIND__json
    json__rcdo = json__FBINDo
    sql_update = FBIND_sql_update
    rcd_update = FBIND_update
    table = FBIND_table
    shorthand = "fbind" }

let FBINDTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Social_FileBind' AND xtype='U')
    BEGIN

        CREATE TABLE Social_FileBind ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[File]
    ,[Moment]
    ,[Desc])
    END
    """


let db__pMOMENT(line:Object[]): pMOMENT =
    let p = pMOMENT_empty()

    p.Title <- string(line[4]).TrimEnd()
    p.Summary <- string(line[5]).TrimEnd()
    p.FullText <- string(line[6]).TrimEnd()
    p.PreviewImgUrl <- string(line[7]).TrimEnd()
    p.Link <- string(line[8]).TrimEnd()
    p.Type <- EnumOfValue(if Convert.IsDBNull(line[9]) then 0 else line[9] :?> int)
    p.State <- EnumOfValue(if Convert.IsDBNull(line[10]) then 0 else line[10] :?> int)
    p.MediaType <- EnumOfValue(if Convert.IsDBNull(line[11]) then 0 else line[11] :?> int)

    p

let pMOMENT__sps (p:pMOMENT) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Title", p.Title) |> kvp__sqlparam
            ("Summary", p.Summary) |> kvp__sqlparam
            ("FullText", p.FullText) |> kvp__sqlparam
            ("PreviewImgUrl", p.PreviewImgUrl) |> kvp__sqlparam
            ("Link", p.Link) |> kvp__sqlparam
            ("Type", p.Type) |> kvp__sqlparam
            ("State", p.State) |> kvp__sqlparam
            ("MediaType", p.MediaType) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("title", p.Title) |> kvp__sqlparam
            ("summary", p.Summary) |> kvp__sqlparam
            ("fulltext", p.FullText) |> kvp__sqlparam
            ("previewimgurl", p.PreviewImgUrl) |> kvp__sqlparam
            ("link", p.Link) |> kvp__sqlparam
            ("type", p.Type) |> kvp__sqlparam
            ("state", p.State) |> kvp__sqlparam
            ("mediatype", p.MediaType) |> kvp__sqlparam |]

let db__MOMENT = db__Rcd db__pMOMENT

let MOMENT_wrapper item: MOMENT =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pMOMENT_clone (p:pMOMENT): pMOMENT = {
    Title = p.Title
    Summary = p.Summary
    FullText = p.FullText
    PreviewImgUrl = p.PreviewImgUrl
    Link = p.Link
    Type = p.Type
    State = p.State
    MediaType = p.MediaType }

let MOMENT_update_transaction output (updater,suc,fail) (rcd:MOMENT) =
    let rollback_p = rcd.p |> pMOMENT_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,MOMENT_table,MOMENT_sql_update(),pMOMENT__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let MOMENT_update output (rcd:MOMENT) =
    rcd
    |> MOMENT_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let MOMENT_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment MOMENT_id
    let ctime = DateTime.UtcNow
    match create (conn,output,MOMENT_table,pMOMENT__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> MOMENT_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let MOMENT_create output p =
    MOMENT_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__MOMENTo id: MOMENT option = id__rcd(conn,MOMENT_fieldorders(),MOMENT_table,db__MOMENT) id

let MOMENT_metadata = {
    fieldorders = MOMENT_fieldorders
    db__rcd = db__MOMENT 
    wrapper = MOMENT_wrapper
    sps = pMOMENT__sps
    id = MOMENT_id
    id__rcdo = id__MOMENTo
    clone = pMOMENT_clone
    empty__p = pMOMENT_empty
    rcd__bin = MOMENT__bin
    bin__rcd = bin__MOMENT
    p__json = pMOMENT__json
    json__po = json__pMOMENTo
    rcd__json = MOMENT__json
    json__rcdo = json__MOMENTo
    sql_update = MOMENT_sql_update
    rcd_update = MOMENT_update
    table = MOMENT_table
    shorthand = "moment" }

let MOMENTTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Social_Moment' AND xtype='U')
    BEGIN

        CREATE TABLE Social_Moment ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Title]
    ,[Summary]
    ,[FullText]
    ,[PreviewImgUrl]
    ,[Link]
    ,[Type]
    ,[State]
    ,[MediaType])
    END
    """


let db__pLOG(line:Object[]): pLOG =
    let p = pLOG_empty()

    p.Location <- string(line[4]).TrimEnd()
    p.Content <- string(line[5]).TrimEnd()
    p.Sql <- string(line[6]).TrimEnd()

    p

let pLOG__sps (p:pLOG) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Location", p.Location) |> kvp__sqlparam
            ("Content", p.Content) |> kvp__sqlparam
            ("Sql", p.Sql) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("location", p.Location) |> kvp__sqlparam
            ("content", p.Content) |> kvp__sqlparam
            ("sql", p.Sql) |> kvp__sqlparam |]

let db__LOG = db__Rcd db__pLOG

let LOG_wrapper item: LOG =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pLOG_clone (p:pLOG): pLOG = {
    Location = p.Location
    Content = p.Content
    Sql = p.Sql }

let LOG_update_transaction output (updater,suc,fail) (rcd:LOG) =
    let rollback_p = rcd.p |> pLOG_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,LOG_table,LOG_sql_update(),pLOG__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let LOG_update output (rcd:LOG) =
    rcd
    |> LOG_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let LOG_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment LOG_id
    let ctime = DateTime.UtcNow
    match create (conn,output,LOG_table,pLOG__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> LOG_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let LOG_create output p =
    LOG_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__LOGo id: LOG option = id__rcd(conn,LOG_fieldorders(),LOG_table,db__LOG) id

let LOG_metadata = {
    fieldorders = LOG_fieldorders
    db__rcd = db__LOG 
    wrapper = LOG_wrapper
    sps = pLOG__sps
    id = LOG_id
    id__rcdo = id__LOGo
    clone = pLOG_clone
    empty__p = pLOG_empty
    rcd__bin = LOG__bin
    bin__rcd = bin__LOG
    p__json = pLOG__json
    json__po = json__pLOGo
    rcd__json = LOG__json
    json__rcdo = json__LOGo
    sql_update = LOG_sql_update
    rcd_update = LOG_update
    table = LOG_table
    shorthand = "log" }

let LOGTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Sys_Log' AND xtype='U')
    BEGIN

        CREATE TABLE Sys_Log ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Location]
    ,[Content]
    ,[Sql])
    END
    """


let db__pPLOG(line:Object[]): pPLOG =
    let p = pPLOG_empty()

    p.Ip <- string(line[4]).TrimEnd()
    p.Request <- string(line[5]).TrimEnd()

    p

let pPLOG__sps (p:pPLOG) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Ip", p.Ip) |> kvp__sqlparam
            ("Request", p.Request) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("ip", p.Ip) |> kvp__sqlparam
            ("request", p.Request) |> kvp__sqlparam |]

let db__PLOG = db__Rcd db__pPLOG

let PLOG_wrapper item: PLOG =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pPLOG_clone (p:pPLOG): pPLOG = {
    Ip = p.Ip
    Request = p.Request }

let PLOG_update_transaction output (updater,suc,fail) (rcd:PLOG) =
    let rollback_p = rcd.p |> pPLOG_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,PLOG_table,PLOG_sql_update(),pPLOG__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let PLOG_update output (rcd:PLOG) =
    rcd
    |> PLOG_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let PLOG_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment PLOG_id
    let ctime = DateTime.UtcNow
    match create (conn,output,PLOG_table,pPLOG__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> PLOG_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let PLOG_create output p =
    PLOG_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__PLOGo id: PLOG option = id__rcd(conn,PLOG_fieldorders(),PLOG_table,db__PLOG) id

let PLOG_metadata = {
    fieldorders = PLOG_fieldorders
    db__rcd = db__PLOG 
    wrapper = PLOG_wrapper
    sps = pPLOG__sps
    id = PLOG_id
    id__rcdo = id__PLOGo
    clone = pPLOG_clone
    empty__p = pPLOG_empty
    rcd__bin = PLOG__bin
    bin__rcd = bin__PLOG
    p__json = pPLOG__json
    json__po = json__pPLOGo
    rcd__json = PLOG__json
    json__rcdo = json__PLOGo
    sql_update = PLOG_sql_update
    rcd_update = PLOG_update
    table = PLOG_table
    shorthand = "plog" }

let PLOGTxSqlServer =
    """
    IF NOT EXISTS(SELECT * FROM sysobjects WHERE [name]='Sys_PageLog' AND xtype='U')
    BEGIN

        CREATE TABLE Sys_PageLog ([ID] BIGINT NOT NULL
    ,[Createdat] BIGINT NOT NULL
    ,[Updatedat] BIGINT NOT NULL
    ,[Sort] BIGINT NOT NULL,
    ,[Ip]
    ,[Request])
    END
    """


let db__pAPI(line:Object[]): pAPI =
    let p = pAPI_empty()

    p.Name <- string(line[4]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line[5]) then 0L else line[5] :?> int64

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
    p__json = pAPI__json
    json__po = json__pAPIo
    rcd__json = API__json
    json__rcdo = json__APIo
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

    p.Name <- string(line[4]).TrimEnd()
    p.Desc <- string(line[5]).TrimEnd()
    p.FieldType <- EnumOfValue(if Convert.IsDBNull(line[6]) then 0 else line[6] :?> int)
    p.Length <- if Convert.IsDBNull(line[7]) then 0L else line[7] :?> int64
    p.SelectLines <- string(line[8]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line[9]) then 0L else line[9] :?> int64
    p.Table <- if Convert.IsDBNull(line[10]) then 0L else line[10] :?> int64

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
    p__json = pFIELD__json
    json__po = json__pFIELDo
    rcd__json = FIELD__json
    json__rcdo = json__FIELDo
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

    p.Hostname <- string(line[4]).TrimEnd()
    p.Database <- EnumOfValue(if Convert.IsDBNull(line[5]) then 0 else line[5] :?> int)
    p.DatabaseName <- string(line[6]).TrimEnd()
    p.DatabaseConn <- string(line[7]).TrimEnd()
    p.DirVs <- string(line[8]).TrimEnd()
    p.DirVsCodeWeb <- string(line[9]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line[10]) then 0L else line[10] :?> int64

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
    p__json = pHOSTCONFIG__json
    json__po = json__pHOSTCONFIGo
    rcd__json = HOSTCONFIG__json
    json__rcdo = json__HOSTCONFIGo
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

    p.Code <- string(line[4]).TrimEnd()
    p.Caption <- string(line[5]).TrimEnd()
    p.TypeSessionUser <- string(line[6]).TrimEnd()

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
    p__json = pPROJECT__json
    json__po = json__pPROJECTo
    rcd__json = PROJECT__json
    json__rcdo = json__PROJECTo
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

    p.Name <- string(line[4]).TrimEnd()
    p.Desc <- string(line[5]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line[6]) then 0L else line[6] :?> int64

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
    p__json = pTABLE__json
    json__po = json__pTABLEo
    rcd__json = TABLE__json
    json__rcdo = json__TABLEo
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

    p.Name <- string(line[4]).TrimEnd()
    p.Caption <- string(line[5]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line[6]) then 0L else line[6] :?> int64

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
    p__json = pCOMP__json
    json__po = json__pCOMPo
    rcd__json = COMP__json
    json__rcdo = json__COMPo
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

    p.Name <- string(line[4]).TrimEnd()
    p.Caption <- string(line[5]).TrimEnd()
    p.Route <- string(line[6]).TrimEnd()
    p.OgTitle <- string(line[7]).TrimEnd()
    p.OgDesc <- string(line[8]).TrimEnd()
    p.OgImage <- string(line[9]).TrimEnd()
    p.Template <- if Convert.IsDBNull(line[10]) then 0L else line[10] :?> int64
    p.Project <- if Convert.IsDBNull(line[11]) then 0L else line[11] :?> int64

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
    p__json = pPAGE__json
    json__po = json__pPAGEo
    rcd__json = PAGE__json
    json__rcdo = json__PAGEo
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

    p.Name <- string(line[4]).TrimEnd()
    p.Caption <- string(line[5]).TrimEnd()
    p.Project <- if Convert.IsDBNull(line[6]) then 0L else line[6] :?> int64

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
    p__json = pTEMPLATE__json
    json__po = json__pTEMPLATEo
    rcd__json = TEMPLATE__json
    json__rcdo = json__TEMPLATEo
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

    p.Name <- string(line[4]).TrimEnd()
    p.Type <- string(line[5]).TrimEnd()
    p.Val <- string(line[6]).TrimEnd()
    p.BindType <- EnumOfValue(if Convert.IsDBNull(line[7]) then 0 else line[7] :?> int)
    p.Bind <- if Convert.IsDBNull(line[8]) then 0L else line[8] :?> int64
    p.Project <- if Convert.IsDBNull(line[9]) then 0L else line[9] :?> int64

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
    p__json = pVARTYPE__json
    json__po = json__pVARTYPEo
    rcd__json = VARTYPE__json
    json__rcdo = json__VARTYPEo
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
| BOOK = 0
| EU = 1
| FILE = 2
| FBIND = 3
| MOMENT = 4
| LOG = 5
| PLOG = 6
| API = 7
| FIELD = 8
| HOSTCONFIG = 9
| PROJECT = 10
| TABLE = 11
| COMP = 12
| PAGE = 13
| TEMPLATE = 14
| VARTYPE = 15

let tablenames = [|
    BOOK_metadata.table
    EU_metadata.table
    FILE_metadata.table
    FBIND_metadata.table
    MOMENT_metadata.table
    LOG_metadata.table
    PLOG_metadata.table
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

    let sqlMaxCa_Book, sqlCountCa_Book =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_Book]", "SELECT COUNT(ID) FROM [Ca_Book]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_book", "SELECT COUNT(id) FROM ca_book"
    match singlevalue_query conn (str__sql sqlMaxCa_Book) with
    | Some v ->
        let max = v :?> int64
        if max > BOOK_id.Value then
            BOOK_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_Book) with
    | Some v ->
        BOOK_count.Value <-
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

    let sqlMaxCa_File, sqlCountCa_File =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ca_File]", "SELECT COUNT(ID) FROM [Ca_File]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ca_file", "SELECT COUNT(id) FROM ca_file"
    match singlevalue_query conn (str__sql sqlMaxCa_File) with
    | Some v ->
        let max = v :?> int64
        if max > FILE_id.Value then
            FILE_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountCa_File) with
    | Some v ->
        FILE_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxSocial_FileBind, sqlCountSocial_FileBind =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Social_FileBind]", "SELECT COUNT(ID) FROM [Social_FileBind]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM social_filebind", "SELECT COUNT(id) FROM social_filebind"
    match singlevalue_query conn (str__sql sqlMaxSocial_FileBind) with
    | Some v ->
        let max = v :?> int64
        if max > FBIND_id.Value then
            FBIND_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountSocial_FileBind) with
    | Some v ->
        FBIND_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxSocial_Moment, sqlCountSocial_Moment =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Social_Moment]", "SELECT COUNT(ID) FROM [Social_Moment]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM social_moment", "SELECT COUNT(id) FROM social_moment"
    match singlevalue_query conn (str__sql sqlMaxSocial_Moment) with
    | Some v ->
        let max = v :?> int64
        if max > MOMENT_id.Value then
            MOMENT_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountSocial_Moment) with
    | Some v ->
        MOMENT_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxSys_Log, sqlCountSys_Log =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Sys_Log]", "SELECT COUNT(ID) FROM [Sys_Log]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM sys_log", "SELECT COUNT(id) FROM sys_log"
    match singlevalue_query conn (str__sql sqlMaxSys_Log) with
    | Some v ->
        let max = v :?> int64
        if max > LOG_id.Value then
            LOG_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountSys_Log) with
    | Some v ->
        LOG_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()

    let sqlMaxSys_PageLog, sqlCountSys_PageLog =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Sys_PageLog]", "SELECT COUNT(ID) FROM [Sys_PageLog]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM sys_pagelog", "SELECT COUNT(id) FROM sys_pagelog"
    match singlevalue_query conn (str__sql sqlMaxSys_PageLog) with
    | Some v ->
        let max = v :?> int64
        if max > PLOG_id.Value then
            PLOG_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountSys_PageLog) with
    | Some v ->
        PLOG_count.Value <-
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