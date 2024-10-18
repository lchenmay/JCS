import { RouteRecordRaw } from 'vue-router';


  const routes: Array<RouteRecordRaw> = [
    {
      path: '//CodeRobotCenter',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: '-CodeRobotCenter',
        component:()=> import("~/pages//CodeRobotCenter.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
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
  