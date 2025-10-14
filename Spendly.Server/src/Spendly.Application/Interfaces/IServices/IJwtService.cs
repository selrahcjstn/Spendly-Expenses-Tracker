using Spendly.Domain.Entities;
using System.Security.Claims;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        string GenerateRefreshToken();
        Guid GetUserId();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);

    }
}
