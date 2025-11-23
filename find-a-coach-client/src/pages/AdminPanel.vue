<template> 
  <div class="layout">
    <admin-panel-sticky-header></admin-panel-sticky-header>
    <div class="content">
      <h1 class="content-title">Admin panel</h1>
      <manage-users-panel class="content-panel"></manage-users-panel>
      <manage-activities-panel></manage-activities-panel>
    </div>
    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import AdminPanelStickyHeader from '../components/admin-panel/AdminPanelStickyHeader.vue'
import ManageUsersPanel from '@/components/admin-panel/ManageUsersPanel.vue'
import ManageActivitiesPanel from '@/components/admin-panel/ManageActivitiesPanel.vue'
import TheFooter from '../components/TheFooter.vue'

export default defineComponent({
  components: {
    AdminPanelStickyHeader,
    ManageUsersPanel,
    ManageActivitiesPanel,
    TheFooter
  }
})
</script>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.header {
  z-index: 2;
  position: sticky;
  top: 0;
}

.content {
  display: flex;
  flex-direction: column;
  justify-content: center;
  margin: 100px 0 100px 150px;
  flex: 1;
  max-width: 921px;

  @media (max-width: $breakpoint) {
    margin: 60px 10px 100px 10px;
  }

  &-title {
    font-size: 28px;
    margin-bottom: 50px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
      margin-bottom: 30px;
    }
  }
  
  &-panel {
    margin-bottom: 30px;
  }
}

.footer {
  margin-top: auto;
}
</style>