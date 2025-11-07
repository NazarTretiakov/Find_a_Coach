import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Network'

export default async function useCheckUserUnreadNotifications(userId: string): Promise<Result | boolean> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/is-user-has-unread-notifications`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(userId)
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while checking unread notifications.',
      }
    }
    const data = await response.json();
    
    return data.isUserHasUnreadNotifications
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}