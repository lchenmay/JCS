<template>

    <div>Moment</div>
    <button @click="glib.panel.tglPanel()">toggle Panel</button>
    
    <div v-if="s.mc.m.id > 0">
        <div>{{ s.mc.m.p.Title }}</div>
        <div>{{ s.mc.m.p.Summary }}</div>
        <div v-if="s.mc.m.p.UrlOriginal.length > 0">
            <a :href="s.mc.m.p.UrlOriginal" target="_blank">Original Link</a>
        </div>
        <div v-if="s.mc.m.p.PreviewImgUrl.length > 0">
            <img :src="s.mc.m.p.PreviewImgUrl" />
        </div>

        <div>
          <a :href="`https://gcha.in/?url=https://cpto.cc/m/${ s.mc.m.id }`" target="_blank">
            Share via GCHA.IN
          </a>
        </div>

    </div>

    </template>
    
    
<script setup lang="ts">
    
import { glib } from '~/lib/glib'


const s = glib.vue.reactive({
    id: rtxx.router.currentRoute.params.all,
    mc: { m: glib.Mor.ctc.MOMENT_empty() } as ctc.MomentComplex,
    clink: ""
})
    
glib.vue.onMounted(async () => {
    
  const rep = await glib.post('/api/public/loadMoment', { 
    id: s.id })

  if(rep.Er == "OK"){
    s.mc = rep.mc
  }
})

</script>