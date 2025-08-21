<template>
  <div class="settings-menu">
    <ul class="settings-menu-items">
      <li class="settings-menu-items_header">
        <img class="settings-menu-items_header-user-profile-photo" src="../../../assets/images/icons/user-icon.jpg" alt="User profile image">
        <h1 class="settings-menu-items_header-inscription">Settings</h1>
      </li>
      <li class="settings-menu-items_notifications">
        <router-link to="/my-profile/settings/notifications" class="settings-menu-items_notifications-link">
          <img class="settings-menu-items_notifications-icon" src="../../../assets/images/icons/notifications-icon.svg" alt="Notifications icon" >
          <span :class="activePage == 'notifications' ? 'settings-menu-items_notifications-inscription-active' : 'settings-menu-items_notifications-inscription'">Notifications</span>
        </router-link>  
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
import { defineComponent, ref, computed, watch } from 'vue'

export default defineComponent({
  props: {
    activePage: { 
      type: String, 
      required: true
    }
  }
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