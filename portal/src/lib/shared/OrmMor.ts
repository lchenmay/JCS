// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
const marshall = {...binCommon }


// [ADDRESS] Structure


export const pADDRESS__bin = (bb:BytesBuilder) => (p:j7.pADDRESS) => {

    
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

export const ADDRESS__bin = (bb:BytesBuilder) => (v:j7.ADDRESS) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pADDRESS__bin (bb) (v.p)
}

export const bin__pADDRESS = (bi:BinIndexed):j7.pADDRESS => {

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


export const bin__ADDRESS = (bi:BinIndexed):j7.ADDRESS => {

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


export const pBIZ__bin = (bb:BytesBuilder) => (p:j7.pBIZ) => {

    
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

export const BIZ__bin = (bb:BytesBuilder) => (v:j7.BIZ) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pBIZ__bin (bb) (v.p)
}

export const bin__pBIZ = (bi:BinIndexed):j7.pBIZ => {

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


export const bin__BIZ = (bi:BinIndexed):j7.BIZ => {

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


export const pCAT__bin = (bb:BytesBuilder) => (p:j7.pCAT) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.int64__bin (bb) (p.Zh)
    
    marshall.int64__bin (bb) (p.Parent)
    
    marshall.int32__bin (bb) (p.CatState)
}

export const CAT__bin = (bb:BytesBuilder) => (v:j7.CAT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCAT__bin (bb) (v.p)
}

export const bin__pCAT = (bi:BinIndexed):j7.pCAT => {

    let p = pCAT_empty()
    p.Caption = marshall.bin__str (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.Zh = marshall.bin__int64 (bi)
    p.Parent = marshall.bin__int64 (bi)
    p.CatState = marshall.bin__int32 (bi)

    return p
}


export const bin__CAT = (bi:BinIndexed):j7.CAT => {

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


export const pCITY__bin = (bb:BytesBuilder) => (p:j7.pCITY) => {

    
    marshall.str__bin (bb) (p.Fullname)
    
    marshall.str__bin (bb) (p.MetropolitanCode3IATA)
    
    marshall.str__bin (bb) (p.NameEn)
    
    marshall.int64__bin (bb) (p.Country)
    
    marshall.int64__bin (bb) (p.Place)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.str__bin (bb) (p.Tel)
}

export const CITY__bin = (bb:BytesBuilder) => (v:j7.CITY) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCITY__bin (bb) (v.p)
}

export const bin__pCITY = (bi:BinIndexed):j7.pCITY => {

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


export const bin__CITY = (bi:BinIndexed):j7.CITY => {

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


export const pCRY__bin = (bb:BytesBuilder) => (p:j7.pCRY) => {

    
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

export const CRY__bin = (bb:BytesBuilder) => (v:j7.CRY) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCRY__bin (bb) (v.p)
}

export const bin__pCRY = (bi:BinIndexed):j7.pCRY => {

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


export const bin__CRY = (bi:BinIndexed):j7.CRY => {

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


export const pCUR__bin = (bb:BytesBuilder) => (p:j7.pCUR) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.bool__bin (bb) (p.IsHidden)
    
    marshall.bool__bin (bb) (p.IsSac)
    
    marshall.bool__bin (bb) (p.IsTransfer)
    
    marshall.bool__bin (bb) (p.IsCash)
    
    marshall.bool__bin (bb) (p.EnableReward)
    
    marshall.bool__bin (bb) (p.EnableOTC)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.int32__bin (bb) (p.CurType)
    
    marshall.int64__bin (bb) (p.FloatDec)
    
    marshall.float__bin (bb) (p.AnchorRate)
    
    marshall.bool__bin (bb) (p.Freezable)
    
    marshall.bool__bin (bb) (p.Authorizable)
    
    marshall.str__bin (bb) (p.ChaninID)
    
    marshall.str__bin (bb) (p.ChaninName)
    
    marshall.str__bin (bb) (p.ContractAddress)
    
    marshall.str__bin (bb) (p.WalletAddress)
    
    marshall.float__bin (bb) (p.BaseRate)
}

export const CUR__bin = (bb:BytesBuilder) => (v:j7.CUR) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCUR__bin (bb) (v.p)
}

export const bin__pCUR = (bi:BinIndexed):j7.pCUR => {

    let p = pCUR_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.IsHidden = marshall.bin__bool (bi)
    p.IsSac = marshall.bin__bool (bi)
    p.IsTransfer = marshall.bin__bool (bi)
    p.IsCash = marshall.bin__bool (bi)
    p.EnableReward = marshall.bin__bool (bi)
    p.EnableOTC = marshall.bin__bool (bi)
    p.Icon = marshall.bin__str (bi)
    p.CurType = marshall.bin__int32 (bi)
    p.FloatDec = marshall.bin__int64 (bi)
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


export const bin__CUR = (bi:BinIndexed):j7.CUR => {

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


export const pEU__bin = (bb:BytesBuilder) => (p:j7.pEU) => {

    
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

export const EU__bin = (bb:BytesBuilder) => (v:j7.EU) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pEU__bin (bb) (v.p)
}

export const bin__pEU = (bi:BinIndexed):j7.pEU => {

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


export const bin__EU = (bi:BinIndexed):j7.EU => {

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


export const pFILE__bin = (bb:BytesBuilder) => (p:j7.pFILE) => {

    
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

export const FILE__bin = (bb:BytesBuilder) => (v:j7.FILE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pFILE__bin (bb) (v.p)
}

export const bin__pFILE = (bi:BinIndexed):j7.pFILE => {

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


export const bin__FILE = (bi:BinIndexed):j7.FILE => {

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


export const pFOLDER__bin = (bb:BytesBuilder) => (p:j7.pFOLDER) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int32__bin (bb) (p.Encrypt)
    
    marshall.int64__bin (bb) (p.Bind)
    
    marshall.int32__bin (bb) (p.BindType)
    
    marshall.int64__bin (bb) (p.Parent)
}

export const FOLDER__bin = (bb:BytesBuilder) => (v:j7.FOLDER) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pFOLDER__bin (bb) (v.p)
}

export const bin__pFOLDER = (bi:BinIndexed):j7.pFOLDER => {

    let p = pFOLDER_empty()
    p.Caption = marshall.bin__str (bi)
    p.Encrypt = marshall.bin__int32 (bi)
    p.Bind = marshall.bin__int64 (bi)
    p.BindType = marshall.bin__int32 (bi)
    p.Parent = marshall.bin__int64 (bi)

    return p
}


export const bin__FOLDER = (bi:BinIndexed):j7.FOLDER => {

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


export const pLANG__bin = (bb:BytesBuilder) => (p:j7.pLANG) => {

    
    marshall.str__bin (bb) (p.Code2)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Native)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.bool__bin (bb) (p.IsBlank)
    
    marshall.bool__bin (bb) (p.IsLocale)
    
    marshall.bool__bin (bb) (p.IsContent)
    
    marshall.bool__bin (bb) (p.IsAutoTranslate)
    
    marshall.bool__bin (bb) (p.IsMiles)
    
    marshall.int32__bin (bb) (p.TextDirection)
}

export const LANG__bin = (bb:BytesBuilder) => (v:j7.LANG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pLANG__bin (bb) (v.p)
}

export const bin__pLANG = (bi:BinIndexed):j7.pLANG => {

    let p = pLANG_empty()
    p.Code2 = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Native = marshall.bin__str (bi)
    p.Icon = marshall.bin__str (bi)
    p.IsBlank = marshall.bin__bool (bi)
    p.IsLocale = marshall.bin__bool (bi)
    p.IsContent = marshall.bin__bool (bi)
    p.IsAutoTranslate = marshall.bin__bool (bi)
    p.IsMiles = marshall.bin__bool (bi)
    p.TextDirection = marshall.bin__int32 (bi)

    return p
}


export const bin__LANG = (bi:BinIndexed):j7.LANG => {

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

// [LOCALE] Structure


export const pLOCALE__bin = (bb:BytesBuilder) => (p:j7.pLOCALE) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.str__bin (bb) (p.Zh)
    
    marshall.str__bin (bb) (p.Text)
}

export const LOCALE__bin = (bb:BytesBuilder) => (v:j7.LOCALE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pLOCALE__bin (bb) (v.p)
}

export const bin__pLOCALE = (bi:BinIndexed):j7.pLOCALE => {

    let p = pLOCALE_empty()
    p.Code = marshall.bin__str (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.Zh = marshall.bin__str (bi)
    p.Text = marshall.bin__str (bi)

    return p
}


export const bin__LOCALE = (bi:BinIndexed):j7.LOCALE => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pLOCALE (bi)
    }
}

// [CSI] Structure


export const pCSI__bin = (bb:BytesBuilder) => (p:j7.pCSI) => {

    
    marshall.int32__bin (bb) (p.Type)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.int64__bin (bb) (p.Bind)
}

export const CSI__bin = (bb:BytesBuilder) => (v:j7.CSI) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCSI__bin (bb) (v.p)
}

export const bin__pCSI = (bi:BinIndexed):j7.pCSI => {

    let p = pCSI_empty()
    p.Type = marshall.bin__int32 (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.Bind = marshall.bin__int64 (bi)

    return p
}


export const bin__CSI = (bi:BinIndexed):j7.CSI => {

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


export const pCWC__bin = (bb:BytesBuilder) => (p:j7.pCWC) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.ExternalId)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.int64__bin (bb) (p.EU)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.Json)
}

export const CWC__bin = (bb:BytesBuilder) => (v:j7.CWC) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCWC__bin (bb) (v.p)
}

export const bin__pCWC = (bi:BinIndexed):j7.pCWC => {

    let p = pCWC_empty()
    p.Caption = marshall.bin__str (bi)
    p.ExternalId = marshall.bin__int64 (bi)
    p.Icon = marshall.bin__str (bi)
    p.EU = marshall.bin__int64 (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.Json = marshall.bin__str (bi)

    return p
}


export const bin__CWC = (bi:BinIndexed):j7.CWC => {

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

// [INS] Structure


export const pINS__bin = (bb:BytesBuilder) => (p:j7.pINS) => {

    
    marshall.str__bin (bb) (p.DescTxt)
    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Long)
    
    marshall.str__bin (bb) (p.LongCode)
    
    marshall.int64__bin (bb) (p.Short)
    
    marshall.str__bin (bb) (p.ShortCode)
    
    marshall.int64__bin (bb) (p.Digit)
    
    marshall.float__bin (bb) (p.Ask)
    
    marshall.float__bin (bb) (p.Bid)
    
    marshall.float__bin (bb) (p.PriceCurrent)
}

export const INS__bin = (bb:BytesBuilder) => (v:j7.INS) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pINS__bin (bb) (v.p)
}

export const bin__pINS = (bi:BinIndexed):j7.pINS => {

    let p = pINS_empty()
    p.DescTxt = marshall.bin__str (bi)
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Long = marshall.bin__int64 (bi)
    p.LongCode = marshall.bin__str (bi)
    p.Short = marshall.bin__int64 (bi)
    p.ShortCode = marshall.bin__str (bi)
    p.Digit = marshall.bin__int64 (bi)
    p.Ask = marshall.bin__float (bi)
    p.Bid = marshall.bin__float (bi)
    p.PriceCurrent = marshall.bin__float (bi)

    return p
}


export const bin__INS = (bi:BinIndexed):j7.INS => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pINS (bi)
    }
}

// [TICKET] Structure


export const pTICKET__bin = (bb:BytesBuilder) => (p:j7.pTICKET) => {

    
    marshall.int64__bin (bb) (p.Instrument)
    
    marshall.str__bin (bb) (p.InsCode)
    
    marshall.str__bin (bb) (p.TicketNum)
    
    marshall.int32__bin (bb) (p.State)
    
    marshall.float__bin (bb) (p.Lot)
    
    marshall.float__bin (bb) (p.OpenPrice)
    
    marshall.float__bin (bb) (p.ClosePrice)
    
    marshall.float__bin (bb) (p.SL)
    
    marshall.float__bin (bb) (p.TP)
    
    marshall.int32__bin (bb) (p.CloseType)
    
    marshall.str__bin (bb) (p.Cmt)
    
    marshall.DateTime__bin (bb) (p.Pendingat)
    
    marshall.DateTime__bin (bb) (p.Opendat)
    
    marshall.DateTime__bin (bb) (p.Closedat)
    
    marshall.DateTime__bin (bb) (p.Canceledat)
}

export const TICKET__bin = (bb:BytesBuilder) => (v:j7.TICKET) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pTICKET__bin (bb) (v.p)
}

export const bin__pTICKET = (bi:BinIndexed):j7.pTICKET => {

    let p = pTICKET_empty()
    p.Instrument = marshall.bin__int64 (bi)
    p.InsCode = marshall.bin__str (bi)
    p.TicketNum = marshall.bin__str (bi)
    p.State = marshall.bin__int32 (bi)
    p.Lot = marshall.bin__float (bi)
    p.OpenPrice = marshall.bin__float (bi)
    p.ClosePrice = marshall.bin__float (bi)
    p.SL = marshall.bin__float (bi)
    p.TP = marshall.bin__float (bi)
    p.CloseType = marshall.bin__int32 (bi)
    p.Cmt = marshall.bin__str (bi)
    p.Pendingat = marshall.bin__DateTime (bi)
    p.Opendat = marshall.bin__DateTime (bi)
    p.Closedat = marshall.bin__DateTime (bi)
    p.Canceledat = marshall.bin__DateTime (bi)

    return p
}


export const bin__TICKET = (bi:BinIndexed):j7.TICKET => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pTICKET (bi)
    }
}

// [TACCT] Structure


export const pTACCT__bin = (bb:BytesBuilder) => (p:j7.pTACCT) => {

    
    marshall.int64__bin (bb) (p.SAC)
    
    marshall.int32__bin (bb) (p.State)
    
    marshall.int32__bin (bb) (p.TradeType)
    
    marshall.int32__bin (bb) (p.RealDemo)
    
    marshall.float__bin (bb) (p.PnL)
    
    marshall.float__bin (bb) (p.Frozen)
    
    marshall.float__bin (bb) (p.Leverage)
    
    marshall.float__bin (bb) (p.Margin)
    
    marshall.float__bin (bb) (p.MarginCallRateWarning)
    
    marshall.float__bin (bb) (p.MarginCallRateWarningII)
    
    marshall.float__bin (bb) (p.MarginCallRateLiq)
    
    marshall.str__bin (bb) (p.PwdTrade)
    
    marshall.str__bin (bb) (p.PwdReadonly)
    
    marshall.str__bin (bb) (p.DescTxt)
}

export const TACCT__bin = (bb:BytesBuilder) => (v:j7.TACCT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pTACCT__bin (bb) (v.p)
}

export const bin__pTACCT = (bi:BinIndexed):j7.pTACCT => {

    let p = pTACCT_empty()
    p.SAC = marshall.bin__int64 (bi)
    p.State = marshall.bin__int32 (bi)
    p.TradeType = marshall.bin__int32 (bi)
    p.RealDemo = marshall.bin__int32 (bi)
    p.PnL = marshall.bin__float (bi)
    p.Frozen = marshall.bin__float (bi)
    p.Leverage = marshall.bin__float (bi)
    p.Margin = marshall.bin__float (bi)
    p.MarginCallRateWarning = marshall.bin__float (bi)
    p.MarginCallRateWarningII = marshall.bin__float (bi)
    p.MarginCallRateLiq = marshall.bin__float (bi)
    p.PwdTrade = marshall.bin__str (bi)
    p.PwdReadonly = marshall.bin__str (bi)
    p.DescTxt = marshall.bin__str (bi)

    return p
}


export const bin__TACCT = (bi:BinIndexed):j7.TACCT => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pTACCT (bi)
    }
}

// [LOG] Structure


export const pLOG__bin = (bb:BytesBuilder) => (p:j7.pLOG) => {

    
    marshall.str__bin (bb) (p.Location)
    
    marshall.str__bin (bb) (p.Content)
    
    marshall.str__bin (bb) (p.Sql)
}

export const LOG__bin = (bb:BytesBuilder) => (v:j7.LOG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pLOG__bin (bb) (v.p)
}

export const bin__pLOG = (bi:BinIndexed):j7.pLOG => {

    let p = pLOG_empty()
    p.Location = marshall.bin__str (bi)
    p.Content = marshall.bin__str (bi)
    p.Sql = marshall.bin__str (bi)

    return p
}


export const bin__LOG = (bi:BinIndexed):j7.LOG => {

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
export const addressAddressTypeEnum_Default = 0 // 默认
export const addressAddressTypeEnum_Biz = 1 // 机构
export const addressAddressTypeEnum_EndUser = 2 // 用户

export const pADDRESS_empty = (): j7.pADDRESS => {
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

export const ADDRESS_empty = (): j7.ADDRESS => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pADDRESS_empty() }
}

export const pBIZ_empty = (): j7.pBIZ => {
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

export const BIZ_empty = (): j7.BIZ => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pBIZ_empty() }
}

export const catCatStateEnum_Normal = 0 // 正常
export const catCatStateEnum_Hidden = 1 // 隐藏
export const catCatStateEnum_Obsolete = 2 // 过时

export const pCAT_empty = (): j7.pCAT => {
    return {
        Caption: "",
        Lang: 0,
        Zh: 0,
        Parent: 0,
        CatState: 0 }
}

export const CAT_empty = (): j7.CAT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCAT_empty() }
}

export const pCITY_empty = (): j7.pCITY => {
    return {
        Fullname: "",
        MetropolitanCode3IATA: "",
        NameEn: "",
        Country: 0,
        Place: 0,
        Icon: "",
        Tel: "" }
}

export const CITY_empty = (): j7.CITY => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCITY_empty() }
}

export const pCRY_empty = (): j7.pCRY => {
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

export const CRY_empty = (): j7.CRY => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCRY_empty() }
}

export const curCurTypeEnum_Fiat = 0 // 法币
export const curCurTypeEnum_Crypto = 1 // 数字币

export const pCUR_empty = (): j7.pCUR => {
    return {
        Code: "",
        Caption: "",
        IsHidden: true,
        IsSac: true,
        IsTransfer: true,
        IsCash: true,
        EnableReward: true,
        EnableOTC: true,
        Icon: "",
        CurType: 0,
        FloatDec: 0,
        AnchorRate: 0.0,
        Freezable: true,
        Authorizable: true,
        ChaninID: "",
        ChaninName: "",
        ContractAddress: "",
        WalletAddress: "",
        BaseRate: 0.0 }
}

export const CUR_empty = (): j7.CUR => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCUR_empty() }
}

export const euGenderEnum_Unknown = 0 // 未知
export const euGenderEnum_Male = 1 // 男
export const euGenderEnum_Female = 2 // 女

export const euStatusEnum_Normal = 0 // 正常
export const euStatusEnum_Frozen = 1 // 冻结
export const euStatusEnum_Terminated = 2 // 注销

export const euAdminEnum_None = 0 // 无
export const euAdminEnum_Admin = 1 // 管理员

export const euBizPartnerEnum_None = 0 // None
export const euBizPartnerEnum_Partner = 1 // 

export const euVerifyEnum_Normal = 0 // 常规
export const euVerifyEnum_Verified = 1 // 认证

export const pEU_empty = (): j7.pEU => {
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

export const EU_empty = (): j7.EU => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pEU_empty() }
}

export const fileEncryptEnum_None = 0 // 未加密
export const fileEncryptEnum_Sys = 1 // 系统加密
export const fileEncryptEnum_Usr = 2 // 用户加密

export const fileBindTypeEnum_Sys = 0 // 系统
export const fileBindTypeEnum_EndUser = 1 // 用户
export const fileBindTypeEnum_Biz = 2 // 机构
export const fileBindTypeEnum_Group = 3 // 群

export const fileStateEnum_Normal = 0 // 正常
export const fileStateEnum_Canceled = 1 // 已取消
export const fileStateEnum_Uploading = 2 // 上传中
export const fileStateEnum_PendingTranscode = 3 // 待媒体转码
export const fileStateEnum_PendingBlockchain = 4 // 待区块链同步
export const fileStateEnum_Deleted = 5 // 已删除

export const fileFileTypeEnum_Default = 0 // 默认
export const fileFileTypeEnum_Text = 1 // 文本
export const fileFileTypeEnum_Bin = 2 // 二进制
export const fileFileTypeEnum_Img = 3 // 图片
export const fileFileTypeEnum_Video = 4 // 视频

export const pFILE_empty = (): j7.pFILE => {
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

export const FILE_empty = (): j7.FILE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pFILE_empty() }
}

export const folderEncryptEnum_None = 0 // 未加密
export const folderEncryptEnum_Sys = 1 // 系统加密
export const folderEncryptEnum_Usr = 2 // 用户加密

export const folderBindTypeEnum_Sys = 0 // 系统
export const folderBindTypeEnum_EndUser = 1 // 用户
export const folderBindTypeEnum_Biz = 2 // 机构
export const folderBindTypeEnum_Group = 3 // 群

export const pFOLDER_empty = (): j7.pFOLDER => {
    return {
        Caption: "",
        Encrypt: 0,
        Bind: 0,
        BindType: 0,
        Parent: 0 }
}

export const FOLDER_empty = (): j7.FOLDER => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pFOLDER_empty() }
}

export const langTextDirectionEnum_Default = 0 // 默认
export const langTextDirectionEnum_RightToLeft = 1 // 从右往左排

export const pLANG_empty = (): j7.pLANG => {
    return {
        Code2: "",
        Caption: "",
        Native: "",
        Icon: "",
        IsBlank: true,
        IsLocale: true,
        IsContent: true,
        IsAutoTranslate: true,
        IsMiles: true,
        TextDirection: 0 }
}

export const LANG_empty = (): j7.LANG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pLANG_empty() }
}

export const pLOCALE_empty = (): j7.pLOCALE => {
    return {
        Code: "",
        Lang: 0,
        Zh: "",
        Text: "" }
}

export const LOCALE_empty = (): j7.LOCALE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pLOCALE_empty() }
}

export const csiTypeEnum_Normal = 0 // 常规
export const csiTypeEnum_ToplinesGlobalNews = 1 // 全站新闻置顶
export const csiTypeEnum_ToplinesGlobalPerson = 2 // 全站人物置顶
export const csiTypeEnum_ToplinesGlobalEvent = 3 // 全站事件置顶

export const pCSI_empty = (): j7.pCSI => {
    return {
        Type: 0,
        Lang: 0,
        Bind: 0 }
}

export const CSI_empty = (): j7.CSI => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCSI_empty() }
}

export const pCWC_empty = (): j7.pCWC => {
    return {
        Caption: "",
        ExternalId: 0,
        Icon: "",
        EU: 0,
        Biz: 0,
        Json: "" }
}

export const CWC_empty = (): j7.CWC => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCWC_empty() }
}

export const pINS_empty = (): j7.pINS => {
    return {
        DescTxt: "",
        Code: "",
        Caption: "",
        Long: 0,
        LongCode: "",
        Short: 0,
        ShortCode: "",
        Digit: 0,
        Ask: 0.0,
        Bid: 0.0,
        PriceCurrent: 0.0 }
}

export const INS_empty = (): j7.INS => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pINS_empty() }
}

export const ticketStateEnum_Pending = 0 // Pending
export const ticketStateEnum_Open = 1 // Open
export const ticketStateEnum_Closed = 2 // Closed
export const ticketStateEnum_Canceled = 3 // Canceled

export const ticketCloseTypeEnum_Manual = 0 // Manual
export const ticketCloseTypeEnum_TakeProfit = 1 // Take Profit
export const ticketCloseTypeEnum_StopLoss = 2 // Stop Loss
export const ticketCloseTypeEnum_Liquidation = 3 // Liquidation
export const ticketCloseTypeEnum_Expire = 4 // Expire

export const pTICKET_empty = (): j7.pTICKET => {
    return {
        Instrument: 0,
        InsCode: "",
        TicketNum: "",
        State: 0,
        Lot: 0.0,
        OpenPrice: 0.0,
        ClosePrice: 0.0,
        SL: 0.0,
        TP: 0.0,
        CloseType: 0,
        Cmt: "",
        Pendingat: new Date(),
        Opendat: new Date(),
        Closedat: new Date(),
        Canceledat: new Date() }
}

export const TICKET_empty = (): j7.TICKET => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pTICKET_empty() }
}

export const tacctStateEnum_Normal = 0 // 正常
export const tacctStateEnum_Frozen = 1 // 冻结
export const tacctStateEnum_FrozenInvisible = 2 // 冻结且不得查询
export const tacctStateEnum_LimitTrade = 3 // 限制交易
export const tacctStateEnum_FrozenTrade = 4 // 冻结交易

export const tacctTradeTypeEnum_Cash = 0 // 现金
export const tacctTradeTypeEnum_Match = 1 // 撮合
export const tacctTradeTypeEnum_Make = 2 // 做市

export const tacctRealDemoEnum_Real = 0 // 真实
export const tacctRealDemoEnum_Demo = 1 // 模拟

export const pTACCT_empty = (): j7.pTACCT => {
    return {
        SAC: 0,
        State: 0,
        TradeType: 0,
        RealDemo: 0,
        PnL: 0.0,
        Frozen: 0.0,
        Leverage: 0.0,
        Margin: 0.0,
        MarginCallRateWarning: 0.0,
        MarginCallRateWarningII: 0.0,
        MarginCallRateLiq: 0.0,
        PwdTrade: "",
        PwdReadonly: "",
        DescTxt: "" }
}

export const TACCT_empty = (): j7.TACCT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pTACCT_empty() }
}

export const pLOG_empty = (): j7.pLOG => {
    return {
        Location: "",
        Content: "",
        Sql: "" }
}

export const LOG_empty = (): j7.LOG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pLOG_empty() }
}
