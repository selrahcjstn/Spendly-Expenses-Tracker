namespace Spendly.Application.Interfaces.IRepositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task SaveChangesAsync();
    }
}
