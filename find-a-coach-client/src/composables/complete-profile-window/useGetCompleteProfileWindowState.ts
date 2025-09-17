import type { CompleteProfileWindowState } from "../../types/complete-profile-window-state/CompleteProfileWindowState"
import { useAuthenticationStore } from "../../stores/authentication";


const API_URL = 'https://localhost:5058/api/completeProfileWindow/get-state'

export default async function useGetCompleteProfileWindowState(): Promise<CompleteProfileWindowState> {
  const authenticationStore = useAuthenticationStore()

  try {
    const now = new Date()

    if (authenticationStore.tokenExpiration && now > authenticationStore.tokenExpiration) {
      if (authenticationStore.refreshTokenExpiration && now < authenticationStore.refreshTokenExpiration) {
        await authenticationStore.refreshJWTToken()
      } else {
        authenticationStore.clearAllFieldsInStore()
        authenticationStore.clearAuthenticationStateFromLocalStore()
        window.location.href = '/login'
      }
    }
    
    const response = await fetch(`${API_URL}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': `Bearer ${authenticationStore.token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.detail || 'Unexpected error occurred while getting "Complete profile" window state.'
      }
    }

    const responseData = await response.json()

    return {
      isSuccessful: true,
      errorMessage: null,
      isVisible: responseData.isVisible,
      title: responseData.title
    }
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message
    }
  }
}