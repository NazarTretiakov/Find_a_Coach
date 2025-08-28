<template> 
  <div class="experience">
    <ul class="experience-header">
      <li class="experience-header_inscription">
        <h1 class="experience-header_inscription-element">Experience</h1>
      </li>
    </ul>
    <ul class="experience-items">
      <li class="experience-items_position">
        <ul class="experience-items_position-items">
          <li class="experience-items_position-items_title">
            <ul class="experience-items_position-items_title-items">
              <li class="experience-items_position-items_title-items_name"><h2 class="experience-items_position-items_title-items_name-element">Technical department employee</h2></li>
            </ul>
          </li>
          <li class="experience-items_position-items_company-and-type-of-employment"><span class="experience-items_position-items_company-and-type-of-employment-element">The Best Company - Freelance</span></li>
          <li class="experience-items_position-items_location"><span class="experience-items_position-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="experience-items_position-items_time"><span class="experience-items_position-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="experience-items_position-items_description"><p class="experience-items_position-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="experience-items_position-items_skills"><the-skills></the-skills></li>
        </ul>
      </li>
            <li class="experience-items_position">
        <ul class="experience-items_position-items">
          <li class="experience-items_position-items_title">
            <ul class="experience-items_position-items_title-items">
              <li class="experience-items_position-items_title-items_name"><h2 class="experience-items_position-items_title-items_name-element">Technical department employee</h2></li>
            </ul>
          </li>
          <li class="experience-items_position-items_company-and-type-of-employment"><span class="experience-items_position-items_company-and-type-of-employment-element">The Best Company - Freelance</span></li>
          <li class="experience-items_position-items_location"><span class="experience-items_position-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="experience-items_position-items_time"><span class="experience-items_position-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="experience-items_position-items_description"><p class="experience-items_position-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="experience-items_position-items_skills"><the-skills></the-skills></li>
        </ul>
      </li>
    </ul>
    <div class="experience-load-more-experience">
      <span class="experience-load-more-experience-inscription">Load more experience</span>
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

.experience {
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

    &_position {
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
          display: block;

          &-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
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
    }
  }

  &-load-more-experience  {
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