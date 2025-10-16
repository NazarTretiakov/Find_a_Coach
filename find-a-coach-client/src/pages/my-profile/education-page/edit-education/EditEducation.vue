<template> 
  <div class="layout">
    <profile-sticky-header class="header"></profile-sticky-header>

    <edit-education-form :id="id" class="edit-education-form"></edit-education-form>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditEducationForm from '../../../../components/my-profile/education-page/edit-education/EditEducationForm.vue';
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
    EditEducationForm,
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
.edit-education-form {
  flex: 1;
}
.footer {
  margin-top: auto;
}
</style>