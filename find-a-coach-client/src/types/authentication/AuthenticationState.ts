interface AuthenticationState {
  email: string | null;
  role: string | null;
  token: string | null;
  tokenExpiration: Date | null;
  refreshToken: string | null;
  refreshTokenExpiration: Date | null;
}

export type { AuthenticationState }