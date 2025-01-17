<template>

  <div class="flex flex-col border-2">
    <div v-for="i in s.selected">
      <div>
        {{ i.p.Desc }}
      </div>
    </div>

    <input type="file" @change="onFileChange" />
    <textarea class="h-[150px]" v-model="s.desc"></textarea>
    <div>{{ s.fileName }}</div>
    <div>{{ s.res }}</div>
    <hr>
    <div>
      <button v-if="s.expand" @click="toggle">{{ __s('{ "zh":"收起文件库", "en":"Collapse"}') }}</button>
      <button v-else @click="toggle">{{ __s('{ "zh":"展开文件库", "en":"Expand"}') }}</button>
    </div>
    <div v-if="s.expand" class="flex flex-row flex-wrap">
      <div v-for="i in s.files" class="shadow-xl rounded-lg m-1 p-2 w-[370px]">
        <div
          v-if="i.p.Suffix == 'jpg' || i.p.Suffix == 'jpeg' || i.p.Suffix == 'png' || i.p.Suffix == 'gif' || i.p.Suffix == 'bmp' || i.p.Suffix == 'svg'"
          class="h-[100px] m-2"><img :src="'data:image/png;base64,' + i.p.Thumbnail">
        </div>
        <div v-if="i.p.Suffix == 'mp3'">
          <audio controls>
            <source :src="'https://' + s.domainname + '/file/' + i.id + '.' + i.p.Suffix" type='audio/mp3' />
          </audio>
        </div>
        <div>
          <div><a :href="'https://' + s.domainname + '/file/' + i.id + '.' + i.p.Suffix" target="_blank">{{ "https://" +
            s.domainname + "/file/" + i.id + "." + i.p.Suffix }}</a></div>
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

import { translate } from '~/lib/bizLogics/lang'
import { glib } from '~/lib/glib'
import { loader,asyncPost } from '~/lib/store/common'
import { checkUrl } from '~/lib/util/fetch'
import { BinIndexed } from '~/lib/util/bin'

const s = glib.vue.reactive({
  fileName: "",
  fileContent: "",
  desc: "",
  res: " - ",
  files: [] as fa.FILE[],
  selected: [] as fa.FBIND[],
  domainname: runtime.domainname,
  expand: false
})

const reload = () => {
  loader('/api/eu/files', {}, (rep: any) => {
    s.files = rep.list as fa.FILE[]
  })
}


const upload = 
  (suc:Function,fail:Function) => 
  (file:any,dst:string,desc:string,res:string) => {

  let url = checkUrl(dst)

  let reader = new FileReader()
  reader.onloadend = async(e) => {

    let bin = reader.result as ArrayBuffer
    let l = bin.byteLength
    if(bin && l > 0){

      let size = 8192
      let n = Math.ceil(l / size)
      
      let rep: any = await asyncPost(url,{
        filename: encodeURIComponent(file.name),
        desc: encodeURIComponent(desc),
        length: l,
        block: n,
        size: size})
        
      console.log(rep)

      if(rep?.Er == "OK"){
        let id = rep.id as number
        let bi:BinIndexed = {
          bin: bin,
          index: 0
        }
        let view = new Uint8Array(bi.bin)

        for(let i = 0; i < n; i ++){

          let iend = bi.index + size
          if(iend > l)
            iend = l

          let block = view.subarray(bi.index,iend)
          
          let bstr = ''
          for (let i = 0; i < block.byteLength; i++) 
            bstr += block[i].toString(16).padStart(2, '0')
            //bstr += String.fromCharCode(block[i])

          s.res = (100 * i % n).toFixed(2) + " %" 

          let r = await asyncPost(url,{
            id: id,
            block: i,
            length: l,
            data: bstr})

          bi.index = iend
        }

        s.res = "OK"

      }
    }
  }

  reader.readAsArrayBuffer(file)
}


const onFileChange = async (e: any) => {
  let file = e.target.files[0]
  if (file) {
    s.fileName = file.name
    upload(((rep: any) => {
      s.res = "OK"
      s.expand = true
      reload()
    }), ((rep: any) => {
      s.res = "Failed"
    }))(file, '/upload', s.desc,s.res)
  }
}

const update = (file: fa.FILE) => {
  loader('/api/eu/file', {
    act: 'update',
    p: file.p,
    id: Number(file.id)
  }, (rep: any) => {
  })
}

const remove = (file: fa.FILE) => {
  loader('/api/eu/file', {
    act: 'remove',
    id: Number(file.id)
  }, (rep: any) => {
    s.files = s.files.filter((i) => i.id != file.id)
  })
}

const toggle = () => {
  s.expand = !s.expand
  if (s.expand) {
    reload()
  }
}

// {{ __s('{ "zh":"", "en":""}') }}
const __s = (s: string) => {
  let items = JSON.parse(s)
  return items[runtime.lang]
}

glib.vue.onMounted(async () => {

})

</script>