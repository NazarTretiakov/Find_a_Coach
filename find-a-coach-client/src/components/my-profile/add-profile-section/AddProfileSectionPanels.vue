<template> 
  <div class="add-profile-section">
    <ul class="add-profile-section-items">
      <li class="add-profile-section-items_header"><h1 class="add-profile-section-items_header-element">Add profile section</h1></li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Core information" description="Enter basic details and how people can contact you" :isProfileSectionAdded="isProfileSectionsCompleted.isCoreInfo" linkToAddProfileSection="/my-profile/edit-personal-information" linkToChangeProfileSection="/my-profile/edit-personal-information"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Description" description="Write something about yourself" :isProfileSectionAdded="isProfileSectionsCompleted.isDescription" linkToAddProfileSection="/my-profile/edit-about-me" linkToChangeProfileSection="/my-profile/edit-about-me"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Activities" description="Share your thoughts, updates, or insights" :isProfileSectionAdded="isProfileSectionsCompleted.isActivities" linkToAddProfileSection="/my-profile/add-activity" linkToChangeProfileSection="/my-profile/activities"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Experience" description="Tell where you've worked before" :isProfileSectionAdded="isProfileSectionsCompleted.isExperience" linkToAddProfileSection="/my-profile/add-position" linkToChangeProfileSection="/my-profile/experience"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Education" description="Add schools or degrees" :isProfileSectionAdded="isProfileSectionsCompleted.isEducation" linkToAddProfileSection="/my-profile/add-education" linkToChangeProfileSection="/my-profile/education"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Projects" description="Showcase your projects" :isProfileSectionAdded="isProfileSectionsCompleted.isProjects" linkToAddProfileSection="/my-profile/add-project" linkToChangeProfileSection="/my-profile/projects"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Certifications" description="Include relevant certificates or qualifications" :isProfileSectionAdded="isProfileSectionsCompleted.isCertifications" linkToAddProfileSection="/my-profile/add-certification" linkToChangeProfileSection="/my-profile/certifications"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel-last">
        <add-panel nameOfSection="Languages" description="Add languages you know" :isProfileSectionAdded="isProfileSectionsCompleted.isLanguages" linkToAddProfileSection="/my-profile/add-language" linkToChangeProfileSection="/my-profile/languages"></add-panel>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import AddPanel from './AddPanel.vue'

import { useRouter } from 'vue-router'
import useIsProfileSectionsCompleted from '@/composables/my-profile/useIsProfileSectionsCompleted'
import type { IsProfileSectionsCompleted } from '@/types/my-profile/IsProfileSectionsCompleted'

export default defineComponent({
  components: {
    AddPanel
  },
  setup() {
    const router = useRouter()
    const isProfileSectionsCompleted = ref<IsProfileSectionsCompleted>({} as IsProfileSectionsCompleted)

    async function loadIsProfileSectionsCompletedInfo() {
      const result = await useIsProfileSectionsCompleted()

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        isProfileSectionsCompleted.value = result as IsProfileSectionsCompleted
      }
    }

    onMounted(() => loadIsProfileSectionsCompletedInfo())

    return { isProfileSectionsCompleted }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.add-profile-section {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 0 10px;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;
    width: 800px;

    @media (max-width: $breakpoint) {
      width: auto;
    }

    &_header {

      &-element {
        font-size: 28px;
        margin: 0 0 50px 6px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_section-panel {
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        margin-bottom: 24px;
      }

      &-last {
        margin-bottom: 100px;
      }
    }
  }
}
</style>