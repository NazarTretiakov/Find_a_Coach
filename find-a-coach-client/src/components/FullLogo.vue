<template>
  <router-link to="/home" class="full-logo-link">
    <ul class="full-logo">
    <li class="full-logo_image"><img class="full-logo_image-element" src="../assets/images/logo.png" alt="Logo of the webpage"></li>
    <li class="full-logo_name"><span class="full-logo_name-element">Find a Coach</span></li>
  </ul>
  </router-link>
</template>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.full-logo {
  margin: 0;
  padding: 0;
  list-style: none;
  display: flex;
  align-items: center;

  &-link {
    text-decoration: none;
    display: inline-block;
  }
  &_image {

    &-element {
      width: 50px;

        @media (max-width: $breakpoint) {
          width: 40px;
        }
    }
  }
  &_name {
    margin-left: 10px;

    &-element {
      font-size: 20px;
      font-weight: bold;
      color: $mainColor;

        @media (max-width: $breakpoint) {
          font-size: 16px;
        }
    }
  }
}
</style>