using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IRepositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken entity);
        void Update(RefreshToken entity);
        void Remove(RefreshToken entity);
        Task<IEnumerable<RefreshToken>> GetAllAsync();
        Task<RefreshToken?> GetByTokenAsync(string token);
    }
}
