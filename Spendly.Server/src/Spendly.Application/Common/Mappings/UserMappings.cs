using Spendly.Application.Dtos.Profile;
using Spendly.Application.Dtos.User;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class UserMappings
    {
        public static CreateUserResponseDto MapToUserWithProfileDto(this User user)
        {
            return new CreateUserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Profile = user.Profile?.ToDto(),
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

        public static UserResponseDto MapToUserDto(this User user)
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
