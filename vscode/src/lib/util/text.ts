export const decode = (src: string) => {
  let s = src.replace(/\\n/g,'\n')
  s = s.replace(/\\r/g,'\r')
  s = s.replace(/\\\\/g,'\\')
  return s
}

export const timestamp__str = (timestamp:any) => {
  let t = new Date(Number(timestamp))
  return t.getFullYear() + "/" + t.getMonth() + "/" + t.getDay()
      + " " + t.getHours() + ":" + t.getMinutes()
}

export const url__param = (name: string) => (url: string):string => {
const regex = new RegExp("[?&]" + name + "=([^&#]*)", "i")
const match = regex.exec(url)
if(match)
  return decodeURIComponent(match[1])
else
  return ""
}

export const __s = (str:string) => {
  let items = JSON.parse(str)
  return items[runtime.lang]
}
