<template>




      <div v-if="s.hiddenAdvanced"
        class="flex flex-row self-center mb-[20px]">
        Hold on! Before shorten your link, try something diffferent:
        <div @click="s.hiddenAdvanced = false"
          class="ml-2 bg-[#ddffee] cursor-pointer">
          Make the link your property！
        </div>
      </div>

      <div v-else class="flex flex-col self-center w-[600px]">

      <div class="py-[20px]">

      <div>
      Even add your specific data into the tiny link. 
      Before redirecting to the intended website, 
      this message will be displayed when clicking the tiny link.
      Try it.
      </div>

      <textarea 
        class="w-[100%] h-[140px]"
        v-model="s.clink.p.Data"></textarea>
      <button
        class="mt-[10px] mb-[30px]"
        >Add Data</button>
      </div>

      </div>

      <div class="self-center w-[600px]">
        <textarea 
          class="w-[100%] h-[140px]"
          v-model="s.clink.p.Src">
        </textarea>
        <button 
          class="mt-[10px] h-[40px]"
          @click="Generate">Generate</button>
        <div v-if="s.generating">Checking information from the website ...</div>
      </div>



      <div 
        class="self-center my-[30px]"
        v-if="s.clink.id > 0">

        <hr class="my-[50px]">

        <div 
          class="mb-[20px]"
          v-if="s.clink.p.Src">
          <a :href="`https://opengraph.dev/panel?url=${ s.clink.p.Src }`" target="_blank">
            Check OG
            </a>
            |
          <a :href="`t/${ s.clink.p.HashTiny }`" target="_blank">
            Preview
            </a>
        </div>

        <OgCard :clink="s.clink"></OgCard>

      </div>

      <div class="py-[60px] pl-[20px] mt-[250px] mb-[50px]">
        Recent clinks

        <div v-for="item in s.clinks" >
            <OgCard :clink="item" />            
        </div>        

      </div>

</template>
  
<script setup lang="ts">

import { glib } from '~/lib/glib'
import OgCard from '~/pcs/gchain/OgCard.vue'

const _rt=ctcRt

const s = glib.vue.reactive({
  hiddenAdvanced: true,
  generating: false,
  clink: glib.Mor.gchain.CLINK_empty(),
  eu: glib.Mor.gchain.EU_empty(),
  clinks: [] as gchain.CLINK[],
  urlparam: glib.misc.url__Params(_rt.router.currentRoute.href)
})

glib.vue.onMounted(() => {

  if(s.urlparam){
    s.clink.p.Src = s.urlparam.url
  }

  loadHomepage()
  glib.route.addTrailingSlash()
})

const apiReq = () => {
  return {
    bizowner: 0,
    url: s.clink.p.Src,
    data: "",
    dst: "",
    session: "",
  }
}

const Generate = async () => {

  s.generating = true

  const rep = await glib.post('/api/public/checkoutCryptoLink', apiReq())

  s.generating = false

  if(rep.Er == "OK"){
    s.clink = rep.clink
  }
}


const loadHomepage = async () => {
  const rep = await glib.post('/api/public/homepage', {})
  if(rep.Er == "OK"){
    s.clinks = rep.clinks
  }
}

</script>
  