declare global {

namespace jcs {

// [Ca_Address] (ADDRESS)

const addressTypeEnum_Default = 0 // 默认
const addressTypeEnum_Biz = 1 // 机构
const addressTypeEnum_EndUser = 2 // 用户

export type pADDRESS = {
    Caption: string
    Bind: number
    Type: number
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
    Desc: string
    Website: string
    Icon: string
    City: number
    Country: number
    Lang: number
    IsSocial: boolean
    IsCmsSource: boolean
    IsPay: boolean
    MomentLatest: number
    CountFollowers: number
    CountFollows: number
    CountMoments: number
    CountViews: number
    CountComments: number
    CountThumbUps: number
    CountThumbDns: number
    IsCrawling: boolean
    IsGSeries: boolean
    RemarksCentral: string
    Agent: number
    SiteCats: string
    ConfiguredCats: string
}

export type BIZ = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pBIZ
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

// [Ca_Cur] (CUR)

const curCurTypeEnum_Legal = 0 // 法币
const curCurTypeEnum_Crypto = 1 // 数字币

export type pCUR = {
    Code: string
    Caption: string
    Hidden: boolean
    IsSac: boolean
    IsTransfer: boolean
    IsCash: boolean
    EnableReward: boolean
    EnableOTC: boolean
    Icon: string
    CurType: number
    Dec: number
    AnchorRate: number
    Freezable: boolean
    Authorizable: boolean
    ChaninID: string
    ChaninName: string
    ContractAddress: string
    WalletAddress: string
    BaseRate: number
}

export type CUR = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCUR
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

// [Ca_File] (FILE)

const fileEncryptEnum_None = 0 // 未加密
const fileEncryptEnum_Sys = 1 // 系统加密
const fileEncryptEnum_Usr = 2 // 用户加密

const fileBindTypeEnum_Sys = 0 // 系统
const fileBindTypeEnum_EndUser = 1 // 用户
const fileBindTypeEnum_Biz = 2 // 机构
const fileBindTypeEnum_Group = 3 // 群

const fileStateEnum_Normal = 0 // 正常
const fileStateEnum_Canceled = 1 // 已取消
const fileStateEnum_Uploading = 2 // 上传中
const fileStateEnum_PendingTranscode = 3 // 待媒体转码
const fileStateEnum_PendingBlockchain = 4 // 待区块链同步
const fileStateEnum_Deleted = 5 // 已删除

const fileFileTypeEnum_Default = 0 // 默认
const fileFileTypeEnum_Text = 1 // 文本
const fileFileTypeEnum_Bin = 2 // 二进制
const fileFileTypeEnum_Img = 3 // 图片
const fileFileTypeEnum_Video = 4 // 视频

export type pFILE = {
    Caption: string
    Encrypt: number
    SHA256: string
    Size: number
    Bind: number
    BindType: number
    State: number
    Folder: number
    FileType: number
    JSON: string
}

export type FILE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pFILE
}

// [Ca_Folder] (FOLDER)

const folderEncryptEnum_None = 0 // 未加密
const folderEncryptEnum_Sys = 1 // 系统加密
const folderEncryptEnum_Usr = 2 // 用户加密

const folderBindTypeEnum_Sys = 0 // 系统
const folderBindTypeEnum_EndUser = 1 // 用户
const folderBindTypeEnum_Biz = 2 // 机构
const folderBindTypeEnum_Group = 3 // 群

export type pFOLDER = {
    Caption: string
    Encrypt: number
    Bind: number
    BindType: number
    Parent: number
}

export type FOLDER = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pFOLDER
}

// [Ca_Lang] (LANG)

const langTextDirectionEnum_Default = 0 // 默认
const langTextDirectionEnum_RightToLeft = 1 // 从右往左排

export type pLANG = {
    Code2: string
    Caption: string
    Native: string
    Icon: string
    IsBlank: boolean
    IsLocale: boolean
    IsContent: boolean
    IsAutoTranslate: boolean
    TextDirection: number
}

export type LANG = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pLANG
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


}

}

export {}
