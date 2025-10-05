using Spendly.Application.Dtos.User;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class UserMappings
    {
        public static UserResponseDto UserMapToDto(this User user)
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
