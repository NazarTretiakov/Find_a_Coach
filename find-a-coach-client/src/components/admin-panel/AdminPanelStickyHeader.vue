<template>
  <header class="header">
    <full-logo class="header-logo"></full-logo>

    <ul class="header-buttons">
      <li class="header-buttons_menu" :class="{ 'header-buttons_menu-active': isMenuButtonActive, 'header-buttons_menu-unactive': !isMenuButtonActive }" @click="toggleMobileMenu"><img class="header-buttons_menu-icon" src="@/assets/images/icons/menu-icon.svg" alt="Menu icon"></li>
      <li v-if="!isAuthenticated" class="header-buttons_register"><register-button></register-button></li>
      <li v-if="!isAuthenticated" class="header-buttons_login"><login-button></login-button></li>
      <li v-if="isAuthenticated" class="header-buttons_logout"><logout-button @click="onLogout"></logout-button></li>
    </ul>
    <transition name="mobile-menu">
      <div v-if="showMobileMenu" class="header-mobile-menu">
        <router-link to="/register" v-if="!isAuthenticated" class="header-mobile-menu-link">Register</router-link>
        <router-link to="/login" v-if="!isAuthenticated" class="header-mobile-menu-link">Login</router-link>
        <span v-if="isAuthenticated" class="header-mobile-menu-link">Logout</span>
      </div>
    </transition>
  </header>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useAuthenticationStore } from '@/stores/authentication'
import { useRouter } from 'vue-router'

import FullLogo from '@/components/FullLogo.vue'
import RegisterButton from '@/components/RegisterButton.vue'
import LoginButton from '@/components/LoginButton.vue'
import LogoutButton from '@/components/admin-panel/LogoutButton.vue'

export default defineComponent({
  components: { 
    FullLogo,
    RegisterButton,
    LoginButton,
    LogoutButton
  },
    setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const isAuthenticated = ref<boolean>(authenticationStore.isAuthenticated)
    const BREAKPOINT = 768
    const showMobileMenu = ref<boolean>(false)
    const isMenuButtonActive = ref<boolean>(false)
    const isLoading = ref<boolean>(false)
    let loadingTimeout: ReturnType<typeof setTimeout> | null = null

    const toggleMobileMenu = () => {
      showMobileMenu.value = !showMobileMenu.value
      isMenuButtonActive.value = showMobileMenu.value
    }

    const handleResize = () => {
      if (window.innerWidth > BREAKPOINT && showMobileMenu.value) {
        showMobileMenu.value = false
        isMenuButtonActive.value = false
      }
    }

    const onLogout = async () => {
      loadingTimeout = setTimeout(() => {
        isLoading.value = true
      }, 1000)

      await authenticationStore.logout()

      clearTimeout(loadingTimeout)
      loadingTimeout = null
      isLoading.value = false

      router.push('/home')
    }

    onMounted(() => {
      window.addEventListener('resize', handleResize)
    })

    return { isAuthenticated, showMobileMenu, isMenuButtonActive, toggleMobileMenu, onLogout }
  }
})
</script>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.header {
  padding: 10px 100px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: $mainBackgroundColor;
  position: sticky;
  top: 0;

  @media (max-width: $breakpoint) {
    padding: 10px;
  }

  &-buttons {
    margin: 0;
    padding: 0;
    list-style: none;
    display: flex;

    &_menu {
      padding: 10px;
      border: 2px $mainColor solid;
      border-radius: 10px;
      transition: background-color 0.3s ease;
      display: none;
      cursor: pointer;

      &-active {
        background-color: $mainBackgroundColorHoverColor;
      }
      &-unactive {
        background-color: $mainBackgroundColor;
      }

      @media (max-width: $breakpoint) {
        display: flex;
      }

      &-icon {
        width: 20px;
        height: 20px;
        display: block;
      }
    }

    &_register {
      margin-right: 70px;

      @media (max-width: $breakpoint) {
        display: none;
      }
    }

    &_login {

      @media (max-width: $breakpoint) {
        display: none;
      }
    }

    &_logout {

      @media (max-width: $breakpoint) {
        display: none;
      }
    }
  }

  &-mobile-menu {
    position: absolute;
    top: 64px;
    right: 10px;
    background-color: $mainBackgroundColor;
    padding: 15px;
    display: flex;
    flex-direction: column;
    z-index: 1000;

    &-link {
      color: $mainColor;
      text-decoration: none;
      padding: 8px 0;
      font-size: 14px;

      &:hover {
        text-decoration: underline;
      }
    }
  }
}

.mobile-menu-enter-active,
.mobile-menu-leave-active {
  transition: all 0.3s ease;
}
.mobile-menu-enter-from {
  opacity: 0;
  transform: translateY(-10px);
}
.mobile-menu-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}

</style>