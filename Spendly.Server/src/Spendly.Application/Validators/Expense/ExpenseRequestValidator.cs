using FluentValidation;
using Spendly.Application.Dtos.Expense;

namespace Spendly.Application.Validators.Expense
{
    public class ExpenseRequestValidator : AbstractValidator<ExpenseRequestDto>
    {
        public ExpenseRequestValidator() 
        {
            RuleFor(x => x.Title)
                .MaximumLength(50).WithMessage("Title cannot be more than 50 characters")
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Title cannot be more than 100 characters");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Title is required")
                .GreaterThan(0).WithMessage("Amount should be greater than zero")
                .LessThanOrEqualTo(1000000).WithMessage("Amount exceed the maximum allowed limit");

            RuleFor(x => x)
                .Must(x => (x.CategoryIds != null && x.CategoryIds.Count != 0) || !string.IsNullOrWhiteSpace(x.CustomCategory))
                .WithMessage("Either select at least one category or provide a custom category.");

        }

    }
}
