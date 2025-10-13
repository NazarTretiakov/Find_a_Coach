import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Project } from "@/types/my-profile/projects/Project"

const API_URL = config.apiBaseUrl + '/Projects'

export default async function useGetProject(schoolId: string): Promise<Result | Project> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get?schoolId=${schoolId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting project.',
      }
    }

    const data: Project = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}