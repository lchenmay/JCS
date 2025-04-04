import App from './App.vue'
//import { beforeApp } from './beforeApp'
import { glib } from '~/lib/glib'
import '~/main.css'
import { ClientRuntime_empty } from './lib/shared/CustomMor'
import * as Route from './lib/mod/route'

globalThis.host = glib.vue.reactive(glib.host.initHost())
globalThis.clientRuntime = glib.vue.reactive(ClientRuntime_empty())
globalThis.runtime = glib.vue.reactive(glib.runtime.prepRuntime())
runtime.host = host

let lang = localStorage.getItem("runtime.lang")
if(lang)
  runtime.lang = lang
else
  runtime.lang = ""
runtime.session = "" + localStorage.getItem("runtime.session")
let localUser = localStorage.getItem("runtime.user")
if(localUser)
  runtime.user = JSON.parse(localUser) as [].[]
else
  runtime.user = glib.Mor.[]_empty()
glib.runtime.createGlobalWatcher()


glib.notify.init()

//if (beforeApp) {beforeApp()}
const app = glib.vue.createApp(App)
app.use(runtime.router).mount('#app')

if(!runtime.domainname){
  runtime.domainname = window.location.host
}

Route.incomingRoute()