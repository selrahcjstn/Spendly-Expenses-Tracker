using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.User;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<Result<UserResponseDto>> LoginAsync(AuthRequestDto dto);
    }
}
