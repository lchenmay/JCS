<template>

<h1>Projects</h1>

<div>
  Query = {{ useRoute().query }}
</div>

<Project :projectx="ProjectComplex_empty()" />

<Project 
  :projectx="v" 
  v-for="[k,v] in (Object.entries(s.projectxs) as [string,jcs.ProjectComplex][])" />

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { useRoute } from 'vue-router'
import * as Common from '~/lib/store/common'
import Project from '~/comps/Common/Project.vue'
import { ProjectComplex_empty } from '~/lib/shared/CustomMor';


const s = glib.vue.reactive({
query: useRoute().query,
projectxs: {} as {[key:string]: jcs.ProjectComplex},
data: runtime.data
})

glib.vue.onMounted(async () => {
  Common.loader('/api/public/reloadProjects', { 
  },(rep:any) => { 
    s.projectxs = {}
    rep.list.forEach((i:jcs.ProjectComplex) => {
      s.projectxs[i.project.id] = i
    })
  })
})

</script>
