<template> 
  <div class="leave-review">
    <ul class="leave-review-items">
      <li class="leave-review-items_header">
        <h1 class="leave-review-items_header-element">Leave review about Grzegorz</h1>
      </li>
      <li class="leave-review-items_content-input">
        <text-input-area label="Your review" name="content" max-number-of-signs="2000"></text-input-area>
      </li>
      <li class="leave-review-items_save-button"><save-button></save-button></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import TextInputArea from '../../input-fields/TextInputArea.vue';
import SaveButton from '../../input-fields/SaveButton.vue';

export default defineComponent({
  components: {
    TextInputArea,
    SaveButton
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.leave-review {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
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
        font-size: 24px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_content-input {
      margin-top: 14px;
    }

    &_save-button {
      margin: 70px 0 100px 0;
    }
  }
}
</style>