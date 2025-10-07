import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Event } from '@/types/forum/Event'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useGetEvent(id: string): Promise<Event | Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get?id=${id}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while getting personal information.',
      }
    }

    const event: Event = await response.json()
    
    return event
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}