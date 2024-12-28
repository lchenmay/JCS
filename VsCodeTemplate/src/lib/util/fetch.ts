
import axios from 'axios'

const checkUrl = (url:string) => {
  const getbase = () =>{
    if (runtime.host.api) 
      return runtime.host.api
    else 
      return `http://localhost`
  }

  if (!/^http(s?):/i.test(url)) 
    return getbase() + url
  else
    return url
}

const request = (method: "POST" | "GET") => async (url: string, data: Record<string, any>) => {

  url = checkUrl(url)

  const inits: RequestInit = {
    method: method,
    mode: 'cors',
    headers: { 'Content-Type': 'application/json' }
  }
  switch (method) {
    case "POST":
      if (runtime.session) { data['session'] = runtime.session }
      inits['body'] = JSON.stringify(data)
      break
    case "GET":
      const queryString = new URLSearchParams(data).toString()
      url = queryString ? `${url}?${queryString}` : url;
      break
  }
  return fetch(url, inits).then(res => { return res.json() }).catch(err => { console.error('Error:', err) })
}

export const upload = (file:any,dst:string) => {

  let formData = new FormData()
  formData.append("file", file)

  let url = checkUrl(dst)

  let reader = new FileReader()
  reader.onloadend = async() => {
    let buffer = reader.result
    let rep = await fetch(url,{
      method: 'POST',
      headers:{
        'Content-Type': 'application/octet-stream',
        'Filename': encodeURIComponent(file.name)
      },
      body: reader.result })
  }

  reader.readAsArrayBuffer(file)
}

export const post = request("POST")
export const get = request("GET")
