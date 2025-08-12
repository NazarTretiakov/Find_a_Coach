<template> 
  <div class="forum">
    <ul class="forum-header">
      <li class="forum-header_inscription">
        <h1 class="forum-header_inscription-element">Activities</h1>
      </li>
      <li class="forum-header_add-button">
        <router-link to="/profile/add-activity" class="forum-header_add-button-link">
          <button class="forum-header_add-button-element">
            <svg class="forum-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="forum-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>
    <ul class="forum-activities">
      <router-link class="forum-activities_activity-link" to="/path-to-activity">
        <li class="forum-activities_activity">
          <ul class="forum-activities_activity-header">
            <li class="forum-activities_activity-header_user-info">
              <img class="forum-activities_activity-header_user-info-profile-image" src="../../assets/images/icons/user-icon.jpg" alt="User profile image">
              <span class="forum-activities_activity-header_user-info-user-name">Janusz Kowalski</span>
            </li>
            <li class="forum-activities_activity-header_publication-time">
              <span class="forum-activities_activity-header_publication-time-element">2 days ago</span>
            </li>
          </ul>
          <h1 class="forum-activities_activity-title">Starting an store checking project</h1>
          <span class="forum-activities_activity-subjects">Logistics, Business, Marketing</span>
          <span class="forum-activities_activity-special-phrase">Searching for 2 people in that event</span>
          <img class="forum-activities_activity-image" src="../../assets/images/activities-image.jpeg" alt="Image of the activity">
        </li>
      </router-link>
    </ul>
    <div class="forum-load-more-activities">
      <span class="forum-load-more-activities-inscription">Load more activities</span>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.forum {
  margin: 50px 0 100px 150px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
  }

  .forum-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-content: center;
    margin-bottom: 30px;

    @media (max-width: $breakpoint) {
      margin-bottom: 20px;
    }

    .forum-header_inscription {
      
      .forum-header_inscription-element {
        font-size: 28px;
        margin-left: 6px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    .forum-header_add-button {
      display: flex;
      align-items: flex-end;

      .forum-header_add-button-element {
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

        .forum-header_add-button-element-icon {
          width: 30px;
          margin-right: 8px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            width: 24px;
            margin-right: 6px;
          }
        }
        .forum-header_add-button-element-inscription {
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

  .forum-activities {
    list-style: none;
    display: flex;
    flex-direction: column;

    .forum-activities_activity {
      padding: 0 50px 50px 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

      @media (max-width: $breakpoint) {
        padding: 0 30px 30px 30px;
      }

      .forum-activities_activity-header {
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

        .forum-activities_activity-header_user-info {
          display: flex;
          align-items: center;

          .forum-activities_activity-header_user-info-profile-image {
            width: 36px;
            margin-right: 10px;

            @media (max-width: $breakpoint) {
              width: 30px;
              margin-right: 8px;
            }
          }
          .forum-activities_activity-header_user-info-user-name {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        .forum-activities_activity-header_publication-time {

          .forum-activities_activity-header_publication-time-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }

      .forum-activities_activity-title {
        font-size: 24px;
        margin-top: 30px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
          margin-top: 20px;
        }
      }
      .forum-activities_activity-subjects {
        font-size: 14px;
        display: block;
        color: $linkColor;
        margin-bottom: 20px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
          margin-bottom: 14px;
        }
      }
      .forum-activities_activity-special-phrase {
        font-size: 14px;
        display: block;
        color: $linkColor;
        margin-bottom: 20px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
      .forum-activities_activity-image {
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

  .forum-load-more-activities {
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

    .forum-load-more-activities-inscription {
      font-size: 14px;

      @media (max-width: $breakpoint) {
          font-size: 12px;
      }
    }
  }
}

</style>