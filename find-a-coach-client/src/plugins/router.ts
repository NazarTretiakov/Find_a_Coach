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
import AddActivity from '../pages/my-profile/activities-page/add-activity/AddActivity.vue'
import ExperiencePage from '../pages/my-profile/experience-page/ExperiencePage.vue'
import AddPosition from '../pages/my-profile/experience-page/add-position/AddPosition.vue'
import EditPosition from '../pages/my-profile/experience-page/edit-position/EditPosition.vue'
import EventPage from '../pages/my-profile/activities-page/event-page/EventPage.vue'
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
    { path: '/register/confirm-email', component: ConfirmEmail, meta: { requiresAuth: false }, beforeEnter: useValidationOfConfirmEmailParams },
    { path: '/register/email-confirmed', component: EmailConfirmed, meta: { requiresAuth: false }, beforeEnter: useValidationOfEmailConfirmedParams },
    { path: '/search', component: SearchPage, meta: { requiresAuth: true } },
    { path: '/my-profile', component: MyProfilePage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-profile-section', component: AddProfileSection, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-personal-information', component: EditPersonalInformation, meta: { requiresAuth: true} },
    { path: '/my-profile/edit-about-me', component: EditAboutMe, meta: { requiresAuth: true} },
    { path: '/my-profile/activities', component: ActivitiesPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-activity', component: AddActivity, meta: { requiresAuth: true } },
    { path: '/my-profile/experience', component: ExperiencePage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-position', component: AddPosition, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-position', component: EditPosition, meta: { requiresAuth: true } },
    { path: '/forum/event', component: EventPage, meta: { requiresAuth: true } },
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