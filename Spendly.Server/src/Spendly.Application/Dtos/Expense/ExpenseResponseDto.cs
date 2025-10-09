using Spendly.Domain.Entities;

namespace Spendly.Application.Dtos.Expense
{
    public class ExpenseResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 

        // Category many to many
        public ICollection<ExpensesCategory> Category { get; set; } = [];
    }
}
