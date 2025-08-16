<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <add-education-form class="add-education-form"></add-education-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import AddEducationForm from '../../../../components/my-profile/education-page/add-education/AddEducationForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    AddEducationForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .add-education-form {
    z-index: 2;
  }
</style>