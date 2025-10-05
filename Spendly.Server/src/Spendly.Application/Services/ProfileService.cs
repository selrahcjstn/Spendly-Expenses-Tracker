using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Profile;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class ProfileService(IUnitOfWork unitOfWork) : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<Profile>> GetProfileByIdAsync(Guid id)
        {
            var profile = await _unitOfWork.Profiles.GetByIdAsync(id);
            if (profile == null)
                return Result<Profile>.Failure(ErrorType.NotFound, "Profile Not Found");

            return Result<Profile>.Success(profile);
        }

        public Task<Result<Profile>> UpdateProfileAsync(ProfileRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
