<template>

<div>
  Query = {{ useRoute().query }}
</div>

<Project :projectx="glib.Mor.jcs.ProjectComplex_empty()" />

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { useRoute } from 'vue-router'
import * as Common from '~/lib/store/common'
import Project from '~/comps/Common/Project.vue'

const s = glib.vue.reactive({
query: useRoute().query,
data: runtime.data
})

glib.vue.onMounted(async () => {
})

</script>
