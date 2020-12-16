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
                .WithMessage("Podaj E-Mail")
                .EmailAddress()
                .WithMessage("Błędny E-Mail");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Podaj hasło");
                //.SetValidator(new PasswordRule());
        }
    }
}