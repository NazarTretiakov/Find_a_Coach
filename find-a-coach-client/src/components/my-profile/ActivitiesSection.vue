<template>
  <div class="activities-section">
    <ul class="activities-section-items">
      <li class="activities-section-items_header">
        <ul class="activities-section-items_header-items">
          <li class="activities-section-items_header-items_incription"><h1 class="activities-section-items_header-items_incription-element">Activities</h1></li>
          <li class="activities-section-items_header-items_buttons">
            <ul class="activities-section-items_header-items_buttons-items">
              <li class="activities-section-items_header-items_buttons-items_add"><router-link to="/profile/add-activity" class="activities-section-items_header-items_buttons-items_add-link"><img src="../../assets/images/icons/add-icon.svg" alt="Add icon" class="activities-section-items_header-items_buttons-items_add-element"></router-link></li>
              <li class="activities-section-items_header-items_buttons-items_edit"><router-link to="/profile/activities" class="activities-section-items_header-items_buttons-items_edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="activities-section-items_header-items_buttons-items_edit-element"></router-link></li>
            </ul>
          </li>
        </ul>
      </li>
      <li class="activities-section-items_activities">
        <event-card></event-card>
        <div class="activities-section-items_activities-divider"></div>
        <event-card></event-card>
      </li>
    </ul>
    <router-link class="activities-section-items_show-all-activities-link" to="/path-to-list-of-activities-of-user">
      <div class="activities-section-items_show-all-activities">
        <span class="activities-section-items_show-all-activities-element">Show all activities</span>
        <img class="activities-section-items_show-all-activities-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon">
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import EventCard from './EventCard.vue';

export default defineComponent({
  components: {
    EventCard
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.activities-section {
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
                transition: transform 0.3s ease;

                &:hover {
                  transform: scale(1.15);
                }

                @media (max-width: $breakpoint) {
                  width: 24px;
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
      }
    }

    &_activities {
      display: flex;
      flex-direction: row;
      justify-content: space-between;

      @media (max-width: $breakpoint) {
        flex-direction: column;
      }

      &-divider {
        height: 20px;
      }
    }

    &_show-all-activities {
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