<template>

  <div id="home" class="flex justify-center"><div class="hor-range">
  
    <div class="card">
      <a href="/m/0">Create</a>
    </div>
  
    <MomentCard v-for="mx in s.mxs" :mx="mx" mode="1" />
  
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
  })
  
  glib.vue.onMounted(async () => {
    Common.loader('/api/public/moments', { },(rep:any) => {
      s.mxs = rep.list as jcs.MomentComplex[]
    })
  })
  
  </script>
  