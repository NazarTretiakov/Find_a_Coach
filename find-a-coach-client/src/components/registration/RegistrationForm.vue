<template>
  <loading-square v-if="isLoading"></loading-square>

  <div class="container-for-registration-form">
    <section class="registration-form">
      <ul class="registration-form-items">
        <li class="registration-form-items_caption">
          <h1 class="registration-form-items_caption-element">Registration</h1>
        </li>
        <li class="registration-form-items_email-input">
          <input v-model="email" class="registration-form-items_email-input-element" type="email" placeholder="Email">
        </li>
        <li class="registration-form-items_password-input">
          <input v-model="password" class="registration-form-items_password-input-element" type="password" placeholder="Password">
        </li>
        <li class="registration-form-items_error-message">
          <span v-if="errorMessage" class="registration-form-items_error-message-element">{{ errorMessage }}</span>
        </li>
        <li class="registration-form-items_button">
          <registration-button @buttonClick="register"></registration-button>
        </li>
        <li class="registration-form-items_inscription">
          <span class="registration-form-items_inscription-question">Already have an account? </span>
          <router-link to="/login" class="registration-form-items_inscription-link">Sign in</router-link>
        </li>
      </ul>
    </section>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'

import RegistrationButton from './RegisterButton.vue'
import LoadingSquare from '../LoadingSquare.vue'
import useRegistration from '../../composables/authentication/useRegistration'


export default defineComponent({
  components: {
    RegistrationButton,
    LoadingSquare
  },
  setup() {
    const email = ref<string>('')
    const password = ref<string>('')
    const errorMessage = ref<string>('')
    const isLoading = ref<boolean>(false)
    let loadingTimeout: ReturnType<typeof setTimeout> | null = null

    const router = useRouter()
    
    const register = async function() {
      loadingTimeout = setTimeout(() => {
        isLoading.value = true
      }, 1000)

      const result = await useRegistration(email.value, password.value)

      clearTimeout(loadingTimeout)
      loadingTimeout = null
      isLoading.value = false

      if (!result.isSuccessful) {
        errorMessage.value = result.errorMessage
      } else {
        router.push({ path: '/register/confirm-email', query: { email: email.value } })
      }
    }

    return { register, email, password, errorMessage, isLoading }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.container-for-registration-form {
  display: flex;
  justify-content: center;
  align-items: center;
}

.registration-form {
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

    &_error-message {
      color: red;
      font-size: 14px;
      margin: 20px 0 0 20px;
      width: 300px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
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