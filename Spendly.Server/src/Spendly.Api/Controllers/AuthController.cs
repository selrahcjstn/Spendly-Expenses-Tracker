using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Spendly.Api.Extensions;
using Spendly.Application.Dtos.Auth;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IValidator<AuthRequestDto> authValidator) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IValidator<AuthRequestDto> _authValidator = authValidator;

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequestDto dto)
        {
            var validationResult = await _authValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

                return this.InvalidInput(message: errors);
            }

            var result = await _authService.LoginAsync(dto);

            return this.ToActionResult(result);
        }
    }
}
