<template>

  <div class="card">

    <div v-if="props.mode > 1" class="mb-[50px]">
      <button v-if="!s.expand && s.mx.m.id > 0" @click="s.expand = true">Edit</button>
      <div v-else>
        <button @click="s.expand = false">Collapse</button>
        <RichTextEditor :mx="s.mx" />
      </div>
    </div>

    <div class="card-caption">
      <div class="flex">
        <a :href="'/m/' + s.mx.m.id">{{ s.mx.m.p.Title }}</a>
      </div>
    </div>

    <div v-if="props.mode > 0" class="lg:mt-[50px] flex">
      <img :src="s.mx.m.p.PreviewImgUrl" class="max-w-[150px] sm:max-w-[50px] mr-5" >
      <div v-html="markdown__html(s.mx.m.p.Summary)" />
    </div>

    <div v-if="props.mode > 1" class="mt-[50px]">
      {{ (new Date(s.mx.m.createdat)).toLocaleDateString() }}
      <button v-if="s.user.eu.p.id > 0" @click="s.expand = true" class="button-small">
        Edit
      </button>
    </div>

    <div v-if="props.mode > 1" class="mt-[50px]">

      <div v-html="markdown__html(s.mx.m.p.FullText)" class="mt-[50px]" />

      <div class="my-[50px]">
        <a :href="'https://jcha.in/link/https://' + s.domainname + '/m/' + s.mx.m.id" target="_blank">
          Share Link
        </a>
      </div>

    </div>

  </div>

</template>

<script setup lang="ts">

import { getCurrentInstance, toRefs, watch } from 'vue'
import { navigate } from '~/lib/mod/route'
import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import { upload, checkUrl } from '~/lib/util/fetch'
import { markdown__html } from '~/lib/util/markdown'

import UserAuth from '~/comps/UserAuth.vue'
import Uploader from '~/comps/Uploader.vue'
import RichTextEditor from '~/comps/RichTextEditor.vue'

const props = defineProps(['mx', 'id', 'mode'])
props.mx as jcs.MomentComplex
props.id as number
props.mode as number

const s = glib.vue.reactive({
  user: runtime.user,
  mx: glib.Mor.jcs.MomentComplex_empty(),
  domainname: 'jcatsys.com',
  expand: false
})

const emits = defineEmits(['edit'])

const reload = () => {
  if (props.mx)
    s.mx = props.mx
  else
    Common.loader('/api/public/moment', { id: Number(props.id) }, (rep: any) => {
      s.mx = rep.data as jcs.MomentComplex
    })
}

watch(() => props.mx, (newValue, oldValue) => {
  s.mx = props.mx
  reload()
})

watch(() => props.id, (newValue, oldValue) => {
  reload()
})

glib.vue.onMounted(async () => {
  reload()
})

</script>