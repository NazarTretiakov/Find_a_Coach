<template>
  <div class="input-field">
    <label class="input-field-label" :for="name">{{ label }}</label>
    <div class="input-field-wrapper">
      <select
        class="input-field-element"
        :name="name"
        :id="name"
        :value="modelValue"
        @change="$emit('update:modelValue', $event.target.value)"
      >
        <option v-for="(option, index) in options" :key="index" :value="option.value">
          {{ option.label }}
        </option>
      </select>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  props: {
    label: { type: String, required: true },
    name: { type: String, required: true },
    options: {
      type: Array as () => { value: string | number; label: string }[],
      required: true
    },
    modelValue: { type: [String, Number], required: true }
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

  &-wrapper {
    position: relative;
    display: inline-block;
    width: 600px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
      width: 100%;
      height: 30px;
    }

    &::after {
      content: '';
      position: absolute;
      right: 12px;
      top: 50%;
      transform: translateY(-50%);
      width: 0;
      height: 0;
      border-left: 6px solid transparent;
      border-right: 6px solid transparent;
      border-top: 6px solid #000000;
      pointer-events: none;

      @media (max-width: $breakpoint) {
        margin-top: 2px;
      }
    }
  }

  &-element {
    border: 1px #000000 solid;
    width: 100%;
    height: 34px;
    border-radius: 10px;
    transition: border 0.2s ease;
    padding: 0 30px 0 8px;
    font-size: 14px;
    background: white;
    cursor: pointer;
    appearance: none;
    -webkit-appearance: none;
    -moz-appearance: none;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }

    &:hover {
      border: 2px #000000 solid;
    }
  }
}
</style>