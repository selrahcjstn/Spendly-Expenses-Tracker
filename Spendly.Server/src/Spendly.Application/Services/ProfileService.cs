using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
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

        public async Task<Result<ProfileResponseDto>> GetProfileByIdAsync(Guid id)
        {
            var profile = await _unitOfWork.Profiles.GetByIdAsync(id);
            if (profile == null)
                return Result<ProfileResponseDto>.Failure(ErrorType.NotFound, "Profile Not Found");

            var user = await _unitOfWork.Users.GetByIdAsync(profile.UserId);
            if (user == null)
                return Result<ProfileResponseDto>.Failure(ErrorType.NotFound, "The profile doesn't have an account yet");

            var newProfile = profile.ToDto();

            return Result<ProfileResponseDto>.Success(newProfile);
        }

        public Task<Result<ProfileResponseDto>> UpdateProfileAsync(ProfileRequestDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
