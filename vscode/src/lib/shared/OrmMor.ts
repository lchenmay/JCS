// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
const marshall = {...binCommon }


// [BOOK] Structure


export const pBOOK__bin = (bb:BytesBuilder) => (p:jcs.pBOOK) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Email)
    
    marshall.str__bin (bb) (p.Message)
}

export const BOOK__bin = (bb:BytesBuilder) => (v:jcs.BOOK) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pBOOK__bin (bb) (v.p)
}

export const bin__pBOOK = (bi:BinIndexed):jcs.pBOOK => {

    let p = pBOOK_empty()
    p.Caption = marshall.bin__str (bi)
    p.Email = marshall.bin__str (bi)
    p.Message = marshall.bin__str (bi)

    return p
}


export const bin__BOOK = (bi:BinIndexed):jcs.BOOK => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pBOOK (bi)
    }
}

// [EU] Structure


export const pEU__bin = (bb:BytesBuilder) => (p:jcs.pEU) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int32__bin (bb) (p.AuthType)
}

export const EU__bin = (bb:BytesBuilder) => (v:jcs.EU) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pEU__bin (bb) (v.p)
}

export const bin__pEU = (bi:BinIndexed):jcs.pEU => {

    let p = pEU_empty()
    p.Caption = marshall.bin__str (bi)
    p.AuthType = marshall.bin__int32 (bi)

    return p
}


export const bin__EU = (bi:BinIndexed):jcs.EU => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pEU (bi)
    }
}

// [FILE] Structure


export const pFILE__bin = (bb:BytesBuilder) => (p:jcs.pFILE) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Desc)
    
    marshall.str__bin (bb) (p.Suffix)
    
    marshall.int64__bin (bb) (p.Size)
    
    marshall.int32__bin (bb) (p.Thumbnail.length)
    bb.append(p.Thumbnail)
    
    marshall.int64__bin (bb) (p.Owner)
}

export const FILE__bin = (bb:BytesBuilder) => (v:jcs.FILE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pFILE__bin (bb) (v.p)
}

export const bin__pFILE = (bi:BinIndexed):jcs.pFILE => {

    let p = pFILE_empty()
    p.Caption = marshall.bin__str (bi)
    p.Desc = marshall.bin__str (bi)
    p.Suffix = marshall.bin__str (bi)
    p.Size = marshall.bin__int64 (bi)
    
    let lengthThumbnail = binCommon.bin__int32(bi)
    p.Thumbnail = bi.bin.slice(bi.index,lengthThumbnail)
    bi.index += lengthThumbnail
    
    p.Owner = marshall.bin__int64 (bi)

    return p
}


export const bin__FILE = (bi:BinIndexed):jcs.FILE => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pFILE (bi)
    }
}

// [FBIND] Structure


export const pFBIND__bin = (bb:BytesBuilder) => (p:jcs.pFBIND) => {

    
    marshall.int64__bin (bb) (p.File)
    
    marshall.int64__bin (bb) (p.Moment)
    
    marshall.str__bin (bb) (p.Desc)
}

export const FBIND__bin = (bb:BytesBuilder) => (v:jcs.FBIND) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pFBIND__bin (bb) (v.p)
}

export const bin__pFBIND = (bi:BinIndexed):jcs.pFBIND => {

    let p = pFBIND_empty()
    p.File = marshall.bin__int64 (bi)
    p.Moment = marshall.bin__int64 (bi)
    p.Desc = marshall.bin__str (bi)

    return p
}


export const bin__FBIND = (bi:BinIndexed):jcs.FBIND => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pFBIND (bi)
    }
}

// [MOMENT] Structure


export const pMOMENT__bin = (bb:BytesBuilder) => (p:jcs.pMOMENT) => {

    
    marshall.str__bin (bb) (p.Title)
    
    marshall.str__bin (bb) (p.Summary)
    
    marshall.str__bin (bb) (p.FullText)
    
    marshall.str__bin (bb) (p.PreviewImgUrl)
    
    marshall.str__bin (bb) (p.Link)
    
    marshall.int32__bin (bb) (p.Type)
    
    marshall.int32__bin (bb) (p.State)
    
    marshall.int32__bin (bb) (p.MediaType)
}

export const MOMENT__bin = (bb:BytesBuilder) => (v:jcs.MOMENT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pMOMENT__bin (bb) (v.p)
}

export const bin__pMOMENT = (bi:BinIndexed):jcs.pMOMENT => {

    let p = pMOMENT_empty()
    p.Title = marshall.bin__str (bi)
    p.Summary = marshall.bin__str (bi)
    p.FullText = marshall.bin__str (bi)
    p.PreviewImgUrl = marshall.bin__str (bi)
    p.Link = marshall.bin__str (bi)
    p.Type = marshall.bin__int32 (bi)
    p.State = marshall.bin__int32 (bi)
    p.MediaType = marshall.bin__int32 (bi)

    return p
}


export const bin__MOMENT = (bi:BinIndexed):jcs.MOMENT => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pMOMENT (bi)
    }
}

// [LOG] Structure


export const pLOG__bin = (bb:BytesBuilder) => (p:jcs.pLOG) => {

    
    marshall.str__bin (bb) (p.Location)
    
    marshall.str__bin (bb) (p.Content)
    
    marshall.str__bin (bb) (p.Sql)
}

export const LOG__bin = (bb:BytesBuilder) => (v:jcs.LOG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pLOG__bin (bb) (v.p)
}

export const bin__pLOG = (bi:BinIndexed):jcs.pLOG => {

    let p = pLOG_empty()
    p.Location = marshall.bin__str (bi)
    p.Content = marshall.bin__str (bi)
    p.Sql = marshall.bin__str (bi)

    return p
}


export const bin__LOG = (bi:BinIndexed):jcs.LOG => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pLOG (bi)
    }
}

// [PLOG] Structure


export const pPLOG__bin = (bb:BytesBuilder) => (p:jcs.pPLOG) => {

    
    marshall.str__bin (bb) (p.Ip)
    
    marshall.str__bin (bb) (p.Request)
}

export const PLOG__bin = (bb:BytesBuilder) => (v:jcs.PLOG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pPLOG__bin (bb) (v.p)
}

export const bin__pPLOG = (bi:BinIndexed):jcs.pPLOG => {

    let p = pPLOG_empty()
    p.Ip = marshall.bin__str (bi)
    p.Request = marshall.bin__str (bi)

    return p
}


export const bin__PLOG = (bi:BinIndexed):jcs.PLOG => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pPLOG (bi)
    }
}

// [API] Structure


export const pAPI__bin = (bb:BytesBuilder) => (p:jcs.pAPI) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.int64__bin (bb) (p.Project)
}

export const API__bin = (bb:BytesBuilder) => (v:jcs.API) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pAPI__bin (bb) (v.p)
}

export const bin__pAPI = (bi:BinIndexed):jcs.pAPI => {

    let p = pAPI_empty()
    p.Name = marshall.bin__str (bi)
    p.Project = marshall.bin__int64 (bi)

    return p
}


export const bin__API = (bi:BinIndexed):jcs.API => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pAPI (bi)
    }
}

// [FIELD] Structure


export const pFIELD__bin = (bb:BytesBuilder) => (p:jcs.pFIELD) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.str__bin (bb) (p.Desc)
    
    marshall.int32__bin (bb) (p.FieldType)
    
    marshall.int64__bin (bb) (p.Length)
    
    marshall.str__bin (bb) (p.SelectLines)
    
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
    p.FieldType = marshall.bin__int32 (bi)
    p.Length = marshall.bin__int64 (bi)
    p.SelectLines = marshall.bin__str (bi)
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
    
    marshall.int32__bin (bb) (p.Database)
    
    marshall.str__bin (bb) (p.DatabaseName)
    
    marshall.str__bin (bb) (p.DatabaseConn)
    
    marshall.str__bin (bb) (p.DirVs)
    
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
    p.Database = marshall.bin__int32 (bi)
    p.DatabaseName = marshall.bin__str (bi)
    p.DatabaseConn = marshall.bin__str (bi)
    p.DirVs = marshall.bin__str (bi)
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
    
    marshall.str__bin (bb) (p.TypeSessionUser)
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
    p.TypeSessionUser = marshall.bin__str (bi)

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

// [VARTYPE] Structure


export const pVARTYPE__bin = (bb:BytesBuilder) => (p:jcs.pVARTYPE) => {

    
    marshall.str__bin (bb) (p.Name)
    
    marshall.str__bin (bb) (p.Type)
    
    marshall.str__bin (bb) (p.Val)
    
    marshall.int32__bin (bb) (p.BindType)
    
    marshall.int64__bin (bb) (p.Bind)
    
    marshall.int64__bin (bb) (p.Project)
}

export const VARTYPE__bin = (bb:BytesBuilder) => (v:jcs.VARTYPE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pVARTYPE__bin (bb) (v.p)
}

export const bin__pVARTYPE = (bi:BinIndexed):jcs.pVARTYPE => {

    let p = pVARTYPE_empty()
    p.Name = marshall.bin__str (bi)
    p.Type = marshall.bin__str (bi)
    p.Val = marshall.bin__str (bi)
    p.BindType = marshall.bin__int32 (bi)
    p.Bind = marshall.bin__int64 (bi)
    p.Project = marshall.bin__int64 (bi)

    return p
}


export const bin__VARTYPE = (bi:BinIndexed):jcs.VARTYPE => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pVARTYPE (bi)
    }
}
export const pBOOK_empty = (): jcs.pBOOK => {
    return {
        Caption: "",
        Email: "",
        Message: "" }
}

export const BOOK_empty = (): jcs.BOOK => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pBOOK_empty() }
}

export const euAuthTypeEnum_Normal = 0 // Normal
export const euAuthTypeEnum_Authorized = 1 // Authorized
export const euAuthTypeEnum_Admin = 2 // Admin

export const pEU_empty = (): jcs.pEU => {
    return {
        Caption: "",
        AuthType: 0 }
}

export const EU_empty = (): jcs.EU => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pEU_empty() }
}

export const pFILE_empty = (): jcs.pFILE => {
    return {
        Caption: "",
        Desc: "",
        Suffix: "",
        Size: 0,
        Thumbnail: [],
        Owner: 0 }
}

export const FILE_empty = (): jcs.FILE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pFILE_empty() }
}

export const pFBIND_empty = (): jcs.pFBIND => {
    return {
        File: 0,
        Moment: 0,
        Desc: "" }
}

export const FBIND_empty = (): jcs.FBIND => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pFBIND_empty() }
}

export const momentTypeEnum_Original = 0 // 原创图文视频
export const momentTypeEnum_Repost = 1 // 转发
export const momentTypeEnum_Thread = 2 // 文章
export const momentTypeEnum_Forum = 3 // 论坛
export const momentTypeEnum_Question = 4 // 问题
export const momentTypeEnum_Answer = 5 // 回答
export const momentTypeEnum_BookmarkList = 6 // 收藏夹
export const momentTypeEnum_Poll = 7 // 投票
export const momentTypeEnum_Miles = 8 // 文贵直播文字版
export const momentTypeEnum_Dict = 9 // 辞典
export const momentTypeEnum_WebPage = 10 // 页面
export const momentTypeEnum_MediaFile = 11 // 媒体文件

export const momentStateEnum_Normal = 0 // 正常
export const momentStateEnum_Deleted = 1 // 标记删除
export const momentStateEnum_Scratch = 2 // 草稿

export const momentMediaTypeEnum_None = 0 // 无
export const momentMediaTypeEnum_Video = 1 // 视频
export const momentMediaTypeEnum_Audio = 2 // 音频

export const pMOMENT_empty = (): jcs.pMOMENT => {
    return {
        Title: "",
        Summary: "",
        FullText: "",
        PreviewImgUrl: "",
        Link: "",
        Type: 0,
        State: 0,
        MediaType: 0 }
}

export const MOMENT_empty = (): jcs.MOMENT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pMOMENT_empty() }
}

export const pLOG_empty = (): jcs.pLOG => {
    return {
        Location: "",
        Content: "",
        Sql: "" }
}

export const LOG_empty = (): jcs.LOG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pLOG_empty() }
}

export const pPLOG_empty = (): jcs.pPLOG => {
    return {
        Ip: "",
        Request: "" }
}

export const PLOG_empty = (): jcs.PLOG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pPLOG_empty() }
}

export const pAPI_empty = (): jcs.pAPI => {
    return {
        Name: "",
        Project: 0 }
}

export const API_empty = (): jcs.API => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pAPI_empty() }
}

export const fieldFieldTypeEnum_Undefined = 0 // Undefined
export const fieldFieldTypeEnum_FK = 1 // FK
export const fieldFieldTypeEnum_Caption = 2 // Caption
export const fieldFieldTypeEnum_Chars = 3 // Chars
export const fieldFieldTypeEnum_Link = 4 // Link
export const fieldFieldTypeEnum_Text = 5 // Text
export const fieldFieldTypeEnum_Bin = 6 // Bin
export const fieldFieldTypeEnum_Integer = 7 // Integer
export const fieldFieldTypeEnum_Float = 8 // Float
export const fieldFieldTypeEnum_Boolean = 9 // Boolean
export const fieldFieldTypeEnum_SelectLines = 10 // Select Lines
export const fieldFieldTypeEnum_Timestamp = 11 // Time Stamp
export const fieldFieldTypeEnum_TimeSeries = 12 // Time Series

export const pFIELD_empty = (): jcs.pFIELD => {
    return {
        Name: "",
        Desc: "",
        FieldType: 0,
        Length: 0,
        SelectLines: "",
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

export const hostconfigDatabaseEnum_SQLSERVER = 0 // SQL Server
export const hostconfigDatabaseEnum_PostgreSQL = 1 // PostgreSQL

export const pHOSTCONFIG_empty = (): jcs.pHOSTCONFIG => {
    return {
        Hostname: "",
        Database: 0,
        DatabaseName: "",
        DatabaseConn: "",
        DirVs: "",
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
        Caption: "",
        TypeSessionUser: "" }
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

export const vartypeBindTypeEnum_ApiRequest = 0 // API Request
export const vartypeBindTypeEnum_ApiResponse = 1 // API Response
export const vartypeBindTypeEnum_CompState = 2 // Component State
export const vartypeBindTypeEnum_CompProps = 3 // Component Propos
export const vartypeBindTypeEnum_PageState = 4 // Page State
export const vartypeBindTypeEnum_PageProps = 5 // Page Propos

export const pVARTYPE_empty = (): jcs.pVARTYPE => {
    return {
        Name: "",
        Type: "",
        Val: "",
        BindType: 0,
        Bind: 0,
        Project: 0 }
}

export const VARTYPE_empty = (): jcs.VARTYPE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pVARTYPE_empty() }
}
