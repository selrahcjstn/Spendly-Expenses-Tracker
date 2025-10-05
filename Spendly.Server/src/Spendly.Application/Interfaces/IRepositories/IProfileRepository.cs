using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IRepositories
{
    public interface IProfileRepository
    {
        Task<Profile?> GetByIdAsync(Guid id);
        Task Add(Profile profile);
        Task Update(Profile profile);
    }
}
