<template> 
  <div class="layout">
    <profile-sticky-header class="header"></profile-sticky-header>

    <ul class="page-sections">
      <li class="page-sections_left-side">
        <q-a-answers :id="id"></q-a-answers>
      </li>
      <li class="page-sections_right-side">
        <recommended-people></recommended-people>
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '@/components/my-profile/ProfileStickyHeader.vue'
import QAAnswers from '@/components/my-profile/activities-page/qa/QAAnswers.vue'

import RecommendedPeople from '@/components/my-profile/RecommendedPeople.vue'
import TheFooter from '@/components/TheFooter.vue'


export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    ProfileStickyHeader,
    QAAnswers,
    RecommendedPeople,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.header {
  z-index: 1;
  position: sticky;
  top: 0;
}

.page-sections {
  display: flex;
  list-style: none;
  padding: 0;
  flex: 1;

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
      z-index: 0;
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