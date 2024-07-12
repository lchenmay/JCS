<template>

  <div class="w-[400px] p-[20px] rounded-lg shadow-2xl">

    <div v-if="_rt.session">

      <div class="flex flex-row border-blue bg-red">

        <img class="w-[40px] mb-[25px] rounded-3xl" v-if="s.ec.eu.p.SocialAuthAvatar"
          :src="s.ec.eu.p.SocialAuthAvatar" />

        <div class="flex flex-col ml-[25px]">
          <div v-if="s.ec.eu.p.Caption">
            {{ s.ec.eu.p.Caption }}
          </div>
          <div v-if="s.ec.eu.p.SocialAuthId">
            {{ s.ec.eu.p.SocialAuthId }}
          </div>
        </div>

        <div class="ml-[30px]">
          <button class="w-[100px]" @click="glib.auth.SignOut()">Sign Out</button>
        </div>

      </div>

      <router-link :to="{ name: 'gchain-myClinks' }">My Clinks</router-link>

    </div>

    <div v-else>
      <div class="mb-[20px]">
        Stay logged in. We will encrypt your identity into the generated links.
        The cypto links will be your own property!
      </div>
      <div class="flex flex-row">
        <div><img src="/svg/logo_google.svg" /></div>
        <div><img src="/svg/logo_x.svg" /></div>
        <a :href="glib.auth.host__DiscordRedirectURL()">
          <img src="/svg/logo_discord.svg" />
        </a>
      </div>

    </div>


  </div>

</template>

<script setup lang="ts">

import { glib } from '~/lib/glib'
import { EU_empty } from '~/lib/shared/gchain/OrmMor';
const _rt = gchainRt

const s = glib.vue.reactive({
  ec: _rt.user
})

glib.vue.onMounted(async () => {
    if (_rt.session) {
      const rep = await glib.post('/api/eu/myProfile', {})
      if (rep.Er == "OK") {
        s.ec = rep.ec
    }
    _rt.user = rep.ec
  }

})


</script>