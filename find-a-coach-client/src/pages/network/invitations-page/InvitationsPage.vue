<template>
  <div class="layout">
    <div class="header">
      <basic-header></basic-header>
    </div>

    <ul class="profile-sections">
      <li class="profile-sections_left-side">
        <the-invitations></the-invitations>
      </li>
      <li class="profile-sections_right-side">
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicHeader from '../../../components/BasicHeader.vue'
import TheInvitations from '../../../components/network/invitations-page/TheInvitations.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    BasicHeader,
    TheInvitations,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.header {
  z-index: 1;
  position: sticky;
  top: 0;
}

.profile-sections {
  display: flex;
  flex-direction: row;
  list-style: none;
  padding: 0;
  flex: 1;

  @media (max-width: $breakpoint) {
    flex-direction: column;
  }

  &_left-side {
    width: 80%;

    @media (max-width: $breakpoint) {
      width: 100%;
    }
  }
  &_right-side {

    @media (max-width: $breakpoint) {
      display: none;
    }
  }
}
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
.footer {
  margin-top: auto;
}
</style>