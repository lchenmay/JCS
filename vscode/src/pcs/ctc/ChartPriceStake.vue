<template>

    <div class="w-[400px] p-[20px] rounded-lg shadow-2xl">
        Price Stake Chart
    </div>

    <div class="w-[400px] p-[20px] rounded-lg shadow-2xl">
        <canvas ref="canvasRef" class="size-full"></canvas>
    </div>

</template>
  
<script setup lang="ts">
  
import { glib } from '~/lib/glib'

const _rt = ctcRt
const canvasRef = glib.vue.ref<HTMLCanvasElement | null>(null);
  
const s = glib.vue.reactive({
    g: null
})
  
glib.vue.onMounted(async () => {
    if (canvasRef.value) {

        _rt.canvas.canvasRef = canvasRef.value;
        _rt.canvas.gl = _rt.canvas.canvasRef.getContext('webgl');

        if (!_rt.canvas.gl) {
            console.error('Unable to initialize WebGL. Your browser may not support it.');
            return;
        }

        s.g = _rt.canvas.gl

        s.g.clearColor(0.3, 0.2, 0.5, 1.0);
        s.g.clear(s.g.COLOR_BUFFER_BIT);

        
    }
})
  
  
</script>

