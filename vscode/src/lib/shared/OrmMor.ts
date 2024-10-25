// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
const marshall = {...binCommon }


// [ADDRESS] Structure


export const pADDRESS__bin = (bb:BytesBuilder) => (p:jcs.pADDRESS) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Bind)
    
    marshall.int32__bin (bb) (p.AddressType)
    
    marshall.str__bin (bb) (p.Line1)
    
    marshall.str__bin (bb) (p.Line2)
    
    marshall.str__bin (bb) (p.State)
    
    marshall.str__bin (bb) (p.County)
    
    marshall.str__bin (bb) (p.Town)
    
    marshall.str__bin (bb) (p.Contact)
    
    marshall.str__bin (bb) (p.Tel)
    
    marshall.str__bin (bb) (p.Email)
    
    marshall.str__bin (bb) (p.Zip)
    
    marshall.int64__bin (bb) (p.City)
    
    marshall.int64__bin (bb) (p.Country)
    
    marshall.str__bin (bb) (p.Remarks)
}

export const ADDRESS__bin = (bb:BytesBuilder) => (v:jcs.ADDRESS) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pADDRESS__bin (bb) (v.p)
}

export const bin__pADDRESS = (bi:BinIndexed):jcs.pADDRESS => {

    let p = pADDRESS_empty()
    p.Caption = marshall.bin__str (bi)
    p.Bind = marshall.bin__int64 (bi)
    p.AddressType = marshall.bin__int32 (bi)
    p.Line1 = marshall.bin__str (bi)
    p.Line2 = marshall.bin__str (bi)
    p.State = marshall.bin__str (bi)
    p.County = marshall.bin__str (bi)
    p.Town = marshall.bin__str (bi)
    p.Contact = marshall.bin__str (bi)
    p.Tel = marshall.bin__str (bi)
    p.Email = marshall.bin__str (bi)
    p.Zip = marshall.bin__str (bi)
    p.City = marshall.bin__int64 (bi)
    p.Country = marshall.bin__int64 (bi)
    p.Remarks = marshall.bin__str (bi)

    return p
}


export const bin__ADDRESS = (bi:BinIndexed):jcs.ADDRESS => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pADDRESS (bi)
    }
}

// [BIZ] Structure


export const pBIZ__bin = (bb:BytesBuilder) => (p:jcs.pBIZ) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Parent)
    
    marshall.int64__bin (bb) (p.BasicAcct)
    
    marshall.str__bin (bb) (p.DescTxt)
    
    marshall.str__bin (bb) (p.Website)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.int64__bin (bb) (p.City)
    
    marshall.int64__bin (bb) (p.Country)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.bool__bin (bb) (p.IsSocialPlatform)
    
    marshall.bool__bin (bb) (p.IsCmsSource)
    
    marshall.bool__bin (bb) (p.IsPayGateway)
}

export const BIZ__bin = (bb:BytesBuilder) => (v:jcs.BIZ) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pBIZ__bin (bb) (v.p)
}

export const bin__pBIZ = (bi:BinIndexed):jcs.pBIZ => {

    let p = pBIZ_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Parent = marshall.bin__int64 (bi)
    p.BasicAcct = marshall.bin__int64 (bi)
    p.DescTxt = marshall.bin__str (bi)
    p.Website = marshall.bin__str (bi)
    p.Icon = marshall.bin__str (bi)
    p.City = marshall.bin__int64 (bi)
    p.Country = marshall.bin__int64 (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.IsSocialPlatform = marshall.bin__bool (bi)
    p.IsCmsSource = marshall.bin__bool (bi)
    p.IsPayGateway = marshall.bin__bool (bi)

    return p
}


export const bin__BIZ = (bi:BinIndexed):jcs.BIZ => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pBIZ (bi)
    }
}

// [CAT] Structure


export const pCAT__bin = (bb:BytesBuilder) => (p:jcs.pCAT) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.int64__bin (bb) (p.Zh)
    
    marshall.int64__bin (bb) (p.Parent)
    
    marshall.int32__bin (bb) (p.CatState)
}

export const CAT__bin = (bb:BytesBuilder) => (v:jcs.CAT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCAT__bin (bb) (v.p)
}

export const bin__pCAT = (bi:BinIndexed):jcs.pCAT => {

    let p = pCAT_empty()
    p.Caption = marshall.bin__str (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.Zh = marshall.bin__int64 (bi)
    p.Parent = marshall.bin__int64 (bi)
    p.CatState = marshall.bin__int32 (bi)

    return p
}


export const bin__CAT = (bi:BinIndexed):jcs.CAT => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pCAT (bi)
    }
}

// [CITY] Structure


export const pCITY__bin = (bb:BytesBuilder) => (p:jcs.pCITY) => {

    
    marshall.str__bin (bb) (p.Fullname)
    
    marshall.str__bin (bb) (p.MetropolitanCode3IATA)
    
    marshall.str__bin (bb) (p.NameEn)
    
    marshall.int64__bin (bb) (p.Country)
    
    marshall.int64__bin (bb) (p.Place)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.str__bin (bb) (p.Tel)
}

export const CITY__bin = (bb:BytesBuilder) => (v:jcs.CITY) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCITY__bin (bb) (v.p)
}

export const bin__pCITY = (bi:BinIndexed):jcs.pCITY => {

    let p = pCITY_empty()
    p.Fullname = marshall.bin__str (bi)
    p.MetropolitanCode3IATA = marshall.bin__str (bi)
    p.NameEn = marshall.bin__str (bi)
    p.Country = marshall.bin__int64 (bi)
    p.Place = marshall.bin__int64 (bi)
    p.Icon = marshall.bin__str (bi)
    p.Tel = marshall.bin__str (bi)

    return p
}


export const bin__CITY = (bi:BinIndexed):jcs.CITY => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pCITY (bi)
    }
}

// [CRY] Structure


export const pCRY__bin = (bb:BytesBuilder) => (p:jcs.pCRY) => {

    
    marshall.str__bin (bb) (p.Code2)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Fullname)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.str__bin (bb) (p.Tel)
    
    marshall.int64__bin (bb) (p.Cur)
    
    marshall.int64__bin (bb) (p.Capital)
    
    marshall.int64__bin (bb) (p.Place)
    
    marshall.int64__bin (bb) (p.Lang)
}

export const CRY__bin = (bb:BytesBuilder) => (v:jcs.CRY) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCRY__bin (bb) (v.p)
}

export const bin__pCRY = (bi:BinIndexed):jcs.pCRY => {

    let p = pCRY_empty()
    p.Code2 = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Fullname = marshall.bin__str (bi)
    p.Icon = marshall.bin__str (bi)
    p.Tel = marshall.bin__str (bi)
    p.Cur = marshall.bin__int64 (bi)
    p.Capital = marshall.bin__int64 (bi)
    p.Place = marshall.bin__int64 (bi)
    p.Lang = marshall.bin__int64 (bi)

    return p
}


export const bin__CRY = (bi:BinIndexed):jcs.CRY => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pCRY (bi)
    }
}

// [EU] Structure


export const pEU__bin = (bb:BytesBuilder) => (p:jcs.pEU) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Username)
    
    marshall.int64__bin (bb) (p.SocialAuthBiz)
    
    marshall.str__bin (bb) (p.SocialAuthId)
    
    marshall.str__bin (bb) (p.SocialAuthAvatar)
    
    marshall.str__bin (bb) (p.Email)
    
    marshall.str__bin (bb) (p.Tel)
    
    marshall.int32__bin (bb) (p.Gender)
    
    marshall.int32__bin (bb) (p.Status)
    
    marshall.int32__bin (bb) (p.Admin)
    
    marshall.int32__bin (bb) (p.BizPartner)
    
    marshall.int64__bin (bb) (p.Privilege)
    
    marshall.int32__bin (bb) (p.Verify)
    
    marshall.str__bin (bb) (p.Pwd)
    
    marshall.bool__bin (bb) (p.Online)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.str__bin (bb) (p.Background)
    
    marshall.int64__bin (bb) (p.BasicAcct)
    
    marshall.int64__bin (bb) (p.Citizen)
    
    marshall.str__bin (bb) (p.Refer)
    
    marshall.int64__bin (bb) (p.Referer)
    
    marshall.str__bin (bb) (p.Url)
    
    marshall.str__bin (bb) (p.About)
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
    p.Username = marshall.bin__str (bi)
    p.SocialAuthBiz = marshall.bin__int64 (bi)
    p.SocialAuthId = marshall.bin__str (bi)
    p.SocialAuthAvatar = marshall.bin__str (bi)
    p.Email = marshall.bin__str (bi)
    p.Tel = marshall.bin__str (bi)
    p.Gender = marshall.bin__int32 (bi)
    p.Status = marshall.bin__int32 (bi)
    p.Admin = marshall.bin__int32 (bi)
    p.BizPartner = marshall.bin__int32 (bi)
    p.Privilege = marshall.bin__int64 (bi)
    p.Verify = marshall.bin__int32 (bi)
    p.Pwd = marshall.bin__str (bi)
    p.Online = marshall.bin__bool (bi)
    p.Icon = marshall.bin__str (bi)
    p.Background = marshall.bin__str (bi)
    p.BasicAcct = marshall.bin__int64 (bi)
    p.Citizen = marshall.bin__int64 (bi)
    p.Refer = marshall.bin__str (bi)
    p.Referer = marshall.bin__int64 (bi)
    p.Url = marshall.bin__str (bi)
    p.About = marshall.bin__str (bi)

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

// [CSI] Structure


export const pCSI__bin = (bb:BytesBuilder) => (p:jcs.pCSI) => {

    
    marshall.int32__bin (bb) (p.Type)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.int64__bin (bb) (p.Bind)
}

export const CSI__bin = (bb:BytesBuilder) => (v:jcs.CSI) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCSI__bin (bb) (v.p)
}

export const bin__pCSI = (bi:BinIndexed):jcs.pCSI => {

    let p = pCSI_empty()
    p.Type = marshall.bin__int32 (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.Bind = marshall.bin__int64 (bi)

    return p
}


export const bin__CSI = (bi:BinIndexed):jcs.CSI => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pCSI (bi)
    }
}

// [CWC] Structure


export const pCWC__bin = (bb:BytesBuilder) => (p:jcs.pCWC) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.ExternalId)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.int64__bin (bb) (p.EU)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.Json)
}

export const CWC__bin = (bb:BytesBuilder) => (v:jcs.CWC) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCWC__bin (bb) (v.p)
}

export const bin__pCWC = (bi:BinIndexed):jcs.pCWC => {

    let p = pCWC_empty()
    p.Caption = marshall.bin__str (bi)
    p.ExternalId = marshall.bin__int64 (bi)
    p.Icon = marshall.bin__str (bi)
    p.EU = marshall.bin__int64 (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.Json = marshall.bin__str (bi)

    return p
}


export const bin__CWC = (bi:BinIndexed):jcs.CWC => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pCWC (bi)
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
export const pADDRESS_empty = (): jcs.pADDRESS => {
    return {
        Caption: "",
        Bind: 0,
        AddressType: 0,
        Line1: "",
        Line2: "",
        State: "",
        County: "",
        Town: "",
        Contact: "",
        Tel: "",
        Email: "",
        Zip: "",
        City: 0,
        Country: 0,
        Remarks: "" }
}

export const ADDRESS_empty = (): jcs.ADDRESS => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pADDRESS_empty() }
}

export const pBIZ_empty = (): jcs.pBIZ => {
    return {
        Code: "",
        Caption: "",
        Parent: 0,
        BasicAcct: 0,
        DescTxt: "",
        Website: "",
        Icon: "",
        City: 0,
        Country: 0,
        Lang: 0,
        IsSocialPlatform: true,
        IsCmsSource: true,
        IsPayGateway: true }
}

export const BIZ_empty = (): jcs.BIZ => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pBIZ_empty() }
}

export const pCAT_empty = (): jcs.pCAT => {
    return {
        Caption: "",
        Lang: 0,
        Zh: 0,
        Parent: 0,
        CatState: 0 }
}

export const CAT_empty = (): jcs.CAT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCAT_empty() }
}

export const pCITY_empty = (): jcs.pCITY => {
    return {
        Fullname: "",
        MetropolitanCode3IATA: "",
        NameEn: "",
        Country: 0,
        Place: 0,
        Icon: "",
        Tel: "" }
}

export const CITY_empty = (): jcs.CITY => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCITY_empty() }
}

export const pCRY_empty = (): jcs.pCRY => {
    return {
        Code2: "",
        Caption: "",
        Fullname: "",
        Icon: "",
        Tel: "",
        Cur: 0,
        Capital: 0,
        Place: 0,
        Lang: 0 }
}

export const CRY_empty = (): jcs.CRY => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCRY_empty() }
}

export const pEU_empty = (): jcs.pEU => {
    return {
        Caption: "",
        Username: "",
        SocialAuthBiz: 0,
        SocialAuthId: "",
        SocialAuthAvatar: "",
        Email: "",
        Tel: "",
        Gender: 0,
        Status: 0,
        Admin: 0,
        BizPartner: 0,
        Privilege: 0,
        Verify: 0,
        Pwd: "",
        Online: true,
        Icon: "",
        Background: "",
        BasicAcct: 0,
        Citizen: 0,
        Refer: "",
        Referer: 0,
        Url: "",
        About: "" }
}

export const EU_empty = (): jcs.EU => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pEU_empty() }
}

export const pCSI_empty = (): jcs.pCSI => {
    return {
        Type: 0,
        Lang: 0,
        Bind: 0 }
}

export const CSI_empty = (): jcs.CSI => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCSI_empty() }
}

export const pCWC_empty = (): jcs.pCWC => {
    return {
        Caption: "",
        ExternalId: 0,
        Icon: "",
        EU: 0,
        Biz: 0,
        Json: "" }
}

export const CWC_empty = (): jcs.CWC => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCWC_empty() }
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
