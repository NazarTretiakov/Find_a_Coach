import { useAuthenticationStore } from "../../stores/authentication"

let refreshInProgress: Promise<string | null> | null = null

export default async function useEnsureValidToken(): Promise<string | null> {
  const authenticationStore = useAuthenticationStore()
  const now = new Date()

  if (refreshInProgress) {
    return await refreshInProgress
  }

  const tokenExp = authenticationStore.expiration ? new Date(authenticationStore.expiration) : null
  const refreshExp = authenticationStore.refreshTokenExpirationDateTime ? new Date(authenticationStore.refreshTokenExpirationDateTime) : null

  const bufferMs = 30 * 1000

  try {
    if (tokenExp && now.getTime() + bufferMs > tokenExp.getTime()) {
      if (refreshExp && now.getTime() < refreshExp.getTime()) {
        refreshInProgress = (async () => {
          const result = await authenticationStore.refreshJWTToken()

          if (!result || (typeof result === 'object' && !('isSuccessful' in result))) {
            console.warn('refreshJWTToken returned unexpected value', result)
            authenticationStore.clearAllFieldsInStore()
            authenticationStore.clearAuthenticationStateFromLocalStore()
            window.location.href = '/login'
            return null
          }

          if (result.isSuccessful === false) {
            authenticationStore.clearAllFieldsInStore()
            authenticationStore.clearAuthenticationStateFromLocalStore()
            window.location.href = '/login'
            return null
          }

          return authenticationStore.token
        })()

        const newToken = await refreshInProgress
        refreshInProgress = null
        return newToken
      } 
      else {
        authenticationStore.clearAllFieldsInStore()
        authenticationStore.clearAuthenticationStateFromLocalStore()
        window.location.href = '/login'
        return null
      }
    }

    return authenticationStore.token
  } catch (error) {
    console.error('useEnsureValidToken error', error)
    refreshInProgress = null
    throw error
  }
}
