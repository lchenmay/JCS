<template>

<div>

<div class="flex">
  <a @click="" class="image-button"><img class="w-[50px]" src="https://i.imgur.com/wZFfVj8.jpeg"></a>
  <div v-if="s.user.eu.id > 0" class="p-[15px]">
    {{ s.user.eu.p.Caption }}
  </div>
  <div v-else class="p-[15px]">
    Visitor
  </div>
  <button @click="s.expand = !s.expand">Expand/Collapse</button>
</div>

<div v-if="s.expand">

<div v-if="s.user.eu.id > 0" class="p-[15px] flex">
  <div v-if="s.user.eu.p.AuthType == 2" class="p-[15px]">
    <router-link to="/Admin">Admin</router-link>
  </div>
  <button class="inline" @click="signOut">Sign Out</button>
</div>

<div class="card">
  <div>
    <div class="flex">
      Key <input v-model="s.key" type="text" />
    </div>
  </div>
  <div>
    <button @click="authKey">Authorize</button>
  </div>
</div>

</div>

</div>

</template>


<script setup lang="ts">

import { stringify } from 'postcss'
import { glib } from '~/lib/glib'
import { EuComplex__bin } from '~/lib/shared/CustomMor'
import * as Common from '~/lib/store/common'

const s = glib.vue.reactive({
  user: runtime.user,
  key: "",
  expand: false
})

const emits = defineEmits(['changed']) 

const signOut = () => {
  Common.loader('/api/public/auth', { act: 'sign-out' },(rep:any) => {
    runtime.user = glib.Mor.jcs.EuComplex_empty()
    runtime.session = ""
    s.user = runtime.user

    localStorage.setItem("runtime.user",JSON.stringify(runtime.user))
    localStorage.setItem("runtime.session",JSON.stringify(runtime.session))

    s.expand = false
    emits('changed',runtime.user,runtime.session)
  })
}

const authKey = () => {
  Common.loader('/api/public/auth', { key: s.key },(rep:any) => {
    runtime.user = rep.eux as jcs.EuComplex
    runtime.session = rep.session
    s.user = runtime.user

    localStorage.setItem("runtime.user",JSON.stringify(runtime.user))
    localStorage.setItem("runtime.session",JSON.stringify(runtime.session))

    s.expand = false
    emits('changed',runtime.user,runtime.session)
  })
}

glib.vue.onMounted(async () => {

})

</script>

