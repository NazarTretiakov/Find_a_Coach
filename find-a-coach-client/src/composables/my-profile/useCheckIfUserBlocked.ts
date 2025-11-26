import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useCheckIfUserBlocked(userId: string): Promise<Result | boolean> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/is-user-blocked?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while checking if user blocked.',
      }
    }
    const data = await response.json();
    
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}