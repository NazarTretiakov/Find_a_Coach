import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'

export default function useValidationOfConfirmEmailParams(to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext): void {
  if (!to.query.email) {
    next('/home')
  } else {
    next()
  }
}