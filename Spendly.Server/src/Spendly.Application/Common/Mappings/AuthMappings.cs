using Spendly.Application.Dtos.Auth;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class AuthMappings
    {
        public static AuthResponseDto MapToAuthResponse(this 
            User user, 
            string accessToken, 
            DateTime accessTokenExpiresAt, 
            string refreshToken, 
            DateTime refreshTokenExpiresAt)
        {
            return new AuthResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                AccessToken = accessToken,
                AccessTokenExpiresAt = accessTokenExpiresAt,
                RefreshToken = refreshToken,
                RefreshTokenExpiresAt = refreshTokenExpiresAt
            };
        }
    }
}
