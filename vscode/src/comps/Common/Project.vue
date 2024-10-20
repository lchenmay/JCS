<template>

<div>
Project Code = {{ props.project.project.p.Code }}
<button>Edit</button>
</div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'

const props = defineProps(['project'])
props.project as jcs.ProjectComplex

const s = glib.vue.reactive({
data: runtime.data
})

glib.vue.onMounted(async () => {
})

</script>
