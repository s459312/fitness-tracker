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
                .SetValidator(new PasswordRule());;

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .SetValidator(new PasswordRule());;

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword);

        }
    }
}