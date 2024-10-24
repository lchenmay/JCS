<template>

<div class="card">

<div class="card-caption">
  <div v-if="props.pagex.page.id > 0">
    {{ props.pagex.page.p.Name }}
  </div>
  <div v-else>
    Page Name:
    <input v-model="props.pagex.page.p.Name" />
    <button @click="Common.loader('/api/public/createPage', { 
      name: props.pagex.page.p.Name, 
      project: Number(props.projectx.project.id) },(rep:any) => { 
        let v = rep.pagex as jcs.PageComplex
        props.pagex.page = v.page 
        props.projectx.pagexs[props.pagex.page.p.Name] = v })">
      Create New Page
    </button>
  </div>
</div>
    
<div>
  Route <input v-model="props.pagex.page.p.Route" />
</div>
<div>
  OG Title <input v-model="props.pagex.page.p.OgTitle" />
</div>
<div>
  OG Description <input v-model="props.pagex.page.p.OgDesc" />
</div>
<div>
  OG Image <input v-model="props.pagex.page.p.OgImage" />
</div>

<div>Props</div>
<VarType :vt="VARTYPE_empty()" />
<VarType 
  :vt="v"
  v-for="[k,v] in (Object.entries(props.pagex.props) as [string,jcs.VARTYPE][])" />
    
<div>States</div>
<VarType :vt="VARTYPE_empty()" />
<VarType 
  :vt="v"
  v-for="[k,v] in (Object.entries(props.pagex.states) as [string,jcs.VARTYPE][])" />
    
</div>
    
</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import VarType from '~/comps/Common/VarType.vue'
import { VARTYPE_empty } from '~/lib/shared/OrmMor'

const props = defineProps(['pagex','projectx'])
props.pagex as jcs.PageComplex
props.projectx as jcs.ProjectComplex

const s = glib.vue.reactive({
})

glib.vue.onMounted(async () => {
})

</script>
