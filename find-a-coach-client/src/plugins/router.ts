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
import ProfilePage from '../pages/ProfilePage.vue'
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
    { path: '/profile', component: ProfilePage, meta: { requiresAuth: true } },
    { path: '/admin', component: AdminPanel, meta: { requiresAuth: true, requiredRole: 'Admin' } },
    { path: '/error-page', component: ErrorPage, meta: { requiresAuth: false } }
  ]
})

router.beforeEach(useUserAccessVerification)

export default router