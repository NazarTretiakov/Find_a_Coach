<template>
  <div class="invitations">
    <div class="invitations-cards">
      <h1 class="invitations-cards-header">Invitations</h1>
      <ul class="invitations-cards-items">
        <div v-for="n in 20" :key="n">
          <router-link to="/network/invitations/invitation" class="invitations-cards-items_invitation-link">
            <li class="invitations-cards-items_invitation">
              <div class="invitations-cards-items_invitation-left-side">
                <img class="invitations-cards-items_invitation-left-side-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile image">
                <span class="invitations-cards-items_invitation-left-side-description"><span class="invitations-cards-items_invitation-left-side-description-invitator-name">Grzegorz Kwiatkowski</span> wants to connect with you</span>
              </div>
              <img class="invitations-cards-items_invitation-arrow-forward-icon" src="../../../assets/images/icons/arrow-forward-icon.svg" alt="">
            </li>
          </router-link>
          <div v-if="n != 20" class="invitations-cards-items_divider" />
          <div v-else class="invitations-cards-items_divider-last" />
        </div>
      </ul>
    </div>
    <div class="network-load-more">
      <span class="network-load-more-inscription">Load more</span>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.invitations {
  &-cards {
    margin: 50px 0 30px 100px;
    padding: 50px;
    border: 2px $grayBorderColor solid;
    border-radius: 20px;
    max-width: 921px;

    @media (max-width: $breakpoint) {
      margin: 30px 10px 30px 10px;
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

      &_invitation-link {
        color: #000000;
        text-decoration: none;

        :hover {
          .invitations-cards-items_invitation-arrow-forward-icon {
            transform: scale(1.2);
          }
        }

        .invitations-cards-items_invitation {
          display: flex;
          justify-content: space-between;
          align-items: center;

          &-left-side {
            display: flex;
            justify-content: space-between;
            align-items: center;

            &-image {
              width: 40px;
              margin-right: 30px;
            }

            &-description {
              font-size: 14px;
              
              &-invitator-name {
                font-weight: bold;
              }
            }
          }

          &-arrow-forward-icon {
            width: 30px;
            transition: transform 0.3s ease;
          }
        }
      }
      &_divider {
        border-bottom: 1px solid $grayBorderColor;
        width: 100%;
        height: 24px;
        margin-bottom: 24px;

        &-last {
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
      color: #000000;
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