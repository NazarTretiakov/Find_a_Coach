<template> 
  <div class="header">
    <full-logo></full-logo>
  </div>

  <div class="message">
    <img class="message-image" src="../assets/images/tick.png" alt="Image of email.">
    <h2 class="message-header">Email confirmed successfully!</h2>
    <router-link to="/home"><button class="message-button">Go to Home page</button></router-link>
  </div>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuthenticationStore } from '../stores/authentication'

import FullLogo from '../components/FullLogo.vue'
import TheFooter from '../components/TheFooter.vue'

export default defineComponent({
  components: { 
    TheFooter,
    FullLogo
  },
  setup() {
    const route = useRoute()
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()

    const email = route.query.email as string
    const role = route.query.role as string
    const token = route.query.token as string
    const tokenExpiration = new Date(route.query.expiration as string)
    const refreshToken = route.query.refreshToken as string
    const refreshTokenExpiration = new Date(route.query.refreshTokenExpiration as string)
    
    authenticationStore.writeAllFieldsInStore(email, role, token, tokenExpiration, refreshToken, refreshTokenExpiration)
    authenticationStore.saveAuthenticationStateInLocalStore()

    setTimeout(() => {
      router.push( { 
        path: '/home', 
        query: { 
          isCompleteProfileWindowVisible: 'true',
          completeProfileWindowTitle: "You have successfully created the account"
        }
      })
    }, 4000)
  }
})
</script>


<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.header {
  padding: 10px 100px;

  @media (max-width: $breakpoint) {
    padding: 10px;
  }
}

.message {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;

  &-image {
    width: 150px;
    margin-top: 100px;

    @media (max-width: $breakpoint) {
      width: 100px;
    }
  }
  &-header {
    font-size: 20px;
    margin-top: 30px;

    @media (max-width: $breakpoint) {
      font-size: 14px;
      margin-top: 20px;
    }
  }
  &-content {
    font-size: 24px;
    margin-top: 10px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
      margin-top: 8px;
    }
  }
  &-button {
    background-color: $secondaryColor;
    color: #FFFFFF;
    border: none;
    width: 200px;
    height: 46px;
    border-radius: 30px;
    transition: background-color 0.3s ease;
    font-size: 14px;
    margin: 20px 0 100px 0;

    &:hover {
      cursor: pointer;
      background-color: $secondaryColorHover;
    }

    @media (max-width: $breakpoint) {
      height: 40px;
      font-size: 12px;
      margin: 30px 0 250px 0;
    }
  }
}

</style>