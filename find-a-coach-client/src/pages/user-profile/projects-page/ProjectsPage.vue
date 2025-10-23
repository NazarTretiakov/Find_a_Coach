<template> 
  <basic-sticky-header class="header"></basic-sticky-header>

  <ul class="projects-sections">
    <li class="projects-sections_left-side">
      <projects-cards :id="id"></projects-cards>
    </li>
    <li class="projects-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../../../components/BasicStickyHeader.vue'
import SearchPanel from '../../../components/SearchPanel.vue'
import ProjectsCards from '../../../components/user-profile/projects-page/ProjectsCards.vue'

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
    ProjectsCards,
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

.projects-sections {
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