import { glib } from '~/lib/glib'

import { wsjsonHandler, wsbinHandler } from '~/lib/store/wshandler'
import { BytesBuilder } from '~/lib/util/bin'
import { MsgEnum } from '../shared/CustomMor'

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

export const disconnect = (ws: WsCtx = runtime.wsctx) => {
    if (ws.ws && ws.ws.readyState === WebSocket.OPEN) {
        ws.ws.close()
    }
}

export const tryApiRequest = async (req: any, rep: Function) => {
    let x = runtime.wsctx
    if (x && x.ws.readyState === WebSocket.OPEN) {

        let msg = {
            e: MsgEnum.ApiRequest as number,
            val: req
        } as game.Msg

        const bb = new BytesBuilder()
        glib.Mor.game.Msg__bin(bb)(msg)
        x.ws.send(bb.bytes())
    }
}
