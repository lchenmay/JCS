<template>

  <div class="flex justify-center"><div class="hor-range">
  
  <MomentCard :id="s.id" :domainname="s.domainname" 
    title="true" summary="true" fulltext="true" link="true" preview="true" />
  
  </div></div>
  
  </template>
  
  <script setup lang="ts">
  
  import { glib } from '~/lib/glib'
  import { useRouter } from 'vue-router'
  import * as Common from '~/lib/store/common'
  import MomentCard from '~/comps/MomentCard.vue'
  
  
  const s = glib.vue.reactive({
  router: useRouter(),
  id: 0,
  domainname: "jcatsys.com"
  })
  
  glib.vue.onMounted(async () => {
    s.id = Number(s.router.currentRoute.params.id)
  })
  
  </script>
  