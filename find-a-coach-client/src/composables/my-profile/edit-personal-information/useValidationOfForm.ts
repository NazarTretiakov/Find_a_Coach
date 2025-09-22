import { Form } from "../../../types/my-profile/personal-information/edit-personal-information/Form"
import { Website } from "../../../types/my-profile/personal-information/edit-personal-information/Website"
import { ValidationError } from "../../../types/my-profile/personal-information/edit-personal-information/ValidationError"


export default function useValidationOfForm(formData: Form): ValidationError[] {
  const errors: ValidationError[] = []

  if (!isValidName(formData.firstName)) {
    errors.push({ fieldName: 'firstName', errorMessage: 'First name must be 2-30 characters.' })
  }

  if (!isValidName(formData.lastName)) {
    errors.push({ fieldName: 'lastName', errorMessage: 'Last name must be 2-30 characters.' })
  }

  if (!isValidPrimaryOccupation(formData.primaryOccupation)) {
    errors.push({ fieldName: 'primaryOccupation', errorMessage: 'Primary occupation must be 4-30 characters.' })
  }

  if (!isValidHeadline(formData.headline)) {
    errors.push({ fieldName: 'headline', errorMessage: 'Headline must be 4-100 characters.' })
  }

  if (!isValidLocation(formData.location)) {
    errors.push({ fieldName: 'location', errorMessage: 'Location must be 4-50 characters.' })
  }

  if (!isValidPhoneNumber(formData.phone)) {
    errors.push({ fieldName: 'phone', errorMessage: 'Phone number must have exactly 9 digits.' })
  }

  for (let i = 0; i < formData.websites.length; i++) {
    if (!isValidWebsite(formData.websites[i])) {
      errors.push({ fieldName: `websites[${i}]`, errorMessage: `Website #${i + 1} is not valid.` })
    }
  }

  return errors
}


function isValidName(name: string): boolean {
  if (name.length < 2 || name.length > 30) {
    return false
  }
  return true
}

function isValidPrimaryOccupation(occupation: string): boolean {
  if (occupation.length > 30) {
    return false
  }
  return true
}

function isValidHeadline(headline: string): boolean {
  if (headline.length < 4 || headline.length > 100) {
    return false
  }
  return true
}

function isValidLocation(location: string): boolean {
  if (location.length < 4 || location.length > 50) {
    return false
  }
  return true
}

function isValidPhoneNumber(phoneNumber: string): boolean {
  if (phoneNumber.length < 9) {
    return false
  }

  const phoneRegex = /^\d{9}$/
  return phoneRegex.test(phoneNumber)
}

function isValidWebsite(website: Website): boolean {
  if (!website.url) {
    return false
  } 
  if (website.type == 'other' || website.type == 'company' || website.type == 'portfolio' || website.type == 'personal' ) {
    return true
  } else {
    return false
  }
}