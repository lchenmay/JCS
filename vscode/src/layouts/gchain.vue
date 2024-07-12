<template>

<div class="flex justify-center bg-[#e9e9f0] pb-[200px]">
  <div class=" flex justify-center bg-[#fafbfd] w-[950px]">
    <div class="flex flex-col bg-[#ffffff] w-[850px]">


      <div class="self-center">
        <a href="https://gcha.in">
          <img class="w-[250px] mt-[60px]" src="https://i.imgur.com/XpzVLGu.png" />
        </a>
        <div class="w-[800px] bg-[#336699] h-[3px] my-2"></div>
        <h1 class="text-xl text-[#ff9933] pt-5 mb-2">
          Shorten, Comment, Bookmark and Claim Your Crypto Links
        </h1>
        <div class="h-[50px]" ></div>
      </div>

      <div class="mb-[60px] self-center">
        <UserAuth />
      </div>

      <router-view />

      <div class="py-[60px] pl-[20px] mb-[800px]">
        <router-link :to="{ name: 'gchain-concept' }">Concept</router-link> |
        About Us | Contact Us | Community |
        Website frontend updated 6/27/2024
      </div>

    </div>  
  </div>
</div>

</template>


<script setup lang="ts">

import { glib } from '~/lib/glib'
import { router } from '~/lib/mod/route';
import UserAuth from '~/pcs/gchain/UserAuth.vue'

const _rt = gchainRt

</script>