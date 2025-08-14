<template>
  <div class="input-field">
    <label class="input-field-label" :for="name">{{ label }}</label>
    <div class="input-field-wrapper" @click="triggerFileInput">
      <input 
        ref="fileInput"
        class="input-field-wrapper-element" 
        type="file"
        :name="name"
        :id="name"
        @change="onFileChange"
      >
      <img v-if="!imageSelected" class="input-field-wrapper-icon" src="../../assets/images/icons/input-image-icon.svg" alt="Input image icon">
      <img v-else class="input-field-wrapper-icon" src="../../assets/images/icons/tick-icon.svg" alt="Image selected">
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

export default defineComponent({
  props: {
    label: { type: String, required: true },
    name: { type: String, required: true }
  },
  emits: ['update:modelValue'],
  setup(_, { emit }) {
    const fileInput = ref<HTMLInputElement | null>(null)
    const imageSelected = ref(false)

    const triggerFileInput = () => {
      fileInput.value?.click()
    }

    const onFileChange = (e: Event) => {
      const target = e.target as HTMLInputElement
      const file = target.files ? target.files[0] : null
      imageSelected.value = !!file
      emit('update:modelValue', file)
    }

    return { fileInput, imageSelected, triggerFileInput, onFileChange }
  }
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
    width: 600px;
    height: 70px;
    border: 1px #000000 solid;
    border-radius: 10px;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: border 0.2s ease;
    cursor: pointer;

    @media (max-width: $breakpoint) {
      font-size: 12px;
      width: 100%;
      height: 60px;
    }

    &:hover {
      border: 2px #000000 solid;
    }

    &-element {
      display: none;
    }

    &-icon {
      width: 30px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }
  }
}
</style>
