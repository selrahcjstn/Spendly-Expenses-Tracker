using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Spendly.Api.Extensions;
using Spendly.Application.Dtos.Profile;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(IProfileService profileService, IValidator<ProfileRequestDto> validator) : ControllerBase
    {
        private readonly IProfileService _profileService = profileService;
        private readonly IValidator<ProfileRequestDto> _validator = validator;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _profileService.GetProfileByIdAsync(id);

            return this.ToActionResult(result);
        }
    }
}
