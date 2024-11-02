<template>
  <div>
    <button @click="$router.push('/CodeRobot/Projects?sd=f&ss=3&d=')">
      Code Robot Center
    </button>
    $router.push('/CodeRobot/Projects?sd=f&ss=3&d=')
  </div>
  <router-view />
</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'

const _rt = runtime

</script>

