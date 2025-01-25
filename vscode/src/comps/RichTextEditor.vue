<template>

  <div class="my-5">

    <div>Caption</div>
    <div>
      <input v-model="s.mx.m.p.Title" type="text" class="w-[1500px]">
    </div>
    <div>Summary</div>

    <div class="flex justify-start">
      <textarea @keydown="s.renderSummary = markdown__html(s.mx.m.p.Summary)" v-model="s.mx.m.p.Summary"
        class="w-[760px] h-[300px] overflow-y-scroll"></textarea>
      <div class="w-[760px] h-[300px] bg-[#ffffdd] overflow-y-scroll pb-[50px]" v-html="s.renderSummary" />
    </div>

    <div>Tags</div>
    <div>
      <input v-model="s.mx.m.p.Tags" type="text" class="w-[1500px]">
    </div>

    <div>Poster</div>
    <div>
      <input v-model="s.mx.m.p.PreviewImgUrl" type="text" class="w-[1500px]">
    </div>
    <div v-if="s.mx.m.p.PreviewImgUrl.length > 0">
      <img :src="s.mx.m.p.PreviewImgUrl">
    </div>

    <div class="flex justify-start">
      <textarea @keydown="s.renderFullText = markdown__html(s.mx.m.p.FullText)" v-model="s.mx.m.p.FullText"
        class="w-[760px] h-[800px] overflow-y-scroll"></textarea>
      <div class="w-[760px] h-[800px] bg-[#ffffdd] overflow-y-scroll pb-[50px]" v-html="s.renderFullText" />
    </div>

    <div class="flex">
      <button @click="s.converted = s.mx.m.p.FullText.replaceAll('\\','\\\\')">Markdown -> Github Markdown</button>
      <button>Markdown -> Tex</button>
    </div>
    <div>
      <textarea v-model="s.converted" class="w-[1400px] h-[300px] overflow-y-scroll"></textarea>
    </div>

    <div class="m-3 p-3">
      <Uploader />
      <button v-if="s.mx.m.id == 0" @click="editMoment">Create</button>
      <button v-else @click="editMoment">Edit</button>
    </div>

  </div>

</template>


<script setup lang="ts">

import { getCurrentInstance, toRefs, watch } from 'vue'
import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import Uploader from '~/comps/Uploader.vue'
import MomentCard from '~/comps/MomentCard.vue'
import { markdown__html } from '~/lib/util/markdown'
import { navigate, router } from '~/lib/mod/route'

const props = defineProps(['mx','href'])
props.mx as j.MomentComplex
props.href as string

watch(() => props.mx, (newValue, oldValue) => {
  s.mx = props.mx,
  s.renderSummary = markdown__html(s.mx.m.p.Summary)
  s.renderFullText = markdown__html(s.mx.m.p.FullText)
})

const s = glib.vue.reactive({
  renderSummary: "",
  renderFullText: "",
  mx: glib.Mor.jcs.MomentComplex_empty(),
  src: "zh",
  dst: "en",
  msg: "",
  converted: "",
  expand: false
})

const editMoment = async () => {
  Common.loader('/api/eu/moment', {
    id: Number(s.mx.m.id),
    p: s.mx.m.p
  }, (rep: any) => {
    if(s.mx.m.id == 0)
      navigate("/m/" + rep.rcd.id,"m",rep.rcd.id)
    else{
      s.mx.m = rep.rcd as jcs.MOMENT

      if(props.href)
        window.location.href = props.href
    }
  })
}

const convertHierachy = async () => {
  Common.loader('/api/eu/moment', {
    id: Number(s.mx.m.id),
    act: 'convert-to-hierachy'
  }, (rep: any) => {
    s.mx.m = rep.rcd as jcs.MOMENT
  })
}

const addOffspring = async () => {
  Common.loader('/api/eu/moment', {
    id: Number(s.mx.m.id),
    act: 'add-offspring'
  }, (rep: any) => {
    s.mx = rep.data as jcs.MomentComplex
  })
}

const convertMultilingual = async () => {
  Common.loader('/api/eu/moment', {
    id: Number(s.mx.m.id),
    act: 'convert-to-multilingual'
  }, (rep: any) => {
    s.mx = rep.data as jcs.MomentComplex
  })
}

const addMultilingual = async () => {
  Common.loader('/api/eu/moment', {
    id: Number(s.mx.m.id),
    act: 'add-multilingual'
  }, (rep: any) => {
    s.mx = rep.data as jcs.MomentComplex
  })
}

const move = async (id:number,ud:string) => {
  Common.loader('/api/eu/moment', {
    id: Number(id),
    ud: ud,
    act: 'move'
  }, (rep: any) => {
    s.mx = rep.data as jcs.MomentComplex
  })
}

const translate = async () => {
  Common.loader('/api/eu/moment', {
    id: Number(s.mx.m.id),
    src: s.src,
    dst: s.dst,
    act: 'translate'
  }, (rep: any) => {
    s.mx = rep.data as jcs.MomentComplex
    s.msg = rep.msg
  })
}

glib.vue.onMounted(async () => {
  s.mx = props.mx

  s.renderSummary = markdown__html(s.mx.m.p.Summary)
  s.renderFullText = markdown__html(s.mx.m.p.FullText)
})

</script>