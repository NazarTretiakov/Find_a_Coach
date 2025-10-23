<template>
  <div class="experience">
    <ul class="experience-header">
      <li class="experience-header_inscription">
        <h1 class="experience-header_inscription-element">Experience</h1>
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
      </li>
    </ul>
  </div>
</template>


<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'

import { useRouter } from 'vue-router'
import useGetAllPositions from '@/composables/my-profile/experience/useGetAllPositions'
import TheSkills from '../TheSkills.vue'
import { Position } from '@/types/my-profile/experience/Position'
import useFormatToReadableDate from '@/composables/useFormatToReadableDate'
import useConvertEmploymentTypeToReadable from '@/composables/my-profile/experience/useConvertEmploymentTypeToReadable'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  components: { TheSkills },
  setup(props) {
    const router = useRouter()
    const positions = ref<Position[]>([])

    async function loadPositions() {
      const result = await useGetAllPositions(props.id)

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
      margin-top: 30px;
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