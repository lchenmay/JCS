declare global {

namespace jcs {

// [Ca_Address] (ADDRESS)

const addressAddressTypeEnum_Default = 0 // 默认
const addressAddressTypeEnum_Biz = 1 // 机构
const addressAddressTypeEnum_EndUser = 2 // 用户

export type pADDRESS = {
    Caption: string
    Bind: number
    AddressType: number
    Line1: string
    Line2: string
    State: string
    County: string
    Town: string
    Contact: string
    Tel: string
    Email: string
    Zip: string
    City: number
    Country: number
    Remarks: string
}

export type ADDRESS = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pADDRESS
}

// [Ca_Biz] (BIZ)

export type pBIZ = {
    Code: string
    Caption: string
    Parent: number
    BasicAcct: number
    DescTxt: string
    Website: string
    Icon: string
    City: number
    Country: number
    Lang: number
    IsSocialPlatform: boolean
    IsCmsSource: boolean
    IsPayGateway: boolean
}

export type BIZ = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pBIZ
}

// [Ca_Cat] (CAT)

const catCatStateEnum_Normal = 0 // 正常
const catCatStateEnum_Hidden = 1 // 隐藏
const catCatStateEnum_Obsolete = 2 // 过时

export type pCAT = {
    Caption: string
    Lang: number
    Zh: number
    Parent: number
    CatState: number
}

export type CAT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCAT
}

// [Ca_City] (CITY)

export type pCITY = {
    Fullname: string
    MetropolitanCode3IATA: string
    NameEn: string
    Country: number
    Place: number
    Icon: string
    Tel: string
}

export type CITY = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCITY
}

// [Ca_Country] (CRY)

export type pCRY = {
    Code2: string
    Caption: string
    Fullname: string
    Icon: string
    Tel: string
    Cur: number
    Capital: number
    Place: number
    Lang: number
}

export type CRY = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCRY
}

// [Ca_EndUser] (EU)

const euGenderEnum_Unknown = 0 // 未知
const euGenderEnum_Male = 1 // 男
const euGenderEnum_Female = 2 // 女

const euStatusEnum_Normal = 0 // 正常
const euStatusEnum_Frozen = 1 // 冻结
const euStatusEnum_Terminated = 2 // 注销

const euAdminEnum_None = 0 // 无
const euAdminEnum_Admin = 1 // 管理员

const euBizPartnerEnum_None = 0 // None
const euBizPartnerEnum_Partner = 1 // 

const euVerifyEnum_Normal = 0 // 常规
const euVerifyEnum_Verified = 1 // 认证

export type pEU = {
    Caption: string
    Username: string
    SocialAuthBiz: number
    SocialAuthId: string
    SocialAuthAvatar: string
    Email: string
    Tel: string
    Gender: number
    Status: number
    Admin: number
    BizPartner: number
    Privilege: number
    Verify: number
    Pwd: string
    Online: boolean
    Icon: string
    Background: string
    BasicAcct: number
    Citizen: number
    Refer: string
    Referer: number
    Url: string
    About: string
}

export type EU = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pEU
}

// [Ca_SpecialItem] (CSI)

const csiTypeEnum_Normal = 0 // 常规
const csiTypeEnum_ToplinesGlobalNews = 1 // 全站新闻置顶
const csiTypeEnum_ToplinesGlobalPerson = 2 // 全站人物置顶
const csiTypeEnum_ToplinesGlobalEvent = 3 // 全站事件置顶

export type pCSI = {
    Type: number
    Lang: number
    Bind: number
}

export type CSI = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCSI
}

// [Ca_WebCredential] (CWC)

export type pCWC = {
    Caption: string
    ExternalId: number
    Icon: string
    EU: number
    Biz: number
    Json: string
}

export type CWC = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCWC
}

// [Ts_Api] (API)

export type pAPI = {
    Name: string
    Project: number
}

export type API = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pAPI
}

// [Ts_Field] (FIELD)

const fieldFieldTypeEnum_Undefined = 0 // Undefined
const fieldFieldTypeEnum_FK = 1 // FK
const fieldFieldTypeEnum_Caption = 2 // Caption
const fieldFieldTypeEnum_Chars = 3 // Chars
const fieldFieldTypeEnum_Link = 4 // Link
const fieldFieldTypeEnum_Text = 5 // Text
const fieldFieldTypeEnum_Bin = 6 // Bin
const fieldFieldTypeEnum_Integer = 7 // Integer
const fieldFieldTypeEnum_Float = 8 // Float
const fieldFieldTypeEnum_Boolean = 9 // Boolean
const fieldFieldTypeEnum_SelectLines = 10 // Select Lines
const fieldFieldTypeEnum_Timestamp = 11 // Time Stamp
const fieldFieldTypeEnum_TimeSeries = 12 // Time Series

export type pFIELD = {
    Name: string
    Desc: string
    FieldType: number
    Length: number
    SelectLines: string
    Project: number
    Table: number
}

export type FIELD = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pFIELD
}

// [Ts_HostConfig] (HOSTCONFIG)

const hostconfigDatabaseEnum_SQLSERVER = 0 // SQL Server
const hostconfigDatabaseEnum_PostgreSQL = 1 // PostgreSQL

export type pHOSTCONFIG = {
    Hostname: string
    Database: number
    DatabaseName: string
    DatabaseConn: string
    DirVs: string
    DirVsCodeWeb: string
    Project: number
}

export type HOSTCONFIG = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pHOSTCONFIG
}

// [Ts_Project] (PROJECT)

export type pPROJECT = {
    Code: string
    Caption: string
    TypeSessionUser: string
}

export type PROJECT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pPROJECT
}

// [Ts_Table] (TABLE)

export type pTABLE = {
    Name: string
    Desc: string
    Project: number
}

export type TABLE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pTABLE
}

// [Ts_UiComponent] (COMP)

export type pCOMP = {
    Name: string
    Caption: string
    Project: number
}

export type COMP = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCOMP
}

// [Ts_UiPage] (PAGE)

export type pPAGE = {
    Name: string
    Caption: string
    Route: string
    OgTitle: string
    OgDesc: string
    OgImage: string
    Template: number
    Project: number
}

export type PAGE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pPAGE
}

// [Ts_UiTemplate] (TEMPLATE)

export type pTEMPLATE = {
    Name: string
    Caption: string
    Project: number
}

export type TEMPLATE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pTEMPLATE
}

// [Ts_VarType] (VARTYPE)

const vartypeBindTypeEnum_ApiRequest = 0 // API Request
const vartypeBindTypeEnum_ApiResponse = 1 // API Response
const vartypeBindTypeEnum_CompState = 2 // Component State
const vartypeBindTypeEnum_CompProps = 3 // Component Propos
const vartypeBindTypeEnum_PageState = 4 // Page State
const vartypeBindTypeEnum_PageProps = 5 // Page Propos

export type pVARTYPE = {
    Name: string
    Type: string
    Val: string
    BindType: number
    Bind: number
    Project: number
}

export type VARTYPE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pVARTYPE
}


}

}

export {}
