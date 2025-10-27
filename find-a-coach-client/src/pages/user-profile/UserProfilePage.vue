<template>
  <div class="layout">
    <basic-sticky-header class="header"></basic-sticky-header>

    <loading-square v-if="isLoading"></loading-square>
    <ul v-else class="profile-sections">
      <li class="profile-sections_left-side">
        <personal-information :id="id"></personal-information>
        <about-me :id="id"></about-me>
        <activities-section :id="id"></activities-section>
        <experience-section :id="id"></experience-section>
        <education-section :id="id"></education-section>
        <projects-section :id="id"></projects-section>
        <certifications-section :id="id"></certifications-section>
        <skills-section :id="id"></skills-section>
        <languages-section :id="id"></languages-section>
        <recommendations-section :id="id"></recommendations-section>
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

import BasicStickyHeader from '../../components/BasicStickyHeader.vue'
import PersonalInformation from '../../components/user-profile/PersonalInformation.vue'
import LoadingSquare from '@/components/LoadingSquare.vue'
import AboutMe from '../../components/user-profile/AboutMe.vue'
import ActivitiesSection from '../../components/user-profile/ActivitiesSection.vue'
import ExperienceSection from '../../components/user-profile/ExperienceSection.vue'
import EducationSection from '../../components/user-profile/EducationSection.vue';
import ProjectsSection from '../../components/user-profile/ProjectsSection.vue';
import CertificationsSection from '../../components/user-profile/CertificationsSection.vue'
import SkillsSection from '../../components/user-profile/SkillsSection.vue'
import LanguagesSection from '../../components/user-profile/LanguagesSection.vue';
import RecommendationsSection from '../../components/user-profile/RecommendationsSection.vue';
import RecommendedPeople from '../../components/user-profile/RecommendedPeople.vue'
import TheFooter from '../../components/TheFooter.vue'

import { useRouter } from 'vue-router'
import { useAuthenticationStore } from '@/stores/authentication'
import useIsProfileSectionsCompleted from '@/composables/my-profile/useIsProfileSectionsCompleted'
import { IsProfileSectionsCompleted } from '@/types/my-profile/IsProfileSectionsCompleted'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    BasicStickyHeader,
    PersonalInformation,
    LoadingSquare,
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
  setup(props) {
    const router = useRouter()
    const isProfileSectionsCompleted = ref<IsProfileSectionsCompleted>({} as IsProfileSectionsCompleted)
    const isLoading = ref<boolean>(true)
    const authenticationStore = useAuthenticationStore()

    async function loadIsProfileSectionsCompletedInfo() {
      const startTime = performance.now()

      if (props.id === authenticationStore.userId) {
        router.push('/my-profile')
      }

      const result = await useIsProfileSectionsCompleted(props.id)

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
  z-index: 0;

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