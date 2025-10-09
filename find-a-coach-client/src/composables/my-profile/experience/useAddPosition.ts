import { Result } from "@/types/Result"
import type { AddPositionForm } from "@/types/my-profile/experience/AddPositionForm"
import useEnsureValidToken from '@/composables/authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/Experience'

export default async function useAddPosition(formData: AddPositionForm): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/add-position`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        title: formData.title,
        employmentType: formData.employmentType,
        companyName: formData.companyName,
        isCurrentlyWorkingHere: formData.isCurrentlyWorkingHere,
        startDate: formData.startDate,
        endDate: formData.endDate,
        location: formData.location,
        description: formData.description,
        skillTitles: formData.skills
      })
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while adding position.'
      }
    }

    return {
      isSuccessful: true,
      errorMessage: null
    }

  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message
    }
  }
}