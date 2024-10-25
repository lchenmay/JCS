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

// [Ca_Address] (ADDRESS)

type addressAddressTypeEnum = 
| Default = 0 // 默认
| Biz = 1 // 机构
| EndUser = 2 // 用户

let addressAddressTypeEnums = [| addressAddressTypeEnum.Default; addressAddressTypeEnum.Biz; addressAddressTypeEnum.EndUser |]
let addressAddressTypeEnumstrs = [| "addressAddressTypeEnum"; "addressAddressTypeEnum"; "addressAddressTypeEnum" |]
let addressAddressTypeNum = 3

let int__addressAddressTypeEnum v =
    match v with
    | 0 -> Some addressAddressTypeEnum.Default
    | 1 -> Some addressAddressTypeEnum.Biz
    | 2 -> Some addressAddressTypeEnum.EndUser
    | _ -> None

let str__addressAddressTypeEnum s =
    match s with
    | "Default" -> Some addressAddressTypeEnum.Default
    | "Biz" -> Some addressAddressTypeEnum.Biz
    | "EndUser" -> Some addressAddressTypeEnum.EndUser
    | _ -> None

let addressAddressTypeEnum__caption e =
    match e with
    | addressAddressTypeEnum.Default -> "默认"
    | addressAddressTypeEnum.Biz -> "机构"
    | addressAddressTypeEnum.EndUser -> "用户"
    | _ -> ""

type pADDRESS = {
mutable Caption: Caption
mutable Bind: Integer
mutable AddressType: addressAddressTypeEnum
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

let ADDRESS_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Caption],[Bind],[AddressType],[Line1],[Line2],[State],[County],[Town],[Contact],[Tel],[Email],[Zip],[City],[Country],[Remarks]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "caption","bind","addresstype","line1","line2","state","county","town","contact","tel","email","zip","city","country","remarks" """

let pADDRESS_fieldordersArray = [|
    "Caption"
    "Bind"
    "AddressType"
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

let ADDRESS_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Caption]=@Caption,[Bind]=@Bind,[AddressType]=@AddressType,[Line1]=@Line1,[Line2]=@Line2,[State]=@State,[County]=@County,[Town]=@Town,[Contact]=@Contact,[Tel]=@Tel,[Email]=@Email,[Zip]=@Zip,[City]=@City,[Country]=@Country,[Remarks]=@Remarks"
    | Rdbms.PostgreSql -> "caption=@caption,bind=@bind,addresstype=@addresstype,line1=@line1,line2=@line2,state=@state,county=@county,town=@town,contact=@contact,tel=@tel,email=@email,zip=@zip,city=@city,country=@country,remarks=@remarks"

let pADDRESS_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Caption("Caption", 256)
            Integer("Bind")
            SelectLines("AddressType", [| ("Default","默认");("Biz","机构");("EndUser","用户") |])
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
    | Rdbms.PostgreSql ->
        [|
            Caption("caption", 256)
            Integer("bind")
            SelectLines("addresstype", [| ("Default","默认");("Biz","机构");("EndUser","用户") |])
            Chars("line1", 300)
            Chars("line2", 300)
            Chars("state", 16)
            Chars("county", 16)
            Chars("town", 16)
            Chars("contact", 64)
            Chars("tel", 20)
            Chars("email", 256)
            Chars("zip", 16)
            FK("city")
            FK("country")
            Text("remarks") |]

let pADDRESS_empty(): pADDRESS = {
    Caption = ""
    Bind = 0L
    AddressType = EnumOfValue 0
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
mutable DescTxt: Text
mutable Website: Link
mutable Icon: Link
mutable City: FK
mutable Country: FK
mutable Lang: FK
mutable IsSocialPlatform: Boolean
mutable IsCmsSource: Boolean
mutable IsPayGateway: Boolean}


type BIZ = Rcd<pBIZ>

let BIZ_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption],[Parent],[BasicAcct],[DescTxt],[Website],[Icon],[City],[Country],[Lang],[IsSocialPlatform],[IsCmsSource],[IsPayGateway]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code","caption","parent","basicacct","desctxt","website","icon","city","country","lang","issocialplatform","iscmssource","ispaygateway" """

let pBIZ_fieldordersArray = [|
    "Code"
    "Caption"
    "Parent"
    "BasicAcct"
    "DescTxt"
    "Website"
    "Icon"
    "City"
    "Country"
    "Lang"
    "IsSocialPlatform"
    "IsCmsSource"
    "IsPayGateway" |]

let BIZ_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code]=@Code,[Caption]=@Caption,[Parent]=@Parent,[BasicAcct]=@BasicAcct,[DescTxt]=@DescTxt,[Website]=@Website,[Icon]=@Icon,[City]=@City,[Country]=@Country,[Lang]=@Lang,[IsSocialPlatform]=@IsSocialPlatform,[IsCmsSource]=@IsCmsSource,[IsPayGateway]=@IsPayGateway"
    | Rdbms.PostgreSql -> "code=@code,caption=@caption,parent=@parent,basicacct=@basicacct,desctxt=@desctxt,website=@website,icon=@icon,city=@city,country=@country,lang=@lang,issocialplatform=@issocialplatform,iscmssource=@iscmssource,ispaygateway=@ispaygateway"

let pBIZ_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code", 64)
            Caption("Caption", 256)
            FK("Parent")
            FK("BasicAcct")
            Text("DescTxt")
            Link("Website", 256)
            Link("Icon", 256)
            FK("City")
            FK("Country")
            FK("Lang")
            Boolean("IsSocialPlatform")
            Boolean("IsCmsSource")
            Boolean("IsPayGateway") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code", 64)
            Caption("caption", 256)
            FK("parent")
            FK("basicacct")
            Text("desctxt")
            Link("website", 256)
            Link("icon", 256)
            FK("city")
            FK("country")
            FK("lang")
            Boolean("issocialplatform")
            Boolean("iscmssource")
            Boolean("ispaygateway") |]

let pBIZ_empty(): pBIZ = {
    Code = ""
    Caption = ""
    Parent = 0L
    BasicAcct = 0L
    DescTxt = ""
    Website = ""
    Icon = ""
    City = 0L
    Country = 0L
    Lang = 0L
    IsSocialPlatform = true
    IsCmsSource = true
    IsPayGateway = true }

let BIZ_id = ref 75865464L
let BIZ_count = ref 0
let BIZ_table = "Ca_Biz"

// [Ca_Cat] (CAT)

type catCatStateEnum = 
| Normal = 0 // 正常
| Hidden = 1 // 隐藏
| Obsolete = 2 // 过时

let catCatStateEnums = [| catCatStateEnum.Normal; catCatStateEnum.Hidden; catCatStateEnum.Obsolete |]
let catCatStateEnumstrs = [| "catCatStateEnum"; "catCatStateEnum"; "catCatStateEnum" |]
let catCatStateNum = 3

let int__catCatStateEnum v =
    match v with
    | 0 -> Some catCatStateEnum.Normal
    | 1 -> Some catCatStateEnum.Hidden
    | 2 -> Some catCatStateEnum.Obsolete
    | _ -> None

let str__catCatStateEnum s =
    match s with
    | "Normal" -> Some catCatStateEnum.Normal
    | "Hidden" -> Some catCatStateEnum.Hidden
    | "Obsolete" -> Some catCatStateEnum.Obsolete
    | _ -> None

let catCatStateEnum__caption e =
    match e with
    | catCatStateEnum.Normal -> "正常"
    | catCatStateEnum.Hidden -> "隐藏"
    | catCatStateEnum.Obsolete -> "过时"
    | _ -> ""

type pCAT = {
mutable Caption: Chars
mutable Lang: FK
mutable Zh: FK
mutable Parent: FK
mutable CatState: catCatStateEnum}


type CAT = Rcd<pCAT>

let CAT_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Caption],[Lang],[Zh],[Parent],[CatState]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "caption","lang","zh","parent","catstate" """

let pCAT_fieldordersArray = [|
    "Caption"
    "Lang"
    "Zh"
    "Parent"
    "CatState" |]

let CAT_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Caption]=@Caption,[Lang]=@Lang,[Zh]=@Zh,[Parent]=@Parent,[CatState]=@CatState"
    | Rdbms.PostgreSql -> "caption=@caption,lang=@lang,zh=@zh,parent=@parent,catstate=@catstate"

let pCAT_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Caption", 64)
            FK("Lang")
            FK("Zh")
            FK("Parent")
            SelectLines("CatState", [| ("Normal","正常");("Hidden","隐藏");("Obsolete","过时") |]) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("caption", 64)
            FK("lang")
            FK("zh")
            FK("parent")
            SelectLines("catstate", [| ("Normal","正常");("Hidden","隐藏");("Obsolete","过时") |]) |]

let pCAT_empty(): pCAT = {
    Caption = ""
    Lang = 0L
    Zh = 0L
    Parent = 0L
    CatState = EnumOfValue 0 }

let CAT_id = ref 0L
let CAT_count = ref 0
let CAT_table = "Ca_Cat"

// [Ca_City] (CITY)

type pCITY = {
mutable Fullname: Chars
mutable MetropolitanCode3IATA: Chars
mutable NameEn: Chars
mutable Country: FK
mutable Place: FK
mutable Icon: Link
mutable Tel: Chars}


type CITY = Rcd<pCITY>

let CITY_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Fullname],[MetropolitanCode3IATA],[NameEn],[Country],[Place],[Icon],[Tel]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "fullname","metropolitancode3iata","nameen","country","place","icon","tel" """

let pCITY_fieldordersArray = [|
    "Fullname"
    "MetropolitanCode3IATA"
    "NameEn"
    "Country"
    "Place"
    "Icon"
    "Tel" |]

let CITY_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Fullname]=@Fullname,[MetropolitanCode3IATA]=@MetropolitanCode3IATA,[NameEn]=@NameEn,[Country]=@Country,[Place]=@Place,[Icon]=@Icon,[Tel]=@Tel"
    | Rdbms.PostgreSql -> "fullname=@fullname,metropolitancode3iata=@metropolitancode3iata,nameen=@nameen,country=@country,place=@place,icon=@icon,tel=@tel"

let pCITY_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Fullname", 64)
            Chars("MetropolitanCode3IATA", 3)
            Chars("NameEn", 64)
            FK("Country")
            FK("Place")
            Link("Icon", 256)
            Chars("Tel", 4) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("fullname", 64)
            Chars("metropolitancode3iata", 3)
            Chars("nameen", 64)
            FK("country")
            FK("place")
            Link("icon", 256)
            Chars("tel", 4) |]

let pCITY_empty(): pCITY = {
    Fullname = ""
    MetropolitanCode3IATA = ""
    NameEn = ""
    Country = 0L
    Place = 0L
    Icon = ""
    Tel = "" }

let CITY_id = ref 1001L
let CITY_count = ref 0
let CITY_table = "Ca_City"

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

let CRY_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code2],[Caption],[Fullname],[Icon],[Tel],[Cur],[Capital],[Place],[Lang]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code2","caption","fullname","icon","tel","cur","capital","place","lang" """

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

let CRY_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code2]=@Code2,[Caption]=@Caption,[Fullname]=@Fullname,[Icon]=@Icon,[Tel]=@Tel,[Cur]=@Cur,[Capital]=@Capital,[Place]=@Place,[Lang]=@Lang"
    | Rdbms.PostgreSql -> "code2=@code2,caption=@caption,fullname=@fullname,icon=@icon,tel=@tel,cur=@cur,capital=@capital,place=@place,lang=@lang"

let pCRY_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code2", 2)
            Chars("Caption", 64)
            Chars("Fullname", 256)
            Link("Icon", 256)
            Chars("Tel", 4)
            FK("Cur")
            FK("Capital")
            FK("Place")
            FK("Lang") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code2", 2)
            Chars("caption", 64)
            Chars("fullname", 256)
            Link("icon", 256)
            Chars("tel", 4)
            FK("cur")
            FK("capital")
            FK("place")
            FK("lang") |]

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
mutable Citizen: FK
mutable Refer: Caption
mutable Referer: FK
mutable Url: Text
mutable About: Text}


type EU = Rcd<pEU>

let EU_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Caption],[Username],[SocialAuthBiz],[SocialAuthId],[SocialAuthAvatar],[Email],[Tel],[Gender],[Status],[Admin],[BizPartner],[Privilege],[Verify],[Pwd],[Online],[Icon],[Background],[BasicAcct],[Citizen],[Refer],[Referer],[Url],[About]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "caption","username","socialauthbiz","socialauthid","socialauthavatar","email","tel","gender","status","admin","bizpartner","privilege","verify","pwd","online","icon","background","basicacct","citizen","refer","referer","url","about" """

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
    "Citizen"
    "Refer"
    "Referer"
    "Url"
    "About" |]

let EU_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Caption]=@Caption,[Username]=@Username,[SocialAuthBiz]=@SocialAuthBiz,[SocialAuthId]=@SocialAuthId,[SocialAuthAvatar]=@SocialAuthAvatar,[Email]=@Email,[Tel]=@Tel,[Gender]=@Gender,[Status]=@Status,[Admin]=@Admin,[BizPartner]=@BizPartner,[Privilege]=@Privilege,[Verify]=@Verify,[Pwd]=@Pwd,[Online]=@Online,[Icon]=@Icon,[Background]=@Background,[BasicAcct]=@BasicAcct,[Citizen]=@Citizen,[Refer]=@Refer,[Referer]=@Referer,[Url]=@Url,[About]=@About"
    | Rdbms.PostgreSql -> "caption=@caption,username=@username,socialauthbiz=@socialauthbiz,socialauthid=@socialauthid,socialauthavatar=@socialauthavatar,email=@email,tel=@tel,gender=@gender,status=@status,admin=@admin,bizpartner=@bizpartner,privilege=@privilege,verify=@verify,pwd=@pwd,online=@online,icon=@icon,background=@background,basicacct=@basicacct,citizen=@citizen,refer=@refer,referer=@referer,url=@url,about=@about"

let pEU_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
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
            FK("Citizen")
            Caption("Refer", 9)
            FK("Referer")
            Text("Url")
            Text("About") |]
    | Rdbms.PostgreSql ->
        [|
            Caption("caption", 64)
            Caption("username", 64)
            FK("socialauthbiz")
            Text("socialauthid")
            Text("socialauthavatar")
            Chars("email", 256)
            Chars("tel", 32)
            SelectLines("gender", [| ("Unknown","未知");("Male","男");("Female","女") |])
            SelectLines("status", [| ("Normal","正常");("Frozen","冻结");("Terminated","注销") |])
            SelectLines("admin", [| ("None","无");("Admin","管理员") |])
            SelectLines("bizpartner", [| ("None","None");("Partner","") |])
            Integer("privilege")
            SelectLines("verify", [| ("Normal","常规");("Verified","认证") |])
            Chars("pwd", 16)
            Boolean("online")
            Link("icon", 256)
            Link("background", 256)
            FK("basicacct")
            FK("citizen")
            Caption("refer", 9)
            FK("referer")
            Text("url")
            Text("about") |]

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
    Citizen = 0L
    Refer = ""
    Referer = 0L
    Url = ""
    About = "" }

let EU_id = ref 54367576L
let EU_count = ref 0
let EU_table = "Ca_EndUser"

// [Ca_SpecialItem] (CSI)

type csiTypeEnum = 
| Normal = 0 // 常规
| ToplinesGlobalNews = 1 // 全站新闻置顶
| ToplinesGlobalPerson = 2 // 全站人物置顶
| ToplinesGlobalEvent = 3 // 全站事件置顶

let csiTypeEnums = [| csiTypeEnum.Normal; csiTypeEnum.ToplinesGlobalNews; csiTypeEnum.ToplinesGlobalPerson; csiTypeEnum.ToplinesGlobalEvent |]
let csiTypeEnumstrs = [| "csiTypeEnum"; "csiTypeEnum"; "csiTypeEnum"; "csiTypeEnum" |]
let csiTypeNum = 4

let int__csiTypeEnum v =
    match v with
    | 0 -> Some csiTypeEnum.Normal
    | 1 -> Some csiTypeEnum.ToplinesGlobalNews
    | 2 -> Some csiTypeEnum.ToplinesGlobalPerson
    | 3 -> Some csiTypeEnum.ToplinesGlobalEvent
    | _ -> None

let str__csiTypeEnum s =
    match s with
    | "Normal" -> Some csiTypeEnum.Normal
    | "ToplinesGlobalNews" -> Some csiTypeEnum.ToplinesGlobalNews
    | "ToplinesGlobalPerson" -> Some csiTypeEnum.ToplinesGlobalPerson
    | "ToplinesGlobalEvent" -> Some csiTypeEnum.ToplinesGlobalEvent
    | _ -> None

let csiTypeEnum__caption e =
    match e with
    | csiTypeEnum.Normal -> "常规"
    | csiTypeEnum.ToplinesGlobalNews -> "全站新闻置顶"
    | csiTypeEnum.ToplinesGlobalPerson -> "全站人物置顶"
    | csiTypeEnum.ToplinesGlobalEvent -> "全站事件置顶"
    | _ -> ""

type pCSI = {
mutable Type: csiTypeEnum
mutable Lang: FK
mutable Bind: Integer}


type CSI = Rcd<pCSI>

let CSI_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Type],[Lang],[Bind]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "type","lang","bind" """

let pCSI_fieldordersArray = [|
    "Type"
    "Lang"
    "Bind" |]

let CSI_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Type]=@Type,[Lang]=@Lang,[Bind]=@Bind"
    | Rdbms.PostgreSql -> "type=@type,lang=@lang,bind=@bind"

let pCSI_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            SelectLines("Type", [| ("Normal","常规");("ToplinesGlobalNews","全站新闻置顶");("ToplinesGlobalPerson","全站人物置顶");("ToplinesGlobalEvent","全站事件置顶") |])
            FK("Lang")
            Integer("Bind") |]
    | Rdbms.PostgreSql ->
        [|
            SelectLines("type", [| ("Normal","常规");("ToplinesGlobalNews","全站新闻置顶");("ToplinesGlobalPerson","全站人物置顶");("ToplinesGlobalEvent","全站事件置顶") |])
            FK("lang")
            Integer("bind") |]

let pCSI_empty(): pCSI = {
    Type = EnumOfValue 0
    Lang = 0L
    Bind = 0L }

let CSI_id = ref 0L
let CSI_count = ref 0
let CSI_table = "Ca_SpecialItem"

// [Ca_WebCredential] (CWC)

type pCWC = {
mutable Caption: Caption
mutable ExternalId: Integer
mutable Icon: Link
mutable EU: FK
mutable Biz: FK
mutable Json: Text}


type CWC = Rcd<pCWC>

let CWC_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Caption],[ExternalId],[Icon],[EU],[Biz],[Json]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "caption","externalid","icon","eu","biz","json" """

let pCWC_fieldordersArray = [|
    "Caption"
    "ExternalId"
    "Icon"
    "EU"
    "Biz"
    "Json" |]

let CWC_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Caption]=@Caption,[ExternalId]=@ExternalId,[Icon]=@Icon,[EU]=@EU,[Biz]=@Biz,[Json]=@Json"
    | Rdbms.PostgreSql -> "caption=@caption,externalid=@externalid,icon=@icon,eu=@eu,biz=@biz,json=@json"

let pCWC_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Caption("Caption", 64)
            Integer("ExternalId")
            Link("Icon", 256)
            FK("EU")
            FK("Biz")
            Text("Json") |]
    | Rdbms.PostgreSql ->
        [|
            Caption("caption", 64)
            Integer("externalid")
            Link("icon", 256)
            FK("eu")
            FK("biz")
            Text("json") |]

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

type fieldFieldTypeEnum = 
| Undefined = 0 // Undefined
| FK = 1 // FK
| Caption = 2 // Caption
| Chars = 3 // Chars
| Link = 4 // Link
| Text = 5 // Text
| Bin = 6 // Bin
| Integer = 7 // Integer
| Float = 8 // Float
| Boolean = 9 // Boolean
| SelectLines = 10 // Select Lines
| Timestamp = 11 // Time Stamp
| TimeSeries = 12 // Time Series

let fieldFieldTypeEnums = [| fieldFieldTypeEnum.Undefined; fieldFieldTypeEnum.FK; fieldFieldTypeEnum.Caption; fieldFieldTypeEnum.Chars; fieldFieldTypeEnum.Link; fieldFieldTypeEnum.Text; fieldFieldTypeEnum.Bin; fieldFieldTypeEnum.Integer; fieldFieldTypeEnum.Float; fieldFieldTypeEnum.Boolean; fieldFieldTypeEnum.SelectLines; fieldFieldTypeEnum.Timestamp; fieldFieldTypeEnum.TimeSeries |]
let fieldFieldTypeEnumstrs = [| "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum"; "fieldFieldTypeEnum" |]
let fieldFieldTypeNum = 13

let int__fieldFieldTypeEnum v =
    match v with
    | 0 -> Some fieldFieldTypeEnum.Undefined
    | 1 -> Some fieldFieldTypeEnum.FK
    | 2 -> Some fieldFieldTypeEnum.Caption
    | 3 -> Some fieldFieldTypeEnum.Chars
    | 4 -> Some fieldFieldTypeEnum.Link
    | 5 -> Some fieldFieldTypeEnum.Text
    | 6 -> Some fieldFieldTypeEnum.Bin
    | 7 -> Some fieldFieldTypeEnum.Integer
    | 8 -> Some fieldFieldTypeEnum.Float
    | 9 -> Some fieldFieldTypeEnum.Boolean
    | 10 -> Some fieldFieldTypeEnum.SelectLines
    | 11 -> Some fieldFieldTypeEnum.Timestamp
    | 12 -> Some fieldFieldTypeEnum.TimeSeries
    | _ -> None

let str__fieldFieldTypeEnum s =
    match s with
    | "Undefined" -> Some fieldFieldTypeEnum.Undefined
    | "FK" -> Some fieldFieldTypeEnum.FK
    | "Caption" -> Some fieldFieldTypeEnum.Caption
    | "Chars" -> Some fieldFieldTypeEnum.Chars
    | "Link" -> Some fieldFieldTypeEnum.Link
    | "Text" -> Some fieldFieldTypeEnum.Text
    | "Bin" -> Some fieldFieldTypeEnum.Bin
    | "Integer" -> Some fieldFieldTypeEnum.Integer
    | "Float" -> Some fieldFieldTypeEnum.Float
    | "Boolean" -> Some fieldFieldTypeEnum.Boolean
    | "SelectLines" -> Some fieldFieldTypeEnum.SelectLines
    | "Timestamp" -> Some fieldFieldTypeEnum.Timestamp
    | "TimeSeries" -> Some fieldFieldTypeEnum.TimeSeries
    | _ -> None

let fieldFieldTypeEnum__caption e =
    match e with
    | fieldFieldTypeEnum.Undefined -> "Undefined"
    | fieldFieldTypeEnum.FK -> "FK"
    | fieldFieldTypeEnum.Caption -> "Caption"
    | fieldFieldTypeEnum.Chars -> "Chars"
    | fieldFieldTypeEnum.Link -> "Link"
    | fieldFieldTypeEnum.Text -> "Text"
    | fieldFieldTypeEnum.Bin -> "Bin"
    | fieldFieldTypeEnum.Integer -> "Integer"
    | fieldFieldTypeEnum.Float -> "Float"
    | fieldFieldTypeEnum.Boolean -> "Boolean"
    | fieldFieldTypeEnum.SelectLines -> "Select Lines"
    | fieldFieldTypeEnum.Timestamp -> "Time Stamp"
    | fieldFieldTypeEnum.TimeSeries -> "Time Series"
    | _ -> ""

type pFIELD = {
mutable Name: Chars
mutable Desc: Text
mutable FieldType: fieldFieldTypeEnum
mutable Length: Integer
mutable SelectLines: Text
mutable Project: FK
mutable Table: FK}


type FIELD = Rcd<pFIELD>

let FIELD_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Desc],[FieldType],[Length],[SelectLines],[Project],[Table]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","desc","fieldtype","length","selectlines","project","table" """

let pFIELD_fieldordersArray = [|
    "Name"
    "Desc"
    "FieldType"
    "Length"
    "SelectLines"
    "Project"
    "Table" |]

let FIELD_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Desc]=@Desc,[FieldType]=@FieldType,[Length]=@Length,[SelectLines]=@SelectLines,[Project]=@Project,[Table]=@Table"
    | Rdbms.PostgreSql -> "name=@name,desc=@desc,fieldtype=@fieldtype,length=@length,selectlines=@selectlines,project=@project,table=@table"

let pFIELD_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Text("Desc")
            SelectLines("FieldType", [| ("Undefined","Undefined");("FK","FK");("Caption","Caption");("Chars","Chars");("Link","Link");("Text","Text");("Bin","Bin");("Integer","Integer");("Float","Float");("Boolean","Boolean");("SelectLines","Select Lines");("Timestamp","Time Stamp");("TimeSeries","Time Series") |])
            Integer("Length")
            Text("SelectLines")
            FK("Project")
            FK("Table") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Text("desc")
            SelectLines("fieldtype", [| ("Undefined","Undefined");("FK","FK");("Caption","Caption");("Chars","Chars");("Link","Link");("Text","Text");("Bin","Bin");("Integer","Integer");("Float","Float");("Boolean","Boolean");("SelectLines","Select Lines");("Timestamp","Time Stamp");("TimeSeries","Time Series") |])
            Integer("length")
            Text("selectlines")
            FK("project")
            FK("table") |]

let pFIELD_empty(): pFIELD = {
    Name = ""
    Desc = ""
    FieldType = EnumOfValue 0
    Length = 0L
    SelectLines = ""
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
mutable DirVs: Chars
mutable DirVsCodeWeb: Chars
mutable Project: FK}


type HOSTCONFIG = Rcd<pHOSTCONFIG>

let HOSTCONFIG_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Hostname],[Database],[DatabaseName],[DatabaseConn],[DirVs],[DirVsCodeWeb],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "hostname","database","databasename","databaseconn","dirvs","dirvscodeweb","project" """

let pHOSTCONFIG_fieldordersArray = [|
    "Hostname"
    "Database"
    "DatabaseName"
    "DatabaseConn"
    "DirVs"
    "DirVsCodeWeb"
    "Project" |]

let HOSTCONFIG_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Hostname]=@Hostname,[Database]=@Database,[DatabaseName]=@DatabaseName,[DatabaseConn]=@DatabaseConn,[DirVs]=@DirVs,[DirVsCodeWeb]=@DirVsCodeWeb,[Project]=@Project"
    | Rdbms.PostgreSql -> "hostname=@hostname,database=@database,databasename=@databasename,databaseconn=@databaseconn,dirvs=@dirvs,dirvscodeweb=@dirvscodeweb,project=@project"

let pHOSTCONFIG_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Hostname", 64)
            SelectLines("Database", [| ("SQLSERVER","SQL Server");("PostgreSQL","PostgreSQL") |])
            Chars("DatabaseName", 64)
            Chars("DatabaseConn", 64)
            Chars("DirVs", 64)
            Chars("DirVsCodeWeb", 64)
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("hostname", 64)
            SelectLines("database", [| ("SQLSERVER","SQL Server");("PostgreSQL","PostgreSQL") |])
            Chars("databasename", 64)
            Chars("databaseconn", 64)
            Chars("dirvs", 64)
            Chars("dirvscodeweb", 64)
            FK("project") |]

let pHOSTCONFIG_empty(): pHOSTCONFIG = {
    Hostname = ""
    Database = EnumOfValue 0
    DatabaseName = ""
    DatabaseConn = ""
    DirVs = ""
    DirVsCodeWeb = ""
    Project = 0L }

let HOSTCONFIG_id = ref 34512L
let HOSTCONFIG_count = ref 0
let HOSTCONFIG_table = "Ts_HostConfig"

// [Ts_Project] (PROJECT)

type pPROJECT = {
mutable Code: Chars
mutable Caption: Chars
mutable TypeSessionUser: Chars}


type PROJECT = Rcd<pPROJECT>

let PROJECT_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Code],[Caption],[TypeSessionUser]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "code","caption","typesessionuser" """

let pPROJECT_fieldordersArray = [|
    "Code"
    "Caption"
    "TypeSessionUser" |]

let PROJECT_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Code]=@Code,[Caption]=@Caption,[TypeSessionUser]=@TypeSessionUser"
    | Rdbms.PostgreSql -> "code=@code,caption=@caption,typesessionuser=@typesessionuser"

let pPROJECT_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Code", 64)
            Chars("Caption", 256)
            Chars("TypeSessionUser", 64) |]
    | Rdbms.PostgreSql ->
        [|
            Chars("code", 64)
            Chars("caption", 256)
            Chars("typesessionuser", 64) |]

let pPROJECT_empty(): pPROJECT = {
    Code = ""
    Caption = ""
    TypeSessionUser = "" }

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
mutable Val: Text
mutable BindType: vartypeBindTypeEnum
mutable Bind: Integer
mutable Project: FK}


type VARTYPE = Rcd<pVARTYPE>

let VARTYPE_fieldorders() =
    match rdbms with
    | Rdbms.SqlServer ->
        "[ID],[Createdat],[Updatedat],[Sort],[Name],[Type],[Val],[BindType],[Bind],[Project]"
    | Rdbms.PostgreSql ->
        $""" "id","createdat","updatedat","sort", "name","type","val","bindtype","bind","project" """

let pVARTYPE_fieldordersArray = [|
    "Name"
    "Type"
    "Val"
    "BindType"
    "Bind"
    "Project" |]

let VARTYPE_sql_update() =
    match rdbms with
    | Rdbms.SqlServer -> "[Name]=@Name,[Type]=@Type,[Val]=@Val,[BindType]=@BindType,[Bind]=@Bind,[Project]=@Project"
    | Rdbms.PostgreSql -> "name=@name,type=@type,val=@val,bindtype=@bindtype,bind=@bind,project=@project"

let pVARTYPE_fields() =
    match rdbms with
    | Rdbms.SqlServer ->
        [|
            Chars("Name", 64)
            Chars("Type", 64)
            Text("Val")
            SelectLines("BindType", [| ("ApiRequest","API Request");("ApiResponse","API Response");("CompState","Component State");("CompProps","Component Propos");("PageState","Page State");("PageProps","Page Propos") |])
            Integer("Bind")
            FK("Project") |]
    | Rdbms.PostgreSql ->
        [|
            Chars("name", 64)
            Chars("type", 64)
            Text("val")
            SelectLines("bindtype", [| ("ApiRequest","API Request");("ApiResponse","API Response");("CompState","Component State");("CompProps","Component Propos");("PageState","Page State");("PageProps","Page Propos") |])
            Integer("bind")
            FK("project") |]

let pVARTYPE_empty(): pVARTYPE = {
    Name = ""
    Type = ""
    Val = ""
    BindType = EnumOfValue 0
    Bind = 0L
    Project = 0L }

let VARTYPE_id = ref 7523431L
let VARTYPE_count = ref 0
let VARTYPE_table = "Ts_VarType"
