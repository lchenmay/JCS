<template>
  <div v-if="props.clink.id >= 0">
    <div class="flex flex-col self-center w-[600px] p-[20px] shadow-2xl rounded-lg">
      <a :href="`/t/${props.clink.p.HashTiny}`" target="_blank">Preview</a>
      <div>
        <a class="text-[#336699] font-semibold" 
          :href="props.clink.p.Src">
          {{ props.clink.p.OgTitle }}</a>
      </div>
      <div>
        {{ props.clink.p.OgDesc }}
      </div>
      <div class="mt-[20px]">
        <a class="text-ellipsis" :href="props.clink.p.Src">
          {{ props.clink.p.Src }}
        </a>
      </div>
      <div class="mt-[20px]">
        <a :href="props.clink.p.Src">
          <img class="w-[560px]" :src="props.clink.p.OgImg" /></a>
      </div>

      <div v-if="props.clink.p.BizOwner > 0">
        (This site has been identified as one of our business partner.
        When you share the generator link,
        the effectiveness of your promotional efforts will be tracked and any owed compensation will be provided to our
        affiliate partner.)
        <div>(partner info content place holder)</div>
      </div>

    </div>


    <br>
    
  </div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
const _rt = rtxx

const props = defineProps<{clink: gchain.CLINK}>()

glib.vue.onMounted(() => {
  
})


</script>