<template> 
  <div class="header">
    <full-logo></full-logo>
  </div>

  <div class="message">
    <img class="message-icon" src="../assets/images/icons/session-expired-icon.svg" alt="Error icon">
    <h1 class="message-title">Your session has expired</h1>
    <p class="message-incription">Please log into your account <br>You will be redirected to the login page</p>
  </div>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent, onMounted } from 'vue'

import FullLogo from '../components/FullLogo.vue'
import TheFooter from '../components/TheFooter.vue'

import { useRouter } from 'vue-router'

export default defineComponent({
  components: { 
    TheFooter,
    FullLogo
  },
  setup() {
    const router = useRouter()

    onMounted(() => {
      setTimeout(() => {
        router.push('/login')
      }, 5000)
    })
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
  justify-content: center;
  align-items: center;
  flex-direction: column;
  height: 500px;

  &-icon {
    width: 110px;

    @media (max-width: $breakpoint) {
      width: 80px;
    }
  }
  &-title {
    font-size: 26px;
    padding-top: 10px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }
  &-incription {
    font-size: 14px;
    padding-top: 6px;
    text-align: center;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }
}
</style>