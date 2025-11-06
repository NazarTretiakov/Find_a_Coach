<template>
  <loading-square v-if="isLoading" />

  <div v-else class="invitation">
    <ul class="invitation-header">
      <li class="invitation-header_user-info">
        <router-link :to="`/user-profile/${connectionRequest.userId}`" class="invitation-header_user-info-link">
          <img
            class="invitation-header_user-info-profile-image"
            :src="connectionRequest.userImagePath"
            alt="User profile image"
          />
          <span class="invitation-header_user-info-user-name">
            {{ connectionRequest.userFirstName }} {{ connectionRequest.userLastName }}
          </span>
        </router-link>
      </li>

      <li class="invitation-header_publication-time">
        <span class="invitation-header_publication-time-element">
          {{ formatDate(connectionRequest.dateOfCreation) }}
        </span>
      </li>
    </ul>

    <p v-if="connectionRequest.message != ''" class="invitation-description">{{ connectionRequest.message }}</p>
    <p v-else class="invitation-description">Connection request was sent without a message.</p>

    <div v-if="connectionRequest.status == 'Pending'" class="invitation-buttons">
      <button @click="decline" class="invitation-buttons-decline">Decline</button>
      <button @click="accept" class="invitation-buttons-accept">Accept</button>
    </div>
    <div v-else class="invitation-buttons-disabled">
      <button class="invitation-buttons-disabled-decline">Decline</button>
      <button class="invitation-buttons-disabled-accept">Accept</button>
    </div>

    <transition name="smooth-fade">
      <span v-if="successMessage" class="success-message">
        {{ successMessage }}
      </span>
    </transition>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue"

import { useRouter } from "vue-router"
import useGetConnectionRequest from "@/composables/network/useGetConnectionRequest"
import useAcceptConnectionRequest from "@/composables/network/useAcceptConnectionRequest"
import useDeclineConnectionRequest from "@/composables/network/useDeclineConnectionRequest"
import useRelativeDate from "@/composables/forum/useRelativeDate"
import LoadingSquare from "@/components/LoadingSquare.vue"
import type { ConnectionRequest } from "@/types/network/ConnectionRequest"
import type { Result } from "@/types/Result"

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  components: {
    LoadingSquare,
  },
  setup(props) {
    const router = useRouter()

    const connectionRequest = ref<ConnectionRequest>({} as ConnectionRequest)
    const isLoading = ref<boolean>(true)
    const isProcessing = ref<boolean>(false)

    const formatDate = (date: string) => useRelativeDate(date)

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetConnectionRequest(props.id)

      if ("isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
        return
      }

      connectionRequest.value = result as ConnectionRequest

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) {
        setTimeout(() => (isLoading.value = false), remaining)
      } else {
        isLoading.value = false
      }
    })

    const accept = async () => {
      if (isProcessing.value) return
      isProcessing.value = true

      const result: Result = await useAcceptConnectionRequest(props.id)

      if (!result.isSuccessful) {
        console.error(result.errorMessage)
      } else {
        connectionRequest.value.status = "Accepted"
      }

      setTimeout(() => {
        isProcessing.value = false
      }, 3000)
    }

    const decline = async () => {
      if (isProcessing.value) return
      isProcessing.value = true

      const result: Result = await useDeclineConnectionRequest(props.id)

      if (!result.isSuccessful) {
        console.error(result.errorMessage)
      } else {
        connectionRequest.value.status = "Rejected"
      }

      setTimeout(() => {
        isProcessing.value = false
      }, 3000)
    }

    return {
      connectionRequest,
      isLoading,
      isProcessing,
      formatDate,
      accept,
      decline,
    }
  },
})
</script>

<style lang="scss" scoped>
@use "../../../../assets/styles/config" as *;

.invitation {
  margin: 50px 0 100px 100px;
  padding: 0 50px 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;
  max-width: 921px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
    padding: 0 30px 0 30px;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-bottom: 2px solid $grayBorderColor;
    margin: 0 -50px;
    padding: 16px 50px;

    @media (max-width: $breakpoint) {
      margin: 0 -30px;
      padding: 14px 30px;
    }

    &_user-info {
      display: flex;
      align-items: center;

      &-link {
        display: flex;
        align-items: center;
        color: black;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }

      &-profile-image {
        width: 36px;
        margin-right: 10px;
        border-radius: 50%;
        border: 1px solid #000000;

        @media (max-width: $breakpoint) {
          width: 30px;
          margin-right: 8px;
        }
      }
      &-user-name {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }

    &_publication-time {

      &-element {
        font-size: 14px;
        color: $grayBorderColor;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }

  &-description {
    font-size: 14px;
    margin: 30px 0;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-buttons {
    display: flex;
    justify-content: space-between;
    margin-bottom: 50px;

    &-decline {
      border: 2px solid $mainColor;
      color: $mainColor;
      background-color: $mainBackgroundColor;
      width: 160px;
      height: 40px;
      border-radius: 12px;
      transition: background-color 0.3s ease;
      font-size: 14px;

      &:hover {
        background-color: $mainBackgroundColorHoverColor;
      }

      @media (max-width: $breakpoint) {
        width: 130px;
        height: 36px;
        font-size: 12px;
      }
    }

    &-accept {
      color: $mainBackgroundColor;
      background-color: $mainColor;
      width: 160px;
      height: 40px;
      border-radius: 12px;
      transition: background-color 0.3s ease;
      font-size: 14px;

      &:hover {
        background-color: $mainColorHoverColor;
      }

      @media (max-width: $breakpoint) {
        width: 130px;
        height: 36px;
        font-size: 12px;
      }
    }
  }

  &-buttons-disabled {
    display: flex;
    justify-content: space-between;
    margin-bottom: 50px;

    &-decline {
      border: 2px solid $grayBorderColor;
      color: $grayBorderColor;
      background-color: #f5f5f5;
      width: 160px;
      height: 40px;
      border-radius: 12px;
      font-size: 14px;
      cursor: not-allowed;

      @media (max-width: $breakpoint) {
        width: 130px;
        height: 36px;
        font-size: 12px;
      }
    }

    &-accept {
      border: 2px solid $grayBorderColor;
      color: $grayBorderColor;
      background-color: #f5f5f5;
      width: 160px;
      height: 40px;
      border-radius: 12px;
      font-size: 14px;
      cursor: not-allowed;

      @media (max-width: $breakpoint) {
        width: 130px;
        height: 36px;
        font-size: 12px;
      }
    }
  }
}
</style>