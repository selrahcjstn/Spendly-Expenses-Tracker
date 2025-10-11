using Spendly.Application.Dtos.Expense;
using Spendly.Domain.Entities;

namespace Spendly.Application.Common.Mappings
{
    public static class ExpenseMappings
    {
        public static ExpenseResponseDto ToResponseDto(this Expense expense)
        {
            return new ExpenseResponseDto
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                Description = expense.Description,
                CreatedAt = expense.CreatedAt,
                UpdatedAt = expense.UpdatedAt,
                CategoryIds = [.. expense.Category.Select(c => c.Id)],
                CustomCategory = expense.CustomCategory,
            };
        }
    }
}
