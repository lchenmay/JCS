
import { glib } from '~/lib/glib'

type authParams = {
  client_id: string
  response_type: string
  redirect_uri: string
  scope: string
}
type LoginOption = {
  biz: string
  code: string
  redirectUrl: string
}

export const SignOut = () => {
  window.localStorage.clear()
  location.reload()
}

export const biz__LoginOptions = (biz: string ="DISCORD"): LoginOption => {
  const options = {
    biz: biz.toUpperCase(),
    code: "",
    redirectUrl: rtxx.host.discordRedirect,
  }

  const urlParams = new URLSearchParams(window.location.search)
  options.code = urlParams.get('code')!
  return options
}
export const LoginOption__RT = async (options: LoginOption) => {
  const res = await glib.post('/api/public/auth', options)
  if (res.session) {
    glib.notify.aSuc("Login Suc")
    rtxx.session= String(res.session)
    if(res.ec) rtxx.user = res.ec
  } 
  return res
}



const authParams__URLQuery = (params: Record<string, any>) => {
  return Object.keys(params)
    .map(key => encodeURIComponent(key) + '=' + encodeURIComponent(params[key]))
    .join('&');
}

export const host__DiscordRedirectURL = (host: Host = rtxx.host) => {
  const p: authParams = {
    client_id: host.discordAPPID,
    response_type: "code",
    redirect_uri: host.discordRedirect,
    scope: "identify"
  }
  const params = authParams__URLQuery(p)
  return `https://discord.com/oauth2/authorize?${params}`
}