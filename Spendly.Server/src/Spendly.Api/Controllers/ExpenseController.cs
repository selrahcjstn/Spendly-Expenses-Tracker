using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spendly.Api.Extensions;
using Spendly.Application.Dtos.Expense;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController(IExpenseService expenseService, IValidator<ExpenseRequestDto> validator) : ControllerBase
    {
        private readonly IExpenseService _expenseService = expenseService;
        private readonly IValidator<ExpenseRequestDto> _validator = validator;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _expenseService.GetExpenseByIdAsync(id);
            return this.ToActionResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _expenseService.GetAllExpensesAsync();
            return this.ToActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ExpenseRequestDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if(!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

                return this.InvalidInput(message: errors);
            }

            var result = await _expenseService.AddExpenseAsync(dto);
            
            Console.WriteLine($"Expense created ID: {result.Value?.Id}");

            return this.ToActionResult(
                result,
                createdAtActionName: nameof(GetById),
                routeValues: new { id = result.Value?.Id }
            );
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ExpenseRequestDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

                return this.InvalidInput(message: errors);
            }

            var result = await _expenseService.UpdateExpenseAsync(id, dto);
            return this.ToActionResult(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _expenseService.DeleteExpenseAsync(id);
            return this.ToActionResult(result);
        }
    }
}
