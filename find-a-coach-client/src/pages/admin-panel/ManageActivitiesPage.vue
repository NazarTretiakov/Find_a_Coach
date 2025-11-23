<template> 
  <div class="layout">
    <div class="header">
      <admin-panel-sticky-header></admin-panel-sticky-header>
      <search-panel @search="handleSearch"></search-panel>
    </div>

    <ul class="forum-sections">
      <li class="forum-sections_left-side">
        <activities-cards :search-string="searchString"></activities-cards>
      </li>
      <li class="forum-sections_right-side">
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import AdminPanelStickyHeader from '@/components/admin-panel/AdminPanelStickyHeader.vue'
import SearchPanel from '../../components/SearchPanel.vue'
import ActivitiesCards from '@/components/admin-panel/manage-activities/ActivitiesCards.vue'

import TheFooter from '../../components/TheFooter.vue'

export default defineComponent({
  components: {
    AdminPanelStickyHeader,
    SearchPanel,
    ActivitiesCards,
    TheFooter
  },
  setup() {
    const searchString = ref('')

    const handleSearch = (text: string) => {
      searchString.value = text
      console.log(searchString.value)
    }

    return { searchString, handleSearch }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.header {
  z-index: 1;
  position: sticky;
  top: 0;
}

.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
.footer {
  margin-top: auto;
}

.forum-sections {
  display: flex;
  list-style: none;
  padding: 0;

  &_left-side {
    width: 80%;
    max-width: 1000px;

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
      top: 126px;
      z-index: 1;
      margin-bottom: 100px;
    }
  }
}
</style>