<template>
  <div class="complete-profile-overlay">
    <div class="complete-profile-window">
      <h2 class="complete-profile-title">Would you like to receive a reminder to complete your profile?</h2>

      <span class="complete-profile-link primary" @click.prevent="onWantToReceiveReminder">Yes</span>

      <div class="complete-profile-divider">
        <span>or</span>
      </div>

      <span class="complete-profile-link secondary" @click.prevent="onDonotWantToReceiveReminder">No</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  emits: ["want-to-receive-reminder", "do-not-want-to-receive-reminder"],
  setup(_, { emit }) {

    const onWantToReceiveReminder = () => {
      emit("want-to-receive-reminder")
    }

    const onDonotWantToReceiveReminder = () => {
      emit("do-not-want-to-receive-reminder")
    }

    return { onWantToReceiveReminder, onDonotWantToReceiveReminder }
  },
});
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.complete-profile-overlay {
  position: fixed;
  inset: 0;
  background-color: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.complete-profile-window {
  background: #ffffff;
  border-radius: 20px;
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.15);
  width: 520px;
  max-width: 90%;
  padding: 40px;
  text-align: center;
  animation: fadeInScale 0.25s ease-out;
}

.complete-profile-title {
  font-size: 20px;
  margin-bottom: 32px;

  @media (max-width: $breakpoint) {
    font-size: 16px;
  }
}

.complete-profile-link {
  display: block;
  font-size: 14px;
  margin-bottom: 24px;
  text-decoration: none;
  transition: color 0.2s;
  cursor: pointer;

  @media (max-width: $breakpoint) {
    font-size: 12px;
  }

  &.primary {
    color: $linkColor;
    &:hover {
      text-decoration: underline;
    }
  }

  &.secondary {
    color: $linkColor;
    &:hover {
      text-decoration: underline;
    }
  }
}

.complete-profile-divider {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-bottom: 24px;

  span {
    margin: 0 12px;
    color: $grayBorderColor;
    font-size: 14px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &::before,
  &::after {
    content: "";
    flex: 1;
    height: 1px;
    background: $grayBorderColor;
  }
}

@keyframes fadeInScale {
  from {
    opacity: 0;
    transform: scale(0.95);
  }
  to {
    opacity: 1;
    transform: scale(1);
  }
}
</style>