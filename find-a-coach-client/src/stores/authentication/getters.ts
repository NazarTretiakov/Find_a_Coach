import type { AuthenticationState } from "../../types/authentication/AuthenticationState"

export default {
  isAuthenticated(state: AuthenticationState) {
    return !!state.email
  }
}