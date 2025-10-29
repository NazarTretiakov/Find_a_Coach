import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { EventApplication } from "@/types/forum/EventApplication"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useGetSearchPersonPanelApplications(searchPersonPanelId: string): Promise<Result | EventApplication[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-applications?searchPersonPanelId=${searchPersonPanelId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting applications of search person panel.',
      }
    }

    const data: EventApplication[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}