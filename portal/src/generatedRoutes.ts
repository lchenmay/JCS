import { RouteRecordRaw } from 'vue-router';


  const routes: Array<RouteRecordRaw> = [
    {
      path: '//index',
      component: ()=>import ( "/src/layouts/blank.vue" ),
      meta: {"layout":"blank"},
      children: [{
        path: "",
        name: '-index',
        component:()=> import("~/pages//index.vue"),
        meta: {"layout":"blank"},
        props: true
      }]
    }
  ];

  export default routes;
  