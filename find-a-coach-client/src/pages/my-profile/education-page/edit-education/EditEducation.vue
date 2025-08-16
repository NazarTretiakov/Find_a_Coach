<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <edit-education-form class="edit-education-form"></edit-education-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditEducationForm from '../../../../components/my-profile/education-page/edit-education/EditEducationForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    EditEducationForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .edit-education-form {
    z-index: 2;
  }
</style>