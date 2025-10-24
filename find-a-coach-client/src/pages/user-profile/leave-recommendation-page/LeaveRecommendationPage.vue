<template>
  <div class="page">
    <basic-sticky-header></basic-sticky-header>

    <leave-recommendation-form :id="id" :name="name" class="page-content"></leave-recommendation-form>

    <the-footer class="page-footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../../../components/BasicStickyHeader.vue'
import LeaveRecommendationForm from '../../../components/user-profile/leave-review/LeaveRecommendationForm.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
    name: {
      type: String,
      required: true
    }
  },
  components: {
    BasicStickyHeader,
    LeaveRecommendationForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
.page {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.page-content {
  flex: 1;
}

.page-footer {
  margin-top: auto;
}
</style>
