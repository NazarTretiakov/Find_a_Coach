import { useAuthenticationStore } from '../../stores/authentication'
import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router'

export default function useUnauthenticatedUserAccessVerification(to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext): void {
  const authenticationStore = useAuthenticationStore()
    
  if (authenticationStore.isAuthenticated) {
    next('/home')
  }
    
  next()
}