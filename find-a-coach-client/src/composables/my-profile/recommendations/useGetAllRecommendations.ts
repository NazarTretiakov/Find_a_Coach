import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Recommendation } from "@/types/my-profile/recommendations/Recommendation"

const API_URL = config.apiBaseUrl + '/Recommendations'

export default async function useGetAllRecommendations(userId: string): Promise<Result | Recommendation[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-all?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting recommendations.',
      }
    }

    const data: Recommendation[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}