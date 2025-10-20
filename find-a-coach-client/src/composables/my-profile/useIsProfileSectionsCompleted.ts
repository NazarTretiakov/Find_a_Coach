import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { IsProfileSectionsCompleted } from "@/types/my-profile/IsProfileSectionsCompleted"

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useIsProfileSectionsCompleted(): Promise<Result | IsProfileSectionsCompleted> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/is-profile-sections-completed`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while information about what profile sections are completed.',
      }
    }

    const data: IsProfileSectionsCompleted = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}