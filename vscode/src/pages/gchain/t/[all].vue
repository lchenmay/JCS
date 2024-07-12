<template>

  <div>GCHAIN </div>

  <div>HASH: {{ s.hash }}</div>

  <div>You are about to be forwarded to an external website. </div>

  
  <OgCard :clink="s.clink" />

  <!-- <component :is="dyComponent"></component> -->

</template>


<script setup lang="ts">

import { glib } from '~/lib/glib'
import OgCard from '~/pcs/gchain/OgCard.vue'
import { CreateOgCard } from '~/pcs/pcs';

const dyComponent: any = glib.vue.shallowRef(null)


const s = glib.vue.reactive({
  hash: rtxx.router.currentRoute.params.all,
  clink: glib.Mor.gchain.CLINK_empty(),
})
glib.vue.onMounted(async () => {

  const rep = await glib.post('/api/public/loadCryptoLink', {
    tiny: s.hash
  })

  if (rep.Er == "OK") {
    s.clink = rep.clink
  }

  // const ogCard1 = CreateOgCard(s.clink)

  // dyComponent.value = ogCard1


})

</script>