<template>
  <div class="max-w-[800px] w-full flex flex-col gap-5 p-2">
    <h1 class="text-center py-10 ">GCHAIN</h1>
    <RouterLink class="bg-blue-100 rounded-lg text-center" to="/ctc">Go to CTC</RouterLink>
    <RouterLink class="bg-blue-100 rounded-lg text-center" to="/gchain">Go to GCHAIN</RouterLink>

    <RouterLink class="bg-blue-100 rounded-lg text-center" to="/admin/mdtohtml">Go to md__html</RouterLink>
    <RouterLink class="bg-blue-100 rounded-lg text-center" to="/t">Go to TEST</RouterLink>
    <RouterLink class="bg-blue-100 rounded-lg text-center" to="/webgl">Go to WebGL</RouterLink>



  </div>
</template>

<script setup lang="ts">
import { glib } from '~/lib/glib'
import { compTitle } from '~/pcs/pcs';
const _rt = rtxx


</script>
