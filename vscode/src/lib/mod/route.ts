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
