<template>
  <div class="max-w-[800px] w-full flex flex-col gap-2">
    <button @click="_rt.router.back()">←</button>
    <textarea class="h-[300px]" v-model="s.inputtext"></textarea>
    <button @click="onRenderClick()">Render</button>
    <textarea class="h-[500px]" v-html="s.outputtext"></textarea>
    <div v-html="s.outputtext"></div>
  </div>
</template>

<script setup lang="ts">
import { glib } from '~/lib/glib'
const _rt = rtxx

const md = `
# This is an <h1> tag
## This is an <h2> tag
### This is an <h3> tag
#### This is an <h4> tag

_This will also be italic_

__This will also be bold__
* Item 1
* Item 2
* Item 3

1. Item 1
2. Item 2
3. Item 3

![博客园logo](https://news.cnblogs.com/images/logo.gif)

`

const s = glib.vue.reactive({
  inputtext: md,
  outputtext: glib.markdown.markdown__html(md)
})

const onRenderClick = () => {
  s.outputtext = glib.markdown.markdown__html(s.inputtext)
}
</script>