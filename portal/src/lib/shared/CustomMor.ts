// OrmMor.ts
import { BinIndexed, BytesBuilder } from "~/lib/util/bin"
import * as binCommon from '~/lib/util/bin'
import * as binOrm from './OrmMor'
const marshall = {...binCommon, ...binOrm }

export const enum MsgEnum {
    Heartbeat = 0,//Heartbeat
    ApiRequest = 1,//ApiRequest
    ApiResponse = 2,//ApiResponse
}

export const enum ErEnum {
    ApiNotExists = 0,//ApiNotExists
    InvalideParameter = 1,//InvalideParameter
    Unauthorized = 2,//Unauthorized
    NotAvailable = 3,//NotAvailable
    Internal = 4,//Internal
}


// [Tick] Structure

export const Tick_empty = (): j7.Tick => { 
    return {
        ask: 0.0,
        bid: 0.0,
        timestamp: new Date(),
    } as j7.Tick
}

export const Tick__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.float__bin (bb) (v.ask)
    marshall.float__bin (bb) (v.bid)
    marshall.DateTime__bin (bb) (v.timestamp)
}

export const bin__Tick = (bi:BinIndexed):j7.Tick => {

    return {
        ask: marshall.bin__float (bi),
        bid: marshall.bin__float (bi),
        timestamp: marshall.bin__DateTime (bi),
    }
}

// [Bar] Structure

export const Bar_empty = (): j7.Bar => { 
    return {
        index: 0,
        timestamp: new Date(),
        oa: 0.0,
        ob: 0.0,
        ca: 0.0,
        cb: 0.0,
        ha: 0.0,
        hb: 0.0,
        la: 0.0,
        lb: 0.0,
    } as j7.Bar
}

export const Bar__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.int32__bin (bb) (v.index)
    marshall.DateTime__bin (bb) (v.timestamp)
    marshall.float__bin (bb) (v.oa)
    marshall.float__bin (bb) (v.ob)
    marshall.float__bin (bb) (v.ca)
    marshall.float__bin (bb) (v.cb)
    marshall.float__bin (bb) (v.ha)
    marshall.float__bin (bb) (v.hb)
    marshall.float__bin (bb) (v.la)
    marshall.float__bin (bb) (v.lb)
}

export const bin__Bar = (bi:BinIndexed):j7.Bar => {

    return {
        index: marshall.bin__int32 (bi),
        timestamp: marshall.bin__DateTime (bi),
        oa: marshall.bin__float (bi),
        ob: marshall.bin__float (bi),
        ca: marshall.bin__float (bi),
        cb: marshall.bin__float (bi),
        ha: marshall.bin__float (bi),
        hb: marshall.bin__float (bi),
        la: marshall.bin__float (bi),
        lb: marshall.bin__float (bi),
    }
}

// [ZenStat] Structure

export const ZenStat_empty = (): j7.ZenStat => { 
    return {
        barLength: binCommon.SpotInStat_empty(),
        timeInterval: binCommon.SpotInStat_empty(),
        dprice: binCommon.SpotInStat_empty(),
        dpriceConfirmed: binCommon.SpotInStat_empty(),
        dpriceRetreat: binCommon.SpotInStat_empty(),
        dpriceGain: binCommon.SpotInStat_empty(),
    } as j7.ZenStat
}

export const ZenStat__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.SpotInStat__bin (bb) (v.barLength)
    marshall.SpotInStat__bin (bb) (v.timeInterval)
    marshall.SpotInStat__bin (bb) (v.dprice)
    marshall.SpotInStat__bin (bb) (v.dpriceConfirmed)
    marshall.SpotInStat__bin (bb) (v.dpriceRetreat)
    marshall.SpotInStat__bin (bb) (v.dpriceGain)
}

export const bin__ZenStat = (bi:BinIndexed):j7.ZenStat => {

    return {
        barLength: marshall.bin__SpotInStat (bi),
        timeInterval: marshall.bin__SpotInStat (bi),
        dprice: marshall.bin__SpotInStat (bi),
        dpriceConfirmed: marshall.bin__SpotInStat (bi),
        dpriceRetreat: marshall.bin__SpotInStat (bi),
        dpriceGain: marshall.bin__SpotInStat (bi),
    }
}

// [ZenMove] Structure

export const ZenMove_empty = (): j7.ZenMove => { 
    return {
        ud: true,
        bar1: Bar_empty(),
        confirm: Bar_empty(),
        retreato: null,
        bar2: Bar_empty(),
    } as j7.ZenMove
}

export const ZenMove__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.bool__bin (bb) (v.ud)
    Bar__bin (bb) (v.bar1)
    Bar__bin (bb) (v.confirm)
    marshall.option__bin (Bar__bin) (bb) (v.retreato)
    Bar__bin (bb) (v.bar2)
}

export const bin__ZenMove = (bi:BinIndexed):j7.ZenMove => {

    return {
        ud: marshall.bin__bool (bi),
        bar1: bin__Bar (bi),
        confirm: bin__Bar (bi),
        retreato: marshall.bin__option (bin__Bar) (bi),
        bar2: bin__Bar (bi),
    }
}

// [Acct] Structure

export const Acct_empty = (): j7.Acct => { 
    return {
        acctnum: "",
        working: {},
        history: {},
        dirMT4: "",
        balance: 0.0,
        equity: 0.0,
        freeMargin: 0.0,
        leverage: 0.0,
    } as j7.Acct
}

export const Acct__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.str__bin (bb) (v.acctnum)
    marshall.dict__bin (marshall.str__bin) (marshall.dict__bin (marshall.str__bin) (marshall.TICKET__bin)) (bb) (v.working)
    marshall.dict__bin (marshall.str__bin) (marshall.dict__bin (marshall.str__bin) (marshall.TICKET__bin)) (bb) (v.history)
    marshall.str__bin (bb) (v.dirMT4)
    marshall.float__bin (bb) (v.balance)
    marshall.float__bin (bb) (v.equity)
    marshall.float__bin (bb) (v.freeMargin)
    marshall.float__bin (bb) (v.leverage)
}

export const bin__Acct = (bi:BinIndexed):j7.Acct => {

    return {
        acctnum: marshall.bin__str (bi),
        working: marshall.bin__dict(marshall.bin__str) (marshall.bin__dict(marshall.bin__str) (marshall.bin__TICKET)) (bi),
        history: marshall.bin__dict(marshall.bin__str) (marshall.bin__dict(marshall.bin__str) (marshall.bin__TICKET)) (bi),
        dirMT4: marshall.bin__str (bi),
        balance: marshall.bin__float (bi),
        equity: marshall.bin__float (bi),
        freeMargin: marshall.bin__float (bi),
        leverage: marshall.bin__float (bi),
    }
}

// [EuComplex] Structure

export const EuComplex_empty = (): j7.EuComplex => { 
    return {
        eu: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pEU_empty() },
    } as j7.EuComplex
}

export const EuComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.EU__bin (bb) (v.eu)
}

export const bin__EuComplex = (bi:BinIndexed):j7.EuComplex => {

    return {
        eu: marshall.bin__EU (bi),
    }
}

// [BizComplex] Structure

export const BizComplex_empty = (): j7.BizComplex => { 
    return {
        biz: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pBIZ_empty() },
    } as j7.BizComplex
}

export const BizComplex__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.BIZ__bin (bb) (v.biz)
}

export const bin__BizComplex = (bi:BinIndexed):j7.BizComplex => {

    return {
        biz: marshall.bin__BIZ (bi),
    }
}

// [TimeFrameHistory] Structure

export const TimeFrameHistory_empty = (): j7.TimeFrameHistory => { 
    return {
        timeframe: "",
        ask: 0.0,
        bid: 0.0,
        currentBar: Bar_empty(),
        bars: [],
        alpha: [],
        beta: [],
        gamma: [],
        gammaBar: [],
        moves: [],
        zenAnchoro: null,
        delta: [],
    } as j7.TimeFrameHistory
}

export const TimeFrameHistory__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.str__bin (bb) (v.timeframe)
    marshall.float__bin (bb) (v.ask)
    marshall.float__bin (bb) (v.bid)
    Bar__bin (bb) (v.currentBar)
    
    marshall.array__bin (Bar__bin) (bb) (v.bars)
    
    marshall.array__bin (marshall.float__bin) (bb) (v.alpha)
    
    marshall.array__bin (marshall.float__bin) (bb) (v.beta)
    
    marshall.array__bin (marshall.float__bin) (bb) (v.gamma)
    
    marshall.array__bin (Bar__bin) (bb) (v.gammaBar)
    
    marshall.array__bin (ZenMove__bin) (bb) (v.moves)
    marshall.option__bin (Bar__bin) (bb) (v.zenAnchoro)
    
    marshall.array__bin (ZenMove__bin) (bb) (v.delta)
}

export const bin__TimeFrameHistory = (bi:BinIndexed):j7.TimeFrameHistory => {

    return {
        timeframe: marshall.bin__str (bi),
        ask: marshall.bin__float (bi),
        bid: marshall.bin__float (bi),
        currentBar: bin__Bar (bi),
        bars: marshall.bin__array (bin__Bar) (bi),
        alpha: marshall.bin__array (marshall.bin__float) (bi),
        beta: marshall.bin__array (marshall.bin__float) (bi),
        gamma: marshall.bin__array (marshall.bin__float) (bi),
        gammaBar: marshall.bin__array (bin__Bar) (bi),
        moves: marshall.bin__array (bin__ZenMove) (bi),
        zenAnchoro: marshall.bin__option (bin__Bar) (bi),
        delta: marshall.bin__array (bin__ZenMove) (bi),
    }
}

// [TicketOnBars] Structure

export const TicketOnBars_empty = (): j7.TicketOnBars => { 
    return {
        pendingBaro: null,
        openBaro: null,
        closeBaro: null,
        cancelBaro: null,
        pTicket: marshall.pTICKET_empty(),
    } as j7.TicketOnBars
}

export const TicketOnBars__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.option__bin (Bar__bin) (bb) (v.pendingBaro)
    marshall.option__bin (Bar__bin) (bb) (v.openBaro)
    marshall.option__bin (Bar__bin) (bb) (v.closeBaro)
    marshall.option__bin (Bar__bin) (bb) (v.cancelBaro)
    marshall.pTICKET__bin (bb) (v.pTicket)
}

export const bin__TicketOnBars = (bi:BinIndexed):j7.TicketOnBars => {

    return {
        pendingBaro: marshall.bin__option (bin__Bar) (bi),
        openBaro: marshall.bin__option (bin__Bar) (bi),
        closeBaro: marshall.bin__option (bin__Bar) (bi),
        cancelBaro: marshall.bin__option (bin__Bar) (bi),
        pTicket: marshall.bin__pTICKET (bi),
    }
}

// [Sample] Structure

export const Sample_empty = (): j7.Sample => { 
    return {
        hitting: Bar_empty(),
        tobo: null,
        starting: 0,
        ending: 0,
        tfh: TimeFrameHistory_empty(),
    } as j7.Sample
}

export const Sample__bin = (bb:BytesBuilder) => (v:any) => {

    Bar__bin (bb) (v.hitting)
    marshall.option__bin (TicketOnBars__bin) (bb) (v.tobo)
    marshall.int32__bin (bb) (v.starting)
    marshall.int32__bin (bb) (v.ending)
    TimeFrameHistory__bin (bb) (v.tfh)
}

export const bin__Sample = (bi:BinIndexed):j7.Sample => {

    return {
        hitting: bin__Bar (bi),
        tobo: marshall.bin__option (bin__TicketOnBars) (bi),
        starting: marshall.bin__int32 (bi),
        ending: marshall.bin__int32 (bi),
        tfh: bin__TimeFrameHistory (bi),
    }
}

// [TpbItem] Structure

export const TpbItem_empty = (): j7.TpbItem => { 
    return {
        up: binCommon.Stat_empty(),
        dn: binCommon.Stat_empty(),
    } as j7.TpbItem
}

export const TpbItem__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.Stat__bin (bb) (v.up)
    marshall.Stat__bin (bb) (v.dn)
}

export const bin__TpbItem = (bi:BinIndexed):j7.TpbItem => {

    return {
        up: marshall.bin__Stat (bi),
        dn: marshall.bin__Stat (bi),
    }
}

// [SampleSet] Structure

export const SampleSet_empty = (): j7.SampleSet => { 
    return {
        zenSamples: [],
        tpb: [],
        countSample: 0,
        countTicket: 0,
        countOpened: 0,
        countCanceled: 0,
        countClosed: 0,
        countClosedWin: 0,
        countClosedLoss: 0,
        openingStat: binCommon.SpotInStat_empty(),
        closingStat: binCommon.SpotInStat_empty(),
        ev: 0.0,
        odds: 0.0,
        probWin: 0.0,
    } as j7.SampleSet
}

export const SampleSet__bin = (bb:BytesBuilder) => (v:any) => {

    
    marshall.array__bin (Sample__bin) (bb) (v.zenSamples)
    
    marshall.array__bin (TpbItem__bin) (bb) (v.tpb)
    marshall.int32__bin (bb) (v.countSample)
    marshall.int32__bin (bb) (v.countTicket)
    marshall.int32__bin (bb) (v.countOpened)
    marshall.int32__bin (bb) (v.countCanceled)
    marshall.int32__bin (bb) (v.countClosed)
    marshall.int32__bin (bb) (v.countClosedWin)
    marshall.int32__bin (bb) (v.countClosedLoss)
    marshall.SpotInStat__bin (bb) (v.openingStat)
    marshall.SpotInStat__bin (bb) (v.closingStat)
    marshall.float__bin (bb) (v.ev)
    marshall.float__bin (bb) (v.odds)
    marshall.float__bin (bb) (v.probWin)
}

export const bin__SampleSet = (bi:BinIndexed):j7.SampleSet => {

    return {
        zenSamples: marshall.bin__array (bin__Sample) (bi),
        tpb: marshall.bin__array (bin__TpbItem) (bi),
        countSample: marshall.bin__int32 (bi),
        countTicket: marshall.bin__int32 (bi),
        countOpened: marshall.bin__int32 (bi),
        countCanceled: marshall.bin__int32 (bi),
        countClosed: marshall.bin__int32 (bi),
        countClosedWin: marshall.bin__int32 (bi),
        countClosedLoss: marshall.bin__int32 (bi),
        openingStat: marshall.bin__SpotInStat (bi),
        closingStat: marshall.bin__SpotInStat (bi),
        ev: marshall.bin__float (bi),
        odds: marshall.bin__float (bi),
        probWin: marshall.bin__float (bi),
    }
}

// [InsComplex] Structure

export const InsComplex_empty = (): j7.InsComplex => { 
    return {
        zstat: ZenStat_empty(),
        ticks: [],
        tfh: TimeFrameHistory_empty(),
        ss: SampleSet_empty(),
        ins: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pINS_empty() },
    } as j7.InsComplex
}

export const InsComplex__bin = (bb:BytesBuilder) => (v:any) => {

    ZenStat__bin (bb) (v.zstat)
    
    marshall.array__bin (Tick__bin) (bb) (v.ticks)
    TimeFrameHistory__bin (bb) (v.tfh)
    SampleSet__bin (bb) (v.ss)
    marshall.INS__bin (bb) (v.ins)
}

export const bin__InsComplex = (bi:BinIndexed):j7.InsComplex => {

    return {
        zstat: bin__ZenStat (bi),
        ticks: marshall.bin__array (bin__Tick) (bi),
        tfh: bin__TimeFrameHistory (bi),
        ss: bin__SampleSet (bi),
        ins: marshall.bin__INS (bi),
    }
}

// [RuntimeData] Structure

export const RuntimeData_empty = (): j7.RuntimeData => { 
    return {
        odds: 0.0,
        rateSLTP: 0.0,
        win1: 0,
        win2: 0,
        timeframe: "",
        portfolio: [],
        curs: {},
        insxs: {},
    } as j7.RuntimeData
}

export const RuntimeData__bin = (bb:BytesBuilder) => (v:any) => {

    marshall.float__bin (bb) (v.odds)
    marshall.float__bin (bb) (v.rateSLTP)
    marshall.int32__bin (bb) (v.win1)
    marshall.int32__bin (bb) (v.win2)
    marshall.str__bin (bb) (v.timeframe)
    
    marshall.array__bin (marshall.str__bin) (bb) (v.portfolio)
    
    marshall.dict__bin (marshall.str__bin)(marshall.CUR__bin) (bb) (v.curs)
    
    marshall.dict__bin (marshall.str__bin)(InsComplex__bin) (bb) (v.insxs)
}

export const bin__RuntimeData = (bi:BinIndexed):j7.RuntimeData => {

    return {
        odds: marshall.bin__float (bi),
        rateSLTP: marshall.bin__float (bi),
        win1: marshall.bin__int32 (bi),
        win2: marshall.bin__int32 (bi),
        timeframe: marshall.bin__str (bi),
        portfolio: marshall.bin__array (marshall.bin__str) (bi),
        curs: marshall.bin__dict(marshall.bin__str)(marshall.bin__CUR) (bi),
        insxs: marshall.bin__dict(marshall.bin__str)(bin__InsComplex) (bi),
    }
}

// [InsComplexClient] Structure

export const InsComplexClient_empty = (): j7.InsComplexClient => { 
    return {
        zstat: ZenStat_empty(),
        tobs: [],
        tfh: TimeFrameHistory_empty(),
        ins: { id: 0, sort: 0, createdat: new Date(), updatedat: new Date(), p: marshall.pINS_empty() },
    } as j7.InsComplexClient
}

export const InsComplexClient__bin = (bb:BytesBuilder) => (v:any) => {

    ZenStat__bin (bb) (v.zstat)
    
    marshall.array__bin (TicketOnBars__bin) (bb) (v.tobs)
    TimeFrameHistory__bin (bb) (v.tfh)
    marshall.INS__bin (bb) (v.ins)
}

export const bin__InsComplexClient = (bi:BinIndexed):j7.InsComplexClient => {

    return {
        zstat: bin__ZenStat (bi),
        tobs: marshall.bin__array (bin__TicketOnBars) (bi),
        tfh: bin__TimeFrameHistory (bi),
        ins: marshall.bin__INS (bi),
    }
}

// [ClientRuntime] Structure

export const ClientRuntime_empty = (): j7.ClientRuntime => { 
    return {
        acct: Acct_empty(),
        rateSLTP: 0.0,
        insxcs: {},
    } as j7.ClientRuntime
}

export const ClientRuntime__bin = (bb:BytesBuilder) => (v:any) => {

    Acct__bin (bb) (v.acct)
    marshall.float__bin (bb) (v.rateSLTP)
    
    marshall.dict__bin (marshall.str__bin)(InsComplexClient__bin) (bb) (v.insxcs)
}

export const bin__ClientRuntime = (bi:BinIndexed):j7.ClientRuntime => {

    return {
        acct: bin__Acct (bi),
        rateSLTP: marshall.bin__float (bi),
        insxcs: marshall.bin__dict(marshall.bin__str)(bin__InsComplexClient) (bi),
    }
}

// [Msg] Structure

export const Msg_empty = (): j7.Msg => { 
    return {
    e:0, val:{}
    } as j7.Msg
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
    }
}

export const bin__Msg = (bi:BinIndexed):j7.Msg => {

    let v:j7.Msg = { e:0, val:{} }
    v.e = marshall.bin__int32 (bi)
    switch (v.e) {
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

export const Er_empty = (): j7.Er => { 
    return {
    e:0, val:{}
    } as j7.Er
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

export const bin__Er = (bi:BinIndexed):j7.Er => {

    let v:j7.Er = { e:0, val:{} }
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