<template>
  <div class="page">
    <basic-sticky-header></basic-sticky-header>

    <contact-information :id="id" class="page-content"></contact-information>

    <the-footer class="page-footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../../../components/BasicStickyHeader.vue'
import ContactInformation from '../../../components/user-profile/contact-information/ContactInformation.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    BasicStickyHeader,
    ContactInformation,
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
  min-height: 500px;
}

.page-footer {
  margin-top: auto;
}
</style>
