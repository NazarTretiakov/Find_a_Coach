import { Result } from "@/types/Result"
import useEnsureValidToken from '../../authentication/useEnsureValidToken'
import { config } from '@/config'
import { CertificationForm } from "@/types/my-profile/certifications/CertificationForm"

const API_URL = config.apiBaseUrl + '/Certifications'

export default async function useEditCertification(certificationId: string, formData: CertificationForm): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const body = new FormData()
    body.append('CertificationId', certificationId)
    body.append('Title', formData.title)
    body.append('IssuingOrganization', formData.issuingOrganization)
    body.append('IssueDate', formData.issueDate?.toString() || '')
    if (formData.image) {
      body.append('Image', formData.image)
    } else {
      body.append('Image', new Blob())
    }
    body.append('CredentialId', formData.credentialId)
    body.append('CredentialUrl', formData.credentialUrl)

    formData.skills.forEach(skill => {
      body.append('SkillTitles', skill)
    })

    const response = await fetch(`${API_URL}/edit`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`
      },
      body
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while editing the certification.',
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