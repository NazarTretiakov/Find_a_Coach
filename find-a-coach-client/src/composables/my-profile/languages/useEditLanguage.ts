import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { LanguageForm } from "@/types/my-profile/languages/LanguageForm"

const API_URL = config.apiBaseUrl + '/Languages'

export default async function useEditLanguages(languageId: string, formData: LanguageForm): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const bodyData = {
      languageId,
      ...formData
    }
    
    console.log(bodyData)

    const response = await fetch(`${API_URL}/edit`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(bodyData)
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while editing the language.',
      }
    }

    return { isSuccessful: true, errorMessage: '' }
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}