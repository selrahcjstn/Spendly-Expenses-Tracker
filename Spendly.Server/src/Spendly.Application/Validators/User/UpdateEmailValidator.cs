using FluentValidation;
using Spendly.Application.Dtos.User;

namespace Spendly.Application.Validators.User
{
    public class UpdateEmailValidator : AbstractValidator<UpdateEmailRequestDto>
    {
        public UpdateEmailValidator() 
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format");
        }   
    }
}
 