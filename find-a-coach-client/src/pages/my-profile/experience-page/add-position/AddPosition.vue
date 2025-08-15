<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <add-position-form class="add-position-form"></add-position-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import AddPositionForm from '../../../../components/my-profile/experience-page/add-position/AddPositionForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    AddPositionForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .add-position-form {
    z-index: 2;
  }
</style>