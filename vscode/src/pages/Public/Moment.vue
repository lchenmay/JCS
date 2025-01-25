<template>

  <div class="flex justify-center"><div class="hor-range">
  
  <MomentCard :id="s.id" mode="2" />
  
  </div></div>
  
  </template>
  
  <script setup lang="ts">
  
  import { glib } from '~/lib/glib'
  import { useRouter } from 'vue-router'
  import * as Common from '~/lib/store/common'
  import MomentCard from '~/comps/MomentCard.vue'
  
  
  const s = glib.vue.reactive({
  router: useRouter(),
  id: 0
  })
  
  glib.vue.onMounted(async () => {
    s.id = Number(s.router.currentRoute.params.id)
  })
  
  </script>
  