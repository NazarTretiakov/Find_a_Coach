<template>
  <basic-sticky-header class="header"></basic-sticky-header>

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

import BasicStickyHeader from '../../components/BasicStickyHeader.vue'
import PersonalInformation from '../../components/user-profile/PersonalInformation.vue'
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

export default defineComponent({
  components: {
    BasicStickyHeader,
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