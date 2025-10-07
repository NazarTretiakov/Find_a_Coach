export interface JwtPayload {
  sub: string;
  jti: string;
  iat: number;
  exp: number;
  aud: string;
  iss: string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
}