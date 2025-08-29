<template> 
  <div class="qa">
    <ul class="qa-header">
      <li class="qa-header_user-info">
        <router-link to="/user-profile" class="qa-header_user-info-link">
          <img class="qa-header_user-info-profile-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile image">
          <span class="qa-header_user-info-user-name">Janusz Kowalski</span>
        </router-link>  
      </li>
      <li class="qa-header_publication-time">
        <span class="qa-header_publication-time-element">2 days ago</span>
      </li>
    </ul>
    <h1 class="qa-title">Starting an store checking project</h1>
    <span class="qa-subjects">Logistics, Business, Marketing</span>
    <img class="qa-image" src="../../../assets/images/activities-image.jpeg" alt="Image of the qa">
    <p class="qa-description">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów. Rozumiem ważność samokształcenia oraz rozwijania umiejętności miękkich.Zawodowo zajmuję się programowaniem backendu na platformie .NET. Mam doświadczenie w tworzeniu aplikacji webowych w technologiach: ASP.NET Core MVC oraz ASP.NET Core Web API. Także posiadam umiejętności programowania frontendu w Vue.js.</p>
    
    <div class="qa-answer-text-area">
      <label class="qa-answer-text-area-label" for="answer">Leave your answer</label>
      <textarea 
        v-model="valueOfTextArea"
        class="qa-answer-text-area-element" 
        name="answer" 
        id="answer"
      ></textarea>
      <span class="qa-answer-text-area-number-of-signs">
        <span :class="isLimitExceeded ? 'qa-answer-text-area-number-of-signs-entered-exceeded' : 'qa-answer-text-area-number-of-signs-entered'">{{ numberOfSignsEntered }}</span>/<span class="qa-answer-text-area-number-of-signs-max">{{ maxNumberOfSigns }}</span>
      </span>
    </div>
    
    <h2 class="qa-comments-header">Comments</h2>
    <ul class="qa-create-comment-bar">
      <li class="qa-create-comment-bar_left-side">
        <input class="qa-create-comment-bar_left-side-input" type="text" placeholder="Leave your comment...">
        <svg class="qa-create-comment-bar_left-side-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Z" fill="#B1B1B1" />          
        </svg>
      </li>
      <li class="qa-create-comment-bar_right-side">
        <div class="qa-create-comment-bar_right-side-like">
          <img @click="triggerLike" v-if="!isLiked" class="qa-create-comment-bar_right-side-like-icon" src="../../../assets/images/icons/like-icon.svg" alt="Like icon">
          <svg @click="triggerLike" v-if="isLiked"  class="qa-create-comment-bar_right-side-like-icon"
            xmlns="http://www.w3.org/2000/svg"
            height="30px"
            width="30px"
            viewBox="0 -960 960 960">
            <path d="m480-120-58-52q-101-91-167-157T150-447.5Q111-500 95.5-544T80-634q0-94 63-157t157-63q52 0 99 22t81 62q34-40 81-62t99-22q94 0 157 63t63 157q0 46-15.5 90T810-447.5Q771-395 705-329T538-172l-58 52" fill="red" />
          </svg>
          <span class="qa-create-comment-bar_right-side-like-amount">123</span>
        </div>
        <img @click="triggerSave" v-if="!isSaved" class="qa-create-comment-bar_right-side-save-icon" src="../../../assets/images/icons/save-icon.svg" alt="Save icon">
        <svg @click="triggerSave" v-else class="qa-create-comment-bar_right-side-save-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M200-120v-640q0-33 23.5-56.5T280-840h400q33 0 56.5 23.5T760-760v640L480-240 200-120Z" fill="black" />
        </svg>
      </li>
    </ul>
    <ul class="qa-comments">
      <li class="qa-comments_comment">
        <ul class="qa-comments_comment-header">
          <li class="qa-comments_comment-header_left-side">
            <router-link to="/user-profile" class="qa-comments_comment-header_left-side-user-name-link">
              <img class="qa-comments_comment-header_left-side-user-profile-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile photo">
              <span class="qa-comments_comment-header_left-side-user-name">Matvii Tretiakov</span>
            </router-link>
          </li>
          <li class="qa-comments_comment-header_right-side">
            <span class="qa-comments_comment-header_right-side-time-of-publication">1 day ago</span>
          </li>
        </ul>
        <p class="qa-comments_comment-content">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów.</p>
        <div class="qa-comments_comment-divider"></div>
      </li>
      <li class="qa-comments_comment">
        <ul class="qa-comments_comment-header">
          <li class="qa-comments_comment-header_left-side">
            <router-link to="/user-profile" class="qa-comments_comment-header_left-side-user-name-link">
              <img class="qa-comments_comment-header_left-side-user-profile-image" src="../../../assets/images/icons/user-icon.jpg" alt="User profile photo">
              <span class="qa-comments_comment-header_left-side-user-name">Matvii Tretiakov</span>
            </router-link>
          </li>
          <li class="qa-comments_comment-header_right-side">
            <span class="qa-comments_comment-header_right-side-time-of-publication">1 day ago</span>
          </li>
        </ul>
        <p class="qa-comments_comment-content">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów.</p>
      </li>
    </ul>
  </div> 
</template>

<script lang="ts">
import { defineComponent, ref, computed } from "vue";

export default defineComponent({
  setup() {
    const valueOfTextArea = ref<string>('')
    const maxNumberOfSigns = 400
    const isLiked = ref<boolean>(false)
    const isSaved = ref<boolean>(false)

    const numberOfSignsEntered = computed(() => valueOfTextArea.value.length)
    const isLimitExceeded = computed(() => numberOfSignsEntered.value > maxNumberOfSigns)

    const triggerLike = () => {
      isLiked.value = !isLiked.value
    }

    const triggerSave = () => {
      isSaved.value = !isSaved.value
    }

    return { valueOfTextArea, numberOfSignsEntered, isLimitExceeded, maxNumberOfSigns, isLiked, isSaved, triggerLike, triggerSave }
  }
});
</script>

<style lang="scss" scoped>
@use "../../../assets/styles/config" as *;

.qa {
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

  &-description {
    font-size: 14px;
    margin-top: 20px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-answer-text-area {
    width: 100%;
    margin-top: 40px;

    @media (max-width: $breakpoint) {
      width: 100%;
    }

    &-label {
      color: $grayBorderColor;
      font-size: 14px;
      display: block;
      margin: 0 0 2px 2px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-element {
      border: 1px #000000 solid;
      width: 100%;
      height: 100px;
      border-radius: 10px;
      transition: border 0.2s ease;
      padding: 0 8px;
      font-size: 14px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
        width: 100%;
        height: 60px;
      }

      &:hover {
        border: 2px #000000 solid;
      }
    }
    &-number-of-signs {
      color: $grayBorderColor;
      font-size: 14px;
      display: block;
      justify-self: flex-end;
      margin: -6px 2px 0 0;

      &-entered {

        &-exceeded {
          color: $errorColor;
        }
      }
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