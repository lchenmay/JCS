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

type pHOSTCONFIG = {
mutable Hostname: Chars
mutable DatabaseName: Chars
mutable DatabaseConn: Chars
mutable DirVsShared: Chars
mutable DirVsCodeWeb: Chars
mutable Project: FK}


type HOSTCONFIG = Rcd<pHOSTCONFIG>

let HOSTCONFIG_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Hostname],[DatabaseName],[DatabaseConn],[DirVsShared],[DirVsCodeWeb],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "hostname","databasename","databaseconn","dirvsshared","dirvscodeweb","project" """

let pHOSTCONFIG_fieldordersArray = [|
    "Hostname"
    "DatabaseName"
    "DatabaseConn"
    "DirVsShared"
    "DirVsCodeWeb"
    "Project" |]

let HOSTCONFIG_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Hostname]=@Hostname,[DatabaseName]=@DatabaseName,[DatabaseConn]=@DatabaseConn,[DirVsShared]=@DirVsShared,[DirVsCodeWeb]=@DirVsCodeWeb,[Project]=@Project"
    | Rdbms.PostgreSql -> "hostname=@hostname,databasename=@databasename,databaseconn=@databaseconn,dirvsshared=@dirvsshared,dirvscodeweb=@dirvscodeweb,project=@project"

let pHOSTCONFIG_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Hostname", 64)
            Chars("DatabaseName", 64)
            Chars("DatabaseConn", 64)
            Chars("DirVsShared", 64)
            Chars("DirVsCodeWeb", 64)
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("hostname", 64)
            Chars("databasename", 64)
            Chars("databaseconn", 64)
            Chars("dirvsshared", 64)
            Chars("dirvscodeweb", 64)
            FK("project") |]

let pHOSTCONFIG_empty(): pHOSTCONFIG = {
    Hostname = ""
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
mutable OgTitle: Text
mutable OgDesc: Text
mutable OgImage: Text
mutable Template: FK
mutable Project: FK}


type PAGE = Rcd<pPAGE>

let PAGE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Caption],[OgTitle],[OgDesc],[OgImage],[Template],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","caption","ogtitle","ogdesc","ogimage","template","project" """

let pPAGE_fieldordersArray = [|
    "Name"
    "Caption"
    "OgTitle"
    "OgDesc"
    "OgImage"
    "Template"
    "Project" |]

let PAGE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Caption]=@Caption,[OgTitle]=@OgTitle,[OgDesc]=@OgDesc,[OgImage]=@OgImage,[Template]=@Template,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,caption=@caption,ogtitle=@ogtitle,ogdesc=@ogdesc,ogimage=@ogimage,template=@template,project=@project"

let pPAGE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Chars("Caption", 256)
            Text("OgTitle")
            Text("OgDesc")
            Text("OgImage")
            FK("Template")
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Chars("caption", 256)
            Text("ogtitle")
            Text("ogdesc")
            Text("ogimage")
            FK("template")
            FK("project") |]

let pPAGE_empty(): pPAGE = {
    Name = ""
    Caption = ""
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
