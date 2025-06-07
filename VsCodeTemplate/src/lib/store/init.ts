import { glib } from '~/lib/glib'
import { watch } from 'vue'
import { MsgEnum } from '../shared/CustomMor'
import { initHost } from './host'
import { stringify } from 'postcss'

let wsOnOpen = (runtime: Runtime) => (event: any) => {
    console.log('WebSocket connected')
    runtime.wsctx.ws.send("Incoming")
}

let wsOnClose = (runtime: Runtime) => (event: any) => {
    console.log('WebSocket closed')
}

let wsOnError = (runtime: Runtime) => (event: any) => {
    console.log(event)
}

let wsOnMsg = (runtime: Runtime) => (event: any) => {
    let msg = event.data as j7.Msg
    console.log(msg)
    switch (msg.e as MsgEnum) {
        case MsgEnum.ApiResponse:
            break
    }
}

export const initRuntime = (runtime: Runtime) => {

    runtime.host = initHost()

    let ws = new WebSocket(runtime.host.wsurl)

    ws.onopen = wsOnOpen(runtime)
    ws.onclose = wsOnClose(runtime)
    ws.onerror = wsOnError(runtime)
    ws.onmessage = wsOnMsg(runtime)

    runtime.wsctx.ws = ws
}
