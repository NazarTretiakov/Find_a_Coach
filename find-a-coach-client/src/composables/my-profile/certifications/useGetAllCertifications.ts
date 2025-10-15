import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Certification } from "@/types/my-profile/certifications/Certification"

const API_URL = config.apiBaseUrl + '/Certifications'

export default async function useGetAllCertifications(userId: string): Promise<Result | Certification[]> {
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
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting certifications.',
      }
    }

    const data: Certification[] = await response.json()
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}