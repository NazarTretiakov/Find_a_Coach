<template>
  <div class="experience-section">
    <ul class="experience-section-items">
      <li class="experience-section-items_header">
        <ul class="experience-section-items_header-items">
          <li class="experience-section-items_header-items_incription"><h1 class="experience-section-items_header-items_incription-element">Experience</h1></li>
          <li class="experience-section-items_header-items_buttons">
            <ul class="experience-section-items_header-items_buttons-items">
              <li class="experience-section-items_header-items_buttons-items_add"><router-link to="/link-to-add-activity" class="experience-section-items_header-items_buttons-items_add-link"><img src="../../assets/images/icons/add-icon.svg" alt="Add icon" class="experience-section-items_header-items_buttons-items_add-element"></router-link></li>
              <li class="experience-section-items_header-items_buttons-items_edit"><router-link to="/link-to-edit-activity" class="experience-section-items_header-items_buttons-items_edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="experience-section-items_header-items_buttons-items_edit-element"></router-link></li>
            </ul>
          </li>
        </ul>
      </li>
      <li class="experience-section-items_position">
        <ul class="experience-section-items_position-items">
          <li class="experience-section-items_position-items_title">
            <ul class="experience-section-items_position-items_title-items">
              <li class="experience-section-items_position-items_title-items_name"><router-link to="/path-to-all-experiences-of-user-with-scrollable-thing" class="experience-section-items_position-items_title-items_name-link"><h2 class="experience-section-items_position-items_title-items_name-element">Technical department employee</h2></router-link></li>
              <li class="experience-section-items_position-items_title-items_time"><span class="experience-section-items_position-items_title-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
            </ul>
          </li>
          <li class="experience-section-items_position-items_company-and-type-of-employment"><span class="experience-section-items_position-items_company-and-type-of-employment-element">The Best Company - Freelance</span></li>
          <li class="experience-section-items_position-items_location"><span class="experience-section-items_position-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="experience-section-items_position-items_time"><span class="experience-section-items_position-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="experience-section-items_position-items_description"><p class="experience-section-items_position-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="experience-section-items_position-items_skills"><router-link to="/path-to-all-experiences-of-user-with-scrollable-thing" class="experience-section-items_position-items_skills-link"><the-skills></the-skills></router-link></li>
        </ul>
        <div class="experience-section-items_position-divider"></div>
      </li>
      <li class="experience-section-items_position">
        <ul class="experience-section-items_position-items">
          <li class="experience-section-items_position-items_title">
            <ul class="experience-section-items_position-items_title-items">
              <li class="experience-section-items_position-items_title-items_name"><router-link to="/path-to-all-experiences-of-user-with-scrollable-thing" class="experience-section-items_position-items_title-items_name-link"><h2 class="experience-section-items_position-items_title-items_name-element">Technical department employee</h2></router-link></li>
              <li class="experience-section-items_position-items_title-items_time"><span class="experience-section-items_position-items_title-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
            </ul>
          </li>
          <li class="experience-section-items_position-items_company-and-type-of-employment"><span class="experience-section-items_position-items_company-and-type-of-employment-element">The Best Company - Freelance</span></li>
          <li class="experience-section-items_position-items_location"><span class="experience-section-items_position-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="experience-section-items_position-items_time"><span class="experience-section-items_position-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="experience-section-items_position-items_description"><p class="experience-section-items_position-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="experience-section-items_position-items_skills"><router-link to="/path-to-all-experiences-of-user-with-scrollable-thing" class="experience-section-items_position-items_skills-link"><the-skills></the-skills></router-link></li>
        </ul>
      </li>
    </ul>
    <router-link class="experience-section-items_show-all-positions-link" to="/path-to-list-of-positions-of-user">
      <div class="experience-section-items_show-all-positions">
        <span class="experience-section-items_show-all-positions-element">Show all positions</span>
        <img class="experience-section-items_show-all-positions-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon">
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import TheSkills from './TheSkills.vue';

export default defineComponent({
  components: {
    TheSkills
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.experience-section {
  margin: 0 0 30px 100px;
  padding: 50px 50px 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
    padding: 0;
    border: none;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_header {
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        margin-bottom: 14px;
      }

      &-items {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-content: center;

        &_incription {

          &-element {
            font-size: 24px;

            @media (max-width: $breakpoint) {
              font-size: 20px;
            }
          }
        }

        &_buttons {
          width: 100px;

          @media (max-width: $breakpoint) {
            width: 80px;
          }

          &-items {
            list-style: none;
            display: flex;
            justify-content: space-between;
            align-content: center;

            &_add {
              &-element {
                width: 30px;

                @media (max-width: $breakpoint) {
                  width: 24px;
                }
              }
            }

            &_edit {
              &-element {
                width: 30px;

                @media (max-width: $breakpoint) {
                  width: 24px;
                }
              }
            }
          }
        }
      }
    }

    &_position {
      
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

            &_time {
              
              @media (max-width: $breakpoint) {
                display: none;
              }

              &-element {
                font-size: 14px;
                color: $grayBorderColor;
              }
            }
          }
        }

        &_company-and-type-of-employment {

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
          display: none;

          @media (max-width: $breakpoint) {
            display: block;
          }

          &-element {
            font-size: 12px;
            color: $grayBorderColor;
          }
        }

        &_description {
          margin-top: 20px;

          @media (max-width: $breakpoint) {
            margin-top: 10px;
          }

          &-element {
            font-size: 14px;

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

    &_show-all-positions {
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
        border: none;
        margin: 0;
        padding: 0;
        display: flex;
        align-items: center;
        margin: 16px 0;
      }

      &-element {
        font-size: 14px;
      }

      &-icon-arrow-forward {
        display: none;
        margin-left: 8px;

        @media (max-width: $breakpoint) {
          display: inline;
        }
      }

      &:hover {
        cursor: pointer;
        background-color: #ececec;
      }

      &-link {
        color: #000000;
        text-decoration: none;
      }
    }
  }
}
</style>