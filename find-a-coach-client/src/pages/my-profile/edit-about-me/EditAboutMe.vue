<template>
  <div class="page">
    <profile-sticky-header></profile-sticky-header>

    <edit-about-me-form class="page-content"></edit-about-me-form>

    <the-footer class="page-footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../components/my-profile/ProfileStickyHeader.vue'
import EditAboutMeForm from '../../../components/my-profile/edit-about-me/EditAboutMeForm.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    EditAboutMeForm,
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
