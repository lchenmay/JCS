import { createMemoryHistory, createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'

import Index from '~/pages/index.vue'

const routes = [
  { path: '/', component: Index }
]

export const router = createRouter({
  history: createMemoryHistory(),
  routes
})
