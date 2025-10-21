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
      expiration: null as Date | null,
      refreshToken: null as string | null,
      refreshTokenExpirationDateTime: null as Date | null
    }
  },
  actions, 
  getters
})