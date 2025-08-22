import { createRouter, createWebHistory } from 'vue-router'

import useUserAccessVerification from '../composables/guards/useUserAccessVerification'
import useUnauthenticatedUserAccessVerification from '../composables/guards/useUnauthenticatedUserAccessVerification'
import useValidationOfResetPasswordParams from '../composables/guards/useValidationOfResetPasswordParams'
import useValidationOfConfirmEmailParams from '../composables/guards/useValidationOfConfirmEmailParams'
import useValidationOfEmailConfirmedParams from '../composables/guards/useValidationOfEmailConfirmedParams'

import HomePage from '../pages/HomePage.vue'
import AboutUs from '../pages/AboutUs.vue'
import LoginPage from '../pages/LoginPage.vue'
import ResetPassword from '../pages/ResetPassword.vue'
import RegistrationPage from '../pages/RegistrationPage.vue'
import ConfirmEmail from '../pages/ConfirmEmail.vue'
import EmailConfirmed from '../pages/EmailConfirmed.vue'
import SearchPage from '../pages/SearchPage.vue'
import MyProfilePage from '../pages/my-profile/MyProfilePage.vue'
import AddProfileSection from '../pages/my-profile/add-profile-section/AddProfileSection.vue'
import EditPersonalInformation from '../pages/my-profile/edit-personal-information/EditPersonalInformation.vue'
import EditAboutMe from '../pages/my-profile/edit-about-me/EditAboutMe.vue'
import ActivitiesPage from '../pages/my-profile/activities-page/ActivitiesPage.vue'
import EventPage from '../pages/my-profile/activities-page/event-page/EventPage.vue'
import AddActivity from '../pages/my-profile/activities-page/add-activity/AddActivity.vue'
import ExperiencePage from '../pages/my-profile/experience-page/ExperiencePage.vue'
import AddPosition from '../pages/my-profile/experience-page/add-position/AddPosition.vue'
import EditPosition from '../pages/my-profile/experience-page/edit-position/EditPosition.vue'
import EducationPage from '../pages/my-profile/education-page/EducationPage.vue'
import AddEducation from '../pages/my-profile/education-page/add-education/AddEducation.vue'
import EditEducation from '../pages/my-profile/education-page/edit-education/EditEducation.vue'
import ProjectsPage from '../pages/my-profile/projects-page/ProjectsPage.vue'
import AddProject from '../pages/my-profile/projects-page/add-project/AddProject.vue'
import EditProject from '../pages/my-profile/projects-page/edit-project/EditProject.vue'
import CertificationsPage from '../pages/my-profile/certifications-page/CertificationsPage.vue'
import AddCertification from '../pages/my-profile/certifications-page/add-certification/AddCertification.vue'
import EditCertification from '../pages/my-profile/certifications-page/edit-certification/EditCertification.vue'
import SkillsPage from '../pages/my-profile/skills-page/SkillsPage.vue'
import LanguagesPage from '../pages/my-profile/languages-page/LanguagesPage.vue'
import AddLanguage from '../pages/my-profile/languages-page/add-language/AddLanguage.vue'
import RecommendationsPage from '../pages/my-profile/recommendations-page/RecommendationsPage.vue'
import EditLanguage from '../pages/my-profile/languages-page/edit-language/EditLanguage.vue'
import NotificationsSettingsPage from '../pages/my-profile/settings-page/notifications-page/NotificationsSettingsPage.vue'
import PrivacySettingsPage from '../pages/my-profile/settings-page/privacy-page/PrivacySettingsPage.vue'
import SecuritySettingsPage from '../pages/my-profile/settings-page/security-page/SecuritySettingsPage.vue'
import NetworkPage from '../pages/network/NetworkPage.vue'
import AdminPanel from '../pages/AdminPanel.vue'
import ErrorPage from '../pages/ErrorPage.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', redirect: '/home', meta: { requiresAuth: false } },
    { path: '/home', component: HomePage, meta: { requiresAuth: false } },
    { path: '/home/about-us', component: AboutUs, meta: { requiresAuth: false } },
    { path: '/login', component: LoginPage, meta: { requiresAuth: false }, beforeEnter: useUnauthenticatedUserAccessVerification },
    { path: '/reset-password', component: ResetPassword, meta: { requiresAuth: false }, beforeEnter: useValidationOfResetPasswordParams },
    { path: '/register', component: RegistrationPage, meta: { requiresAuth: false }, beforeEnter: useUnauthenticatedUserAccessVerification },
    { path: '/register/confirm-email', component: ConfirmEmail, meta: { requiresAuthT: false }, beforeEnter: useValidationOfConfirmEmailParams },
    { path: '/register/email-confirmed', component: EmailConfirmed, meta: { requiresAuth: false }, beforeEnter: useValidationOfEmailConfirmedParams },
    { path: '/search', component: SearchPage, meta: { requiresAuth: true } },
    { path: '/my-profile', component: MyProfilePage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-profile-section', component: AddProfileSection, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-personal-information', component: EditPersonalInformation, meta: { requiresAuth: true} },
    { path: '/my-profile/edit-about-me', component: EditAboutMe, meta: { requiresAuth: true} },
    { path: '/my-profile/activities', component: ActivitiesPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-activity', component: AddActivity, meta: { requiresAuth: true } },
    { path: '/my-profile/event', component: EventPage, meta: { requiresAuth: true } },
    { path: '/my-profile/experience', component: ExperiencePage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-position', component: AddPosition, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-position', component: EditPosition, meta: { requiresAuth: true } },
    { path: '/my-profile/education', component: EducationPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-education', component: AddEducation, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-education', component: EditEducation, meta: { requiresAuth: true } },
    { path: '/my-profile/projects', component: ProjectsPage, meta: { requiresAuth: true} },
    { path: '/my-profile/add-project', component: AddProject, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-project', component: EditProject, meta: { requiresAuth: true } },
    { path: '/my-profile/certifications', component: CertificationsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-certification', component: AddCertification, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-certification', component: EditCertification, meta: { requiresAuth: true } },
    { path: '/my-profile/skills', component: SkillsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/languages', component: LanguagesPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-language', component: AddLanguage, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-language', component: EditLanguage, meta: { requiresAuth: true } },
    { path: '/my-profile/recommendations', component: RecommendationsPage, meta: { requiresAuth: true } }, 
    { path: '/my-profile/settings', redirect: "/my-profile/settings/notifications", meta: { requiresAuth: true } },
    { path: '/my-profile/settings/notifications', component: NotificationsSettingsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/settings/privacy', component: PrivacySettingsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/settings/security', component: SecuritySettingsPage, meta: { requiresAuth: true } },
    { path: '/network', component: NetworkPage, meta: { requiresAuth: true } },
    { path: '/admin', component: AdminPanel, meta: { requiresAuth: true, requiredRole: 'Admin' } },
    { path: '/error-page', component: ErrorPage, meta: { requiresAuth: false } }
  ],
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    } else {
      return { top: 0 }
    }
  }
})

router.beforeEach(useUserAccessVerification)

export default router