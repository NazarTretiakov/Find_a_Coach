<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <ul class="page-sections">
    <li class="page-sections_left-side">
      <the-event></the-event>
    </li>
    <li class="page-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue'
import TheEvent from '../../../../components/my-profile/activities-page/event/TheEvent.vue'

import RecommendedPeople from '../../../../components/my-profile/RecommendedPeople.vue'
import TheFooter from '../../../../components/TheFooter.vue'


export default defineComponent({
  components: {
    ProfileStickyHeader,
    TheEvent,
    RecommendedPeople,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.page-sections {
  display: flex;
  list-style: none;
  padding: 0;

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

    > * {
      position: sticky;
      top: 100px;
      z-index: 1;
      margin-bottom: 100px;
    }
  }
}
</style>