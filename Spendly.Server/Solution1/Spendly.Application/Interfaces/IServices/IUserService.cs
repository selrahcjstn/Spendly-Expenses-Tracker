using Spendly.Application.Common.Result;
using Spendly.Application.Dtos;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<Result<UserResponseDto>> GetByIdAsync(Guid id);
        Task<Result<UserResponseDto>> AddAsync(CreateUserRequestDto dto);
        Task<Result<UserResponseDto>> UpdateEmailAsync(UpdateEmailRequestDto dto);
    }
}
