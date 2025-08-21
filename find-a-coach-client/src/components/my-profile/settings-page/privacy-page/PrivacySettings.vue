<template>
  <div class="privacy-settings">
    <h1 class="privacy-settings_header">Privacy</h1>
    <dropdown-menu 
      class="privacy-settings_contact-information"
      label="Who can see your contact information" 
      name="contact-information-privacy" 
      :options="[
        { value: 'everyone', label: 'Everyone' },
        { value: 'network', label: 'Only your network' },
        { value: 'no-one', label: 'No one' }
      ]"
    ></dropdown-menu>
    <save-button></save-button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import DropdownMenu from '../../../input-fields/DropdownMenu.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'

export default defineComponent({
  components: {
    DropdownMenu,
    SaveButton
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.privacy-settings {
  margin: 50px 0 100px 0;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 100px 10px;
  }

  &_header {
    font-size: 28px;
    margin-bottom: 50px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }

  &_contact-information {
    margin-bottom: 70px;
  }
}

</style>