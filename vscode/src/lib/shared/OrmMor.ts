// OrmMor.ts
import { BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
const marshall = {...binCommon }


// [ADDRESS] Structure

export const pADDRESS__bin = (bb:BytesBuilder) => (p:jcs.pADDRESS) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Bind)
    
    marshall.int32__bin (bb) (p.Type)
    
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
    p.Type = marshall.bin__int32 (bi)
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
    
    marshall.str__bin (bb) (p.Desc)
    
    marshall.str__bin (bb) (p.Website)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.int64__bin (bb) (p.City)
    
    marshall.int64__bin (bb) (p.Country)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.bool__bin (bb) (p.IsSocial)
    
    marshall.bool__bin (bb) (p.IsCmsSource)
    
    marshall.bool__bin (bb) (p.IsPay)
    
    marshall.int64__bin (bb) (p.MomentLatest)
    
    marshall.int64__bin (bb) (p.CountFollowers)
    
    marshall.int64__bin (bb) (p.CountFollows)
    
    marshall.int64__bin (bb) (p.CountMoments)
    
    marshall.int64__bin (bb) (p.CountViews)
    
    marshall.int64__bin (bb) (p.CountComments)
    
    marshall.int64__bin (bb) (p.CountThumbUps)
    
    marshall.int64__bin (bb) (p.CountThumbDns)
    
    marshall.bool__bin (bb) (p.IsCrawling)
    
    marshall.bool__bin (bb) (p.IsGSeries)
    
    marshall.str__bin (bb) (p.RemarksCentral)
    
    marshall.int64__bin (bb) (p.Agent)
    
    marshall.str__bin (bb) (p.SiteCats)
    
    marshall.str__bin (bb) (p.ConfiguredCats)
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
    p.Desc = marshall.bin__str (bi)
    p.Website = marshall.bin__str (bi)
    p.Icon = marshall.bin__str (bi)
    p.City = marshall.bin__int64 (bi)
    p.Country = marshall.bin__int64 (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.IsSocial = marshall.bin__bool (bi)
    p.IsCmsSource = marshall.bin__bool (bi)
    p.IsPay = marshall.bin__bool (bi)
    p.MomentLatest = marshall.bin__int64 (bi)
    p.CountFollowers = marshall.bin__int64 (bi)
    p.CountFollows = marshall.bin__int64 (bi)
    p.CountMoments = marshall.bin__int64 (bi)
    p.CountViews = marshall.bin__int64 (bi)
    p.CountComments = marshall.bin__int64 (bi)
    p.CountThumbUps = marshall.bin__int64 (bi)
    p.CountThumbDns = marshall.bin__int64 (bi)
    p.IsCrawling = marshall.bin__bool (bi)
    p.IsGSeries = marshall.bin__bool (bi)
    p.RemarksCentral = marshall.bin__str (bi)
    p.Agent = marshall.bin__int64 (bi)
    p.SiteCats = marshall.bin__str (bi)
    p.ConfiguredCats = marshall.bin__str (bi)

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

// [CUR] Structure

export const pCUR__bin = (bb:BytesBuilder) => (p:jcs.pCUR) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.bool__bin (bb) (p.Hidden)
    
    marshall.bool__bin (bb) (p.IsSac)
    
    marshall.bool__bin (bb) (p.IsTransfer)
    
    marshall.bool__bin (bb) (p.IsCash)
    
    marshall.bool__bin (bb) (p.EnableReward)
    
    marshall.bool__bin (bb) (p.EnableOTC)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.int32__bin (bb) (p.CurType)
    
    marshall.int64__bin (bb) (p.Dec)
    
    marshall.float__bin (bb) (p.AnchorRate)
    
    marshall.bool__bin (bb) (p.Freezable)
    
    marshall.bool__bin (bb) (p.Authorizable)
    
    marshall.str__bin (bb) (p.ChaninID)
    
    marshall.str__bin (bb) (p.ChaninName)
    
    marshall.str__bin (bb) (p.ContractAddress)
    
    marshall.str__bin (bb) (p.WalletAddress)
    
    marshall.float__bin (bb) (p.BaseRate)
}

export const CUR__bin = (bb:BytesBuilder) => (v:jcs.CUR) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCUR__bin (bb) (v.p)
}

export const bin__pCUR = (bi:BinIndexed):jcs.pCUR => {

    let p = pCUR_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Hidden = marshall.bin__bool (bi)
    p.IsSac = marshall.bin__bool (bi)
    p.IsTransfer = marshall.bin__bool (bi)
    p.IsCash = marshall.bin__bool (bi)
    p.EnableReward = marshall.bin__bool (bi)
    p.EnableOTC = marshall.bin__bool (bi)
    p.Icon = marshall.bin__str (bi)
    p.CurType = marshall.bin__int32 (bi)
    p.Dec = marshall.bin__int64 (bi)
    p.AnchorRate = marshall.bin__float (bi)
    p.Freezable = marshall.bin__bool (bi)
    p.Authorizable = marshall.bin__bool (bi)
    p.ChaninID = marshall.bin__str (bi)
    p.ChaninName = marshall.bin__str (bi)
    p.ContractAddress = marshall.bin__str (bi)
    p.WalletAddress = marshall.bin__str (bi)
    p.BaseRate = marshall.bin__float (bi)

    return p
}


export const bin__CUR = (bi:BinIndexed):jcs.CUR => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pCUR (bi)
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

// [FILE] Structure

export const pFILE__bin = (bb:BytesBuilder) => (p:jcs.pFILE) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int32__bin (bb) (p.Encrypt)
    
    marshall.str__bin (bb) (p.SHA256)
    
    marshall.int64__bin (bb) (p.Size)
    
    marshall.int64__bin (bb) (p.Bind)
    
    marshall.int32__bin (bb) (p.BindType)
    
    marshall.int32__bin (bb) (p.State)
    
    marshall.int64__bin (bb) (p.Folder)
    
    marshall.int32__bin (bb) (p.FileType)
    
    marshall.str__bin (bb) (p.JSON)
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
    p.Encrypt = marshall.bin__int32 (bi)
    p.SHA256 = marshall.bin__str (bi)
    p.Size = marshall.bin__int64 (bi)
    p.Bind = marshall.bin__int64 (bi)
    p.BindType = marshall.bin__int32 (bi)
    p.State = marshall.bin__int32 (bi)
    p.Folder = marshall.bin__int64 (bi)
    p.FileType = marshall.bin__int32 (bi)
    p.JSON = marshall.bin__str (bi)

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

// [FOLDER] Structure

export const pFOLDER__bin = (bb:BytesBuilder) => (p:jcs.pFOLDER) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int32__bin (bb) (p.Encrypt)
    
    marshall.int64__bin (bb) (p.Bind)
    
    marshall.int32__bin (bb) (p.BindType)
    
    marshall.int64__bin (bb) (p.Parent)
}

export const FOLDER__bin = (bb:BytesBuilder) => (v:jcs.FOLDER) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pFOLDER__bin (bb) (v.p)
}

export const bin__pFOLDER = (bi:BinIndexed):jcs.pFOLDER => {

    let p = pFOLDER_empty()
    p.Caption = marshall.bin__str (bi)
    p.Encrypt = marshall.bin__int32 (bi)
    p.Bind = marshall.bin__int64 (bi)
    p.BindType = marshall.bin__int32 (bi)
    p.Parent = marshall.bin__int64 (bi)

    return p
}


export const bin__FOLDER = (bi:BinIndexed):jcs.FOLDER => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pFOLDER (bi)
    }
}

// [LANG] Structure

export const pLANG__bin = (bb:BytesBuilder) => (p:jcs.pLANG) => {

    
    marshall.str__bin (bb) (p.Code2)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Native)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.bool__bin (bb) (p.IsBlank)
    
    marshall.bool__bin (bb) (p.IsLocale)
    
    marshall.bool__bin (bb) (p.IsContent)
    
    marshall.bool__bin (bb) (p.IsAutoTranslate)
    
    marshall.int32__bin (bb) (p.TextDirection)
}

export const LANG__bin = (bb:BytesBuilder) => (v:jcs.LANG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pLANG__bin (bb) (v.p)
}

export const bin__pLANG = (bi:BinIndexed):jcs.pLANG => {

    let p = pLANG_empty()
    p.Code2 = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Native = marshall.bin__str (bi)
    p.Icon = marshall.bin__str (bi)
    p.IsBlank = marshall.bin__bool (bi)
    p.IsLocale = marshall.bin__bool (bi)
    p.IsContent = marshall.bin__bool (bi)
    p.IsAutoTranslate = marshall.bin__bool (bi)
    p.TextDirection = marshall.bin__int32 (bi)

    return p
}


export const bin__LANG = (bi:BinIndexed):jcs.LANG => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pLANG (bi)
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
export const pADDRESS_empty = (): jcs.pADDRESS => {
    return {
        Caption: "",
        Bind: 0,
        Type: 0,
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
        Desc: "",
        Website: "",
        Icon: "",
        City: 0,
        Country: 0,
        Lang: 0,
        IsSocial: true,
        IsCmsSource: true,
        IsPay: true,
        MomentLatest: 0,
        CountFollowers: 0,
        CountFollows: 0,
        CountMoments: 0,
        CountViews: 0,
        CountComments: 0,
        CountThumbUps: 0,
        CountThumbDns: 0,
        IsCrawling: true,
        IsGSeries: true,
        RemarksCentral: "",
        Agent: 0,
        SiteCats: "",
        ConfiguredCats: "" }
}

export const BIZ_empty = (): jcs.BIZ => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pBIZ_empty() }
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

export const pCUR_empty = (): jcs.pCUR => {
    return {
        Code: "",
        Caption: "",
        Hidden: true,
        IsSac: true,
        IsTransfer: true,
        IsCash: true,
        EnableReward: true,
        EnableOTC: true,
        Icon: "",
        CurType: 0,
        Dec: 0,
        AnchorRate: 0.0,
        Freezable: true,
        Authorizable: true,
        ChaninID: "",
        ChaninName: "",
        ContractAddress: "",
        WalletAddress: "",
        BaseRate: 0.0 }
}

export const CUR_empty = (): jcs.CUR => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCUR_empty() }
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

export const pFILE_empty = (): jcs.pFILE => {
    return {
        Caption: "",
        Encrypt: 0,
        SHA256: "",
        Size: 0,
        Bind: 0,
        BindType: 0,
        State: 0,
        Folder: 0,
        FileType: 0,
        JSON: "" }
}

export const FILE_empty = (): jcs.FILE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pFILE_empty() }
}

export const pFOLDER_empty = (): jcs.pFOLDER => {
    return {
        Caption: "",
        Encrypt: 0,
        Bind: 0,
        BindType: 0,
        Parent: 0 }
}

export const FOLDER_empty = (): jcs.FOLDER => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pFOLDER_empty() }
}

export const pLANG_empty = (): jcs.pLANG => {
    return {
        Code2: "",
        Caption: "",
        Native: "",
        Icon: "",
        IsBlank: true,
        IsLocale: true,
        IsContent: true,
        IsAutoTranslate: true,
        TextDirection: 0 }
}

export const LANG_empty = (): jcs.LANG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pLANG_empty() }
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
