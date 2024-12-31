declare global {
    namespace JSX {
        interface IntrinsicElements {
            [elem: string]: any
        }
    }
    namespace globalThis {
        var host: Host
        var clientRuntime: [].ClientRuntime
        var runtime: Runtime
        var panelrt: { showpanel: boolean }
    }

    export interface Runtime {
        host: Host
        wsctx: WsCtx
        router: Router
        session: string

        user: [].[]
        data: [].ClientRuntime
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
}

export { }
