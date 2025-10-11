import type { EducationForm } from '@/types/my-profile/education/EducationForm'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfAddEducationForm(formData: EducationForm): ValidationError[] {
  const errors: ValidationError[] = []

  if (!formData.schoolName || formData.schoolName.trim() === '') {
    errors.push({
      fieldName: 'school',
      errorMessage: 'School name is required.'
    })
  } else if (formData.schoolName.length > 60) {
    errors.push({
      fieldName: 'school',
      errorMessage: 'School name cannot exceed 60 characters.'
    })
  }

  if (!formData.degree || formData.degree.trim() === '') {
    errors.push({
      fieldName: 'degree',
      errorMessage: 'Degree must be selected.'
    })
  }

  if (!formData.fieldOfStudy || formData.fieldOfStudy.trim() === '') {
    errors.push({
      fieldName: 'school',
      errorMessage: 'Field of study is required.'
    })
  } else if (formData.fieldOfStudy.length > 60) {
    errors.push({
      fieldName: 'school',
      errorMessage: 'Field of study cannot exceed 60 characters.'
    })
  }

  if (!formData.location || formData.location.trim() === '') {
    errors.push({
      fieldName: 'location',
      errorMessage: 'Location is required.'
    })
  } else if (formData.location.length > 40) {
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