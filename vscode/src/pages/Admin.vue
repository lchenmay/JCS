<template>

<div class="flex justify-center"><div class="hor-range">
    
      <div class="card">
        <div class="card-caption">Admin Console</div>
      </div>
    
      <div class="card">
        <div class="card-caption">Books</div>
        <div v-for="book in s.books">
          <div>{{ book.createdat }}</div>
          <div>{{ book.p.Caption }}</div>
          <div>{{ book.p.Email }}</div>
          <div v-html="book.p.Message" />
          <hr class="mt-3">
        </div>
      </div>
      

      <div class="card">
      <div class="card-caption">Page Log</div>
      <div v-for="plog in s.plogs">
        <div>{{ plog.time }}</div>
        <div>{{ plog.ip }}</div>
        <div>{{ plog.pathline }}</div>
        <div>{{ plog.from }}</div>
        <hr class="mt-3">
      </div>
    </div>


</div></div>
  
</template>
    
<script setup lang="ts">
  
import { glib } from '~/lib/glib'
import * as Common from '~/lib/store/common'
  
const s = glib.vue.reactive({
books: [] as jcs.BOOK[],
plogs: [] as any[]
})
  
glib.vue.onMounted(async () => {
  
  Common.loader('/api/admin/books', {},(rep:any) => {
    s.books = rep.list as jcs.BOOK[]
  })  
  Common.loader('/api/admin/plogs', {},(rep:any) => {
    s.plogs = rep.list 
  })  
})
  
</script>
  