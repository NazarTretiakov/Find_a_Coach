<template>
  <div class="personal-information" v-if="personalInformation">
    <ul class="personal-information-items">
      <li class="personal-information-items_left-side">
        <ul class="personal-information-items_left-side-items">
          <li class="personal-information-items_left-side-items_image">
            <img class="personal-information-items_left-side-items_image-element" :src="personalInformation.profileImageUrl" alt="User profile image" />
          </li>
          <li class="personal-information-items_left-side-items_name">
            <h1 v-if="personalInformation.firstName || personalInformation.lastName" class="personal-information-items_left-side-items_name-element"> {{ personalInformation.firstName }} {{ personalInformation.lastName }}</h1>
            <h1 v-else class="personal-information-items_left-side-items_name-element">Unnamed user</h1>
          </li>
          <li class="personal-information-items_left-side-items_incription">
            <span v-if="personalInformation.headline" class="personal-information-items_left-side-items_incription-element"> {{ personalInformation.headline }}</span>
            <span v-else class="personal-information-items_left-side-items_incription-element">No inscription provided</span>
          </li>
          <li class="personal-information-items_left-side-items_location">
            <span v-if="personalInformation.location" class="personal-information-items_left-side-items_location-element">{{ personalInformation.location }}</span>
            <span v-if="personalInformation.location" class="personal-information-items_left-side-items_location-divider">-</span>
            <router-link :to="`/user-profile/${id}/contact-information`" class="personal-information-items_left-side-items_location-contact-information">Contact information</router-link>
          </li>
          <li class="personal-information-items_left-side-items_connections">
            <router-link to="/network/connections" class="personal-information-items_left-side-items_connections-link">{{ personalInformation.connectionsAmount }} connections</router-link>
          </li>
          <li class="personal-information-items_left-side-items_buttons">
            <router-link v-if="!personalInformation.isConnected && personalInformation.connectionStatus == null" :to="{ path: '/user-profile/send-connection-request', query: { id: id, name: personalInformation.firstName }}" class="personal-information-items_left-side-items_buttons-leave-review-link">
              <button class="personal-information-items_left-side-items_buttons-connect">
                <svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#ffffff">
                  <path d="M720-400v-120H600v-80h120v-120h80v120h120v80H800v120h-80Zm-360-80q-66 0-113-47t-47-113q0-66 47-113t113-47q66 0 113 47t47 113q0 66-47 113t-113 47ZM40-160v-112q0-34 17.5-62.5T104-378q62-31 126-46.5T360-440q66 0 130 15.5T616-378q29 15 46.5 43.5T680-272v112H40Zm80-80h480v-32q0-11-5.5-20T580-306q-54-27-109-40.5T360-360q-56 0-111 13.5T140-306q-9 5-14.5 14t-5.5 20v32Zm240-320q33 0 56.5-23.5T440-640q0-33-23.5-56.5T360-720q-33 0-56.5 23.5T280-640q0 33 23.5 56.5T360-560Zm0-80Zm0 400Z"/>
                </svg>
                <span class="personal-information-items_left-side-items_buttons-connect-inscription">Connect</span>
              </button>
            </router-link>
            <button v-else-if="personalInformation.connectionStatus === 'Pending'" class="personal-information-items_left-side-items_buttons-pending">
              <img src="@/assets/images/icons/pending-icon.svg" alt="Pending connection" class="personal-information-items_left-side-items_buttons-pending-icon">
              <span class="personal-information-items_left-side-items_buttons-pending-inscription">Pending</span>
            </button>
            <button @click="removeConnection" v-else-if="personalInformation.connectionStatus === 'Accepted'" class="personal-information-items_left-side-items_buttons-remove-connection">
              <span class="personal-information-items_left-side-items_buttons-pending-inscription">Remove connection</span>
            </button>
            <router-link :to="{ path: '/user-profile/leave-recommendation', query: { id: id, name: personalInformation.firstName }}" class="personal-information-items_left-side-items_buttons-leave-review-link">
              <button class="personal-information-items_left-side-items_buttons-leave-review">
                <span class="personal-information-items_left-side-items_buttons-leave-review-inscription">Leave review</span>
              </button>
            </router-link>
          </li>
        </ul>
      </li>
      <li class="personal-information-items_right-side">
        <ul class="personal-information-items_right-side-items">
          <li class="personal-information-items_right-side-items_edit"></li>
          <li class="personal-information-items_right-side-items_occupation"><img class="personal-information-items_right-side-items_occupation-icon" src="../../assets/images/icons/occupation-icon.svg" alt="Occupation icon"><span class="personal-information-items_right-side-items_occupation-incription">{{ personalInformation.primaryOccupation }}</span></li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue"
import type { PersonalInformation } from "@/types/my-profile/personal-information/PersonalInformation"
import useGetPersonalInformation from "../../composables/my-profile/personal-information/useGetPersonalInformation"
import useRemoveConnection from "../../composables/network/useRemoveConnection"
import { useAuthenticationStore } from "@/stores/authentication"
import { useRouter } from "vue-router"

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const personalInformation = ref<PersonalInformation | null>(null)
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()

    onMounted(async () => {
      const result = await useGetPersonalInformation(props.id)

      if ("isSuccessful" in result) {
        if (!result.isSuccessful) {
          router.push("/error-page")  // TODO: redirect there to "unauthorized" page
        }
      } else {
        personalInformation.value = result
      }
    })

    const removeConnection = async () => {
      const isSuccessful = await useRemoveConnection(authenticationStore.userId, props.id)

      if (isSuccessful) {
        if (personalInformation.value) {
          personalInformation.value.connectionStatus = null
          personalInformation.value.isConnected = false
        }
      }
    }

    return { personalInformation, removeConnection }
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.personal-information {
  margin: 50px 0 30px 100px;
  padding: 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px;
    padding: 0;
    border: none;
  }

  &-items {
    display: flex;
    list-style: none;
    padding: 0;
    justify-content: space-between;

    &_left-side {
      &-items {
        display: flex;
        list-style: none;
        padding: 0;
        flex-direction: column;

        &_image {
          &-element {
            width: 140px;
            border-radius: 50%;
            border: 1px solid #000000;

            @media (max-width: $breakpoint) {
              width: 120px;
            }
          }
        }
        &_name {
          &-element {
            font-size: 28px;

            @media (max-width: $breakpoint) {
              font-size: 24px;
            }
          }
        }
        &_incription {
          &-element {
            font-size: 14px;
            display: block;
            margin-top: 10px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
              margin-top: 8px;
            }
          }
        }
        &_location {
          font-size: 14px;
          margin-top: 4px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
            margin-top: 2px;
          }

          &-element {
            margin-right: 6px;
            color: $grayBorderColor;
          }
          &-divider {
            margin-right: 6px;
            color: $grayBorderColor;
          }
          &-contact-information {
            color: $linkColor;
            text-decoration: none;

            &:hover {
              text-decoration: underline;
            }
          }
        }
        &_connections {
          font-size: 14px;
          margin-top: 4px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
            margin-top: 2px;
          }

          &-link {
            color: $linkColor;
            text-decoration: none;

            &:hover {
              text-decoration: underline;
            }
          }
        }
        &_buttons {
          margin-top: 10px;
          display: flex;
          justify-content: space-between;
          width: 400px;

          @media (max-width: $breakpoint) {
            margin-top: 6px;
            width: 300px;
          }

          &-connect {
            color: $mainBackgroundColor;
            background-color: $mainColor;
            width: 160px;
            height: 40px;
            border-radius: 12px;
            transition: background-color 0.3s ease;
            font-size: 14px;
            display: flex;
            justify-content: center;
            align-items: center;

            @media (max-width: $breakpoint) {
              width: 130px;
              height: 36px;
              font-size: 12px;
            }

            &-inscription {
              margin-left: 8px;
            }

            &:hover {
              cursor: pointer;
              background-color: $mainColorHoverColor;
            }
          }

          &-pending {
            color: $mainColor;
            background-color: $mainBackgroundColor;
            width: 160px;
            height: 40px;
            border-radius: 12px;
            border: 2px solid $mainColor;
            transition: background-color 0.3s ease;
            font-size: 14px;
            display: flex;
            justify-content: center;
            align-items: center;
            cursor: default;

            @media (max-width: $breakpoint) {
              width: 130px;
              height: 36px;
              font-size: 12px;
            }

            &-inscription {
              margin-left: 8px;
            }
          }

          &-remove-connection {
            color: $mainColor;
            background-color: $mainBackgroundColor;
            width: 160px;
            height: 40px;
            border-radius: 12px;
            border: 2px solid $mainColor;
            transition: background-color 0.3s ease;
            font-size: 14px;
            display: flex;
            justify-content: center;
            align-items: center;

            @media (max-width: $breakpoint) {
              width: 130px;
              height: 36px;
              font-size: 12px;
            }

            &-inscription {
              margin-left: 8px;
            }

            &:hover {
              cursor: pointer;
              background-color: $mainBackgroundColorHoverColor;
            }
          }

          &-leave-review {
            border: 2px solid $mainColor;
            color: $mainColor;
            background-color: $mainBackgroundColor;
            width: 160px;
            height: 40px;
            border-radius: 12px;
            transition: background-color 0.3s ease;
            font-size: 14px;
            display: flex;
            justify-content: center;
            align-items: center;

            @media (max-width: $breakpoint) {
              width: 130px;
              height: 36px;
              font-size: 12px;
            }

            &-inscription {
              margin-left: 8px;
            }

            &:hover {
              cursor: pointer;
              background-color: $mainBackgroundColorHoverColor;
            }
            &-link {
              text-decoration: none;
            }
          }
        }
      }
    }

    &_right-side {
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-end;
      flex-grow: 1;

      &-items {
        display: flex;
        list-style: none;
        flex-direction: column;
        justify-content: space-between;
        align-items: flex-end;
        height: 60%;

        &_edit {
          &-icon {
            width: 30px;
            transition: transform 0.3s ease;

            &:hover {
              transform: scale(1.15);
            }

            @media (max-width: $breakpoint) {
              width: 24px;
            }
          }
        }
        &_occupation {
          display: flex;
          align-items: center;

          @media (max-width: $breakpoint) {
            display: none;
          }
          
          &-icon {
            width: 40px;
            margin-right: 14px;
          }
          &-incription {
            font-size: 14px;
          }
        }
      }
    }
  }
}
</style>