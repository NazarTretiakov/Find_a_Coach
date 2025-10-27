import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useLeaveQAAnswer(creatorId: string, QAId: string, content: string): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/leave-qa-answer`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ creatorId, QAId, content })
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while leaving QA answer.',
      }
    }
    
    return { isSuccessful: true, errorMessage: null }
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}