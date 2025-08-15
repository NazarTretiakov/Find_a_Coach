<template> 
  <div class="add-profile-section">
    <ul class="add-profile-section-items">
      <li class="add-profile-section-items_header"><h1 class="add-profile-section-items_header-element">Add profile section</h1></li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Core information" description="Enter basic details and how people can contact you" :isProfileSectionAdded="true" linkToAddProfileSection="/profile/edit-personal-information" linkToChangeProfileSection="/profile/edit-personal-information"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Description" description="Write something about yourself" :isProfileSectionAdded="false" linkToAddProfileSection="/profile/edit-about-me" linkToChangeProfileSection="/profile/edit-about-me"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Experience" description="Tell where you've worked before" :isProfileSectionAdded="false" linkToAddProfileSection="/profile/add-experience" linkToChangeProfileSection="/profile/edit-experience"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Education" description="Add schools or degrees" :isProfileSectionAdded="false" linkToAddProfileSection="/profile/add-education" linkToChangeProfileSection="/profile/edit-education"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Projects" description="Showcase your projects" :isProfileSectionAdded="false" linkToAddProfileSection="/profile/add-project" linkToChangeProfileSection="/profile/edit-projects"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Certifications" description="Include relevant certificates or qualifications" :isProfileSectionAdded="false" linkToAddProfileSection="/profile/add-certification" linkToChangeProfileSection="/profile/edit-certifications"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel">
        <add-panel nameOfSection="Skills" description="List your skills here" :isProfileSectionAdded="false" linkToAddProfileSection="/profile/add-skill" linkToChangeProfileSection="/profile/edit-skills"></add-panel>
      </li>
      <li class="add-profile-section-items_section-panel-last">
        <add-panel nameOfSection="Languages" description="Add languages you know" :isProfileSectionAdded="false" linkToAddProfileSection="/profile/add-language" linkToChangeProfileSection="/profile/edit-languages"></add-panel>
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