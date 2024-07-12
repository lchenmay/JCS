import { RouteRecordRaw } from 'vue-router';


  const routes: Array<RouteRecordRaw> = [
    {
      path: '/admin',
      component: ()=>import ( "/src/layouts/admin.vue" ),
      meta: {"layout":"admin"},
      children: [{
        path: "",
        name: 'admin',
        component:()=> import("~/pages/admin/index.vue"),
        meta: {"layout":"admin"},
        props: true
      }]
    },
    {
      path: '/admin/mdtohtml',
      component: ()=>import ( "/src/layouts/admin.vue" ),
      meta: {"layout":"admin"},
      children: [{
        path: "",
        name: 'admin-mdtohtml',
        component:()=> import("~/pages/admin/mdtohtml.vue"),
        meta: {"layout":"admin"},
        props: true
      }]
    },
    {
      path: '/ctc',
      component: ()=>import ( "/src/layouts/ctc.vue" ),
      meta: {"layout":"ctc"},
      children: [{
        path: "",
        name: 'ctc',
        component:()=> import("~/pages/ctc/index.vue"),
        meta: {"layout":"ctc"},
        props: true
      }]
    },
    {
      path: '/ctc/m/:all',
      component: ()=>import ( "/src/layouts/ctc.vue" ),
      meta: {"layout":"ctc"},
      children: [{
        path: "",
        name: 'ctc-m-all',
        component:()=> import("~/pages/ctc/m/[all].vue"),
        meta: {"layout":"ctc"},
        props: true
      }]
    },
    {
      path: '//error',
      component: ()=>import ( "/src/layouts/blank.vue" ),
      meta: {"layout":"blank"},
      children: [{
        path: "",
        name: '-error',
        component:()=> import("~/pages//error.vue"),
        meta: {"layout":"blank"},
        props: true
      }]
    },
    {
      path: '/fx/getog',
      component: ()=>import ( "/src/layouts/fx.vue" ),
      meta: {"layout":"fx"},
      children: [{
        path: "",
        name: 'fx-getog',
        component:()=> import("~/pages/fx/getog.vue"),
        meta: {"layout":"fx"},
        props: true
      }]
    },
    {
      path: '/fx',
      component: ()=>import ( "/src/layouts/fx.vue" ),
      meta: {"layout":"fx"},
      children: [{
        path: "",
        name: 'fx',
        component:()=> import("~/pages/fx/index.vue"),
        meta: {"layout":"fx"},
        props: true
      }]
    },
    {
      path: '/fx/t',
      component: ()=>import ( "/src/layouts/fx.vue" ),
      meta: {"layout":"fx"},
      children: [{
        path: "",
        name: 'fx-t',
        component:()=> import("~/pages/fx/t.vue"),
        meta: {"layout":"fx"},
        props: true
      }]
    },
    {
      path: '/fx/webgl',
      component: ()=>import ( "/src/layouts/fx.vue" ),
      meta: {"layout":"fx"},
      children: [{
        path: "",
        name: 'fx-webgl',
        component:()=> import("~/pages/fx/webgl/index.vue"),
        meta: {"layout":"fx"},
        props: true
      }]
    },
    {
      path: '/gchain/concept',
      component: ()=>import ( "/src/layouts/gchain.vue" ),
      meta: {"layout":"gchain"},
      children: [{
        path: "",
        name: 'gchain-concept',
        component:()=> import("~/pages/gchain/concept.vue"),
        meta: {"layout":"gchain"},
        props: true
      }]
    },
    {
      path: '/gchain',
      component: ()=>import ( "/src/layouts/gchain.vue" ),
      meta: {"layout":"gchain"},
      children: [{
        path: "",
        name: 'gchain',
        component:()=> import("~/pages/gchain/index.vue"),
        meta: {"layout":"gchain"},
        props: true
      }]
    },
    {
      path: '/gchain/j/:all',
      component: ()=>import ( "/src/layouts/gchain.vue" ),
      meta: {"layout":"gchain"},
      children: [{
        path: "",
        name: 'gchain-j-all',
        component:()=> import("~/pages/gchain/j/[all].vue"),
        meta: {"layout":"gchain"},
        props: true
      }]
    },
    {
      path: '/gchain/myClinks',
      component: ()=>import ( "/src/layouts/gchain.vue" ),
      meta: {"layout":"gchain"},
      children: [{
        path: "",
        name: 'gchain-myClinks',
        component:()=> import("~/pages/gchain/myClinks.vue"),
        meta: {"layout":"gchain"},
        props: true
      }]
    },
    {
      path: '/gchain/t/:all',
      component: ()=>import ( "/src/layouts/gchain.vue" ),
      meta: {"layout":"gchain"},
      children: [{
        path: "",
        name: 'gchain-t-all',
        component:()=> import("~/pages/gchain/t/[all].vue"),
        meta: {"layout":"gchain"},
        props: true
      }]
    },
    {
      path: '/redirect/:type',
      component: ()=>import ( "/src/layouts/blank.vue" ),
      meta: {"layout":"blank"},
      children: [{
        path: "",
        name: 'redirect-type',
        component:()=> import("~/pages/redirect/[type].vue"),
        meta: {"layout":"blank"},
        props: true
      }]
    },
    {
      path: '/:catchAll(.*)',
      component: ()=>import ( "/src/layouts/blank.vue" ),
      meta: {"layout":"blank"},
      children: [{
        path: "",
        name: 'error',
        component:()=> import("~/pages/error.vue"),
        meta: {"layout":"blank"},
        props: true
      }]
    }
  ];

  export default routes;
  