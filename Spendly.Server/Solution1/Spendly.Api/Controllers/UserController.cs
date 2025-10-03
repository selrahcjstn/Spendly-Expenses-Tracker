using Microsoft.AspNetCore.Mvc;
using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponseDto>> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.ErrorMessage),
                    _ => BadRequest(result.ErrorMessage)
                };
            }

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] CreateUserRequestDto dto)
        {
            var result = await _userService.AddAsync(dto);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.Conflict => Conflict(result.ErrorMessage),
                    ErrorType.BadRequest => BadRequest(result.ErrorMessage),
                    _ => BadRequest(result.ErrorMessage)
                };
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserResponseDto>> UpdateEmail(Guid id, [FromBody] UpdateEmailRequestDto dto)
        {
            dto.Id = id;
            var result = await _userService.UpdateEmailAsync(dto);

            if (!result.IsSuccess)
            {
                return result.ErrorType switch
                {
                    ErrorType.NotFound => NotFound(result.ErrorMessage),
                    ErrorType.BadRequest => BadRequest(result.ErrorMessage),
                    _ => BadRequest(result.ErrorMessage)
                };
            }

            return Ok(result.Value);
        }
    }
}
