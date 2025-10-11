using Spendly.Application.Common.Enums;
using Spendly.Application.Common.Mappings;
using Spendly.Application.Common.Result;
using Spendly.Application.Dtos.Expense;
using Spendly.Application.Interfaces.IRepositories;
using Spendly.Application.Interfaces.IServices;
using Spendly.Domain.Entities;

namespace Spendly.Application.Services
{
    public class ExpenseService(IUnitOfWork unitOfWork, IJwtService jwtService) : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<Result<ExpenseResponseDto>> AddExpenseAsync(ExpenseRequestDto dto)
        {
            var userId = _jwtService.GetUserId();

            var expense = new Expense(dto.Title, dto.Amount, dto.Description, dto.CustomCategory)
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            if (dto.CategoryIds != null && dto.CategoryIds.Count != 0)
            {
                var categories = await _unitOfWork.Categories
                    .GetAllByIdsAsync(dto.CategoryIds);

                if (categories == null || !categories.Any())
                    return Result<ExpenseResponseDto>.Failure(ErrorType.BadRequest, "Selected Category does not exist");

                foreach (var category in categories)
                {
                    expense.Category.Add(category);
                }
            }

            await _unitOfWork.Expenses.AddAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            var response = expense.ToResponseDto();
            return Result<ExpenseResponseDto>.Success(response);
        }

        public async Task<Result<object>> DeleteExpenseAsync(Guid id)
        {
            var userId = _jwtService.GetUserId();
            var expense = await _unitOfWork.Expenses.GetByIdAsync(id);  

            if(expense == null)
                return Result<object>.Failure(ErrorType.NotFound, "Expense Id Not Found!");
            
            if(expense.UserId != userId)
                return Result<object>.Failure(ErrorType.Forbidden, "You do not own this expense");

            await _unitOfWork.Expenses.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Success(expense);
        }

        public async Task<Result<IEnumerable<ExpenseResponseDto>>> GetAllExpensesAsync()
        {
            var userId = _jwtService.GetUserId();

            var expenses = await _unitOfWork.Expenses.GetAllAsync(userId);
            if (expenses == null || !expenses.Any())
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
            var userId = _jwtService.GetUserId();
            var expense = await _unitOfWork.Expenses.GetByIdAsync(id);

            if (expense == null)
                return Result<ExpenseResponseDto>.Failure(ErrorType.NotFound, "Expenes not found");

            if (expense.UserId != userId)
                return Result<ExpenseResponseDto>.Failure(ErrorType.Forbidden, "You do not own this expense");

            expense.UpdateExpense(dto.Title, dto.Amount, dto.Description);

            await _unitOfWork.Expenses.UpdateAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            var response = expense.ToResponseDto(); 
            return Result<ExpenseResponseDto>.Success(response);
        }
    }
}
