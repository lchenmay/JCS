import { RouteRecordRaw } from 'vue-router';


  const routes: Array<RouteRecordRaw> = [
    {
      path: '//index',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: '-index',
        component:()=> import("~/pages//index.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    }
  ];

  export default routes;
  