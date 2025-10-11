import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { School } from "@/types/my-profile/education/School"

const API_URL = config.apiBaseUrl + '/Education'

export default async function useGetLastTwoSchools(userId: string): Promise<Result | School[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-last-two-schools?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting schools.',
      }
    }

    const data: School[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}