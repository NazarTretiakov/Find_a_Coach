<template>
  <div class="skills">
    <ul class="skills-header">
      <li class="skills-header_inscription"><h1 class="skills-header_inscription-element">Skills</h1></li>
      <li class="skills-header_add-button">
        <router-link to="/my-profile/add-skill" class="skills-header_add-button-link">
          <button class="skills-header_add-button-element">
            <svg class="skills-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"><path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/></svg>
            <span class="skills-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
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
import { useAuthenticationStore } from '@/stores/authentication'
import type { Skill } from '@/types/my-profile/skills/Skill'
import useGetAllSkills from '@/composables/my-profile/skills/useGetAllSkills'

export default defineComponent({
  setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const skills = ref<Skill[]>([])

    async function loadSkills() {
      const result = await useGetAllSkills(authenticationStore.userId)

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

    &_add-button {
      display: flex;
      align-items: flex-end;

      &-element {
        display: flex;
        justify-content: center;
        align-items: center;
        border: 2px solid $grayBorderColor;
        border-radius: 10px;
        width: 100px;
        height: 40px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          height: 36px;
        }

        &:hover {
          transform: scale(1.10);
        }

        &-icon {
          width: 30px;
          margin-right: 8px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            width: 24px;
            margin-right: 6px;
          }
        }
        &-inscription {
          font-size: 20px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            font-size: 16px;
          }
        }
      }

      &-link {
        text-decoration: none;
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
}
</style>