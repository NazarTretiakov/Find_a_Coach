<template>
  <section class="forum-section">
    <ul class="forum-section-items">
      <li class="forum-section-items_image">
        <img class="forum-section-items_image-element" src="../../assets/images/forum-section.png" alt="Image of the forum section.">
      </li>
      <li class="forum-section-items_description">
        <ul class="forum-section-items_description-items">
          <li class="forum-section-items_description-items_caption">
            <h1 class="forum-section-items_description-items_caption-element">The Forum: Learn, Share, Connect</h1>
          </li>
          <li class="forum-section-items_description-items_paragraph">
            <p class="forum-section-items_description-items_paragraph-element">Take your experience further by joining our community forum. Here, you can post questions, share events, exchange resources, or start conversations related to your studies. <br/>It’s a space designed for collaboration - where you can meet new people, find learning partners, and grow together through active communication and shared goals. <br/><br/>If you want to share your study progress, post news about interesting events, or simply need advice, the forum is a great place for all of that!</p>
          </li>
          <li class="forum-section-items_description-items_button">
            <forum-button></forum-button>
          </li>
        </ul>
      </li>
    </ul>
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ForumButton from './GotoforumButton.vue'

export default defineComponent({
  components: {
    ForumButton
  },
  setup() {
    
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.forum-section {
  margin: 100px 125px;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 46px;
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

    &_image {

      @media (max-width: $breakpoint) {
        order: 2;
      }

      &-element {
        width: 500px;
        border-radius: 20px;

        @media (max-width: $breakpoint) {
          width: 100%;
        }
      }
    }

    &_description {

      @media (max-width: $breakpoint) {
        order: 1;
      }

      &-items {
        list-style: none;
        padding: 0;
        margin: 20px 0 0 50px;

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
  }
}
</style>