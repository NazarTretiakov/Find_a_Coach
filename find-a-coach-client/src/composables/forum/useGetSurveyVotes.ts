import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import type { Vote } from '@/types/survey/Vote'

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useGetSurveyVotes(surveyId: string): Promise<Result | Vote[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-survey-votes?surveyId=${surveyId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while survey votes.',
      }
    }

    const data: Vote[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}