using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Expense;

namespace Spendly.Application.Interfaces.IServices
{
    public interface IExpenseService
    {
        Task<Result<ExpenseResponseDto>> GetExpenseByIdAsync(Guid id);
        Task<Result<IEnumerable<ExpenseResponseDto>>> GetAllExpensesAsync();
        Task<Result<ExpenseResponseDto>> AddExpenseAsync(ExpenseRequestDto dto);
        Task<Result<ExpenseResponseDto>> UpdateExpenseAsync(Guid id, ExpenseRequestDto dto);
        Task<Result<object>> DeleteExpenseAsync(Guid id);
    }
}