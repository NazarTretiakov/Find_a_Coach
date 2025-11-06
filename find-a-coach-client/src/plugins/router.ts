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
import ContactInformation from '../pages/my-profile/contact-information-page/ContactInformationPage.vue'
import EditAboutMe from '../pages/my-profile/edit-about-me/EditAboutMe.vue'
import ActivitiesPage from '../pages/my-profile/activities-page/ActivitiesPage.vue'
import EventPage from '../pages/my-profile/activities-page/event-page/EventPage.vue'
import QuestionAnswerPage from '../pages/my-profile/activities-page/qa-page/QAPage.vue'
import QAAnswersPage from '@/pages/my-profile/activities-page/qa-page/qa-answers-page/QAAnswersPage.vue'
import SurveyPage from '../pages/my-profile/activities-page/survey-page/SurveyPage.vue'
import PostPage from '../pages/my-profile/activities-page/post-page/PostPage.vue'
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
import ConnectionsPage from '../pages/network/connections-page/ConnectionsPage.vue'
import InvitationsPage from '../pages/network/invitations-page/InvitationsPage.vue'
import InvitationPage from '../pages/network/invitations-page/invitation-page/InvitationPage.vue'
import ForumPage from '../pages/forum/ForumPage.vue'
import Event from '../pages/forum/event-page/EventPage.vue'
import SearchPersonPanelApplicationsPage from '../pages/my-profile/activities-page/event-page/search-person-panel-applications-page/SearchPersonPanelApplicationsPage.vue'
import QuestionAnswer from '../pages/forum/qa-page/QAPage.vue'
import Survey from '../pages/forum/survey-page/SurveyPage.vue'
import Post from '../pages/forum/post-page/PostPage.vue'
import UserProfilePage from '../pages/user-profile/UserProfilePage.vue'
import ConnectPage from '../pages/user-profile/connect-page/ConnectPage.vue'
import UserLeaveReviewPage from '../pages/user-profile/leave-recommendation-page/LeaveRecommendationPage.vue'
import UserContactInformation from '../pages/user-profile/contact-information-page/ContactInformationPage.vue'
import UserProfileActivities from '../pages/user-profile/activities-page/ActivitiesPage.vue'
import UserProfileExperience from '../pages/user-profile/experience-page/ExperiencePage.vue'
import UserProfileEducation from '../pages/user-profile/education-page/EducationPage.vue'
import UserProfileProjects from '../pages/user-profile/projects-page/ProjectsPage.vue'
import UserProfileCertifications from '../pages/user-profile/certifications-page/CertificationsPage.vue'
import UserProfileSkills from '../pages/user-profile/skills-page/SkillsPage.vue'
import UserProfileLanguages from '../pages/user-profile/languages-page/LanguagesPage.vue'
import UserProfileRecommendations from '../pages/user-profile/recommendations-page/RecommendationsPage.vue'
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
    { path: '/my-profile/contact-information', component: ContactInformation, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-about-me', component: EditAboutMe, meta: { requiresAuth: true} },
    { path: '/my-profile/activities', component: ActivitiesPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-activity', component: AddActivity, meta: { requiresAuth: true } },
    { path: '/my-profile/activities/event/:id', component: EventPage, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/activities/event/:id/applications/:panelId', component: SearchPersonPanelApplicationsPage, meta: { requiresAuth: true }, props: true},
    { path: '/my-profile/activities/qa/:id', component: QuestionAnswerPage, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/activities/qa/:id/answers', component: QAAnswersPage, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/activities/survey/:id', component: SurveyPage, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/activities/post/:id', component: PostPage, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/experience', component: ExperiencePage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-position', component: AddPosition, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-position/:id', component: EditPosition, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/education', component: EducationPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-education', component: AddEducation, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-education/:id', component: EditEducation, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/projects', component: ProjectsPage, meta: { requiresAuth: true} },
    { path: '/my-profile/add-project', component: AddProject, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-project/:id', component: EditProject, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/certifications', component: CertificationsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-certification', component: AddCertification, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-certification/:id', component: EditCertification, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/skills', component: SkillsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/languages', component: LanguagesPage, meta: { requiresAuth: true } },
    { path: '/my-profile/add-language', component: AddLanguage, meta: { requiresAuth: true } },
    { path: '/my-profile/edit-language/:id', component: EditLanguage, meta: { requiresAuth: true }, props: true },
    { path: '/my-profile/recommendations', component: RecommendationsPage, meta: { requiresAuth: true } }, 
    { path: '/my-profile/settings', redirect: "/my-profile/settings/notifications", meta: { requiresAuth: true } },
    { path: '/my-profile/settings/notifications', component: NotificationsSettingsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/settings/privacy', component: PrivacySettingsPage, meta: { requiresAuth: true } },
    { path: '/my-profile/settings/security', component: SecuritySettingsPage, meta: { requiresAuth: true } },
    { path: '/network', component: NetworkPage, meta: { requiresAuth: true } },
    { path: '/network/connections', component: ConnectionsPage, meta: { requiresAuth: true } },
    { path: '/network/notifications', component: InvitationsPage, meta: { requiresAuth: true } },
    { path: '/network/notifications/connection-request/:id', component: InvitationPage, meta: { requiresAuth: true }, props: true },
    { path: '/forum', component: ForumPage, meta: { requiresAuth: true } },
    { path: '/forum/event/:id', component: Event, meta: { requiresAuth: true }, props: true },
    { path: '/forum/qa/:id', component: QuestionAnswer, meta: { requiresAuth: true }, props: true },
    { path: '/forum/survey/:id', component: Survey, meta: { requiresAuth: true }, props: true },
    { path: '/forum/post/:id', component: Post, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id', component: UserProfilePage, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/send-connection-request', component: ConnectPage, meta: { requiresAuth: true }, props: route => ({ id: route.query.id, name: route.query.name }) },
    { path: '/user-profile/leave-recommendation', component: UserLeaveReviewPage, meta: { requiresAuth: true }, props: route => ({ id: route.query.id, name: route.query.name }) },
    { path: '/user-profile/:id/contact-information', component: UserContactInformation, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/activities', component: UserProfileActivities, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/experience', component: UserProfileExperience, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/education', component: UserProfileEducation, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/projects', component: UserProfileProjects, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/certifications', component: UserProfileCertifications, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/skills', component: UserProfileSkills, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/languages', component: UserProfileLanguages, meta: { requiresAuth: true }, props: true },
    { path: '/user-profile/:id/recommendations', component: UserProfileRecommendations, meta: { requiresAuth: true }, props: true },
    { path: '/admin', component: AdminPanel, meta: { requiresAuth: true, requiredRole: 'Admin' } },
    { path: '/error-page', component: ErrorPage, meta: { requiresAuth: false } }
  ],
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    } 
    
    if (to.hash) {
      return new Promise((resolve) => {
        setTimeout(() => {
          resolve({
            el: to.hash,
            behavior: 'smooth'
          })
        }, 300)
      })
    }

    return { top: 0 }
  }
})

router.beforeEach(useUserAccessVerification)

export default router