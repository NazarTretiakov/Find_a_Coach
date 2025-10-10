<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <edit-position-form :id="id" class="edit-position-form"></edit-position-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditPositionForm from '../../../../components/my-profile/experience-page/edit-position/EditPositionForm.vue';
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
    EditPositionForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .edit-position-form {
    z-index: 2;
  }
</style>