﻿namespace FindACoach.Core.DTO
{
    public class AuthenticationResponse
    {
        public string? Email { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpirationDateTime { get; set; }
    }
}