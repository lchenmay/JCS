<template>

<div class="flex flex-col border-2">
  <div>
    Create or select from the library
  </div>
  <div v-for="i in s.selected">
    <div>
      {{ i.p.Desc }}
    </div>
  </div>

  <input type="file" @change="onFileChange"  />
  <textarea class="h-[150px]" v-model="s.desc"></textarea>
  <div>{{ s.fileName }}</div>
  <div>{{ s.res }}</div>
  <hr>
  <div>
    <button v-if="s.expand" 
      @click="toggle">Collapse</button>
    <button v-else   
      @click="toggle">Expand</button>
  </div>
  <div v-if="s.expand">
    <div v-for="i in s.files" class="flex">
      <div class="w-[100px] h-[100px] m-2"><img :src="'data:image/png;base64,' + i.p.Thumbnail"></div>
      <div>
        <div><a :href="'https://jcatsys.com/file/' + i.id + '.' + i.p.Suffix" target="_blank">{{ "https://jcatsys.com/file/" + i.id + "." + i.p.Suffix }}</a></div>
        <div>{{ i.p.Caption }}</div>
        <div>{{ i.p.Desc }}</div>  
        <div><button class="button-small">Toggle</button></div>
      </div>
    </div>
  </div>
</div>

</template>


<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import { upload,checkUrl }  from '~/lib/util/fetch'

const s = glib.vue.reactive({
  fileName: "",
  fileContent: "",
  desc: "",
  res: " - ",
  files: [] as jcs.FILE[],
  selected: [] as jcs.FBIND[],
  expand: false
})

const reload = () => {
  Common.loader('/api/eu/files', { },(rep:any) => {
      s.files = rep.list as jcs.FILE[]
    })
}

const onFileChange = async (e:any) => {
  let file = e.target.files[0]
  if(file){
    s.fileName = file.name
    upload(((rep:any) => {
      s.res = "OK"
      s.expand = true
      reload()
    }),((rep:any) => {
      s.res = "Failed"
    }))(file,'/upload',s.desc)
  }
}

const toggle = () => {
  s.expand = !s.expand
  if(s.expand){
    reload()
  }
}

glib.vue.onMounted(async () => {

})

</script>

