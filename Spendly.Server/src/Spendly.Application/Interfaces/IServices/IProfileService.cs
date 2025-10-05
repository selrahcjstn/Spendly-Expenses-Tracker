using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Profile;
using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IProfileService
    {
        Task<Result<Profile>> GetProfileByIdAsync(Guid id);
        Task<Result<Profile>> UpdateProfileAsync(ProfileRequestDto dto);
    }
}
