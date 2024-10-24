<template>

<div class="card">

<div class="card-caption">
  <div v-if="props.compx.comp.id > 0">
    {{ props.compx.comp.p.Name }}
  </div>
  <div v-else>
    Create New Component
  </div>
</div>

<div>Props</div>
<VarType :vt="VARTYPE_empty()" />
<VarType 
  :vt="v"
  v-for="[k,v] in (Object.entries(props.compx.props) as [string,jcs.VARTYPE][])" />

<div>States</div>
<VarType :vt="VARTYPE_empty()" />
<VarType 
  :vt="v"
  v-for="[k,v] in (Object.entries(props.compx.states) as [string,jcs.VARTYPE][])" />

</div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import VarType from '~/comps/Common/VarType.vue'
import { VARTYPE_empty } from '~/lib/shared/OrmMor'

const props = defineProps(['projectx','compx'])
props.projectx as jcs.ProjectComplex
props.compx as jcs.CompComplex

const s = glib.vue.reactive({
})

glib.vue.onMounted(async () => {
})

</script>
