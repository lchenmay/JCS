<template>
  <div class="overflow-hidden fixed w-full top-0 text-center mx-auto">
    <div class="w-full max-w-middle mx-auto mt-1 ">
      <transition name="fade">
        <div v-if="_rt.msgList.length" class="h-6 w-[360px] text-center ml-auto overflow-hidden rounded-xl" :class="_rt.msgList[0].bgcolor">
          <div>{{ _rt.msgList[0].msg }}</div>
        </div>
      </transition>
    </div>
  </div>
</template>

<script setup lang="ts">
import { glib } from '~/lib/glib'
const _rt = rtxx
</script>
