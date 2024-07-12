<template>
    <div v-if="props.moment.m.id >= 0">
      <div class="flex flex-col self-center w-[500px] p-[20px] shadow-2xl rounded-lg">
        <div>{{ props.moment.m.p.BizCode }}</div>
        <div>
          <a class="text-[#336699] font-semibold" :href="`m/${ props.moment.m.id }`" target="_blank">
            {{ props.moment.m.p.Title }}
        </a>
        </div>
        <div>
          {{ props.moment.m.p.Summary }}
        </div>
        <div class="mt-[20px]">
          <a :href="`m/${ props.moment.m.id }`" target="_blank">
            <img class="w-[100px]" :src="props.moment.m.p.PreviewImgUrl" />
          </a>
        </div>

    </div>
  
  
      <br>
      
    </div>
  
  </template>
  
  <script setup lang="ts">
  
  import { glib } from '~/lib/glib'
  const _rt = rtxx
  
  const props = defineProps(['moment']) ;
  props.moment as ctc.MomentComplex
  
  glib.vue.onMounted(() => {
    
  })
  
  
  </script>