<template> 
  <div class="layout">
    <div class="header">
      <profile-sticky-header></profile-sticky-header>
    </div>

    <ul class="recommendations-sections">
      <li class="recommendations-sections_left-side">
        <recommendations-cards></recommendations-cards>
      </li>
      <li class="recommendations-sections_right-side">
        <recommended-people></recommended-people>
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../components/my-profile/ProfileStickyHeader.vue'
import RecommendationsCards from '../../../components/my-profile/recommendations-page/RecommendationsCards.vue'

import RecommendedPeople from '../../../components/my-profile/RecommendedPeople.vue'
import TheFooter from '../../../components/TheFooter.vue'


export default defineComponent({
  components: {
    ProfileStickyHeader,
    RecommendationsCards,
    RecommendedPeople,
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

.recommendations-sections {
  z-index: 0;
  flex: 1;
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
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
.footer {
  margin-top: auto;
}
</style>