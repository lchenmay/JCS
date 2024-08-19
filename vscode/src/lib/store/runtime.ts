import { glib } from '~/lib/glib'
import { watch } from 'vue'
import { RuntimeData_empty } from '../shared/CustomMor'

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

export const init = (): Runtime => {
    return {
        host: {} as Host,
        wsctx: initWebSocketMeta(),
        router: glib.route.router,

        session: loadLS("session", ''),
        user: {},

        data: RuntimeData_empty()
    } as Runtime
}

export const InitWSConn = () => {
    glib.ws.createWebSocket(runtime.host.wsurl)()
    //glib.ws.autoping()
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



export const getRT = (key: string, obj: any = runtime) => {
    const keys = key.split('.')
    return keys.reduce((acc, curr) => acc?.[curr], obj);
}

export const setNested = (key: string, value: any, f_map: Function = (value: any) => { return value }, obj: any = runtime) => {
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
            //watch(() => rt[rtkey], (newValue, oldValue) => { prstLS(rtkey, newValue); }, { deep: true })
        }
    }
    const rtkeys = [
        'session', 'devmode'
    ]
    createwatch(rtkeys)
}