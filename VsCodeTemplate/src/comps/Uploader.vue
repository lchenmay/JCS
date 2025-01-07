<template>

<div class="flex flex-col border-2">
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
      @click="toggle">{{ __s('{ "zh":"收起", "en":"Collapse"}') }}</button>
    <button v-else   
      @click="toggle">{{ __s('{ "zh":"展开", "en":"Expand"}') }}</button>
  </div>
  <div v-if="s.expand" class="flex flex-row flex-wrap">
    <div v-for="i in s.files" class="shadow-xl rounded-lg m-1 p-2 w-[370px]">
      <div v-if="i.p.Suffix == 'jpg' || i.p.Suffix == 'jpeg' || i.p.Suffix == 'png' || i.p.Suffix == 'gif' || i.p.Suffix == 'bmp' || i.p.Suffix == 'svg'"
        class="h-[100px] m-2"><img :src="'data:image/png;base64,' + i.p.Thumbnail">
      </div>
      <div v-if="i.p.Suffix == 'mp3'">
        <audio controls>
          <source :src="'https://' + props.domainname + '/file/' + i.id + '.' + i.p.Suffix" type='audio/mp3' />
        </audio>
      </div>
      <div>
        <div><a :href="'https://' + props.domainname + '/file/' + i.id + '.' + i.p.Suffix" target="_blank">{{ "https://" + props.domainname + "/file/" + i.id + "." + i.p.Suffix }}</a></div>
        <div>{{ i.p.Size }} B</div>
        <div><input v-model="i.p.Caption" @change="update(i)" type="text" class="w-[350px]"></div>
        <div><textarea v-model="i.p.Desc" @change="update(i)" type="text" class="w-[350px] h-[80px]"></textarea></div>  
        <div>
          <button class="button-small">[+/-]</button>
          <button @click="remove(i)" class="button-small">{{ __s('{ "zh":"删除", "en":"Remove"}') }}</button>
        </div>
      </div>
    </div>
  </div>
</div>

</template>


<script setup lang="ts">

import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
import { upload,checkUrl }  from '~/lib/util/fetch'

const props = defineProps(['domainname'])
props.domainname as string

const s = glib.vue.reactive({
fileName: "",
fileContent: "",
desc: "",
res: " - ",
files: [] as fa.FILE[],
selected: [] as fa.FBIND[],
expand: false
})

const reload = () => {
  Common.loader('/api/eu/files', { },(rep:any) => {
      s.files = rep.list as fa.FILE[]
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

const update = (file:fa.FILE) => {
  Common.loader('/api/eu/file', { 
    act: 'update', 
    p: file.p,
    id: Number(file.id)},(rep:any) => {
  })
}

const remove = (file:fa.FILE) => {
  Common.loader('/api/eu/file', { 
    act: 'remove', 
    id: Number(file.id)},(rep:any) => {
    s.files = s.files.filter((i) => i.id != file.id)
  })
}

const toggle = () => {
  s.expand = !s.expand
  if(s.expand){
    reload()
  }
}

// {{ __s('{ "zh":"", "en":""}') }}
const __s = (s:string) => {
  let items = JSON.parse(s)
  return items[runtime.lang] 
}

glib.vue.onMounted(async () => {

})

</script>

