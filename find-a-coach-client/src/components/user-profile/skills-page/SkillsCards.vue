<template>
  <div class="skills">
    <ul class="skills-header">
      <li class="skills-header_inscription"><h1 class="skills-header_inscription-element">Skills</h1></li>
    </ul>

    <ul class="skills-items">
      <li v-for="skill in skills" :key="skill.skillId" class="skills-items_skill" :id="skill.skillId">
        <ul class="skills-items_skill-items">
          <li class="skills-items_skill-items_name"><h2 class="skills-items_skill-items_name-element">{{ skill.title }}</h2></li>

          <li v-for="(usage, index) in skill.usages" :key="index" class="skills-items_skill-items_source">
            <img class="skills-items_skill-items_source-icon" :src="getUsageIcon(usage.type)" :alt="`${usage.type} icon`" />
            <span class="skills-items_skill-items_source-inscription">{{ usage.title }}</span>
          </li>
        </ul>
      </li>
    </ul>
  </div>
</template>


<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import type { Skill } from '@/types/my-profile/skills/Skill'
import useGetAllSkills from '@/composables/my-profile/skills/useGetAllSkills'

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
      const result = await useGetAllSkills(props.id)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        skills.value = result as Skill[]
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
@use '../../../assets/styles/config' as *;

.skills {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-content: center;

    &_inscription {
      &-element {
        font-size: 28px;
        margin-left: 6px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_skill {
      margin-top: 30px;
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