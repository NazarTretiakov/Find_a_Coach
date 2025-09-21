<template>
  <div class="layout">
    <profile-sticky-header class="header" />

    <main class="content">
      <edit-personal-information-form />
    </main>

    <the-footer class="footer" />
  </div>
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
  }
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

.content {
  flex: 1;
}

.footer {
  margin-top: auto;
}
</style>