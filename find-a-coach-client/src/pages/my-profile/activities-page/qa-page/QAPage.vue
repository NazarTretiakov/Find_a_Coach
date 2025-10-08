<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <ul class="page-sections">
    <li class="page-sections_left-side">
      <question-answer :id="id"></question-answer>
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
import QuestionAnswer from '../../../../components/my-profile/activities-page/qa/QuestionAnswer.vue'

import RecommendedPeople from '../../../../components/my-profile/RecommendedPeople.vue'
import TheFooter from '../../../../components/TheFooter.vue'


export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    ProfileStickyHeader,
    QuestionAnswer,
    RecommendedPeople,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.header {
  z-index: 2;
  position: sticky;
  top: 0;
}

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