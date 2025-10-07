import type { AuthenticationState } from "../../types/authentication/AuthenticationState"
import { jwtDecode } from 'jwt-decode'
import type { JwtPayload } from '@/types/authentication/JwtPayload'

export default {
  isAuthenticated(state: AuthenticationState) {
    return !!state.email
  },
  userId: (state: AuthenticationState): string | null => {
    if (!state.token) return null
    try {
    const decoded = jwtDecode<JwtPayload>(state.token)
      return decoded.sub || null
    } catch {
      return null
    }
  },
}