<template>

<div id="home" class="flex justify-center"><div class="hor-range">

<RichTextEditor v-if="s.user.eu.p.AuthType == 2" :mx="s.mx" />

<MomentCard v-for="mx in s.mxs" :mx="mx" v-on:edit="editMoment" level="0" />

</div></div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { useRoute } from 'vue-router'
import * as Common from '~/lib/store/common'

import RichTextEditor from '~/comps/RichTextEditor.vue'
import MomentCard from '~/comps/MomentCard.vue'

const s = glib.vue.reactive({
  user: runtime.user,
  mx: glib.Mor.jcs.MomentComplex_empty(),
  mxs: [] as jcs.MomentComplex[]
})

const loadMoments = async () => {
  Common.loader('/api/public/moments', { },(rep:any) => {
    s.mxs = rep.list as jcs.MomentComplex[]
  })
}

const editMoment = (mx:jcs.MomentComplex) => {
  s.mx = mx
}

glib.vue.onMounted(async () => {
  loadMoments()
})

</script>
