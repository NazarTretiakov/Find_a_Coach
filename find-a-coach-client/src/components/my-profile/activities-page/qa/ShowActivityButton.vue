<template> 
  <button class="button">
    Show activity
  </button> 
</template>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.button {
  color: $mainColor;
  background-color: $mainBackgroundColor;
  border: 2px solid $mainColor;
  width: 160px;
  height: 40px;
  border-radius: 12px;
  transition: background-color 0.3s ease;
  font-size: 14px;
  font-weight: bold;

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