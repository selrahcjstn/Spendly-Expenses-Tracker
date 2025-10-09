using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IRepositories
{
    public interface IExpenseRepository
    {
        Task<Expense> GetByIdAsync(Guid id);
        Task<IEnumerable<Expense>> GetAllAsync(); 
        Task AddAsync(Expense expenses);           
        Task UpdateAsync(Expense expenses);
        Task DeleteAsync(Guid id);
    }
}
