import App from './App.vue'
import { glib } from '~/lib/glib'
import '~/main.css'

globalThis.host = glib.vue.reactive(glib.host.initHost())
globalThis.runtime = glib.vue.reactive(glib.runtime.prepRuntime())
runtime.host = host

runtime.user = glib.Mor.jcs.EuComplex_empty()

glib.runtime.createGlobalWatcher()

glib.notify.init()

const app = glib.vue.createApp(App)
app.use(runtime.router).mount('#app')
glib.route.router.push('/index')

