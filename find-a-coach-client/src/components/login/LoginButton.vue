<template>
  <button @click="buttonClick" class="button">Login</button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  setup(props, context) {
    const buttonClick = function(): void {
      context.emit('buttonClick')
    }

    return { buttonClick }
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.button {
  background-color: $mainColor;
  color: $mainBackgroundColor;
  border: none;
  width: 342px;
  height: 40px;
  border-radius: 30px;
  transition: background-color 0.3s ease;
  font-size: 14px;

  &:hover {
    cursor: pointer;
    background-color: $mainColorHoverColor;
  }

  @media (max-width: $breakpoint) {
    height: 36px;
    font-size: 12px;
  }
}
</style>