import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import type { PositionForm } from '@/types/my-profile/experience/PositionForm'

const API_URL = config.apiBaseUrl + '/Experience'

export default async function useEditPosition(positionId: string, formData: PositionForm): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const bodyData = {
      positionId,
      title: formData.title,
      employmentType: formData.employmentType,
      companyName: formData.companyName,
      isCurrentlyWorkingHere: formData.isCurrentlyWorkingHere,
      startDate: `${formData.startDate}T00:00:00`,
      endDate: `${formData.endDate}T00:00:00`,
      location: formData.location,
      description: formData.description,
      skillTitles: formData.skills
    }
    
    console.log(bodyData)

    const response = await fetch(`${API_URL}/edit-position`, {
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
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while editing the position.',
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