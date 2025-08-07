<template>
  <button class="add-profile-section-button">Add profile section</button>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.add-profile-section-button {
  border: 2px solid $mainColor;
  color: $mainColor;
  background-color: $mainBackgroundColor;
  width: 160px;
  height: 40px;
  border-radius: 10px;
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