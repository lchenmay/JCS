declare global {

namespace j7 {


// [Tick]
export type Tick = {
ask:number,

bid:number,

timestamp:Date
}

// [Bar]
export type Bar = {
index:number,

timestamp:Date,

oa:number,

ob:number,

ca:number,

cb:number,

ha:number,

hb:number,

la:number,

lb:number
}

// [ZenStat]
export type ZenStat = {
barLength:SpotInStat,

timeInterval:SpotInStat,

dprice:SpotInStat,

dpriceConfirmed:SpotInStat,

dpriceRetreat:SpotInStat,

dpriceGain:SpotInStat
}

// [ZenMove]
export type ZenMove = {
ud:boolean,

bar1:Bar,

confirm:Bar,

retreato:Bar | null,

bar2:Bar
}

// [Acct]
export type Acct = {
acctnum:string,

working:{[key:string]: Dictionary<string,TICKET>},

history:{[key:string]: Dictionary<string,TICKET>},

dirMT4:string,

balance:number,

equity:number,

freeMargin:number,

leverage:number
}

// [EuComplex]
export type EuComplex = {
eu:EU
}

// [BizComplex]
export type BizComplex = {
biz:BIZ
}

// [TimeFrameHistory]
export type TimeFrameHistory = {
timeframe:string,

ask:number,

bid:number,

currentBar:Bar,

bars:Array<Bar>,

alpha:Array<float>,

beta:Array<float>,

gamma:Array<float>,

gammaBar:Array<Bar>,

moves:Array<ZenMove>,

zenAnchoro:Bar | null,

delta:Array<ZenMove>
}

// [TicketOnBars]
export type TicketOnBars = {
pendingBaro:Bar | null,

openBaro:Bar | null,

closeBaro:Bar | null,

cancelBaro:Bar | null,

pTicket:pTICKET
}

// [Sample]
export type Sample = {
hitting:Bar,

tobo:TicketOnBars | null,

starting:number,

ending:number,

tfh:TimeFrameHistory
}

// [TpbItem]
export type TpbItem = {
up:Stat,

dn:Stat
}

// [SampleSet]
export type SampleSet = {
zenSamples:Array<Sample>,

tpb:Array<TpbItem>,

countSample:number,

countTicket:number,

countOpened:number,

countCanceled:number,

countClosed:number,

countClosedWin:number,

countClosedLoss:number,

openingStat:SpotInStat,

closingStat:SpotInStat,

ev:number,

odds:number,

probWin:number
}

// [InsComplex]
export type InsComplex = {
zstat:ZenStat,

ticks:Array<Tick>,

tfh:TimeFrameHistory,

ss:SampleSet,

ins:INS
}

// [RuntimeData]
export type RuntimeData = {
odds:number,

rateSLTP:number,

win1:number,

win2:number,

timeframe:string,

portfolio:Array<string>,

curs:{[key:string]: CUR},

insxs:{[key:string]: InsComplex}
}

// [InsComplexClient]
export type InsComplexClient = {
zstat:ZenStat,

tobs:Array<TicketOnBars>,

tfh:TimeFrameHistory,

ins:INS
}

// [ClientRuntime]
export type ClientRuntime = {
acct:Acct,

rateSLTP:number,

insxcs:{[key:string]: InsComplexClient}
}

// [Msg]
export type Msg = {
e: number,

val: any
}

// [Er]
export type Er = {
e: number,

val: any
}

}

}

export {}
