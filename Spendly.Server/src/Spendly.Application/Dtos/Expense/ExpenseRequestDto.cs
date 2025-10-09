using Spendly.Domain.Entities;

namespace Spendly.Application.Dtos.Expense
{
    public class ExpenseRequestDto
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        // Category many to many
        public ICollection<ExpenseCategoryDto> Category { get; set; } = [];
    }
}
