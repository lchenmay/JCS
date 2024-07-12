declare global {
  namespace JSX {
    interface IntrinsicElements {
      [elem: string]: any
    }
  }
  namespace globalThis {
    var rtxx: RT
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
