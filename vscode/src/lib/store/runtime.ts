import { glib } from '~/lib/glib'
import { watch } from 'vue'

export const loadLS = (key: string, defaultv: string = '{}') => {
  const v = window.localStorage.getItem(key)
  if (v != undefined) {
    try { return JSON.parse(v) }
    catch {
      if (v) return v
      else return defaultv
    }
  }
  return defaultv
}

export const prstLS = (key: string, value: any) => {
  window.localStorage.setItem(key, JSON.stringify(value))
}

export const is_local = () => {
  return ["localhost"].includes(window.location.hostname)
}
export const is_gchain = () => {
  return ["gcha.in"].includes(window.location.hostname)
}
export const is_ctc = () => {
  return ["cryptradeclub.com","cpto.cc"].includes(window.location.hostname)
}

export const initRT = (): RT => {
  const s: RT = {
    host: initHost(),
    wsctx: initWebSocketMeta(),
    router: glib.route.router,

    session: loadLS("session", ''),
    user: {
      eu: glib.Mor.gchain.EU_empty(),
      clinks: {}
    },

    canvas: { canvasRef: null, gl: null },

    devmode: false,

    msgList: [],
    bizList: []
  } as RT
  return s
}

export const InitWSConn = () => {
  glib.ws.createWebSocket(rtxx.host.wsurl)()
  // glib.ws.autoping()
}

const initWebSocketMeta = () => {
  const res: WsCtx = {
    ws: {} as WebSocket,
    ping: null,
    autoping: true,
    pinginterval: 7000,
    sent: 0,
    sentsize: 0,
    rec: 0,
    recsize: 0
  }
  return res
}

const initHost = () => {
  const hostname = window.location.hostname

  const host: Host = {
    hostname: hostname,
    api: `${window.location.protocol}//${hostname}`,
    wsurl: `wss://${hostname}/ws/`,
    discordAPPID: "1254790111913181274",
    discordRedirect: `${window.location.protocol}//${window.location.host}/redirect/DISCORD`,
  }

  switch (host.hostname) {
    case 'localhost':
      host.hostname = 'gcha.in'
      host.api = 'http://localhost'
      host.wsurl = 'wss://localhost/'
      break
    case '127.0.0.1':
      host.hostname = '127.0.0.1'
      host.api = 'https://cpto.cc'
      host.wsurl = 'wss://gcha.in'
      break
  }

  return host
}


export const getRT = (key: string, obj: any = rtxx) => {
  const keys = key.split('.')
  return keys.reduce((acc, curr) => acc?.[curr], obj);
}

export const setNested = (key: string, value: any, f_map: Function = (value: any) => { return value }, obj: any = rtxx) => {
  const keys = key.split('.')
  const lastKey = keys.pop();
  if (!lastKey) return;

  const parent = keys.reduce((acc, curr) => acc?.[curr] ?? (acc[curr] = {}), obj);
  parent[lastKey] = f_map(value)
}

export const setRT = setNested



export const createGlobalWatcher = () => {
  const createwatch = (rtkeys: string[]) => {
    for (const rtkey of rtkeys) {
      watch(() => rtxx[rtkey], (newValue, oldValue) => { prstLS(rtkey, newValue); }, { deep: true })
    }
  }
  const rtkeys = [
    'session', 'devmode'
  ]
  createwatch(rtkeys)
}