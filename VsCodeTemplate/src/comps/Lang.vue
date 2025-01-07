<template>

  <div class="flex">
    <button class="image-button">
      <img :src="checkUrl('/file/35461258.png')"  class="h-[30px]" />    
    </button>
    <div class="p-2">
      <select v-model="s.lang" @change="onSelect" class="min-w-[60px]">
        <option v-for="lang in ['en','zh']" :value="lang">{{ lang }}</option>
      </select>
    </div>
    <div class="p-2">
      <a href="/zh/">中文</a>
      | <a href="/en/">English</a>
    </div>
  </div>
  
  </template>
  
  
  <script setup>
  
  import { navigate } from '~/lib/mod/route'
  import { glib } from '~/lib/glib'
  import { upload,checkUrl }  from '~/lib/util/fetch'
  
  const s = glib.vue.reactive({
    user: runtime.user,
    lang: runtime.lang,
    expand: false
  })
  
  const emits = defineEmits(['changed']) 
  
  const onSelect = () => {
    runtime.lang = s.lang
    localStorage.setItem("runtime.lang",s.lang)
    s.expand = false
    emits('changed',runtime.lang)
  }
  
  glib.vue.onMounted(async () => {
  
    let changed = false
  
    let path = window.location.pathname    
    let langs = ['zh','en']
    langs.forEach((lang) => {
      let pattern = '/' + lang + '/'
      if(path.indexOf(pattern) >= 0){
        runtime.lang = lang
        localStorage.setItem("runtime.lang",lang)
        window.location.pathname = path.replace(pattern,'/')
        changed = true
      }
    })
  
    if(runtime.lang == ""){
      runtime.lang = "zh"
      localStorage.setItem("runtime.lang",s.lang)
      changed = true
    }
  
    if(changed){
      s.lang = runtime.lang
      emits('changed',runtime.lang)
    }
  })
  
  </script>
    