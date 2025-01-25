<template> 
  <div class="w-[1200px] mr-[10px]">
    <div class="main-color w-screen">
  
  
    <div class="flex justify-start">
      <UserAuth @changed="onAuthChanged" />
    </div>  
  
    <div class="w-full flex justify-center bg-[#0d1a2f]">
      <img class="w-[150px] my-3" :src="checkUrl('/file/35461232.png')">
    </div>
  
    <div class="flex justify-center p-3"><div class="flex hor-range">
      <div class="menu"><a href="/">Home</a></div>
      <div class="menu"><a href="/moments/fp">Functional Programming</a></div>
      <div class="menu"><a href="/moments/ia">Immutable Architecture</a></div>
      <div class="menu"><a href="/moments/cat">Category Theory</a></div>
      <div class="menu"><a href="/moments/service"></a>Service</div>
      <div class="menu"><a href="/moments">Articles</a></div>
      <div class="menu"><a href="#contact">Contact</a></div>
    </div></div>
  
      <router-view />
  
      <div class="hor" />
  
      <div class="flex justify-center"><div class="hor-range">
      <div class="pt-2 pb-5">
          <br>© Copyright J-Cat Sys LLC. 2024
      </div>
      </div></div>
  
    </div>
  </div>
  </template>
    
  <script setup>
  
  import { glib } from '~/lib/glib'
  import { upload,checkUrl }  from '~/lib/util/fetch'
  
  import UserAuth from '~/comps/UserAuth.vue'
    
  </script>
    
