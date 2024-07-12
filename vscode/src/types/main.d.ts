declare global {
  namespace JSX {
    interface IntrinsicElements {
      [elem: string]: any
    }
  }
  namespace globalThis {
    var rtxx: gchainRT | ctcRT | RT
    var gchainRt: gchainRT
    var ctcRt: ctcRT
    var panelrt: {showpanel:boolean}
  }

  export interface RT  {
    host: Host
    wsctx: WsCtx
    router: Router
    session: string

    devmode: boolean
    msgList: NotifyItem[]

    canvas: {
      canvasRef:HTMLCanvasElement | null
      gl: WebGLRenderingContext | null
    }

    [key:string]: any
  }
  export interface gchainRT extends RT  {
    user: gchain.EuComplex
  } 
  export interface ctcRT extends RT {
    user: ctc.EuComplex
  } 

  export type WsCtx = {
    ws: WebSocket
    ping: NodeJS.Timeout | null
    autoping: boolean
    pinginterval: number
    sent: number
    sentsize: number
    rec: number
    recsize: number
  }

  export type Host = {
    hostname: string
    api: string
    wsurl: string
    discordAPPID: string
    discordRedirect: string
  }

  export type NotifyItem = {
    cdate?: number
    msg?: string
    label?: string
    bgcolor?: string
    url?: string
    expire?: number
    textColor?: string
  }

  export type BinIndexed = {
    bin: ArrayBuffer,
    index: number
}
}

export { }
