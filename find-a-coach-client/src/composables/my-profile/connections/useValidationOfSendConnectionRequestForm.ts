import { ConnectionForm } from '@/types/my-profile/connections/ConnectionForm'
import type { ValidationError } from '@/types/ValidationError'

export default function useValidationOfSendConnectionRequestForm(formData: ConnectionForm): ValidationError[] {
  const errors: ValidationError[] = []

  if (formData.message.length > 1000) {
    errors.push({
      fieldName: 'message',
      errorMessage: 'Message of recommendation cannot exceed 1000 characters.'
    })
  }

  return errors
}