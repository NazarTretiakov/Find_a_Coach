<template> 
  <div class="page">
    <basic-sticky-header></basic-sticky-header>

    <ul class="page-sections">
      <li class="page-sections_left-side">
        <the-invitation></the-invitation>
      </li>
      <li class="page-sections_right-side">
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import BasicStickyHeader from '../../../../components/BasicStickyHeader.vue'
import TheInvitation from '../../../../components/network/invitations-page/invitation-page/TheInvitation.vue'
import TheFooter from '../../../../components/TheFooter.vue'


export default defineComponent({
  components: {
    BasicStickyHeader,
    TheInvitation,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.page {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.page-sections {
  display: flex;
  list-style: none;
  padding: 0;
  flex: 1;

  &_left-side {
    width: 80%;

    @media (max-width: $breakpoint) {
      width: 100%;
    }
  }
  &_right-side {

    @media (max-width: $breakpoint) {
      display: none;
    }

    > * {
      position: sticky;
      top: 100px;
      z-index: 1;
      margin-bottom: 100px;
    }
  }
}

.footer {
  margin-top: auto;
}
</style>