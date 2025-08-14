<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <add-activity-form class="add-activity-form"></add-activity-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../components/ProfileStickyHeader.vue';
import AddActivityForm from '../components/add-activity/AddActivityForm.vue';
import TheFooter from '../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    AddActivityForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
  .header {
    z-index: 1;
  }

  .add-activity-form {
    z-index: 2;
  }
</style>