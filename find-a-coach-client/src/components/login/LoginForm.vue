<template>
  <loading-square v-if="isLoading"></loading-square>

  <div class="container-for-login-form">
    <section class="login-form">
      <ul class="login-form-items">
        <li class="login-form-items_caption">
          <h1 class="login-form-items_caption-element">Welcome back</h1>
        </li>
        <li class="login-form-items_email-input">
          <input v-model="email" class="login-form-items_email-input-element" type="email" placeholder="Email">
        </li>
        <li class="login-form-items_password-input">
          <input v-model="password" class="login-form-items_password-input-element" type="password" placeholder="Password">
        </li>
        <li class="login-form-items_forgot-password">
          <span v-if="isForgotPasswordMessageShown" class="login-form-items_forgot-password-inscription">Forgot your password? Reset it by clicking <span @click="resetPassword" class="login-form-items_forgot-password-inscription-reset-password">here</span></span>
        </li>
        <li class="login-form-items_error-message">
          <span v-if="errorMessage" class="login-form-items_error-message-element">{{ errorMessage }}</span>
          <span v-if="isInformationMessageShown" class="login-form-items_information-message-element">If the email is correct, instructions for resetting password have been sent.</span>
        </li>
        <li class="login-form-items_button">
          <login-button @buttonClick="login"></login-button>
        </li>
        <li class="login-form-items_inscription">
          <span class="login-form-items_inscription-question">Don't have an account? </span>
          <router-link to="/register" class="login-form-items_inscription-link">Sign up</router-link>
        </li>
      </ul>
    </section>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'

import LoginButton from './LoginButton.vue'
import LoadingSquare from '../LoadingSquare.vue'
import useLogin from '../../composables/authentication/useLogin'
import { useAuthenticationStore } from '../../stores/authentication'
import useGetCompleteProfileWindowState from '../../composables/my-profile/complete-profile-window/useGetCompleteProfileWindowState'

export default defineComponent({
  components: {
    LoginButton,
    LoadingSquare
  },
  setup() {
    const email = ref<string>('')
    const password = ref<string>('')
    const errorMessage = ref<string | null>('')
    const isForgotPasswordMessageShown = ref<boolean>(false)
    const isInformationMessageShown = ref<boolean>(false)
    const isLoading = ref<boolean>(false)
    let loadingTimeout: ReturnType<typeof setTimeout> | null = null

    const router = useRouter()
    const authenticationStore = useAuthenticationStore()

    const login = async function() {
      isForgotPasswordMessageShown.value = false
      isInformationMessageShown.value = false

      loadingTimeout = setTimeout(() => {
        isLoading.value = true
      }, 1000)

      const result = await useLogin(email.value, password.value)

      clearTimeout(loadingTimeout)
      loadingTimeout = null
      isLoading.value = false

      if (!result.isSuccessful) {
        errorMessage.value = result.errorMessage

        if (email.value.length > 5 && email.value.length < 50) {
          const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/

          if (emailRegex.test(email.value)) {
            isForgotPasswordMessageShown.value = true
          }
        }
      } else {
        const completeProfileWindowState = await useGetCompleteProfileWindowState()
        
        router.push({ 
        path: '/home', 
        query: { 
          isCompleteProfileWindowVisible: String(completeProfileWindowState.isVisible),
          completeProfileWindowTitle: completeProfileWindowState.title
        }
      })
      }
    }

    const resetPassword = async function() {
      loadingTimeout = setTimeout(() => {
        isLoading.value = true
      }, 1000)

      await authenticationStore.sendForgotPasswordEmail(email.value)

      clearTimeout(loadingTimeout)
      loadingTimeout = null
      isLoading.value = false

      errorMessage.value = null
      isInformationMessageShown.value = true
    }

    return { login, email, password, errorMessage, isLoading, isForgotPasswordMessageShown, isInformationMessageShown, resetPassword }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.loading-spinner {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;

  &::before {
    content: "";
    width: 300px;
    height: 400px;
    background-color: white;
    border-radius: 20px;
    position: absolute;
    z-index: -1;
  }

  &-content {
    position: relative;
    width: 50px;
    height: 50px;
    margin-top: 24px;
  }

  &-inscription {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -130%);
    font-size: 16px;
    font-weight: bold;
    color: $mainColor;
    text-align: center;
    white-space: nowrap;
    pointer-events: none;
    padding-bottom: 24px;
    font-size: 24px;
  }
}

.container-for-login-form {
  display: flex;
  justify-content: center;
  align-items: center;
}

.login-form {
  margin: 50px 0 100px 0;
  padding: 125px 75px;
  border-radius: 30px;;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);

  @media (max-width: $breakpoint) {
    margin: 20px 0 50px 0;
    padding: 100px 50px;
    box-shadow: none;
  }

  &-items {
    margin: 0;
    padding: 0;
    list-style: none;
    
    &_caption {

      &-element {
        font-size: 28px;
        margin: 0 0 70px 10px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_email-input {
      margin-bottom: 30px;

      &-element {
        padding: 0 20px;
        font-size: 14px;
        width: 342px;
        height: 40px;
        border-radius: 30px;
        border: 2px rgb(177, 177, 177) solid;
        transition: border-color 0.3s ease, box-shadow 0.3s ease;

        &:focus {
          outline: none;
          box-shadow: 0 0 5px rgba($mainColor, 0.5);
        }

        @media (max-width: $breakpoint) {
          height: 36px;
          font-size: 12px;
        }
      }
    }

    &_password-input {

      &-element {
        padding: 0 20px;
        font-size: 14px;
        width: 342px;
        height: 40px;
        border-radius: 30px;
        border: 2px rgb(177, 177, 177) solid;
        transition: border-color 0.3s ease, box-shadow 0.3s ease;

        &:focus {
          outline: none;
          box-shadow: 0 0 5px rgba($mainColor, 0.5);
        }

        @media (max-width: $breakpoint) {
          height: 36px;
          font-size: 12px;
        }
      }
    }

    &_forgot-password {
      font-size: 14px;
      margin: 4px 0 0 20px;

      &-inscription {

        &-reset-password {
          color: $linkColor;

          &:hover {
            cursor: pointer;
            text-decoration: underline;
          }
        }
      }
    }

    &_error-message {
      color: $errorColor;
      font-size: 14px;
      margin: 20px 0 0 20px;
      width: 300px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }

    &_information-message-element {
      color: green;
    }

    &_button {
      margin-top: 70px;
    }

    &_inscription {
      font-size: 14px;
      justify-self: center;
      margin-top: 6px;

      &-link {
        font-size: 14px;
        color: $linkColor;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
  }
}
</style>
