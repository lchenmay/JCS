<template>

<div class="card">

<div v-if="s.expand == false">
  <button @click="s.expand = true">Contact</button>
</div>

<div v-else class="m-3 p-3 bg-[#eeffdd] w-[1000px]">
      <div>Name</div>
      <input v-model="s.name" type="text" class="w-[800px]"/>
      <div>Email</div>
      <input v-model="s.email" type="text" class="w-[800px]"/>
      <div>Message</div>
      <textarea v-model="s.msg" class="w-[800px] h-[500px]"></textarea>
      <button @click="contact">Send</button>
</div>
<div>
  {{ s.prompt }}
</div>

</div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import { upload,checkUrl }  from '~/lib/util/fetch'
import { markdown__html } from '~/lib/util/markdown'

import UserAuth from '~/comps/UserAuth.vue'
import Uploader from '~/comps/Uploader.vue'
import RichTextEditor from '~/comps/RichTextEditor.vue'

const s = glib.vue.reactive({
  name: "",
  email: "",
  msg: "",
  prompt: "",
  expand: false
})

const contact = async () => {
  Common.loader('/api/public/msg', { 
    name: s.name,
    email: s.email,
    msg: s.msg },(rep:any) => {
    s.expand = false
    s.prompt = "OK"
  })
}


</script>