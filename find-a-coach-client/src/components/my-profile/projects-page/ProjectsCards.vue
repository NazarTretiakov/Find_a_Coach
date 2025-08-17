<template> 
  <div class="projects">
    <ul class="projects-header">
      <li class="projects-header_inscription">
        <h1 class="projects-header_inscription-element">Projects</h1>
      </li>
      <li class="projects-header_add-button">
        <router-link to="/my-profile/add-project" class="projects-header_add-button-link">
          <button class="projects-header_add-button-element">
            <svg class="projects-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="projects-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>
    <ul class="projects-items">
      <li class="projects-items_project">
        <ul class="projects-items_project-items">
          <li class="projects-items_project-items_title">
            <ul class="projects-items_project-items_title-items">
              <li class="projects-items_project-items_title-items_name"><router-link to="/path-to-all-projects-of-user-with-scrollable-thing" class="projects-items_project-items_title-items_name-link"><h2 class="projects-items_project-items_title-items_name-element">Pregląd regał magazynowych for Amazon company</h2></router-link></li>
              <li class="projects-items_project-items_title-items_edit"><router-link to="/my-profile/edit-project"><img class="projects-items_project-items_title-items_edit-element" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link></li>
            </ul>
          </li>
          <li class="projects-items_project-items_participated-with">
            <span class="projects-items_project-items_participated-with-element">Participated with: Ulia Malska, Ariana Popa, Grzegorz Brzędrzyszczykiewicz</span>
          </li>
          <li class="projects-items_project-items_type"><span class="projects-items_project-items_type-element">Job-related project</span></li>
          <li class="projects-items_project-items_location"><span class="projects-items_project-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="projects-items_project-items_time"><span class="projects-items_project-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="projects-items_project-items_description"><p class="projects-items_project-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="projects-items_project-items_skills"><router-link to="/path-to-all-projectss-of-user-with-scrollable-thing" class="projects-items_project-items_skills-link"><the-skills></the-skills></router-link></li>
        </ul>
        <div class="projects-items_project-delete">
          <span class="projects-items_project-delete-inscription">Delete project</span>
        </div>
      </li>
      <li class="projects-items_project">
        <ul class="projects-items_project-items">
          <li class="projects-items_project-items_title">
            <ul class="projects-items_project-items_title-items">
              <li class="projects-items_project-items_title-items_name"><router-link to="/path-to-all-projects-of-user-with-scrollable-thing" class="projects-items_project-items_title-items_name-link"><h2 class="projects-items_project-items_title-items_name-element">Pregląd regał magazynowych for Amazon company</h2></router-link></li>
              <li class="projects-items_project-items_title-items_edit"><router-link to="/my-profile/edit-project"><img class="projects-items_project-items_title-items_edit-element" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link></li>
            </ul>
          </li>
          <li class="projects-items_project-items_participated-with">
            <span class="projects-items_project-items_participated-with-element">Participated with: Ulia Malska, Ariana Popa, Grzegorz Brzędrzyszczykiewicz</span>
          </li>
          <li class="projects-items_project-items_type"><span class="projects-items_project-items_type-element">Job-related project</span></li>
          <li class="projects-items_project-items_location"><span class="projects-items_project-items_location-element">Gdańsk, Woj. Pomorskie, Poland</span></li>
          <li class="projects-items_project-items_time"><span class="projects-items_project-items_time-element">Jan 2024 - Present (1 yr 6 mon)</span></li>
          <li class="projects-items_project-items_description"><p class="projects-items_project-items_description-element">- Projektowanie i implementacja logiki biznesowej z wykorzystaniem platformy .NET w kontekście systemu ERP enova365<br/>- Tworzenie serwisów REST API w platformie .NET do wykonania operacji CRUD na tabelach bazy danych systemu ERP enova365<br/>- Tworzenie dodatków w platformie .NET do realizacji specjalistycznych funkcji w systemie ERP enova365<br/>- Użycie JWT do uwierzytelniania i wymiany danych oraz Swaggera do testowania i dokumentowania tworzonych serwisów REST API</p></li>
          <li class="projects-items_project-items_skills"><router-link to="/path-to-all-projectss-of-user-with-scrollable-thing" class="projects-items_project-items_skills-link"><the-skills></the-skills></router-link></li>
        </ul>
        <div class="projects-items_project-delete">
          <span class="projects-items_project-delete-inscription">Delete project</span>
        </div>
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

    &_project {
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

        &_participated-with {

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }

            &-person-link {
              color: $linkColor;
              text-decoration: none;

              &:hover {
                text-decoration: underline;
              }
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