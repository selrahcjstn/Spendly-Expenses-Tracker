using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<ExpensesCategory>> GetAllByIdsAsync(IEnumerable<Guid> ids);
    }
}
