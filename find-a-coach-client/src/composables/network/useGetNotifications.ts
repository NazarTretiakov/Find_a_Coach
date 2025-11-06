import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import type { Notification } from '@/types/network/Notification'

const API_URL = config.apiBaseUrl + '/Network'

export default async function useGetNotifications(userId: string, page: number, pageSize: number): Promise<Result | Notification[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-notifications?userId=${userId}&page=${page}&pageSize=${pageSize}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting notifications.',
      }
    }

    const data: Notification[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}