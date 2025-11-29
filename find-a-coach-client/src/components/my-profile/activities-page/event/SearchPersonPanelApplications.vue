<template> 
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="event">
    <ul class="event-header">
      <li class="event-header_user-info">
        <router-link :to="`/user-profile/${event.userId}`" class="event-header_user-info-link">
          <img class="event-header_user-info-profile-image" :src="event.userImagePath" alt="User profile image">
          <span class="event-header_user-info-user-name">{{ event.userFirstName }} {{ event.userLastName }}</span>
        </router-link>  
      </li>
      <li class="event-header_publication-time">
        <span class="event-header_publication-time-element">{{ formatDate(event.createdAt) }}</span>
      </li>
    </ul>

    <h1 class="event-title">{{ event.title }}</h1>
    <span class="event-subjects">{{ event.subjects?.join(', ') }}</span>

    <div class="event-position">
      <ul class="event-position-header">
        <li class="event-position-header_left-side">
          <img class="event-position-header_left-side-icon" src="@/assets/images/icons/position-icon.svg" alt="Position icon">
          <span class="event-position-header_left-side-name">{{ panel?.positionName }}</span>
        </li>
      </ul>

      <the-skills v-if="panel?.preferredSkills && panel.preferredSkills.length > 0" :skills="panel.preferredSkills" class="event-position-skills" />

      <span class="event-position-payment">Payment: {{ panel?.payment || 'Unpaid' }}</span>

      <p class="event-position-description">{{ panel?.description }}</p>

      <show-activity-button @click="openActivityPage()" class="event-position-button" />
    </div>
    
    <h2 class="event-applications-header">Applications</h2>

    <ul class="event-applications-cards-items">
      <router-link v-for="(application, index) in eventApplications" :key="index" :to="`/user-profile/${application.userId}`" class="event-applications-cards-items_person-link">
        <li class="event-applications-cards-items_person">
          <img class="event-applications-cards-items_person-image" :src="application.userProfileImagePath" alt="User profile image">
          <div class="event-applications-cards-items_person-info">
            <h3 class="event-applications-cards-items_person-info-name">{{ application.userFirstName}} {{ application.userLastName }}</h3>
            <p class="event-applications-cards-items_person-info-paragraph">{{ application.userHeadline }}</p>
          </div>
        </li>
      </router-link>
    </ul>
    
    <div v-if="!isLoading && eventApplications.length == 0" class="event-applications-cards-items_empty-state">
      <img class="event-applications-cards-items_empty-state-icon" src="@/assets/images/icons/empty-state-icon.svg" alt="Empty state icon">
      <span class="event-applications-cards-items_empty-state-inscription">That search person panel has no applications yet.</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue"
import LoadingSquare from "@/components/LoadingSquare.vue"
import ShowActivityButton from "./ShowActivityButton.vue"
import TheSkills from '@/components/my-profile/TheSkills.vue'

import { useRouter } from "vue-router"
import useRelativeDate from "@/composables/forum/useRelativeDate"
import type { Event } from '@/types/forum/Event'
import useGetEvent from "@/composables/forum/useGetEvent"
import { useAuthenticationStore } from "@/stores/authentication"
import useGetSearchPersonPanelApplications from '@/composables/forum/useGetSearchPersonPanelApplications'
import type { EventApplication } from '@/types/forum/EventApplication'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    },
    panelId: {
      type: String,
      required: true
    }
  },
  components: {
    LoadingSquare,
    ShowActivityButton,
    TheSkills
  },
  setup(props) {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()
    const event = ref<Event>({} as Event)
    const panel = ref<any>(null)
    const isLoading = ref(true)
    const eventApplications = ref<EventApplication[]>([] as EventApplication[])

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetEvent(props.id)
      const eventApplicationsResult = await useGetSearchPersonPanelApplications(props.panelId)

      if ("isSuccessful" in result && !result.isSuccessful && "isSuccessful" in eventApplicationsResult && !eventApplicationsResult.isSuccessful) {
        router.push("/error-page")
      } else {
        event.value = result as Event
        eventApplications.value = eventApplicationsResult as EventApplication[]
        panel.value = event.value.searchPersonPanels.find(p => p.id === props.panelId)
        if (authenticationStore.userId != event.value.userId) {
          router.push(`/forum/event/${event.value.id}`)
        }
        console.log(eventApplications.value)
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) {
        setTimeout(() => { isLoading.value = false }, remaining)
      } else {
        isLoading.value = false
      }
    })

    const openActivityPage = () => {
      router.push({
        path: `/my-profile/activities/event/${props.id}`
      })
    }

    const formatDate = (date: string) => {
      return useRelativeDate(date)
    }

    return { formatDate, panel, openActivityPage, isLoading, event, eventApplications }
  }
});
</script>

<style lang="scss" scoped>
@use "../../../../assets/styles/config" as *;

.event {
  margin: 50px 0 100px 100px;
  padding: 0 50px 50px 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
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

      &-link {
        display: flex;
        align-items: center;
        color: black;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }

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

  &-position {
    border: 2px solid $grayBorderColor;
    padding: 10px 20px 20px 20px;
    border-radius: 12px;
    margin-top: 10px;
    transition: all 0.3s ease;

    &-header {
      list-style: none;
      display: flex;
      justify-content: space-between;
      align-items: center;

      &_left-side {
        display: flex;
        justify-content: space-between;
        align-items: center;

        &-icon {
          width: 30px;
          margin-right: 10px;

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }

        &-name {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }
    }

    &-skills {
      display: block;
      margin-top: 20px;
    }
    &-payment {
      font-size: 14px;
      display: block;
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-description {
      display: block;
      font-size: 14px;
      margin-top: 10px;
      white-space: pre-wrap;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-button {
      margin-top: 20px;
    }
  }

  &-applications-header {
    font-size: 20px;
    margin: 40px 0 20px 0;

    @media (max-width: $breakpoint) {
      font-size: 16px;
    }
  }

  &-applications-cards {
    margin: 50px 0 30px 100px;
    padding: 50px;
    border: 2px $grayBorderColor solid;
    border-radius: 20px;

    @media (max-width: $breakpoint) {
      margin: 30px 10px 30px 10px;
      padding: 30px;
    }

    &-header {
      font-size: 24px;
      margin: 0 0 30px 6px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-items {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 20px;
      list-style: none;
      padding: 0;

      @media (max-width: $breakpoint) {
        grid-template-columns: 1fr;
      }

      &_person {
        height: 150px;
        width: 100%;
        max-width: 400px;
        padding: 20px;
        border: 2px $grayBorderColor solid;
        border-radius: 20px;
        display: flex;
        align-items: center;
        background: #fff;

        @media (max-width: $breakpoint) {
          height: 100px;
        }

        &-image {
          width: 50px;
          margin: -6px 10px 0 0;
          border: 1px solid #000000;
          border-radius: 50%;

          @media (max-width: $breakpoint) {
            width: 40px;
          }
        }

        &-info {
          display: flex;
          flex-direction: column;

          &-name {
            font-size: 16px;
            margin: 0 0 6px 0;

            @media (max-width: $breakpoint) {
              font-size: 14px;
            }
          }

          &-paragraph {
            font-size: 14px;
            margin: 0;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
        &-link {
          color: #000000;
          text-decoration: none;
        }
      }

      &_empty-state {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;

        &-icon {
          width: 50px;
          margin-bottom: 14px;

          @media (max-width: $breakpoint) {
            width: 40px;
            margin-bottom: 10px;
          }
        }

        &-inscription {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            margin-bottom: 30px;
            font-size: 12px;
          }
        }
      }
    }
  }
}
</style>