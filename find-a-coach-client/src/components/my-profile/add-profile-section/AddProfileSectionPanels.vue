<template> 
  <div class="add-profile-section">
    <ul class="add-profile-section-items">
      <li class="add-profile-section-items_header"><h1 class="add-profile-section-items_header-element">Add profile section</h1></li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Core information" description="Enter basic details and how people can contact you" :isProfileSectionAdded="true" linkToAddProfileSection="/my-profile/edit-personal-information" linkToChangeProfileSection="/my-profile/edit-personal-information"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Description" description="Write something about yourself" :isProfileSectionAdded="false" linkToAddProfileSection="/my-profile/edit-about-me" linkToChangeProfileSection="/my-profile/edit-about-me"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Experience" description="Tell where you've worked before" :isProfileSectionAdded="false" linkToAddProfileSection="/my-profile/add-position" linkToChangeProfileSection="/my-profile/edit-experience"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Education" description="Add schools or degrees" :isProfileSectionAdded="false" linkToAddProfileSection="/my-profile/add-education" linkToChangeProfileSection="/my-profile/edit-education"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Projects" description="Showcase your projects" :isProfileSectionAdded="false" linkToAddProfileSection="/my-profile/add-project" linkToChangeProfileSection="/my-profile/edit-projects"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Certifications" description="Include relevant certificates or qualifications" :isProfileSectionAdded="false" linkToAddProfileSection="/my-profile/add-certification" linkToChangeProfileSection="/my-profile/edit-certifications"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel-last">
        <add-panel nameOfSection="Languages" description="Add languages you know" :isProfileSectionAdded="false" linkToAddProfileSection="/my-profile/add-language" linkToChangeProfileSection="/my-profile/edit-languages"></add-panel>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import AddPanel from './AddPanel.vue'

export default defineComponent({
  components: {
    AddPanel
  },
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