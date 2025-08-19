<template> 
  <div class="add-language">
    <ul class="add-language-items">
      <li class="add-language-items_header">
        <h1 class="add-language-items_header-element">Add language</h1>
      </li>
      <li class="add-language-items_language-input">
        <input-field label="Language" name="language" type="text"></input-field>
      </li>
      <li class="add-language-items_proficiency-dropdown">
        <dropdown-menu
          label="Proficiency"
          name="proficiency"
          :options="[
            { value: 'beginner-A1', label: 'Beginner - A1' },
            { value: 'pre-intermediate-A2', label: 'Pre-Intermediate - A2' },
            { value: 'intermediate-B1', label: 'Intermediate - B1' },
            { value: 'upper-intermediate-B2', label: 'Upper-Intermediate - B2' },
            { value: 'advanced-C1', label: 'Advanced - C1' },
            { value: 'mastery-C2', label: 'Mastery - C2' }
          ]"
        />
      </li>
      <li class="add-language-items_save-button"><save-button></save-button></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import InputField from "../../../input-fields/InputField.vue";
import DropdownMenu from '../../../input-fields/DropdownMenu.vue';
import SaveButton from '../../../input-fields/SaveButton.vue';

export default defineComponent({
  components: {
    InputField,
    DropdownMenu,
    SaveButton
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.add-language {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 0 10px;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;
    width: 600px;

    @media (max-width: $breakpoint) {
      width: auto;
    }

    &_header {

      &-element {
        font-size: 28px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_language-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_proficiency-dropdown {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }
    
    &_save-button {
      margin: 70px 0 100px 0;
    }
  }
}
</style>