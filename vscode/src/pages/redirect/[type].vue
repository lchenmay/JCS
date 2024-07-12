<template>
  <div>{{ s.msg }}</div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { glib } from '~/lib/glib'

const s = glib.vue.reactive({
  biz: rtxx.router.currentRoute.params.type,
  msg: ""
})


glib.vue.onMounted(async () => {
  const options = glib.auth.biz__LoginOptions(s.biz)
  await glib.auth.LoginOption__RT(options).catch(er=>s.msg=`Something wrong ${er}`)

  if (rtxx.session) { rtxx.router.push("/") }
})



</script>