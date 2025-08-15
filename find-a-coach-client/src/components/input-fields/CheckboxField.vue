<template>
  <div class="checkbox-field">
    <label class="checkbox-field-label">
      <input
        class="checkbox-field-element"
        type="checkbox"
        :name="name"
        :id="name"
        :checked="modelValue"
        @change="$emit('update:modelValue', $event.target.checked)"
      >
      <span class="checkbox-field-text">{{ label }}</span>
    </label>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  props: {
    label: { 
      type: String, 
      required: true 
    },
    name: { 
      type: String, 
      required: true 
    },
    modelValue: { 
      type: Boolean, 
      default: false 
    }
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.checkbox-field {
  display: flex;
  align-items: center;

  &-label {
    display: flex;
    align-items: center;
    cursor: pointer;
    font-size: 14px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-element {
    width: 16px;
    height: 16px;
    border: 1px solid $grayBorderColor;
    border-radius: 4px;
    margin-right: 8px;
    cursor: pointer;
    transition: border 0.2s ease;

    &:hover {
      border: 2px solid $grayBorderColor;
    }
  }

  &-text {
    user-select: none;
  }
}
</style>