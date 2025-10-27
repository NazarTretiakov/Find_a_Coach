<template> 
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="qa">
    <ul class="qa-header">
      <li class="qa-header_user-info">
        <router-link :to="`/user-profile/${qa.userId}`" class="qa-header_user-info-link">
          <img class="qa-header_user-info-profile-image" :src="qa.userImagePath" alt="User profile image">
          <span class="qa-header_user-info-user-name">{{ qa.userFirstName }} {{ qa.userLastName }}</span>
        </router-link>  
      </li>
      <li class="qa-header_publication-time">
        <span class="qa-header_publication-time-element">{{ formatDate(qa.createdAt) }}</span>
      </li>
    </ul>

    <h1 class="qa-title">{{ qa.title }}</h1>
    <span class="qa-subjects">{{ qa.subjects?.join(', ') }}</span>

    <div class="qa-card">
      <img v-if="qa.imagePath" class="qa-card-image" :src="qa.imagePath" alt="Image of the qa">
      <div class="qa-card-right-side">
        <p class="qa-card-right-side-description">{{ qa.description }}</p>
        <show-activity-button class="qa-card-right-side-button" @click="openActivityPage"></show-activity-button>
      </div>
    </div>
    
    <h2 class="qa-answers-header">Answers</h2>

    <ul class="qa-answers">
      <li v-for="(answer, index) in answers" :key="index" class="qa-answers_answer">
        <ul class="qa-answers_answer-header">
          <li class="qa-answers_answer-header_left-side">
            <router-link :to="`/user-profile/${answer.creatorId}`" class="qa-answers_answer-header_left-side-user-name-link">
              <img class="qa-answers_answer-header_left-side-user-profile-image" :src="answer.creatorProfileImagePath" alt="User profile photo">
              <span class="qa-answers_answer-header_left-side-user-name">{{ answer.creatorFirstName }} {{ answer.creatorLastName }}</span>
            </router-link>
          </li>
          <li class="qa-answers_answer-header_right-side">
            <span class="qa-answers_answer-header_right-side-time-of-publication">{{ formatDate(answer.dateOfCreation) }}</span>
          </li>
        </ul>
        <p class="qa-answers_answer-content">{{ answer.content }}</p>
        <div v-if="index !== answers.length - 1" class="qa-answers_answer-divider"></div>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue"
import LoadingSquare from "@/components/LoadingSquare.vue"
import ShowActivityButton from "./ShowActivityButton.vue"

import { useRouter } from "vue-router"
import useRelativeDate from "@/composables/forum/useRelativeDate"
import type { QA } from '@/types/forum/QA'
import useGetqa from "@/composables/forum/useGetQA"
import { useAuthenticationStore } from "@/stores/authentication"
import useGetQAAnswers from '@/composables/forum/useGetQAAnswers'
import { QAAnswer } from '@/types/forum/QAAnswer'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    LoadingSquare,
    ShowActivityButton
  },
  setup(props) {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()
    const qa = ref<QA>({} as QA)
    const isLoading = ref(true)
    const answers = ref<QAAnswer[]>([] as QAAnswer[])

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetqa(props.id)
      const answersResult = await useGetQAAnswers(props.id)

      if ("isSuccessful" in result && !result.isSuccessful && "isSuccessful" in answersResult && !answersResult.isSuccessful) {
        router.push("/error-page")
      } else {
        qa.value = result as QA
        answers.value = answersResult as QAAnswer[]
        if (authenticationStore.userId != qa.value.userId) {
          router.push(`/forum/qa/${qa.value.id}`)
        }
        console.log(answers.value)
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
        path: `/my-profile/activities/qa/${props.id}`
      })
    }

    const formatDate = (date: string) => {
      return useRelativeDate(date)
    }

    return { formatDate, openActivityPage, isLoading, qa, answers }
  }
});
</script>

<style lang="scss" scoped>
@use "../../../../assets/styles/config" as *;

.qa {
  margin: 50px 0 100px 100px;
  padding: 0 50px 50px 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
    padding: 0 30px;
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

  &-card {
    display: flex;
    border-bottom: 2px solid $grayBorderColor;
    padding: 0 50px 50px 50px;
    margin: 0 -50px 0 -50px;
    
    @media (max-width: $breakpoint) {
      flex-direction: column;
      padding: 0 30px 50px 30px;
      margin: 0 -30px 0 -30px;
    }

    &-image {
      width: 40%;
      margin-right: 50px;

      @media (max-width: $breakpoint) {
        width: 100%;
      }
    }

    &-right-side {

      &-description {
        font-size: 14px;
        margin: 20px 0 30px 0;
        width: 100%;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }

  &-answers-header {
    font-size: 20px;
    margin-top: 40px;

    @media (max-width: $breakpoint) {
      font-size: 16px;
    }
  }

  &-answers {
    list-style: none;
    display: flex;
    flex-direction: column;
    margin-top: 30px;

    &_answer {

      &-header {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-items: center;

        &_left-side {
          display: flex;
          align-items: center;

          &-user-profile-image {
            width: 30px;
            margin-right: 10px;
            border-radius: 50%;
            border: 1px solid #000000;
          }

          &-user-name {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }

            &-link {
              display: flex;
              align-items: center;
              color: black;
              text-decoration: none;
              
              &:hover {
                text-decoration: underline;
              }
            }
          }
        }

        &_right-side {

          &-time-of-publication {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }

      &-content {
        font-size: 14px;
        margin-top: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }

      &-divider {
        border-bottom: 1px solid $grayBorderColor;
        height: 24px;
        margin-bottom: 24px;
      }
    }
  }
}
</style>