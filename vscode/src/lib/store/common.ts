import { glib } from '~/lib/glib'

export const loader = async (url:string,post:any,h:Function) => {
  let rep = await glib.post(url, post)
  if(rep?.Er == 'OK')
    h(rep)
}

export const apiCheck = (api:string,params:string,gameCode:string) => {
    let host = "https://api-external.oddsjam.com/api/v2/"    
    let key = "0329d593-ffb6-4782-b855-0f010ba2915f"    
    let hypern = ""
    if(params.length > 0)
      hypern = "&"
    return host + api + "?" + params + hypern + "game_id=" + gameCode + "&key=" + key
}

export const compareDate = (positive:Date,negative:Date) => {
  let interval = positive.getTime() - negative.getTime()
  return {
    day: interval/(24 * 3600 * 1000),
    hours: interval/(3600 * 1000)
  }
}

export const compareNowDays = (positive:Date) => {
  let interval = positive.getTime() - (new Date()).getTime()
  return interval/(24 * 3600 * 1000)
}

export const compareNowHours = (positive:Date) => {
  let interval = positive.getTime() - (new Date()).getTime()
  return interval/(3600 * 1000)
}

export const filtering = 
  (filter:game.Filter) => 
  (league:string) => {
  let res = true

//  if(filter.sportCode != "" && filter.sportCode != sport)
//    res = false
  if(filter.league != "" && filter.league != league)
    res = false
//  if(filter.teamCode != "" && filter.teamCode != ha && filter.teamCode != aw)
//    res = false
  return res
}

export const oddsAmerican__rpp = (odds:number) => {
  let r = 0
  if(odds > 0.0)
    r = odds / 100.0
  else if(odds < 0.0)
    r = - 100.0 / odds
  return r
}

export const odds__impliedProb = (odds:number) => {
  let r = oddsAmerican__rpp(odds)
  let p = r + 1
  return 1/p
}

export const odds__ev = (odds:number,prob:number) => {
  let r = oddsAmerican__rpp(odds)
  let p = r + 1
  return p * prob - 1
}

export const odds__Kelly = (odds:number,prob:number) => {
  let r = oddsAmerican__rpp(odds)
  return (r * prob - (1 - prob))/r
}