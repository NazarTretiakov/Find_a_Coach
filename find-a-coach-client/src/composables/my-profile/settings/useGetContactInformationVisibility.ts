import { Result } from "@/types/Result"
import useEnsureValidToken from '@/composables/authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/Settings'

export default async function useGetContactInformationVisibility(): Promise<Result | string> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-contact-information-visibility`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting contact information visibility.',
      }
    }

    const data = await response.json()
    return data.contactInformationVisibilityType
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}