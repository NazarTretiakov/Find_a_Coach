<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <add-certification-form class="add-certification-form"></add-certification-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import AddCertificationForm from '../../../../components/my-profile/certifications-page/add-certification/AddCertificationForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    AddCertificationForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .add-certification-form {
    z-index: 2;
  }
</style>