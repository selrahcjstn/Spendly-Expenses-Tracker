using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Expense;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class ExpenseService(IUnitOfWork unitOfWork) : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<ExpenseResponseDto>> AddExpenseAsync(ExpenseRequestDto dto)
        {
            var expense = new Expense(dto.Title, dto.Amount, dto.Description)
            {
                UserId = dto.UserId,
            };

            await _unitOfWork.Expenses.AddAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            var response = expense.ToResponseDto();

            return Result<ExpenseResponseDto>.Success(response);
        }

        public async Task<Result<object>> DeleteExpenseAsync(Guid id)
        {
            var expense = await _unitOfWork.Expenses.GetByIdAsync(id);  
            if(expense == null)
                return Result<object>.Failure(ErrorType.NotFound, "Expense Id Not Found!");
            
            await _unitOfWork.Expenses.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success(expense);
        }

        public async Task<Result<IEnumerable<ExpenseResponseDto>>> GetAllExpensesAsync()
        {
            var expenses = await _unitOfWork.Expenses.GetAllAsync();
            if (expenses == null || expenses.Any())
                return Result<IEnumerable<ExpenseResponseDto>>.Failure(ErrorType.NotFound, "No Expenses Record Yet");

            var response = expenses
                .Select(e => e.ToResponseDto())
                .ToList();

            return Result<IEnumerable<ExpenseResponseDto>>.Success(response);
        }

        public async Task<Result<ExpenseResponseDto>> GetExpenseByIdAsync(Guid id)
        {
            var expense = await _unitOfWork.Expenses.GetByIdAsync(id);
            if (expense == null)
                return Result<ExpenseResponseDto>.Failure(ErrorType.NotFound, "Expenes not found");

            var response = expense.ToResponseDto();
            return Result<ExpenseResponseDto>.Success(response);    
        }

        public async Task<Result<ExpenseResponseDto>> UpdateExpenseAsync(Guid id, ExpenseRequestDto dto)
        {
            var expense = await _unitOfWork.Expenses.GetByIdAsync(id);
            if (expense == null)
                return Result<ExpenseResponseDto>.Failure(ErrorType.NotFound, "Expenes not found");

            expense.UpdateExpense(dto.Title, dto.Amount, dto.Description);

            await _unitOfWork.Expenses.UpdateAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            var response = expense.ToResponseDto(); 
            return Result<ExpenseResponseDto>.Success(response);
        }
    }
}
