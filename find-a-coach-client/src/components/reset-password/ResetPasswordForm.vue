<template> 
  <loading-square v-if="isLoading"></loading-square>
  
  <div class="container-for-reset-password-form">
    <section class="reset-password-form">
      <ul class="reset-password-form-items">
        <li class="reset-password-form-items_caption">
          <h1 class="reset-password-form-items_caption-element">Reset password</h1>
        </li>
        <li class="reset-password-form-items_email-input">
          <input class="reset-password-form-items_email-input-element" type="email" :value="email" disabled>
        </li>
        <li class="reset-password-form-items_password-input">
          <input v-model="password" class="reset-password-form-items_password-input-element" type="password" placeholder="New password">
        </li>
        <li class="reset-password-form-items_confirmation-password-input">
          <input v-model="confirmationPassword" class="reset-password-form-items_confirmation-password-input-element" type="password" placeholder="Confirm new password">
        </li>
        <li class="reset-password-form-items_error-message">
          <span v-if="errorMessage" class="reset-password-form-items_error-message-element">{{ errorMessage }}</span>
          <span v-if="isInformationMessageShown" class="reset-password-form-items_information-message-element">The password has been successfully changed.</span>
        </li>
        <li class="reset-password-form-items_button">
          <reset-password-button @buttonClick="resetPassword"></reset-password-button>
        </li>
      </ul>
    </section>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useAuthenticationStore } from '../../stores/authentication'

import ResetPasswordButton from './ResetPasswordButton.vue'
import LoadingSquare from '../LoadingSquare.vue'

export default defineComponent({
  components: { 
    ResetPasswordButton,
    LoadingSquare
  },
  setup() {
    let email: string = ''
    let token: string = ''
    const password = ref<string>('')
    const confirmationPassword = ref<string>('')
    const errorMessage = ref<string>('')
    const isInformationMessageShown = ref<boolean>(false)
    const isLoading = ref<boolean>(false)
    let loadingTimeout: ReturnType<typeof setTimeout> | null = null

    const route = useRoute()
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()

    email = route.query.email as string
    token = route.query.token as string

    const resetPassword = async function() {
      isInformationMessageShown.value = false

      loadingTimeout = setTimeout(() => {
        isLoading.value = true
      }, 1000)

      let result: { isSuccessful: boolean, errorMessage: string | null }

      if (password.value === confirmationPassword.value) {
        result = await authenticationStore.resetPassword(email, token, password.value)
      } else {
        result = {
          errorMessage: 'Password and confirmation password don\'t match',
          isSuccessful: false
        }
      }

      clearTimeout(loadingTimeout)
      loadingTimeout = null
      isLoading.value = false

      if (!result.isSuccessful) {
        errorMessage.value = result.errorMessage
      } else {
        errorMessage.value = null
        isInformationMessageShown.value = true
        setTimeout(() => {
          router.push('/home')
        }, 2000)
      }
    }

    return { email, password, confirmationPassword, errorMessage, isInformationMessageShown, isLoading, resetPassword}
  }
})
</script>


<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.container-for-reset-password-form {
  display: flex;
  justify-content: center;
  align-items: center;
}

.reset-password-form {
  margin: 50px 0 100px 0;
  padding: 125px 75px;
  border-radius: 30px;;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);

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

    &_confirmation-password-input {

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

    &_error-message {
      color: red;
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
      }
    }
  }
}
</style>