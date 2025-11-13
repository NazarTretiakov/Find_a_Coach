import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Network'

export default async function useCheckUserUnreadNotifications(userId: string, connectedUserId: string): Promise<Result | boolean> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/is-users-connected?userId=${userId}&connectedUserId=${connectedUserId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while checking if users connected.',
      }
    }
    const data = await response.json()

    if (data.isUsersConnected == false) {
      return false
    } else if (data.status != 'Accepted') {
      return false
    }
    
    return true
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}