import type { AddPositionForm } from '@/types/my-profile/experience/AddPositionForm'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfAddPositionForm(formData: AddPositionForm): ValidationError[] {
  const errors: ValidationError[] = []

  if (!formData.title || formData.title.trim() === '') {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Title is required.'
    })
  } else if (formData.title.length > 40) {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Title cannot exceed 40 characters.'
    })
  }

  if (!formData.employmentType || formData.employmentType.trim() === '') {
    errors.push({
      fieldName: 'employmentType',
      errorMessage: 'Employment type must be selected.'
    })
  }

  if (!formData.companyName || formData.companyName.trim() === '') {
    errors.push({
      fieldName: 'companyName',
      errorMessage: 'Company name is required.'
    })
  }

  if (!formData.startDate) {
    errors.push({
      fieldName: 'startDate',
      errorMessage: 'Start date is required.'
    })
  }

  if (!formData.isCurrentlyWorkingHere && !formData.endDate) {
    errors.push({
      fieldName: 'endDate',
      errorMessage: 'End date is required if not currently working in this role.'
    })
  }

  if (formData.startDate && formData.endDate) {
    const start = new Date(formData.startDate)
    const end = new Date(formData.endDate)
    if (end < start) {
      errors.push({
        fieldName: 'endDate',
        errorMessage: 'End date cannot be earlier than start date.'
      })
    }
  }

  if (formData.location.length > 40) {
    errors.push({
      fieldName: 'location',
      errorMessage: 'Location cannot exceed 40 characters.'
    })
  }

  if (formData.description.length > 200) {
    errors.push({
      fieldName: 'description',
      errorMessage: 'Description cannot exceed 200 characters.'
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