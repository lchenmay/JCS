<template>
    <input v-model="s.url"/>
    <button @click="getHTML()">get</button>
    <div class="max-w-[800px]">


    </div>
</template>

<script setup lang="ts">
import { glib } from '~/lib/glib'
import { compTitle } from '~/pcs/c';
const _rt = rtxx

const s = glib.vue.reactive({
    url:"https://x.com",
    html: "",
    og_img: "",
})

const getHTML = async () => {
    const res = await glib.get(`https://cors-anywhere.herokuapp.com/${s.url}`,{})
    s.html=res.data
}
</script>