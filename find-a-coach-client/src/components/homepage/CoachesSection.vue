<template>
  <section class="coaches-section">
    <ul class="coaches-section-items">
      <li class="coaches-section-items_description">
        <ul class="coaches-section-items_description-items">
          <li class="coaches-section-items_description-items_caption">
            <h1 class="coaches-section-items_description-items_caption-element">Experience <br/>the Power of Coaching</h1>
          </li>
          <li class="coaches-section-items_description-items_paragraph">
            <p class="coaches-section-items_description-items_paragraph-element">Looking to grow your skills and expand your knowledge? <br/>You’re in the right place! <br/><br/>Connect with experienced coaches who can guide you in the subject you’re passionate about - or find like-minded individuals who are learning the same thing. <br/><br/>Whether you need expert guidance or a study companion, this is the place to build meaningful connections and support your educational journey.</p>
          </li>
          <li class="coaches-section-items_description-items_button">
            <coaches-button></coaches-button>
          </li>
        </ul>
      </li>
      <li class="coaches-section-items_image">
        <img class="coaches-section-items_image-element" src="../../assets/images/coaches-section.png" alt="Image of the coaches section.">
      </li>
    </ul>
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import CoachesButton from './CoachesButton.vue'

export default defineComponent({
  components: {
    CoachesButton
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.coaches-section {
  margin: 50px;
  padding: 75px;
  background-color: $mainColor;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 10px;
    padding: 36px;
  }

  &-items {
    list-style: none;
    padding: 0;
    margin: 0;
    display: flex;
    justify-content: space-around;

    @media (max-width: $breakpoint) {
      flex-direction: column;
    }

    &_description {
      color: white;

      &-items {
        list-style: none;
        padding: 0;
        margin: 10px 0 0 10px;

        @media (max-width: $breakpoint) {
          margin: 0;
        }

        &_caption {

          &-element {
            font-size: 28px;
            margin: 0;

            @media (max-width: $breakpoint) {
              font-size: 20px;
            }
          }
        }

        &_paragraph {

          &-element {
            margin-top: 36px;
            font-size: 14px;
            width: 600px;

            @media (max-width: $breakpoint) {
              margin-top: 20px;
              font-size: 12px; 
              width: 100%;
            }
          }
        }

        &_button {
          margin-top: 40px;

            @media (max-width: $breakpoint) {
              margin-top: 20px;
              margin-bottom: 30px;
            }
        }
      }
    }

    &_image {

      &-element {
        width: 500px;
        border-radius: 20px;

          @media (max-width: $breakpoint) {
            width: 100%;
          }
      }
    }
  }
}
</style>