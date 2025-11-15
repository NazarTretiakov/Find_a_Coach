import { SecuritySettings } from '@/types/my-profile/settings/SecuritySettings'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfEditSecuritySettingsForm(formData: SecuritySettings): ValidationError[] {
  const errors: ValidationError[] = []

  if (!formData.oldPassword || formData.oldPassword.trim() === '') {
    errors.push({
      fieldName: 'oldPassword',
      errorMessage: 'Old password is required.'
    })
  }

    if (!formData.newPassword || formData.newPassword.trim() === '') {
    errors.push({
      fieldName: 'newPassword',
      errorMessage: 'New password is required.'
    })
  } else {
    if (formData.newPassword.length < 8) {
      errors.push({
        fieldName: 'newPassword',
        errorMessage: 'New password must be at least 8 characters long.'
      })
    }
  }

  const hasUpper = /[A-Z]/.test(formData.newPassword)
  const hasLower = /[a-z]/.test(formData.newPassword)
  const hasDigit = /[0-9]/.test(formData.newPassword)
  const hasSpecial = /[^A-Za-z0-9]/.test(formData.newPassword)

  if (!hasUpper || !hasLower || !hasDigit || !hasSpecial) {
    errors.push({
      fieldName: 'newPassword',
      errorMessage: 'New password must include uppercase and lowercase letters, digits, and at least one special character.'
    })
  }

  if (!formData.repeatNewPassword || formData.repeatNewPassword.trim() === '') {
    errors.push({
      fieldName: 'repeatNewPassword',
      errorMessage: 'Please repeat the new password.'
    })
  } else if (formData.repeatNewPassword !== formData.newPassword) {
    errors.push({
      fieldName: 'repeatNewPassword',
      errorMessage: 'Passwords do not match.'
    })
  }

  return errors
}