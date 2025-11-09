import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import type { NetworkOverview } from '@/types/network/NetworkOverview'

const API_URL = config.apiBaseUrl + '/Network'

export default async function useGetNetworkOverview(userId: string): Promise<Result | NetworkOverview> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-network-overview?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting network overview.',
      }
    }

    const data: NetworkOverview = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}