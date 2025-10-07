import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useToggleLikeOfActivity(activityId: string, userId: string): Promise<{ numberOfLikes: number, isLiked: boolean } | Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/toggle-like`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ activityId, userId })
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while toggling like for activity.',
      }
    }

    const likesInformation: { numberOfLikes: number, isLiked: boolean } = await response.json()
    
    return likesInformation
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}