<template>
  <div class="recommendations-section">
    <ul class="recommendations-section-items">
      <li class="recommendations-section-items_header">
        <h1 class="recommendations-section-items_header-element">Recommendations</h1>
      </li>
      <li class="recommendations-section-items_type-of-recommendations">
        <ul class="recommendations-section-items_type-of-recommendations-items">
          <li class="recommendations-section-items_type-of-recommendations-items_received"><span @click="toggleRecommendationType('received')" class="recommendations-section-items_type-of-recommendations-items_received-element" :class="isReceivedRecommendationsVisible ? 'recommendations-section-items_type-of-recommendations-items_received-element-active' : 'recommendations-section-items_type-of-recommendations-items_received-element-unactive'">Received</span></li>
          <li class="recommendations-section-items_type-of-recommendations-items_given"><span @click="toggleRecommendationType('given')" class="recommendations-section-items_type-of-recommendations-items_given-element" :class="isGivenRecommendationsVisible ? 'recommendations-section-items_type-of-recommendations-items_received-element-active' : 'recommendations-section-items_type-of-recommendations-items_received-element-unactive'">Given</span></li>
        </ul>
      </li>
      <li class="recommendations-section-items_recommendation">
        <ul class="recommendations-section-items_recommendation-items">
          <li class="recommendations-section-items_recommendation-items_recommender-info">
            <router-link to="/path-to-user-profile" class="recommendations-section-items_recommendation-items_recommender-info-link">
              <ul class="recommendations-section-items_recommendation-items_recommender-info-items">
                <li class="recommendations-section-items_recommendation-items_recommender-info-items-profile-image"><img class="recommendations-section-items_recommendation-items_recommender-info-items-profile-image-element" src="../../assets/images/icons/user-icon.jpg" alt="User profile image"></li>
                <li class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description">
                  <span class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description-name">Matvii Tretiakov</span>
                  <span class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description-incription">Marketing Expert | Team Leader | General Manager</span>
                  <span class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description-date-of-recommendation">June 7, 2025</span>
                </li>
              </ul>
            </router-link>
          </li>
          <li class="recommendations-section-items_recommendation-items_content">
            <p class="recommendations-section-items_recommendation-items_content-element">Nazar is a highly motivated specialist in programming, problem solving, and creating innovative products. He consistently seeks out new challenges to expand his skills and deliver efficient, high-quality solutions. His strong analytical thinking and attention to detail make him a valuable asset to any development team.</p>
          </li>
        </ul>
        <div class="recommendations-section-items_recommendation-divider"></div>
      </li>
      <li class="recommendations-section-items_recommendation">
        <ul class="recommendations-section-items_recommendation-items">
          <li class="recommendations-section-items_recommendation-items_recommender-info">
            <router-link to="/path-to-user-profile" class="recommendations-section-items_recommendation-items_recommender-info-link">
              <ul class="recommendations-section-items_recommendation-items_recommender-info-items">
                <li class="recommendations-section-items_recommendation-items_recommender-info-items-profile-image"><img class="recommendations-section-items_recommendation-items_recommender-info-items-profile-image-element" src="../../assets/images/icons/user-icon.jpg" alt="User profile image"></li>
                <li class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description">
                  <span class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description-name">Matvii Tretiakov</span>
                  <span class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description-incription">Marketing Expert | Team Leader | General Manager</span>
                  <span class="recommendations-section-items_recommendation-items_recommender-info-items-profile-description-date-of-recommendation">June 7, 2025</span>
                </li>
              </ul>
            </router-link>
          </li>
          <li class="recommendations-section-items_recommendation-items_content">
            <p class="recommendations-section-items_recommendation-items_content-element">Nazar is a highly motivated specialist in programming, problem solving, and creating innovative products. He consistently seeks out new challenges to expand his skills and deliver efficient, high-quality solutions. His strong analytical thinking and attention to detail make him a valuable asset to any development team.</p>
          </li>
        </ul>
      </li>
    </ul>
    <router-link class="recommendations-section-items_show-all-recommendations-link" to="/path-to-list-of-recommendations-of-user">
      <div class="recommendations-section-items_show-all-recommendations">
        <span class="recommendations-section-items_show-all-recommendations-element">Show all recommendations</span>
        <img class="recommendations-section-items_show-all-recommendations-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon">
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

export default defineComponent({
  setup() {
    const isReceivedRecommendationsVisible = ref<boolean>(true)
    const isGivenRecommendationsVisible = ref<boolean>(false)

    const toggleRecommendationType = async function(recommendationType: string) {
      if (recommendationType == "received" && isReceivedRecommendationsVisible.value == false || recommendationType == "given" && isGivenRecommendationsVisible.value == false) {
        isReceivedRecommendationsVisible.value = !isReceivedRecommendationsVisible.value;
        isGivenRecommendationsVisible.value = !isGivenRecommendationsVisible.value;
      }
    }

    return { isReceivedRecommendationsVisible, isGivenRecommendationsVisible, toggleRecommendationType };
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.recommendations-section {
  margin: 0 0 100px 100px;
  padding: 50px 50px 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 50px 10px;
    padding: 0;
    border: none;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_header {
      margin-bottom: 10px;

      @media (max-width: $breakpoint) {
        margin-bottom: 6px;
      }

      &-element {
        font-size: 24px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_type-of-recommendations {
      width: 200px;
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        width: 150px;
        margin-bottom: 20px;
      }

      &-items {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-content: center;

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

    &_recommendation {

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

    &_show-all-recommendations {
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