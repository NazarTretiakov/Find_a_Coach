import type { CertificationForm } from '@/types/my-profile/certifications/CertificationForm'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfAddCertificationForm(formData: CertificationForm): ValidationError[] {
  const errors: ValidationError[] = []

  if (!formData.title || formData.title.trim() === '') {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Title is required.'
    })
  } else if (formData.title.length > 60) {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Title cannot exceed 60 characters.'
    })
  }

  if (!formData.issuingOrganization || formData.issuingOrganization.trim() === '') {
    errors.push({
      fieldName: 'organization',
      errorMessage: 'Issuing organization is required.'
    })
  } else if (formData.issuingOrganization.length > 30) {
    errors.push({
      fieldName: 'organization',
      errorMessage: 'Organization name cannot exceed 30 characters.'
    })
  }

  if (!formData.issueDate) {
    errors.push({
      fieldName: 'issueDate',
      errorMessage: 'Issue date is required.'
    })
  } else {
    const issue = new Date(formData.issueDate)
    const now = new Date()
    if (issue > now) {
      errors.push({
        fieldName: 'issueDate',
        errorMessage: 'Issue date cannot be in the future.'
      })
    }
  }

  if (formData.image) {
    const allowedTypes = ['image/jpeg', 'image/png', 'image/webp', 'image/jpg']
    if (!allowedTypes.includes(formData.image.type)) {
      errors.push({
        fieldName: 'image',
        errorMessage: 'Only JPEG, PNG, or WEBP images are allowed.'
      })
    }
    if (formData.image.size > 5 * 1024 * 1024) {
      errors.push({
        fieldName: 'image',
        errorMessage: 'Image size cannot exceed 5 MB.'
      })
    }
  }

  if (!formData.credentialId || formData.credentialId.trim() === '') {
    errors.push({
      fieldName: 'credentialId',
      errorMessage: 'Credential ID is required.'
    })
  } else if (formData.credentialId.length > 200) {
    errors.push({
      fieldName: 'credentialId',
      errorMessage: 'Credential ID cannot exceed 200 characters.'
    })
  }

  if (!formData.credentialUrl || formData.credentialUrl.trim() === '') {
    errors.push({
      fieldName: 'credentialUrl',
      errorMessage: 'Credential URL is required.'
    })
  } else if (formData.credentialUrl.length > 2048) {
    errors.push({
      fieldName: 'credentialUrl',
      errorMessage: 'Credential URL cannot exceed 2048 characters.'
    })
  }

  if (formData.skills.length > 0) {
    formData.skills.forEach((skill, index) => {
      if (!skill || skill.trim() === '') {
        errors.push({
          fieldName: `skills[${index}]`,
          errorMessage: `Skill #${index + 1} cannot be empty.`
        })
      } else if (skill.length > 50) {
        errors.push({
          fieldName: `skills[${index}]`,
          errorMessage: `Skill #${index + 1} cannot exceed 50 characters.`
        })
      }
    })
  }

  return errors
}