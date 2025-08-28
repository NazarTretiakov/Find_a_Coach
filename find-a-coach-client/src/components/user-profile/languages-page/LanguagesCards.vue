<template> 
  <div class="languages">
    <ul class="languages-header">
      <li class="languages-header_inscription">
        <h1 class="languages-header_inscription-element">Languages</h1>
      </li>
    </ul>
    <ul class="languages-items">
      <li class="languages-items_language">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">English</h2>
          </li>
          <li class="languages-items_language-items_level-of-knowledge"><span class="languages-items_language-items_level-of-knowledge-element">Upper-Intermediate - B2</span></li>
        </ul>
      </li>
      <li class="languages-items_language">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">Polish</h2>
          </li>
          <li class="languages-items_language-items_level-of-knowledge"><span class="languages-items_language-items_level-of-knowledge-element">Advanced - C1</span></li>
        </ul>
      </li>
      <li class="languages-items_language">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">Spanish</h2>
          </li>
          <li class="languages-items_language-items_level-of-knowledge"><span class="languages-items_language-items_level-of-knowledge-element">Beginner - A1</span></li>
        </ul>
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
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_language {
      margin-bottom: 30px;
      padding: 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

      @media (max-width: $breakpoint) {
        padding: 30px;
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