import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Post } from '@/types/forum/Post'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useGetPost(id: string): Promise<Post | Result> {
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
        errorMessage: responseData.title || 'Unexpected error occured while getting event.',
      }
    }

    const post: Post = await response.json()
    
    return post
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}