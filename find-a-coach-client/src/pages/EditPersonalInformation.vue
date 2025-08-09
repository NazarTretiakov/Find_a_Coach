<template> 
  <profile-sticky-header></profile-sticky-header>

  <edit-personal-information-form></edit-personal-information-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../components/ProfileStickyHeader.vue';
import EditPersonalInformationForm from '../components/edit-personal-information/EditPersonalInfromationForm.vue';
import TheFooter from '../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    EditPersonalInformationForm,
    TheFooter
  },
})
</script>


<style lang="scss" scoped>
@use '../assets/styles/config' as *;


</style>