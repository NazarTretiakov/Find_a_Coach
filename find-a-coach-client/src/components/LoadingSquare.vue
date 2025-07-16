<template>
  <div class="loading-spinner">
    <div class="loading-spinner-content">
      <h2 class="loading-spinner-inscription">Loading...</h2>
      <v-progress-circular indeterminate color="#1b3b80" size="45" width="5"></v-progress-circular>
    </div>
  </div>
</template>

<style scoped lang="scss">
@use '../assets/styles/config' as *;

.loading-spinner {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-color: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;

  &::before {
    content: "";
    width: 300px;
    height: 400px;
    background-color: white;
    border-radius: 20px;
    position: absolute;
    z-index: -1;
  }

  &-content {
    position: relative;
    width: 50px;
    height: 50px;
    margin-top: 24px;
  }

  &-inscription {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -130%);
    font-size: 16px;
    font-weight: bold;
    color: $mainColor;
    text-align: center;
    white-space: nowrap;
    pointer-events: none;
    padding-bottom: 24px;
    font-size: 24px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }
}
</style>