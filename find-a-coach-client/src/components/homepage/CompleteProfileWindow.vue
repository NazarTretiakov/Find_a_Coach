<template>
  <div class="complete-profile-overlay">
    <div class="complete-profile-window">
      <h2 class="complete-profile-title">{{ title }}</h2>
      <p class="complete-profile-text">Tell us about yourself by completing your profile, so other users can get to know you better and connect or team up with you.</p>
      <span class="complete-profile-link primary" @click.prevent="onCompleteProfile">Complete my profile</span>
      <div class="complete-profile-divider">
        <span>or</span>
      </div>
      <span class="complete-profile-link secondary" @click.prevent="onMaybeLater">Maybe later</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

export default defineComponent({
  props: {
    title: {
      type: String,
      required: true
    }
  },
  emits: ["maybe-later", "complete-profile"],
  setup(_, { emit }) {

    const onMaybeLater = () => {
      emit("maybe-later")
    }

    const onCompleteProfile = () => {
      emit("complete-profile")
    }

    return { onMaybeLater, onCompleteProfile }
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
  margin-bottom: 16px;

  @media (max-width: $breakpoint) {
    font-size: 16px;
  }
}

.complete-profile-text {
  font-size: 14px;
  color: $grayBorderColor;
  line-height: 1.6;
  margin-bottom: 32px;

  @media (max-width: $breakpoint) {
    font-size: 12px;
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