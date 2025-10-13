import type { ProjectForm } from '@/types/my-profile/projects/ProjectForm'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfAddProjectForm(formData: ProjectForm): ValidationError[] {
  const errors: ValidationError[] = []

  if (!formData.title || formData.title.trim() === '') {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Project title is required.'
    })
  } else if (formData.title.length > 60) {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Project title cannot exceed 60 characters.'
    })
  }

  if (!formData.relatedTo || formData.relatedTo.trim() === '') {
    errors.push({
      fieldName: 'relatedTo',
      errorMessage: 'You must select what the project is related to.'
    })
  }

  if (formData.location.length > 40) {
    errors.push({
      fieldName: 'location',
      errorMessage: 'Location cannot exceed 40 characters.'
    })
  }

  if (!formData.startDate) {
    errors.push({
      fieldName: 'startDate',
      errorMessage: 'Start date is required.'
    })
  }

  if (!formData.endDate) {
    errors.push({
      fieldName: 'endDate',
      errorMessage: 'End date is required.'
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

  if (formData.description && formData.description.length > 500) {
    errors.push({
      fieldName: 'description',
      errorMessage: 'Description cannot exceed 500 characters.'
    })
  }

  if (formData.skillTitles.length > 0) {
    formData.skillTitles.forEach((skill, index) => {
      if (!skill || skill.trim() === '') {
        errors.push({
          fieldName: `skillTitles[${index}]`,
          errorMessage: `Skill #${index + 1} cannot be empty.`
        })
      } else if (skill.length > 50) {
        errors.push({
          fieldName: `skillTitles[${index}]`,
          errorMessage: `Skill #${index + 1} cannot exceed 50 characters.`
        })
      }
    })
  }

  if (formData.participants.length > 0) {
    formData.participants.forEach((person, index) => {
      if (!person || person.trim() === '') {
        errors.push({
          fieldName: `participants[${index}]`,
          errorMessage: `Participant #${index + 1} cannot be empty.`
        })
      } else if (person.length > 50) {
        errors.push({
          fieldName: `participants[${index}]`,
          errorMessage: `Participant #${index + 1} cannot exceed 50 characters.`
        })
      }
    })
  }

  return errors
}