<template>
  <router-link :to="`/activities/${activity.id}`" class="event-link">
    <div class="event">
      <ul class="event-items">
        <li class="event-items_header">
          <ul class="event-items_header-items">
            <li class="event-items_header-items_user">
              <img :src="activity.imageNameOfCreator" alt="User profile image" class="event-items_header-items_user-icon">
              <h1 class="event-items_header-items_user-name">{{ activity.firstNameOfCreator }} {{ activity.lastNameOfCreator }}</h1>
            </li>
            <li class="event-items_header-items_date-of-publication">
              {{ formattedDate }}
            </li>
          </ul>
        </li>
        <li class="event-items_title">
          <h2 class="event-items_title-element">{{ activity.title }}</h2>
        </li>
        <li class="event-items_incription">
          <span class="event-items_incription-element">{{ inscription }}</span>
        </li>
        <li class="event-items_paragraph">
          <p class="event-items_paragraph-element">{{ trimmedDescription }}</p>
        </li>
      </ul>
    </div>
  </router-link>
</template>

<script lang="ts">
import { defineComponent, computed, type PropType } from 'vue'
import type { ActivityCard } from '@/types/my-profile/activities/ActivityCard'

export default defineComponent({
  name: 'ActivityCard',
  props: {
    activity: {
      type: Object as PropType<ActivityCard>,
      required: true
    }
  },
  setup(props) {
    const pubDate = computed(() => new Date(props.activity.publicationDate))
    
    const formattedDate = computed(() => {
      const now = new Date()
      const diffMs = now.getTime() - pubDate.value.getTime()
      const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24))
      
      if (diffDays < 7) return diffDays === 0 ? 'Today' : `${diffDays} day${diffDays > 1 ? 's' : ''} ago`
      const diffWeeks = Math.floor(diffDays / 7)
      if (diffWeeks < 4) return `${diffWeeks} week${diffWeeks > 1 ? 's' : ''} ago`
      const diffMonths = Math.floor(diffDays / 30)
      return `${diffMonths} month${diffMonths > 1 ? 's' : ''} ago`
    })
    
    const trimmedDescription = computed(() => {
      return props.activity.description?.length > 400
        ? props.activity.description.substring(0, 400) + '...'
        : props.activity.description
    })
    
    const inscription = computed(() => {
      const activityType = props.activity.activityType

      if (activityType == "Event") {
        return `Something special is happening - ${props.activity.firstNameOfCreator} is hosting an event!`
      } else if (activityType == "Survey") {
        return "Take a moment to share your thoughts in this survey!"
      } else if (activityType == "QA") {
        return `Help ${props.activity.firstNameOfCreator} by sharing your insights!`
      } else {
        return `Check out the latest update from ${props.activity.firstNameOfCreator}!`
      }
    })
    
    return { formattedDate, trimmedDescription, inscription }
  }
})
</script>


<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.event {
  width: 400px;
  padding: 14px 40px 20px 40px;
  border: 2px solid $grayBorderColor;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    width: 100%;
    padding: 14px 36px 20px 36px;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-content: center;

    &_header {
      border-bottom: 2px solid $grayBorderColor;
      margin: 0 -40px;
      padding: 0 40px 14px 40px;

      @media (max-width: $breakpoint) {
        margin: 0 -38px 0 -37px; 
        padding: 0 36px 14px 36px; 
      }

      &-items {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-items: center;

        &_user {
          display: flex;
          align-items: center;

          &-icon {
            width: 40px;
            border-radius: 20px;
            border: 1px #000000 solid;

            @media (max-width: $breakpoint) {
              width: 30px;
            }
          }

          &-name {
            font-size: 14px;
            margin-left: 10px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_date-of-publication {
          font-size: 14px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }
    }

    &_title {
      margin-top: 20px;

      &-element {
        font-size: 16px;

        @media (max-width: $breakpoint) {
          font-size: 14px;
        }
      }
    }

    &_incription {
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        margin-top: 6px;
      }

      &-element {
        font-size: 14px;
        color: $linkColor;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }

    &_paragraph {
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        margin-top: 6px;
      }

      &-element {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }

  &-link {
    color: #000000;
    text-decoration: none;
  }
}

</style>