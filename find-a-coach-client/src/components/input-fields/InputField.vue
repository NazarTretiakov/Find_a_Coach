<template>
  <div class="input-field">
    <label class="input-field-label" :for="name">{{ label }}</label>
    <input
      class="input-field-element"
      :name="name"
      :id="name"
      :type="type"
      :value="modelValue"
      @input="$emit('update:modelValue', $event.target.value)"
    >
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  props: {
    label: { type: String, required: true },
    name: { type: String, required: true },
    type: { type: String, required: true },
    modelValue: { type: String, default: '' }
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.input-field {

  &-label {
    color: $grayBorderColor;
    font-size: 14px;
    display: block;
    margin: 0 0 2px 2px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }
  &-element {
    border: 1px #000000 solid;
    width: 600px;
    height: 34px;
    border-radius: 10px;
    transition: border 0.2s ease;
    padding: 0 8px;
    font-size: 14px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
      width: 100%;
      height: 30px;
    }

    &:hover {
      border: 2px #000000 solid;
    }
  }
}
</style>