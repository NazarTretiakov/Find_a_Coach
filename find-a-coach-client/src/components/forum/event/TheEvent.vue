<template> 
  <div class="event">
    <ul class="event-header">
      <li class="event-header_user-info">
        <router-link to="user-profile" class="event-header_user-info-link">
          <img class="event-header_user-info-profile-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile image">
          <span class="event-header_user-info-user-name">Janusz Kowalski</span>
        </router-link>  
      </li>
      <li class="event-header_publication-time">
        <span class="event-header_publication-time-element">2 days ago</span>
      </li>
    </ul>
    <h1 class="event-title">Starting an store checking project</h1>
    <span class="event-subjects">Logistics, Business, Marketing</span>
    <img class="event-image" src="../../../assets/images/activities-image.jpeg" alt="Image of the event">
    <h3 class="event-searching-for">Searching for two people:</h3>
    <div :class="!isPositionPanelOpened[0] ? 'event-position' : 'event-position-opened'">
      <ul :class="!isPositionPanelOpened[0] ? 'event-position-header' : 'event-position-opened-header'">
        <li :class="!isPositionPanelOpened[0] ? 'event-position-header_left-side' : 'event-position-opened-header_left-side'">
          <img :class="!isPositionPanelOpened[0] ? 'event-position-header_left-side-icon' : 'event-position-opened-header_left-side-icon'" src="../../../assets/images/icons/position-icon.svg" alt="Position icon">
          <span :class="!isPositionPanelOpened[0] ? 'event-position-header_left-side-name' : 'event-position-opened-header_left-side-name'">Logistics coordinator</span>
        </li>
        <li :class="!isPositionPanelOpened[0] ? 'event-position-header_right-side' : 'event-position-opened-header_right-side'">
          <img @click="triggerPositionPanelOpening(0)" :class="!isPositionPanelOpened[0] ? 'event-position-header_right-side-arrow-icon' : 'event-position-opened-header_right-side-arrow-icon'" src="../../../assets/images/icons/arrow-down-icon.svg" alt="Arrow down icon">
        </li>
      </ul>
      <the-skills :class="!isPositionPanelOpened[0] ? 'event-position-skills' : 'event-position-opened-skills'"></the-skills>
      <span :class="!isPositionPanelOpened[0] ? 'event-position-payment' : 'event-position-opened-payment'">Payment: Unpaid</span>
      <p :class="!isPositionPanelOpened[0] ? 'event-position-description' : 'event-position-opened-description'">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów. Rozumiem ważność samokształcenia oraz rozwijania umiejętności miękkich.Zawodowo zajmuję się programowaniem backendu na platformie .NET. Mam doświadczenie w tworzeniu aplikacji webowych w technologiach: ASP.NET Core MVC oraz ASP.NET Core Web API. Także posiadam umiejętności programowania frontendu w Vue.js.</p>
      <apply-on-event-button :class="!isPositionPanelOpened[0] ? 'event-position-button' : 'event-position-opened-button'"></apply-on-event-button>
    </div>

    <div :class="!isPositionPanelOpened[1] ? 'event-position' : 'event-position-opened'">
      <ul :class="!isPositionPanelOpened[1] ? 'event-position-header' : 'event-position-opened-header'">
        <li :class="!isPositionPanelOpened[1] ? 'event-position-header_left-side' : 'event-position-opened-header_left-side'">
          <img :class="!isPositionPanelOpened[1] ? 'event-position-header_left-side-icon' : 'event-position-opened-header_left-side-icon'" src="../../../assets/images/icons/position-icon.svg" alt="Position icon">
          <span :class="!isPositionPanelOpened[1] ? 'event-position-header_left-side-name' : 'event-position-opened-header_left-side-name'">Logistics coordinator</span>
        </li>
        <li :class="!isPositionPanelOpened[1] ? 'event-position-header_right-side' : 'event-position-opened-header_right-side'">
          <img @click="triggerPositionPanelOpening(1)" :class="!isPositionPanelOpened[1] ? 'event-position-header_right-side-arrow-icon' : 'event-position-opened-header_right-side-arrow-icon'" src="../../../assets/images/icons/arrow-down-icon.svg" alt="Arrow down icon">
        </li>
      </ul>
      <the-skills :class="!isPositionPanelOpened[1] ? 'event-position-skills' : 'event-position-opened-skills'"></the-skills>
      <span :class="!isPositionPanelOpened[1] ? 'event-position-payment' : 'event-position-opened-payment'">Payment: Unpaid</span>
      <p :class="!isPositionPanelOpened[1] ? 'event-position-description' : 'event-position-opened-description'">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów. Rozumiem ważność samokształcenia oraz rozwijania umiejętności miękkich.Zawodowo zajmuję się programowaniem backendu na platformie .NET. Mam doświadczenie w tworzeniu aplikacji webowych w technologiach: ASP.NET Core MVC oraz ASP.NET Core Web API. Także posiadam umiejętności programowania frontendu w Vue.js.</p>
      <apply-on-event-button :class="!isPositionPanelOpened[1] ? 'event-position-button' : 'event-position-opened-button'"></apply-on-event-button>
    </div>
    <span class="event-date-of-beginning">Date of beginning: 14 august 2022</span>
    <p class="event-description">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów. Rozumiem ważność samokształcenia oraz rozwijania umiejętności miękkich.Zawodowo zajmuję się programowaniem backendu na platformie .NET. Mam doświadczenie w tworzeniu aplikacji webowych w technologiach: ASP.NET Core MVC oraz ASP.NET Core Web API. Także posiadam umiejętności programowania frontendu w Vue.js.</p>
    <h2 class="event-comments-header">Comments</h2>
    <ul class="event-create-comment-bar">
      <li class="event-create-comment-bar_left-side">
        <input class="event-create-comment-bar_left-side-input" type="text" placeholder="Leave your comment...">
        <svg class="event-create-comment-bar_left-side-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Z" fill="#B1B1B1" />          
        </svg>
      </li>
      <li class="event-create-comment-bar_right-side">
        <div class="event-create-comment-bar_right-side-like">
          <img @click="triggerLike" v-if="!isLiked" class="event-create-comment-bar_right-side-like-icon" src="../../../assets/images/icons/like-icon.svg" alt="Like icon">
          <svg @click="triggerLike" v-if="isLiked"  class="event-create-comment-bar_right-side-like-icon"
            xmlns="http://www.w3.org/2000/svg"
            height="30px"
            width="30px"
            viewBox="0 -960 960 960">
            <path d="m480-120-58-52q-101-91-167-157T150-447.5Q111-500 95.5-544T80-634q0-94 63-157t157-63q52 0 99 22t81 62q34-40 81-62t99-22q94 0 157 63t63 157q0 46-15.5 90T810-447.5Q771-395 705-329T538-172l-58 52" fill="red" />
          </svg>
          <span class="event-create-comment-bar_right-side-like-amount">123</span>
        </div>
        <img @click="triggerSave" v-if="!isSaved" class="event-create-comment-bar_right-side-save-icon" src="../../../assets/images/icons/save-icon.svg" alt="Save icon">
        <svg @click="triggerSave" v-else class="event-create-comment-bar_right-side-save-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M200-120v-640q0-33 23.5-56.5T280-840h400q33 0 56.5 23.5T760-760v640L480-240 200-120Z" fill="black" />
        </svg>
      </li>
    </ul>
    <ul class="event-comments">
      <li class="event-comments_comment">
        <ul class="event-comments_comment-header">
          <li class="event-comments_comment-header_left-side">
            <router-link to="/user-profile" class="event-comments_comment-header_left-side-user-name-link">
              <img class="event-comments_comment-header_left-side-user-profile-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile photo">
              <span class="event-comments_comment-header_left-side-user-name">Matvii Tretiakov</span>
            </router-link>
          </li>
          <li class="event-comments_comment-header_right-side">
            <span class="event-comments_comment-header_right-side-time-of-publication">1 day ago</span>
          </li>
        </ul>
        <p class="event-comments_comment-content">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów.</p>
        <div class="event-comments_comment-divider"></div>
      </li>
      <li class="event-comments_comment">
        <ul class="event-comments_comment-header">
          <li class="event-comments_comment-header_left-side">
            <router-link to="/user-profile" class="event-comments_comment-header_left-side-user-name-link">
              <img class="event-comments_comment-header_left-side-user-profile-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile photo">
              <span class="event-comments_comment-header_left-side-user-name">Matvii Tretiakov</span>
            </router-link>
          </li>
          <li class="event-comments_comment-header_right-side">
            <span class="event-comments_comment-header_right-side-time-of-publication">1 day ago</span>
          </li>
        </ul>
        <p class="event-comments_comment-content">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów.</p>
      </li>
    </ul>
  </div> 
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";

import TheSkills from "../../my-profile/TheSkills.vue";
import ApplyOnEventButton from "../../my-profile/activities-page/event/TheEvent.vue";

export default defineComponent({
  components: {
    TheSkills,
    ApplyOnEventButton,
  },
  setup() {
    const isPositionPanelOpened = ref<boolean[]>([false, false])
    const isLiked = ref<boolean>(false)
    const isSaved = ref<boolean>(false)

    const triggerPositionPanelOpening = (positionId: number) => {
      isPositionPanelOpened.value[positionId] = !isPositionPanelOpened.value[positionId]
    }

    const triggerLike = () => {
      isLiked.value = !isLiked.value
    }

    const triggerSave = () => {
      isSaved.value = !isSaved.value
    }

    return { isPositionPanelOpened, isLiked, isSaved, triggerPositionPanelOpening, triggerLike, triggerSave }
  }
});
</script>

<style lang="scss" scoped>
@use "../../../assets/styles/config" as *;

.event {
  margin: 50px 0 100px 100px;
  padding: 0 50px 50px 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
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

      &-link {
        display: flex;
        align-items: center;
        color: black;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }

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

  &-image {
    width: 100%;
  }

  &-searching-for {
    font-size: 16px;
    font-weight: normal;
    margin-top: 20px;

    @media (max-width: $breakpoint) {
      font-size: 14px;
    }
  }

  &-position {
    border: 2px solid $grayBorderColor;
    padding: 10px 20px;
    border-radius: 12px;
    margin-top: 14px;
    transition: all 0.5s ease;

    &-header {
      list-style: none;
      display: flex;
      justify-content: space-between;
      align-items: center;

      &_left-side {
        display: flex;
        justify-content: space-between;
        align-items: center;

        &-icon {
          width: 30px;
          margin-right: 10px;

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }
        &-name {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }

      &_right-side {
        height: 30px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          height: 24px;
        }

        &:hover {
          cursor: pointer;
          transform: scale(1.20);
        }

        &-arrow-icon {
          width: 30px;

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }
      }
    }

    &-skills {
      display: none;
    }

    &-payment {
      display: none;
    }

    &-description {
      display: none;
    }

    &-button {
      display: none;
    }
  }
  &-position-opened {
    border: 2px solid $grayBorderColor;
    padding: 10px 20px 20px 20px;
    border-radius: 12px;
    margin-top: 10px;
    transition: all 0.3s ease;

    &-header {
      list-style: none;
      display: flex;
      justify-content: space-between;
      align-items: center;

      &_left-side {
        display: flex;
        justify-content: space-between;
        align-items: center;

        &-icon {
          width: 30px;
          margin-right: 10px;

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }

        &-name {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }

      &_right-side {
        height: 30px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          height: 24px;
        }

        &:hover {
          cursor: pointer;
          transform: scale(1.20);
        }

        &-arrow-icon {
          width: 30px;
          transform: rotate(180deg);

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }
      }
    }

    &-skills {
      margin-top: 20px;
    }
    &-payment {
      font-size: 14px;
      display: block;
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-description {
      font-size: 14px;
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-button {
      margin-top: 20px;
    }
  }

  &-date-of-beginning {
    font-size: 14px;
    display: block;
    margin-top: 30px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-description {
    font-size: 14px;
    margin-top: 20px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-comments-header {
    font-size: 20px;
    margin-top: 40px;

    @media (max-width: $breakpoint) {
      font-size: 16px;
    }
  }

  &-create-comment-bar {
    list-style: none;
    display: flex;
    justify-content: space-between;
    margin-top: 20px;

    &_left-side {

      &-input {
        border: 2px solid $grayBorderColor;
        border-radius: 12px;
        height: 34px;
        width: 46vw;
        padding: 10px 46px 10px 18px;
        font-size: 14px;

        &:active {
          border: 2px solid $grayBorderColor;
        }

        @media (max-width: $breakpoint) {
          font-size: 12px;
          width: 54vw;
          padding: 10px 46px 10px 14px;
        }
      }

      &-icon {
        margin: 0 0 -11px -30px;
        padding: 0 10px 4px 0;

        &:hover {
          cursor: pointer;
        }
      }
    }

    &_right-side {
      display: flex;
      align-items: center;

      &-like {
        display: flex;
        align-items: center;
        margin-right: 16px;

        &-icon {
          width: 30px;
          margin-right: 2px;
          transition: transform 0.3s ease;

          @media (max-width: $breakpoint) {
            width: 28px;
          }

          &:hover {
            cursor: pointer;
            transform: scale(1.20);
          }
        }

        &-amount {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }

      &-save-icon {
        width: 30px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          width: 28px;
        }

        &:hover {
          cursor: pointer;
          transform: scale(1.20);
        }
      }
    }
  }

  &-comments {
    list-style: none;
    display: flex;
    flex-direction: column;
    margin-top: 30px;

    &_comment {

      &-header {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-items: center;

        &_left-side {
          display: flex;
          align-items: center;

          &-user-profile-image {
            width: 30px;
            margin-right: 10px;
          }

          &-user-name {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }

            &-link {
              display: flex;
              align-items: center;
              color: black;
              text-decoration: none;
              
              &:hover {
                text-decoration: underline;
              }
            }
          }
        }

        &_right-side {

          &-time-of-publication {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }

      &-content {
        font-size: 14px;
        margin-top: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }

      &-divider {
        border-bottom: 1px solid $grayBorderColor;
        height: 24px;
        margin-bottom: 24px;
      }
    }
  }

  &-delete {
    display: flex;
    justify-content: center;
    align-content: center;
    border-top: 2px solid $grayBorderColor;
    margin: 40px -50px 0 -50px;
    padding: 14px 0;
    transition: background-color 0.3s ease;
    border-bottom-right-radius: 20px;
    border-bottom-left-radius: 20px;

    @media (max-width: $breakpoint) {
      margin: 40px -30px 0 -30px;
    }

    &-inscription {
      font-size: 14px;
      color: $errorColor;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }

    &:hover {
      cursor: pointer;
      background-color: #ececec;
    }
  }
}
</style>