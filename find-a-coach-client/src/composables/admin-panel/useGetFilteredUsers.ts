import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { User } from "@/types/admin-panel/User"

const API_URL = config.apiBaseUrl + '/Admin'

export default async function useGetFilteredUsers(searchString: string, page: number, pageSize: number): Promise<Result | User[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-filtered-users?searchString=${searchString}&page=${page}&pageSize=${pageSize}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting filtered users.',
      }
    }

    const data: User[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}