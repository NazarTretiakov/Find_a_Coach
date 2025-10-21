import { useAuthenticationStore } from './index';
import type { AuthenticationDTO } from '../../types/authentication/AuthenticationDTO'
import { Result } from '@/types/Result';
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/Authentication'

export default {
  async register(payload: AuthenticationDTO): Promise<Result> {
    try {
      const response = await fetch(`${API_URL}/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        },
        body: JSON.stringify({
          email: payload.email,
          password: payload.password,
        }),
      })

      if (!response.ok) {
        const responseData = await response.json();
        return {
          isSuccessful: false,
          errorMessage: responseData.errorMessage || 'Unexpected error occured while registration.',
        }
      }

      return {
        isSuccessful: true,
        errorMessage: null,
      }
    } catch (error) {
      return {
        isSuccessful: false,
        errorMessage: (error as Error).message,
      }
    }
  },

  async login(payload: AuthenticationDTO): Promise<Result> {
    try {
      const authenticationStore = useAuthenticationStore()

      const response = await fetch(`${API_URL}/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        },
        body: JSON.stringify({
          email: payload.email,
          password: payload.password,
        }),
      })

      const responseData = await response.json()

      if (!response.ok) {
        return {
          isSuccessful: false,
          errorMessage: responseData.errorMessage || 'Unexpected error occured while logging in.',
        }
      }
      console.log(responseData)

      authenticationStore.writeAllFieldsInStore(responseData.email, responseData.role, responseData.token, new Date(responseData.expiration), responseData.refreshToken, new Date(responseData.refreshTokenExpirationDateTime))
      authenticationStore.saveAuthenticationStateInLocalStore()

      return {
        isSuccessful: true,
        errorMessage: null,
      }
    } catch (error) {
      return {
        isSuccessful: false,
        errorMessage: (error as Error).message,
      }
    }
  },

  async logout(): Promise<Result> {
    const authenticationStore = useAuthenticationStore()

    try {
      if (!authenticationStore.isAuthenticated) {
        return {
          isSuccessful: false,
          errorMessage: 'User isn\'t logged in. User has to be logged into accout in order to be able to log out.',
        }
      }

      const response = await fetch(`${API_URL}/logout`, {
        method: 'POST',
        headers: {
          'Authorization': `Bearer ${authenticationStore.token}`,
          'Accept': 'application/json'
        }
      })

      authenticationStore.clearAllFieldsInStore()
      authenticationStore.clearAuthenticationStateFromLocalStore()

      if (!response.ok) {
        return {
          isSuccessful: false,
          errorMessage: 'Unexpected error occured while logging out.',
        }
      }

      return {
        isSuccessful: true,
        errorMessage: null
      }
    } catch (error) {
      
      authenticationStore.clearAllFieldsInStore()
      authenticationStore.clearAuthenticationStateFromLocalStore()

      return {
        isSuccessful: false,
        errorMessage: (error as Error).message
      }
    }
  },

  async resendConfirmationEmail(email: string): Promise<Result> {
    try {
      const response = await fetch(`${API_URL}/resend-confirmation-email?email=${encodeURIComponent(email)}`, {
        method: 'POST',
        headers: {
          'Accept': 'application/json'
        }
      })

      if (!response.ok) {
        const responseData = await response.json();
        return {
          isSuccessful: false,
          errorMessage: responseData.message || 'Failed to resend confirmation email.',
        }
      }

      return {
        isSuccessful: true,
        errorMessage: null,
      };
    } catch (error) {
      return {
        isSuccessful: false,
        errorMessage: (error as Error).message,
      }
    }
  },

  async sendForgotPasswordEmail(email: string): Promise<Result> {
    try {
      const response = await fetch(`${API_URL}/forgot-password`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        },
        body: JSON.stringify({ email })
      })

      if (!response.ok) {
        const responseData = await response.json()
        return {
          isSuccessful: false,
          errorMessage: responseData.message || 'Error occurred while sending password reset request.',
        }
      }

      return {
        isSuccessful: true,
        errorMessage: null,
      }
    } catch (error) {
      return {
        isSuccessful: false,
        errorMessage: (error as Error).message,
      }
    }
  },

  async resetPassword(email: string, token: string, newPassword: string): Promise<Result> {
    const authenticationStore = useAuthenticationStore()
    
    try {
      const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=\[\]{};':"\\|,.<>/?]).{8,}$/

      if (newPassword.length < 8 || newPassword.length > 50 || !passwordRegex.test(newPassword)) {
        return {
          isSuccessful: false,
          errorMessage: 'Password is not in the correct format. Password should have length between 8 and 50 symbols and contain one lowercase character, one uppercase character and one special symbol.'
        }
      }

      const response = await fetch(`${API_URL}/reset-password`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        },
        body: JSON.stringify({
          email,
          token,
          newPassword
        })
      })

      const responseData = await response.json();

      if (!response.ok) {
        return {
          isSuccessful: false,
          errorMessage: responseData.errorMessage || 'Failed to reset password.',
        }
      }

      authenticationStore.writeAllFieldsInStore(responseData.email, responseData.role, responseData.token, new Date(responseData.expiration), responseData.refreshToken, new Date(responseData.refreshTokenExpirationDateTime))
      authenticationStore.saveAuthenticationStateInLocalStore()

      return {
        isSuccessful: true,
        errorMessage: null
      }
    } catch (error) {
      return {
        isSuccessful: false,
        errorMessage: (error as Error).message
      }
    }
  },

  async refreshJWTToken(): Promise<Result> {
    const authenticationStore = useAuthenticationStore()

    if (!authenticationStore.token || !authenticationStore.refreshToken) {
      return {
        isSuccessful: false,
        errorMessage: 'Missing JWT or refresh token.',
      }
    }

    try {
      const response = await fetch(`${API_URL}/refresh-jwt-token`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        },
        body: JSON.stringify({
          jwtToken: authenticationStore.token,
          refreshToken: authenticationStore.refreshToken,
        }),
      })

      const responseData = await response.json()

      if (!response.ok) {
        return {
          isSuccessful: false,
          errorMessage: responseData.errorMessage || 'Failed to refresh token.',
        }
      }

      authenticationStore.writeAllFieldsInStore(responseData.email, responseData.role, responseData.token, new Date(responseData.expiration), responseData.refreshToken, new Date(responseData.refreshTokenExpirationDateTime))
      authenticationStore.saveAuthenticationStateInLocalStore()

      return {
        isSuccessful: true,
        errorMessage: null,
      }

    } catch (error) {
      return {
        isSuccessful: false,
        errorMessage: (error as Error).message,
      }
    }
  },

  writeAllFieldsInStore(email: string, role: string, token: string, expiration: Date, refreshToken: string, refreshTokenExpirationDateTime: Date): void {
    const authenticationStore = useAuthenticationStore()
    
    authenticationStore.email = email
    authenticationStore.role = role
    authenticationStore.token = token
    authenticationStore.expiration = expiration
    authenticationStore.refreshToken = refreshToken
    authenticationStore.refreshTokenExpirationDateTime = refreshTokenExpirationDateTime
  },

  clearAllFieldsInStore(): void {
    const authenticationStore = useAuthenticationStore()
    
    authenticationStore.email = null
    authenticationStore.role = null
    authenticationStore.token = null
    authenticationStore.expiration = null
    authenticationStore.refreshToken = null
    authenticationStore.refreshTokenExpirationDateTime = null
  },

  saveAuthenticationStateInLocalStore(): void {
    const authenticationStore = useAuthenticationStore()
    
    if (authenticationStore.email == null || authenticationStore.role == null || authenticationStore.token == null || authenticationStore.expiration == null || authenticationStore.refreshToken == null || authenticationStore.refreshTokenExpirationDateTime == null) {
      throw Error('One or more fields in authentication state is null.')
    }

    localStorage.setItem('authenticationState', JSON.stringify({
      email: authenticationStore.email,
      role: authenticationStore.role,
      token: authenticationStore.token,
      expiration: authenticationStore.expiration,
      refreshToken: authenticationStore.refreshToken,
      refreshTokenExpirationDateTime: authenticationStore.refreshTokenExpirationDateTime,
    }))
  },

  clearAuthenticationStateFromLocalStore(): void {
    localStorage.removeItem('authenticationState')
  },
}