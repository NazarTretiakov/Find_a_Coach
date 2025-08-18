<template> 
  <div class="certifications">
    <ul class="certifications-header">
      <li class="certifications-header_inscription">
        <h1 class="certifications-header_inscription-element">Certifications</h1>
      </li>
      <li class="certifications-header_add-button">
        <router-link to="/my-profile/add-certification" class="certifications-header_add-button-link">
          <button class="certifications-header_add-button-element">
            <svg class="certifications-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="certifications-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>
    <ul class="certifications-items">
      <li class="certifications-items_certification">
        <ul class="certifications-items_certification-items">
          <li class="certifications-items_certification-items_title">
            <ul class="certifications-items_certification-items_title-items">
              <li class="certifications-items_certification-items_title-items_name"><h2 class="certifications-items_certification-items_title-items_name-element">Complete JavaScript masterclass</h2></li>
              <li class="certifications-items_certification-items_title-items_issued"><router-link to="/my-profile/edit-certification"><img class="certifications-items_certification-items_title-items_edit-element" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link></li>
            </ul>
          </li>
          <li class="certifications-items_certification-items_organization-that-issued"><span class="certifications-items_certification-items_organization-that-issued-element">Udemy</span></li>
          <li class="certifications-items_certification-items_issued"><span class="certifications-items_certification-items_issued-element">Issued Jan 2024</span></li>
          <li class="certifications-items_certification-items_source">
            <img class="certifications-items_certification-items_source-image" src="../../../assets/images/certification-example.jpg" alt="Certification example">
            <span class="certifications-items_certification-items_source-id">Id: <router-link to="/link-to-certification" class="certifications-items_certification-items_source-id-link">UC-87c7cbd1-d1pr-76d9-cg88-49365454882b</router-link></span>
          </li>
          <li class="certifications-items_certification-items_skills"><the-skills></the-skills></li>
        </ul>
        <div class="certifications-items_certification-delete">
          <span class="certifications-items_certification-delete-inscription">Delete certification</span>
        </div>
      </li>
      <li class="certifications-items_certification">
        <ul class="certifications-items_certification-items">
          <li class="certifications-items_certification-items_title">
            <ul class="certifications-items_certification-items_title-items">
              <li class="certifications-items_certification-items_title-items_name"><h2 class="certifications-items_certification-items_title-items_name-element">Complete JavaScript masterclass</h2></li>
              <li class="certifications-items_certification-items_title-items_issued"><router-link to="/my-profile/edit-certification"><img class="certifications-items_certification-items_title-items_edit-element" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon"></router-link></li>
            </ul>
          </li>
          <li class="certifications-items_certification-items_organization-that-issued"><span class="certifications-items_certification-items_organization-that-issued-element">Udemy</span></li>
          <li class="certifications-items_certification-items_issued"><span class="certifications-items_certification-items_issued-element">Issued Jan 2024</span></li>
          <li class="certifications-items_certification-items_source">
            <img class="certifications-items_certification-items_source-image" src="../../../assets/images/certification-example.jpg" alt="Certification example">
            <span class="certifications-items_certification-items_source-id">Id: <router-link to="/link-to-certification" class="certifications-items_certification-items_source-id-link">UC-87c7cbd1-d1pr-76d9-cg88-49365454882b</router-link></span>
          </li>
          <li class="certifications-items_certification-items_skills"><the-skills></the-skills></li>
        </ul>
        <div class="certifications-items_certification-delete">
          <span class="certifications-items_certification-delete-inscription">Delete certification</span>
        </div>
      </li>
    </ul>
    <div class="certifications-load-more-certifications">
      <span class="certifications-load-more-certifications-inscription">Load more certifications</span>
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

.certifications {
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

    &_certification {
      margin-bottom: 30px;
      padding: 50px 50px 0 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

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

        &_organization-that-issued {
          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_source {
          display: flex;
          align-items: center;
          margin: 4px 0;

          &-image {
            width: 140px;
            margin-right: 20px;

            @media (max-width: $breakpoint) {
              width: 100px;
              margin-right: 14px;
            }
          }

          &-id {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }

            &-link {
              color: $linkColor;
              text-decoration: none;

              &:hover {
                text-decoration: underline;
              }
            }
          }
        }

        &_issued {

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

  &-load-more-certifications {
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
