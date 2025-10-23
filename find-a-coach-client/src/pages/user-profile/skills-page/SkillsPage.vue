<template> 
  <basic-sticky-header class="header"></basic-sticky-header>

  <ul class="skills-sections">
    <li class="skills-sections_left-side">
      <skills-cards :id="id"></skills-cards>
    </li>
    <li class="skills-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../../../components/BasicStickyHeader.vue'
import SearchPanel from '../../../components/SearchPanel.vue'
import SkillsCards from '../../../components/user-profile/skills-page/SkillsCards.vue'

import RecommendedPeople from '../../../components/user-profile/RecommendedPeople.vue'
import TheFooter from '../../../components/TheFooter.vue'


export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  components: {
    BasicStickyHeader,
    SearchPanel,
    SkillsCards,
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

.skills-sections {
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