using Spendly.Application.Interfaces.IRepositories;
using Spendly.Domain.Entities;
using Spendly.Infrastructure.Persistence.Database;

namespace Spendly.Infrastructure.Persistence.Repositories
{
    public class ExpenseRepository(ApplicationDbContext context) : IExpenseRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Task AddAsync(Expense expenses)
        {
            _context.Expenses.Add(expenses);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
                return; 

            _context.Expenses.Remove(expense);
        }

        public Task<IEnumerable<Expense>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Expense> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Expense expenses)
        {
            throw new NotImplementedException();
        }
    }
}
