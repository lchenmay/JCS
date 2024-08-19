import { glib } from '~/lib/glib'
import { watch } from 'vue'
import { RuntimeData_empty } from '../shared/CustomMor'
import { initHost } from './host'

let wsOnOpen = (runtime: Runtime) => (event: any) => {
    console.log('WebSocket connected')
}

let wsOnClose = (runtime: Runtime) => (event: any) => {
    console.log('WebSocket closed')
}

let wsOnError = (runtime: Runtime) => (event: any) => {
    console.log(event)
}

let wsOnMsg = (runtime: Runtime) => (event: any) => {
    console.log(event)
}

export const initRuntime = (runtime: Runtime) => {

    runtime.host = initHost()

    console.log(runtime.host.wsurl)
    let ws = new WebSocket(runtime.host.wsurl)

    ws.onopen = wsOnOpen(runtime)
    ws.onclose = wsOnClose(runtime)
    ws.onerror = wsOnError(runtime)
    ws.onmessage = wsOnMsg(runtime)

    runtime.wsctx.ws = ws

}
