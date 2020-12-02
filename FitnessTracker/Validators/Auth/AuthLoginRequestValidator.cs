using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Validators.Rules;
using FluentValidation;

namespace FitnessTracker.Validators.Auth
{
    public class AuthLoginRequestValidator : AbstractValidator<AuthLoginRequest>
    {
        public AuthLoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .SetValidator(new PasswordRule());
        }
    }
}