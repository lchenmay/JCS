declare global {

namespace jcs {

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

const hostconfigGenderEnum_SQLSERVER = 0 // SQL Server
const hostconfigGenderEnum_PostgreSQL = 1 // PostgreSQL

export type pHOSTCONFIG = {
    Hostname: string
    Gender: number
    DatabaseName: string
    DatabaseConn: string
    DirVsShared: string
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
