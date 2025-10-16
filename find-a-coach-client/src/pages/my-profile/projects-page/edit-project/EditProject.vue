<template> 
  <div class="layout">
    <profile-sticky-header class="header"></profile-sticky-header>

    <edit-project-form :id="id" class="edit-project-form"></edit-project-form>

    <the-footer class="footer"></the-footer>
  </div>
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
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
.header {
  z-index: 1;
}
.edit-certification-form {
  flex: 1;
}
.footer {
  margin-top: auto;
}
</style>