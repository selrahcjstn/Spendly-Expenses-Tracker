using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Profile;
using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IProfileService
    {
        Task<Result<ProfileResponseDto>> GetProfileByIdAsync(Guid id);
        Task<Result<ProfileResponseDto>> UpdateProfileAsync(ProfileRequestDto dto);
    }
}
