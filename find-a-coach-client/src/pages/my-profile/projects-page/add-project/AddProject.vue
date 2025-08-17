<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <add-project-form class="add-project-form"></add-project-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import AddProjectForm from '../../../../components/my-profile/projects-page/add-project/AddProjectForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    AddProjectForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .add-project-form {
    z-index: 2;
  }
</style>