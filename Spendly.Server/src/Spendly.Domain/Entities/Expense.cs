namespace Spendly.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // User one to many
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        // Category many to many
        public ICollection<ExpensesCategory> Category { get; set; } = [];

        // Custom Category
        public String? CustomCategory { get; set; }

        public Expense() { }

        public Expense(string title, decimal amount, string? description, string? customeCategory)
        {
            Id = Guid.NewGuid();
            Title = title;
            Amount = amount;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            CustomCategory = customeCategory;
        }

        public void UpdateExpense(string title, decimal amount, string? description)
        {
            Title = title;
            Amount = amount;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
