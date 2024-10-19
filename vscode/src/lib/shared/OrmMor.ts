// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
const marshall = {...binCommon }


// [FIELD] Structure


export const pFIELD__bin = (bb:BytesBuilder) => (p:jcs.pFIELD) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.str__bin (bb) (p.Desc)
    
    marshall.int64__bin (bb) (p.Project)
    
    marshall.int64__bin (bb) (p.Table)
}

export const FIELD__bin = (bb:BytesBuilder) => (v:jcs.FIELD) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pFIELD__bin (bb) (v.p)
}

export const bin__pFIELD = (bi:BinIndexed):jcs.pFIELD => {

    let p = pFIELD_empty()
    p.Name = marshall.bin__str (bi)
    p.Desc = marshall.bin__str (bi)
    p.Project = marshall.bin__int64 (bi)
    p.Table = marshall.bin__int64 (bi)

    return p
}


export const bin__FIELD = (bi:BinIndexed):jcs.FIELD => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pFIELD (bi)
    }
}

// [HOSTCONFIG] Structure


export const pHOSTCONFIG__bin = (bb:BytesBuilder) => (p:jcs.pHOSTCONFIG) => {

    
    marshall.str__bin (bb) (p.Hostname)
    
    marshall.str__bin (bb) (p.DatabaseName)
    
    marshall.str__bin (bb) (p.DatabaseConn)
    
    marshall.str__bin (bb) (p.DirVsShared)
    
    marshall.str__bin (bb) (p.DirVsCodeWeb)
    
    marshall.int64__bin (bb) (p.Project)
}

export const HOSTCONFIG__bin = (bb:BytesBuilder) => (v:jcs.HOSTCONFIG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pHOSTCONFIG__bin (bb) (v.p)
}

export const bin__pHOSTCONFIG = (bi:BinIndexed):jcs.pHOSTCONFIG => {

    let p = pHOSTCONFIG_empty()
    p.Hostname = marshall.bin__str (bi)
    p.DatabaseName = marshall.bin__str (bi)
    p.DatabaseConn = marshall.bin__str (bi)
    p.DirVsShared = marshall.bin__str (bi)
    p.DirVsCodeWeb = marshall.bin__str (bi)
    p.Project = marshall.bin__int64 (bi)

    return p
}


export const bin__HOSTCONFIG = (bi:BinIndexed):jcs.HOSTCONFIG => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pHOSTCONFIG (bi)
    }
}

// [PROJECT] Structure


export const pPROJECT__bin = (bb:BytesBuilder) => (p:jcs.pPROJECT) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
}

export const PROJECT__bin = (bb:BytesBuilder) => (v:jcs.PROJECT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pPROJECT__bin (bb) (v.p)
}

export const bin__pPROJECT = (bi:BinIndexed):jcs.pPROJECT => {

    let p = pPROJECT_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)

    return p
}


export const bin__PROJECT = (bi:BinIndexed):jcs.PROJECT => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pPROJECT (bi)
    }
}

// [TABLE] Structure


export const pTABLE__bin = (bb:BytesBuilder) => (p:jcs.pTABLE) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.str__bin (bb) (p.Desc)
    
    marshall.int64__bin (bb) (p.Project)
}

export const TABLE__bin = (bb:BytesBuilder) => (v:jcs.TABLE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pTABLE__bin (bb) (v.p)
}

export const bin__pTABLE = (bi:BinIndexed):jcs.pTABLE => {

    let p = pTABLE_empty()
    p.Name = marshall.bin__str (bi)
    p.Desc = marshall.bin__str (bi)
    p.Project = marshall.bin__int64 (bi)

    return p
}


export const bin__TABLE = (bi:BinIndexed):jcs.TABLE => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pTABLE (bi)
    }
}

// [COMP] Structure


export const pCOMP__bin = (bb:BytesBuilder) => (p:jcs.pCOMP) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Project)
}

export const COMP__bin = (bb:BytesBuilder) => (v:jcs.COMP) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCOMP__bin (bb) (v.p)
}

export const bin__pCOMP = (bi:BinIndexed):jcs.pCOMP => {

    let p = pCOMP_empty()
    p.Name = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Project = marshall.bin__int64 (bi)

    return p
}


export const bin__COMP = (bi:BinIndexed):jcs.COMP => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pCOMP (bi)
    }
}

// [PAGE] Structure


export const pPAGE__bin = (bb:BytesBuilder) => (p:jcs.pPAGE) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Route)
    
    marshall.str__bin (bb) (p.OgTitle)
    
    marshall.str__bin (bb) (p.OgDesc)
    
    marshall.str__bin (bb) (p.OgImage)
    
    marshall.int64__bin (bb) (p.Template)
    
    marshall.int64__bin (bb) (p.Project)
}

export const PAGE__bin = (bb:BytesBuilder) => (v:jcs.PAGE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pPAGE__bin (bb) (v.p)
}

export const bin__pPAGE = (bi:BinIndexed):jcs.pPAGE => {

    let p = pPAGE_empty()
    p.Name = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Route = marshall.bin__str (bi)
    p.OgTitle = marshall.bin__str (bi)
    p.OgDesc = marshall.bin__str (bi)
    p.OgImage = marshall.bin__str (bi)
    p.Template = marshall.bin__int64 (bi)
    p.Project = marshall.bin__int64 (bi)

    return p
}


export const bin__PAGE = (bi:BinIndexed):jcs.PAGE => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pPAGE (bi)
    }
}

// [TEMPLATE] Structure


export const pTEMPLATE__bin = (bb:BytesBuilder) => (p:jcs.pTEMPLATE) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Project)
}

export const TEMPLATE__bin = (bb:BytesBuilder) => (v:jcs.TEMPLATE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pTEMPLATE__bin (bb) (v.p)
}

export const bin__pTEMPLATE = (bi:BinIndexed):jcs.pTEMPLATE => {

    let p = pTEMPLATE_empty()
    p.Name = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Project = marshall.bin__int64 (bi)

    return p
}


export const bin__TEMPLATE = (bi:BinIndexed):jcs.TEMPLATE => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pTEMPLATE (bi)
    }
}
export const pFIELD_empty = (): jcs.pFIELD => {
    return {
        Name: "",
        Desc: "",
        Project: 0,
        Table: 0 }
}

export const FIELD_empty = (): jcs.FIELD => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pFIELD_empty() }
}

export const pHOSTCONFIG_empty = (): jcs.pHOSTCONFIG => {
    return {
        Hostname: "",
        DatabaseName: "",
        DatabaseConn: "",
        DirVsShared: "",
        DirVsCodeWeb: "",
        Project: 0 }
}

export const HOSTCONFIG_empty = (): jcs.HOSTCONFIG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pHOSTCONFIG_empty() }
}

export const pPROJECT_empty = (): jcs.pPROJECT => {
    return {
        Code: "",
        Caption: "" }
}

export const PROJECT_empty = (): jcs.PROJECT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pPROJECT_empty() }
}

export const pTABLE_empty = (): jcs.pTABLE => {
    return {
        Name: "",
        Desc: "",
        Project: 0 }
}

export const TABLE_empty = (): jcs.TABLE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pTABLE_empty() }
}

export const pCOMP_empty = (): jcs.pCOMP => {
    return {
        Name: "",
        Caption: "",
        Project: 0 }
}

export const COMP_empty = (): jcs.COMP => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCOMP_empty() }
}

export const pPAGE_empty = (): jcs.pPAGE => {
    return {
        Name: "",
        Caption: "",
        Route: "",
        OgTitle: "",
        OgDesc: "",
        OgImage: "",
        Template: 0,
        Project: 0 }
}

export const PAGE_empty = (): jcs.PAGE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pPAGE_empty() }
}

export const pTEMPLATE_empty = (): jcs.pTEMPLATE => {
    return {
        Name: "",
        Caption: "",
        Project: 0 }
}

export const TEMPLATE_empty = (): jcs.TEMPLATE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pTEMPLATE_empty() }
}
