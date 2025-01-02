<template>

<div class="my-5">

  <div v-if="s.expand == false">
    <button @click="s.expand = true">{{ translate(props.lang)('New') }}</button>
  </div>

  <div v-else>

  <div>{{ translate(props.lang)('Caption') }}</div>
  <div>
    <input v-model="s.mx.m.p.Title" type="text" class="w-[1500px]">
  </div>
  <div>{{ translate(props.lang)('Summary') }}</div>
  <div>
    <textarea v-model="s.mx.m.p.Summary" class="w-[1500px] h-[200px] overflow-y-scroll" />
  </div>
  <div>{{ translate(props.lang)('Poster') }}</div>
  <div>
    <input v-model="s.mx.m.p.PreviewImgUrl" type="text" class="w-[1500px]">
  </div>
  <div v-if="s.mx.m.p.PreviewImgUrl.length > 0">
    <img :src="s.mx.m.p.PreviewImgUrl" >
  </div>

  <div class="flex justify-start">
    <textarea @keydown="render" v-model="s.mx.m.p.FullText" class="w-[760px] h-[800px] overflow-y-scroll"></textarea>
    <div class="w-[760px] h-[800px] overflow-y-scroll pb-[50px]" v-html="s.render" />
  </div>

  <div class="m-3 p-3">
    <Uploader :domainname="props.domainname" :lang="props.lang" />
    <button v-if="s.mx.m.id == 0" @click="editMoment">{{ translate(props.lang)('Create') }}</button>
    <button v-else @click="editMoment">{{ translate(props.lang)('Edit') }}</button>
  </div>
  
  </div>

</div>

</template>


<script setup lang="ts">

import { translate } from '~/lib/bizLogics/lang'
import { getCurrentInstance, toRefs, watch } from 'vue'
import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import Uploader from '~/comps/Uploader.vue'
import { markdown__html } from '~/lib/util/markdown'

const props = defineProps(['mx','domainname','lang'])
props.mx as [].MomentComplex
props.domainname as string
props.lang as string

watch(() => props.mx,(newValue, oldValue) => {
  s.mx = props.mx
  s.expand = true
  render()
})

const s = glib.vue.reactive({
  render: "",
  mx: glib.Mor.[].MomentComplex_empty(),
  expand: false
})

const emits = defineEmits(['changed']) 

const editMoment = async () => {
  Common.loader('/api/eu/moment', { 
    id: Number(s.mx.m.id),
    p: s.mx.m.p },(rep:any) => {
    s.expand = false
  })
}

const render = () => {
  s.render = markdown__html(s.mx.m.p.FullText)
}

glib.vue.onMounted(async () => {
  s.mx = props.mx
})

</script>

