<template>
  <div class="container">
    <profile-sticky-header class="header"></profile-sticky-header>

    <ul class="settings">
      <li class="settings_left-side">
        <settings-menu></settings-menu>
      </li>
      <li class="settings_right-side">
        <notifications-settings></notifications-settings>
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue'
import SettingsMenu from '../../../../components/my-profile/settings-page/SettingsMenu.vue'
import NotificationsSettings from '../../../../components/my-profile/settings-page/notifications-page/NotificationsSettings.vue'
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    SettingsMenu,
    NotificationsSettings,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.container {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.header {
  z-index: 2;
}

.settings {
  flex: 1;
  display: flex;
  list-style: none;
  padding: 0;

  @media (max-width: $breakpoint) {
    flex-direction: column;
  }

  &_left-side {
    width: 30%;

    @media (max-width: $breakpoint) {
      width: 100%;
    }

    > * {
      position: sticky;
      top: 150px;
      z-index: 1;
      margin-bottom: 100px;

      @media (max-width: $breakpoint) {
        margin-bottom: 0;
      }
    }
  }
}

.footer {
  margin-top: auto;
}
</style>
