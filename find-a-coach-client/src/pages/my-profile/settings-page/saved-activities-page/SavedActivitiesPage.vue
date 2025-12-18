<template> 
  <div class="layout">
    <div class="header">
      <profile-header></profile-header>
    </div>

    <ul class="forum-sections">
      <li class="forum-sections_left-side">
        <saved-activities-cards></saved-activities-cards>
      </li>
      <li class="forum-sections_right-side">
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import ProfileHeader from '@/components/my-profile/ProfileHeader.vue'
import SavedActivitiesCards from '@/components/my-profile/settings-page/saved-activities/SavedActivitiesCards.vue'

import TheFooter from '@/components/TheFooter.vue'


export default defineComponent({
  components: {
    ProfileHeader,
    SavedActivitiesCards,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.header {
  z-index: 2;
  position: sticky;
  top: 0;
}

.forum-sections {
  display: flex;
  list-style: none;
  padding: 0;

  &_left-side {
    width: 80%;
    max-width: 1025px;

    @media (max-width: $breakpoint) {
      width: 100%;
    }
  }
  &_right-side {

    @media (max-width: $breakpoint) {
      display: none;
    }

    > * {
      position: sticky;
      top: 126px;
      z-index: 1;
      margin-bottom: 100px;
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