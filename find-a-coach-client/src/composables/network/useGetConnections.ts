import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import type { Connection } from '@/types/network/Connection'

const API_URL = config.apiBaseUrl + '/Network'

export default async function useGetConnections(userId: string, page: number, pageSize: number): Promise<Result | Connection[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-connections?userId=${userId}&page=${page}&pageSize=${pageSize}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting connections.',
      }
    }

    const data: Connection[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}