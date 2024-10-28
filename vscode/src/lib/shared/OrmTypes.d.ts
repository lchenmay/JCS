declare global {

namespace jcs {

// [Ca_Address] (ADDRESS)


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
