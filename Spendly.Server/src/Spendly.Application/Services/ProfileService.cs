using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Profile;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Application.Services
{
    public class ProfileService(IUnitOfWork unitOfWork, IJwtService jwtService) : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtService _jwtService = jwtService;   

        public async Task<Result<ProfileResponseDto>> GetProfileByIdAsync(Guid id)
        {
            var profile = await _unitOfWork.Profiles.GetByIdAsync(id);
            if (profile == null)
                return Result<ProfileResponseDto>.Failure(ErrorType.NotFound, "Profile Not Found");

            var user = await _unitOfWork.Users.GetByIdAsync(profile.UserId);
            if (user == null)
                return Result<ProfileResponseDto>.Failure(ErrorType.NotFound, "The profile doesn't have an account yet");

            var newProfile = profile.MapToProfileResponse();

            return Result<ProfileResponseDto>.Success(newProfile);
        }

        public async Task<Result<ProfileResponseDto>> UpdateProfileAsync(ProfileRequestDto dto)
        {
            var userId = _jwtService.GetUserId();

            var userProfile = await _unitOfWork.Profiles.GetByIdAsync(userId);
            if (userProfile == null)
                return Result<ProfileResponseDto>.Failure(ErrorType.NotFound, "User not found");

            userProfile.UpdateProfile(dto.Firstname, dto.LastName, dto.MiddleName, dto.Sex, dto.BirthDate);

            await _unitOfWork.Profiles.Update(userProfile); 
            await _unitOfWork.SaveChangesAsync();

            var response = userProfile.MapToProfileResponse();
            return Result<ProfileResponseDto>.Success(response);
        }
    }
}
