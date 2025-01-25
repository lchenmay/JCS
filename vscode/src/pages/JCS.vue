<template>

  <div id="home" class="flex justify-center"><div class="hor-range">
    <div class="text-xl flex m-5">
      <p>J-Category Systems</p>
    </div>
    <div v-html="markdown__html(s.mxHome.m.p.FullText)" class="card" />
  </div></div>

  <div id="fp" class="flex justify-center"><div class="hor-range">
    <div class="caption-1">Functional Programming</div>
    <MomentCard v-for="mx in s.mxsFP" :mx="mx" mode="1" />
  </div></div>

  <div id="ia" class="flex justify-center"><div class="hor-range">
    <div class="caption-1">Immutable Architecture</div>
    <MomentCard v-for="mx in s.mxsIA" :mx="mx" mode="1" />
  </div></div>

  <div id="cat" class="flex justify-center"><div class="hor-range">
    <div  class="caption-1">Category Theory</div>
    <MomentCard v-for="mx in s.mxsCAT" :mx="mx" mode="1" />
  </div></div>

  <div id="service" class="flex justify-center"><div class="hor-range">
    <div  class="caption-1">Service</div>
    <MomentCard v-for="mx in s.mxsService" :mx="mx" mode="1" />
  </div></div>

  <div id="contact" class="flex justify-center"><div class="hor-range">
    <div class="caption-1">Contact</div>
    <Contact />
    
  </div></div>


</template>
  


<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import { markdown__html } from '~/lib/util/markdown'

import MomentCard from '~/comps/MomentCard.vue'
import Contact from '~/comps/Contact.vue'

const s = glib.vue.reactive({
  user: runtime.user,
  mxHome: glib.Mor.jcs.MomentComplex_empty(),
  mxsFP: [] as jcs.MomentComplex[],
  mxsIA: [] as jcs.MomentComplex[],
  mxsCAT: [] as jcs.MomentComplex[],
  mxsService: [] as jcs.MomentComplex[],
  domainname: "jcatsys.com"
})

glib.vue.onMounted(async () => {
  Common.loader('/api/public/homepage', { },(rep:any) => {
      s.mxHome = rep.home as jcs.MomentComplex
      s.mxsFP = rep.FP as jcs.MomentComplex[]
      s.mxsIA = rep.IA as jcs.MomentComplex[]
      s.mxsCAT = rep.CAT as jcs.MomentComplex[]
      s.mxsService = rep.Service as jcs.MomentComplex[]
    })
})

</script>
