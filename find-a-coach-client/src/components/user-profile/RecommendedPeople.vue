<template>
  <div class="people-section">
    <div class="people-section_header-wrapper">
      <h1 class="people-section_header">People you may know</h1>
    </div>

    <ul class="people-section_people-list">
      <li class="people-section_people-person" v-for="(connection, index) in connections" :key="connection.connectedUserId">
        <ul class="people-section_people-person-items">
          <li class="people-section_people-person-items_image">
            <router-link :to="`/user-profile/${connection.connectedUserId}`" class="people-section_people-person-link">
              <img class="people-section_people-person-items_image-element" :src="connection.imagePath" alt="User profile image" />
            </router-link>
          </li>

          <li class="people-section_people-person-items_info">
            <router-link :to="`/user-profile/${connection.connectedUserId}`" class="people-section_people-person-link">
              <h2 class="people-section_people-person-items_info-name">{{ connection.firstName }} {{ connection.lastName }}</h2>
            </router-link>
            <p class="people-section_people-person-items_info-incription">{{ connection.headline }}</p>
          </li>
        </ul>

        <div v-if="index !== connections.length - 1" class="people-section_people-person-divider" />
        <div v-else class="people-section_people-person-divider-last" />
      </li>
    </ul>

    <div class="people-section_footer-wrapper">
      <router-link to="/network" class="people-section_people-show-all-link">
        <div class="people-section_people-show-all">
          <span class="people-section_people-show-all-element">Show all</span>
        </div>
      </router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, watch } from 'vue'

import { useRouter } from "vue-router"
import { useAuthenticationStore } from "@/stores/authentication"
import type { Connection } from "@/types/network/Connection"
import useGetRecommendedUsers from "@/composables/network/useGetRecommendedUsers"

export default defineComponent({
  setup() {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()
    const isLoading = ref(false)
    const connections = ref<Connection[]>([])

    async function loadConnections() {
      if (isLoading.value) return
      isLoading.value = true
      const result = await useGetRecommendedUsers(authenticationStore.userId, 1, 7)
      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
        return
      }
      const fetched = result as Connection[]
      connections.value.push(...fetched)
      isLoading.value = false
    }

    onMounted(async () => {
      await loadConnections()
    })

    return { isLoading, connections, loadConnections }
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.people-section {
  list-style: none;
  margin: 50px 100px 0 30px;
  border: $grayBorderColor 2px solid;
  border-radius: 20px;
  height: 600px;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  padding: 0;
  width: 363px;

  &_header-wrapper {
    padding: 30px 50px 0 50px;
    flex-shrink: 0;
  }

  &_header {
    font-size: 20px;
    margin-bottom: 20px;
  }

  &_people-list {
    list-style: none;
    padding: 0 50px;
    margin: 0;
    overflow-y: auto;
    flex-grow: 1;

    scrollbar-width: none;
    -ms-overflow-style: none;
    &::-webkit-scrollbar {
      display: none;
    }
  }

  &_people-person {

    &-items {
      list-style: none;
      display: flex;

      &_image {
        &-element {
          width: 40px;
          margin: 14px 10px 0 0;
          border-radius: 50%;
          border: 1px solid #000000;
        }
      }

      &_info {
        &-name {
          font-size: 14px;
          margin-bottom: 4px;
        }

        &-incription {
          font-size: 14px;
          margin-bottom: 8px;
        }
      }
    }

    &-divider {
      border-bottom: 1px solid $grayBorderColor;
      width: 100%;
      height: 20px;
      margin-bottom: 20px;

      &-last {
        width: 100%;
        height: 30px;
      }
    }

    &-link {
      text-decoration: none;
      color: #000000;
    }
  }

  &_footer-wrapper {
    flex-shrink: 0;
  }

  &_people-show-all-link {
    display: block;
    width: 100%;
    text-decoration: none;
    color: #000000;
  }

  &_people-show-all {
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    border-top: 2px solid $grayBorderColor;
    padding: 10px 0;
    transition: background-color 0.3s ease;
    border-bottom-right-radius: 20px;
    border-bottom-left-radius: 20px;

    &:hover {
      cursor: pointer;
      background-color: #ececec;
    }

    &-element {
      font-size: 14px;
    }
  }
}
</style>