import { defineStore } from 'pinia'

import actions from './actions'
import getters from './getters'
import type { AuthenticationState } from '../../types/authentication/AuthenticationState.ts'


export const useAuthenticationStore = defineStore('authentication', {
  state(): AuthenticationState {
    return {
      email: null as string | null,
      role: null as string | null,
      token: null as string | null,
      tokenExpiration: null as Date | null,
      refreshToken: null as string | null,
      refreshTokenExpiration: null as Date | null
    }
  },
  actions, 
  getters
})