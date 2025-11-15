import { Result } from "@/types/Result"
import useEnsureValidToken from '@/composables/authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/Settings'

export default async function useIsUserHasLoginNotificationEnabled(): Promise<Result | boolean> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/is-user-has-login-notification-enabled`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting information about is user has login notification enabled.',
      }
    }

    const data = await response.json()
    return data.isLoginNotificationEnabled
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}