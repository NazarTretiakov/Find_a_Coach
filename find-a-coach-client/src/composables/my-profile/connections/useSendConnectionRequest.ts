import { Result } from "@/types/Result"
import useEnsureValidToken from '@/composables/authentication/useEnsureValidToken'
import { config } from '@/config'
import type { ConnectionForm } from '@/types/my-profile/connections/ConnectionForm'

const API_URL = config.apiBaseUrl + '/Network'

export default async function useAddRecommendation(formData: ConnectionForm): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/send-connection-request`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        ...formData
      })
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while sending connection form.'
      }
    }

    return {
      isSuccessful: true,
      errorMessage: null
    }

  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message
    }
  }
}