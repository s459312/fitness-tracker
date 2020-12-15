using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Services.Interfaces;
using FitnessTracker.Validators.Rules;
using FluentValidation;

namespace FitnessTracker.Validators.Auth
{
    public class AuthRegisterRequestValidator : AbstractValidator<AuthRegisterRequest>
    {
        public AuthRegisterRequestValidator(IGoalService goalService)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .SetValidator(new PasswordRule());

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password).WithMessage("Podane hasła nie są identyczne");

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Surname)
                .NotEmpty();
            
            RuleFor(x => x.GoalId)
                .NotEmpty().WithMessage("Podaj poprwany cel ćwiczenia")
                .DependentRules(() =>
                {
                    RuleFor(x => x.GoalId)
                        .Must(roleId => goalService.GoalExists(roleId))
                        .WithMessage("Podany cel nie istnieje");
                });
        }
    }
}