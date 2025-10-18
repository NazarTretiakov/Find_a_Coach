import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Skill } from "@/types/my-profile/skills/Skill"

const API_URL = config.apiBaseUrl + '/Skills'

export default async function useGetAllSkills(userId: string): Promise<Result | Skill[]> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-all?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting skills of user.',
      }
    }

    const data: Skill[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}