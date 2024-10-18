<template>
  <div>/src/layouts/game.vue</div>
  <div><button @click="$router.push('/admin')">$router.push('/admin')</button></div>
  <router-view />
</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'

const _rt = runtime

</script>

