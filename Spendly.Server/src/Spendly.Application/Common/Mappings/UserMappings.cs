using Spendly.Application.Dtos.Auth;
using Spendly.Application.Dtos.Profile;
using Spendly.Application.Dtos.User;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class UserMappings
    {
        public static CreateUserResponseDto MapToUserEntity(this User user)
        {
            return new CreateUserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Profile = user.Profile?.MapToRegistrationResponse(),
            };
        }

        public static SearchUserResponseDto MapToSearchUserResponseDto(this User user)
        {
            return new SearchUserResponseDto
            {
                Username = user.Username,
                Email = user.Email,
            };
        }

        public static UserResponseDto MapToUserToResponse(this User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
            };
        }

        
    }
}
