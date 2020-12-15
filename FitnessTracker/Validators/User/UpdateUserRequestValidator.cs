using FitnessTracker.Contracts.Request.User;
using FitnessTracker.Services.Interfaces;
using FluentValidation;

namespace FitnessTracker.Validators.User
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator(IGoalService goalService)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(60);
            
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