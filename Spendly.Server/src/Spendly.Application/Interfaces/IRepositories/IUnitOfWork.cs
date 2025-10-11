namespace Spendly.Application.Interfaces.IRepositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IProfileRepository Profiles { get; }
        IExpenseRepository Expenses { get; }
        ICategoryRepository Categories { get; }
        Task SaveChangesAsync();
    }
}
