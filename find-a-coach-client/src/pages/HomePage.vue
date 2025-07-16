<template> 
  <basic-sticky-header></basic-sticky-header>
  <coaches-section></coaches-section>
  <forum-section></forum-section>
  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../components/BasicStickyHeader.vue'
import TheFooter from '../components/TheFooter.vue'
import CoachesSection from '../components/homepage/CoachesSection.vue'
import ForumSection from '../components/homepage/ForumSection.vue'

export default defineComponent({
  components: {
    BasicStickyHeader,
    CoachesSection,
    ForumSection,
    TheFooter
  }
})
</script>
