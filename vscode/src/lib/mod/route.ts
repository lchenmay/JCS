import { createMemoryHistory, createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

import Index from '~/pages/JCS.vue'
import Moments from '~/pages/Public/Moments.vue'
import Moment from '~/pages/Public/Moment.vue'

import Admin from '~/pages/Admin.vue'

const routes = [
  { path: '/', component: Index },
  { path: '/moments', component: Moments },
  { name:'m', path: '/m/:id', component: Moment },

  { path: '/admin', component: Admin }
]

export const router = createRouter({
  history: createMemoryHistory(),
  routes
})

export const navigate = (href:string,name:string,id:number) => {

  let url = href

  window.location.href = url
  if(id != 0)
    router.push({ name: name, params: { id: id } })
  else
    router.push(name)
}

const pages = ['m','doc']

export const incomingRoute = () => {
  let path = window.location.pathname

  let hit = false
  pages.forEach((page:string) => {
    let pattern = "/" + page + "/"
    if(path.startsWith(pattern)){
      hit = true
      let id = path.substring(pattern.length)
      router.push({ name: page, params: { id: id }})
    }
  })

  if(hit == false){
    if(path.startsWith("/moments"))
      router.push('/moments')
    else if(path.startsWith("/admin"))
      router.push('/admin')
    else
      router.push('/')
  }
}
