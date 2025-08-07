<template>
  <router-link to="/path-to-post" class="event-link">
    <div class="event">
      <ul class="event-items">
        <li class="event-items_header">
          <ul class="event-items_header-items">
            <li class="event-items_header-items_user">
              <img src="../../assets/images/icons/user-icon.jpg" alt="User profile image"  class="event-items_header-items_user-icon">
              <h1 class="event-items_header-items_user-name">Grzegorz Kwiatkowski</h1>
            </li>
            <li class="event-items_header-items_date-of-publication">2 days ago</li>
          </ul>
        </li>
        <li class="event-items_title"><h2 class="event-items_title-element">Starting an English language course</h2></li>
        <li class="event-items_incription"><span class="event-items_incription-element">Searching for 2 people in that event!</span></li>
        <li class="event-items_paragraph"><p class="event-items_paragraph-element">Happy to share that I'm starting an English language course! I've wanted to start learning English since the beginning of this year, and now it's finally becoming a reality. This is an offline course, so while I'm attending it, I won't have much opportunity to practice real-life English communication. That's why I'm looking for someone with intermediate English skills to teach me...</p></li>
      </ul>
    </div>
  </router-link>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.event {
  width: 400px;
  padding: 14px 40px 20px 40px;
  border: 2px solid $grayBorderColor;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    width: 100%;
    padding: 14px 36px 20px 36px;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-content: center;

    &_header {
      border-bottom: 2px solid $grayBorderColor;
      margin: 0 -40px;
      padding: 0 40px 14px 40px;

      @media (max-width: $breakpoint) {
        margin: 0 -38px 0 -37px; 
        padding: 0 36px 14px 36px; 
      }

      &-items {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-items: center;

        &_user {
          display: flex;
          align-items: center;

          &-icon {
            width: 40px;

            @media (max-width: $breakpoint) {
              width: 30px;
            }
          }

          &-name {
            font-size: 14px;
            margin-left: 10px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_date-of-publication {
          font-size: 14px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }
    }

    &_title {
      margin-top: 20px;

      &-element {
        font-size: 16px;

        @media (max-width: $breakpoint) {
          font-size: 14px;
        }
      }
    }

    &_incription {
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        margin-top: 6px;
      }

      &-element {
        font-size: 14px;
        color: $linkColor;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }

    &_paragraph {
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        margin-top: 6px;
      }

      &-element {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }

  &-link {
    color: #000000;
    text-decoration: none;
  }
}

</style>