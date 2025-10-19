import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Language } from "@/types/my-profile/languages/Language"

const API_URL = config.apiBaseUrl + '/Languages'

export default async function useGetAllLanguages(userId: string): Promise<Result | Language[]> {
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
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting languages.',
      }
    }

    const data: Language[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}