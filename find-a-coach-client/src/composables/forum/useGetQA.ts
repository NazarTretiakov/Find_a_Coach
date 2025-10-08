import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import type { QA } from '@/types/forum/QA'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useGetQA(id: string): Promise<QA | Result> {
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
        errorMessage: responseData.title || 'Unexpected error occured while getting QA.',
      }
    }

    const qa: QA = await response.json()
    
    return qa
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}