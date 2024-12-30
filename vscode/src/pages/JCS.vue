<template>

  <div id="home" class="flex justify-center"><div class="hor-range">
    <div class="text-xl flex m-5">
      <p>J-Cat Sys LLC</p>
    </div>

    <RichTextEditor v-if="s.user.eu.p.AuthType == 2" :mx="s.mx" />
    <router-link to="/moments">All articles</router-link>

    <MomentCard id="54864677" v-on:edit="editMoment" level="0" />

  </div></div>

  <div id="fp" class="flex justify-center"><div class="hor-range">
    <div class="caption-1">Functional Programming</div>
  </div></div>

  <div id="ia" class="flex justify-center"><div class="hor-range">
    <div class="caption-1">Immutable Architecture</div>
  </div></div>

  <div id="cat" class="flex justify-center"><div class="hor-range">
    <div  class="caption-1">Category Theory</div>
  </div></div>

  <div id="service" class="flex justify-center"><div class="hor-range">
    <div  class="caption-1">Service</div>

    <MomentCard id="54864676" v-on:edit="editMoment" level="0" />

  </div></div>

  <div id="contact" class="flex justify-center"><div class="hor-range">
    <div class="caption-1">Contact</div>
    <Contact />
    
  </div></div>


</template>
  

<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import { upload,checkUrl }  from '~/lib/util/fetch'
import { markdown__html } from '~/lib/util/markdown'

import MomentCard from '~/comps/MomentCard.vue'
import Contact from '~/comps/Contact.vue'
import RichTextEditor from '~/comps/RichTextEditor.vue'

const s = glib.vue.reactive({
  user: runtime.user,
  mx: glib.Mor.jcs.MomentComplex_empty(),  
  mxs: [] as jcs.MomentComplex[],
  name: "",
  email: "",
  msg: "",
  promptContact: "",
  expandContact: false
})

const onAuthChanged = (user:jcs.EuComplex,session:string) => {
  s.user = user
}

const loadMoments = async () => {
  Common.loader('/api/public/moments', { },(rep:any) => {
    s.mxs = rep.list as jcs.MomentComplex[]
  })
}

const editMoment = (mx:jcs.MomentComplex) => {
  s.mx = mx
}

const contact = async () => {
  Common.loader('/api/public/msg', { 
    name: s.name,
    email: s.email,
    msg: s.msg },(rep:any) => {
    s.expandContact = false
    s.promptContact = "Your message has been sent."
  })
}

glib.vue.onMounted(async () => {
  loadMoments()
})

</script>

