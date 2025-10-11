using FluentValidation;
using Spendly.Application.Dtos.Auth;

namespace Spendly.Application.Validators.Auth
{
    public class AuthRequestValidator : AbstractValidator<AuthRequestDto>
    {
        public AuthRequestValidator() 
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format")
                .NotEmpty().WithMessage("Email address is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
