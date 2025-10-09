using Spendly.Application.Dtos.Profile;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class ProfileMappings
    {
        public static ProfileResponseDto MapToProfileResponse(this Profile profile)
        {

            return new ProfileResponseDto
            {
                Id = profile.Id,
                Firstname = profile.Firstname,
                LastName = profile.LastName,
                MiddleName = profile.MiddleName,
                Sex = profile.Sex,
                BirthDate = profile.BirthDate,
                UserId = profile.UserId,
                User = profile.User.MapToSearchUserResponseDto()
            };

        }
        public static ProfileRegistrationResponse MapToRegistrationResponse(this Profile profile)
        {
            return new ProfileRegistrationResponse
            {
                Id = profile.Id,
                Firstname = profile.Firstname,
                LastName = profile.LastName,
                MiddleName = profile.MiddleName,
                Sex = profile.Sex,
                BirthDate = profile.BirthDate
            };
        }


    }
}
