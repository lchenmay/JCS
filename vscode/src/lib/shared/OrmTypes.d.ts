declare global {

namespace jcs {

// [Ca_Book] (BOOK)

export type pBOOK = {
[key:string]: any
    Caption: string
    Email: string
    Message: string
}

export type BOOK = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pBOOK
}

// [Ca_EndUser] (EU)


export type pEU = {
[key:string]: any
    Caption: string
    AuthType: number
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
    Desc: string
    Suffix: string
    Size: number
    Thumbnail: array
    Owner: number
}

export type FILE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pFILE
}

// [Social_FileBind] (FBIND)

export type pFBIND = {
[key:string]: any
    File: number
    Moment: number
    Desc: string
}

export type FBIND = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pFBIND
}

// [Social_Moment] (MOMENT)




export type pMOMENT = {
[key:string]: any
    Title: string
    Summary: string
    FullText: string
    Tags: string
    PreviewImgUrl: string
    Link: string
    Type: number
    State: number
    MediaType: number
}

export type MOMENT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pMOMENT
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

// [Sys_PageLog] (PLOG)

export type pPLOG = {
[key:string]: any
    Ip: string
    Request: string
}

export type PLOG = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pPLOG
}

// [Ts_Api] (API)

export type pAPI = {
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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
[key:string]: any
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
