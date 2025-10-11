namespace Spendly.Application.Dtos.Expense
{
    public class ExpenseRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }

        // Category many to many
        public ICollection<Guid> CategoryIds { get; set; } = [];
        public string? CustomCategory { get; set; }
    }
}
