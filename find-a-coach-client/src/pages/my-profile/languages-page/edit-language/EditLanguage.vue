<template> 
  <div class="layout">
    <profile-sticky-header class="header"></profile-sticky-header>

    <edit-language-form :id="id" class="form"></edit-language-form>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../../../components/my-profile/ProfileStickyHeader.vue';
import EditLanguageForm from '../../../../components/my-profile/languages-page/edit-language/EditLanguageForm.vue';
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
    EditLanguageForm,
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
.form {
  flex: 1;
}
.footer {
  margin-top: auto;
}
</style>