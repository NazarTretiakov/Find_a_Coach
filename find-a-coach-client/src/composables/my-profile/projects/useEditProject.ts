import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import type { ProjectForm } from "@/types/my-profile/projects/ProjectForm"

const API_URL = config.apiBaseUrl + '/Projects'

export default async function useEditProject(projectId: string, formData: ProjectForm): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const bodyData = {
      projectId,
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
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while editing the project.',
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