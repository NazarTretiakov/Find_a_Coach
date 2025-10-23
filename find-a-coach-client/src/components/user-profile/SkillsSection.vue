<template>
  <div class="skills-section">
    <ul class="skills-section-items">
      <li class="skills-section-items_header">
        <ul class="skills-section-items_header-items">
          <li class="skills-section-items_header-items_inscription"><h1 class="skills-section-items_header-items_inscription-element">Skills</h1></li>
        </ul>
      </li>

      <li v-for="(skill, index) in skills" :key="skill.skillId" class="skills-section-items_skill">
        <ul class="skills-section-items_skill-items">
          <li class="skills-section-items_skill-items_name">
            <router-link :to="`/user-profile/${id}/skills/#${skill.skillId}`" class="skills-section-items_skill-items_name-link">
              <h2 class="skills-section-items_skill-items_name-element">{{ skill.title }}</h2>
            </router-link>
          </li>

          <li v-for="(usage, i) in skill.usages" :key="i" class="skills-section-items_skill-items_source">
            <img class="skills-section-items_skill-items_source-icon" :src="getUsageIcon(usage.type)" :alt="`${usage.type} icon`" />
            <span class="skills-section-items_skill-items_source-inscription">{{ usage.title }}</span>
          </li>
        </ul>

        <div v-if="index !== skills.length - 1" class="skills-section-items_skill-divider"></div>
      </li>
    </ul>

    <router-link class="skills-section-items_show-all-skills-link" :to="`/user-profile/${id}/skills`">
      <div class="skills-section-items_show-all-skills">
        <span class="skills-section-items_show-all-skills-element">Show all skills</span>
        <img class="skills-section-items_show-all-skills-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon" />
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import type { Skill } from '@/types/my-profile/skills/Skill'
import useGetTwoSkillsWithMostUsages from '@/composables/my-profile/skills/useGetLastTwoSkillsWithMostUsages'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  setup(props) {
    const router = useRouter()
    const skills = ref<Skill[]>([])

    async function loadSkills() {
      const result = await useGetTwoSkillsWithMostUsages(props.id)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        skills.value = result as Skill[]
        console.log('Loaded skills:', skills.value)
      }
    }

    function getUsageIcon(type: string): string {
      switch (type) {
        case 'Project':
          return new URL('@/assets/images/icons/project-icon.svg', import.meta.url).href
        case 'Company':
          return new URL('@/assets/images/icons/job-icon.svg', import.meta.url).href
        case 'Certification':
          return new URL('@/assets/images/icons/certification-icon.svg', import.meta.url).href
        case 'School':
          return new URL('@/assets/images/icons/education-icon.svg', import.meta.url).href
        default:
          return new URL('@/assets/images/icons/project-icon.svg', import.meta.url).href
      }
    }

    onMounted(() => loadSkills())

    return { skills, getUsageIcon }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.skills-section {
  margin: 0 0 30px 100px;
  padding: 50px 50px 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
    padding: 0;
    border: none;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_header {
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        margin-bottom: 14px;
      }

      &-items {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-content: center;

        &_inscription {

          &-element {
            font-size: 24px;

            @media (max-width: $breakpoint) {
              font-size: 20px;
            }
          }
        }
      }
    }

    &_skill {
      
      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;

        &_name {
          margin-bottom: 10px;

          &-link {
            color: #000000;
            text-decoration: none;
          } 
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

    &_show-all-skills {
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