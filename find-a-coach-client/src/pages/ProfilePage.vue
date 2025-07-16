<template>
  <profile-sticky-header></profile-sticky-header>


  <div class="container">
    <p class="description-of-page">The profile page in which user could see it's profile details, shared posts, created events, etc.</p>
    <button class="logout-button">Logout</button>
  </div>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'


import ProfileStickyHeader from '../components/ProfileStickyHeader.vue'
import TheFooter from '../components/TheFooter.vue'

export default defineComponent({
  components: {
    ProfileStickyHeader, 
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 500px;
  flex-direction: column;
}

.description-of-page {
  margin-bottom: 20px;
}

.logout-button {
  background-color: $mainBackgroundColor;
  color: $mainColor;
  border: 2px solid $mainColor;
  width: 100px;
  height: 40px;
  border-radius: 20px;
  transition: background-color 0.3s ease;
  font-size: 14px;

  &:hover {
    cursor: pointer;
    background-color: $mainBackgroundColorHoverColor;
  }
}
</style>