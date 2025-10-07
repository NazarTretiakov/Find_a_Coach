import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useDeleteComment(commentId: string, userId: string): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/delete-comment`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ commentId, userId })
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while deleting comment.',
      }
    }
    
    return {
      isSuccessful: true,
      errorMessage: null
    }
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}