<template>
  <div class="layout">
    <div class="header">
      <basic-header></basic-header>
    </div>

    <ul class="profile-sections">
      <li class="profile-sections_left-side">
        <people-cards :id="id" :search-string="searchString"></people-cards>
      </li>
      <li class="profile-sections_right-side">
        <the-invitations class="profile-sections_right-side-invitations"></the-invitations>
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import BasicHeader from '../../../components/BasicHeader.vue'
import PeopleCards from '../../../components/network/connections-page/PeopleCards.vue'
import NetworkOverview from '../../../components/network/NetworkOverview.vue'
import TheInvitations from '../../../components/network/TheNotifications.vue'

import TheFooter from '../../../components/TheFooter.vue'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    BasicHeader,
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
@use '../../../assets/styles/config' as *;


.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.header {
  z-index: 1;
  position: sticky;
  top: 0;
}

.profile-sections {
  display: flex;
  flex-direction: row;
  list-style: none;
  padding: 0;
  flex: 1;

  @media (max-width: $breakpoint) {
    flex-direction: column;
  }

  &_left-side {
    width: 80%;

    @media (max-width: $breakpoint) {
      width: 100%;
    }
  }
  &_right-side {
    margin-top: 20px;

    @media (max-width: $breakpoint) {
      display: none;
    }

    &-invitations {
      position: sticky;
      top: 126px;
      margin-bottom: 100px;

      @media (max-width: $breakpoint) {
        margin-bottom: 0;
      }
    }
  }
}
.footer {
  margin-top: auto;
}
</style>