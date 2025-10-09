using Microsoft.AspNetCore.Mvc;
using Spendly.Api.Extensions;
using Spendly.Application.Dtos.Expense;
using Spendly.Application.Interfaces.IServices;

namespace Spendly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController(IExpenseService expenseService) : ControllerBase
    {
        private readonly IExpenseService _expenseService = expenseService;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
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
            var result = await _expenseService.AddExpenseAsync(dto);
            return this.ToActionResult(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ExpenseRequestDto dto)
        {
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
