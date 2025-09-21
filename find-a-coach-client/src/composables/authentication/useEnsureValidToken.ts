import { useAuthenticationStore } from "../../stores/authentication"

export default async function useEnsureValidToken(): Promise<string | null> {
  const authenticationStore = useAuthenticationStore()
  const now = new Date()

  try {
    if (authenticationStore.tokenExpiration && now > authenticationStore.tokenExpiration) {
      if (authenticationStore.refreshTokenExpiration && now < authenticationStore.refreshTokenExpiration) {
        await authenticationStore.refreshJWTToken()
      } else {
        authenticationStore.clearAllFieldsInStore()
        authenticationStore.clearAuthenticationStateFromLocalStore()
        window.location.href = '/login'
      }
    }

    return authenticationStore.token
  } 
  catch (error) {
    throw error
  }
}