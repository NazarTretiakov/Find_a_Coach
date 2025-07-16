<template>
  <div class="header">
    <full-logo></full-logo>
  </div>

  <registration-form></registration-form>

  <the-footer></the-footer>
</template>


<script lang="ts">
import { defineComponent } from 'vue'

import FullLogo from '../components/FullLogo.vue'
import TheFooter from '../components/TheFooter.vue'
import RegistrationForm from '../components/registration/RegistrationForm.vue'

export default defineComponent({
  components: { 
    TheFooter,
    FullLogo,
    RegistrationForm
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