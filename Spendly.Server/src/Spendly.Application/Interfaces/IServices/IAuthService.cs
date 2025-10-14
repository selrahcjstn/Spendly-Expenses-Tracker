using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Auth;
using Spendly.Application.Dtos.User;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> LoginAsync(AuthRequestDto dto);
        Task<Result<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);

    }
}
