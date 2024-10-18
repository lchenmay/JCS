declare global {

namespace game {

// [Ca_Address] (ADDRESS)

const addressAddressTypeEnum_Default = 0 // 默认
const addressAddressTypeEnum_Biz = 1 // 机构
const addressAddressTypeEnum_EndUser = 2 // 用户

export type pADDRESS = {
    Caption: string
    Bind: number
    AddressType: number
    Line1: string
    Line2: string
    State: string
    County: string
    Town: string
    Contact: string
    Tel: string
    Email: string
    Zip: string
    City: number
    Country: number
    Remarks: string
}

export type ADDRESS = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pADDRESS
}

// [Ca_Biz] (BIZ)

export type pBIZ = {
    Code: string
    Caption: string
    Parent: number
    BasicAcct: number
    DescTxt: string
    Website: string
    Icon: string
    City: number
    Country: number
    Lang: number
    IsSocialPlatform: boolean
    IsCmsSource: boolean
    IsPayGateway: boolean
}

export type BIZ = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pBIZ
}

// [Ca_Cat] (CAT)

const catCatStateEnum_Normal = 0 // 正常
const catCatStateEnum_Hidden = 1 // 隐藏
const catCatStateEnum_Obsolete = 2 // 过时

export type pCAT = {
    Caption: string
    Lang: number
    Zh: number
    Parent: number
    CatState: number
}

export type CAT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCAT
}

// [Ca_City] (CITY)

export type pCITY = {
    Fullname: string
    MetropolitanCode3IATA: string
    NameEn: string
    Country: number
    Place: number
    Icon: string
    Tel: string
}

export type CITY = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCITY
}

// [Ca_Country] (CRY)

export type pCRY = {
    Code2: string
    Caption: string
    Fullname: string
    Icon: string
    Tel: string
    Cur: number
    Capital: number
    Place: number
    Lang: number
}

export type CRY = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCRY
}

// [Ca_EndUser] (EU)

const euGenderEnum_Unknown = 0 // 未知
const euGenderEnum_Male = 1 // 男
const euGenderEnum_Female = 2 // 女

const euStatusEnum_Normal = 0 // 正常
const euStatusEnum_Frozen = 1 // 冻结
const euStatusEnum_Terminated = 2 // 注销

const euAdminEnum_None = 0 // 无
const euAdminEnum_Admin = 1 // 管理员

const euBizPartnerEnum_None = 0 // None
const euBizPartnerEnum_Partner = 1 // 

const euVerifyEnum_Normal = 0 // 常规
const euVerifyEnum_Verified = 1 // 认证

export type pEU = {
    Caption: string
    Username: string
    SocialAuthBiz: number
    SocialAuthId: string
    SocialAuthAvatar: string
    Email: string
    Tel: string
    Gender: number
    Status: number
    Admin: number
    BizPartner: number
    Privilege: number
    Verify: number
    Pwd: string
    Online: boolean
    Icon: string
    Background: string
    BasicAcct: number
    Citizen: number
    Refer: string
    Referer: number
    Url: string
    About: string
}

export type EU = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pEU
}

// [Ca_SpecialItem] (CSI)

const csiTypeEnum_Normal = 0 // 常规
const csiTypeEnum_ToplinesGlobalNews = 1 // 全站新闻置顶
const csiTypeEnum_ToplinesGlobalPerson = 2 // 全站人物置顶
const csiTypeEnum_ToplinesGlobalEvent = 3 // 全站事件置顶

export type pCSI = {
    Type: number
    Lang: number
    Bind: number
}

export type CSI = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCSI
}

// [Ca_WebCredential] (CWC)

export type pCWC = {
    Caption: string
    ExternalId: number
    Icon: string
    EU: number
    Biz: number
    Json: string
}

export type CWC = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pCWC
}

// [Game_Bet] (BET)

export type pBET = {
    IsClosed: boolean
    Game: number
    BetCombo: number
    GameCode: string
    GameCaption: string
    Sportsbook: number
    SportsbookCode: string
    SampleSpaceCode: string
    Caption: string
    Price: number
    Probability: number
    ExpectedPayout: number
    EV: number
    Bet: number
    Win: number
    Payout: number
    PayoutActual: number
    Market: number
    MarketCode: string
    Sport: number
    SportCode: string
    League: number
    LeagueCode: string
}

export type BET = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pBET
}

// [Game_BetCombo] (BETCOMBO)

const betcomboTypeEnum_Arb = 0 // Arbitrage
const betcomboTypeEnum_EV = 1 // EV

const betcomboStateEnum_Scratch = 0 // Scratch
const betcomboStateEnum_Open = 1 // Open
const betcomboStateEnum_Closed = 2 // Closed

export type pBETCOMBO = {
    Type: number
    State: number
    Caption: string
    Strike: number
    Game: number
    GameCode: string
    Investment: number
    Payout: number
    PayoutActual: number
    PnL: number
    Market: number
    MarketCode: string
    Sport: number
    SportCode: string
    League: number
    LeagueCode: string
    SampleSpaceCode: string
}

export type BETCOMBO = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pBETCOMBO
}

// [Game_Game] (GAME)

export type pGAME = {
    Code: string
    Sport: number
    SportCode: string
    Sync: Date
    HomeTeam: number
    AwayTeam: number
    HomeTeamCode: string
    AwayTeamCode: string
    IsLive: boolean
    StartTime: Date
    League: number
    LeagueCode: string
    Tournament: number
    Biz: number
    BizCode: string
}

export type GAME = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pGAME
}

// [Game_GameSportsBook] (GGSB)

export type pGGSB = {
    Game: number
    GameCode: string
    Sportsbook: number
    SportsbookCode: string
    Biz: number
    BizCode: string
}

export type GGSB = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pGGSB
}

// [Game_League] (LEAGUE)

export type pLEAGUE = {
    Code: string
    Caption: string
    Sport: number
    SportCode: string
    Biz: number
    BizCode: string
}

export type LEAGUE = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pLEAGUE
}

// [Game_Market] (MARKET)

export type pMARKET = {
    Code: string
    Caption: string
    Biz: number
    BizCode: string
}

export type MARKET = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pMARKET
}

// [Game_Msg] (MSG)

export type pMSG = {
    GameCode: string
    MarketCode: string
    SampleSpaceCode: string
    Content: string
}

export type MSG = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pMSG
}

// [Game_Odds] (ODDS)

const oddsHomeAwayEnum_Home = 0 // Home
const oddsHomeAwayEnum_Away = 1 // Away
const oddsHomeAwayEnum_NA = 2 // NA

const oddsOverUnderEnum_Over = 0 // Over
const oddsOverUnderEnum_Under = 1 // Under
const oddsOverUnderEnum_Exact = 2 // Exact
const oddsOverUnderEnum_NA = 3 // NA

const oddsOddEvenEnum_Odd = 0 // Odd
const oddsOddEvenEnum_Even = 1 // Even
const oddsOddEvenEnum_NA = 2 // NA

export type pODDS = {
    Code: string
    Name: string
    Available: boolean
    Game: number
    GameCode: string
    Link: string
    Sportsbook: number
    SportsbookCode: string
    Price: number
    BetPoints: number
    Market: number
    MarketCode: string
    SampleSpace: number
    SampleSpaceCode: string
    PlayerCode: string
    Selection: string
    SelectionLine: string
    SelectionPoints: number
    HomeAway: number
    OverUnder: number
    OddEven: number
    Sport: number
    SportCode: string
    HomeTeam: number
    AwayTeam: number
    HomeTeamCode: string
    AwayTeamCode: string
    IsLive: boolean
    StartTime: Date
    League: number
    LeagueCode: string
    Biz: number
    BizCode: string
}

export type ODDS = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pODDS
}

// [Game_Player] (PLAYER)

export type pPLAYER = {
    Code: string
    Caption: string
    Team: number
    Sport: number
    League: number
    Biz: number
    BizCode: string
}

export type PLAYER = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pPLAYER
}

// [Game_SampleSpace] (GSS)

export type pGSS = {
    Code: string
    Sync: Date
    BestArb: number
    BestEV: number
    BestEVprob: number
    Bookmarked: boolean
    EnableNotifyArb: boolean
    EnableNotifyGlitch: boolean
    Game: number
    GameCode: string
    Market: number
    MarketCode: string
    Sport: number
    SportCode: string
    League: number
    LeagueCode: string
}

export type GSS = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pGSS
}

// [Game_Sport] (SPORT)

export type pSPORT = {
    Code: string
    Emoji: string
    Caption: string
    Biz: number
    BizCode: string
}

export type SPORT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pSPORT
}

// [Game_SportsBook] (SPORTSBOOK)

export type pSPORTSBOOK = {
    Code: string
    Available: boolean
    Glitch: boolean
    AllowNotify: boolean
    Weight: number
    Biz: number
    BizCode: string
}

export type SPORTSBOOK = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pSPORTSBOOK
}

// [Game_Team] (TEAM)

export type pTEAM = {
    Code: string
    Caption: string
    Abbr: string
    City: string
    Mascot: string
    Sport: number
    SportCode: string
    League: number
    LeagueCode: string
    Logo: string
    Biz: number
    BizCode: string
}

export type TEAM = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pTEAM
}

// [Game_Tournament] (TOURNAMENT)

export type pTOURNAMENT = {
    Code: string
    Caption: string
    Sport: number
    League: number
    StartTime: Date
    EndTime: Date
    VenueName: string
    VenueLocation: string
    Biz: number
    BizCode: string
}

export type TOURNAMENT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pTOURNAMENT
}

// [Social_Moment] (MOMENT)

const momentTypeEnum_NewGame = 0 // New Game
const momentTypeEnum_SampleSpaceProfit = 1 // Sample Space Profit

export type pMOMENT = {
    ShortText: string
    Fulltext: string
    Type: number
    GameCode: string
    MarketCode: string
    League: number
    LeagueCode: string
    SampleSpaceCode: string
}

export type MOMENT = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pMOMENT
}

// [Sys_Log] (LOG)

export type pLOG = {
    Location: string
    Content: string
    Sql: string
}

export type LOG = {
id:number
createdat:Date
updatedat:Date
sort:number
p:pLOG
}


}

}

export {}
