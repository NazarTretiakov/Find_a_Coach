<template> 
  <div class="header">
    <profile-sticky-header></profile-sticky-header>
  </div>

  <ul class="languages-sections">
    <li class="languages-sections_left-side">
      <languages-cards></languages-cards>
    </li>
    <li class="languages-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../components/my-profile/ProfileStickyHeader.vue'
import LanguagesCards from '../../../components/my-profile/languages-page/LanguagesCards.vue'

import RecommendedPeople from '../../../components/my-profile/RecommendedPeople.vue'
import TheFooter from '../../../components/TheFooter.vue'


export default defineComponent({
  components: {
    ProfileStickyHeader,
    LanguagesCards,
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

.languages-sections {
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