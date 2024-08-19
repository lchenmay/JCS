import { glib } from '~/lib/glib'

import { wsjsonHandler, wsbinHandler } from '~/lib/store/wshandler'
import { BytesBuilder } from '~/lib/util/bin';

const tryJSONobj = (data: any) => {
    try {
        const re = JSON.parse(data)
        return re
    }
    catch (e) {
        return data
    }
}

const createWebSocket_base = (wsbinHandler: Function, wsjsonHandler: Function) => (wsurl: string) => (savetoRT: string = 'ws.conn'): WebSocket => {
    const ws = new WebSocket(wsurl);

    ws.onopen = (event) => {
        console.log('WebSocket connected');
    }
    ws.onclose = (event) => {
        console.log('WebSocket closed');
        setTimeout(() => { glib.setRT(savetoRT, glib.ws.createWebSocket(wsurl)(savetoRT)) }, 3000);
    }
    ws.onerror = (event) => {
        console.error('WebSocket error:', event);
    }
    ws.onmessage = (event) => {
        let msg = event.data
        switch (true) {
            case (typeof msg == 'string'):
                msg = tryJSONobj(msg)
                if (typeof msg !== 'string') {
                    wsjsonHandler(msg)
                } else {
                    console.log(msg)
                }
                break;
            default:
                wsbinHandler(msg)
                break;
        }
    }
    glib.setRT(savetoRT, ws)

    return ws;
}
export const createWebSocket = createWebSocket_base(wsbinHandler, wsjsonHandler)


export const disconnect = (ws: WsCtx = rt.wsctx) => {
    if (ws.ws && ws.ws.readyState === WebSocket.OPEN) {
        ws.ws.close()
    }
}

export const trySend = (x: WsCtx) => (e: number) => (msg: Record<string, any> | Object) => {

    if (!x) {
        x = rt.wsctx
    }

    if (x && x.ws.readyState === WebSocket.OPEN) {
        const bb = new BytesBuilder()
        glib.Mor.j7.Msg__bin(bb)({ e: 1, val: msg })
        x.ws.send(bb.bytes())
    } else {

    }
}

export const trySendx = (e: number) => (msg: Record<string, any> | Object) => {
    trySend(rt.wsctx)(e)(msg)
}

export const tryApiRequest = async (req: any, rep: Function) => {
    trySendx(MsgEnum.ApiRequest)(req)
}
