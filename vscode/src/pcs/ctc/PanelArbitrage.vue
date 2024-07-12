<template>

    <div class="w-[400px] p-[20px] rounded-lg shadow-2xl">
        <div>{{ props.ins }}</div>
        <div class="mt-[20px]">Caption</div>
        <input type="text" v-model="s.rcd.p.Caption">
        <div class="mt-[20px]">Stake</div>
        <input type="text" v-model="s.rcd.p.Stake">
        <div class="mt-[20px]">Entry</div>
        <input type="text" v-model="s.rcd.p.Entry">
        <div class="mt-[20px]">Exit</div>
        <input type="text" v-model="s.rcd.p.Exit">
        <div class="mt-[20px]">Description</div>
        <textarea v-model="s.rcd.p.Desc"></textarea>
        <button
            v-if="s.rcd.id == 0"
            class="mt-[20px]" @click="Create">Create</button>
        <button
            v-else
            class="mt-[20px]" @click="Update()">Update</button>
    </div>

    <div class="p-[20px] rounded-lg shadow-2xl">
        <div class="flex flex-row" 
            v-for="item in s.arbitrages">
            <button @click="s.rcd = item">Select</button>
            <div class="ml-[20px]">
            [{{ item.p.Code }}]
            Stake: {{ item.p.Stake }}
            {{ item.p.Entry }} ->
            {{ item.p.Exit }}
            {{ item.p.Caption }}
            {{ item.p.Desc }}
            </div>
        </div>
    </div>

</template>
  
<script setup lang="ts">
  
import { glib } from '~/lib/glib'
const _rt = ctcRt
  
const props = defineProps(['ins']) ;
props.ins as string

const s = glib.vue.reactive({
    rcd: glib.Mor.ctc.ARBITRAGE_empty(),
    arbitrages: [] as ctc.ARBITRAGE[],
    urlparams:glib.misc.url__Params(rtxx.router.currentRoute.href)
})

const Create = async () => {
    const rep = await glib.post('/api/public/createArbitrage', {
        p: glib.bin.data__base64(glib.Mor.ctc.pARBITRAGE__bin)(s.rcd.p),
        Ins: props.ins
    })
    if(rep.Er == "OK"){
        s.arbitrages.push(rep.arbitrage)
    }
}

const Update = async () => {
    const rep = await glib.post('/api/public/updateArbitrage', {
        rcd: glib.bin.data__base64(glib.Mor.ctc.ARBITRAGE__bin)(s.rcd)
    })
    if(rep.Er == "OK"){
    }
}

glib.vue.onMounted(async () => {

    const rep = await glib.post('/api/public/listArbitrage', {})
    if(rep.Er == "OK"){
        s.arbitrages = rep.list
    }

})
  
  
</script>

