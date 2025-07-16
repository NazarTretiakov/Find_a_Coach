<template>
  <router-link to="/login" class="button-link">
    <button class="button">Login</button>
  </router-link>
</template>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.button {
  background-color: $mainColor;
  color: $mainBackgroundColor;
  border: none;
  width: 100px;
  height: 40px;
  border-radius: 20px;
  transition: background-color 0.3s ease;
  font-size: 14px;

  &:hover {
    cursor: pointer;
    background-color: $mainColorHoverColor;
  }
}
</style>