declare global {

namespace jcs {

// [Ts_Field] (FIELD)

export type pFIELD = {
    Name: string
    Desc: string
}

export type FIELD = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pFIELD
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
    Code: string
    Caption: string
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
    Code: string
    Caption: string
}

export type PAGE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pPAGE
}


}

}

export {}
