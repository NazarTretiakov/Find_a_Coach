import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'

export default function useValidationOfResetPasswordParams(to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext): void {
  if (!to.query.email || !to.query.token) {
    next('/home')
  } else {
    next()
  }
}