const request = (method: "POST" | "GET") => async (url: string, data: Record<string, any>) => {
  const getbase = () =>{
    if (runtime.host.api) return runtime.host.api
    else return `http://localhost`
  }
  if (!/^http(s?):/i.test(url)) {
    url = getbase() + url
  }
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

export const post = request("POST")
export const get = request("GET")