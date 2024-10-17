// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
const marshall = {...binCommon }


// [PROJ] Structure


export const pPROJ__bin = (bb:BytesBuilder) => (p:jcs.pPROJ) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
}

export const PROJ__bin = (bb:BytesBuilder) => (v:jcs.PROJ) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pPROJ__bin (bb) (v.p)
}

export const bin__pPROJ = (bi:BinIndexed):jcs.pPROJ => {

    let p = pPROJ_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)

    return p
}


export const bin__PROJ = (bi:BinIndexed):jcs.PROJ => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pPROJ (bi)
    }
}
export const pPROJ_empty = (): jcs.pPROJ => {
    return {
        Code: "",
        Caption: "" }
}

export const PROJ_empty = (): jcs.PROJ => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pPROJ_empty() }
}
