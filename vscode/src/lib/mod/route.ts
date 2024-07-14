import * as runtime from '~/lib/store/runtime'
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import originalRoutes from '~/generatedRoutes'
import { glib } from '../glib';

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
    case runtime.is_domainname():
      return updateRoute(rtxx.host.projectname)

    case runtime.is_local():
      return updateRoute(rtxx.host.projectname)

    default:
      return updateRoute(rtxx.host.projectname)
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

router.beforeEach(async (to: any, from: any, next) => {
  next()
})

router.afterEach((to, from) => {
  // console.log(to)
})

