import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Comment } from '@/types/forum/Comment'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useToggleLikeOfActivity(activityId: string, userId: string, content: string): Promise<Comment | Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/add-comment`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ activityId, userId, content })
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while toggling like for activity.',
      }
    }

    const createdComment: Comment = await response.json()
    
    return createdComment
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}