<template>
  <v-app>
    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script lang="ts">
import { defineComponent, onMounted } from 'vue'
import { useAuthenticationStore } from './stores/authentication'

export default defineComponent({
  name: 'App',
  setup() {
    const authenticationStore = useAuthenticationStore()

    onMounted(() => {
      const authenticationDataRaw: string | null = localStorage.getItem('authenticationState')

      if (authenticationDataRaw != null) {
        const parsedAuthenticationData = JSON.parse(authenticationDataRaw)

        authenticationStore.writeAllFieldsInStore(parsedAuthenticationData.email, parsedAuthenticationData.role, parsedAuthenticationData.token, parsedAuthenticationData.tokenExpiration, parsedAuthenticationData.refreshToken, parsedAuthenticationData.refreshTokenExpiration)
      }
    })

    return {}
  }
})
</script>


<style lang="scss" scoped>
@use './assets/styles/config' as *;

:root {
  font-family: "Open Sans", sans-serif;
}

body {
  margin: 0;
  color: $mainFontColor;
  background-color: $mainBackgroundColor;
}
</style>
