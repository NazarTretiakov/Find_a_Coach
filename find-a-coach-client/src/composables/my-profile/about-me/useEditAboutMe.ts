import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useEditAboutMe(aboutMe: string): Promise<Result> {
  try {
    const token = await useEnsureValidToken()
    
    const formDataToSend = new FormData()
    formDataToSend.append('AboutMe', aboutMe ?? '')

    const response = await fetch(`${API_URL}/edit-about-me`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`
      },
      body: formDataToSend,
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occured while editing "About me" information.',
      }
    }

    return {
      isSuccessful: true,
      errorMessage: null,
    }
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}