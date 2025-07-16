<template> 
  <full-logo class="header"></full-logo>

  <reset-password-form></reset-password-form>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import FullLogo from '../components/FullLogo.vue'
import ResetPasswordForm from '../components/reset-password/ResetPasswordForm.vue'
import TheFooter from '../components/TheFooter.vue'

export default defineComponent({
  components: { 
    FullLogo,
    TheFooter,
    ResetPasswordForm 
  }
})
</script>


<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.header {
  padding: 10px 100px;

  @media (max-width: $breakpoint) {
    padding: 10px;
  }
}
</style>