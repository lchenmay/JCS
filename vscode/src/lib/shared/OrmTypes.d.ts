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
