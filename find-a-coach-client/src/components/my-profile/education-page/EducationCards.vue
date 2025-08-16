<template> 
  <div class="education">
    <ul class="education-header">
      <li class="education-header_inscription">
        <h1 class="education-header_inscription-element">Education</h1>
      </li>
      <li class="education-header_add-button">
        <router-link to="/my-profile/add-education" class="education-header_add-button-link">
          <button class="education-header_add-button-element">
            <svg class="education-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="education-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>
    <ul class="education-items">
      <li class="education-items_school">
        <ul class="education-items_school-items">
          <li class="education-items_school-items_title">
            <ul class="education-items_school-items_title-items">
              <li class="education-items_school-items_title-items_name"><h2 class="education-items_school-items_title-items_name-element">Gdańsk University of Technology</h2></li>
              <li class="education-items_school-items_title-items_edit"><router-link to="/my-profile/edit-education"><img class="education-items_school-items_title-items_edit-element" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link></li>
            </ul>
          </li>
          <li class="education-items_school-items_academic-degree"><span class="education-items_school-items_academic-degree-element">Bachelor's degree</span></li>
          <li class="education-items_school-items_field-of-study"><span class="education-items_school-items_field-of-study-element">Economic Analyst - Financial Analyst</span></li>
          <li class="education-items_school-items_location"><span class="education-items_school-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="education-items_school-items_time"><span class="education-items_school-items_time-element">2022 - 2026</span></li>
          <li class="education-items_school-items_skills"><the-skills></the-skills></li>
        </ul>
        <div class="education-items_school-delete">
          <span class="education-items_school-delete-inscription">Delete education</span>
        </div>
      </li>
      <li class="education-items_school">
        <ul class="education-items_school-items">
          <li class="education-items_school-items_title">
            <ul class="education-items_school-items_title-items">
              <li class="education-items_school-items_title-items_name"><h2 class="education-items_school-items_title-items_name-element">Gdańsk University of Technology</h2></li>
              <li class="education-items_school-items_title-items_edit"><router-link to="/my-profile/edit-education"><img class="education-items_school-items_title-items_edit-element" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link></li>
            </ul>
          </li>
          <li class="education-items_school-items_academic-degree"><span class="education-items_school-items_academic-degree-element">Bachelor's degree</span></li>
          <li class="education-items_school-items_field-of-study"><span class="education-items_school-items_field-of-study-element">Economic Analyst - Financial Analyst</span></li>
          <li class="education-items_school-items_location"><span class="education-items_school-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="education-items_school-items_time"><span class="education-items_school-items_time-element">2022 - 2026</span></li>
          <li class="education-items_school-items_skills"><the-skills></the-skills></li>
        </ul>
        <div class="education-items_school-delete">
          <span class="education-items_school-delete-inscription">Delete education</span>
        </div>
      </li>
    </ul>
    <div class="education-load-more-education">
      <span class="education-load-more-education-inscription">Load more education</span>
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

.education {
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

    &_school {
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

        &_title {

          &-items {
            list-style: none;
            display: flex;
            justify-content: space-between;
            align-items: center;

            &_name {

              &-link {
                color: #000000;
                text-decoration: none;
              } 
              &-element {
                font-size: 20px;

                @media (max-width: $breakpoint) {
                  font-size: 16px;
                }
              }
            }

            &_edit {

              &-element {
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
          }
        }

        &_academic-degree {

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_field-of-study{

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_location {

          &-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_time {

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

          @media (max-width: $breakpoint) {
            margin-top: 10px;
          }

          &-link {
            color: #000000;
            text-decoration: none;
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
  }

  &-load-more-education  {
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