<template>
  <button class="button">
    <svg class="button-icon" xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24">
      <path d="M12 5v14m-7-7h14" fill="none" stroke="currentColor" stroke-width="2"/>
    </svg>
    Add {{ addedObjectName }}
  </button>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  props: {
    addedObjectName: {
      type: String,
      required: true
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.button {
  background-color: $mainBackgroundColor;
  color: $mainColor;
  border: $mainColor 1px solid;
  width: 160px;
  height: 40px;
  border-radius: 12px;
  transition: background-color 0.3s ease;
  font-size: 14px;
  display: flex;
  justify-content: center;
  align-items: center;

  &-icon {
    width: 24px;
    margin-right: 4px;
    color: $mainColor;
    margin-bottom: 1px;
  }

  &:hover {
    background-color: $mainBackgroundColorHoverColor;
  }

  @media (max-width: $breakpoint) {
    width: 130px;
    height: 36px;
    font-size: 12px;
  }
}
</style>