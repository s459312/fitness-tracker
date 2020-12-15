using FitnessTracker.Contracts.Request.Coach;
using FitnessTracker.Services.Interfaces;
using FluentValidation;

namespace FitnessTracker.Validators.AddCoach
{

    public class AddCoachRequestValidator : AbstractValidator<CreateCoach>
    {

        public AddCoachRequestValidator(IGoalService goalService)
        {

            RuleFor(x => x.GoalId)
                .NotEmpty().WithMessage("Podaj poprwany cel ćwiczenia")
                .DependentRules(() =>
                {
                    RuleFor(x => x.GoalId)
                        .Must(roleId => goalService.GoalExists(roleId))
                        .WithMessage("Podany cel nie istnieje");
                });

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(60);

        }

    }

}
