<template> 
  <div class="activities">
    <ul class="activities-header">
      <li class="activities-header_inscription">
        <h1 class="activities-header_inscription-element">Activities</h1>
      </li>
      <li class="activities-header_add-button">
        <router-link to="/my-profile/add-activity" class="activities-header_add-button-link">
          <button class="activities-header_add-button-element">
            <svg class="activities-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="activities-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>
    <ul class="activities-items">
      <router-link class="activities-items_activity-link" to="/my-profile/event">
        <li class="activities-items_activity">
          <ul class="activities-items_activity-header">
            <li class="activities-items_activity-header_user-info">
              <img class="activities-items_activity-header_user-info-profile-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile image">
              <span class="activities-items_activity-header_user-info-user-name">Janusz Kowalski</span>
            </li>
            <li class="activities-items_activity-header_publication-time">
              <span class="activities-items_activity-header_publication-time-element">2 days ago</span>
            </li>
          </ul>
          <h1 class="activities-items_activity-title">Starting an store checking project</h1>
          <span class="activities-items_activity-subjects">Logistics, Business, Marketing</span>
          <span class="activities-items_activity-special-phrase">Searching for 2 people in that event</span>
          <img class="activities-items_activity-image" src="../../../assets/images/activities-image.jpeg" alt="Image of the activity">
        </li>
      </router-link>
    </ul>
    <div class="activities-load-more-activities">
      <span class="activities-load-more-activities-inscription">Load more activities</span>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.activities {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-content: center;
    margin-bottom: 30px;

    @media (max-width: $breakpoint) {
      margin-bottom: 20px;
    }

    &_inscription {
      &-element {
        font-size: 28px;
        margin-left: 6px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_add-button {
      display: flex;
      align-items: flex-end;

      &-element {
        display: flex;
        justify-content: center;
        align-items: center;
        border: 2px solid $grayBorderColor;
        border-radius: 10px;
        width: 100px;
        height: 40px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          height: 36px;
        }

        &:hover {
          transform: scale(1.10);
        }

        &-icon {
          width: 30px;
          margin-right: 8px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            width: 24px;
            margin-right: 6px;
          }
        }
        &-inscription {
          font-size: 20px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            font-size: 16px;
          }
        }
      }

      &-link {
        text-decoration: none;
      }
    }
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_activity {
      padding: 0 50px 50px 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

      @media (max-width: $breakpoint) {
        padding: 0 30px 30px 30px;
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

          &-profile-image {
            width: 36px;
            margin-right: 10px;

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

      &-title {
        font-size: 24px;
        margin-top: 30px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
          margin-top: 20px;
        }
      }
      &-subjects {
        font-size: 14px;
        display: block;
        color: $linkColor;
        margin-bottom: 20px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
          margin-bottom: 14px;
        }
      }
      &-special-phrase {
        font-size: 14px;
        display: block;
        color: $linkColor;
        margin-bottom: 20px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
      &-image {
        width: 100%;
      }

      &-link {
        color: #000000;
        text-decoration: none;
        margin-bottom: 30px;

        @media (max-width: $breakpoint) {
          margin-bottom: 20px;
        }
      }
    }
  }

  &-load-more-activities {
    display: flex;
    justify-content: center;
    align-items: center;
    border: 2px $grayBorderColor solid;
    border-radius: 20px;
    height: 50px;
    transition: background-color 0.3s ease;

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