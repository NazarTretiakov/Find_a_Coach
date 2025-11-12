<template>
  <div class="network">
    <div class="network-people-cards">
      <h1 v-if="searchString.trim() != ''" class="network-people-cards-header">Search result</h1>
      <h1 v-else class="network-people-cards-header">Recommended connections</h1>
      <ul class="network-people-cards-items">
        <router-link v-for="connection in connections" :key="connection.connectedUserId" :to="`/user-profile/${connection.connectedUserId}`" class="network-people-cards-items_person-link">
          <li class="network-people-cards-items_person">
            <img class="network-people-cards-items_person-image" :src="connection.imagePath" alt="User profile image" />
            <div class="network-people-cards-items_person-info">
              <h3 class="network-people-cards-items_person-info-name">{{ connection.firstName }} {{ connection.lastName }}</h3>
              <p class="network-people-cards-items_person-info-paragraph">{{ connection.headline }}</p>
            </div>
          </li>
        </router-link>
      </ul>
      <div v-if="isLoading" class="notifications-cards-items_loading">
        <v-progress-circular class="notifications-cards-items_loading-spinner-item" indeterminate color="#1b3b80" size="35" width="4"></v-progress-circular>
      </div>
      <div class="notifications-cards-items_load-more" v-if="isMoreConnectionsLeft && !isLoading">
        <span @click="loadConnections" class="notifications-cards-items_load-more-inscription">Load more</span>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, watch, onMounted } from "vue"
import { useRouter } from "vue-router"
import { useAuthenticationStore } from "@/stores/authentication"
import useGetRecommendedUsers from "@/composables/network/useGetRecommendedUsers"
import useGetFilteredUsers from "@/composables/network/useGetFilteredUsers"
import type { Connection } from "@/types/network/Connection"

export default defineComponent({
  props: {
    searchString: {
      type: String,
      required: false,
      default: "",
    },
  },
  setup(props) {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()
    const isLoading = ref(false)
    const connections = ref<Connection[]>([])
    const page = ref(1)
    const pageSize = 7
    const isMoreConnectionsLeft = ref(true)

    async function loadConnections() {
      if (isLoading.value) return
      isLoading.value = true
      const result = await useGetRecommendedUsers(authenticationStore.userId, page.value, pageSize)
      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
        return
      }
      const fetched = result as Connection[]
      if (fetched.length < pageSize) {
        isMoreConnectionsLeft.value = false
      }
      connections.value.push(...fetched)
      page.value++
      isLoading.value = false
    }

    async function loadFilteredConnections() {
      if (isLoading.value) return
      isLoading.value = true
      const result = await useGetFilteredUsers(authenticationStore.userId, props.searchString, page.value, pageSize)
      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
        return
      }
      const fetched = result as Connection[]
      if (fetched.length < pageSize) {
        isMoreConnectionsLeft.value = false
      }
      connections.value.push(...fetched)
      page.value++
      isLoading.value = false
    }

    async function refreshConnections() {
      connections.value = []
      page.value = 1
      isMoreConnectionsLeft.value = true
      if (props.searchString.trim() === "") {
        await loadConnections()
      } else {
        await loadFilteredConnections()
      }
    }

    watch(() => props.searchString, async () => {
      await refreshConnections()
    })

    onMounted(async () => {
      await loadConnections()
    })

    return { isLoading, connections, isMoreConnectionsLeft, loadConnections, loadFilteredConnections }
  },
})
</script>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.network-people-cards {
  margin: 50px 0 30px 100px;
  padding: 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px;
    padding: 30px;
  }

  &-header {
    font-size: 24px;
    margin: 0 0 30px 6px;

    @media (max-width: $breakpoint) {
      font-size: 16px;
    }
  }

  &-items {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 20px;
    list-style: none;
    padding: 0;

    @media (max-width: $breakpoint) {
      grid-template-columns: 1fr;
    }

    &_person {
      height: 150px;
      width: 100%;
      max-width: 400px;
      padding: 20px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;
      display: flex;
      align-items: center;
      background: #fff;

      @media (max-width: $breakpoint) {
        height: 100px;
      }

      &-image {
        width: 50px;
        height: 50px;
        object-fit: cover;
        border-radius: 50%;
        margin-right: 10px;
        border: 1px solid #000000;
      }

      &-info {
        display: flex;
        flex-direction: column;

        &-name {
          font-size: 16px;
          margin: 0 0 6px 0;

          @media (max-width: $breakpoint) {
            font-size: 14px;
          }
        }

        &-paragraph {
          font-size: 14px;
          margin: 0;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }

      &-link {
        color: #000;
        text-decoration: none;
      }
    }
  }
}

.notifications-cards-items_load-more {
  font-size: 14px;
  display: flex;
  justify-content: center;
  align-content: center;
  margin: 40px 0 0 0;

  @media (max-width: $breakpoint) {
    font-size: 12px;
    margin: 30px 0 0 0;
  }

  &-inscription {
    text-decoration: none;
    cursor: pointer;

    &:hover {
      text-decoration: underline;
    }
  }
}

.notifications-cards-items_loading {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 30px;
}
</style>
