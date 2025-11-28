<template>
  <div class="notifications">
    <div class="notifications_header">
      <h1 class="notifications_header-title">Notifications</h1>
      <router-link to="/network/notifications" class="notifications_header-see-all-button">See all</router-link>
    </div>

    <ul :class="['notifications_list', { 'notifications_list-loading': isLoading || notifications.length == 0 }]">
      <li class="notifications_list_item" v-for="(notification, index) in notifications" :key="notification.notificationId">
        <router-link :to="relatedObjectLink(notification)" class="notifications_list_item-content">
          <img class="notifications_list_item-content-user-image" :src="notification.relatedUserImagePath" alt="User profile image" />
          <p class="notifications_list_item-content-message">{{ notification.content }}</p>
        </router-link>
        <div v-if="index !== notifications.length - 1" class="notifications_list_item-divider" />
        <div v-else class="notifications_list_item-divider-last" />
      </li>
      <li v-if="isLoading" class="notifications_list_loading">
        <v-progress-circular class="notifications_list_loading-spinner-item" indeterminate color="#1b3b80" size="35" width="4"></v-progress-circular>
      </li>
      <li v-if="notifications.length == 0" class="notifications_list_empty-state">
        <img class="notifications_list_empty-state-icon" src="@/assets/images/icons/empty-state-icon.svg" alt="Empty state icon">
        <span class="notifications_list_empty-state-inscription">You have no notifications.</span>
      </li>
    </ul>

    <div class="notifications_footer-wrapper">
      <router-link to="/network/notifications" class="notifications-show-all-link">
        <div class="notifications_show-all">
          <span class="notifications_show-all-element">Show all</span>
        </div>
      </router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue"
import { useRouter } from "vue-router"
import useGetNotifications from "@/composables/network/useGetNotifications"
import { useAuthenticationStore } from "@/stores/authentication"
import type { Notification } from "@/types/network/Notification"

export default defineComponent({
  setup() {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()

    const notifications = ref<Notification[]>([])
    const page = ref<number>(1)
    const pageSize = 7
    const isLoading = ref<boolean>(false)
    const isMoreNotificationsLeft = ref<boolean>(true)

    const relatedObjectLink = (notification: Notification) => {
      if (notification.type === "ConnectionRequest") {
        return `/network/notifications/connection-request/${notification.notifiedObjectId}`
      } else if (notification.type === "ConnectionRequestAcceptance" || notification.type === "ConnectionRequestRejection") {
        return `/user-profile/${notification.notifiedObjectId}`
      } else if (notification.type === "EventApplication") {
        return `/my-profile/activities/event/${notification.notifiedObjectId}`
      } else if (notification.type === "QAAnswer") {
        return `/my-profile/activities/qa/${notification.notifiedObjectId}/answers`
      } else if (notification.type === "Recommendation") {
        return `/my-profile/recommendations`
      }
    }

    async function loadNotifications() {
      if (isLoading.value) return
      isLoading.value = true

      let result = await useGetNotifications(authenticationStore.userId, page.value, pageSize)

      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
        return
      }

      result = result as Notification[]
      console.log(result)

      if (result.length < pageSize) {
        isMoreNotificationsLeft.value = false
      }

      notifications.value.push(...result)
      page.value++
      isLoading.value = false
    }

    onMounted(() => {
      loadNotifications()
    })

    return {
      notifications,
      relatedObjectLink,
      isMoreNotificationsLeft,
      loadNotifications,
      isLoading,
    }
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.notifications {
  margin: 30px 100px 0 30px;
  border: $grayBorderColor 2px solid;
  border-radius: 20px;
  height: 600px;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  padding: 0;
  width: 366px;

  @media (max-width: $breakpoint) {
    margin: 20px 10px 30px 10px;
    height: auto;
    width: auto;
  }

  &_header {
    display: flex;
    justify-content: space-between;
    align-content: center;
    padding: 30px 50px 0 50px;

    &-title {
      font-size: 20px;
      margin-bottom: 20px;
    }

    &-see-all-button {
      margin-top: 4px;
      font-size: 14px;
      color: $grayBorderColor;
      text-decoration: none;

      &:hover {
        text-decoration: underline;
        cursor: pointer;
      }
    }
  }

  &_list-loading {
    align-content: center;
  }

  &_list {
    padding: 0 50px;
    list-style: none;
    overflow-y: auto;
    flex-grow: 1;

    scrollbar-width: none;
    -ms-overflow-style: none;
    &::-webkit-scrollbar {
      display: none;
    }

    &_item {
      margin-bottom: 20px;

      &-content {
        display: flex;
        align-items: center;
        color: black;
        text-decoration: none;

        &-user-image {
          width: 40px;
          height: 40px;
          border-radius: 50%;
          margin-right: 10px;
          border: 1px solid #000000;
        }

        &-message {
          font-size: 14px;

          &-user-name {
            font-weight: bold;
          }
        }
      }

      &-divider {
        border-bottom: 1px solid $grayBorderColor;
        width: 100%;
        height: 24px;
        margin-bottom: 24px;

        &-last {
          width: 100%;
          height: 30px;
        }
      }
    }

    &_loading {
      display: flex;
      justify-content: center;
      align-content: center;

      @media (max-width: $breakpoint) {
        margin: 40px 0 50px 0;
      }
    }

    &_empty-state {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;

      &-icon {
        width: 50px;
        margin-bottom: 14px;

        @media (max-width: $breakpoint) {
          width: 40px;
          margin-bottom: 10px;
        }
      }
      &-inscription {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          margin-bottom: 30px;
          font-size: 12px;
        }
      }
    }
  }

  &_footer-wrapper {
    display: flex;
    justify-content: center;

    .notifications-show-all-link {
      display: block;
      width: 100%;
      text-decoration: none;
      color: #000000;

      .notifications_show-all {
        width: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        border-top: 2px solid $grayBorderColor;
        padding: 10px 0;
        border-bottom-right-radius: 20px;
        border-bottom-left-radius: 20px;
        transition: background-color 0.3s ease;

        &:hover {
          cursor: pointer;
          background-color: #ececec;
        }

        .notifications_show-all-element {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }
    }
  }
}

</style>