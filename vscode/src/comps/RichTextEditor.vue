<template>

<div class="my-5">

  <div v-if="s.expand == false">
    <button @click="s.expand = true">Authoring</button>
  </div>

  <div v-else>

  <div>
    Caption<input v-model="s.mx.m.p.Title" type="text">
  </div>
  <div>
    Summary
    <textarea v-model="s.mx.m.p.Summary" />
  </div>

  <div class="flex justify-start">
    <textarea @keydown="render" v-model="s.mx.m.p.FullText" class="w-[500px] h-[500px]"></textarea>
    <div class="w-[500px] h-[500px]" v-html="s.render" />
  </div>

  <div class="m-3 p-3">
    <Uploader />
    <button v-if="s.mx.m.id == 0" @click="editMoment">Create</button>
    <button v-else @click="editMoment">Edit</button>
  </div>
  
  </div>

</div>

</template>


<script setup lang="ts">

import { getCurrentInstance, toRefs, watch } from 'vue'
import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import Uploader from '~/comps/Uploader.vue'
import { markdown__html } from '~/lib/util/markdown'

const props = defineProps(['mx'])
props.mx as jcs.MomentComplex

watch(() => props.mx,(newValue, oldValue) => {
  s.mx = props.mx
  s.expand = true
  render()
})

const s = glib.vue.reactive({
  render: "",
  mx: glib.Mor.jcs.MomentComplex_empty(),
  expand: false
})

const emits = defineEmits(['changed']) 

const editMoment = async () => {
  Common.loader('/api/eu/moment', { 
    id: Number(s.mx.m.id),
    title: s.mx.m.p.Title,
    summary: s.mx.m.p.Summary,
    content: s.mx.m.p.FullText },(rep:any) => {
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

