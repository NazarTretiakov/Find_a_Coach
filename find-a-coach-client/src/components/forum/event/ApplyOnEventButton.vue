<template> 
  <button class="button">
    Apply on event
  </button> 
</template>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.button {
  color: $mainBackgroundColor;
  background-color: $mainColor;
  width: 160px;
  height: 40px;
  border-radius: 12px;
  transition: background-color 0.3s ease;
  font-size: 14px;

  &:hover {
    background-color: $mainColorHoverColor;
  }

  @media (max-width: $breakpoint) {
    width: 130px;
    height: 36px;
    font-size: 12px;
  }
}
</style>