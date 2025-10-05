using FluentValidation;
using Spendly.Application.Dtos.Profile;

namespace Spendly.Application.Validators.Profile
{
    public class CreateProfileValidator : AbstractValidator<ProfileRequestDto>
    {
        public CreateProfileValidator()
        {
            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name should not exceed 50 charaters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name should not exceed 50 characters");

            RuleFor(x => x.Sex)
                .NotEmpty().WithMessage("Sex is required");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birthdate is required")
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Birthdate cannot be in the future.");
        }
    }
}
