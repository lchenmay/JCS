// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
const marshall = {...binCommon }


// [ADDRESS] Structure


export const pADDRESS__bin = (bb:BytesBuilder) => (p:game.pADDRESS) => {

    
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

export const ADDRESS__bin = (bb:BytesBuilder) => (v:game.ADDRESS) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pADDRESS__bin (bb) (v.p)
}

export const bin__pADDRESS = (bi:BinIndexed):game.pADDRESS => {

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


export const bin__ADDRESS = (bi:BinIndexed):game.ADDRESS => {

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


export const pBIZ__bin = (bb:BytesBuilder) => (p:game.pBIZ) => {

    
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

export const BIZ__bin = (bb:BytesBuilder) => (v:game.BIZ) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pBIZ__bin (bb) (v.p)
}

export const bin__pBIZ = (bi:BinIndexed):game.pBIZ => {

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


export const bin__BIZ = (bi:BinIndexed):game.BIZ => {

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


export const pCAT__bin = (bb:BytesBuilder) => (p:game.pCAT) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.int64__bin (bb) (p.Zh)
    
    marshall.int64__bin (bb) (p.Parent)
    
    marshall.int32__bin (bb) (p.CatState)
}

export const CAT__bin = (bb:BytesBuilder) => (v:game.CAT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCAT__bin (bb) (v.p)
}

export const bin__pCAT = (bi:BinIndexed):game.pCAT => {

    let p = pCAT_empty()
    p.Caption = marshall.bin__str (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.Zh = marshall.bin__int64 (bi)
    p.Parent = marshall.bin__int64 (bi)
    p.CatState = marshall.bin__int32 (bi)

    return p
}


export const bin__CAT = (bi:BinIndexed):game.CAT => {

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


export const pCITY__bin = (bb:BytesBuilder) => (p:game.pCITY) => {

    
    marshall.str__bin (bb) (p.Fullname)
    
    marshall.str__bin (bb) (p.MetropolitanCode3IATA)
    
    marshall.str__bin (bb) (p.NameEn)
    
    marshall.int64__bin (bb) (p.Country)
    
    marshall.int64__bin (bb) (p.Place)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.str__bin (bb) (p.Tel)
}

export const CITY__bin = (bb:BytesBuilder) => (v:game.CITY) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCITY__bin (bb) (v.p)
}

export const bin__pCITY = (bi:BinIndexed):game.pCITY => {

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


export const bin__CITY = (bi:BinIndexed):game.CITY => {

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


export const pCRY__bin = (bb:BytesBuilder) => (p:game.pCRY) => {

    
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

export const CRY__bin = (bb:BytesBuilder) => (v:game.CRY) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCRY__bin (bb) (v.p)
}

export const bin__pCRY = (bi:BinIndexed):game.pCRY => {

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


export const bin__CRY = (bi:BinIndexed):game.CRY => {

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


export const pEU__bin = (bb:BytesBuilder) => (p:game.pEU) => {

    
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

export const EU__bin = (bb:BytesBuilder) => (v:game.EU) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pEU__bin (bb) (v.p)
}

export const bin__pEU = (bi:BinIndexed):game.pEU => {

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


export const bin__EU = (bi:BinIndexed):game.EU => {

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


export const pCSI__bin = (bb:BytesBuilder) => (p:game.pCSI) => {

    
    marshall.int32__bin (bb) (p.Type)
    
    marshall.int64__bin (bb) (p.Lang)
    
    marshall.int64__bin (bb) (p.Bind)
}

export const CSI__bin = (bb:BytesBuilder) => (v:game.CSI) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCSI__bin (bb) (v.p)
}

export const bin__pCSI = (bi:BinIndexed):game.pCSI => {

    let p = pCSI_empty()
    p.Type = marshall.bin__int32 (bi)
    p.Lang = marshall.bin__int64 (bi)
    p.Bind = marshall.bin__int64 (bi)

    return p
}


export const bin__CSI = (bi:BinIndexed):game.CSI => {

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


export const pCWC__bin = (bb:BytesBuilder) => (p:game.pCWC) => {

    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.ExternalId)
    
    marshall.str__bin (bb) (p.Icon)
    
    marshall.int64__bin (bb) (p.EU)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.Json)
}

export const CWC__bin = (bb:BytesBuilder) => (v:game.CWC) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pCWC__bin (bb) (v.p)
}

export const bin__pCWC = (bi:BinIndexed):game.pCWC => {

    let p = pCWC_empty()
    p.Caption = marshall.bin__str (bi)
    p.ExternalId = marshall.bin__int64 (bi)
    p.Icon = marshall.bin__str (bi)
    p.EU = marshall.bin__int64 (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.Json = marshall.bin__str (bi)

    return p
}


export const bin__CWC = (bi:BinIndexed):game.CWC => {

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

// [BET] Structure


export const pBET__bin = (bb:BytesBuilder) => (p:game.pBET) => {

    
    marshall.bool__bin (bb) (p.IsClosed)
    
    marshall.int64__bin (bb) (p.Game)
    
    marshall.int64__bin (bb) (p.BetCombo)
    
    marshall.str__bin (bb) (p.GameCode)
    
    marshall.str__bin (bb) (p.GameCaption)
    
    marshall.int64__bin (bb) (p.Sportsbook)
    
    marshall.str__bin (bb) (p.SportsbookCode)
    
    marshall.str__bin (bb) (p.SampleSpaceCode)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.float__bin (bb) (p.Price)
    
    marshall.float__bin (bb) (p.Probability)
    
    marshall.float__bin (bb) (p.ExpectedPayout)
    
    marshall.float__bin (bb) (p.EV)
    
    marshall.float__bin (bb) (p.Bet)
    
    marshall.float__bin (bb) (p.Win)
    
    marshall.float__bin (bb) (p.Payout)
    
    marshall.float__bin (bb) (p.PayoutActual)
    
    marshall.int64__bin (bb) (p.Market)
    
    marshall.str__bin (bb) (p.MarketCode)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.str__bin (bb) (p.SportCode)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.str__bin (bb) (p.LeagueCode)
}

export const BET__bin = (bb:BytesBuilder) => (v:game.BET) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pBET__bin (bb) (v.p)
}

export const bin__pBET = (bi:BinIndexed):game.pBET => {

    let p = pBET_empty()
    p.IsClosed = marshall.bin__bool (bi)
    p.Game = marshall.bin__int64 (bi)
    p.BetCombo = marshall.bin__int64 (bi)
    p.GameCode = marshall.bin__str (bi)
    p.GameCaption = marshall.bin__str (bi)
    p.Sportsbook = marshall.bin__int64 (bi)
    p.SportsbookCode = marshall.bin__str (bi)
    p.SampleSpaceCode = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Price = marshall.bin__float (bi)
    p.Probability = marshall.bin__float (bi)
    p.ExpectedPayout = marshall.bin__float (bi)
    p.EV = marshall.bin__float (bi)
    p.Bet = marshall.bin__float (bi)
    p.Win = marshall.bin__float (bi)
    p.Payout = marshall.bin__float (bi)
    p.PayoutActual = marshall.bin__float (bi)
    p.Market = marshall.bin__int64 (bi)
    p.MarketCode = marshall.bin__str (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.SportCode = marshall.bin__str (bi)
    p.League = marshall.bin__int64 (bi)
    p.LeagueCode = marshall.bin__str (bi)

    return p
}


export const bin__BET = (bi:BinIndexed):game.BET => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pBET (bi)
    }
}

// [BETCOMBO] Structure


export const pBETCOMBO__bin = (bb:BytesBuilder) => (p:game.pBETCOMBO) => {

    
    marshall.int32__bin (bb) (p.Type)
    
    marshall.int32__bin (bb) (p.State)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Strike)
    
    marshall.int64__bin (bb) (p.Game)
    
    marshall.str__bin (bb) (p.GameCode)
    
    marshall.float__bin (bb) (p.Investment)
    
    marshall.float__bin (bb) (p.Payout)
    
    marshall.float__bin (bb) (p.PayoutActual)
    
    marshall.float__bin (bb) (p.PnL)
    
    marshall.int64__bin (bb) (p.Market)
    
    marshall.str__bin (bb) (p.MarketCode)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.str__bin (bb) (p.SportCode)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.str__bin (bb) (p.LeagueCode)
    
    marshall.str__bin (bb) (p.SampleSpaceCode)
}

export const BETCOMBO__bin = (bb:BytesBuilder) => (v:game.BETCOMBO) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pBETCOMBO__bin (bb) (v.p)
}

export const bin__pBETCOMBO = (bi:BinIndexed):game.pBETCOMBO => {

    let p = pBETCOMBO_empty()
    p.Type = marshall.bin__int32 (bi)
    p.State = marshall.bin__int32 (bi)
    p.Caption = marshall.bin__str (bi)
    p.Strike = marshall.bin__int64 (bi)
    p.Game = marshall.bin__int64 (bi)
    p.GameCode = marshall.bin__str (bi)
    p.Investment = marshall.bin__float (bi)
    p.Payout = marshall.bin__float (bi)
    p.PayoutActual = marshall.bin__float (bi)
    p.PnL = marshall.bin__float (bi)
    p.Market = marshall.bin__int64 (bi)
    p.MarketCode = marshall.bin__str (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.SportCode = marshall.bin__str (bi)
    p.League = marshall.bin__int64 (bi)
    p.LeagueCode = marshall.bin__str (bi)
    p.SampleSpaceCode = marshall.bin__str (bi)

    return p
}


export const bin__BETCOMBO = (bi:BinIndexed):game.BETCOMBO => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pBETCOMBO (bi)
    }
}

// [GAME] Structure


export const pGAME__bin = (bb:BytesBuilder) => (p:game.pGAME) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.str__bin (bb) (p.SportCode)
    
    marshall.DateTime__bin (bb) (p.Sync)
    
    marshall.int64__bin (bb) (p.HomeTeam)
    
    marshall.int64__bin (bb) (p.AwayTeam)
    
    marshall.str__bin (bb) (p.HomeTeamCode)
    
    marshall.str__bin (bb) (p.AwayTeamCode)
    
    marshall.bool__bin (bb) (p.IsLive)
    
    marshall.DateTime__bin (bb) (p.StartTime)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.str__bin (bb) (p.LeagueCode)
    
    marshall.int64__bin (bb) (p.Tournament)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const GAME__bin = (bb:BytesBuilder) => (v:game.GAME) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pGAME__bin (bb) (v.p)
}

export const bin__pGAME = (bi:BinIndexed):game.pGAME => {

    let p = pGAME_empty()
    p.Code = marshall.bin__str (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.SportCode = marshall.bin__str (bi)
    p.Sync = marshall.bin__DateTime (bi)
    p.HomeTeam = marshall.bin__int64 (bi)
    p.AwayTeam = marshall.bin__int64 (bi)
    p.HomeTeamCode = marshall.bin__str (bi)
    p.AwayTeamCode = marshall.bin__str (bi)
    p.IsLive = marshall.bin__bool (bi)
    p.StartTime = marshall.bin__DateTime (bi)
    p.League = marshall.bin__int64 (bi)
    p.LeagueCode = marshall.bin__str (bi)
    p.Tournament = marshall.bin__int64 (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__GAME = (bi:BinIndexed):game.GAME => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pGAME (bi)
    }
}

// [GGSB] Structure


export const pGGSB__bin = (bb:BytesBuilder) => (p:game.pGGSB) => {

    
    marshall.int64__bin (bb) (p.Game)
    
    marshall.str__bin (bb) (p.GameCode)
    
    marshall.int64__bin (bb) (p.Sportsbook)
    
    marshall.str__bin (bb) (p.SportsbookCode)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const GGSB__bin = (bb:BytesBuilder) => (v:game.GGSB) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pGGSB__bin (bb) (v.p)
}

export const bin__pGGSB = (bi:BinIndexed):game.pGGSB => {

    let p = pGGSB_empty()
    p.Game = marshall.bin__int64 (bi)
    p.GameCode = marshall.bin__str (bi)
    p.Sportsbook = marshall.bin__int64 (bi)
    p.SportsbookCode = marshall.bin__str (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__GGSB = (bi:BinIndexed):game.GGSB => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pGGSB (bi)
    }
}

// [LEAGUE] Structure


export const pLEAGUE__bin = (bb:BytesBuilder) => (p:game.pLEAGUE) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.str__bin (bb) (p.SportCode)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const LEAGUE__bin = (bb:BytesBuilder) => (v:game.LEAGUE) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pLEAGUE__bin (bb) (v.p)
}

export const bin__pLEAGUE = (bi:BinIndexed):game.pLEAGUE => {

    let p = pLEAGUE_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.SportCode = marshall.bin__str (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__LEAGUE = (bi:BinIndexed):game.LEAGUE => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pLEAGUE (bi)
    }
}

// [MARKET] Structure


export const pMARKET__bin = (bb:BytesBuilder) => (p:game.pMARKET) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const MARKET__bin = (bb:BytesBuilder) => (v:game.MARKET) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pMARKET__bin (bb) (v.p)
}

export const bin__pMARKET = (bi:BinIndexed):game.pMARKET => {

    let p = pMARKET_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__MARKET = (bi:BinIndexed):game.MARKET => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pMARKET (bi)
    }
}

// [MSG] Structure


export const pMSG__bin = (bb:BytesBuilder) => (p:game.pMSG) => {

    
    marshall.str__bin (bb) (p.GameCode)
    
    marshall.str__bin (bb) (p.MarketCode)
    
    marshall.str__bin (bb) (p.SampleSpaceCode)
    
    marshall.str__bin (bb) (p.Content)
}

export const MSG__bin = (bb:BytesBuilder) => (v:game.MSG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pMSG__bin (bb) (v.p)
}

export const bin__pMSG = (bi:BinIndexed):game.pMSG => {

    let p = pMSG_empty()
    p.GameCode = marshall.bin__str (bi)
    p.MarketCode = marshall.bin__str (bi)
    p.SampleSpaceCode = marshall.bin__str (bi)
    p.Content = marshall.bin__str (bi)

    return p
}


export const bin__MSG = (bi:BinIndexed):game.MSG => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pMSG (bi)
    }
}

// [ODDS] Structure


export const pODDS__bin = (bb:BytesBuilder) => (p:game.pODDS) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Name)
    
    marshall.bool__bin (bb) (p.Available)
    
    marshall.int64__bin (bb) (p.Game)
    
    marshall.str__bin (bb) (p.GameCode)
    
    marshall.str__bin (bb) (p.Link)
    
    marshall.int64__bin (bb) (p.Sportsbook)
    
    marshall.str__bin (bb) (p.SportsbookCode)
    
    marshall.float__bin (bb) (p.Price)
    
    marshall.float__bin (bb) (p.BetPoints)
    
    marshall.int64__bin (bb) (p.Market)
    
    marshall.str__bin (bb) (p.MarketCode)
    
    marshall.int64__bin (bb) (p.SampleSpace)
    
    marshall.str__bin (bb) (p.SampleSpaceCode)
    
    marshall.str__bin (bb) (p.PlayerCode)
    
    marshall.str__bin (bb) (p.Selection)
    
    marshall.str__bin (bb) (p.SelectionLine)
    
    marshall.float__bin (bb) (p.SelectionPoints)
    
    marshall.int32__bin (bb) (p.HomeAway)
    
    marshall.int32__bin (bb) (p.OverUnder)
    
    marshall.int32__bin (bb) (p.OddEven)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.str__bin (bb) (p.SportCode)
    
    marshall.int64__bin (bb) (p.HomeTeam)
    
    marshall.int64__bin (bb) (p.AwayTeam)
    
    marshall.str__bin (bb) (p.HomeTeamCode)
    
    marshall.str__bin (bb) (p.AwayTeamCode)
    
    marshall.bool__bin (bb) (p.IsLive)
    
    marshall.DateTime__bin (bb) (p.StartTime)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.str__bin (bb) (p.LeagueCode)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const ODDS__bin = (bb:BytesBuilder) => (v:game.ODDS) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pODDS__bin (bb) (v.p)
}

export const bin__pODDS = (bi:BinIndexed):game.pODDS => {

    let p = pODDS_empty()
    p.Code = marshall.bin__str (bi)
    p.Name = marshall.bin__str (bi)
    p.Available = marshall.bin__bool (bi)
    p.Game = marshall.bin__int64 (bi)
    p.GameCode = marshall.bin__str (bi)
    p.Link = marshall.bin__str (bi)
    p.Sportsbook = marshall.bin__int64 (bi)
    p.SportsbookCode = marshall.bin__str (bi)
    p.Price = marshall.bin__float (bi)
    p.BetPoints = marshall.bin__float (bi)
    p.Market = marshall.bin__int64 (bi)
    p.MarketCode = marshall.bin__str (bi)
    p.SampleSpace = marshall.bin__int64 (bi)
    p.SampleSpaceCode = marshall.bin__str (bi)
    p.PlayerCode = marshall.bin__str (bi)
    p.Selection = marshall.bin__str (bi)
    p.SelectionLine = marshall.bin__str (bi)
    p.SelectionPoints = marshall.bin__float (bi)
    p.HomeAway = marshall.bin__int32 (bi)
    p.OverUnder = marshall.bin__int32 (bi)
    p.OddEven = marshall.bin__int32 (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.SportCode = marshall.bin__str (bi)
    p.HomeTeam = marshall.bin__int64 (bi)
    p.AwayTeam = marshall.bin__int64 (bi)
    p.HomeTeamCode = marshall.bin__str (bi)
    p.AwayTeamCode = marshall.bin__str (bi)
    p.IsLive = marshall.bin__bool (bi)
    p.StartTime = marshall.bin__DateTime (bi)
    p.League = marshall.bin__int64 (bi)
    p.LeagueCode = marshall.bin__str (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__ODDS = (bi:BinIndexed):game.ODDS => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pODDS (bi)
    }
}

// [PLAYER] Structure


export const pPLAYER__bin = (bb:BytesBuilder) => (p:game.pPLAYER) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Team)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const PLAYER__bin = (bb:BytesBuilder) => (v:game.PLAYER) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pPLAYER__bin (bb) (v.p)
}

export const bin__pPLAYER = (bi:BinIndexed):game.pPLAYER => {

    let p = pPLAYER_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Team = marshall.bin__int64 (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.League = marshall.bin__int64 (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__PLAYER = (bi:BinIndexed):game.PLAYER => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pPLAYER (bi)
    }
}

// [GSS] Structure


export const pGSS__bin = (bb:BytesBuilder) => (p:game.pGSS) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.DateTime__bin (bb) (p.Sync)
    
    marshall.float__bin (bb) (p.BestArb)
    
    marshall.float__bin (bb) (p.BestEV)
    
    marshall.float__bin (bb) (p.BestEVprob)
    
    marshall.bool__bin (bb) (p.Bookmarked)
    
    marshall.bool__bin (bb) (p.EnableNotifyArb)
    
    marshall.bool__bin (bb) (p.EnableNotifyGlitch)
    
    marshall.int64__bin (bb) (p.Game)
    
    marshall.str__bin (bb) (p.GameCode)
    
    marshall.int64__bin (bb) (p.Market)
    
    marshall.str__bin (bb) (p.MarketCode)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.str__bin (bb) (p.SportCode)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.str__bin (bb) (p.LeagueCode)
}

export const GSS__bin = (bb:BytesBuilder) => (v:game.GSS) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pGSS__bin (bb) (v.p)
}

export const bin__pGSS = (bi:BinIndexed):game.pGSS => {

    let p = pGSS_empty()
    p.Code = marshall.bin__str (bi)
    p.Sync = marshall.bin__DateTime (bi)
    p.BestArb = marshall.bin__float (bi)
    p.BestEV = marshall.bin__float (bi)
    p.BestEVprob = marshall.bin__float (bi)
    p.Bookmarked = marshall.bin__bool (bi)
    p.EnableNotifyArb = marshall.bin__bool (bi)
    p.EnableNotifyGlitch = marshall.bin__bool (bi)
    p.Game = marshall.bin__int64 (bi)
    p.GameCode = marshall.bin__str (bi)
    p.Market = marshall.bin__int64 (bi)
    p.MarketCode = marshall.bin__str (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.SportCode = marshall.bin__str (bi)
    p.League = marshall.bin__int64 (bi)
    p.LeagueCode = marshall.bin__str (bi)

    return p
}


export const bin__GSS = (bi:BinIndexed):game.GSS => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pGSS (bi)
    }
}

// [SPORT] Structure


export const pSPORT__bin = (bb:BytesBuilder) => (p:game.pSPORT) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Emoji)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const SPORT__bin = (bb:BytesBuilder) => (v:game.SPORT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pSPORT__bin (bb) (v.p)
}

export const bin__pSPORT = (bi:BinIndexed):game.pSPORT => {

    let p = pSPORT_empty()
    p.Code = marshall.bin__str (bi)
    p.Emoji = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__SPORT = (bi:BinIndexed):game.SPORT => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pSPORT (bi)
    }
}

// [SPORTSBOOK] Structure


export const pSPORTSBOOK__bin = (bb:BytesBuilder) => (p:game.pSPORTSBOOK) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.bool__bin (bb) (p.Available)
    
    marshall.bool__bin (bb) (p.Glitch)
    
    marshall.bool__bin (bb) (p.AllowNotify)
    
    marshall.float__bin (bb) (p.Weight)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const SPORTSBOOK__bin = (bb:BytesBuilder) => (v:game.SPORTSBOOK) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pSPORTSBOOK__bin (bb) (v.p)
}

export const bin__pSPORTSBOOK = (bi:BinIndexed):game.pSPORTSBOOK => {

    let p = pSPORTSBOOK_empty()
    p.Code = marshall.bin__str (bi)
    p.Available = marshall.bin__bool (bi)
    p.Glitch = marshall.bin__bool (bi)
    p.AllowNotify = marshall.bin__bool (bi)
    p.Weight = marshall.bin__float (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__SPORTSBOOK = (bi:BinIndexed):game.SPORTSBOOK => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pSPORTSBOOK (bi)
    }
}

// [TEAM] Structure


export const pTEAM__bin = (bb:BytesBuilder) => (p:game.pTEAM) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.str__bin (bb) (p.Abbr)
    
    marshall.str__bin (bb) (p.City)
    
    marshall.str__bin (bb) (p.Mascot)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.str__bin (bb) (p.SportCode)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.str__bin (bb) (p.LeagueCode)
    
    marshall.str__bin (bb) (p.Logo)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const TEAM__bin = (bb:BytesBuilder) => (v:game.TEAM) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pTEAM__bin (bb) (v.p)
}

export const bin__pTEAM = (bi:BinIndexed):game.pTEAM => {

    let p = pTEAM_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Abbr = marshall.bin__str (bi)
    p.City = marshall.bin__str (bi)
    p.Mascot = marshall.bin__str (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.SportCode = marshall.bin__str (bi)
    p.League = marshall.bin__int64 (bi)
    p.LeagueCode = marshall.bin__str (bi)
    p.Logo = marshall.bin__str (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__TEAM = (bi:BinIndexed):game.TEAM => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pTEAM (bi)
    }
}

// [TOURNAMENT] Structure


export const pTOURNAMENT__bin = (bb:BytesBuilder) => (p:game.pTOURNAMENT) => {

    
    marshall.str__bin (bb) (p.Code)
    
    marshall.str__bin (bb) (p.Caption)
    
    marshall.int64__bin (bb) (p.Sport)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.DateTime__bin (bb) (p.StartTime)
    
    marshall.DateTime__bin (bb) (p.EndTime)
    
    marshall.str__bin (bb) (p.VenueName)
    
    marshall.str__bin (bb) (p.VenueLocation)
    
    marshall.int64__bin (bb) (p.Biz)
    
    marshall.str__bin (bb) (p.BizCode)
}

export const TOURNAMENT__bin = (bb:BytesBuilder) => (v:game.TOURNAMENT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pTOURNAMENT__bin (bb) (v.p)
}

export const bin__pTOURNAMENT = (bi:BinIndexed):game.pTOURNAMENT => {

    let p = pTOURNAMENT_empty()
    p.Code = marshall.bin__str (bi)
    p.Caption = marshall.bin__str (bi)
    p.Sport = marshall.bin__int64 (bi)
    p.League = marshall.bin__int64 (bi)
    p.StartTime = marshall.bin__DateTime (bi)
    p.EndTime = marshall.bin__DateTime (bi)
    p.VenueName = marshall.bin__str (bi)
    p.VenueLocation = marshall.bin__str (bi)
    p.Biz = marshall.bin__int64 (bi)
    p.BizCode = marshall.bin__str (bi)

    return p
}


export const bin__TOURNAMENT = (bi:BinIndexed):game.TOURNAMENT => {

    let ID = marshall.bin__int64 (bi)
    let Sort = marshall.bin__int64 (bi)
    let Createdat = marshall.bin__DateTime (bi)
    let Updatedat = marshall.bin__DateTime (bi)
    
    return {
        id: ID,
        sort: Sort,
        createdat: Createdat,
        updatedat: Updatedat,
        p:  bin__pTOURNAMENT (bi)
    }
}

// [MOMENT] Structure


export const pMOMENT__bin = (bb:BytesBuilder) => (p:game.pMOMENT) => {

    
    marshall.str__bin (bb) (p.ShortText)
    
    marshall.str__bin (bb) (p.Fulltext)
    
    marshall.int32__bin (bb) (p.Type)
    
    marshall.str__bin (bb) (p.GameCode)
    
    marshall.str__bin (bb) (p.MarketCode)
    
    marshall.int64__bin (bb) (p.League)
    
    marshall.str__bin (bb) (p.LeagueCode)
    
    marshall.str__bin (bb) (p.SampleSpaceCode)
}

export const MOMENT__bin = (bb:BytesBuilder) => (v:game.MOMENT) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pMOMENT__bin (bb) (v.p)
}

export const bin__pMOMENT = (bi:BinIndexed):game.pMOMENT => {

    let p = pMOMENT_empty()
    p.ShortText = marshall.bin__str (bi)
    p.Fulltext = marshall.bin__str (bi)
    p.Type = marshall.bin__int32 (bi)
    p.GameCode = marshall.bin__str (bi)
    p.MarketCode = marshall.bin__str (bi)
    p.League = marshall.bin__int64 (bi)
    p.LeagueCode = marshall.bin__str (bi)
    p.SampleSpaceCode = marshall.bin__str (bi)

    return p
}


export const bin__MOMENT = (bi:BinIndexed):game.MOMENT => {

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


export const pLOG__bin = (bb:BytesBuilder) => (p:game.pLOG) => {

    
    marshall.str__bin (bb) (p.Location)
    
    marshall.str__bin (bb) (p.Content)
    
    marshall.str__bin (bb) (p.Sql)
}

export const LOG__bin = (bb:BytesBuilder) => (v:game.LOG) => {
    marshall.int64__bin (bb) (v.id)
    marshall.int64__bin (bb) (v.sort)
    marshall.DateTime__bin (bb) (v.createdat)
    marshall.DateTime__bin (bb) (v.updatedat)

    pLOG__bin (bb) (v.p)
}

export const bin__pLOG = (bi:BinIndexed):game.pLOG => {

    let p = pLOG_empty()
    p.Location = marshall.bin__str (bi)
    p.Content = marshall.bin__str (bi)
    p.Sql = marshall.bin__str (bi)

    return p
}


export const bin__LOG = (bi:BinIndexed):game.LOG => {

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
export const pADDRESS_empty = (): game.pADDRESS => {
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

export const ADDRESS_empty = (): game.ADDRESS => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pADDRESS_empty() }
}

export const pBIZ_empty = (): game.pBIZ => {
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

export const BIZ_empty = (): game.BIZ => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pBIZ_empty() }
}

export const pCAT_empty = (): game.pCAT => {
    return {
        Caption: "",
        Lang: 0,
        Zh: 0,
        Parent: 0,
        CatState: 0 }
}

export const CAT_empty = (): game.CAT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCAT_empty() }
}

export const pCITY_empty = (): game.pCITY => {
    return {
        Fullname: "",
        MetropolitanCode3IATA: "",
        NameEn: "",
        Country: 0,
        Place: 0,
        Icon: "",
        Tel: "" }
}

export const CITY_empty = (): game.CITY => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCITY_empty() }
}

export const pCRY_empty = (): game.pCRY => {
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

export const CRY_empty = (): game.CRY => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCRY_empty() }
}

export const pEU_empty = (): game.pEU => {
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

export const EU_empty = (): game.EU => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pEU_empty() }
}

export const pCSI_empty = (): game.pCSI => {
    return {
        Type: 0,
        Lang: 0,
        Bind: 0 }
}

export const CSI_empty = (): game.CSI => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCSI_empty() }
}

export const pCWC_empty = (): game.pCWC => {
    return {
        Caption: "",
        ExternalId: 0,
        Icon: "",
        EU: 0,
        Biz: 0,
        Json: "" }
}

export const CWC_empty = (): game.CWC => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pCWC_empty() }
}

export const pBET_empty = (): game.pBET => {
    return {
        IsClosed: true,
        Game: 0,
        BetCombo: 0,
        GameCode: "",
        GameCaption: "",
        Sportsbook: 0,
        SportsbookCode: "",
        SampleSpaceCode: "",
        Caption: "",
        Price: 0.0,
        Probability: 0.0,
        ExpectedPayout: 0.0,
        EV: 0.0,
        Bet: 0.0,
        Win: 0.0,
        Payout: 0.0,
        PayoutActual: 0.0,
        Market: 0,
        MarketCode: "",
        Sport: 0,
        SportCode: "",
        League: 0,
        LeagueCode: "" }
}

export const BET_empty = (): game.BET => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pBET_empty() }
}

export const pBETCOMBO_empty = (): game.pBETCOMBO => {
    return {
        Type: 0,
        State: 0,
        Caption: "",
        Strike: 0,
        Game: 0,
        GameCode: "",
        Investment: 0.0,
        Payout: 0.0,
        PayoutActual: 0.0,
        PnL: 0.0,
        Market: 0,
        MarketCode: "",
        Sport: 0,
        SportCode: "",
        League: 0,
        LeagueCode: "",
        SampleSpaceCode: "" }
}

export const BETCOMBO_empty = (): game.BETCOMBO => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pBETCOMBO_empty() }
}

export const pGAME_empty = (): game.pGAME => {
    return {
        Code: "",
        Sport: 0,
        SportCode: "",
        Sync: new Date(),
        HomeTeam: 0,
        AwayTeam: 0,
        HomeTeamCode: "",
        AwayTeamCode: "",
        IsLive: true,
        StartTime: new Date(),
        League: 0,
        LeagueCode: "",
        Tournament: 0,
        Biz: 0,
        BizCode: "" }
}

export const GAME_empty = (): game.GAME => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pGAME_empty() }
}

export const pGGSB_empty = (): game.pGGSB => {
    return {
        Game: 0,
        GameCode: "",
        Sportsbook: 0,
        SportsbookCode: "",
        Biz: 0,
        BizCode: "" }
}

export const GGSB_empty = (): game.GGSB => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pGGSB_empty() }
}

export const pLEAGUE_empty = (): game.pLEAGUE => {
    return {
        Code: "",
        Caption: "",
        Sport: 0,
        SportCode: "",
        Biz: 0,
        BizCode: "" }
}

export const LEAGUE_empty = (): game.LEAGUE => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pLEAGUE_empty() }
}

export const pMARKET_empty = (): game.pMARKET => {
    return {
        Code: "",
        Caption: "",
        Biz: 0,
        BizCode: "" }
}

export const MARKET_empty = (): game.MARKET => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pMARKET_empty() }
}

export const pMSG_empty = (): game.pMSG => {
    return {
        GameCode: "",
        MarketCode: "",
        SampleSpaceCode: "",
        Content: "" }
}

export const MSG_empty = (): game.MSG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pMSG_empty() }
}

export const pODDS_empty = (): game.pODDS => {
    return {
        Code: "",
        Name: "",
        Available: true,
        Game: 0,
        GameCode: "",
        Link: "",
        Sportsbook: 0,
        SportsbookCode: "",
        Price: 0.0,
        BetPoints: 0.0,
        Market: 0,
        MarketCode: "",
        SampleSpace: 0,
        SampleSpaceCode: "",
        PlayerCode: "",
        Selection: "",
        SelectionLine: "",
        SelectionPoints: 0.0,
        HomeAway: 0,
        OverUnder: 0,
        OddEven: 0,
        Sport: 0,
        SportCode: "",
        HomeTeam: 0,
        AwayTeam: 0,
        HomeTeamCode: "",
        AwayTeamCode: "",
        IsLive: true,
        StartTime: new Date(),
        League: 0,
        LeagueCode: "",
        Biz: 0,
        BizCode: "" }
}

export const ODDS_empty = (): game.ODDS => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pODDS_empty() }
}

export const pPLAYER_empty = (): game.pPLAYER => {
    return {
        Code: "",
        Caption: "",
        Team: 0,
        Sport: 0,
        League: 0,
        Biz: 0,
        BizCode: "" }
}

export const PLAYER_empty = (): game.PLAYER => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pPLAYER_empty() }
}

export const pGSS_empty = (): game.pGSS => {
    return {
        Code: "",
        Sync: new Date(),
        BestArb: 0.0,
        BestEV: 0.0,
        BestEVprob: 0.0,
        Bookmarked: true,
        EnableNotifyArb: true,
        EnableNotifyGlitch: true,
        Game: 0,
        GameCode: "",
        Market: 0,
        MarketCode: "",
        Sport: 0,
        SportCode: "",
        League: 0,
        LeagueCode: "" }
}

export const GSS_empty = (): game.GSS => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pGSS_empty() }
}

export const pSPORT_empty = (): game.pSPORT => {
    return {
        Code: "",
        Emoji: "",
        Caption: "",
        Biz: 0,
        BizCode: "" }
}

export const SPORT_empty = (): game.SPORT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pSPORT_empty() }
}

export const pSPORTSBOOK_empty = (): game.pSPORTSBOOK => {
    return {
        Code: "",
        Available: true,
        Glitch: true,
        AllowNotify: true,
        Weight: 0.0,
        Biz: 0,
        BizCode: "" }
}

export const SPORTSBOOK_empty = (): game.SPORTSBOOK => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pSPORTSBOOK_empty() }
}

export const pTEAM_empty = (): game.pTEAM => {
    return {
        Code: "",
        Caption: "",
        Abbr: "",
        City: "",
        Mascot: "",
        Sport: 0,
        SportCode: "",
        League: 0,
        LeagueCode: "",
        Logo: "",
        Biz: 0,
        BizCode: "" }
}

export const TEAM_empty = (): game.TEAM => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pTEAM_empty() }
}

export const pTOURNAMENT_empty = (): game.pTOURNAMENT => {
    return {
        Code: "",
        Caption: "",
        Sport: 0,
        League: 0,
        StartTime: new Date(),
        EndTime: new Date(),
        VenueName: "",
        VenueLocation: "",
        Biz: 0,
        BizCode: "" }
}

export const TOURNAMENT_empty = (): game.TOURNAMENT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pTOURNAMENT_empty() }
}

export const pMOMENT_empty = (): game.pMOMENT => {
    return {
        ShortText: "",
        Fulltext: "",
        Type: 0,
        GameCode: "",
        MarketCode: "",
        League: 0,
        LeagueCode: "",
        SampleSpaceCode: "" }
}

export const MOMENT_empty = (): game.MOMENT => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pMOMENT_empty() }
}

export const pLOG_empty = (): game.pLOG => {
    return {
        Location: "",
        Content: "",
        Sql: "" }
}

export const LOG_empty = (): game.LOG => {
    return {
        id: 0,
        createdat: new Date(),
        updatedat: new Date(),
        sort: 0,
        p: pLOG_empty() }
}
