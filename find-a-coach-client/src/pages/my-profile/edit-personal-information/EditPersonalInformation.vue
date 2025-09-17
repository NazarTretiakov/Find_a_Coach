<template> 
  <profile-sticky-header class="header"></profile-sticky-header>

  <edit-personal-information-form></edit-personal-information-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../components/my-profile/ProfileStickyHeader.vue'
import EditPersonalInformationForm from '../../../components/my-profile/edit-personal-information/EditPersonalInformationForm.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    EditPersonalInformationForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
.header {
  z-index: 1;
}
</style>