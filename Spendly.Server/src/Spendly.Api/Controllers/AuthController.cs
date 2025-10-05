using Microsoft.AspNetCore.Mvc;
using Spendly.Application.Dtos.User;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> LoginAsync([FromBody] AuthRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            return Ok(result);

        }
    }
}
