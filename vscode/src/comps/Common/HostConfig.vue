<template>

{{ props.hostconfig.p.Hostname }}

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
