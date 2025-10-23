<template>
  <div class="education-section">
    <ul class="education-section-items">
      <li class="education-section-items_header">
        <ul class="education-section-items_header-items">
          <li class="education-section-items_header-items_incription"><h1 class="education-section-items_header-items_incription-element">Education</h1></li>
        </ul>
      </li>

      <li v-for="(school, index) in schools" :key="school.schoolId" class="education-section-items_education">
        <ul class="education-section-items_education-items">
          <li class="education-section-items_education-items_title">
            <ul class="education-section-items_education-items_title-items">
              <li class="education-section-items_education-items_title-items_name"><router-link :to="`/user-profile/${id}/education/#${school.schoolId}`" class="education-section-items_education-items_title-items_name-link"><h2 class="education-section-items_education-items_title-items_name-element">{{ school.schoolName }}</h2></router-link></li>
              <li class="education-section-items_education-items_title-items_time"><span class="education-section-items_education-items_title-items_time-element">{{ formatDate(school.startDate) }} - {{ formatDate(school.endDate) }}</span></li>
            </ul>
          </li>
          <li class="education-section-items_education-items_academic-degree"><span class="education-section-items_education-items_academic-degree-element">{{ school.degree }}</span></li>
          <li class="education-section-items_education-items_field-of-study"><span class="education-section-items_education-items_field-of-study-element">{{ school.fieldOfStudy }}</span></li>
          <li class="education-section-items_education-items_location"><span class="education-section-items_education-items_location-element">{{ school.location }}</span></li>
          <li class="education-section-items_education-items_skills"><router-link :to="`/user-profile/${id}/education/#${school.schoolId}`" class="education-section-items_education-items_skills-link"><the-skills :skills="school.skillTitles"></the-skills></router-link></li>
        </ul>
        <div v-if="index !== schools.length - 1" class="education-section-items_education-divider"></div>
      </li>
    </ul>

    <router-link class="education-section-items_show-all-education-link" :to="`/user-profile/${id}/education`">
      <div class="education-section-items_show-all-education">
        <span class="education-section-items_show-all-education-element">Show all education</span>
        <img class="education-section-items_show-all-education-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon" />
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import TheSkills from './TheSkills.vue'
import { School } from '@/types/my-profile/education/School'
import useGetLastTwoSchools from '@/composables/my-profile/education/useGetLastTwoSchools'

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
    const schools = ref<School[]>([])

    async function loadSchools() {
      const result = await useGetLastTwoSchools(props.id)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        schools.value = result as School[]
      }
    }

    function formatDate(date: string | null): string {
      if (!date) return ''
      
      const formattedDate = new Date(date).toLocaleDateString("en-US", {
        year: "numeric"
      });

      return formattedDate
    }

    onMounted(() => loadSchools())

    return { schools, formatDate }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.education-section {
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

    &_education {
      
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

        &_academic-degree {

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_field-of-study{

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

    &_show-all-education {
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