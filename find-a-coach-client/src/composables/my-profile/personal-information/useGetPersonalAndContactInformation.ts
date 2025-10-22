import { PersonalAndContactInformation } from "@/types/my-profile/personal-information/PersonalAndContactInformation"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from "@/types/Result"

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useGetPersonalAndContactInformation(userId: string): Promise<PersonalAndContactInformation | Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-personal-and-contact-information?userId=${userId}`, {
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

    const data: PersonalAndContactInformation = await response.json()
    
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}