import { useAuthenticationStore } from "../../stores/authentication";


const API_URL = 'https://localhost:5058/api/completeProfileWindow/change-state';

export default async function changeCompleteProfileWindowVisibility(): Promise<{ isSuccessful: boolean, errorMessage: string | null }> {
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
      },
      body: JSON.stringify({
        isVisible: false,
        title: 'Welcome back'
      })
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.detail || 'Unexpected error occured while changing "Complete profile" window title.',
      }
    }

    return {
      isSuccessful: true,
      errorMessage: null,
    }
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}