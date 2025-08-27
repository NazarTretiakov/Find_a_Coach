<template>
  <div class="certifications-section">
    <ul class="certifications-section-items">
      <li class="certifications-section-items_header">
        <ul class="certifications-section-items_header-items">
          <li class="certifications-section-items_header-items_incription"><h1 class="certifications-section-items_header-items_incription-element">Certifications</h1></li>
        </ul>
      </li>
      <li class="certifications-section-items_certification">
        <ul class="certifications-section-items_certification-items">
          <li class="certifications-section-items_certification-items_title">
            <ul class="certifications-section-items_certification-items_title-items">
              <li class="certifications-section-items_certification-items_title-items_name"><router-link to="/path-to-all-certifications-of-user-with-scrollable-thing" class="certifications-section-items_certification-items_title-items_name-link"><h2 class="certifications-section-items_certification-items_title-items_name-element">Complete JavaScript masterclass</h2></router-link></li>
              <li class="certifications-section-items_certification-items_title-items_issued"><span class="certifications-section-items_certification-items_title-items_issued-element">Issued Jan 2024</span></li>
            </ul>
          </li>
          <li class="certifications-section-items_certification-items_organization-that-issued"><span class="certifications-section-items_certification-items_organization-that-issued-element">Udemy</span></li>
          <li class="certifications-section-items_certification-items_issued"><span class="certifications-section-items_certification-items_issued-element">Issued Jan 2024</span></li>
          <li class="certifications-section-items_certification-items_source">
            <img class="certifications-section-items_certification-items_source-image" src="../../assets/images/certification-example.jpg" alt="Certification example">
            <span class="certifications-section-items_certification-items_source-id">Id: <router-link to="/link-to-certification" class="certifications-section-items_certification-items_source-id-link">UC-87c7cbd1-d1pr-76d9-cg88-49365454882b</router-link></span>
          </li>
          <li class="certifications-section-items_certification-items_skills"><router-link to="/path-to-all-certificationss-of-user-with-scrollable-thing" class="certifications-section-items_certification-items_skills-link"><the-skills></the-skills></router-link></li>
        </ul>
        <div class="certifications-section-items_certification-divider"></div>
      </li>
      <li class="certifications-section-items_certification">
        <ul class="certifications-section-items_certification-items">
          <li class="certifications-section-items_certification-items_title">
            <ul class="certifications-section-items_certification-items_title-items">
              <li class="certifications-section-items_certification-items_title-items_name"><router-link to="/path-to-all-certifications-of-user-with-scrollable-thing" class="certifications-section-items_certification-items_title-items_name-link"><h2 class="certifications-section-items_certification-items_title-items_name-element">Complete JavaScript masterclass</h2></router-link></li>
              <li class="certifications-section-items_certification-items_title-items_issued"><span class="certifications-section-items_certification-items_title-items_issued-element">Issued Jan 2024</span></li>
            </ul>
          </li>
          <li class="certifications-section-items_certification-items_organization-that-issued"><span class="certifications-section-items_certification-items_organization-that-issued-element">Udemy</span></li>
          <li class="certifications-section-items_certification-items_issued"><span class="certifications-section-items_certification-items_issued-element">Issued Jan 2024</span></li>
          <li class="certifications-section-items_certification-items_source">
            <img class="certifications-section-items_certification-items_source-image" src="../../assets/images/certification-example.jpg" alt="Certification example">
            <span class="certifications-section-items_certification-items_source-id">Id: <router-link to="/link-to-certification" class="certifications-section-items_certification-items_source-id-link">UC-87c7cbd1-d1pr-76d9-cg88-49365454882b</router-link></span>
          </li>
          <li class="certifications-section-items_certification-items_skills"><router-link to="/path-to-all-certificationss-of-user-with-scrollable-thing" class="certifications-section-items_certification-items_skills-link"><the-skills></the-skills></router-link></li>
        </ul>
      </li>
    </ul>
    <router-link class="certifications-section-items_show-all-certifications-link" to="/user-profile/certifications">
      <div class="certifications-section-items_show-all-certifications">
        <span class="certifications-section-items_show-all-certifications-element">Show all certifications</span>
        <img class="certifications-section-items_show-all-certifications-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon">
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

.certifications-section {
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
      }
    }

    &_certification {
      
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

            &_issued {
              
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
          display: none;
          
          @media (max-width: $breakpoint) {
            display: block;
          }

          &-element {
            font-size: 12px;
            color: $grayBorderColor;
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

    &_show-all-certifications {
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