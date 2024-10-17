declare global {

namespace jcs {

// [Ts_Project] (PROJ)

export type pPROJ = {
    Code: string
    Caption: string
}

export type PROJ = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pPROJ
}


}

}

export {}
