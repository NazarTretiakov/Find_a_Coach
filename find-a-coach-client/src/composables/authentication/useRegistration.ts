import useValidationOfAuthenticationData from './useValidationOfAuthenticationData'
import { useAuthenticationStore } from '../../stores/authentication'

import type { AuthenticationStore } from '../../types/authentication/AuthenticationStore'
import type { AuthenticationDTO } from '../../types/authentication/AuthenticationDTO'


export default async function useRegistration(email: string, password: string): Promise<{ isSuccessful: boolean, errorMessage: string | null}> {
  const store: AuthenticationStore = useAuthenticationStore()
  
  const validationResult = useValidationOfAuthenticationData(email, password)
  if (!validationResult.isSuccessful) {
    return validationResult
  }

  const registrationPayload: AuthenticationDTO = {
    email: email,
    password: password
  }

  try {
    const result: { isSuccessful: boolean, errorMessage: string | null} = await store.register(registrationPayload)
    return result
  } catch (err) {
    if (err instanceof Error) {
      return { isSuccessful: false, errorMessage: err.message }
    } else {
      return { isSuccessful: false, errorMessage: 'An unexpected error occurred' }
    }
  } 
}