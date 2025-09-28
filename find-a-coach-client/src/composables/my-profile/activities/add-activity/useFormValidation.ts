import type { Form } from '@/types/my-profile/add-activity/Form'
import type { ValidationError } from '@/types/ValidationError'
import type { Panel } from '@/types/my-profile/add-activity/Panel'

export default function useValidationOfActivityForm(formData: Form): ValidationError[] {
  const errors: ValidationError[] = []

  if (!isValidTitle(formData.title)) {
    errors.push({ fieldName: 'title', errorMessage: 'Title must be 3-50 characters.' })
  }

  if (formData.activityType == 'event') {
    if (!formData.dateOfBeginning) {
      errors.push({ fieldName: 'dateOfBeginning', errorMessage: 'Date of beginning is required.' })
    }
  }

  if (!isValidActivityType(formData.activityType)) {
    errors.push({ fieldName: 'activityType', errorMessage: 'Activity type is required.' })
  }

  if (formData.activityType == 'post' || formData.activityType == 'qa') {
    if (!formData.description) {
      errors.push({ fieldName: 'description', errorMessage: 'Description is required.' })
    }
    if (!isValidDescription(formData.description)) {
      errors.push({ fieldName: 'description', errorMessage: 'Panel description must not be longer than 200 characters.' })
    }
  }

  for (let i = 0; i < formData.subjects.length; i++) {
    if (!isValidSubject(formData.subjects[i])) {
      errors.push({ fieldName: `subjects[${i}]`, errorMessage: `Subject #${i + 1} must be 2-30 characters.` })
    }
  }

  for (let i = 0; i < formData.searchPeoplePanels.length; i++) {
    const panel: Panel = formData.searchPeoplePanels[i]

    if (!panel.nameOfPosition || panel.nameOfPosition.length < 2  || panel.nameOfPosition.length > 30) {
      errors.push({ fieldName: `panelsForSearchPeople[${i}].nameOfPosition`, errorMessage: 'Name of position must be at 2-30 characters.' })
    }

    if (panel.preferredSkills) {
      let skills: string[] = panel.preferredSkills.split(',')

      for (let j = 0; j < skills.length; j++) {
        if (skills[j].length > 30) {
          errors.push({
            fieldName: `panelsForSearchPeople[${i}].preferredSkills`,
            errorMessage: `Skill #${j + 1} is too long. Maximum length is 30 characters.`
          })
        }
      }

      if (skills.length > 5) {
        errors.push({ fieldName: `panelsForSearchPeople[${i}].preferredSkills`, errorMessage: 'Maximum 5 preferred skills allowed.' })
      }
    }

    if (!isValidPayment(panel.payment)) {
      errors.push({ fieldName: `panelsForSearchPeople[${i}].payment`, errorMessage: 'Too many characters entered in payment field.' })
    }

    if (!isValidDescription(panel.description)) {
      errors.push({ fieldName: `panelsForSearchPeople[${i}].description`, errorMessage: 'Panel description must not be longer than 200 characters.' })
    }
  }

  for (let i = 0; i < formData.surveyOptions.length; i++) {
    if (!isValidSurveyOption(formData.surveyOptions[i])) {
      errors.push({ fieldName: `surveyOptions[${i}]`, errorMessage: `Survey option #${i + 1} must be 2-100 characters.` })
    }
  }

  return errors
}

function isValidTitle(title: string): boolean {
  return title.length >= 3 && title.length <= 50
}

function isValidActivityType(type: string): boolean {
  return ['event', 'survey', 'qa', 'post'].includes(type)
}

function isValidDescription(desc: string): boolean {
  return desc.length <= 200
}

function isValidSubject(subject: string): boolean {
  return subject.length >= 2 && subject.length <= 30
}

function isValidPayment(payment: string): boolean {
  return payment.length <= 20
}

function isValidSurveyOption(option: string): boolean {
  return option.length >= 2 && option.length <= 100
}
