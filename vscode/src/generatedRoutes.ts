import { RouteRecordRaw } from 'vue-router';


  const routes: Array<RouteRecordRaw> = [
    {
      path: '//Admin',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: '-Admin',
        component:()=> import("~/pages//Admin.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/CodeRobot/Project',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'CodeRobot-Project',
        component:()=> import("~/pages/CodeRobot/Project.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/CodeRobot/Projects',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'CodeRobot-Projects',
        component:()=> import("~/pages/CodeRobot/Projects.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
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
      path: '/Common/Api',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-Api',
        component:()=> import("~/pages/Common/Api.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Common/Comp',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-Comp',
        component:()=> import("~/pages/Common/Comp.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Common/Field',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-Field',
        component:()=> import("~/pages/Common/Field.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Common/Page',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-Page',
        component:()=> import("~/pages/Common/Page.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Common/Project',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-Project',
        component:()=> import("~/pages/Common/Project.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Common/Table',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-Table',
        component:()=> import("~/pages/Common/Table.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Common/Template',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-Template',
        component:()=> import("~/pages/Common/Template.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Common/VarType',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Common-VarType',
        component:()=> import("~/pages/Common/VarType.vue"),
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
    },
    {
      path: '//JCS',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: '-JCS',
        component:()=> import("~/pages//JCS.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Public/HomePage',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Public-HomePage',
        component:()=> import("~/pages/Public/HomePage.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Public/Moment',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Public-Moment',
        component:()=> import("~/pages/Public/Moment.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    },
    {
      path: '/Public/Moments',
      component: ()=>import ( "/src/layouts/jcs.vue" ),
      meta: {"layout":"jcs"},
      children: [{
        path: "",
        name: 'Public-Moments',
        component:()=> import("~/pages/Public/Moments.vue"),
        meta: {"layout":"jcs"},
        props: true
      }]
    }
  ];

  export default routes;
  