declare global {

namespace j7 {

// [Ca_Address] (ADDRESS)


export type pADDRESS = {
[key:string]: any
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
[key:string]: any
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

// [Ca_Cat] (CAT)


export type pCAT = {
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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


export type pCUR = {
[key:string]: any
    Code: string
    Caption: string
    IsHidden: boolean
    IsSac: boolean
    IsTransfer: boolean
    IsCash: boolean
    EnableReward: boolean
    EnableOTC: boolean
    Icon: string
    CurType: number
    FloatDec: number
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






export type pEU = {
[key:string]: any
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

// [Ca_File] (FILE)





export type pFILE = {
[key:string]: any
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



export type pFOLDER = {
[key:string]: any
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


export type pLANG = {
[key:string]: any
    Code2: string
    Caption: string
    Native: string
    Icon: string
    IsBlank: boolean
    IsLocale: boolean
    IsContent: boolean
    IsAutoTranslate: boolean
    IsMiles: boolean
    TextDirection: number
}

export type LANG = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pLANG
}

// [Ca_Locale] (LOCALE)

export type pLOCALE = {
[key:string]: any
    Code: string
    Lang: number
    Zh: string
    Text: string
}

export type LOCALE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pLOCALE
}

// [Ca_SpecialItem] (CSI)


export type pCSI = {
[key:string]: any
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
[key:string]: any
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

// [Market_Instrument] (INS)

export type pINS = {
[key:string]: any
    DescTxt: string
    Code: string
    Caption: string
    Long: number
    LongCode: string
    Short: number
    ShortCode: string
    Digit: number
    Ask: number
    Bid: number
    PriceCurrent: number
}

export type INS = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pINS
}

// [Market_Ticket] (TICKET)



export type pTICKET = {
[key:string]: any
    Instrument: number
    InsCode: string
    TicketNum: string
    State: number
    Lot: number
    OpenPrice: number
    ClosePrice: number
    SL: number
    TP: number
    CloseType: number
    Cmt: string
    Pendingat: Date
    Opendat: Date
    Closedat: Date
    Canceledat: Date
}

export type TICKET = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pTICKET
}

// [Market_TradeAcct] (TACCT)




export type pTACCT = {
[key:string]: any
    SAC: number
    State: number
    TradeType: number
    RealDemo: number
    PnL: number
    Frozen: number
    Leverage: number
    Margin: number
    MarginCallRateWarning: number
    MarginCallRateWarningII: number
    MarginCallRateLiq: number
    PwdTrade: string
    PwdReadonly: string
    DescTxt: string
}

export type TACCT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pTACCT
}

// [Sys_Log] (LOG)

export type pLOG = {
[key:string]: any
    Location: string
    Content: string
    Sql: string
}

export type LOG = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pLOG
}


}

}

export {}
