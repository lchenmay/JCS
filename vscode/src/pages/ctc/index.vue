<template>

<div class="max-w-[800px] w-full flex flex-row">
  <div class="w-[600px]">
    <div>
        <div v-for="item in s.mcs" :key="item.m.id">
          <MomentCard :moment="item" />
        </div>
    </div>
  </div>

  <div class="ml-[20px]">

      
      <div>
          <PanelArbitrage :ins="`FIL/USD`" />
      </div>

      <div>
          <ChartPriceStake />
      </div>

      <div class="mt-[10px] p-[10px]"
           v-for="item in s.curs" :key="item.id">
          <div class="flex flex-row">
              <img class="w-[50px] h-[50px]" :src="item.p.Icon">
              <div class="flex flex-col">
                  <div class="ml-[20px]">
                      {{ item.p.Code }} | {{ item.p.Caption }}
                  </div>
                  <div class="ml-[20px]">
                      {{ item.p.AnchorRate.toFixed(5) }}
                  </div>
              </div>
          </div>
      </div>

  </div>
</div>
 

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import MomentCard from '~/pcs/ctc/MomentCard.vue'
import UserAuth from '~/pcs/ctc/UserAuth.vue'
import ChartPriceStake from '~/pcs/ctc/ChartPriceStake.vue'
import PanelArbitrage from '~/pcs/ctc/PanelArbitrage.vue'

const s = glib.vue.reactive({
  mcs: [] as ctc.MomentComplex[],
  curs: [] as ctc.CUR[]
})

glib.vue.onMounted(() => {
  loadHomepages()
})

const loadHomepages = async () => {
  const rep = await glib.post('/api/public/homepage', {})
  if(rep.Er == "OK"){
    s.mcs = rep.mcs
    s.curs = rep.curs
  }
}

</script>