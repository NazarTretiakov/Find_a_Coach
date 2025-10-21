<template>
  <div class="layout">
    <profile-sticky-header class="header"></profile-sticky-header>

    <loading-square v-if="isLoading"></loading-square>
    <ul v-else class="profile-sections">
      <li class="profile-sections_left-side">
        <personal-information></personal-information>
        <about-me v-if="isProfileSectionsCompleted.isDescription"></about-me>
        <activities-section v-if="isProfileSectionsCompleted.isActivities"></activities-section>
        <experience-section v-if="isProfileSectionsCompleted.isExperience"></experience-section>
        <education-section v-if="isProfileSectionsCompleted.isEducation"></education-section>
        <projects-section v-if="isProfileSectionsCompleted.isProjects"></projects-section>
        <certifications-section v-if="isProfileSectionsCompleted.isCertifications"></certifications-section>
        <skills-section v-if="isProfileSectionsCompleted.isSkills"></skills-section>
        <languages-section v-if="isProfileSectionsCompleted.isLanguages"></languages-section>
        <recommendations-section></recommendations-section>
      </li>
      <li class="profile-sections_right-side">
        <recommended-people></recommended-people>
      </li>
    </ul>

    <the-footer class="footer"></the-footer>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import ProfileStickyHeader from '../../components/my-profile/ProfileStickyHeader.vue'
import PersonalInformation from '../../components/my-profile/PersonalInformation.vue'
import LoadingSquare from '@/components/LoadingSquare.vue'
import AboutMe from '../../components/my-profile/AboutMe.vue'
import ActivitiesSection from '../../components/my-profile/ActivitiesSection.vue'
import ExperienceSection from '../../components/my-profile/ExperienceSection.vue'
import EducationSection from '../../components/my-profile/EducationSection.vue';
import ProjectsSection from '../../components/my-profile/ProjectsSection.vue';
import CertificationsSection from '../../components/my-profile/CertificationsSection.vue'
import SkillsSection from '../../components/my-profile/SkillsSection.vue'
import LanguagesSection from '../../components/my-profile/LanguagesSection.vue';
import RecommendationsSection from '../../components/my-profile/RecommendationsSection.vue';
import RecommendedPeople from '../../components/my-profile/RecommendedPeople.vue'
import TheFooter from '../../components/TheFooter.vue'

import { useRouter } from 'vue-router'
import useIsProfileSectionsCompleted from '@/composables/my-profile/useIsProfileSectionsCompleted'
import type { IsProfileSectionsCompleted } from '@/types/my-profile/IsProfileSectionsCompleted'

export default defineComponent({
  components: {
    ProfileStickyHeader,
    LoadingSquare,
    PersonalInformation,
    AboutMe,
    ActivitiesSection,
    ExperienceSection,
    EducationSection,
    ProjectsSection,
    CertificationsSection,
    SkillsSection,
    LanguagesSection,
    RecommendationsSection,
    RecommendedPeople,
    TheFooter
  },
  setup() {
    const router = useRouter()
    const isProfileSectionsCompleted = ref<IsProfileSectionsCompleted>({} as IsProfileSectionsCompleted)
    const isLoading = ref<boolean>(true)

    async function loadIsProfileSectionsCompletedInfo() {
      const startTime = performance.now()

      const result = await useIsProfileSectionsCompleted()

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        isProfileSectionsCompleted.value = result as IsProfileSectionsCompleted
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) {
        setTimeout(() => (isLoading.value = false), remaining)
      } else {
        isLoading.value = false
      }
    }

    onMounted(() => loadIsProfileSectionsCompletedInfo())

    return { isProfileSectionsCompleted, isLoading }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.header {
  z-index: 1;
}

.profile-sections {
  display: flex;
  list-style: none;
  padding: 0;
  flex: 1;

  &_left-side {
    width: 80%;

    @media (max-width: $breakpoint) {
      width: 100%;
    }
  }
  &_right-side {

    @media (max-width: $breakpoint) {
      display: none;
    }

    > * {
      position: sticky;
      top: 100px;
      z-index: 1;
      margin-bottom: 100px;
    }
  }
}
.layout {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}
.footer {
  margin-top: auto;
}
</style>