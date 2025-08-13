<template>
  <header class="header">
    <full-logo class="header-logo"></full-logo>

    <ul class="header-buttons">
      <li class="header-buttons_menu" :class="{ 'header-buttons_menu-active': isMenuButtonActive, 'header-buttons_menu-unactive': !isMenuButtonActive }" @click="toggleMobileMenu"><img class="header-buttons_menu-icon" src="../assets/images/icons/menu-icon.svg" alt="Menu icon"></li>
      <li v-if="!isAuthenticated" class="header-buttons_register"><register-button></register-button></li>
      <li v-if="!isAuthenticated" class="header-buttons_login"><login-button></login-button></li>
      <li v-if="isAuthenticated" class="header-buttons_forum">
        <router-link to="/forum" class="header-buttons_forum-link">
          <div class="header-buttons_forum-container">
            <img class="header-buttons_forum-container-icon" src="../assets/images/icons/forum-icon.svg" alt="Forum icon">
            <span class="header-buttons_forum-container-incription">Forum</span>
          </div>
        </router-link>
      </li>
      <li v-if="isAuthenticated" class="header-buttons_network">
        <router-link to="/network" class="header-buttons_network-link">
          <div class="header-buttons_network-container">
            <img class="header-buttons_network-container-icon" src="../assets/images/icons/network-icon.svg" alt="Network icon">
            <span class="header-buttons_network-container-incription">Network</span>
          </div>
        </router-link>
      </li>
      <li v-if="isAuthenticated" class="header-buttons_profile">
        <router-link to="/my-profile" class="header-buttons_profile-link">
          <div class="header-buttons_profile-container">
            <img class="header-buttons_profile-container-icon" src="../assets/images/icons/profile-icon.svg" alt="Profile icon">
            <span class="header-buttons_profile-container-incription">Profile</span>
          </div>
        </router-link>
      </li>
    </ul>
    <transition name="mobile-menu">
      <div v-if="showMobileMenu" class="header-mobile-menu">
        <router-link to="/register" v-if="!isAuthenticated" class="header-mobile-menu-link">Register</router-link>
        <router-link to="/login" v-if="!isAuthenticated" class="header-mobile-menu-link">Login</router-link>
        <router-link to="/forum" v-if="isAuthenticated" class="header-mobile-menu_forum-link">
          <div class="header-mobile-menu_forum-container">
            <img class="header-mobile-menu_forum-container-icon" src="../assets/images/icons/forum-icon.svg" alt="Forum icon">
            <span class="header-mobile-menu_forum-container-incription">Forum</span>
          </div>
        </router-link>
        <router-link to="/network" v-if="isAuthenticated" class="header-mobile-menu_network-link">
          <div class="header-mobile-menu_network-container">
            <img class="header-mobile-menu_network-container-icon" src="../assets/images/icons/network-icon.svg" alt="Network icon">
            <span class="header-mobile-menu_network-container-incription">Network</span>
          </div>
        </router-link>
        <router-link to="/profile" v-if="isAuthenticated" class="header-mobile-menu_profile-link">
          <div class="header-mobile-menu_profile-container">
            <img class="header-mobile-menu_profile-container-icon" src="../assets/images/icons/profile-icon.svg" alt="Profile icon">
            <span class="header-mobile-menu_profile-container-incription">Profile</span>
          </div>
        </router-link>
      </div>
    </transition>
  </header>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, onUnmounted } from 'vue'
import { useAuthenticationStore } from '../stores/authentication'

import FullLogo from './FullLogo.vue'
import RegisterButton from './RegisterButton.vue'
import LoginButton from './LoginButton.vue'

export default defineComponent({
  components: { 
    FullLogo,
    RegisterButton,
    LoginButton
  },
    setup() {
    const authenticationStore = useAuthenticationStore()
    const isAuthenticated = ref<boolean>(authenticationStore.isAuthenticated)
    const BREAKPOINT = 768
    const showMobileMenu = ref<boolean>(false)
    const isMenuButtonActive = ref<boolean>(false)


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


    onMounted(() => {
      window.addEventListener('resize', handleResize)
    })

    onUnmounted(() => {
      window.removeEventListener('resize', handleResize)
    })

    return { isAuthenticated, showMobileMenu, isMenuButtonActive, toggleMobileMenu  }
  }
})
</script>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.header {
  padding: 10px 100px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: $mainBackgroundColor;

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

    &_forum, &_network, &_profile {
      margin-left: 30px;

      &-container {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;

        &-icon {
          width: 30px;
        }
        &-incription {
          font-size: 14px;
          margin-top: 4px;
        }
      }
      &-link {
        color: $mainColor;
        text-decoration: none;
      }

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

    &_forum, &_network, &_profile {

      &-container {
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;

        &-icon {
          width: 24px;
          margin-top: 14px;
        }
        &-incription {
          font-size: 12px;
          margin-top: 4px;
        }
      }
      &-link {
        color: $mainColor;
        text-decoration: none;
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