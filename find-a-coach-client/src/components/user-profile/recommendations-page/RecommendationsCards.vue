<template>
  <div class="recommendations">
    <ul class="recommendations-header">
      <li class="recommendations-header_inscription">
        <h1 class="recommendations-header_inscription-element">Recommendations</h1>
        <ul class="recommendations-header_type-of-recommendations">
          <li class="recommendations-header_type-of-recommendations_received">
            <span @click="toggleRecommendationType('received')" class="recommendations-header_type-of-recommendations_received-element" :class="isReceivedRecommendationsVisible ? 'recommendations-header_type-of-recommendations_received-element-active' : 'recommendations-header_type-of-recommendations_received-element-unactive'">Received</span>
          </li>
          <li class="recommendations-header_type-of-recommendations_given">
            <span @click="toggleRecommendationType('given')" class="recommendations-header_type-of-recommendations_given-element" :class="isGivenRecommendationsVisible ? 'recommendations-header_type-of-recommendations_received-element-active' : 'recommendations-header_type-of-recommendations_received-element-unactive'">Given</span>
          </li>
        </ul>
      </li>
    </ul>

    <ul class="recommendations-items">
      <li v-for="recommendation in visibleRecommendations" :key="recommendation.recommendationId" class="recommendations-items_recommendation">
        <ul class="recommendations-items_recommendation-items">
          <li class="recommendations-items_recommendation-items_recommender-info">
            <router-link :to="`/user-profile/${recommendation.recommenderUserId}`" class="recommendations-items_recommendation-items_recommender-info-link">
              <ul class="recommendations-items_recommendation-items_recommender-info-items">
                <li class="recommendations-items_recommendation-items_recommender-info-items-profile-image"><img class="recommendations-items_recommendation-items_recommender-info-items-profile-image-element" :src="recommendation.recommenderUserProfileImagePath || '/default-profile.png'" alt="User profile image"/></li>
                <li class="recommendations-items_recommendation-items_recommender-info-items-profile-description">
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-name">{{ recommendation.recommenderUserFirstName }} {{ recommendation.recommenderUserLastName }}</span>
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-incription">{{ recommendation.recommenderUserHeadline }}</span>
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-date-of-recommendation">{{ formatDate(recommendation.dateOfCreation) }}</span>
                </li>
              </ul>
            </router-link>
          </li>
          <li class="recommendations-items_recommendation-items_content"><p class="recommendations-items_recommendation-items_content-element">{{ recommendation.content }}</p></li>
        </ul>
      </li>

      <div class="recommendations-items_empty-state" v-if="!visibleRecommendations || visibleRecommendations.length == 0">
        <span class="recommendations-items_empty-state-inscription" v-if="isGivenRecommendationsVisible">User hasn't given any recommendations yet.</span>
        <span class="recommendations-items_empty-state-inscription" v-else>User hasn't received any recommendations yet.</span>
      </div>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import type { Recommendation } from '@/types/my-profile/recommendations/Recommendation'
import useGetAllRecommendations from '@/composables/my-profile/recommendations/useGetAllRecommendations'
import useDeleteRecommendation from '@/composables/my-profile/recommendations/useDeleteRecommendation'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  setup(props) {
    const router = useRouter()

    const receivedRecommendations = ref<Recommendation[]>([])
    const givenRecommendations = ref<Recommendation[]>([])
    const isReceivedRecommendationsVisible = ref(true)
    const isGivenRecommendationsVisible = ref(false)

    const toggleRecommendationType = (type: 'received' | 'given') => {
      isReceivedRecommendationsVisible.value = type === 'received'
      isGivenRecommendationsVisible.value = type === 'given'
    }

    const visibleRecommendations = computed(() =>
      isReceivedRecommendationsVisible.value ? receivedRecommendations.value : givenRecommendations.value
    )

    const noRecommendationsMessage = computed(() =>
      isReceivedRecommendationsVisible.value
        ? "User hasn't received any recommendations yet."
        : "User hasn't given any recommendations yet."
    )

    const loadRecommendations = async () => {
      console.log(props.id)
      const result = await useGetAllRecommendations(props.id)
      if (typeof result === 'object' && 'isSuccessful' in result && !result.isSuccessful) {
        router.push('/error-page')
        return
      }
      const recommendations = result as Recommendation[]
      receivedRecommendations.value = recommendations.filter(r => r.recommendedUserId === props.id)
      givenRecommendations.value = recommendations.filter(r => r.recommenderUserId === props.id)
    }

    const formatDate = (date: string | null) => {
      if (!date) return ''
      return new Date(date).toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' })
    }

    const deleteRecommendation = async (recommendationId: string) => {
      const result = await useDeleteRecommendation(recommendationId)
    
      if (!result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      givenRecommendations.value = givenRecommendations.value.filter(recommendation => recommendation.recommendationId !== recommendationId)
    }

    onMounted(loadRecommendations)

    return {
      isReceivedRecommendationsVisible,
      isGivenRecommendationsVisible,
      toggleRecommendationType,
      visibleRecommendations,
      noRecommendationsMessage,
      formatDate,
      deleteRecommendation
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.recommendations {
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


    &_type-of-recommendations {
      width: 200px;
      list-style: none;
      display: flex;
      justify-content: space-between;
      align-content: center;
      margin-left: 6px;

      @media (max-width: $breakpoint) {
        width: 150px;
        margin-bottom: 20px;
      }

      &_received {

        &-element {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }

          &-active {
            font-weight: bold;
            text-decoration: underline;
          }
          &-unactive {
            font-weight: normal;
            text-decoration: none;

            &:hover {
              text-decoration: underline;
            }
          }
        }

        &:hover {
          cursor: pointer;
        }
      }

      &_given {

        &-element {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }

          &-active {
            font-weight: bold;
            text-decoration: underline;
          }
          &-unactive {
            font-weight: normal;
            text-decoration: none;

            &:hover {
              text-decoration: underline;
            }
          }
        }

        &:hover {
          cursor: pointer;
        }
      }
    }
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_recommendation {
      margin-top: 30px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;
      padding: 50px;
      
      @media (max-width: $breakpoint) {
        padding: 30px;
      }

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;

        &_recommender-info {

          &-items {
            list-style: none;
            display: flex;
            align-content: center;

            &-profile-image {
              margin: 6px 10px 0 0;

              &-element {
                width: 40px;
                border-radius: 50%;
                border: 1px solid #000000;

                @media (max-width: $breakpoint) {
                  width: 30px;
                }
              }
            }

            &-profile-description {
              display: flex;
              flex-direction: column;
              font-size: 14px;
              margin-bottom: 10px;

              @media (max-width: $breakpoint) {
                font-size: 12px;
                margin-bottom: 8px;
              }

              &-name {
                font-weight: bold;
              }

              &-date-of-recommendation {
                color: $grayBorderColor;
              }
            }
          }
          &-link {
            color: #000000;
            text-decoration: none;
          }
        }

        &_content {
          margin-left: 50px;

          @media (max-width: $breakpoint) {
            margin-left: 40px;
          }

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }
    }

    &_empty-state {
      display: flex;
      justify-content: center;
      align-items: center;
      margin-top: 100px;

      &-inscription {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }

  &-load-more-recommendations  {
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