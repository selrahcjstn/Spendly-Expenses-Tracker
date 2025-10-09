using Spendly.Application.Dtos.Auth;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class AuthMappings
    {
        public static AuthResponseDto MapToAuthResponse(this User user, string token, DateTime expiration)
        {
            return new AuthResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                Token = token,
                Expiration = expiration
            };
        }
    }
}
