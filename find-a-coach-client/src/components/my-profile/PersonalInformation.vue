<template>
  <div class="personal-information">
    <ul class="personal-information-items">
      <li class="personal-information-items_left-side">
        <ul class="personal-information-items_left-side-items">
          <li class="personal-information-items_left-side-items_image"><img class="personal-information-items_left-side-items_image-element" src="../../assets/images/icons/user-icon.jpg" alt="User profile image"></li>
          <li class="personal-information-items_left-side-items_name"><h1 class="personal-information-items_left-side-items_name-element">Janusz Kowalski</h1></li>
          <li class="personal-information-items_left-side-items_incription"><span class="personal-information-items_left-side-items_incription-element">Pracownik działu technicznego firmy The Best Storage w Gdańsku</span></li>
          <li class="personal-information-items_left-side-items_location"><span class="personal-information-items_left-side-items_location-element">Gdańsk, Woj. Pomorskie, Polska</span><span class="personal-information-items_left-side-items_location-divider">-</span><router-link to="/profile/contact-information" class="personal-information-items_left-side-items_location-contact-information">Contact information</router-link></li>
          <li class="personal-information-items_left-side-items_connections"><router-link to="/link-to-connections-of-user" class="personal-information-items_left-side-items_connections-link">45 connections</router-link></li>
          <li class="personal-information-items_left-side-items_button"><router-link to="/profile/add-profile-section"><add-profile-section class="personal-information-items_left-side-items_button">Add profile section</add-profile-section></router-link></li>
        </ul>
      </li>
      <li class="personal-information-items_right-side">
        <ul class="personal-information-items_right-side-items">
          <li class="personal-information-items_right-side-items_edit"><router-link to="/profile/edit-personal-information" class="personal-information-items_right-side-items_edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="personal-information-items_right-side-items_edit-icon"></router-link></li>
          <li class="personal-information-items_right-side-items_occupation"><img class="personal-information-items_right-side-items_occupation-icon" src="..//../assets/images/icons/occupation-icon.svg" alt="Occupation icon"><span class="personal-information-items_right-side-items_occupation-incription">Logistics and Marketing</span></li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import AddProfileSection from './AddProfileSection.vue'

export default defineComponent({
  components: {
    AddProfileSection
  }
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