<template>

  <div class="my-5">
  
    <div v-if="s.mx.m.p.Parent != null" class="flex border-2 border-blue-300 p-2">
      
      <div v-if="s.mx.m.p.Parent == 0 && s.mx.offsprings.length == 0">
        <button @click="convertMultilingual">{{ __s('{ "zh":"将当前文档转化为分级文档集", "en":"Convert the current document into a hierarchical document set"}') }}</button>
      </div>

      <div v-if="s.mx.m.p.Parent > 0">
        <div>{{ __s('{ "zh":"上级文档", "en":"Superior document"}') }}</div>
        <MomentCard :id="s.mx.m.p.Parent" :domainname="props.domainname" title="true" />
      </div>

      <div v-if="s.mx.offsprings.length > 0">
        <div>{{ __s('{ "zh":"下级文档", "en":"Subordinate documents"}') }}</div>
        <MomentCard v-for="id in s.mx.offsprings" :id="id" :domainname="props.domainname" lang="true" title="true" />
        <button @click="addMultilingual">{{ __s('{ "zh":"添加语言版本", "en":"Add language version"}') }}</button>
      </div>
    </div>

    <div v-if="s.mx.m.p.MultiLingualMaster != null" class="flex border-2 border-blue-300 p-2">
      
      <div v-if="s.mx.m.p.MultiLingualMaster == 0 && s.mx.multilinguals.length == 0">
        <button @click="convertMultilingual">{{ __s('{ "zh":"将当前单一语言文档转化为多语言文档集", "en":"Convert the current single language document into a multi-language document set"}') }}</button>
      </div>

      <div v-if="s.mx.m.p.MultiLingualMaster > 0">
        <div>{{ __s('{ "zh":"多语言主文档", "en":"Multilingual master document"}') }}</div>
        <MomentCard :id="s.mx.m.p.MultiLingualMaster" :domainname="props.domainname" title="true" />
      </div>

      <div v-if="s.mx.multilinguals.length > 0">
        <div>{{ __s('{ "zh":"各语言版本", "en":"Various language versions"}') }}</div>
        <MomentCard v-for="id in s.mx.multilinguals" :id="id" :domainname="props.domainname" lang="true" title="true" />
        <button @click="addMultilingual">{{ __s('{ "zh":"添加语言版本", "en":"Add language version"}') }}</button>
      </div>
    </div>
    

    <div>{{ __s('{ "zh":"语言", "en":"Language"}') }}</div>
    <div>
      <select v-model="s.mx.m.p.LangCode">
        <option></option>
        <option v-for="lang in ['en','zh']">{{ lang }}</option>
      </select>
    </div>
    <div>{{ __s('{ "zh":"标题", "en":"Caption"}') }}</div>
    <div>
      <input v-model="s.mx.m.p.Title" type="text" class="w-[1500px]">
    </div>
    <div>{{ __s('{ "zh":"摘要", "en":"Summary"}') }}</div>
  
    <div class="flex justify-start">
      <textarea @keydown="renderSummary" v-model="s.mx.m.p.Summary" class="w-[760px] h-[300px] overflow-y-scroll"></textarea>
      <div class="w-[760px] h-[300px] bg-[#ffffdd] overflow-y-scroll pb-[50px]" v-html="s.renderSummary" />
    </div>
  
    <div>{{ __s('{ "zh":"海报", "en":"Poster"}') }}</div>
    <div>
      <input v-model="s.mx.m.p.PreviewImgUrl" type="text" class="w-[1500px]">
    </div>
    <div v-if="s.mx.m.p.PreviewImgUrl.length > 0">
      <img :src="s.mx.m.p.PreviewImgUrl" >
    </div>
  
    <div class="flex justify-start">
      <textarea @keydown="renderFullText" v-model="s.mx.m.p.FullText" class="w-[760px] h-[800px] overflow-y-scroll"></textarea>
      <div class="w-[760px] h-[800px] bg-[#ffffdd] overflow-y-scroll pb-[50px]" v-html="s.renderFullText" />
    </div>
  
    <div class="m-3 p-3">
      <Uploader :domainname="props.domainname" />
      <button v-if="s.mx.m.id == 0" @click="editMoment">{{ __s('{ "zh":"创建", "en":"Create"}') }}</button>
      <button v-else @click="editMoment">{{ __s('{ "zh":"修改", "en":"Edit"}') }}</button>
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
  
const props = defineProps(['mx','domainname'])
props.mx as fa.MomentComplex
props.domainname as string
  
  watch(() => props.mx,(newValue, oldValue) => {
    s.mx = props.mx,
    renderSummary()
    renderFullText()
  })
  
  const s = glib.vue.reactive({
    renderSummary: "",
    renderFullText: "",
    mx: glib.Mor.fa.MomentComplex_empty(),
    expand: false
  })
  
  const emits = defineEmits(['changed']) 
  
  const editMoment = async () => {
    Common.loader('/api/eu/moment', { 
      id: Number(s.mx.m.id),
      p: s.mx.m.p },(rep:any) => {
      
    })
  }
  
const convertMultilingual = async() => {
  Common.loader('/api/eu/moment', { 
    id: Number(s.mx.m.id),
    act: 'convert-to-multilingual' },(rep:any) => {
    s.mx = rep.data as fa.MomentComplex
  })
}

const addMultilingual = async() => {
  Common.loader('/api/eu/moment', { 
    id: Number(s.mx.m.id),
    act: 'add-multilingual' },(rep:any) => {
    s.mx = rep.data as fa.MomentComplex
  })
}

const convertParent = async() => {
  Common.loader('/api/eu/moment', { 
    id: Number(s.mx.m.id),
    act: 'convert-to-parent' },(rep:any) => {
    s.mx = rep.data as fa.MomentComplex
  })
}

const addOffspring = async() => {
  Common.loader('/api/eu/moment', { 
    id: Number(s.mx.m.id),
    act: 'add-offspring' },(rep:any) => {
    s.mx = rep.data as fa.MomentComplex
  })
}

  const renderSummary = () => {
    s.renderSummary = markdown__html(s.mx.m.p.Summary)
  }
  const renderFullText = () => {
    s.renderFullText = markdown__html(s.mx.m.p.FullText)
  }

// {{ __s('{ "zh":"", "en":""}') }}
const __s = (s:string) => {
  let items = JSON.parse(s)
  return items[runtime.lang] 
}


  glib.vue.onMounted(async () => {
    s.mx = props.mx
    renderSummary()
    renderFullText()
  })
  
</script>
  
  