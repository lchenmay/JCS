module Shared.OrmTypes

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

// [Ts_Api] (API)

type pAPI = {
mutable Name: Chars
mutable Project: FK}


type API = Rcd<pAPI>

let API_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","project" """

let pAPI_fieldordersArray = [|
    "Name"
    "Project" |]

let API_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,project=@project"

let pAPI_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            FK("project") |]

let pAPI_empty(): pAPI = {
    Name = ""
    Project = 0L }

let API_id = ref 7523431L
let API_count = ref 0
let API_table = "Ts_Api"

// [Ts_Field] (FIELD)

type pFIELD = {
mutable Name: Chars
mutable Desc: Text
mutable Project: FK
mutable Table: FK}


type FIELD = Rcd<pFIELD>

let FIELD_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Desc],[Project],[Table]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","desc","project","table" """

let pFIELD_fieldordersArray = [|
    "Name"
    "Desc"
    "Project"
    "Table" |]

let FIELD_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Desc]=@Desc,[Project]=@Project,[Table]=@Table"
    | Rdbms.PostgreSql -> "name=@name,desc=@desc,project=@project,table=@table"

let pFIELD_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Text("Desc")
            FK("Project")
            FK("Table") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Text("desc")
            FK("project")
            FK("table") |]

let pFIELD_empty(): pFIELD = {
    Name = ""
    Desc = ""
    Project = 0L
    Table = 0L }

let FIELD_id = ref 7523431L
let FIELD_count = ref 0
let FIELD_table = "Ts_Field"

// [Ts_HostConfig] (HOSTCONFIG)

type hostconfigDatabaseEnum = 
| SQLSERVER = 0 // SQL Server
| PostgreSQL = 1 // PostgreSQL

let hostconfigDatabaseEnums = [| hostconfigDatabaseEnum.SQLSERVER; hostconfigDatabaseEnum.PostgreSQL |]
let hostconfigDatabaseEnumstrs = [| "hostconfigDatabaseEnum"; "hostconfigDatabaseEnum" |]
let hostconfigDatabaseNum = 2

let int__hostconfigDatabaseEnum v =
    match v with
    | 0 -> Some hostconfigDatabaseEnum.SQLSERVER
    | 1 -> Some hostconfigDatabaseEnum.PostgreSQL
    | _ -> None

let str__hostconfigDatabaseEnum s =
    match s with
    | "SQLSERVER" -> Some hostconfigDatabaseEnum.SQLSERVER
    | "PostgreSQL" -> Some hostconfigDatabaseEnum.PostgreSQL
    | _ -> None

let hostconfigDatabaseEnum__caption e =
    match e with
    | hostconfigDatabaseEnum.SQLSERVER -> "SQL Server"
    | hostconfigDatabaseEnum.PostgreSQL -> "PostgreSQL"
    | _ -> ""

type pHOSTCONFIG = {
mutable Hostname: Chars
mutable Database: hostconfigDatabaseEnum
mutable DatabaseName: Chars
mutable DatabaseConn: Chars
mutable DirVsShared: Chars
mutable DirVsCodeWeb: Chars
mutable Project: FK}


type HOSTCONFIG = Rcd<pHOSTCONFIG>

let HOSTCONFIG_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Hostname],[Database],[DatabaseName],[DatabaseConn],[DirVsShared],[DirVsCodeWeb],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "hostname","database","databasename","databaseconn","dirvsshared","dirvscodeweb","project" """

let pHOSTCONFIG_fieldordersArray = [|
    "Hostname"
    "Database"
    "DatabaseName"
    "DatabaseConn"
    "DirVsShared"
    "DirVsCodeWeb"
    "Project" |]

let HOSTCONFIG_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Hostname]=@Hostname,[Database]=@Database,[DatabaseName]=@DatabaseName,[DatabaseConn]=@DatabaseConn,[DirVsShared]=@DirVsShared,[DirVsCodeWeb]=@DirVsCodeWeb,[Project]=@Project"
    | Rdbms.PostgreSql -> "hostname=@hostname,database=@database,databasename=@databasename,databaseconn=@databaseconn,dirvsshared=@dirvsshared,dirvscodeweb=@dirvscodeweb,project=@project"

let pHOSTCONFIG_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Hostname", 64)
            SelectLines("Database", [| ("SQLSERVER","SQL Server");("PostgreSQL","PostgreSQL") |])
            Chars("DatabaseName", 64)
            Chars("DatabaseConn", 64)
            Chars("DirVsShared", 64)
            Chars("DirVsCodeWeb", 64)
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("hostname", 64)
            SelectLines("database", [| ("SQLSERVER","SQL Server");("PostgreSQL","PostgreSQL") |])
            Chars("databasename", 64)
            Chars("databaseconn", 64)
            Chars("dirvsshared", 64)
            Chars("dirvscodeweb", 64)
            FK("project") |]

let pHOSTCONFIG_empty(): pHOSTCONFIG = {
    Hostname = ""
    Database = EnumOfValue 0
    DatabaseName = ""
    DatabaseConn = ""
    DirVsShared = ""
    DirVsCodeWeb = ""
    Project = 0L }

let HOSTCONFIG_id = ref 34512L
let HOSTCONFIG_count = ref 0
let HOSTCONFIG_table = "Ts_HostConfig"

// [Ts_Project] (PROJECT)

type pPROJECT = {
mutable Code: Chars
mutable Caption: Chars}


type PROJECT = Rcd<pPROJECT>

let PROJECT_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code","caption" """

let pPROJECT_fieldordersArray = [|
    "Code"
    "Caption" |]

let PROJECT_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code]=@Code,[Caption]=@Caption"
    | Rdbms.PostgreSql -> "code=@code,caption=@caption"

let pPROJECT_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code", 64)
            Chars("Caption", 256) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code", 64)
            Chars("caption", 256) |]

let pPROJECT_empty(): pPROJECT = {
    Code = ""
    Caption = "" }

let PROJECT_id = ref 234345L
let PROJECT_count = ref 0
let PROJECT_table = "Ts_Project"

// [Ts_Table] (TABLE)

type pTABLE = {
mutable Name: Chars
mutable Desc: Text
mutable Project: FK}


type TABLE = Rcd<pTABLE>

let TABLE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Desc],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","desc","project" """

let pTABLE_fieldordersArray = [|
    "Name"
    "Desc"
    "Project" |]

let TABLE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Desc]=@Desc,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,desc=@desc,project=@project"

let pTABLE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Text("Desc")
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Text("desc")
            FK("project") |]

let pTABLE_empty(): pTABLE = {
    Name = ""
    Desc = ""
    Project = 0L }

let TABLE_id = ref 7523431L
let TABLE_count = ref 0
let TABLE_table = "Ts_Table"

// [Ts_UiComponent] (COMP)

type pCOMP = {
mutable Name: Chars
mutable Caption: Chars
mutable Project: FK}


type COMP = Rcd<pCOMP>

let COMP_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Caption],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","caption","project" """

let pCOMP_fieldordersArray = [|
    "Name"
    "Caption"
    "Project" |]

let COMP_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Caption]=@Caption,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,caption=@caption,project=@project"

let pCOMP_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Chars("Caption", 256)
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Chars("caption", 256)
            FK("project") |]

let pCOMP_empty(): pCOMP = {
    Name = ""
    Caption = ""
    Project = 0L }

let COMP_id = ref 6723431L
let COMP_count = ref 0
let COMP_table = "Ts_UiComponent"

// [Ts_UiPage] (PAGE)

type pPAGE = {
mutable Name: Chars
mutable Caption: Chars
mutable Route: Text
mutable OgTitle: Text
mutable OgDesc: Text
mutable OgImage: Text
mutable Template: FK
mutable Project: FK}


type PAGE = Rcd<pPAGE>

let PAGE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Caption],[Route],[OgTitle],[OgDesc],[OgImage],[Template],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","caption","route","ogtitle","ogdesc","ogimage","template","project" """

let pPAGE_fieldordersArray = [|
    "Name"
    "Caption"
    "Route"
    "OgTitle"
    "OgDesc"
    "OgImage"
    "Template"
    "Project" |]

let PAGE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Caption]=@Caption,[Route]=@Route,[OgTitle]=@OgTitle,[OgDesc]=@OgDesc,[OgImage]=@OgImage,[Template]=@Template,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,caption=@caption,route=@route,ogtitle=@ogtitle,ogdesc=@ogdesc,ogimage=@ogimage,template=@template,project=@project"

let pPAGE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Chars("Caption", 256)
            Text("Route")
            Text("OgTitle")
            Text("OgDesc")
            Text("OgImage")
            FK("Template")
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Chars("caption", 256)
            Text("route")
            Text("ogtitle")
            Text("ogdesc")
            Text("ogimage")
            FK("template")
            FK("project") |]

let pPAGE_empty(): pPAGE = {
    Name = ""
    Caption = ""
    Route = ""
    OgTitle = ""
    OgDesc = ""
    OgImage = ""
    Template = 0L
    Project = 0L }

let PAGE_id = ref 6723431L
let PAGE_count = ref 0
let PAGE_table = "Ts_UiPage"

// [Ts_UiTemplate] (TEMPLATE)

type pTEMPLATE = {
mutable Name: Chars
mutable Caption: Chars
mutable Project: FK}


type TEMPLATE = Rcd<pTEMPLATE>

let TEMPLATE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Caption],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","caption","project" """

let pTEMPLATE_fieldordersArray = [|
    "Name"
    "Caption"
    "Project" |]

let TEMPLATE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Caption]=@Caption,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,caption=@caption,project=@project"

let pTEMPLATE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Chars("Caption", 256)
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Chars("caption", 256)
            FK("project") |]

let pTEMPLATE_empty(): pTEMPLATE = {
    Name = ""
    Caption = ""
    Project = 0L }

let TEMPLATE_id = ref 6723431L
let TEMPLATE_count = ref 0
let TEMPLATE_table = "Ts_UiTemplate"

// [Ts_VarType] (VARTYPE)

type vartypeBindTypeEnum = 
| ApiRequest = 0 // API Request
| ApiResponse = 1 // API Response
| CompState = 2 // Component State
| CompProps = 3 // Component Propos
| PageState = 4 // Page State
| PageProps = 5 // Page Propos

let vartypeBindTypeEnums = [| vartypeBindTypeEnum.ApiRequest; vartypeBindTypeEnum.ApiResponse; vartypeBindTypeEnum.CompState; vartypeBindTypeEnum.CompProps; vartypeBindTypeEnum.PageState; vartypeBindTypeEnum.PageProps |]
let vartypeBindTypeEnumstrs = [| "vartypeBindTypeEnum"; "vartypeBindTypeEnum"; "vartypeBindTypeEnum"; "vartypeBindTypeEnum"; "vartypeBindTypeEnum"; "vartypeBindTypeEnum" |]
let vartypeBindTypeNum = 6

let int__vartypeBindTypeEnum v =
    match v with
    | 0 -> Some vartypeBindTypeEnum.ApiRequest
    | 1 -> Some vartypeBindTypeEnum.ApiResponse
    | 2 -> Some vartypeBindTypeEnum.CompState
    | 3 -> Some vartypeBindTypeEnum.CompProps
    | 4 -> Some vartypeBindTypeEnum.PageState
    | 5 -> Some vartypeBindTypeEnum.PageProps
    | _ -> None

let str__vartypeBindTypeEnum s =
    match s with
    | "ApiRequest" -> Some vartypeBindTypeEnum.ApiRequest
    | "ApiResponse" -> Some vartypeBindTypeEnum.ApiResponse
    | "CompState" -> Some vartypeBindTypeEnum.CompState
    | "CompProps" -> Some vartypeBindTypeEnum.CompProps
    | "PageState" -> Some vartypeBindTypeEnum.PageState
    | "PageProps" -> Some vartypeBindTypeEnum.PageProps
    | _ -> None

let vartypeBindTypeEnum__caption e =
    match e with
    | vartypeBindTypeEnum.ApiRequest -> "API Request"
    | vartypeBindTypeEnum.ApiResponse -> "API Response"
    | vartypeBindTypeEnum.CompState -> "Component State"
    | vartypeBindTypeEnum.CompProps -> "Component Propos"
    | vartypeBindTypeEnum.PageState -> "Page State"
    | vartypeBindTypeEnum.PageProps -> "Page Propos"
    | _ -> ""

type pVARTYPE = {
mutable Name: Chars
mutable Type: Chars
mutable BindType: vartypeBindTypeEnum
mutable Bind: Integer
mutable Project: FK}


type VARTYPE = Rcd<pVARTYPE>

let VARTYPE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Type],[BindType],[Bind],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","type","bindtype","bind","project" """

let pVARTYPE_fieldordersArray = [|
    "Name"
    "Type"
    "BindType"
    "Bind"
    "Project" |]

let VARTYPE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Type]=@Type,[BindType]=@BindType,[Bind]=@Bind,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,type=@type,bindtype=@bindtype,bind=@bind,project=@project"

let pVARTYPE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Chars("Type", 64)
            SelectLines("BindType", [| ("ApiRequest","API Request");("ApiResponse","API Response");("CompState","Component State");("CompProps","Component Propos");("PageState","Page State");("PageProps","Page Propos") |])
            Integer("Bind")
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Chars("type", 64)
            SelectLines("bindtype", [| ("ApiRequest","API Request");("ApiResponse","API Response");("CompState","Component State");("CompProps","Component Propos");("PageState","Page State");("PageProps","Page Propos") |])
            Integer("bind")
            FK("project") |]

let pVARTYPE_empty(): pVARTYPE = {
    Name = ""
    Type = ""
    BindType = EnumOfValue 0
    Bind = 0L
    Project = 0L }

let VARTYPE_id = ref 7523431L
let VARTYPE_count = ref 0
let VARTYPE_table = "Ts_VarType"
