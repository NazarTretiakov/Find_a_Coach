<template>  
  <div class="activities">
    <ul class="activities-header">
      <li class="activities-header_inscription">
        <h1 class="activities-header_inscription-element">Activities</h1>
      </li>
      <li class="activities-header_add-button">
        <router-link to="/my-profile/add-activity" class="activities-header_add-button-link">
          <button class="activities-header_add-button-element">
            <svg class="activities-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="activities-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>

    <ul class="activities-items">
      <router-link v-for="(activity, index) in activities" :key="activity.id" class="activities-items_activity-link" :to="`/my-profile/activities/${activity.activityType.toLowerCase()}/${activity.id}`">
        <li class="activities-items_activity">
          <ul class="activities-items_activity-header">
            <li class="activities-items_activity-header_user-info">
              <img class="activities-items_activity-header_user-info-profile-image" :src="activity.imagePathOfCreator" alt="User profile image">
              <span class="activities-items_activity-header_user-info-user-name">{{ activity.firstNameOfCreator }} {{ activity.lastNameOfCreator }}</span>
            </li>
            <li class="activities-items_activity-header_publication-time">
              <span class="activities-items_activity-header_publication-time-element">{{ formattedDates[index] }}</span>
            </li>
          </ul>

          <h1 class="activities-items_activity-title">{{ activity.title }}</h1>
          <span class="activities-items_activity-subjects">{{ activity.subjects.join(', ') }}</span>
          <span class="activities-items_activity-special-phrase">{{ activitiesInscriptions[index] }}</span>
          <img v-if="activity.imagePath" class="activities-items_activity-image" :src="activity.imagePath" alt="Image of the activity">
          <p class="activities-items_activity-description"  v-else>{{ activity.description }}</p>
        </li>
      </router-link>
    </ul>

    <div class="activities-load-more-activities" v-if="isMoreActivitiesLeft" @click="loadActivities">
      <button class="activities-load-more-activities-inscription">Load more activities</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue"

import LoadingSquare from '@/components/LoadingSquare.vue'

import useGetActivitiesList from '@/composables/my-profile/activities/useGetActivitiesList'
import { useRouter } from "vue-router"
import { ActivityOfActivitiesList } from "@/types/my-profile/activities/ActivityOfActivitiesList"
import useRelativeDate from "@/composables/forum/useRelativeDate"

export default defineComponent({
  components: { LoadingSquare },
  setup() {
    const router = useRouter()
    const activities = ref<ActivityOfActivitiesList[]>([])
    const page = ref<number>(1)
    const pageSize = 7
    const isMoreActivitiesLeft = ref<boolean>(true)

    const activitiesInscriptions = computed(() =>
      activities.value.map(activity => {
        switch (activity.activityType) {
          case "Event":
            return `Something special is happening - ${activity.firstNameOfCreator} is hosting an event!`
          case "Survey":
            return "Take a moment to share your thoughts in this survey!"
          case "QA":
            return `Help ${activity.firstNameOfCreator} by sharing your insights!`
          default:
            return `Check out the latest update from ${activity.firstNameOfCreator}!`
        }
      })
    )

    const formattedDates = computed(() => {
      return activities.value.map(activity => {
        const pubDate = new Date(activity.publicationDate)
        return useRelativeDate(pubDate)
      })
    })

    async function loadActivities() {
      const result = await useGetActivitiesList(page.value, pageSize)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        if (result.length < pageSize) {
          isMoreActivitiesLeft.value = false
        }
        activities.value.push(...result)
        page.value++
      }
    }

    onMounted(() => {
      loadActivities()
    })

    return { activities, activitiesInscriptions, formattedDates, isMoreActivitiesLeft, loadActivities }
  },
})

</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.activities {
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

    &_activity {
      padding: 0 50px 50px 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

      @media (max-width: $breakpoint) {
        padding: 0 30px 30px 30px;
      }

      &-header {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-items: center;
        border-bottom: 2px solid $grayBorderColor;
        margin: 0 -50px;
        padding: 16px 50px;

        @media (max-width: $breakpoint) {
          margin: 0 -30px;
          padding: 14px 30px;
        }

        &_user-info {
          display: flex;
          align-items: center;

          &-profile-image {
            width: 36px;
            margin-right: 10px;
            border-radius: 18px;
            border: 1px #000000 solid;

            @media (max-width: $breakpoint) {
              width: 30px;
              border-radius: 15px;
              margin-right: 8px;
            }
          }
          &-user-name {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_publication-time {
          &-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }

      &-title {
        font-size: 24px;
        margin-top: 30px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
          margin-top: 20px;
        }
      }
      &-subjects {
        font-size: 14px;
        display: block;
        color: $linkColor;
        margin-bottom: 20px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
          margin-bottom: 14px;
        }
      }
      &-special-phrase {
        font-size: 14px;
        display: block;
        color: $linkColor;
        margin-bottom: 20px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
      &-image {
        width: 100%;
      }
      &-description {
        font-size: 14px;
        margin-top: 10px;
        white-space: pre-wrap;

        @media (max-width: $breakpoint) {
          font-size: 12px;
          margin-top: 6px;
        }
      }

      &-link {
        color: #000000;
        text-decoration: none;
        margin-top: 30px;

        @media (max-width: $breakpoint) {
          margin-top: 20px;
        }
      }
    }
  }

  &-load-more-activities {
    display: flex;
    justify-content: center;
    align-items: center;
    border: 2px $grayBorderColor solid;
    border-radius: 20px;
    height: 50px;
    transition: background-color 0.3s ease;
    margin-top: 30px;

    @media (max-width: $breakpoint) {
      margin-top: 20px;
    }

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