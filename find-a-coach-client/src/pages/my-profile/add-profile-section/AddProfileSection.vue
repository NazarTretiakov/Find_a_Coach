<template> 
  <profile-sticky-header></profile-sticky-header>

  <add-profile-section-panels></add-profile-section-panels>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../components/my-profile/ProfileStickyHeader.vue'
import AddProfileSectionPanels from '../../../components/my-profile/add-profile-section/AddProfileSectionPanels.vue'
import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    AddProfileSectionPanels,
    TheFooter
  },
})
</script>