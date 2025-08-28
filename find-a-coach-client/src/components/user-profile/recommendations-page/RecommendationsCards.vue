<template> 
  <div class="recommendations">
    <ul class="recommendations-header">
      <li class="recommendations-header_inscription">
        <h1 class="recommendations-header_inscription-element">Recommendations</h1>
        <ul class="recommendations-header_type-of-recommendations">
          <li class="recommendations-header_type-of-recommendations_received"><span @click="toggleRecommendationType('received')" class="recommendations-header_type-of-recommendations_received-element" :class="isReceivedRecommendationsVisible ? 'recommendations-header_type-of-recommendations_received-element-active' : 'recommendations-header_type-of-recommendations_received-element-unactive'">Received</span></li>
          <li class="recommendations-header_type-of-recommendations_given"><span @click="toggleRecommendationType('given')" class="recommendations-header_type-of-recommendations_given-element" :class="isGivenRecommendationsVisible ? 'recommendations-header_type-of-recommendations_received-element-active' : 'recommendations-header_type-of-recommendations_received-element-unactive'">Given</span></li>
        </ul>
      </li>
    </ul>
    <ul class="recommendations-items">
      <li class="recommendations-items_recommendation">
        <ul class="recommendations-items_recommendation-items">
          <li class="recommendations-items_recommendation-items_recommender-info">
            <router-link to="/path-to-user-profile" class="recommendations-items_recommendation-items_recommender-info-link">
              <ul class="recommendations-items_recommendation-items_recommender-info-items">
                <li class="recommendations-items_recommendation-items_recommender-info-items-profile-image"><img class="recommendations-items_recommendation-items_recommender-info-items-profile-image-element" src="../../../assets/images/icons/user-icon.jpg" alt="User profile image"></li>
                <li class="recommendations-items_recommendation-items_recommender-info-items-profile-description">
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-name">Matvii Tretiakov</span>
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-incription">Marketing Expert | Team Leader | General Manager</span>
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-date-of-recommendation">June 7, 2025</span>
                </li>
              </ul>
            </router-link>
          </li>
          <li class="recommendations-items_recommendation-items_content">
            <p class="recommendations-items_recommendation-items_content-element">Nazar is a highly motivated specialist in programming, problem solving, and creating innovative products. He consistently seeks out new challenges to expand his skills and deliver efficient, high-quality solutions. His strong analytical thinking and attention to detail make him a valuable asset to any development team.</p>
          </li>
        </ul>
      </li>
      <li class="recommendations-items_recommendation">
        <ul class="recommendations-items_recommendation-items">
          <li class="recommendations-items_recommendation-items_recommender-info">
            <router-link to="/path-to-user-profile" class="recommendations-items_recommendation-items_recommender-info-link">
              <ul class="recommendations-items_recommendation-items_recommender-info-items">
                <li class="recommendations-items_recommendation-items_recommender-info-items-profile-image"><img class="recommendations-items_recommendation-items_recommender-info-items-profile-image-element" src="../../../assets/images/icons/user-icon.jpg" alt="User profile image"></li>
                <li class="recommendations-items_recommendation-items_recommender-info-items-profile-description">
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-name">Matvii Tretiakov</span>
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-incription">Marketing Expert | Team Leader | General Manager</span>
                  <span class="recommendations-items_recommendation-items_recommender-info-items-profile-description-date-of-recommendation">June 7, 2025</span>
                </li>
              </ul>
            </router-link>
          </li>
          <li class="recommendations-items_recommendation-items_content">
            <p class="recommendations-items_recommendation-items_content-element">Nazar is a highly motivated specialist in programming, problem solving, and creating innovative products. He consistently seeks out new challenges to expand his skills and deliver efficient, high-quality solutions. His strong analytical thinking and attention to detail make him a valuable asset to any development team.</p>
          </li>
        </ul>
      </li>
    </ul>
    <div class="recommendations-load-more-recommendations">
      <span class="recommendations-load-more-recommendations-inscription">Load more recommendations</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import TheSkills from '../TheSkills.vue'

export default defineComponent({
  components: {
    TheSkills
  },
  setup() {
    const isReceivedRecommendationsVisible = ref<boolean>(true)
    const isGivenRecommendationsVisible = ref<boolean>(false)

    const toggleRecommendationType = async function(recommendationType: string) {
      if (recommendationType == "received" && isReceivedRecommendationsVisible.value == false || recommendationType == "given" && isGivenRecommendationsVisible.value == false) {
        isReceivedRecommendationsVisible.value = !isReceivedRecommendationsVisible.value;
        isGivenRecommendationsVisible.value = !isGivenRecommendationsVisible.value;
      }
    }

    return { isReceivedRecommendationsVisible, isGivenRecommendationsVisible, toggleRecommendationType }
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
    margin-bottom: 30px;

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
      margin-bottom: 30px;
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

        &_recommender-info {

          &-items {
            list-style: none;
            display: flex;
            align-content: center;

            &-profile-image {
              margin: 6px 10px 0 0;

              &-element {
                width: 40px;

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