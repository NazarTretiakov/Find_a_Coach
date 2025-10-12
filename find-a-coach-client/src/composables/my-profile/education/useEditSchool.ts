import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { EducationForm } from '@/types/my-profile/education/EducationForm'

const API_URL = config.apiBaseUrl + '/Education'

export default async function useEditPosition(schoolId: string, formData: EducationForm): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const bodyData = {
      schoolId,
      schoolName: formData.schoolName,
      degree: formData.degree,
      fieldOfStudy: formData.fieldOfStudy,
      startDate: `${formData.startDate}T00:00:00`,
      endDate: `${formData.endDate}T00:00:00`,
      location: formData.location,
      skillTitles: formData.skills
    }
    
    console.log(bodyData)

    const response = await fetch(`${API_URL}/edit-school`, {
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
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while editing the school.',
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