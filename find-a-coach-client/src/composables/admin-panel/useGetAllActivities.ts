import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { ActivityOfActivitiesList } from "@/types/my-profile/activities/ActivityOfActivitiesList"

const API_URL = config.apiBaseUrl + '/Admin'

export default async function useGetAllActivities(page: number, pageSize: number): Promise<Result | ActivityOfActivitiesList[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-all-activities?page=${page}&pageSize=${pageSize}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting all activities.',
      }
    }

    const data: ActivityOfActivitiesList[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}