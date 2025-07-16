<template> 
  <basic-sticky-header></basic-sticky-header>
  <div class="content">
    Admin panel
  </div>
  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../components/BasicStickyHeader.vue'  //in admin page should be different header
import TheFooter from '../components/TheFooter.vue'

export default defineComponent({
  components: {
    BasicStickyHeader,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
.content {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 400px;
}
</style>