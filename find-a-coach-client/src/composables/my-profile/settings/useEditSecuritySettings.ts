import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { SecuritySettings } from "@/types/my-profile/settings/SecuritySettings"

const API_URL = config.apiBaseUrl + '/Settings'

export default async function useEditSecuritySettings(securitySettings: SecuritySettings): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/edit-security-settings`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(securitySettings)
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while editing security settings.',
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