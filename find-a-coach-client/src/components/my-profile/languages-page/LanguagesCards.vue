<template> 
  <div class="languages">
    <ul class="languages-header">
      <li class="languages-header_inscription">
        <h1 class="languages-header_inscription-element">Languages</h1>
      </li>
      <li class="languages-header_add-button">
        <router-link to="/my-profile/add-language" class="languages-header_add-button-link">
          <button class="languages-header_add-button-element">
            <svg class="languages-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="languages-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>
    <ul class="languages-items">
      <li class="languages-items_language">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">English</h2>
            <router-link to="/my-profile/edit-language"><img class="languages-items_language-items_header-edit-icon" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link>
          </li>
          <li class="languages-items_language-items_level-of-knowledge"><span class="languages-items_language-items_level-of-knowledge-element">Upper-Intermediate - B2</span></li>
        </ul>
        <div class="languages-items_language-delete">
          <span class="languages-items_language-delete-inscription">Delete language</span>
        </div>
      </li>
      <li class="languages-items_language">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">Polish</h2>
            <router-link to="/my-profile/edit-language"><img class="languages-items_language-items_header-edit-icon" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link>
          </li>
          <li class="languages-items_language-items_level-of-knowledge"><span class="languages-items_language-items_level-of-knowledge-element">Advanced - C1</span></li>
        </ul>
        <div class="languages-items_language-delete">
          <span class="languages-items_language-delete-inscription">Delete language</span>
        </div>
      </li>
      <li class="languages-items_language">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">Spanish</h2>
            <router-link to="/my-profile/edit-language"><img class="languages-items_language-items_header-edit-icon" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link>
          </li>
          <li class="languages-items_language-items_level-of-knowledge"><span class="languages-items_language-items_level-of-knowledge-element">Beginner - A1</span></li>
        </ul>
        <div class="languages-items_language-delete">
          <span class="languages-items_language-delete-inscription">Delete language</span>
        </div>
      </li>
    </ul>
    <div class="languages-load-more-languages">
      <span class="languages-load-more-languages-inscription">Load more languages</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import TheSkills from '../TheSkills.vue'

export default defineComponent({
  components: {
    TheSkills
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.languages {
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

    &_language {
      margin-bottom: 30px;
      padding: 50px 50px 0 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

      @media (max-width: $breakpoint) {
        padding: 30px 30px 0 30px;
      }

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;

        &_header {
          display: flex;
          justify-content: space-between;
          align-items: center;

          &-name-of-language {
            font-size: 20px;

            @media (max-width: $breakpoint) {
              font-size: 16px;
            }
          }
          &-edit-icon {
            width: 30px;
            transition: transform 0.3s ease;

            &:hover {
              transform: scale(1.15);
            }

            @media (max-width: $breakpoint) {
              width: 24px;
            }
          }
        }

        &_level-of-knowledge {

          &-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_skills {
          margin-top: 20px;

           &-link {
            color: #000000;
            text-decoration: none;
          } 
        }
      }

      &-divider {
        border-bottom: 1px solid $grayBorderColor;
        width: 100%;
        height: 30px;
        margin-bottom: 30px;

        @media (max-width: $breakpoint) {
          height: 20px;
          margin-bottom: 20px;
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
  }

  &-load-more-languages  {
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