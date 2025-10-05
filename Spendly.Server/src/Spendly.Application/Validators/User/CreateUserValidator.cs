using FluentValidation;
using Spendly.Application.Dtos.User;

namespace Spendly.Application.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(6).WithMessage("Username must be at least 6 characters")
                .MaximumLength(30).WithMessage("Username must not exceed 30 characters")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Username must contain only letters and numbers");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches(@"[@$!%*?&]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");
        }
    }
}
