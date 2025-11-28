<template>
  <div class="layout">
    <div class="header">
      <basic-header></basic-header>
    </div>

    <ul class="profile-sections">
      <li class="profile-sections_left-side">
        <connections-cards :id="id" :name="name"></connections-cards>
      </li>
      <li class="profile-sections_right-side">
        <recommended-people></recommended-people>
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import BasicHeader from '@/components/BasicHeader.vue'
import ConnectionsCards from '@/components/user-profile/ConnectionsCards.vue'
import RecommendedPeople from '@/components/my-profile/RecommendedPeople.vue'
import TheFooter from '@/components/TheFooter.vue'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    },
    name: {
      type: String,
      required: true
    }
  },
  components: {
    BasicHeader,
    RecommendedPeople,
    ConnectionsCards,
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
@use '@/assets/styles/config' as *;

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
    margin-bottom: 100px;

    @media (max-width: $breakpoint) {
      order: 1;
      display: none;
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

.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
.footer {
  margin-top: auto;
}
</style>