<template>
  <div class="experience-section">
    <ul class="experience-section-items">

      <li class="experience-section-items_header">
        <ul class="experience-section-items_header-items">
          <li class="experience-section-items_header-items_incription"><h1 class="experience-section-items_header-items_incription-element">Experience</h1></li>
          <li class="experience-section-items_header-items_buttons">
            <ul class="experience-section-items_header-items_buttons-items">
              <li class="experience-section-items_header-items_buttons-items_add"><router-link to="/my-profile/add-position" class="experience-section-items_header-items_buttons-items_add-link"><img src="../../assets/images/icons/add-icon.svg" alt="Add icon" class="experience-section-items_header-items_buttons-items_add-element"></router-link></li>
              <li class="experience-section-items_header-items_buttons-items_edit"><router-link to="/my-profile/experience" class="experience-section-items_header-items_buttons-items_edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="experience-section-items_header-items_buttons-items_edit-element"></router-link></li>
            </ul>
          </li>
        </ul>
      </li>

      <li v-for="(position, index) in positions" :key="position.positionId" class="experience-section-items_position">
        <ul class="experience-section-items_position-items">
          <li class="experience-section-items_position-items_title">
            <ul class="experience-section-items_position-items_title-items">
              <li class="experience-section-items_position-items_title-items_name">
                <router-link :to="`/my-profile/experience/#${position.positionId}`" class="experience-section-items_position-items_title-items_name-link">
                  <h2 class="experience-section-items_position-items_title-items_name-element">{{ position.title }}</h2>
                </router-link>
              </li>
              <li class="experience-section-items_position-items_title-items_time">
                <span class="experience-section-items_position-items_title-items_time-element">
                  {{ formatDate(position.startDate) }} -
                  {{ position.isCurrentlyWorkingHere ? 'Present' : formatDate(position.endDate) }}
                  ({{ getDuration(position.startDate, position.endDate, position.isCurrentlyWorkingHere) }})
                </span>
              </li>
            </ul>
          </li>
          <li class="experience-section-items_position-items_company-and-type-of-employment">
            <span class="experience-section-items_position-items_company-and-type-of-employment-element">
              {{ position.companyName }} - {{ convertEmploymentType(position.employmentType) }}
            </span>
          </li>
          <li class="experience-section-items_position-items_location">
            <span class="experience-section-items_position-items_location-element">{{ position.location }}</span>
          </li>
          <li class="experience-section-items_position-items_description">
            <p class="experience-section-items_position-items_description-element" v-html="position.description.replace(/\n/g, '<br/>')"></p>
          </li>
          <li class="experience-section-items_position-items_skills">
            <router-link :to="`/my-profile/experience/#${position.positionId}`" class="experience-section-items_position-items_skills-link">
              <the-skills :skills="position.skillTitles"></the-skills>
            </router-link>
          </li>
        </ul>
        <div v-if="index != positions.length - 1" class="experience-section-items_position-divider"></div>
      </li>
    </ul>

    <router-link class="experience-section-items_show-all-positions-link" to="/my-profile/experience">
      <div class="experience-section-items_show-all-positions">
        <span class="experience-section-items_show-all-positions-element">Show all positions</span>
        <img class="experience-section-items_show-all-positions-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon">
      </div>
    </router-link>
  </div>
</template>


<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'

import TheSkills from './TheSkills.vue'

import { useRouter } from 'vue-router'
import { useAuthenticationStore } from '@/stores/authentication'
import { Position } from '@/types/my-profile/experience/Position'
import useFormatToReadableDate from '@/composables/useFormatToReadableDate'
import useConvertEmploymentTypeToReadable from '@/composables/my-profile/experience/useConvertEmploymentTypeToReadable'
import useGetLastTwoPositions from '@/composables/my-profile/experience/useGetAllPositions'

export default defineComponent({
  components: { TheSkills },
  setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const positions = ref<Position[]>([])

    async function loadPositions() {
      const result = await useGetLastTwoPositions(authenticationStore.userId)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        positions.value = result as Position[]
      }
    }

    function convertEmploymentType(employmentType: string): string {
      return useConvertEmploymentTypeToReadable(employmentType)
    }

    function formatDate(date: Date | null): string {
      return useFormatToReadableDate(date?.toString() || '')
    }

    function getDuration(start: Date | null, end: Date | null, isCurrent: boolean): string {
      if (!start) return ''
      const startDate = new Date(start)
      const endDate = isCurrent ? new Date() : end ? new Date(end) : new Date()
      const diff = endDate.getTime() - startDate.getTime()

      const months = Math.floor(diff / (1000 * 60 * 60 * 24 * 30))
      const years = Math.floor(months / 12)
      const remainingMonths = months % 12

      if (years > 0 && remainingMonths > 0) return `${years} yr ${remainingMonths} mon`
      if (years > 0) return `${years} yr`
      if (remainingMonths > 0) return `${remainingMonths} mon`
      return 'Less than 1 mon'
    }

    onMounted(() => loadPositions())

    return {
      positions,
      convertEmploymentType,
      formatDate,
      getDuration
    }
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