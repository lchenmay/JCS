<template>
  <div class="flex flex-col gap-2">
    <button @click="_rt.router.back()">←</button>
    <div class="size-full">
      <WebGLCanvas />
    </div>
  </div>
</template>

<script setup lang="ts">
import { glib } from '~/lib/glib'
import WebGLCanvas from '~/pcs/WebGLCanvas.vue'
const _rt = rtxx

</script>