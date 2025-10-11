using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spendly.Api.Extensions;
using Spendly.Application.Dtos.User;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(
        IUserService userService, 
        IValidator<CreateUserRequestDto> validator, 
        IValidator<UpdateEmailRequestDto> emailValidator) 
        : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IValidator<CreateUserRequestDto> _validator = validator;
        private readonly IValidator<UpdateEmailRequestDto> _emaiLValidator = emailValidator;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequestDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

                return this.InvalidInput(message: errors);
            }

            var result = await _userService.AddAsync(dto);

            return this.ToActionResult(
                result, 
                createdAtActionName: nameof(GetById),
                routeValues: new { id = result.Value?.Id }
            );
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmailAsync(Guid id, [FromBody] UpdateEmailRequestDto dto)
        {
            var validationResult = await _emaiLValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

                return this.InvalidInput(message: errors);
            }

            var result = await _userService.UpdateEmailAsync(id, dto);
            return this.ToActionResult(result);
        }
    }
}
