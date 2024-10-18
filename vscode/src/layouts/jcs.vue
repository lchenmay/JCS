<template>
  <div><button @click="$router.push('/CodeRobotCenter')">Code Robot Center</button></div>
  <router-view />
</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'

const _rt = runtime

</script>

