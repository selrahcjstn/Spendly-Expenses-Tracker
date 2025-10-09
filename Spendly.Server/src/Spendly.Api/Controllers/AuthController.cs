using Microsoft.AspNetCore.Mvc;
using Spendly.Api.Extensions;
using Spendly.Application.Dtos.Auth;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IJwtService jwtService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IJwtService _jwtService = jwtService;

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            return this.ToActionResult(result);
        }
    }
}
