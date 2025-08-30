<template>
  <div class="page">
    <profile-sticky-header></profile-sticky-header>

    <contact-information class="page-content"></contact-information>

    <the-footer class="page-footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../components/my-profile/ProfileStickyHeader.vue'
import ContactInformation from '../../../components/my-profile/contact-information/ContactInformation.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
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
}

.page-footer {
  margin-top: auto;
}
</style>
