import App from './App.vue'
import { glib } from '~/lib/glib'
import '~/main.css'

globalThis.rtxx = glib.vue.reactive(glib.runtime.initRT())
globalThis.gchainRt = glib.vue.reactive(rtxx as gchainRT)
globalThis.ctcRt = glib.vue.reactive(rtxx as ctcRT)

gchainRt.user.eu = glib.Mor.gchain.EU_empty()
ctcRt.user.eu = glib.Mor.ctc.EU_empty()

glib.runtime.createGlobalWatcher()


//glib.runtime.InitWSConn()

glib.notify.init()

const app = glib.vue.createApp(App)
app.use(rtxx.router).mount('#app')


