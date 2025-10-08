import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Survey } from '@/types/forum/Survey'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useGetSurvey(id: string): Promise<Survey | Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get?id=${id}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while getting survey.',
      }
    }

    const survey: Survey = await response.json()
    
    return survey
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}