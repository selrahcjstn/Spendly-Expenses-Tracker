using Spendly.Domain.Entities;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
