<template>
  <div class="notifications">
    <div class="notifications-cards">
      <h1 class="notifications-cards-header">Notifications</h1>

      <ul class="notifications-cards-items">
        <div v-for="(notification, index) in notifications" :key="notification.notificationId">
          <router-link :to="relatedObjectLink(notification)" class="notifications-cards-items_notification-link">
            <li class="notifications-cards-items_notification">
              <div class="notifications-cards-items_notification-left-side">
                <img class="notifications-cards-items_notification-left-side-image" :src="notification.relatedUserImagePath" alt="User profile image" />
                <span class="notifications-cards-items_notification-left-side-description">
                  {{ notification.content }}
                </span>
              </div>
              <img class="notifications-cards-items_notification-arrow-forward-icon" src="../../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon" />
            </li>
          </router-link>
          <div v-if="index !== notifications.length - 1 || isMoreNotificationsLeft" class="notifications-cards-items_divider" />
        </div>
        <div class="notifications-cards-items_load-more" v-if="isMoreNotificationsLeft && !isLoading">
          <span @click="loadNotifications" class="notifications-cards-items_load-more-inscription">Load more</span>
        </div>
        <li v-if="isLoading" class="notifications-cards-items_loading">
          <v-progress-circular class="notifications-cards-items_loading-spinner-item" indeterminate color="#1b3b80" size="35" width="4"></v-progress-circular>
        </li>
        <li v-if="notifications.length == 0" class="notifications-cards-items_empty-state">
          <img class="notifications-cards-items_empty-state-icon" src="@/assets/images/icons/empty-state-icon.svg" alt="Empty state icon">
          <span class="notifications-cards-items_empty-state-inscription">You have no notifications.</span>
        </li>
      </ul>
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
      loadNotifications,
      isLoading,
      isMoreNotificationsLeft,
    }
  },
})
</script>


<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.notifications {
  &-cards {
    margin: 50px 0 100px 100px;
    padding: 50px 50px 30px 50px;
    border: 2px $grayBorderColor solid;
    border-radius: 20px;
    max-width: 921px;

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
      list-style: none;
      padding: 0;
      margin: 0;

      &_notification-link {
        color: #000;
        text-decoration: none;

        &:hover {
          .notifications-cards-items_notification-arrow-forward-icon {
            transform: scale(1.2);
          }
        }

        .notifications-cards-items_notification {
          display: flex;
          justify-content: space-between;
          align-items: center;
          padding: 10px 0;

          &-left-side {
            display: flex;
            align-items: center;

            &-image {
              width: 40px;
              margin-right: 30px;
              border-radius: 50%;
              border: 1px solid #000;
            }

            &-description {
              font-size: 14px;

              @media (max-width: $breakpoint) {
                font-size: 12px;
              }

              &-invitator-name {
                font-weight: bold;
              }
            }
          }

          &-arrow-forward-icon {
            width: 30px;
            transition: transform 0.3s ease;

              @media (max-width: $breakpoint) {
                width: 24px;
              }
          }
        }
      }

      &_divider {
        border-bottom: 1px solid $grayBorderColor;
        width: 100%;
        height: 24px;
        margin-bottom: 24px;

        &-last {
          border: none;
          height: 10px;
        }
      }

      &_loading {
        display: flex;
        justify-content: center;
        align-items: center;
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
          margin-bottom: 30px;

          @media (max-width: $breakpoint) {
            margin-bottom: 20px;
            font-size: 12px;
          }
        }
      }

      &_load-more {
        font-size: 14px;
        display: flex;
        justify-content: center;
        align-content: center;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }

        &-inscription {
          text-decoration: none;
          cursor: pointer;

          &:hover {
            text-decoration: underline;
          }
        }
      }
    }
  }

  .network-load-more {
    margin: 0 0 100px 100px;
    display: flex;
    justify-content: center;
    align-items: center;
    border: 2px $grayBorderColor solid;
    border-radius: 20px;
    height: 50px;
    transition: background-color 0.3s ease;
    max-width: 921px;

    @media (max-width: $breakpoint) {
      margin: 0 10px 100px 10px;
    }

    &:hover {
      cursor: pointer;
      background-color: #ececec;
    }

    &-link {
      color: #000;
      text-decoration: none;
    }

    &-inscription {
      font-size: 14px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
  }
}
</style>
