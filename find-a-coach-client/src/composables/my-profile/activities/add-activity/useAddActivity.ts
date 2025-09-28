import { Result } from "@/types/Result"
import { Form } from "@/types/my-profile/add-activity/Form"
import useEnsureValidToken from '../../../authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/Activities'

export default async function useAddActivity(formData: Form): Promise<Result> {
  try {
    const formDataToSend = new FormData()

    if (formData.image) {
      formDataToSend.append('Image', formData.image as File)
    }

    formDataToSend.append('Title', formData.title)
    formDataToSend.append('ActivityType', formData.activityType)
    formDataToSend.append('Description', formData.description ?? '')
    
    if (formData.dateOfBeginning) {
      const date = new Date(formData.dateOfBeginning as unknown as string) 
      formDataToSend.append('BeginningDate', date.toISOString())
    }

    formData.subjects.forEach((subject, index) => {
      formDataToSend.append(`Subjects[${index}].Title`, subject)
    })

    formData.searchPeoplePanels.forEach((panel, index) => {
      formDataToSend.append(`SearchPersonPanels[${index}].PositionName`, panel.nameOfPosition)
      formDataToSend.append(`SearchPersonPanels[${index}].PreferredSkills`, panel.preferredSkills)
      formDataToSend.append(`SearchPersonPanels[${index}].Payment`, panel.payment)
      formDataToSend.append(`SearchPersonPanels[${index}].Description`, panel.description)
    })

    formData.surveyOptions.forEach((option, index) => {
      formDataToSend.append(`SurveyOptions[${index}].Inscription`, option)
    })

    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/add`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`
      },
      body: formDataToSend,
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while adding activity.',
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
