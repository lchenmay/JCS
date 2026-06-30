import App from './App.vue'
//import { beforeApp } from './beforeApp'
import { glib } from '~/lib/glib'
import '~/main.css'
import { ClientRuntime_empty } from './lib/shared/CustomMor'

globalThis.host = glib.vue.reactive(glib.host.initHost())
globalThis.clientRuntime = glib.vue.reactive(ClientRuntime_empty())
globalThis.runtime = glib.vue.reactive(glib.runtime.prepRuntime())
runtime.host = host

// 判断调试来源：调用 jcs-common 的 getDebugger()
runtime.debugger = glib.bin.getDebugger()
;(window as any).__DEBUGGER__ = runtime.debugger
console.log('[debugger] source:', runtime.debugger)

//runtime.user = glib.Mor.[]

glib.runtime.createGlobalWatcher()


glib.notify.init()

//if (beforeApp) {beforeApp()}
const app = glib.vue.createApp(App)
app.use(runtime.router).mount('#app')
glib.route.router.push('/')

