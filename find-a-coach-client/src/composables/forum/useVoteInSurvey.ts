import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"
import type { Vote } from '@/types/survey/Vote'

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useToggleSaveOfActivity(optionId: string): Promise<Vote[] | Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/vote-in-survey`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ optionId })
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while voting in survey.',
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