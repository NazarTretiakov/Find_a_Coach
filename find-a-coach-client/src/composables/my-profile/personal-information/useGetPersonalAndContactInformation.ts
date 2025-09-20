import { PersonalAndContactInformation } from "@/types/my-profile/PersonalAndContactInformation"
import { useAuthenticationStore } from "../../../stores/authentication"
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useGetPersonalAndContactInformation(): Promise<PersonalAndContactInformation | { isSuccessful: boolean, errorMessage: string | null }> {
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

    const response = await fetch(`${API_URL}/get-personal-and-contact-information`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${authenticationStore.token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occured while getting personal information.',
      }
    }

    const data: PersonalAndContactInformation = await response.json()
    
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}