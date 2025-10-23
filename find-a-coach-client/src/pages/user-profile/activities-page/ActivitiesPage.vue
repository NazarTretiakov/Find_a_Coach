<template> 
  <div class="header">
    <basic-header></basic-header>
    <search-panel></search-panel>
  </div>

  <ul class="activities-sections">
    <li class="activities-sections_left-side">
      <activities-cards :id="id"></activities-cards>
    </li>
    <li class="activities-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicHeader from '../../../components/BasicHeader.vue'
import SearchPanel from '../../../components/SearchPanel.vue'
import ActivitiesCards from '../../../components/user-profile/activities-page/ActivitiesCards.vue'

import RecommendedPeople from '../../../components/user-profile/RecommendedPeople.vue'
import TheFooter from '../../../components/TheFooter.vue'


export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    BasicHeader,
    SearchPanel,
    ActivitiesCards,
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

.activities-sections {
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