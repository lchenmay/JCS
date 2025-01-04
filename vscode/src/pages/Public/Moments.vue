<template>

  <div id="home" class="flex justify-center"><div class="hor-range">
  
    <MomentCard :domainname="s.domainname" create="true" />
  
    <MomentCard v-for="mx in s.mxs" :mx="mx" :domainname="s.domainname" title="true" summary="true" />
  
  </div></div>
  
  </template>
  
  <script setup lang="ts">
  
  import { glib } from '~/lib/glib'
  import { useRoute } from 'vue-router'
  import * as Common from '~/lib/store/common'
  
  import MomentCard from '~/comps/MomentCard.vue'
  
  const s = glib.vue.reactive({
    user: runtime.user,
    mxs: [] as jcs.MomentComplex[],
    domainname: "jcatsys.com"
  })
  
  glib.vue.onMounted(async () => {
    Common.loader('/api/public/moments', { },(rep:any) => {
      s.mxs = rep.list as jcs.MomentComplex[]
    })
  })
  
  </script>
  