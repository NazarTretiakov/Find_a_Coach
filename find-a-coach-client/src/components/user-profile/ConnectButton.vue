<template>
  <button class="connect-button">
    <img class="connect-button-icon" src="../../assets/images/icons/connect-button-icon.svg" alt="Connect button icon">
    <span class="connect-button-incription">Connect</span>
  </button>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.connect-button {
  border: 1px solid black;
  color: black;
  background-color: $mainBackgroundColor;
  width: 150px;
  height: 30px;
  border-radius: 10px;
  transition: background-color 0.3s ease;
  font-size: 12px;
  font-weight: bold;
  display: flex;
  justify-content: center;
  align-items: center;

  &-icon {
    margin-right: 6px;
  }
  &-incription {
    font-weight: normal;
  }

  &:hover {
    background-color: #ececec;
  }
}
</style>