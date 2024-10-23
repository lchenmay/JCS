<template>

<h1>{{ props.projectx.project.p.Code }}</h1>
<!--button v-on:click="router.push('/CodeRobot/Project/' + projectx.project.id)">Edit</button-->

<h2>Host Configurations</h2>
<div v-for="[k,v] in (Object.entries(props.projectx.hostconfigs) as [string,jcs.HOSTCONFIG][])">
    {{ v.p.Hostname }}
</div>

<h2 class="caption-full-width">Tables</h2>
<Table :tablex="glib.Mor.jcs.TableComplex_empty()" />
<Table
  :tablex="v"
  v-for="[k,v] in (Object.entries(props.projectx.tablexs) as [string,jcs.TableComplex][])" />

<h2 class="caption-full-width">Components</h2>
<Comp :compx="glib.Mor.jcs.CompComplex_empty()" />
<Comp 
  :compx="v"
  v-for="[k,v] in (Object.entries(props.projectx.compxs) as [string,jcs.CompComplex][])" />

<h2 class="caption-full-width">Pages</h2>
<Page :pagex="glib.Mor.jcs.PageComplex_empty()" />
<Page 
  :pagex="v"
  v-for="[k,v] in (Object.entries(props.projectx.pagexs) as [string,jcs.PageComplex][])" />

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { useRoute } from 'vue-router'
import * as Common from '~/lib/store/common'
import Table from '~/comps/Common/Table.vue'
import Comp from '~/comps/Common/Comp.vue'
import Page from '~/comps/Common/Page.vue'

const props = defineProps(['projectx'])
props.projectx as jcs.ProjectComplex

const s = glib.vue.reactive({
query: useRoute().query,
data: runtime.data
})

glib.vue.onMounted(async () => {
})

</script>
