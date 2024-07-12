<template>

    <div class="mt-[40px]">
        <div v-for="item in glib.bin.dict__kvpArray(_rt.user.clinks)" >
            <OgCard :clink="item.v" />            
        </div>

    </div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { dict__kvpArray } from '~/lib/util/bin';
import OgCard from '~/pcs/gchain/OgCard.vue'
import UserAuth from '~/pcs/gchain/UserAuth.vue'

const _rt = gchainRt

glib.vue.onMounted(async ()=>{

    const rep = await glib.post('/api/eu/myClinks', {})
    if(rep.Er == "OK"){
        _rt.user.clinks = rep.list
    }

})

</script>