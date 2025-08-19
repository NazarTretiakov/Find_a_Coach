<template> 
  <div class="page">
    <profile-sticky-header class="page-header"></profile-sticky-header>

    <add-language-form class="page-add-language-form"></add-language-form>

    <the-footer class="page-footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import AddLanguageForm from '../../../../components/my-profile/languages-page/add-language/AddLanguageForm.vue';
import TheFooter from '../../../../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    AddLanguageForm,
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

  &-add-language-form {
    flex: 1;
  }

  &-footer {
    margin-top: auto;
  }
}
</style>