<template>
  <div class="experience">
    <ul class="experience-header">
      <li class="experience-header_inscription">
        <h1 class="experience-header_inscription-element">Experience</h1>
      </li>
      <li class="experience-header_add-button">
        <router-link to="/my-profile/add-position" class="experience-header_add-button-link">
          <button class="experience-header_add-button-element">
            <svg class="experience-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
            </svg>
            <span class="experience-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>

    <ul class="experience-items">
      <li v-for="position in positions" :key="position.positionId" class="experience-items_position" :id="position.positionId">
        <ul class="experience-items_position-items">
          <li class="experience-items_position-items_title">
            <ul class="experience-items_position-items_title-items">
              <li class="experience-items_position-items_title-items_name">
                <h2 class="experience-items_position-items_title-items_name-element">{{ position.title }}</h2>
              </li>
              <li class="experience-items_position-items_title-items_edit">
                <router-link :to="`/my-profile/edit-position/${position.positionId}`">
                  <img class="experience-items_position-items_title-items_edit-element" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon" />
                </router-link>
              </li>
            </ul>
          </li>

          <li class="experience-items_position-items_company-and-type-of-employment">
            <span class="experience-items_position-items_company-and-type-of-employment-element">{{ position.companyName }} - {{ convertEmploymentType(position.employmentType) }}</span>
          </li>

          <li class="experience-items_position-items_location">
            <span class="experience-items_position-items_location-element">{{ position.location }}</span>
          </li>

          <li class="experience-items_position-items_time">
            <span class="experience-items_position-items_time-element">{{ formatDate(position.startDate) }} - {{ position.isCurrentlyWorkingHere ? 'Present' : formatDate(position.endDate) }} ({{ getDuration(position.startDate, position.endDate, position.isCurrentlyWorkingHere) }})</span>
          </li>

          <li class="experience-items_position-items_description">
            <p class="experience-items_position-items_description-element">{{ position.description }}</p>
          </li>

          <li class="experience-items_position-items_skills">
            <the-skills :skills="position.skillTitles"></the-skills>
          </li>
        </ul>

        <div class="experience-items_position-delete" @click="deletePosition(position.positionId)">
          <span class="experience-items_position-delete-inscription">Delete position</span>
        </div>
      </li>
    </ul>
  </div>
</template>


<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'

import { useRouter } from 'vue-router'
import { useAuthenticationStore } from '@/stores/authentication'
import useGetAllPositions from '@/composables/my-profile/experience/useGetAllPositions'
import TheSkills from '../TheSkills.vue'
import { Position } from '@/types/my-profile/experience/Position'
import useFormatToReadableDate from '@/composables/useFormatToReadableDate'
import useConvertEmploymentTypeToReadable from '@/composables/my-profile/experience/useConvertEmploymentTypeToReadable'
import useDeletePosition from '@/composables/my-profile/experience/useDeletePosition'

export default defineComponent({
  components: { TheSkills },
  setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const positions = ref<Position[]>([])

    async function loadPositions() {
      const result = await useGetAllPositions(authenticationStore.userId)

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

    function formatDate(date: Date | string | null): string {
      return useFormatToReadableDate(date?.toString() || '')
    }

    function getDuration(start: Date | string | null, end: Date | string | null, isCurrent: boolean): string {
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

    const deletePosition = async (positionId: string) => {
      const result = await useDeletePosition(positionId)
    
      if (!result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      positions.value = positions.value.filter(position => position.positionId !== positionId)
    }

    onMounted(() => loadPositions())

    return {
      positions,
      convertEmploymentType,
      formatDate,
      getDuration,
      deletePosition
    }
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

    &_position {
      margin-top: 30px;
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