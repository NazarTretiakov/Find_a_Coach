import { Result } from "@/types/Result"
import type { ActivityCard } from "@/types/my-profile/activities/ActivityCard"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useGetActivityCards(userId: string): Promise<Result | ActivityCard[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-last-two-activities?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting activity cards.',
      }
    }

    const data: ActivityCard[] = await response.json()
    
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}