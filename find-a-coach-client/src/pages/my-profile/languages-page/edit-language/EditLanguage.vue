<template> 
  <div class="page">
    <profile-sticky-header class="page-header"></profile-sticky-header>

    <edit-language-form class="page-edit-language-form"></edit-language-form>

    <the-footer class="page-footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditLanguageForm from '../../../../components/my-profile/languages-page/edit-language/EditLanguageForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    EditLanguageForm,
    TheFooter
  },
})
</script>

<style lang="scss" scoped>
.page {
  display: flex;
  flex-direction: column;
  min-height: 100vh;

  &-header {
    z-index: 1;
  }

  &-edit-language-form {
    flex: 1;
  }

  &-footer {
    margin-top: auto;
  }
}
</style>