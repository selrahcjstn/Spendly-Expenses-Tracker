using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Expense>> GetAllAsync(Guid userId)
        {
            return await _context.Expenses
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.UpdatedAt ?? e.CreatedAt)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public Task UpdateAsync(Expense expenses)
        {
            _context.Expenses.Update(expenses);
            return Task.CompletedTask;
        }
    }
}
