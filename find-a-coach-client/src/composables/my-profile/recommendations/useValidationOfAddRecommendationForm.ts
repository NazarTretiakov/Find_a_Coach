import type { RecommendationForm } from '@/types/my-profile/recommendations/RecommendationForm'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfAddRecommendationForm(formData: RecommendationForm): ValidationError[] {
  const errors: ValidationError[] = []

  if (!formData.content || formData.content.trim() === '') {
    errors.push({
      fieldName: 'content',
      errorMessage: 'Content of recommendation is required.'
    })
  } else if (formData.content.length > 1000) {
    errors.push({
      fieldName: 'content',
      errorMessage: 'Content of recommendation cannot exceed 1000 characters.'
    })
  } else if (formData.content.length < 10) {
    errors.push({
      fieldName: 'content',
      errorMessage: 'Content of recommendation cannot be less than 10 characters.'
    })
  }

  return errors
}