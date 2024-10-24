<template>

<div>
    <button class="caption-full-width"
        @click="s.expand = !s.expand">
            <div v-if="props.projectx.project.id == 0">
                Create New Project
            </div>
            <div v-else>
                {{ props.projectx.project.p.Code }}
            </div>
    </button>
</div>
<!--button v-on:click="router.push('/CodeRobot/Project/' + projectx.project.id)">Edit</button-->

<div v-if="s.expand">

<div v-if="props.projectx.project.id == 0">

    Project Code:
    <input v-model="props.projectx.project.p.Code" />
    
    <button @click="Common.loader('/api/public/createProject', {code: props.projectx.project.p.Code },(rep:any) => { })">
        Create New Project
    </button>

</div>

<div v-else>

<h1>Host Configurations</h1>
<div v-for="[k,v] in (Object.entries(props.projectx.hostconfigs) as [string,jcs.HOSTCONFIG][])">
    {{ v.p.Hostname }}
</div>

<h1>Tables</h1>
<Table :tablex="glib.Mor.jcs.TableComplex_empty()" />
<Table
  :tablex="v"
  v-for="[k,v] in (Object.entries(props.projectx.tablexs) as [string,jcs.TableComplex][])" />

<h1>Components</h1>
<Comp :compx="glib.Mor.jcs.CompComplex_empty()" :projectx="props.projectx" />
<Comp 
  :compx="v" :projectx="props.projectx"
  v-for="[k,v] in (Object.entries(props.projectx.compxs) as [string,jcs.CompComplex][])" />

<h1>Pages</h1>
<Page :pagex="glib.Mor.jcs.PageComplex_empty()" :projectx="props.projectx" />
<Page 
  :pagex="v" :projectx="props.projectx"
  v-for="[k,v] in (Object.entries(props.projectx.pagexs) as [string,jcs.PageComplex][])" />

</div>

</div>

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
expand: false,
query: useRoute().query,
data: runtime.data
})

glib.vue.onMounted(async () => {
})

</script>
