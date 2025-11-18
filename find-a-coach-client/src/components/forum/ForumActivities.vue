<template>
  <div class="activities">
    <ul class="activities-items">
      <router-link v-for="(activity, index) in activities" :key="activity.id" :class="index != activities.length - 1 ? 'activities-items_activity-link' : 'activities-items_activity-link-last'" :to="`/forum/${activity.activityType.toLowerCase()}/${activity.id}`">
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
          <p class="activities-items_activity-description"  v-else>{{ trimmDescription(activity.description) }}</p>
        </li>
      </router-link>
    </ul>

    <div class="activities-load-more-activities" v-if="isMoreActivitiesLeft" @click="loadActivities">
      <button class="activities-load-more-activities-inscription">Load more activities</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted, watch } from "vue"

import LoadingSquare from '@/components/LoadingSquare.vue'
import useGetRecommendedActivities from '@/composables/forum/useGetRecommendedActivities'
import useGetFilteredActivities from '@/composables/forum/useGetFilteredActivities'
import { useRouter } from "vue-router"
import { useAuthenticationStore } from "@/stores/authentication"
import { ActivityOfActivitiesList } from "@/types/my-profile/activities/ActivityOfActivitiesList"
import useRelativeDate from "@/composables/forum/useRelativeDate"

export default defineComponent({
  props: {
    searchString: { 
      type: String, 
      required: false, 
      default: '' 
    }
  },
  components: { LoadingSquare },
  setup(props) {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()

    const activities = ref<ActivityOfActivitiesList[]>([])
    const page = ref<number>(1)
    const pageSize = 7
    const isMoreActivitiesLeft = ref<boolean>(true)
    const isLoading = ref<boolean>(false)

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

    const trimmDescription = (description: string) => {
      return description?.length > 400
        ? description.substring(0, 400) + '...'
        : description
    }

    async function loadActivities() {
      if (isLoading.value) return
      isLoading.value = true

      const result = await useGetRecommendedActivities(authenticationStore.userId, page.value, pageSize)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        router.push('/error-page')
        return
      }

      if (result.length < pageSize) {
        isMoreActivitiesLeft.value = false
      }

      activities.value.push(...result)
      page.value++
      isLoading.value = false
    }

    async function loadFilteredActivities() {
      if (isLoading.value) return
      isLoading.value = true

      const result = await useGetFilteredActivities(props.searchString, page.value, pageSize)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        router.push('/error-page')
        return
      }

      if (result.length < pageSize) {
        isMoreActivitiesLeft.value = false
      }

      activities.value.push(...result)
      page.value++
      isLoading.value = false
    }

    async function refreshActivities() {
      activities.value = []
      page.value = 1
      isMoreActivitiesLeft.value = true

      if (props.searchString.trim() === '') {
        await loadActivities()
      } else {
        await loadFilteredActivities()
      }
    }

    watch(() => props.searchString, async () => {
      await refreshActivities()
    })

    onMounted(() => {
      loadActivities()
    })

    return { 
      activities, 
      activitiesInscriptions, 
      trimmDescription,
      formattedDates, 
      isMoreActivitiesLeft, 
      loadActivities,
      loadFilteredActivities,
      isLoading
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.activities {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
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
            border-radius: 50%;
            border: 1px solid #000000;

            @media (max-width: $breakpoint) {
              width: 30px;
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
        margin-bottom: 30px;

        @media (max-width: $breakpoint) {
          margin-bottom: 20px;
        }
      }

      &-link-last {
        color: #000000;
        text-decoration: none;
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