using Spendly.Application.Dtos.Profile;
using Spendly.Application.Dtos.User;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class UserMappings
    {
        public static CreateUserResponseDto UserMapToDto(this User user)
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

        public static ProfileResponseDto ToDto(this Profile profile)
        {
            return new ProfileResponseDto
            {
                Id = profile.Id,
                Firstname = profile.Firstname,
                LastName = profile.LastName,
                MiddleName = profile.MiddleName,
                Sex = profile.Sex,
                BirthDate = profile.BirthDate,
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
