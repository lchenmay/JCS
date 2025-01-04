import { createMemoryHistory, createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

import Index from '~/pages/JCS.vue'
import Moments from '~/pages/Public/Moments.vue'
import Moment from '~/pages/Public/Moment.vue'

import Admin from '~/pages/Admin.vue'

const routes = [
  { path: '/', component: Index },
  { path: '/moments', component: Moments },
  { path: '/m/:id', component: Moment },

  { path: '/admin', component: Admin }
]

export const router = createRouter({
  history: createMemoryHistory(),
  routes
})

export const navigate = (href:string,name:string,id:number) => {
  console.log("href = " + href)
  console.log("href = " + name)
  console.log("name = " + id)
  window.location.href = href
  if(id != 0)
    router.push({ name: name, params: { id: id } })
  else
    router.push(name)
}

export const incomingRoute = () => {
  let path = window.location.pathname
  console.log(path)
  
  if(path.startsWith("/m/")){
    let id = path.substring(3)
    router.push({ name: 'm', params: { id: id }})
  }
  else
    router.push('/')
}
