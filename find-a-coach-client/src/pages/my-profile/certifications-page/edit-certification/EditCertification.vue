<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <edit-certification-form class="edit-certification-form"></edit-certification-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditCertificationForm from '../../../../components/my-profile/certifications-page/edit-certification/EditCertificationForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    EditCertificationForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .edit-certification-form {
    z-index: 2;
  }
</style>