<template>

  <div class="card">
    <div v-if="s.showCreateBtn">
      <button @click="onClickCreate">新建</button>
    </div>
  
    <div v-if="s.showMomentEdit">
      <button @click="onClickCollapse">收起</button>
      <RichTextEditor v-if="s.user.eu.p.AuthType == 2" :mx="s.mx" :domainname="props.domainname" lang="en" />
    </div>
  
    <div v-if="s.showMomentView">
      <div class="flex">
        <div v-if="props.summary && !props.fulltext">
          <img class="max-w-[200px] mr-3" :src="s.mx.m.p.PreviewImgUrl" />
        </div>
        <div>
          <div v-if="props.title" class="card-caption">
            <a @click="navigate('/m/' + s.mx.m.id,'m',s.mx.m.id)" href="#">{{ s.mx.m.p.Title }}</a>
          </div>
  
          <div v-if="!props.create">
            {{ (new Date(s.mx.m.createdat)).toLocaleDateString() }}
            <button v-if="s.user.eu.p.AuthType == 2" @click="s.showMomentEdit = true" class="button-small">
              编辑
            </button>
          </div>
  
          <div v-if="props.summary" v-html="markdown__html(s.mx.m.p.Summary)" />
        </div>
      </div>
  
      <div v-if="props.preview && props.fulltext">
        <img :src="s.mx.m.p.PreviewImgUrl" />
      </div>
      <div v-if="props.fulltext" v-html="markdown__html(s.mx.m.p.FullText)" class="mt-[100px] border-t-2 pt-[100px]" />
      <div v-if="props.link" class="mt-5">
        <a :href="'https://' + props.domainname + '/m/' + s.mx.m.id" target="_blank">
          {{ 'https://' + props.domainname + '/m/' + s.mx.m.id }}</a>
      </div>
  
      <div class="mt-3"></div>
  
    </div>
  
  </div>
      
  </template>
  
  <script setup lang="ts">
  
  import { getCurrentInstance, toRefs, watch } from 'vue'
  import { navigate } from '~/lib/mod/route'
  import { glib } from '~/lib/glib'
  import * as Common from '~/lib/store/common'
  import { upload,checkUrl }  from '~/lib/util/fetch'
  import { markdown__html } from '~/lib/util/markdown'
  
  import UserAuth from '~/comps/UserAuth.vue'
  import Uploader from '~/comps/Uploader.vue'
  import RichTextEditor from '~/comps/RichTextEditor.vue'
  
  const props = defineProps(['domainname','mx','id','create','title','summary','preview','fulltext','link'])
  props.domainname as string
  props.mx as jcs.MomentComplex
  props.id as number
  props.create as boolean
  props.title as boolean
  props.summary as boolean
  props.preview as boolean
  props.fulltext as boolean
  props.link as boolean
  
  const s = glib.vue.reactive({
    user: runtime.user,
    mx: glib.Mor.jcs.MomentComplex_empty(),
    router: runtime.router,
    href: window.location.href,
    showCreateBtn: false,
    showMomentView: false,
    showMomentEdit: false
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
  
  const onClickCreate = () => {
    s.showMomentEdit = true
    s.showCreateBtn = false
  }
  const onClickCollapse = () => {
    s.showMomentEdit = false
    if(props.create && s.mx.m.id == 0 && s.user.eu.p.AuthType == 2)
      s.showCreateBtn = true
  }
  
  watch(() => props.mx,(newValue, oldValue) => {
    s.mx = props.mx
    reload()
  })
  
  watch(() => props.id,(newValue, oldValue) => {
    reload()
  })
  
  glib.vue.onMounted(async () => {
    if(props.create && s.mx.m.id == 0 && s.user.eu.p.AuthType == 2)
      s.showCreateBtn = true
    if(!props.create)
      s.showMomentView = true
  
    reload()
  })
  
  </script>