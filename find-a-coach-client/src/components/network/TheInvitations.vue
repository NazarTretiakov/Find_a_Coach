<template>
  <div class="invitations">
    <div class="invitations_header">
      <h1 class="invitations_header-title">Invitations</h1>
      <router-link to="/network/invitations" class="invitations_header-see-all-button">See all</router-link>
    </div>

    <ul class="invitations_list">
      <li class="invitations_list_invitation" v-for="n in 10" :key="n">
        <router-link to="/network/invitations/invitation" class="invitations_list_invitation-items">
          <img class="invitations_list_invitation-items-user-profile-image" src="../../assets/images/icons/user-icon.jpg" alt="User profile image">
          <p class="invitations_list_invitation-items-message"><span class="invitations_list_invitation-items-message-user-name">Grzegorz Kwiatkowski</span> wants to connect with you</p>
        </router-link>
        <div v-if="n != 10" class="invitations_list_invitation-divider" />
        <div v-else class="invitations_list_invitation-divider-last" />
      </li>
    </ul>

    <div class="invitations_footer-wrapper">
      <router-link to="/network/invitations" class="invitations-show-all-link">
        <div class="invitations_show-all">
          <span class="invitations_show-all-element">Show all</span>
        </div>
      </router-link>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.invitations {
  margin: 50px 100px 0 30px;
  border: $grayBorderColor 2px solid;
  border-radius: 20px;
  height: 600px;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  padding: 0;

  @media (max-width: $breakpoint) {
    margin: 20px 10px 30px 10px;
    height: auto;
  }

  &_header {
    display: flex;
    justify-content: space-between;
    align-content: center;
    padding: 30px 50px 0 50px;

    @media (max-width: $breakpoint) {
      padding: 30px;
      height: auto;
    }

    &-title {
      font-size: 20px;
      margin-bottom: 20px;

      @media (max-width: $breakpoint) {
        margin-bottom: 0;
        font-size: 16px;
      }
    }

    &-see-all-button {
      margin-top: 4px;
      font-size: 14px;
      color: $grayBorderColor;
      text-decoration: none;

      @media (max-width: $breakpoint) {
        margin-bottom: 0;
        font-size: 12px;
      }

      &:hover {
        text-decoration: underline;
        cursor: pointer;
      }
    }
  }

  &_list {
    padding: 0 50px;
    list-style: none;
    overflow-y: auto;
    flex-grow: 1;

    @media (max-width: $breakpoint) {
      display: none;
    }

    scrollbar-width: none;
    -ms-overflow-style: none;
    &::-webkit-scrollbar {
      display: none;
    }

    &_invitation {

      &-items {
        display: flex;
        align-items: center;
        color: black;
        text-decoration: none;

        &-user-profile-image {
          width: 40px;
          margin-right: 10px;
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
  }

.invitations_footer-wrapper {
  display: flex;
  justify-content: center;

  @media (max-width: $breakpoint) {
    display: none;
  }

  .invitations-show-all-link {
    display: block;
    width: 100%;
    text-decoration: none;
    color: #000000;

    .invitations_show-all {
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

      .invitations_show-all-element {
        font-size: 14px;
      }
    }
  }
}
}
</style>