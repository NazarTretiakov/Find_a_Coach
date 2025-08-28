<template> 
  <div class="projects">
    <ul class="projects-header">
      <li class="projects-header_inscription">
        <h1 class="projects-header_inscription-element">Projects</h1>
      </li>
    </ul>
    <ul class="projects-items">
      <li class="projects-items_project">
        <ul class="projects-items_project-items">
          <li class="projects-items_project-items_title">
            <ul class="projects-items_project-items_title-items">
              <li class="projects-items_project-items_title-items_name"><h2 class="projects-items_project-items_title-items_name-element">Pregląd regał magazynowych for Amazon company</h2></li>
            </ul>
          </li>
          <li class="projects-items_project-items_participated-with">
            <span class="projects-items_project-items_participated-with-element">Participated with: Ulia Malska, Ariana Popa, Grzegorz Brzędrzyszczykiewicz</span>
          </li>
          <li class="projects-items_project-items_type"><span class="projects-items_project-items_type-element">Job-related project</span></li>
          <li class="projects-items_project-items_location"><span class="projects-items_project-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="projects-items_project-items_time"><span class="projects-items_project-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="projects-items_project-items_description"><p class="projects-items_project-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="projects-items_project-items_skills"><the-skills></the-skills></li>
        </ul>
      </li>
      <li class="projects-items_project">
        <ul class="projects-items_project-items">
          <li class="projects-items_project-items_title">
            <ul class="projects-items_project-items_title-items">
              <li class="projects-items_project-items_title-items_name"><h2 class="projects-items_project-items_title-items_name-element">Pregląd regał magazynowych for Amazon company</h2></li>
            </ul>
          </li>
          <li class="projects-items_project-items_participated-with">
            <span class="projects-items_project-items_participated-with-element">Participated with: Ulia Malska, Ariana Popa, Grzegorz Brzędrzyszczykiewicz</span>
          </li>
          <li class="projects-items_project-items_type"><span class="projects-items_project-items_type-element">Job-related project</span></li>
          <li class="projects-items_project-items_location"><span class="projects-items_project-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="projects-items_project-items_time"><span class="projects-items_project-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="projects-items_project-items_description"><p class="projects-items_project-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="projects-items_project-items_skills"><the-skills></the-skills></li>
        </ul>
      </li>
    </ul>
    <div class="projects-load-more-projects">
      <span class="projects-load-more-projects-inscription">Load more projects</span>
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

.projects {
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

    &_project {
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

              &-element {
                font-size: 20px;

                @media (max-width: $breakpoint) {
                  font-size: 16px;
                }
              }
            }
          }
        }

        &_participated-with {

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_type {

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
        }
      }
    }
  }

  &-load-more-projects {
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