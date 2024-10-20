<template>

<div>
Project Code = {{ props.projectx.project.p.Code }}
<button v-on:click="router.push('/CodeRobot/Project/' + projectx.project.id)">Edit</button>
</div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'

const props = defineProps(['projectx'])
props.projectx as jcs.ProjectComplex

const s = glib.vue.reactive({
data: runtime.data
})

glib.vue.onMounted(async () => {
})

</script>
