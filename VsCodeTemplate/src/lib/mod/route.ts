import * as runtime from '~/lib/store/runtime'
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import * as hosts from '~/lib/store/host'
import originalRoutes from '~/generatedRoutes'

export const addTrailingSlash = () => {
  const currentPath = window.location.pathname;
  if (!currentPath.endsWith('/')) {
    const newPath = currentPath + '/';
    history.pushState(null, '', newPath);
  }
}


const updateRouteb = (originalRoutes: RouteRecordRaw[]) => (routeToUpdate: string): RouteRecordRaw[] => {
  const regex = new RegExp(`^/${routeToUpdate}(\/|$)`);
  const newRouteArray = originalRoutes.map((item: any) => ({ ...item }));
  for (let i = 0; i < newRouteArray.length; i++) {
    if (regex.test(newRouteArray[i].path)) {
      newRouteArray[i].path = newRouteArray[i].path.replace(regex, '/');
    }
  }
  return newRouteArray;
}

const updateRoute = updateRouteb(originalRoutes)


const initRoutes = (): RouteRecordRaw[] => {
  switch (true) {
    default:
      return updateRoute('jcs')
  }
}

const routes = initRoutes()

export const router = createRouter({
  history: createWebHistory('/'),
  scrollBehavior: (to, from, savePosition) => {
    if (savePosition) { return savePosition } else { return { top: 0 } }
  },
  routes
})

export const navigate = (href:string,name:string,id:number) => {
  window.location.href = href
  if(id == 0)
    router.push({ name: name, params: { id: id } })
  else
    router.push({ name: name })
}

router.beforeEach(async (to: any, from: any, next) => {
  next()
})

router.afterEach((to, from) => {
  // console.log(to)
})

