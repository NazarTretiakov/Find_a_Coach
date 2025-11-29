<template>
  <router-link :to="{ path: '/network' }" class="button-link">
    <button class="button">Find a Coach</button>
  </router-link>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.button {
  background-color: $secondaryColor;
  color: #FFFFFF;
  border: none;
  width: 160px;
  height: 46px;
  border-radius: 20px;
  transition: background-color 0.3s ease;
  font-size: 14px;


  @media (max-width: $breakpoint) {
    font-size: 12px;
    width: 130px;
    height: 36px;
  }

  &:hover {
    cursor: pointer;
    background-color: $secondaryColorHover;
  }
}
</style>