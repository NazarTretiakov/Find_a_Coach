<template>
  <button class="button">Search</button>
</template>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.button {
  background-color: $mainBackgroundColor;
  color: $mainColor;
  border: 2px solid $mainColor;
  width: 100px;
  height: 40px;
  border-radius: 20px;
  transition: background-color 0.3s ease;
  font-size: 14px;

  @media (max-width: $breakpoint) {
    height: 34px;
    font-size: 12px;
  }

  &:hover {
    cursor: pointer;
    background-color: $mainBackgroundColorHoverColor;
  }
}
</style>