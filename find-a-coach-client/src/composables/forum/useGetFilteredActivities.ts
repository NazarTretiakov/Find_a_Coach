import { Result } from "@/types/Result"
import useEnsureValidToken from '@/composables/authentication/useEnsureValidToken'
import { config } from '@/config'
import { ActivitiesPaged } from '@/types/my-profile/activities/ActivitiesPaged'

const API_URL = config.apiBaseUrl + '/Forum'

export default async function useGetActivityCards(searchString: string, page: number, pageSize: number): Promise<Result | ActivitiesPaged> {
  try {
    const token = await useEnsureValidToken()

    const encodedSearchString = encodeURIComponent(searchString)

    const response = await fetch(`${API_URL}/get-filtered-activities?searchString=${encodedSearchString}&page=${page}&pageSize=${pageSize}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting filtered activities.',
      }
    }

    const data: ActivitiesPaged = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}