<template>
  <button class="button">
    <img class="button-icon" src="../../assets/images/icons/remove-icon.svg" alt="">
    Remove
  </button>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.button {
  background-color: $mainBackgroundColor;
  border: #000000 1px solid;
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
    margin-bottom: 1px;
  }

  &:hover {
    background-color: $blackColorHoverColor;
  }

  @media (max-width: $breakpoint) {
    width: 130px;
    height: 36px;
    font-size: 12px;
  }
}
</style>