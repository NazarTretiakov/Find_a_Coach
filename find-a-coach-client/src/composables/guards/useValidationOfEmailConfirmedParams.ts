import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'

export default function useValidationOfEmailConfirmedParams(to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext): void {
  if (!to.query.email || !to.query.role || !to.query.token || !to.query.expiration || !to.query.refreshToken || !to.query.refreshTokenExpiration) {
    next('/home')
  } else {
    next()
  }
}