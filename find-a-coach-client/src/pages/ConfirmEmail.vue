<template> 
  <div class="header">
    <full-logo></full-logo>
  </div>

  <loading-square v-if="isLoading"></loading-square>

  <div class="message">
    <img class="message-image" src="../assets/images/email.png" alt="Image of email.">
    <h2 class="message-header">Registration successful!</h2>
    <h1 class="message-content">Please check your email and confirm your email address to activate your account</h1>
    <button @click="resendConfirmationEmail" class="message-button">Resend confirmation email</button>
    <span v-if="message" :class="{ 'message-success': !isError, 'message-error': isError }" class="message-response">{{ message }}</span>
  </div>

  <the-footer class="message-footer"></the-footer>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useAuthenticationStore } from '../stores/authentication'

import FullLogo from '../components/FullLogo.vue'
import TheFooter from '../components/TheFooter.vue'
import RegistrationForm from '../components/registration/RegistrationForm.vue'
import LoadingSquare from '../components/LoadingSquare.vue'

export default defineComponent({
  components: { 
    TheFooter,
    FullLogo,
    RegistrationForm,
    LoadingSquare
  },
  setup() {
    const message = ref<string | null>('')
    const isError = ref<boolean>(false)
    const isLoading = ref<boolean>(false)
    let loadingTimeout: ReturnType<typeof setTimeout> | null = null

    const route = useRoute()

    const resendConfirmationEmail = async function() {
      const authenticationStore = await useAuthenticationStore()

      loadingTimeout = setTimeout(() => {
        isLoading.value = true
      }, 1000)

      const result: { isSuccessful: boolean, errorMessage: string | null} = await authenticationStore.resendConfirmationEmail(route.query.email)

      clearTimeout(loadingTimeout)
      loadingTimeout = null
      isLoading.value = false

      if (!result.isSuccessful) {
        isError.value = true
        message.value = result.errorMessage
      } else {
        isError.value = false
        message.value = 'Confirmation email has been sent'
      }
    }

    return { message, isError, isLoading, resendConfirmationEmail }
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
      margin-top: 24px;
    }
  }
  &-content {
    font-size: 24px;
    margin-top: 10px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
      margin-top: 8px;
      width: 400px;
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
    margin: 20px 0 20px 0;

    &:hover {
      cursor: pointer;
      background-color: $secondaryColorHover;
    }

    @media (max-width: $breakpoint) {
      font-size: 12px;
      height: 40px;
      margin: 20px 0 100px 0;
    }
  }
  &-response {
    font-size: 14px;
    width: 300px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }
  &-error {
    color: $errorColor;
  }
  &-success {
    color: $successColor;
  }
  &-footer {
    margin-top: 100px;
  }
}

</style>