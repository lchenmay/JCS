import { createMemoryHistory, createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

import Index from '~/pages/[].vue'

const routes = [
  { path: '/', component: Index },
]

export const router = createRouter({
  history: createMemoryHistory(),
  routes
})

export const navigate = (href:string,name:string,id:number) => {
  window.location.href = href
  if(id != 0)
    router.push({ name: name, params: { id: id } })
  else
    router.push(name)
}


export const incomingRoute = () => {
  let path = window.location.pathname
  
  if(path.startsWith("/m/")){
    let id = path.substring(3)
    router.push({ name: 'm', params: { id: id }})
  }
  else
    router.push('/')
}
