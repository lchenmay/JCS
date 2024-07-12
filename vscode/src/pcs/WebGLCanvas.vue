<template>
  <canvas ref="canvasRef" class="size-full"></canvas>
</template>

<script setup lang="ts">
import { glib } from '~/lib/glib'

const _rt = rtxx
const canvasRef = glib.vue.ref<HTMLCanvasElement | null>(null);

glib.vue.onMounted(() => {
  if (canvasRef.value) {
    
    rtxx.canvas.canvasRef = canvasRef.value; 
    rtxx.canvas.gl = rtxx.canvas.canvasRef.getContext('webgl');

    if (!rtxx.canvas.gl) {
      console.error('Unable to initialize WebGL. Your browser may not support it.');
      return;
    }

    const g = rtxx.canvas.gl

    g.clearColor(0.3, 0.2, 0.5, 1.0);
    g.clear(g.COLOR_BUFFER_BIT);

  }
});
</script>