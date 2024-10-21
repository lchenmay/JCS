<template>
    
<div class="card">

<div class="card-caption">
  <div v-if="props.tablex.table.id > 0">
    {{ props.tablex.table.p.Name }}
  </div>
  <div v-else>
    Create New Table
  </div>
</div>

<div>Fields</div>
<Field :field="FIELD_empty()" />
<Field 
  :vt="v"
  v-for="[k,v] in (Object.entries(props.tablex.fields) as [string,jcs.VARTYPE][])" />

</div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { FIELD_empty } from '~/lib/shared/OrmMor'
import * as Common from '~/lib/store/common'
import Field from '~/comps/Common/Field.vue'

const props = defineProps(['tablex'])
props.tablex as jcs.TableComplex

const s = glib.vue.reactive({
data: runtime.data
})

glib.vue.onMounted(async () => {
})

</script>
