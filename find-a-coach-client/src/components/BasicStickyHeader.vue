<template> 
  <div class="sticky-header">
    <basic-header class="header"></basic-header>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import BasicHeader from './BasicHeader.vue'

export default defineComponent({
  components: {
    BasicHeader
  }
})
</script>
    BasicHeader

<style lang='scss' scoped>
@use '../assets/styles/config' as *;

.sticky-header {
  position: sticky;
  top: 0;
}
</style>