<template> 
  <div class="sticky-header">
    <profile-header class="header"></profile-header>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import ProfileHeader from './ProfileHeader.vue'

export default defineComponent({
  components: {
    ProfileHeader
  }
})
</script>

<style lang='scss' scoped>
@use '../assets/styles/config' as *;

.sticky-header {
  position: sticky;
  top: 0;
}
</style>