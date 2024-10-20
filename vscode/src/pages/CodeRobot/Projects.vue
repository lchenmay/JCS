<template>

<h1>Projects</h1>

<Project :project="v" v-for="[k,v] in Object.entries(s.data.pcs)" />

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'


const s = glib.vue.reactive({
data: runtime.data
})

glib.vue.onMounted(async () => {
  Common.loader('/api/public/reloadProjects', { 
  },(rep:any) => { 
    s.data.pcs = {}
    rep.list.forEach((i:jcs.ProjectComplex) => {
      s.data.projectxs[i.project.id] = i
    })
  })
})

</script>
