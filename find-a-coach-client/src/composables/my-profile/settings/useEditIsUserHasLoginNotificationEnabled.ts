import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/Settings'

export default async function useEditIsUserHasLoginNotificationEnabled(isLoginNotificationEnabled: boolean): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const bodyData = {
      isLoginNotificationEnabled
    }
    
    console.log(bodyData)

    const response = await fetch(`${API_URL}/edit-user-login-notification-enabled`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(bodyData)
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while editing enabling of login notification.',
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