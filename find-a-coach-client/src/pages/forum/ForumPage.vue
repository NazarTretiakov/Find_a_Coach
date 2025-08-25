<template> 
  <div class="header">
    <basic-header></basic-header>
    <search-panel></search-panel>
  </div>

  <ul class="forum-sections">
    <li class="forum-sections_left-side">
      <forum-cards></forum-cards>
    </li>
    <li class="forum-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicHeader from '../../components/BasicHeader.vue'
import SearchPanel from '../../components/SearchPanel.vue'
import forumCards from '../../components/forum/ForumActivities.vue'

import RecommendedPeople from '../../components/my-profile/RecommendedPeople.vue'
import TheFooter from '../../components/TheFooter.vue'


export default defineComponent({
  components: {
    BasicHeader,
    SearchPanel,
    forumCards,
    RecommendedPeople,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

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
</style>