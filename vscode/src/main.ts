import App from './App.vue'
import { glib } from '~/lib/glib'
import '~/main.css'

globalThis.rtxx = glib.vue.reactive(glib.runtime.initRT())

glib.runtime.createGlobalWatcher()


//glib.runtime.InitWSConn()

glib.notify.init()

const app = glib.vue.createApp(App)
app.use(rtxx.router).mount('#app')


