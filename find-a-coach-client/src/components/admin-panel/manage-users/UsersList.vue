<template>
  <div class="users">
    <ul class="users-list">
      <li class="users-list_user">
        <img class="users-list_user-profile-image" src="@/assets/images/icons/user-icon.jpg" alt="User profile image">
        <span class="users-list_user-email">some_email@gmail.com</span>
        <span class="users-list_user-name">FirstName LastName</span>
        <block-button class="users-list_user-button"></block-button>
      </li>
      <li class="users-list_user">
        <img class="users-list_user-profile-image" src="@/assets/images/icons/user-icon.jpg" alt="User profile image">
        <span class="users-list_user-email">some_email@gmail.com</span>
        <span class="users-list_user-name">FirstName LastName</span>
        <unblock-button class="users-list_user-button"></unblock-button>
      </li>
      <li class="users-list_user">
        <img class="users-list_user-profile-image" src="@/assets/images/icons/user-icon.jpg" alt="User profile image">
        <span class="users-list_user-email">some_email@gmail.com</span>
        <span class="users-list_user-name">FirstName LastName</span>
        <block-button class="users-list_user-button"></block-button>
      </li>
      <li class="users-list_load-more"><span class="users-list_load-more-inscription">Load more</span></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted, watch } from "vue"

import LoadingSquare from '@/components/LoadingSquare.vue'
import BlockButton from "./BlockButton.vue"
import UnblockButton from "./UnblockButton.vue"

export default defineComponent({
  props: {
    searchString: { 
      type: String, 
      required: false, 
      default: '' 
    }
  },
  components: { 
    LoadingSquare,
    BlockButton,
    UnblockButton
  },
  setup(props) {

  }
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