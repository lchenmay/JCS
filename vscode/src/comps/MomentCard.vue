<template>

<div class="card">
  <div class="card-caption"><router-link :to="'/m/' + s.mx.m.id">{{ s.mx.m.p.Title }}</router-link></div>
    <div>
      {{ (new Date(s.mx.m.createdat)).toLocaleDateString() }}
      <button v-if="s.user.eu.p.AuthType == 2" @click="emits('edit',s.mx)" class="button-small">Edit</button>
    </div>
    <div v-html="markdown__html(s.mx.m.p.Summary)" />
    <div v-if="props.level > 0" v-html="markdown__html(s.mx.m.p.FullText)" />
    <div v-if="props.level > 0">
      <a :href="'https://jcatsys.com.com/m/' + s.mx.m.id" target="_blank">{{ 'https://jcatsys.com.com/m/' + s.mx.m.id }}</a>
    </div>
    <hr class="mt-3">
</div>

</template>

<script setup lang="ts">

import { getCurrentInstance, toRefs, watch } from 'vue'
import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import { upload,checkUrl }  from '~/lib/util/fetch'
import { markdown__html } from '~/lib/util/markdown'

import UserAuth from '~/comps/UserAuth.vue'
import Uploader from '~/comps/Uploader.vue'
import RichTextEditor from '~/comps/RichTextEditor.vue'

const props = defineProps(['mx','id','level'])
props.mx as jcs.MomentComplex
props.id as number
props.level as number

const s = glib.vue.reactive({
  user: runtime.user,
  mx: glib.Mor.jcs.MomentComplex_empty()
})

const emits = defineEmits(['edit']) 

const reload = () => {
  if(props.mx)
    s.mx = props.mx
  else
    Common.loader('/api/public/moment', { id: Number(props.id) },(rep:any) => {
      s.mx = rep.data as jcs.MomentComplex
    })
}

watch(() => props.mx,(newValue, oldValue) => {
  s.mx = props.mx
  reload()
})

watch(() => props.id,(newValue, oldValue) => {
  reload()
})

glib.vue.onMounted(async () => {
  reload()
})

</script>