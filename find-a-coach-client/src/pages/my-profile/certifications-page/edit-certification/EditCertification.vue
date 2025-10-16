<template> 
  <div class="layout">
    <profile-sticky-header class="header"></profile-sticky-header>

    <edit-certification-form :id="id" class="edit-certification-form"></edit-certification-form>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditCertificationForm from '../../../../components/my-profile/certifications-page/edit-certification/EditCertificationForm.vue';
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
    EditCertificationForm,
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