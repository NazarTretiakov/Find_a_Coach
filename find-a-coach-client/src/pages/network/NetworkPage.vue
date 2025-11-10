<template>
  <div class="header">
    <basic-header></basic-header>
    <search-panel @search="handleSearch"></search-panel>
  </div>

  <ul class="profile-sections">
    <li class="profile-sections_left-side">
      <people-cards :search-string="searchString"></people-cards>
    </li>
    <li class="profile-sections_right-side">
      <network-overview></network-overview>
      <the-invitations class="profile-sections_right-side-invitations"></the-invitations>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import BasicHeader from '../../components/BasicHeader.vue'
import SearchPanel from '../../components/SearchPanel.vue'
import PeopleCards from '../../components/network/PeopleCards.vue'
import NetworkOverview from '../../components/network/NetworkOverview.vue'
import TheInvitations from '../../components/network/TheNotifications.vue'

import TheFooter from '../../components/TheFooter.vue'

export default defineComponent({
  components: {
    BasicHeader,
    SearchPanel,
    PeopleCards,
    NetworkOverview,
    TheInvitations,
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
  z-index: 2;
  position: sticky;
  top: 0;
}

.profile-sections {
  display: flex;
  flex-direction: row;
  list-style: none;
  padding: 0;

  @media (max-width: $breakpoint) {
    flex-direction: column;
  }

  &_left-side {
    width: 80%;

    @media (max-width: $breakpoint) {
      width: 100%;
      order: 2;
    }
  }
  &_right-side {

    @media (max-width: $breakpoint) {
      order: 1;
    }

    &-invitations {
      position: sticky;
      top: 126px;
      z-index: 1;
      margin-bottom: 100px;

      @media (max-width: $breakpoint) {
        margin-bottom: 0;
      }
    }
  }
}
</style>