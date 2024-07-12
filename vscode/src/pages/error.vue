<template>
  <div class="wrapper m-auto text-center">
    Welcome to {{ _rt.host.hostname }}<br>
    This Page does not exist. (HTTP 404)<br>
    <button @click="$router.push('/')">Go Home</button>
  </div>
</template>

<script setup lang="ts">
const _rt = rtxx
</script>