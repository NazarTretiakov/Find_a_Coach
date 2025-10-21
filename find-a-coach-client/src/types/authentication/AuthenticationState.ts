interface AuthenticationState {
  email: string | null;
  role: string | null;
  token: string | null;
  expiration: Date | null;
  refreshToken: string | null;
  refreshTokenExpirationDateTime: Date | null;
}

export type { AuthenticationState }