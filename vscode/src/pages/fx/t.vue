<template>
  <div class="max-w-[800px] w-full flex flex-col gap-5 p-2 bg-red-200">
    <h1 class="text-center py-10 ">TEST</h1>
    <div class="flex gap-2">
      <button @click="_rt.router.back()">←</button>
      <RouterLink class="bg-blue-100 rounded-lg text-center" to="/admin">Go to Admin</RouterLink>
      <RouterLink class="bg-blue-100 rounded-lg text-center" to="/t">Go to TEST</RouterLink>
      <RouterLink class="bg-blue-100 rounded-lg text-center" to="/webgl">WebGL</RouterLink>
      <button @click="glib.notify.onADDClick()">Notify</button>
    </div>
    <div class="flex flex-col gap-2">
      <button @click="onTogglePingClick()">AutoPing {{_rt.ws.autoping}},{{ _rt.ws.pinginterval }}</button>
      <button @click="onFetchClick()">fetch</button>
      Result
      <div>{{ s.res }}</div>
    </div>
    <compTitle msg="Imported component"></compTitle>

    <div v-if="_rt.bizList.length" v-for="item in _rt.bizList">
      id:{{ Number(item.id) }}, sort:{{Number(item.sort) }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { glib } from '~/lib/glib'
import { BytesBuilder } from '~/lib/util/bin';
import { compTitle } from '~/pcs/pcs';
const _rt = rtxx

const s = glib.vue.reactive({ res: {} })

const onFetchClick = async () => {
  wsapiTinyLink()
}
const onTogglePingClick = () => {
  rtxx.wsctx.autoping=!rtxx.wsctx.autoping
}

const apiReq = {
  api:"CheckoutTinyLink",
  bizowner: 1,
  url: "http://localhost:5173/",
  data: "ASDASDAS",
  dst: "",
  session: "",
}

const wsapiTinyLink = () => {
  const bb = new BytesBuilder()
  glib.Mor.Msg__bin(bb)({ e: 1, val: apiReq })
  glib.send(bb.bytes())
}



const fetchRESTTinyLink = async () => {
  s.res = await glib.post('/api/public/CheckoutTinyLink', apiReq)
}
</script>