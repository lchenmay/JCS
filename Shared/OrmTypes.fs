module Shared.OrmTypes

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


rdbms <- Rdbms.PostgreSql


// [Ca_Address] (ADDRESS)

type addressTypeEnum = 
| Default = 0 // 默认
| Biz = 1 // 机构
| EndUser = 2 // 用户

let addressTypeEnums = [| addressTypeEnum.Default; addressTypeEnum.Biz; addressTypeEnum.EndUser |]
let addressTypeEnumstrs = [| "addressTypeEnum"; "addressTypeEnum"; "addressTypeEnum" |]
let addressTypeNum = 3

let int__addressTypeEnum v =
    match v with
    | 0 -> Some addressTypeEnum.Default
    | 1 -> Some addressTypeEnum.Biz
    | 2 -> Some addressTypeEnum.EndUser
    | _ -> None

let str__addressTypeEnum s =
    match s with
    | "Default" -> Some addressTypeEnum.Default
    | "Biz" -> Some addressTypeEnum.Biz
    | "EndUser" -> Some addressTypeEnum.EndUser
    | _ -> None

let addressTypeEnum__caption e =
    match e with
    | addressTypeEnum.Default -> "默认"
    | addressTypeEnum.Biz -> "机构"
    | addressTypeEnum.EndUser -> "用户"
    | _ -> ""

type pADDRESS = {
mutable Caption: Caption
mutable Bind: Integer
mutable Type: addressTypeEnum
mutable Line1: Chars
mutable Line2: Chars
mutable State: Chars
mutable County: Chars
mutable Town: Chars
mutable Contact: Chars
mutable Tel: Chars
mutable Email: Chars
mutable Zip: Chars
mutable City: FK
mutable Country: FK
mutable Remarks: Text}


type ADDRESS = Rcd<pADDRESS>

let ADDRESS_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Caption],[Bind],[Type],[Line1],[Line2],[State],[County],[Town],[Contact],[Tel],[Email],[Zip],[City],[Country],[Remarks]"

let pADDRESS_fieldordersArray = [|
    "Caption"
    "Bind"
    "Type"
    "Line1"
    "Line2"
    "State"
    "County"
    "Town"
    "Contact"
    "Tel"
    "Email"
    "Zip"
    "City"
    "Country"
    "Remarks" |]

let ADDRESS_sql_update = "[Updatedat]=@Updatedat,[Caption]=@Caption,[Bind]=@Bind,[Type]=@Type,[Line1]=@Line1,[Line2]=@Line2,[State]=@State,[County]=@County,[Town]=@Town,[Contact]=@Contact,[Tel]=@Tel,[Email]=@Email,[Zip]=@Zip,[City]=@City,[Country]=@Country,[Remarks]=@Remarks"

let pADDRESS_fields = [|
    Caption("Caption", 256)
    Integer("Bind")
    SelectLines("Type", [| ("Default","默认");("Biz","机构");("EndUser","用户") |])
    Chars("Line1", 300)
    Chars("Line2", 300)
    Chars("State", 16)
    Chars("County", 16)
    Chars("Town", 16)
    Chars("Contact", 64)
    Chars("Tel", 20)
    Chars("Email", 256)
    Chars("Zip", 16)
    FK("City")
    FK("Country")
    Text("Remarks") |]

let pADDRESS_empty(): pADDRESS = {
    Caption = ""
    Bind = 0L
    Type = EnumOfValue 0
    Line1 = ""
    Line2 = ""
    State = ""
    County = ""
    Town = ""
    Contact = ""
    Tel = ""
    Email = ""
    Zip = ""
    City = 0L
    Country = 0L
    Remarks = "" }

let ADDRESS_id = ref 0L
let ADDRESS_count = ref 0
let ADDRESS_table = "Ca_Address"

// [Ca_Biz] (BIZ)

type pBIZ = {
mutable Code: Chars
mutable Caption: Caption
mutable Parent: FK
mutable BasicAcct: FK
mutable Desc: Text
mutable Website: Link
mutable Icon: Link
mutable City: FK
mutable Country: FK
mutable Lang: FK
mutable IsSocial: Boolean
mutable IsCmsSource: Boolean
mutable IsPay: Boolean
mutable MomentLatest: FK
mutable CountFollowers: Integer
mutable CountFollows: Integer
mutable CountMoments: Integer
mutable CountViews: Integer
mutable CountComments: Integer
mutable CountThumbUps: Integer
mutable CountThumbDns: Integer
mutable IsCrawling: Boolean
mutable IsGSeries: Boolean
mutable RemarksCentral: Text
mutable Agent: FK
mutable SiteCats: Text
mutable ConfiguredCats: Text}


type BIZ = Rcd<pBIZ>

let BIZ_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption],[Parent],[BasicAcct],[Desc],[Website],[Icon],[City],[Country],[Lang],[IsSocial],[IsCmsSource],[IsPay],[MomentLatest],[CountFollowers],[CountFollows],[CountMoments],[CountViews],[CountComments],[CountThumbUps],[CountThumbDns],[IsCrawling],[IsGSeries],[RemarksCentral],[Agent],[SiteCats],[ConfiguredCats]"

let pBIZ_fieldordersArray = [|
    "Code"
    "Caption"
    "Parent"
    "BasicAcct"
    "Desc"
    "Website"
    "Icon"
    "City"
    "Country"
    "Lang"
    "IsSocial"
    "IsCmsSource"
    "IsPay"
    "MomentLatest"
    "CountFollowers"
    "CountFollows"
    "CountMoments"
    "CountViews"
    "CountComments"
    "CountThumbUps"
    "CountThumbDns"
    "IsCrawling"
    "IsGSeries"
    "RemarksCentral"
    "Agent"
    "SiteCats"
    "ConfiguredCats" |]

let BIZ_sql_update = "[Updatedat]=@Updatedat,[Code]=@Code,[Caption]=@Caption,[Parent]=@Parent,[BasicAcct]=@BasicAcct,[Desc]=@Desc,[Website]=@Website,[Icon]=@Icon,[City]=@City,[Country]=@Country,[Lang]=@Lang,[IsSocial]=@IsSocial,[IsCmsSource]=@IsCmsSource,[IsPay]=@IsPay,[MomentLatest]=@MomentLatest,[CountFollowers]=@CountFollowers,[CountFollows]=@CountFollows,[CountMoments]=@CountMoments,[CountViews]=@CountViews,[CountComments]=@CountComments,[CountThumbUps]=@CountThumbUps,[CountThumbDns]=@CountThumbDns,[IsCrawling]=@IsCrawling,[IsGSeries]=@IsGSeries,[RemarksCentral]=@RemarksCentral,[Agent]=@Agent,[SiteCats]=@SiteCats,[ConfiguredCats]=@ConfiguredCats"

let pBIZ_fields = [|
    Chars("Code", 256)
    Caption("Caption", 256)
    FK("Parent")
    FK("BasicAcct")
    Text("Desc")
    Link("Website", 256)
    Link("Icon", 256)
    FK("City")
    FK("Country")
    FK("Lang")
    Boolean("IsSocial")
    Boolean("IsCmsSource")
    Boolean("IsPay")
    FK("MomentLatest")
    Integer("CountFollowers")
    Integer("CountFollows")
    Integer("CountMoments")
    Integer("CountViews")
    Integer("CountComments")
    Integer("CountThumbUps")
    Integer("CountThumbDns")
    Boolean("IsCrawling")
    Boolean("IsGSeries")
    Text("RemarksCentral")
    FK("Agent")
    Text("SiteCats")
    Text("ConfiguredCats") |]

let pBIZ_empty(): pBIZ = {
    Code = ""
    Caption = ""
    Parent = 0L
    BasicAcct = 0L
    Desc = ""
    Website = ""
    Icon = ""
    City = 0L
    Country = 0L
    Lang = 0L
    IsSocial = true
    IsCmsSource = true
    IsPay = true
    MomentLatest = 0L
    CountFollowers = 0L
    CountFollows = 0L
    CountMoments = 0L
    CountViews = 0L
    CountComments = 0L
    CountThumbUps = 0L
    CountThumbDns = 0L
    IsCrawling = true
    IsGSeries = true
    RemarksCentral = ""
    Agent = 0L
    SiteCats = ""
    ConfiguredCats = "" }

let BIZ_id = ref 75865464L
let BIZ_count = ref 0
let BIZ_table = "Ca_Biz"

// [Ca_Country] (CRY)

type pCRY = {
mutable Code2: Chars
mutable Caption: Chars
mutable Fullname: Chars
mutable Icon: Link
mutable Tel: Chars
mutable Cur: FK
mutable Capital: FK
mutable Place: FK
mutable Lang: FK}


type CRY = Rcd<pCRY>

let CRY_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Code2],[Caption],[Fullname],[Icon],[Tel],[Cur],[Capital],[Place],[Lang]"

let pCRY_fieldordersArray = [|
    "Code2"
    "Caption"
    "Fullname"
    "Icon"
    "Tel"
    "Cur"
    "Capital"
    "Place"
    "Lang" |]

let CRY_sql_update = "[Updatedat]=@Updatedat,[Code2]=@Code2,[Caption]=@Caption,[Fullname]=@Fullname,[Icon]=@Icon,[Tel]=@Tel,[Cur]=@Cur,[Capital]=@Capital,[Place]=@Place,[Lang]=@Lang"

let pCRY_fields = [|
    Chars("Code2", 2)
    Chars("Caption", 64)
    Chars("Fullname", 256)
    Link("Icon", 256)
    Chars("Tel", 4)
    FK("Cur")
    FK("Capital")
    FK("Place")
    FK("Lang") |]

let pCRY_empty(): pCRY = {
    Code2 = ""
    Caption = ""
    Fullname = ""
    Icon = ""
    Tel = ""
    Cur = 0L
    Capital = 0L
    Place = 0L
    Lang = 0L }

let CRY_id = ref 1001L
let CRY_count = ref 0
let CRY_table = "Ca_Country"

// [Ca_Cur] (CUR)

type curCurTypeEnum = 
| Legal = 0 // 法币
| Crypto = 1 // 数字币

let curCurTypeEnums = [| curCurTypeEnum.Legal; curCurTypeEnum.Crypto |]
let curCurTypeEnumstrs = [| "curCurTypeEnum"; "curCurTypeEnum" |]
let curCurTypeNum = 2

let int__curCurTypeEnum v =
    match v with
    | 0 -> Some curCurTypeEnum.Legal
    | 1 -> Some curCurTypeEnum.Crypto
    | _ -> None

let str__curCurTypeEnum s =
    match s with
    | "Legal" -> Some curCurTypeEnum.Legal
    | "Crypto" -> Some curCurTypeEnum.Crypto
    | _ -> None

let curCurTypeEnum__caption e =
    match e with
    | curCurTypeEnum.Legal -> "法币"
    | curCurTypeEnum.Crypto -> "数字币"
    | _ -> ""

type pCUR = {
mutable Code: Chars
mutable Caption: Caption
mutable Hidden: Boolean
mutable IsSac: Boolean
mutable IsTransfer: Boolean
mutable IsCash: Boolean
mutable EnableReward: Boolean
mutable EnableOTC: Boolean
mutable Icon: Link
mutable CurType: curCurTypeEnum
mutable Dec: Integer
mutable AnchorRate: Float
mutable Freezable: Boolean
mutable Authorizable: Boolean
mutable ChaninID: Chars
mutable ChaninName: Chars
mutable ContractAddress: Chars
mutable WalletAddress: Chars
mutable BaseRate: Float}


type CUR = Rcd<pCUR>

let CUR_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption],[Hidden],[IsSac],[IsTransfer],[IsCash],[EnableReward],[EnableOTC],[Icon],[CurType],[Dec],[AnchorRate],[Freezable],[Authorizable],[ChaninID],[ChaninName],[ContractAddress],[WalletAddress],[BaseRate]"

let pCUR_fieldordersArray = [|
    "Code"
    "Caption"
    "Hidden"
    "IsSac"
    "IsTransfer"
    "IsCash"
    "EnableReward"
    "EnableOTC"
    "Icon"
    "CurType"
    "Dec"
    "AnchorRate"
    "Freezable"
    "Authorizable"
    "ChaninID"
    "ChaninName"
    "ContractAddress"
    "WalletAddress"
    "BaseRate" |]

let CUR_sql_update = "[Updatedat]=@Updatedat,[Code]=@Code,[Caption]=@Caption,[Hidden]=@Hidden,[IsSac]=@IsSac,[IsTransfer]=@IsTransfer,[IsCash]=@IsCash,[EnableReward]=@EnableReward,[EnableOTC]=@EnableOTC,[Icon]=@Icon,[CurType]=@CurType,[Dec]=@Dec,[AnchorRate]=@AnchorRate,[Freezable]=@Freezable,[Authorizable]=@Authorizable,[ChaninID]=@ChaninID,[ChaninName]=@ChaninName,[ContractAddress]=@ContractAddress,[WalletAddress]=@WalletAddress,[BaseRate]=@BaseRate"

let pCUR_fields = [|
    Chars("Code", 16)
    Caption("Caption", 64)
    Boolean("Hidden")
    Boolean("IsSac")
    Boolean("IsTransfer")
    Boolean("IsCash")
    Boolean("EnableReward")
    Boolean("EnableOTC")
    Link("Icon", 512)
    SelectLines("CurType", [| ("Legal","法币");("Crypto","数字币") |])
    Integer("Dec")
    Float("AnchorRate")
    Boolean("Freezable")
    Boolean("Authorizable")
    Chars("ChaninID", 256)
    Chars("ChaninName", 256)
    Chars("ContractAddress", 256)
    Chars("WalletAddress", 256)
    Float("BaseRate") |]

let pCUR_empty(): pCUR = {
    Code = ""
    Caption = ""
    Hidden = true
    IsSac = true
    IsTransfer = true
    IsCash = true
    EnableReward = true
    EnableOTC = true
    Icon = ""
    CurType = EnumOfValue 0
    Dec = 0L
    AnchorRate = 0.0
    Freezable = true
    Authorizable = true
    ChaninID = ""
    ChaninName = ""
    ContractAddress = ""
    WalletAddress = ""
    BaseRate = 0.0 }

let CUR_id = ref 1001L
let CUR_count = ref 0
let CUR_table = "Ca_Cur"

// [Ca_EndUser] (EU)

type euGenderEnum = 
| Unknown = 0 // 未知
| Male = 1 // 男
| Female = 2 // 女

let euGenderEnums = [| euGenderEnum.Unknown; euGenderEnum.Male; euGenderEnum.Female |]
let euGenderEnumstrs = [| "euGenderEnum"; "euGenderEnum"; "euGenderEnum" |]
let euGenderNum = 3

let int__euGenderEnum v =
    match v with
    | 0 -> Some euGenderEnum.Unknown
    | 1 -> Some euGenderEnum.Male
    | 2 -> Some euGenderEnum.Female
    | _ -> None

let str__euGenderEnum s =
    match s with
    | "Unknown" -> Some euGenderEnum.Unknown
    | "Male" -> Some euGenderEnum.Male
    | "Female" -> Some euGenderEnum.Female
    | _ -> None

let euGenderEnum__caption e =
    match e with
    | euGenderEnum.Unknown -> "未知"
    | euGenderEnum.Male -> "男"
    | euGenderEnum.Female -> "女"
    | _ -> ""

type euStatusEnum = 
| Normal = 0 // 正常
| Frozen = 1 // 冻结
| Terminated = 2 // 注销

let euStatusEnums = [| euStatusEnum.Normal; euStatusEnum.Frozen; euStatusEnum.Terminated |]
let euStatusEnumstrs = [| "euStatusEnum"; "euStatusEnum"; "euStatusEnum" |]
let euStatusNum = 3

let int__euStatusEnum v =
    match v with
    | 0 -> Some euStatusEnum.Normal
    | 1 -> Some euStatusEnum.Frozen
    | 2 -> Some euStatusEnum.Terminated
    | _ -> None

let str__euStatusEnum s =
    match s with
    | "Normal" -> Some euStatusEnum.Normal
    | "Frozen" -> Some euStatusEnum.Frozen
    | "Terminated" -> Some euStatusEnum.Terminated
    | _ -> None

let euStatusEnum__caption e =
    match e with
    | euStatusEnum.Normal -> "正常"
    | euStatusEnum.Frozen -> "冻结"
    | euStatusEnum.Terminated -> "注销"
    | _ -> ""

type euAdminEnum = 
| None = 0 // 无
| Admin = 1 // 管理员

let euAdminEnums = [| euAdminEnum.None; euAdminEnum.Admin |]
let euAdminEnumstrs = [| "euAdminEnum"; "euAdminEnum" |]
let euAdminNum = 2

let int__euAdminEnum v =
    match v with
    | 0 -> Some euAdminEnum.None
    | 1 -> Some euAdminEnum.Admin
    | _ -> None

let str__euAdminEnum s =
    match s with
    | "None" -> Some euAdminEnum.None
    | "Admin" -> Some euAdminEnum.Admin
    | _ -> None

let euAdminEnum__caption e =
    match e with
    | euAdminEnum.None -> "无"
    | euAdminEnum.Admin -> "管理员"
    | _ -> ""

type euBizPartnerEnum = 
| None = 0 // None
| Partner = 1 // 

let euBizPartnerEnums = [| euBizPartnerEnum.None; euBizPartnerEnum.Partner |]
let euBizPartnerEnumstrs = [| "euBizPartnerEnum"; "euBizPartnerEnum" |]
let euBizPartnerNum = 2

let int__euBizPartnerEnum v =
    match v with
    | 0 -> Some euBizPartnerEnum.None
    | 1 -> Some euBizPartnerEnum.Partner
    | _ -> None

let str__euBizPartnerEnum s =
    match s with
    | "None" -> Some euBizPartnerEnum.None
    | "Partner" -> Some euBizPartnerEnum.Partner
    | _ -> None

let euBizPartnerEnum__caption e =
    match e with
    | euBizPartnerEnum.None -> "None"
    | euBizPartnerEnum.Partner -> ""
    | _ -> ""

type euVerifyEnum = 
| Normal = 0 // 常规
| Verified = 1 // 认证

let euVerifyEnums = [| euVerifyEnum.Normal; euVerifyEnum.Verified |]
let euVerifyEnumstrs = [| "euVerifyEnum"; "euVerifyEnum" |]
let euVerifyNum = 2

let int__euVerifyEnum v =
    match v with
    | 0 -> Some euVerifyEnum.Normal
    | 1 -> Some euVerifyEnum.Verified
    | _ -> None

let str__euVerifyEnum s =
    match s with
    | "Normal" -> Some euVerifyEnum.Normal
    | "Verified" -> Some euVerifyEnum.Verified
    | _ -> None

let euVerifyEnum__caption e =
    match e with
    | euVerifyEnum.Normal -> "常规"
    | euVerifyEnum.Verified -> "认证"
    | _ -> ""

type pEU = {
mutable Caption: Caption
mutable Username: Caption
mutable SocialAuthBiz: FK
mutable SocialAuthId: Text
mutable SocialAuthAvatar: Text
mutable Email: Chars
mutable Tel: Chars
mutable Gender: euGenderEnum
mutable Status: euStatusEnum
mutable Admin: euAdminEnum
mutable BizPartner: euBizPartnerEnum
mutable Privilege: Integer
mutable Verify: euVerifyEnum
mutable Pwd: Chars
mutable Online: Boolean
mutable Icon: Link
mutable Background: Link
mutable BasicAcct: FK
mutable Refer: Caption
mutable Referer: FK
mutable Url: Text
mutable About: Text}


type EU = Rcd<pEU>

let EU_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Caption],[Username],[SocialAuthBiz],[SocialAuthId],[SocialAuthAvatar],[Email],[Tel],[Gender],[Status],[Admin],[BizPartner],[Privilege],[Verify],[Pwd],[Online],[Icon],[Background],[BasicAcct],[Refer],[Referer],[Url],[About]"

let pEU_fieldordersArray = [|
    "Caption"
    "Username"
    "SocialAuthBiz"
    "SocialAuthId"
    "SocialAuthAvatar"
    "Email"
    "Tel"
    "Gender"
    "Status"
    "Admin"
    "BizPartner"
    "Privilege"
    "Verify"
    "Pwd"
    "Online"
    "Icon"
    "Background"
    "BasicAcct"
    "Refer"
    "Referer"
    "Url"
    "About" |]

let EU_sql_update = "[Updatedat]=@Updatedat,[Caption]=@Caption,[Username]=@Username,[SocialAuthBiz]=@SocialAuthBiz,[SocialAuthId]=@SocialAuthId,[SocialAuthAvatar]=@SocialAuthAvatar,[Email]=@Email,[Tel]=@Tel,[Gender]=@Gender,[Status]=@Status,[Admin]=@Admin,[BizPartner]=@BizPartner,[Privilege]=@Privilege,[Verify]=@Verify,[Pwd]=@Pwd,[Online]=@Online,[Icon]=@Icon,[Background]=@Background,[BasicAcct]=@BasicAcct,[Refer]=@Refer,[Referer]=@Referer,[Url]=@Url,[About]=@About"

let pEU_fields = [|
    Caption("Caption", 64)
    Caption("Username", 64)
    FK("SocialAuthBiz")
    Text("SocialAuthId")
    Text("SocialAuthAvatar")
    Chars("Email", 256)
    Chars("Tel", 32)
    SelectLines("Gender", [| ("Unknown","未知");("Male","男");("Female","女") |])
    SelectLines("Status", [| ("Normal","正常");("Frozen","冻结");("Terminated","注销") |])
    SelectLines("Admin", [| ("None","无");("Admin","管理员") |])
    SelectLines("BizPartner", [| ("None","None");("Partner","") |])
    Integer("Privilege")
    SelectLines("Verify", [| ("Normal","常规");("Verified","认证") |])
    Chars("Pwd", 16)
    Boolean("Online")
    Link("Icon", 256)
    Link("Background", 256)
    FK("BasicAcct")
    Caption("Refer", 7)
    FK("Referer")
    Text("Url")
    Text("About") |]

let pEU_empty(): pEU = {
    Caption = ""
    Username = ""
    SocialAuthBiz = 0L
    SocialAuthId = ""
    SocialAuthAvatar = ""
    Email = ""
    Tel = ""
    Gender = EnumOfValue 0
    Status = EnumOfValue 0
    Admin = EnumOfValue 0
    BizPartner = EnumOfValue 0
    Privilege = 0L
    Verify = EnumOfValue 0
    Pwd = ""
    Online = true
    Icon = ""
    Background = ""
    BasicAcct = 0L
    Refer = ""
    Referer = 0L
    Url = ""
    About = "" }

let EU_id = ref 54367576L
let EU_count = ref 0
let EU_table = "Ca_EndUser"

// [Ca_File] (FILE)

type fileEncryptEnum = 
| None = 0 // 未加密
| Sys = 1 // 系统加密
| Usr = 2 // 用户加密

let fileEncryptEnums = [| fileEncryptEnum.None; fileEncryptEnum.Sys; fileEncryptEnum.Usr |]
let fileEncryptEnumstrs = [| "fileEncryptEnum"; "fileEncryptEnum"; "fileEncryptEnum" |]
let fileEncryptNum = 3

let int__fileEncryptEnum v =
    match v with
    | 0 -> Some fileEncryptEnum.None
    | 1 -> Some fileEncryptEnum.Sys
    | 2 -> Some fileEncryptEnum.Usr
    | _ -> None

let str__fileEncryptEnum s =
    match s with
    | "None" -> Some fileEncryptEnum.None
    | "Sys" -> Some fileEncryptEnum.Sys
    | "Usr" -> Some fileEncryptEnum.Usr
    | _ -> None

let fileEncryptEnum__caption e =
    match e with
    | fileEncryptEnum.None -> "未加密"
    | fileEncryptEnum.Sys -> "系统加密"
    | fileEncryptEnum.Usr -> "用户加密"
    | _ -> ""

type fileBindTypeEnum = 
| Sys = 0 // 系统
| EndUser = 1 // 用户
| Biz = 2 // 机构
| Group = 3 // 群

let fileBindTypeEnums = [| fileBindTypeEnum.Sys; fileBindTypeEnum.EndUser; fileBindTypeEnum.Biz; fileBindTypeEnum.Group |]
let fileBindTypeEnumstrs = [| "fileBindTypeEnum"; "fileBindTypeEnum"; "fileBindTypeEnum"; "fileBindTypeEnum" |]
let fileBindTypeNum = 4

let int__fileBindTypeEnum v =
    match v with
    | 0 -> Some fileBindTypeEnum.Sys
    | 1 -> Some fileBindTypeEnum.EndUser
    | 2 -> Some fileBindTypeEnum.Biz
    | 3 -> Some fileBindTypeEnum.Group
    | _ -> None

let str__fileBindTypeEnum s =
    match s with
    | "Sys" -> Some fileBindTypeEnum.Sys
    | "EndUser" -> Some fileBindTypeEnum.EndUser
    | "Biz" -> Some fileBindTypeEnum.Biz
    | "Group" -> Some fileBindTypeEnum.Group
    | _ -> None

let fileBindTypeEnum__caption e =
    match e with
    | fileBindTypeEnum.Sys -> "系统"
    | fileBindTypeEnum.EndUser -> "用户"
    | fileBindTypeEnum.Biz -> "机构"
    | fileBindTypeEnum.Group -> "群"
    | _ -> ""

type fileStateEnum = 
| Normal = 0 // 正常
| Canceled = 1 // 已取消
| Uploading = 2 // 上传中
| PendingTranscode = 3 // 待媒体转码
| PendingBlockchain = 4 // 待区块链同步
| Deleted = 5 // 已删除

let fileStateEnums = [| fileStateEnum.Normal; fileStateEnum.Canceled; fileStateEnum.Uploading; fileStateEnum.PendingTranscode; fileStateEnum.PendingBlockchain; fileStateEnum.Deleted |]
let fileStateEnumstrs = [| "fileStateEnum"; "fileStateEnum"; "fileStateEnum"; "fileStateEnum"; "fileStateEnum"; "fileStateEnum" |]
let fileStateNum = 6

let int__fileStateEnum v =
    match v with
    | 0 -> Some fileStateEnum.Normal
    | 1 -> Some fileStateEnum.Canceled
    | 2 -> Some fileStateEnum.Uploading
    | 3 -> Some fileStateEnum.PendingTranscode
    | 4 -> Some fileStateEnum.PendingBlockchain
    | 5 -> Some fileStateEnum.Deleted
    | _ -> None

let str__fileStateEnum s =
    match s with
    | "Normal" -> Some fileStateEnum.Normal
    | "Canceled" -> Some fileStateEnum.Canceled
    | "Uploading" -> Some fileStateEnum.Uploading
    | "PendingTranscode" -> Some fileStateEnum.PendingTranscode
    | "PendingBlockchain" -> Some fileStateEnum.PendingBlockchain
    | "Deleted" -> Some fileStateEnum.Deleted
    | _ -> None

let fileStateEnum__caption e =
    match e with
    | fileStateEnum.Normal -> "正常"
    | fileStateEnum.Canceled -> "已取消"
    | fileStateEnum.Uploading -> "上传中"
    | fileStateEnum.PendingTranscode -> "待媒体转码"
    | fileStateEnum.PendingBlockchain -> "待区块链同步"
    | fileStateEnum.Deleted -> "已删除"
    | _ -> ""

type fileFileTypeEnum = 
| Default = 0 // 默认
| Text = 1 // 文本
| Bin = 2 // 二进制
| Img = 3 // 图片
| Video = 4 // 视频

let fileFileTypeEnums = [| fileFileTypeEnum.Default; fileFileTypeEnum.Text; fileFileTypeEnum.Bin; fileFileTypeEnum.Img; fileFileTypeEnum.Video |]
let fileFileTypeEnumstrs = [| "fileFileTypeEnum"; "fileFileTypeEnum"; "fileFileTypeEnum"; "fileFileTypeEnum"; "fileFileTypeEnum" |]
let fileFileTypeNum = 5

let int__fileFileTypeEnum v =
    match v with
    | 0 -> Some fileFileTypeEnum.Default
    | 1 -> Some fileFileTypeEnum.Text
    | 2 -> Some fileFileTypeEnum.Bin
    | 3 -> Some fileFileTypeEnum.Img
    | 4 -> Some fileFileTypeEnum.Video
    | _ -> None

let str__fileFileTypeEnum s =
    match s with
    | "Default" -> Some fileFileTypeEnum.Default
    | "Text" -> Some fileFileTypeEnum.Text
    | "Bin" -> Some fileFileTypeEnum.Bin
    | "Img" -> Some fileFileTypeEnum.Img
    | "Video" -> Some fileFileTypeEnum.Video
    | _ -> None

let fileFileTypeEnum__caption e =
    match e with
    | fileFileTypeEnum.Default -> "默认"
    | fileFileTypeEnum.Text -> "文本"
    | fileFileTypeEnum.Bin -> "二进制"
    | fileFileTypeEnum.Img -> "图片"
    | fileFileTypeEnum.Video -> "视频"
    | _ -> ""

type pFILE = {
mutable Caption: Caption
mutable Encrypt: fileEncryptEnum
mutable SHA256: Text
mutable Size: Integer
mutable Bind: Integer
mutable BindType: fileBindTypeEnum
mutable State: fileStateEnum
mutable Folder: FK
mutable FileType: fileFileTypeEnum
mutable JSON: Text}


type FILE = Rcd<pFILE>

let FILE_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Caption],[Encrypt],[SHA256],[Size],[Bind],[BindType],[State],[Folder],[FileType],[JSON]"

let pFILE_fieldordersArray = [|
    "Caption"
    "Encrypt"
    "SHA256"
    "Size"
    "Bind"
    "BindType"
    "State"
    "Folder"
    "FileType"
    "JSON" |]

let FILE_sql_update = "[Updatedat]=@Updatedat,[Caption]=@Caption,[Encrypt]=@Encrypt,[SHA256]=@SHA256,[Size]=@Size,[Bind]=@Bind,[BindType]=@BindType,[State]=@State,[Folder]=@Folder,[FileType]=@FileType,[JSON]=@JSON"

let pFILE_fields = [|
    Caption("Caption", 256)
    SelectLines("Encrypt", [| ("None","未加密");("Sys","系统加密");("Usr","用户加密") |])
    Text("SHA256")
    Integer("Size")
    Integer("Bind")
    SelectLines("BindType", [| ("Sys","系统");("EndUser","用户");("Biz","机构");("Group","群") |])
    SelectLines("State", [| ("Normal","正常");("Canceled","已取消");("Uploading","上传中");("PendingTranscode","待媒体转码");("PendingBlockchain","待区块链同步");("Deleted","已删除") |])
    FK("Folder")
    SelectLines("FileType", [| ("Default","默认");("Text","文本");("Bin","二进制");("Img","图片");("Video","视频") |])
    Text("JSON") |]

let pFILE_empty(): pFILE = {
    Caption = ""
    Encrypt = EnumOfValue 0
    SHA256 = ""
    Size = 0L
    Bind = 0L
    BindType = EnumOfValue 0
    State = EnumOfValue 0
    Folder = 0L
    FileType = EnumOfValue 0
    JSON = "" }

let FILE_id = ref 65464758L
let FILE_count = ref 0
let FILE_table = "Ca_File"

// [Ca_Folder] (FOLDER)

type folderEncryptEnum = 
| None = 0 // 未加密
| Sys = 1 // 系统加密
| Usr = 2 // 用户加密

let folderEncryptEnums = [| folderEncryptEnum.None; folderEncryptEnum.Sys; folderEncryptEnum.Usr |]
let folderEncryptEnumstrs = [| "folderEncryptEnum"; "folderEncryptEnum"; "folderEncryptEnum" |]
let folderEncryptNum = 3

let int__folderEncryptEnum v =
    match v with
    | 0 -> Some folderEncryptEnum.None
    | 1 -> Some folderEncryptEnum.Sys
    | 2 -> Some folderEncryptEnum.Usr
    | _ -> None

let str__folderEncryptEnum s =
    match s with
    | "None" -> Some folderEncryptEnum.None
    | "Sys" -> Some folderEncryptEnum.Sys
    | "Usr" -> Some folderEncryptEnum.Usr
    | _ -> None

let folderEncryptEnum__caption e =
    match e with
    | folderEncryptEnum.None -> "未加密"
    | folderEncryptEnum.Sys -> "系统加密"
    | folderEncryptEnum.Usr -> "用户加密"
    | _ -> ""

type folderBindTypeEnum = 
| Sys = 0 // 系统
| EndUser = 1 // 用户
| Biz = 2 // 机构
| Group = 3 // 群

let folderBindTypeEnums = [| folderBindTypeEnum.Sys; folderBindTypeEnum.EndUser; folderBindTypeEnum.Biz; folderBindTypeEnum.Group |]
let folderBindTypeEnumstrs = [| "folderBindTypeEnum"; "folderBindTypeEnum"; "folderBindTypeEnum"; "folderBindTypeEnum" |]
let folderBindTypeNum = 4

let int__folderBindTypeEnum v =
    match v with
    | 0 -> Some folderBindTypeEnum.Sys
    | 1 -> Some folderBindTypeEnum.EndUser
    | 2 -> Some folderBindTypeEnum.Biz
    | 3 -> Some folderBindTypeEnum.Group
    | _ -> None

let str__folderBindTypeEnum s =
    match s with
    | "Sys" -> Some folderBindTypeEnum.Sys
    | "EndUser" -> Some folderBindTypeEnum.EndUser
    | "Biz" -> Some folderBindTypeEnum.Biz
    | "Group" -> Some folderBindTypeEnum.Group
    | _ -> None

let folderBindTypeEnum__caption e =
    match e with
    | folderBindTypeEnum.Sys -> "系统"
    | folderBindTypeEnum.EndUser -> "用户"
    | folderBindTypeEnum.Biz -> "机构"
    | folderBindTypeEnum.Group -> "群"
    | _ -> ""

type pFOLDER = {
mutable Caption: Caption
mutable Encrypt: folderEncryptEnum
mutable Bind: Integer
mutable BindType: folderBindTypeEnum
mutable Parent: FK}


type FOLDER = Rcd<pFOLDER>

let FOLDER_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Caption],[Encrypt],[Bind],[BindType],[Parent]"

let pFOLDER_fieldordersArray = [|
    "Caption"
    "Encrypt"
    "Bind"
    "BindType"
    "Parent" |]

let FOLDER_sql_update = "[Updatedat]=@Updatedat,[Caption]=@Caption,[Encrypt]=@Encrypt,[Bind]=@Bind,[BindType]=@BindType,[Parent]=@Parent"

let pFOLDER_fields = [|
    Caption("Caption", 256)
    SelectLines("Encrypt", [| ("None","未加密");("Sys","系统加密");("Usr","用户加密") |])
    Integer("Bind")
    SelectLines("BindType", [| ("Sys","系统");("EndUser","用户");("Biz","机构");("Group","群") |])
    FK("Parent") |]

let pFOLDER_empty(): pFOLDER = {
    Caption = ""
    Encrypt = EnumOfValue 0
    Bind = 0L
    BindType = EnumOfValue 0
    Parent = 0L }

let FOLDER_id = ref 54665847L
let FOLDER_count = ref 0
let FOLDER_table = "Ca_Folder"

// [Ca_Lang] (LANG)

type langTextDirectionEnum = 
| Default = 0 // 默认
| RightToLeft = 1 // 从右往左排

let langTextDirectionEnums = [| langTextDirectionEnum.Default; langTextDirectionEnum.RightToLeft |]
let langTextDirectionEnumstrs = [| "langTextDirectionEnum"; "langTextDirectionEnum" |]
let langTextDirectionNum = 2

let int__langTextDirectionEnum v =
    match v with
    | 0 -> Some langTextDirectionEnum.Default
    | 1 -> Some langTextDirectionEnum.RightToLeft
    | _ -> None

let str__langTextDirectionEnum s =
    match s with
    | "Default" -> Some langTextDirectionEnum.Default
    | "RightToLeft" -> Some langTextDirectionEnum.RightToLeft
    | _ -> None

let langTextDirectionEnum__caption e =
    match e with
    | langTextDirectionEnum.Default -> "默认"
    | langTextDirectionEnum.RightToLeft -> "从右往左排"
    | _ -> ""

type pLANG = {
mutable Code2: Chars
mutable Caption: Chars
mutable Native: Chars
mutable Icon: Link
mutable IsBlank: Boolean
mutable IsLocale: Boolean
mutable IsContent: Boolean
mutable IsAutoTranslate: Boolean
mutable TextDirection: langTextDirectionEnum}


type LANG = Rcd<pLANG>

let LANG_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Code2],[Caption],[Native],[Icon],[IsBlank],[IsLocale],[IsContent],[IsAutoTranslate],[TextDirection]"

let pLANG_fieldordersArray = [|
    "Code2"
    "Caption"
    "Native"
    "Icon"
    "IsBlank"
    "IsLocale"
    "IsContent"
    "IsAutoTranslate"
    "TextDirection" |]

let LANG_sql_update = "[Updatedat]=@Updatedat,[Code2]=@Code2,[Caption]=@Caption,[Native]=@Native,[Icon]=@Icon,[IsBlank]=@IsBlank,[IsLocale]=@IsLocale,[IsContent]=@IsContent,[IsAutoTranslate]=@IsAutoTranslate,[TextDirection]=@TextDirection"

let pLANG_fields = [|
    Chars("Code2", 2)
    Chars("Caption", 64)
    Chars("Native", 64)
    Link("Icon", 256)
    Boolean("IsBlank")
    Boolean("IsLocale")
    Boolean("IsContent")
    Boolean("IsAutoTranslate")
    SelectLines("TextDirection", [| ("Default","默认");("RightToLeft","从右往左排") |]) |]

let pLANG_empty(): pLANG = {
    Code2 = ""
    Caption = ""
    Native = ""
    Icon = ""
    IsBlank = true
    IsLocale = true
    IsContent = true
    IsAutoTranslate = true
    TextDirection = EnumOfValue 0 }

let LANG_id = ref 1001L
let LANG_count = ref 0
let LANG_table = "Ca_Lang"

// [Ca_WebCredential] (CWC)

type pCWC = {
mutable Caption: Caption
mutable ExternalId: Integer
mutable Icon: Link
mutable EU: FK
mutable Biz: FK
mutable Json: Text}


type CWC = Rcd<pCWC>

let CWC_fieldorders = "[ID],[Createdat],[Updatedat],[Sort],[Caption],[ExternalId],[Icon],[EU],[Biz],[Json]"

let pCWC_fieldordersArray = [|
    "Caption"
    "ExternalId"
    "Icon"
    "EU"
    "Biz"
    "Json" |]

let CWC_sql_update = "[Updatedat]=@Updatedat,[Caption]=@Caption,[ExternalId]=@ExternalId,[Icon]=@Icon,[EU]=@EU,[Biz]=@Biz,[Json]=@Json"

let pCWC_fields = [|
    Caption("Caption", 64)
    Integer("ExternalId")
    Link("Icon", 256)
    FK("EU")
    FK("Biz")
    Text("Json") |]

let pCWC_empty(): pCWC = {
    Caption = ""
    ExternalId = 0L
    Icon = ""
    EU = 0L
    Biz = 0L
    Json = "" }

let CWC_id = ref 0L
let CWC_count = ref 0
let CWC_table = "Ca_WebCredential"
