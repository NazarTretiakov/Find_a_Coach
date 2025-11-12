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
            <router-link to="/my-profile/contact-information" class="personal-information-items_left-side-items_location-contact-information">Contact information</router-link>
          </li>
          <li class="personal-information-items_left-side-items_connections">
            <router-link :to="`/network/connections/${userId}`" class="personal-information-items_left-side-items_connections-link">{{ personalInformation.connectionsAmount }} connections</router-link>
          </li>
          <li class="personal-information-items_left-side-items_button">
            <router-link to="/my-profile/add-profile-section">
              <add-profile-section-button class="personal-information-items_left-side-items_button">Add profile section</add-profile-section-button>
            </router-link>
          </li>
        </ul>
      </li>
      <li class="personal-information-items_right-side">
        <ul class="personal-information-items_right-side-items">
          <li class="personal-information-items_right-side-items_edit"><router-link to="/my-profile/edit-personal-information" class="personal-information-items_right-side-items_edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="personal-information-items_right-side-items_edit-icon"></router-link></li>
          <li v-if="personalInformation.primaryOccupation" class="personal-information-items_right-side-items_occupation"><img class="personal-information-items_right-side-items_occupation-icon" src="../../assets/images/icons/occupation-icon.svg" alt="Occupation icon"><span class="personal-information-items_right-side-items_occupation-incription">{{ personalInformation.primaryOccupation }}</span></li>
          <li v-else class="personal-information-items_right-side-items_occupation"><img class="personal-information-items_right-side-items_occupation-icon" src="../../assets/images/icons/occupation-icon.svg" alt="Occupation icon"><span class="personal-information-items_right-side-items_occupation-incription">No occupation provided</span></li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue"
import AddProfileSectionButton from "./AddProfileSectionButton.vue"
import type { PersonalInformation } from "@/types/my-profile/personal-information/PersonalInformation"
import useGetPersonalInformation from "../../composables/my-profile/personal-information/useGetPersonalInformation"
import { useRouter } from "vue-router"
import { useAuthenticationStore } from "@/stores/authentication"

export default defineComponent({
  components: {
    AddProfileSectionButton,
  },
  setup() {
    const personalInformation = ref<PersonalInformation | null>(null)
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const userId = authenticationStore.userId;

    onMounted(async () => {
      const result = await useGetPersonalInformation(authenticationStore.userId)

      if ("isSuccessful" in result) {
        if (!result.isSuccessful) {
          router.push("/error-page")  // TODO: redirect there to "unauthorized" page
        }
      } else {
        personalInformation.value = result
      }
    })

    return { personalInformation, userId }
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
            max-width: 140px;
            max-height: 140px;
            border-radius: 50%;
            object-fit: cover;
            display: block;
            border: 1px solid #000000;

            @media (max-width: $breakpoint) {
              width: 120px;
              height: 120px;
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
        &_button {
          margin-top: 10px;

          @media (max-width: $breakpoint) {
            margin-top: 6px;
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