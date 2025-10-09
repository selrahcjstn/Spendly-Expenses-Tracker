namespace Spendly.Domain.Entities
{
    public class ExpensesCategory
    {
        public Guid Id { get; set; }
        public string Category { get; set; } = string.Empty;
         
        // many to many
        public ICollection<Expense> Expenses { get; set; } = [];
    }
}
    