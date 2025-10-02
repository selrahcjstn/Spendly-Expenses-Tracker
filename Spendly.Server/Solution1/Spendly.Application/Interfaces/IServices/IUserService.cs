using Spendly.Application.Dtos;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<UserResponseDto> GetByIdAsync(Guid id);
        Task<UserResponseDto> AddAsync(UserCreateRequestDto dto);
        Task<UserResponseDto> UpdateEmailAsync(UpdateEmailRequestDto dto);
    }
}
