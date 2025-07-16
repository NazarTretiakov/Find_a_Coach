<template>
  <header class="header">
    <full-logo class="header-logo"></full-logo>

    <ul class="header-buttons">
      <li class="header-buttons_menu" :class="{ 'header-buttons_menu-active': isMenuButtonActive, 'header-buttons_menu-unactive': !isMenuButtonActive }" @click="toggleMobileMenu"><img class="header-buttons_menu-icon" src="../assets/images/icons/menu-icon.svg" alt="Menu icon"></li>
      <li class="header-buttons_settings">
        <router-link to="/profile/settings" class="header-buttons_settings-link">
          <div class="header-buttons_settings-container">
            <img class="header-buttons_settings-container-icon" src="../assets/images/icons/settings-icon.svg" alt="Settings icon">
            <span class="header-buttons_settings-container-incription">Settings</span>
          </div>
        </router-link>
      </li>
      <li class="header-buttons_logout">
        <div @click="logout" class="header-buttons_logout-container">
          <img class="header-buttons_logout-container-icon" src="../assets/images/icons/logout-icon.svg" alt="Logout icon">
          <span class="header-buttons_logout-container-incription">Logout</span>
        </div>
      </li>
    </ul>
    <transition name="mobile-menu">
      <div v-if="showMobileMenu" class="header-mobile-menu">
        <router-link to="/profile/settings" class="header-mobile-menu_settings-link">
          <div class="header-mobile-menu_settings-container">
            <img class="header-mobile-menu_settings-container-icon" src="../assets/images/icons/settings-icon.svg" alt="Settings icon">
            <span class="header-mobile-menu_settings-container-incription">Settings</span>
          </div>
        </router-link>
        <div @click="logout" class="header-mobile-menu_logout-container">
          <img class="header-mobile-menu_logout-container-icon" src="../assets/images/icons/logout-icon.svg" alt="Logout icon">
          <span class="header-mobile-menu_logout-container-incription">Logout</span>
        </div>
      </div>
    </transition>
  </header>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, onUnmounted } from 'vue'
import { useAuthenticationStore } from '../stores/authentication'
import { useRouter } from 'vue-router'

import FullLogo from './FullLogo.vue'

export default defineComponent({
  components: { 
    FullLogo,
  },
    setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const BREAKPOINT = 768
    const showMobileMenu = ref<boolean>(false)
    const isMenuButtonActive = ref<boolean>(false)
    const isLoading = ref<boolean>(false)
    let loadingTimeout: ReturnType<typeof setTimeout> | null = null

    const logout = async function() {
      loadingTimeout = setTimeout(() => {
        isLoading.value = true
      }, 1000)

      await authenticationStore.logout()

      clearTimeout(loadingTimeout)
      loadingTimeout = null
      isLoading.value = false

      router.push('/home')
    }

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

    return { showMobileMenu, isMenuButtonActive, toggleMobileMenu, logout }
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

    &_settings, &_logout {
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
          color: $mainColor;
        }
      }
      &-link {
        color: $mainColor;
        text-decoration: none;
      }
      &:hover {
        cursor: pointer;
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

    &_settings, &_logout {

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
          color: $mainColor;
        }
        &:hover {
          cursor: pointer;
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