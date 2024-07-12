<template>

<div class="flex justify-center bg-[#e9e9f0] pb-[200px]">
  <div class=" flex justify-center bg-[#fafbfd] w-[1050px]">
    <div class="flex flex-col bg-[#ffffff] w-[950px]">

      <div class="flex flex-row mb-[20px]">
        <div class="pr-[250px]">
          <a href="https://cpto.cc/">
            <img src="https://i.imgur.com/hzWYQow.png" >
          </a>
        </div>

        <div>
            <UserAuth />
        </div>
      </div>

      <router-view />

    </div>  
  </div>
</div>  

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import UserAuth from '~/pcs/ctc/UserAuth.vue'

const _rt = ctcRt

</script>

