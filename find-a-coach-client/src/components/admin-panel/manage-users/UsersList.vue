<template>
  <div class="users">
    <ul class="users-list">
      <li v-for="user in users" :key="user.userId" class="users-list_user">
        <img class="users-list_user-profile-image" :src="user.imagePath || '@/assets/images/icons/user-icon.jpg'" alt="User profile image">
        <span class="users-list_user-email">{{ user.email }}</span>
        <span class="users-list_user-name">{{ user.firstName }} {{ user.lastName }}</span>
        <block-button v-if="!user.isBlocked" class="users-list_user-button" @click.stop="toggleBlock(user)" />
        <unblock-button v-else class="users-list_user-button" @click.stop="toggleBlock(user)" />
      </li>

      <li v-if="isLoading" class="users-list_loading"><loading-square /></li>

      <li v-if="isMoreUsersLeft && !isLoading" class="users-list_load-more" @click="loadUsers">
        <span class="users-list_load-more-inscription">Load more</span>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, watch } from "vue"

import LoadingSquare from '@/components/LoadingSquare.vue'
import BlockButton from "./BlockButton.vue"
import UnblockButton from "./UnblockButton.vue"

import useGetAllUsers from "@/composables/admin-panel/useGetAllUsers"
import useGetFilteredUsers from "@/composables/admin-panel/useGetFilteredUsers"
import useToggleBlockOfUser from '@/composables/admin-panel/useToggleBlockOfUser'
import type { User } from "@/types/admin-panel/User"

export default defineComponent({
  props: {
    searchString: {
      type: String,
      required: false,
      default: "",
    },
  },
  components: {
    LoadingSquare,
    BlockButton,
    UnblockButton,
  },

  setup(props) {
    const users = ref<User[]>([])
    const page = ref(1)
    const pageSize = 10
    const isMoreUsersLeft = ref(true)
    const isLoading = ref(false)

    async function loadUsers() {
      if (isLoading.value) return
      isLoading.value = true

      const result = await useGetAllUsers(page.value, pageSize)
      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        isLoading.value = false
        return
      }

      const fetched = result as User[]
      if (fetched.length < pageSize) {
        isMoreUsersLeft.value = false
      }

      users.value.push(...fetched)
      page.value++
      isLoading.value = false
    }

    async function loadFilteredUsers() {
      if (isLoading.value) return
      isLoading.value = true

      const result = await useGetFilteredUsers(props.searchString, page.value, pageSize)
      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        isLoading.value = false
        return
      }

      const fetched = result as User[]
      if (fetched.length < pageSize) {
        isMoreUsersLeft.value = false
      }

      users.value.push(...fetched)
      page.value++
      isLoading.value = false
    }

    async function refreshUsers() {
      users.value = []
      page.value = 1
      isMoreUsersLeft.value = true

      if (props.searchString.trim() === "") await loadUsers()
      else await loadFilteredUsers()
    }

    watch(() => props.searchString, async () => {
      await refreshUsers()
    })

    onMounted(async () => {
      await loadUsers()
    })

    async function toggleBlock(user: User) {
      const result = await useToggleBlockOfUser(user.userId)
      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }
      user.isBlocked = result as boolean
    }

    return {
      users,
      isLoading,
      isMoreUsersLeft,
      loadUsers,
      loadFilteredUsers,
      toggleBlock,
    }
  },
})
</script>


<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.users {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
  }

  &-list {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_user {
      font-size: 14px;
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 30px 0;
      border-bottom: 2px solid $grayBorderColor;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }

      &-profile-image {
        width: 40px;
        border-radius: 50%;
        border: 1px solid #000000;
        
        @media (max-width: $breakpoint) {
          width: 30px;
        }
      }
      &-email {
        width: 250px;
      }
      &-name {
        width: 250px;      
        
        @media (max-width: $breakpoint) {
          display: none;
        }
      }
      &-button {
        display: inline;
      }
    }

    &_load-more {
      display: flex;
      justify-self: center;
      align-self: center;

      &-inscription {
        font-size: 14px;
        margin-top: 20px;

        &:hover {
          cursor: pointer;
          text-decoration: underline;
        }

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }
}
</style>