<template>
<div class="flex">
<button v-if="props.vt.id > 0">Update</button>
<button v-else @click="Common.loader('/api/public/createVarType', {
    project: Number(props.projectx.project.id),
    bind: props.bind,
    bindType: props.bindType,
    name: props.vt.p.Name,
    type: props.vt.p.Type
  },(rep:any) => { 
    let vt = rep.vt as jcs.VARTYPE
    switch(vt.p.BindType){
      case glib.Mor.jcs.vartypeBindTypeEnum_CompProps:
        projectx.compxs[bind].propxs[vt.p.Name] = vt
        break
      case glib.Mor.jcs.vartypeBindTypeEnum_CompState:
        projectx.compxs[bind].states[vt.p.Name] = vt
        break
        case glib.Mor.jcs.vartypeBindTypeEnum_PageProps:
        projectx.pagexs[bind].propxs[vt.p.Name] = vt
        break
      case glib.Mor.jcs.vartypeBindTypeEnum_PageState:
        projectx.pagexs[bind].states[vt.p.Name] = vt
        break
    }
  })">Create</button>
<input v-model="props.vt.p.Name" />
:
<input v-model="props.vt.p.Type" />
<button v-if="props.vt.id > 0">Remove</button>
</div>
</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'

const props = defineProps(['projectx','bindType','bind','vt'])
props.projectx as jcs.ProjectComplex
props.bindType as number
props.bind as string
props.vt as jcs.VARTYPE

const s = glib.vue.reactive({
})

glib.vue.onMounted(async () => {
})

</script>
