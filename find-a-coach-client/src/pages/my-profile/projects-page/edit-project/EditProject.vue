<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <edit-project-form :id="id" class="edit-project-form"></edit-project-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditProjectForm from '../../../../components/my-profile/projects-page/edit-project/EditProjectForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    ProfileStickyHeader,
    EditProjectForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .edit-project-form {
    z-index: 2;
  }
</style>