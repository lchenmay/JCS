<template>

<div>
  Host Name <input v-model="props.hostconfig.p.Hostname" />
</div>

<div>
  Database <input v-model="props.hostconfig.p.Database" />
</div>

<div>
  Database Name <input v-model="props.hostconfig.p.DatabaseName" />
</div>

<div>
  Database Conn <input v-model="props.hostconfig.p.DatabaseConn" />
</div>

<div>
  Dir Visual Studio /Shared <input v-model="props.hostconfig.p.DirVsShared" />
</div>

<div>
  Dir VsCode Web <input v-model="props.hostconfig.p.DirVsCodeWeb" />
</div>


</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { useRoute } from 'vue-router'
import * as Common from '~/lib/store/common'

const props = defineProps(['projectx','hostconfig'])
props.projectx as jcs.ProjectComplex
props.hostconfig as jcs.HOSTCONFIG

const s = glib.vue.reactive({
query: useRoute().query,
data: runtime.data
})

glib.vue.onMounted(async () => {
})

</script>
