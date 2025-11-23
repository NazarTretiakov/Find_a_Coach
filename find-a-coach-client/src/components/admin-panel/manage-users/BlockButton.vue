<template>
  <button class="button">
    <span class="button-incription">Block</span>
  </button>
</template>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.button {
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
  
  &-incription {
    font-weight: normal;
  }

  &:hover {
    background-color: #ececec;
  }
}
</style>