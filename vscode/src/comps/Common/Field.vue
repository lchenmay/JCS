<template>
</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'

const props = defineProps(['field'])
props.field as jcs.FIELD

const s = glib.vue.reactive({
})

glib.vue.onMounted(async () => {
})

</script>
