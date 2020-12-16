using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Validators.Rules;
using FluentValidation;

namespace FitnessTracker.Validators.Auth
{
    public class AuthChangePasswordRequestValidator : AbstractValidator<AuthChangePasswordRequest>
    {
        public AuthChangePasswordRequestValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Podaj stare hasło")
                .SetValidator(new PasswordRule());

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Podaj nowe hasło")
                .SetValidator(new PasswordRule());

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .WithMessage("Powtórz hasło")
                .Equal(x => x.NewPassword)
                .WithMessage("Hasła muszą się zgadzać");

        }
    }
}