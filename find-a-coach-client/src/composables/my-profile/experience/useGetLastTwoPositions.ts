import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Position } from "@/types/my-profile/experience/Position"

const API_URL = config.apiBaseUrl + '/Experience'

export default async function useGetLastTwoPositions(userId: string): Promise<Result | Position[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-last-two-positions?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting positions.',
      }
    }

    const data: Position[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}