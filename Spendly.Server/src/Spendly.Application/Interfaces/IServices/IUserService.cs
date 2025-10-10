using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.User;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<Result<UserResponseDto>> GetByIdAsync(Guid id);
        Task<Result<CreateUserResponseDto>> AddAsync(CreateUserRequestDto dto);
        Task<Result<UserResponseDto>> UpdateEmailAsync(Guid id, UpdateEmailRequestDto dto);
    }
}