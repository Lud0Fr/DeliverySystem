using DeliverySystem.Api.Commands;
using FluentValidation;

namespace DeliverySystem.Api.CommandValidators
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(r => r.Email).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
