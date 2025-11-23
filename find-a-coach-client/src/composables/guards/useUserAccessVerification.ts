import { useAuthenticationStore } from '../../stores/authentication'
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'

export default function useUserAccessVerification(to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext): void {
  const authenticationStore = useAuthenticationStore()
    
  if (to.meta.requiresAuth) {
    if (!authenticationStore.isAuthenticated) {
      next('/login')
      return
    }
      
    if (to.meta.requiredRole) {
      if (authenticationStore.role !== to.meta.requiredRole) {
        next('/error-page')
        return
      }
      if (authenticationStore.role === 'Admin' && to.meta.requiredRole != 'Admin') {
        next('/admin')
      }
    } else if (authenticationStore.role === 'Admin') {
      next('/admin')
    }
  }
    
  next()
}