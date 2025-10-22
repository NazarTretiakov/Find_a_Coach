import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { Result } from '@/types/Result'
import { ContactInformation } from '@/types/my-profile/contact-information/ContactInformation'

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useGetContactInformation(userId: string): Promise<ContactInformation | Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-contact-information?userId=${userId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.title || 'Unexpected error occured while getting contact information.',
      }
    }

    const data: ContactInformation = await response.json()
    
    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}