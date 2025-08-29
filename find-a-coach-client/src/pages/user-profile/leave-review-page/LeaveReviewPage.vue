<template>
  <div class="page">
    <basic-sticky-header></basic-sticky-header>

    <leave-review-form class="page-content"></leave-review-form>

    <the-footer class="page-footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../../../components/BasicStickyHeader.vue'
import LeaveReviewForm from '../../../components/user-profile/leave-review/LeaveReviewForm.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    BasicStickyHeader,
    LeaveReviewForm,
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
