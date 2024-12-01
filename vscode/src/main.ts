import App from './App.vue'
//import { beforeApp } from './beforeApp'
import { glib } from '~/lib/glib'
import '~/main.css'
import { ClientRuntime_empty } from './lib/shared/CustomMor'

globalThis.host = glib.vue.reactive(glib.host.initHost())
globalThis.clientRuntime = glib.vue.reactive(ClientRuntime_empty())
globalThis.runtime = glib.vue.reactive(glib.runtime.prepRuntime())
runtime.host = host

//runtime.user = glib.Mor.jcs.EuComplex_empty()

glib.runtime.createGlobalWatcher()


glib.notify.init()

//if (beforeApp) {beforeApp()}
const app = glib.vue.createApp(App)
app.use(runtime.router).mount('#app')
glib.route.router.push('/')

