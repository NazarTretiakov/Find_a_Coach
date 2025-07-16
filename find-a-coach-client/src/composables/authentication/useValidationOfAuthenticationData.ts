export default function useValidationOfAuthenticationData(email: string, password: string): { isSuccessful: boolean, errorMessage: string | null} {
  if (!isValidEmail(email)) {
    return { isSuccessful: false, errorMessage: 'Email is not in the correct format.' } 
  }
  if (!isValidPassword(password)) {
    return { isSuccessful: false, errorMessage: 'Password is not in the correct format. Password should have length between 8 and 50 symbols and contain one lowercase character, one uppercase character and one special symbol.' } 
  }

  return { isSuccessful: true, errorMessage: null}
}

function isValidEmail(email: string): boolean {
  if (email.length < 5 || email.length > 50) {
    return false
  }

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email)
}

function isValidPassword(password: string): boolean {
  if (password.length < 8 || password.length > 50) {
    return false
  }

  const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=\[\]{};':"\\|,.<>/?]).{8,}$/
  return passwordRegex.test(password)
}