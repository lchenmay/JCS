// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
import * as binOrm from './OrmMor'
const marshall = {...binCommon, ...binOrm }

export const enum FactEnum {
    Undefined = 0,//Undefined
}

export const enum MsgEnum {
    Heartbeat = 0,//Heartbeat
    ApiRequest = 1,//ApiRequest
    ApiResponse = 2,//ApiResponse
    SingleFact = 3,//SingleFact
    MultiFact = 4,//MultiFact
}

export const enum ErEnum {
    ApiNotExists = 0,//ApiNotExists
    InvalideParameter = 1,//InvalideParameter
    Unauthorized = 2,//Unauthorized
    NotAvailable = 3,//NotAvailable
    Internal = 4,//Internal
}


// [EuComplex] Structure

export const EuComplex_empty = (): game.EuComplex => { 
    return {
        eu: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pEU_empty() },
    } as game.EuComplex
}

export const EuComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.EU__bin (bb) (v.eu)
}

export const bin__EuComplex = (bi:BinIndexed):game.EuComplex => {

    return {
        eu: marshall.bin__EU (bi),
    }
}

// [SamplePoint] Structure

export const SamplePoint_empty = (): game.SamplePoint => { 
    return {
        caption: "",
        weight: 0.0,
        prob: 0.0,
        odds: [],
    } as game.SamplePoint
}

export const SamplePoint__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.str__bin (bb) (v.caption)
    marshall.float__bin (bb) (v.weight)
    marshall.float__bin (bb) (v.prob)
    
    marshall.array__bin (marshall.ODDS__bin) (bb) (v.odds)
}

export const bin__SamplePoint = (bi:BinIndexed):game.SamplePoint => {

    return {
        caption: marshall.bin__str (bi),
        weight: marshall.bin__float (bi),
        prob: marshall.bin__float (bi),
        odds: marshall.bin__array (marshall.bin__ODDS) (bi),
    }
}

// [ProbDist] Structure

export const ProbDist_empty = (): game.ProbDist => { 
    return {
        events: [],
        books: [],
        implieds: [],
        Iavgs: [],
        v: 0.0,
        lambda: 0.0,
        sigma: 0.0,
        dist: [],
    } as game.ProbDist
}

export const ProbDist__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.array__bin (marshall.str__bin) (bb) (v.events)
    
    marshall.array__bin (marshall.str__bin) (bb) (v.books)
    
    marshall.array__bin (marshall.float__bin) (bb) (v.implieds)
    
    marshall.array__bin (marshall.float__bin) (bb) (v.Iavgs)
    marshall.float__bin (bb) (v.v)
    marshall.float__bin (bb) (v.lambda)
    marshall.float__bin (bb) (v.sigma)
    
    marshall.array__bin (marshall.float__bin) (bb) (v.dist)
}

export const bin__ProbDist = (bi:BinIndexed):game.ProbDist => {

    return {
        events: marshall.bin__array (marshall.bin__str) (bi),
        books: marshall.bin__array (marshall.bin__str) (bi),
        implieds: marshall.bin__array (marshall.bin__float) (bi),
        Iavgs: marshall.bin__array (marshall.bin__float) (bi),
        v: marshall.bin__float (bi),
        lambda: marshall.bin__float (bi),
        sigma: marshall.bin__float (bi),
        dist: marshall.bin__array (marshall.bin__float) (bi),
    }
}

// [SSComplex] Structure

export const SSComplex_empty = (): game.SSComplex => { 
    return {
        sps: {},
        pd: ProbDist_empty(),
        glitchBooks: [],
        ss: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pGSS_empty() },
    } as game.SSComplex
}

export const SSComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.dict__bin (marshall.str__bin) (SamplePoint__bin) (bb) (v.sps)
    ProbDist__bin (bb) (v.pd)
    
    marshall.array__bin (marshall.str__bin) (bb) (v.glitchBooks)
    marshall.GSS__bin (bb) (v.ss)
}

export const bin__SSComplex = (bi:BinIndexed):game.SSComplex => {

    return {
        sps: marshall.bin__dict(marshall.bin__str) (bin__SamplePoint) (bi),
        pd: bin__ProbDist (bi),
        glitchBooks: marshall.bin__array (marshall.bin__str) (bi),
        ss: marshall.bin__GSS (bi),
    }
}

// [MarketComplex] Structure

export const MarketComplex_empty = (): game.MarketComplex => { 
    return {
        bookmarked: true,
        oddsCount: 0,
        sss: {},
        market: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pMARKET_empty() },
        bestArb: 0.0,
        bestEVprob: 0.0,
        bestEV: 0.0,
    } as game.MarketComplex
}

export const MarketComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.bool__bin (bb) (v.bookmarked)
    marshall.int32__bin (bb) (v.oddsCount)
    
    marshall.dict__bin (marshall.str__bin)(SSComplex__bin) (bb) (v.sss)
    marshall.MARKET__bin (bb) (v.market)
    marshall.float__bin (bb) (v.bestArb)
    marshall.float__bin (bb) (v.bestEVprob)
    marshall.float__bin (bb) (v.bestEV)
}

export const bin__MarketComplex = (bi:BinIndexed):game.MarketComplex => {

    return {
        bookmarked: marshall.bin__bool (bi),
        oddsCount: marshall.bin__int32 (bi),
        sss: marshall.bin__dict(marshall.bin__str)(bin__SSComplex) (bi),
        market: marshall.bin__MARKET (bi),
        bestArb: marshall.bin__float (bi),
        bestEVprob: marshall.bin__float (bi),
        bestEV: marshall.bin__float (bi),
    }
}

// [BetComboComplex] Structure

export const BetComboComplex_empty = (): game.BetComboComplex => { 
    return {
        gameo: null,
        homeTeamCaption: "",
        awayTeamCaption: "",
        bets: [],
        betcombo: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pBETCOMBO_empty() },
    } as game.BetComboComplex
}

export const BetComboComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.option__bin (marshall.GAME__bin) (bb) (v.gameo)
    marshall.str__bin (bb) (v.homeTeamCaption)
    marshall.str__bin (bb) (v.awayTeamCaption)
    
    marshall.array__bin (marshall.BET__bin) (bb) (v.bets)
    marshall.BETCOMBO__bin (bb) (v.betcombo)
}

export const bin__BetComboComplex = (bi:BinIndexed):game.BetComboComplex => {

    return {
        gameo: marshall.bin__option (marshall.bin__GAME) (bi),
        homeTeamCaption: marshall.bin__str (bi),
        awayTeamCaption: marshall.bin__str (bi),
        bets: marshall.bin__array (marshall.bin__BET) (bi),
        betcombo: marshall.bin__BETCOMBO (bi),
    }
}

// [GameComplex] Structure

export const GameComplex_empty = (): game.GameComplex => { 
    return {
        bookmarked: true,
        ggsbs: [],
        oddses: {},
        mcs: {},
        bestArb: 0.0,
        bestEVprob: 0.0,
        bestEV: 0.0,
        homeTeamCaption: "",
        awayTeamCaption: "",
        game: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pGAME_empty() },
    } as game.GameComplex
}

export const GameComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.bool__bin (bb) (v.bookmarked)
    
    marshall.array__bin (marshall.str__bin) (bb) (v.ggsbs)
    
    marshall.dict__bin (marshall.str__bin)(marshall.ODDS__bin) (bb) (v.oddses)
    
    marshall.dict__bin (marshall.str__bin)(MarketComplex__bin) (bb) (v.mcs)
    marshall.float__bin (bb) (v.bestArb)
    marshall.float__bin (bb) (v.bestEVprob)
    marshall.float__bin (bb) (v.bestEV)
    marshall.str__bin (bb) (v.homeTeamCaption)
    marshall.str__bin (bb) (v.awayTeamCaption)
    marshall.GAME__bin (bb) (v.game)
}

export const bin__GameComplex = (bi:BinIndexed):game.GameComplex => {

    return {
        bookmarked: marshall.bin__bool (bi),
        ggsbs: marshall.bin__array (marshall.bin__str) (bi),
        oddses: marshall.bin__dict(marshall.bin__str)(marshall.bin__ODDS) (bi),
        mcs: marshall.bin__dict(marshall.bin__str)(bin__MarketComplex) (bi),
        bestArb: marshall.bin__float (bi),
        bestEVprob: marshall.bin__float (bi),
        bestEV: marshall.bin__float (bi),
        homeTeamCaption: marshall.bin__str (bi),
        awayTeamCaption: marshall.bin__str (bi),
        game: marshall.bin__GAME (bi),
    }
}

// [Filter] Structure

export const Filter_empty = (): game.Filter => { 
    return {
        sport: "",
        league: "",
        team: "",
    } as game.Filter
}

export const Filter__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.str__bin (bb) (v.sport)
    marshall.str__bin (bb) (v.league)
    marshall.str__bin (bb) (v.team)
}

export const bin__Filter = (bi:BinIndexed):game.Filter => {

    return {
        sport: marshall.bin__str (bi),
        league: marshall.bin__str (bi),
        team: marshall.bin__str (bi),
    }
}

// [MomentComplex] Structure

export const MomentComplex_empty = (): game.MomentComplex => { 
    return {
        moment: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pMOMENT_empty() },
    } as game.MomentComplex
}

export const MomentComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.MOMENT__bin (bb) (v.moment)
}

export const bin__MomentComplex = (bi:BinIndexed):game.MomentComplex => {

    return {
        moment: marshall.bin__MOMENT (bi),
    }
}

// [Fact] Structure

export const Fact_empty = (): game.Fact => { 
    return {
    e:0, val:{}
    } as game.Fact
}

export const Fact__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            break
    }
}

export const bin__Fact = (bi:BinIndexed):game.Fact => {

    let v:game.Fact = { e:0, val:{} }
    v.e = marshall.bin__int32 (bi)
    switch (v.e) {
        case 0:
            break
    }
    return v
}

// [RuntimeData] Structure

export const RuntimeData_empty = (): game.RuntimeData => { 
    return {
        facts: [],
        bizes: {},
        sports: {},
        markets: {},
        sss: {},
        players: {},
        tournaments: {},
        leagues: {},
        sportsbooks: {},
        teams: {},
        odds: {},
        msgs: [],
        ms: [],
        bccs: [],
        gcs: {},
    } as game.RuntimeData
}

export const RuntimeData__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.array__bin (Fact__bin) (bb) (v.facts)
    marshall.dict__bin (marshall.str__bin) (marshall.BIZ__bin) (bb) (v.bizes)
    marshall.dict__bin (marshall.str__bin) (marshall.SPORT__bin) (bb) (v.sports)
    marshall.dict__bin (marshall.str__bin) (marshall.MARKET__bin) (bb) (v.markets)
    marshall.dict__bin (marshall.int64__bin) (marshall.GSS__bin) (bb) (v.sss)
    marshall.dict__bin (marshall.str__bin) (marshall.PLAYER__bin) (bb) (v.players)
    marshall.dict__bin (marshall.str__bin) (marshall.TOURNAMENT__bin) (bb) (v.tournaments)
    marshall.dict__bin (marshall.str__bin) (marshall.LEAGUE__bin) (bb) (v.leagues)
    marshall.dict__bin (marshall.str__bin) (marshall.SPORTSBOOK__bin) (bb) (v.sportsbooks)
    marshall.dict__bin (marshall.str__bin) (marshall.TEAM__bin) (bb) (v.teams)
    marshall.dict__bin (marshall.str__bin) (marshall.ODDS__bin) (bb) (v.odds)
    
    marshall.array__bin (marshall.MSG__bin) (bb) (v.msgs)
    
    marshall.array__bin (MomentComplex__bin) (bb) (v.ms)
    
    marshall.array__bin (BetComboComplex__bin) (bb) (v.bccs)
    marshall.dict__bin (marshall.str__bin) (GameComplex__bin) (bb) (v.gcs)
}

export const bin__RuntimeData = (bi:BinIndexed):game.RuntimeData => {

    return {
        facts: marshall.bin__array (bin__Fact) (bi),
        bizes: marshall.bin__dict(marshall.bin__str) (marshall.bin__BIZ) (bi),
        sports: marshall.bin__dict(marshall.bin__str) (marshall.bin__SPORT) (bi),
        markets: marshall.bin__dict(marshall.bin__str) (marshall.bin__MARKET) (bi),
        sss: marshall.bin__dict(marshall.bin__int64) (marshall.bin__GSS) (bi),
        players: marshall.bin__dict(marshall.bin__str) (marshall.bin__PLAYER) (bi),
        tournaments: marshall.bin__dict(marshall.bin__str) (marshall.bin__TOURNAMENT) (bi),
        leagues: marshall.bin__dict(marshall.bin__str) (marshall.bin__LEAGUE) (bi),
        sportsbooks: marshall.bin__dict(marshall.bin__str) (marshall.bin__SPORTSBOOK) (bi),
        teams: marshall.bin__dict(marshall.bin__str) (marshall.bin__TEAM) (bi),
        odds: marshall.bin__dict(marshall.bin__str) (marshall.bin__ODDS) (bi),
        msgs: marshall.bin__array (marshall.bin__MSG) (bi),
        ms: marshall.bin__array (bin__MomentComplex) (bi),
        bccs: marshall.bin__array (bin__BetComboComplex) (bi),
        gcs: marshall.bin__dict(marshall.bin__str) (bin__GameComplex) (bi),
    }
}

// [Msg] Structure

export const Msg_empty = (): game.Msg => { 
    return {
    e:0, val:{}
    } as game.Msg
}

export const Msg__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            break
        case 1:
            marshall.Json__bin (bb) (v.val)
            break
        case 2:
            marshall.Json__bin (bb) (v.val)
            break
        case 3:
            Fact__bin (bb) (v.val)
            break
        case 4:
            
            marshall.array__bin (Fact__bin) (bb) (v.val)
            break
    }
}

export const bin__Msg = (bi:BinIndexed):game.Msg => {

    let v:game.Msg = { e:0, val:{} }
    v.e = marshall.bin__int32 (bi)
    switch (v.e) {
        case 4:
            v.val = marshall.bin__array (bin__Fact) (bi) 
            break
        case 3:
            v.val = bin__Fact (bi) 
            break
        case 2:
            v.val = marshall.bin__Json (bi) 
            break
        case 1:
            v.val = marshall.bin__Json (bi) 
            break
        case 0:
            break
    }
    return v
}

// [Er] Structure

export const Er_empty = (): game.Er => { 
    return {
    e:0, val:{}
    } as game.Er
}

export const Er__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.e)
    switch (v.e) {
        case 0:
            break
        case 1:
            break
        case 2:
            break
        case 3:
            break
        case 4:
            break
    }
}

export const bin__Er = (bi:BinIndexed):game.Er => {

    let v:game.Er = { e:0, val:{} }
    v.e = marshall.bin__int32 (bi)
    switch (v.e) {
        case 4:
            break
        case 3:
            break
        case 2:
            break
        case 1:
            break
        case 0:
            break
    }
    return v
}