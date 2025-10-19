import type { LanguageForm } from '@/types/my-profile/languages/LanguageForm'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfAddLanguageForm(formData: LanguageForm): ValidationError[] {
  const errors: ValidationError[] = []

  if (!formData.title || formData.title.trim() === '') {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Language name is required.'
    })
  } else if (formData.title.length > 40) {
    errors.push({
      fieldName: 'title',
      errorMessage: 'Language name cannot exceed 40 characters.'
    })
  }

  if (!formData.proficiency || formData.proficiency.trim() === '') {
    errors.push({
      fieldName: 'proficiency',
      errorMessage: 'You must select a proficiency level.'
    })
  } else if (!['A1', 'A2', 'B1', 'B2', 'C1', 'C2'].includes(formData.proficiency)) {
    errors.push({
      fieldName: 'proficiency',
      errorMessage: 'Invalid proficiency level selected.'
    })
  }

  return errors
}