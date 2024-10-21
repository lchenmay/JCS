<template>
<div class="flex">
<button v-if="props.vt.id > 0">Update</button>
<button v-else>Create</button>
<input v-model="props.vt.p.Name" />
:
<input v-model="props.vt.p.Type" />
<button v-if="props.vt.id > 0">Remove</button>
</div>
</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'

const props = defineProps(['vt'])
props.vt as jcs.VARTYPE

const s = glib.vue.reactive({
})

glib.vue.onMounted(async () => {
})

</script>
