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

// [PROJ] Structure


let pPROJ__bin (bb:BytesBuilder) (p:pPROJ) =

    
    let binCode = p.Code |> Encoding.UTF8.GetBytes
    binCode.Length |> BitConverter.GetBytes |> bb.append
    binCode |> bb.append
    
    let binCaption = p.Caption |> Encoding.UTF8.GetBytes
    binCaption.Length |> BitConverter.GetBytes |> bb.append
    binCaption |> bb.append

let PROJ__bin (bb:BytesBuilder) (v:PROJ) =
    v.ID |> BitConverter.GetBytes |> bb.append
    v.Sort |> BitConverter.GetBytes |> bb.append
    DateTime__bin bb v.Createdat
    DateTime__bin bb v.Updatedat
    
    pPROJ__bin bb v.p

let bin__pPROJ (bi:BinIndexed):pPROJ =
    let bin,index = bi

    let p = pPROJ_empty()
    
    let count_Code = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Code <- Encoding.UTF8.GetString(bin,index.Value,count_Code)
    index.Value <- index.Value + count_Code
    
    let count_Caption = BitConverter.ToInt32(bin,index.Value)
    index.Value <- index.Value + 4
    p.Caption <- Encoding.UTF8.GetString(bin,index.Value,count_Caption)
    index.Value <- index.Value + count_Caption
    
    p

let bin__PROJ (bi:BinIndexed):PROJ =
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
        p = bin__pPROJ bi }

let pPROJ__json (p:pPROJ) =

    [|
        ("Code",p.Code |> Json.Str)
        ("Caption",p.Caption |> Json.Str) |]
    |> Json.Braket

let PROJ__json (v:PROJ) =

    let p = v.p
    
    [|  ("id",v.ID.ToString() |> Json.Num)
        ("sort",v.Sort.ToString() |> Json.Num)
        ("createdat",(v.Createdat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("updatedat",(v.Updatedat |> Util.Time.wintime__unixtime).ToString() |> Json.Num)
        ("p",pPROJ__json v.p) |]
    |> Json.Braket

let PROJ__jsonTbw (w:TextBlockWriter) (v:PROJ) =
    json__str w (PROJ__json v)

let PROJ__jsonStr (v:PROJ) =
    (PROJ__json v) |> json__strFinal


let json__pPROJo (json:Json):pPROJ option =
    let fields = json |> json__items

    let p = pPROJ_empty()
    
    p.Code <- checkfieldz fields "Code" 64
    
    p.Caption <- checkfieldz fields "Caption" 256
    
    p |> Some
    

let json__PROJo (json:Json):PROJ option =
    let fields = json |> json__items

    let ID = checkfield fields "id" |> parse_int64
    let Sort = checkfield fields "sort" |> parse_int64
    let Createdat = checkfield fields "createdat" |> parse_int64 |> DateTime.FromBinary
    let Updatedat = checkfield fields "updatedat" |> parse_int64 |> DateTime.FromBinary
    
    let o  =
        match
            json
            |> tryFindByAtt "p" with
        | Some (s,v) -> json__pPROJo v
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

let mutable conn = ""

let db__pPROJ(line:Object[]): pPROJ =
    let p = pPROJ_empty()

    p.Code <- string(line.[4]).TrimEnd()
    p.Caption <- string(line.[5]).TrimEnd()

    p

let pPROJ__sps (p:pPROJ) =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            ("Code", p.Code) |> kvp__sqlparam
            ("Caption", p.Caption) |> kvp__sqlparam |]
    | Rdbms.PostgreSql ->
        [|
            ("code", p.Code) |> kvp__sqlparam
            ("caption", p.Caption) |> kvp__sqlparam |]

let db__PROJ = db__Rcd db__pPROJ

let PROJ_wrapper item: PROJ =
    let (i,c,u,s),p = item
    { ID = i; Createdat = c; Updatedat = u; Sort = s; p = p }

let pPROJ_clone (p:pPROJ): pPROJ = {
    Code = p.Code
    Caption = p.Caption }

let PROJ_update_transaction output (updater,suc,fail) (rcd:PROJ) =
    let rollback_p = rcd.p |> pPROJ_clone
    let rollback_updatedat = rcd.Updatedat
    updater rcd.p
    let ctime,res =
        (rcd.ID,rcd.p,rollback_p,rollback_updatedat)
        |> update (conn,output,PROJ_table,PROJ_sql_update(),pPROJ__sps,suc,fail)
    match res with
    | Suc ctx ->
        rcd.Updatedat <- ctime
        suc(ctime,ctx)
    | Fail(eso,ctx) ->
        rcd.p <- rollback_p
        rcd.Updatedat <- rollback_updatedat
        fail eso

let PROJ_update output (rcd:PROJ) =
    rcd
    |> PROJ_update_transaction output ((fun p -> ()),(fun (ctime,ctx) -> ()),(fun dte -> ()))

let PROJ_create_incremental_transaction output (suc,fail) p =
    let cid = Interlocked.Increment PROJ_id
    let ctime = DateTime.UtcNow
    match create (conn,output,PROJ_table,pPROJ__sps) (cid,ctime,p) with
    | Suc ctx -> ((cid,ctime,ctime,cid),p) |> PROJ_wrapper |> suc
    | Fail(eso,ctx) -> fail(eso,ctx)

let PROJ_create output p =
    PROJ_create_incremental_transaction output ((fun rcd -> ()),(fun (eso,ctx) -> ())) p
    

let id__PROJo id: PROJ option = id__rcd(conn,PROJ_fieldorders(),PROJ_table,db__PROJ) id

let PROJ_metadata = {
    fieldorders = PROJ_fieldorders
    db__rcd = db__PROJ 
    wrapper = PROJ_wrapper
    sps = pPROJ__sps
    id = PROJ_id
    id__rcdo = id__PROJo
    clone = pPROJ_clone
    empty__p = pPROJ_empty
    rcd__bin = PROJ__bin
    bin__rcd = bin__PROJ
    sql_update = PROJ_sql_update
    rcd_update = PROJ_update
    table = PROJ_table
    shorthand = "proj" }

let PROJTxSqlServer =
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


type MetadataEnum = 
| PROJ = 0

let tablenames = [|
    PROJ_metadata.table |]

let init() =

    let sqlMaxTs_Project, sqlCountTs_Project =
        match rdbms with
        | Rdbms.SqlServer -> "SELECT MAX(ID) FROM [Ts_Project]", "SELECT COUNT(ID) FROM [Ts_Project]"
        | Rdbms.PostgreSql -> "SELECT MAX(id) FROM ts_project", "SELECT COUNT(id) FROM ts_project"
    match singlevalue_query conn (str__sql sqlMaxTs_Project) with
    | Some v ->
        let max = v :?> int64
        if max > PROJ_id.Value then
            PROJ_id.Value <- max
    | None -> ()

    match singlevalue_query conn (str__sql sqlCountTs_Project) with
    | Some v ->
        PROJ_count.Value <-
            match rdbms with
            | Rdbms.SqlServer -> v :?> int32
            | Rdbms.PostgreSql -> v :?> int64 |> int32
    | None -> ()
    ()