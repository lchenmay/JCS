<template>

<div id="home" class="flex justify-center"><div class="hor-range">

<RichTextEditor v-if="s.user.eu.p.AuthType == 2" :mx="s.mx" />

<MomentCard :id="s.id" v-on:edit="editMoment" level="1" />

</div></div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { useRouter } from 'vue-router'
import VueRouter from 'vue-router'
import * as Common from '~/lib/store/common'
import RichTextEditor from '~/comps/RichTextEditor.vue'
import MomentCard from '~/comps/MomentCard.vue'


const s = glib.vue.reactive({
router: useRouter(),
id: 0,
mx: glib.Mor.jcs.MomentComplex_empty(),
user: runtime.user,
data: runtime.data
})

const editMoment = (mx:jcs.MomentComplex) => {
  s.mx = mx
}

glib.vue.onMounted(async () => {
  s.id = Number(s.router.currentRoute.params.id)
})

</script>
