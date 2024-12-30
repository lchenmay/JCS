import { glib } from '~/lib/glib'

export const loader = async (url:string,post:any,h:Function) => {
  post.session = runtime.session
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
  (filter:jcs.Filter) => 
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
