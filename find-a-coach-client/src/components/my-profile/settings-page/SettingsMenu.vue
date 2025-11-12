<template>
  <div v-if="!isLoading" class="settings-menu">
    <ul class="settings-menu-items">
      <li class="settings-menu-items_header">
        <img v-if="profileImagePath" :src="profileImagePath" alt="User profile image" class="settings-menu-items_header-user-profile-photo"/>
        <h1 class="settings-menu-items_header-inscription">Settings</h1>
      </li>
      <li class="settings-menu-items_privacy">
        <router-link to="/my-profile/settings/privacy" class="settings-menu-items_privacy-link" >
          <img class="settings-menu-items_privacy-icon" src="../../../assets/images/icons/privacy-icon.svg" alt="Privacy icon" >
          <span :class="activePage == 'privacy' ? 'settings-menu-items_privacy-inscription-active' : 'settings-menu-items_privacy-inscription'">Privacy</span>
        </router-link>
      </li>
      <li class="settings-menu-items_security">
        <router-link to="/my-profile/settings/security" class="settings-menu-items_security-link" >
          <img class="settings-menu-items_security-icon" src="../../../assets/images/icons/security-icon.svg" alt="Security icon">
          <span :class="activePage == 'security' ? 'settings-menu-items_security-inscription-active' : 'settings-menu-items_security-inscription'">Security</span>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import { useRouter } from 'vue-router'
import useGetProfileImagePath from '@/composables/my-profile/settings/useGetProfileImage'

export default defineComponent({
  props: {
    activePage: { 
      type: String, 
      required: true
    }
  },
  setup() {
    const profileImagePath = ref<string>()
    const router = useRouter()
    const isLoading = ref<boolean>(true)

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetProfileImagePath()

      if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        profileImagePath.value = result as string
        const elapsed = performance.now() - startTime
        const remaining = 500 - elapsed
        console.log(result)

        if (remaining > 0) {
          setTimeout(() => {
            isLoading.value = false
          }, remaining)
        } else {
          isLoading.value = false
        }
      }
    })

    return { profileImagePath, isLoading }
  },
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.settings-menu {
  margin: 50px 0 0 100px;

  @media (max-width: $breakpoint) {
    margin: 10px;
  }

  &-items {
    list-style: none;
    padding: 0;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: flex-start;

    @media (max-width: $breakpoint) {
      flex-direction: row;
      justify-content: space-around;
    }

    &_header {
      display: flex;
      align-items: center;
      margin-bottom: 40px;

      @media (max-width: $breakpoint) {
        display: none;
      }

      &-user-profile-photo {
        width: 50px;
        margin-right: 14px;
        border-radius: 50%;
        border: 1px solid #000000;
      }
      &-inscription {
        font-size: 20px;
      }
    }

    &_notifications {
      display: flex;
      align-items: center;
      margin-bottom: 20px;
      
      &-icon {
        width: 30px;
        margin-right: 10px;

        @media (max-width: $breakpoint) {
          width: 24px;
          margin-right: 6px;
        }
      }
      &-inscription {
        font-size: 16px;

        @media (max-width: $breakpoint) {
          font-size: 14px;
        }

        &-active {
          font-weight: bold;
        }
      }
      &-link {
        color: #000000;
        display: flex;
        align-items: center;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }
    }

    &_privacy {
      display: flex;
      align-items: center;
      margin-bottom: 20px;
      
      &-icon {
        width: 30px;
        margin-right: 10px;

        @media (max-width: $breakpoint) {
          width: 24px;
          margin-right: 6px;
        }
      }
      &-inscription {
        font-size: 16px;

        @media (max-width: $breakpoint) {
          font-size: 14px;
        }

        &-active {
          font-weight: bold;
        }
      }
      &-link {
        color: #000000;
        display: flex;
        align-items: center;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }
    }

    &_security {
      display: flex;
      align-items: center;
      margin-bottom: 20px;
      
      &-icon {
        width: 30px;
        margin-right: 10px;

        @media (max-width: $breakpoint) {
          width: 24px;
          margin-right: 6px;
        }
      }
      &-inscription {
        font-size: 16px;

        @media (max-width: $breakpoint) {
          font-size: 14px;
        }

        &-active {
          font-weight: bold;
        }
      }
      &-link {
        color: #000000;
        display: flex;
        align-items: center;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }
    }
  }
}
</style>