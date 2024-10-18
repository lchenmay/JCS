declare global {

namespace game {


// [EuComplex]
export type EuComplex = {
eu:EU
}

// [SamplePoint]
export type SamplePoint = {
caption:string,

weight:number,

prob:number,

odds:Array<ODDS>
}

// [ProbDist]
export type ProbDist = {
events:Array<string>,

books:Array<string>,

implieds:Array<float>,

Iavgs:Array<float>,

v:number,

lambda:number,

sigma:number,

dist:Array<float>
}

// [SSComplex]
export type SSComplex = {
sps:{[key:string]: SamplePoint},

pd:ProbDist,

glitchBooks:Array<string>,

ss:GSS
}

// [MarketComplex]
export type MarketComplex = {
bookmarked:boolean,

oddsCount:number,

sss:{[key:string]: SSComplex},

market:MARKET,

bestArb:number,

bestEVprob:number,

bestEV:number
}

// [BetComboComplex]
export type BetComboComplex = {
gameo:GAME | null,

homeTeamCaption:string,

awayTeamCaption:string,

bets:Array<BET>,

betcombo:BETCOMBO
}

// [GameComplex]
export type GameComplex = {
bookmarked:boolean,

ggsbs:Array<string>,

oddses:{[key:string]: ODDS},

mcs:{[key:string]: MarketComplex},

bestArb:number,

bestEVprob:number,

bestEV:number,

homeTeamCaption:string,

awayTeamCaption:string,

game:GAME
}

// [Filter]
export type Filter = {
sport:string,

league:string,

team:string
}

// [MomentComplex]
export type MomentComplex = {
moment:MOMENT
}

// [Fact]
export type Fact = {
e: number,

val: any
}

// [RuntimeData]
export type RuntimeData = {
facts:Array<Fact>,

bizes:{[key:string]: BIZ},

sports:{[key:string]: SPORT},

markets:{[key:string]: MARKET},

sss:{[key:int64]: GSS},

players:{[key:string]: PLAYER},

tournaments:{[key:string]: TOURNAMENT},

leagues:{[key:string]: LEAGUE},

sportsbooks:{[key:string]: SPORTSBOOK},

teams:{[key:string]: TEAM},

odds:{[key:string]: ODDS},

msgs:Array<MSG>,

ms:Array<MomentComplex>,

bccs:Array<BetComboComplex>,

gcs:{[key:string]: GameComplex}
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
