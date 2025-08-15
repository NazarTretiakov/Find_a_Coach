<template> 
  <div class="header">
    <profile-sticky-header></profile-sticky-header>
    <search-panel></search-panel>
  </div>

  <ul class="experience-sections">
    <li class="experience-sections_left-side">
      <experience-cards></experience-cards>
    </li>
    <li class="experience-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../components/my-profile/ProfileStickyHeader.vue'
import SearchPanel from '../../../components/SearchPanel.vue'
import ExperienceCards from '../../../components/my-profile/experience-page/ExperienceCards.vue'

import RecommendedPeople from '../../../components/my-profile/RecommendedPeople.vue'
import TheFooter from '../../../components/TheFooter.vue'


export default defineComponent({
  components: {
    ProfileStickyHeader,
    SearchPanel,
    ExperienceCards,
    RecommendedPeople,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.header {
  z-index: 2;
  position: sticky;
  top: 0;
}

.experience-sections {
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