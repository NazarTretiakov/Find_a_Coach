<template> 
  <div class="skills">
    <h1 class="skills-header">Skills</h1>
    <ul class="skills-items">
      <li class="skills-items_skill">
        <ul class="skills-items_skill-items">
          <li class="skills-items_skill-items_name"><h2 class="skills-items_skill-items_name-element">JavaScript</h2></li>
          <li class="skills-items_skill-items_source">
            <img class="skills-items_skill-items_source-icon" src="../../../assets/images/icons/education-icon.svg" alt="Education icon">
            <span class="skills-items_skill-items_source-inscription">Pomeranian University in Słupsk</span>
          </li>
          <li class="skills-items_skill-items_source">
            <img class="skills-items_skill-items_source-icon" src="../../../assets/images/icons/job-icon.svg" alt="Job icon">
            <span class="skills-items_skill-items_source-inscription">The Best Company</span>
          </li>
          <li class="skills-items_skill-items_source">
            <img class="skills-items_skill-items_source-icon" src="../../../assets/images/icons/certification-icon.svg" alt="Certification icon">
            <span class="skills-items_skill-items_source-inscription">JavaScript + TypeScript Course</span>
          </li>
        </ul>
      </li>
      <li class="skills-items_skill">
        <ul class="skills-items_skill-items">
          <li class="skills-items_skill-items_name"><h2 class="skills-items_skill-items_name-element">Store management</h2></li>
          <li class="skills-items_skill-items_source">
            <img class="skills-items_skill-items_source-icon" src="../../../assets/images/icons/project-icon.svg" alt="Project icon">
            <span class="skills-items_skill-items_source-inscription">Store management for Amazon company</span>
          </li>
          <li class="skills-items_skill-items_source">
            <img class="skills-items_skill-items_source-icon" src="../../../assets/images/icons/certification-icon.svg" alt="Certification icon">
            <span class="skills-items_skill-items_source-inscription">Complete store management guide</span>
          </li>
        </ul>
      </li>
    </ul>
    <div class="skills-load-more-skills">
      <span class="skills-load-more-skills-inscription">Load more skills</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import TheSkills from '../TheSkills.vue'

export default defineComponent({
  components: {
    TheSkills
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.skills {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
  }

  &-header {
    font-size: 28px;
    margin-left: 6px;
    margin-bottom: 30px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_skill {
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

        &_name {
          margin-bottom: 10px;

          &-element {
            font-size: 20px;

            @media (max-width: $breakpoint) {
              font-size: 16px;
            }
          }
        }

        &_source {
          display: flex;
          align-items: center;
          margin-bottom: 8px;

          &-icon {
            width: 28px;
            margin-right: 10px;

            @media (max-width: $breakpoint) {
              width: 24px;
              margin-right: 8px;
            }
          }
          &-inscription {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }
    }
  }

  &-load-more-skills  {
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