<template> 
  <div class="activities">
    <ul class="activities-items">
      <router-link class="activities-items_activity-link" to="/forum/event">
        <li class="activities-items_activity">
          <ul class="activities-items_activity-header">
            <li class="activities-items_activity-header_user-info">
              <img class="activities-items_activity-header_user-info-profile-image" src="../../assets/images/icons/user-icon.jpg" alt="User profile image">
              <span class="activities-items_activity-header_user-info-user-name">Janusz Kowalski</span>
            </li>
            <li class="activities-items_activity-header_publication-time">
              <span class="activities-items_activity-header_publication-time-element">2 days ago</span>
            </li>
          </ul>
          <h1 class="activities-items_activity-title">Starting an store checking project</h1>
          <span class="activities-items_activity-subjects">Logistics, Business, Marketing</span>
          <span class="activities-items_activity-special-phrase">Searching for 2 people in that event</span>
          <img class="activities-items_activity-image" src="../../assets/images/activities-image.jpeg" alt="Image of the activity">
        </li>
      </router-link>
      <router-link class="activities-items_activity-link" to="/forum/qa">
        <li class="activities-items_activity">
          <ul class="activities-items_activity-header">
            <li class="activities-items_activity-header_user-info">
              <img class="activities-items_activity-header_user-info-profile-image" src="../../assets/images/icons/user-icon.jpg" alt="User profile image">
              <span class="activities-items_activity-header_user-info-user-name">Janusz Kowalski</span>
            </li>
            <li class="activities-items_activity-header_publication-time">
              <span class="activities-items_activity-header_publication-time-element">2 days ago</span>
            </li>
          </ul>
          <h1 class="activities-items_activity-title">What is the best technology to use in store checking?</h1>
          <span class="activities-items_activity-subjects">Logistics, Business, Marketing</span>
          <span class="activities-items_activity-special-phrase">Leave your answer for that question</span>
          <img class="activities-items_activity-image" src="../../assets/images/activities-image.jpeg" alt="Image of the activity">
        </li>
      </router-link>
      <router-link class="activities-items_activity-link" to="/forum/survey">
        <li class="activities-items_activity">
          <ul class="activities-items_activity-header">
            <li class="activities-items_activity-header_user-info">
              <img class="activities-items_activity-header_user-info-profile-image" src="../../assets/images/icons/user-icon.jpg" alt="User profile image">
              <span class="activities-items_activity-header_user-info-user-name">Janusz Kowalski</span>
            </li>
            <li class="activities-items_activity-header_publication-time">
              <span class="activities-items_activity-header_publication-time-element">2 days ago</span>
            </li>
          </ul>
          <h1 class="activities-items_activity-title">What is the best technology to use in store checking?</h1>
          <span class="activities-items_activity-subjects">Logistics, Business, Marketing</span>
          <span class="activities-items_activity-special-phrase">Select your answer in this questionnaire</span>
          <img class="activities-items_activity-image" src="../../assets/images/activities-image.jpeg" alt="Image of the activity">
        </li>
      </router-link>
      <router-link class="activities-items_activity-link" to="/forum/post">
        <li class="activities-items_activity">
          <ul class="activities-items_activity-header">
            <li class="activities-items_activity-header_user-info">
              <img class="activities-items_activity-header_user-info-profile-image" src="../../assets/images/icons/user-icon.jpg" alt="User profile image">
              <span class="activities-items_activity-header_user-info-user-name">Janusz Kowalski</span>
            </li>
            <li class="activities-items_activity-header_publication-time">
              <span class="activities-items_activity-header_publication-time-element">2 days ago</span>
            </li>
          </ul>
          <h1 class="activities-items_activity-title">5 tips how to optimize the store checking</h1>
          <span class="activities-items_activity-subjects">Logistics, Business, Marketing</span>
          <span class="activities-items_activity-special-phrase">Join the discussion in this post</span>
          <img class="activities-items_activity-image" src="../../assets/images/activities-image.jpeg" alt="Image of the activity">
        </li>
      </router-link>
    </ul>
    <div class="activities-load-more-activities">
      <span class="activities-load-more-activities-inscription">Load more activities</span>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.activities {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
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