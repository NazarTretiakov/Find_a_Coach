<template>
  <profile-sticky-header class="header"></profile-sticky-header>

  <ul class="profile-sections">
    <li class="profile-sections_left-side">
      <personal-information></personal-information>
      <about-me></about-me>
      <activities-section></activities-section>
      <experience-section></experience-section>
      <education-section></education-section>
      <projects-section></projects-section>
      <certifications-section></certifications-section>
      <skills-section></skills-section>
      <languages-section></languages-section>
      <recommendations-section></recommendations-section>
    </li>
    <li class="profile-sections_right-side">
      <recommended-people></recommended-people>
    </li>
  </ul>

  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import ProfileStickyHeader from '../../components/my-profile/ProfileStickyHeader.vue'
import PersonalInformation from '../../components/my-profile/PersonalInformation.vue'
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

export default defineComponent({
  components: {
    ProfileStickyHeader,
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
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.header {
  z-index: 2;
}

.profile-sections {
  display: flex;
  list-style: none;
  padding: 0;

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
</style>