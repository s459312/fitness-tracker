using FitnessTracker.Contracts.Request.Coach;
using FluentValidation;

namespace FitnessTracker.Validators.AddCoach
{
    
    public class AddCoachRequestValidator : AbstractValidator<CreateCoach>
    {
    
        public AddCoachRequestValidator()
        {
            RuleFor(x => x.GoalId)
                .NotNull()
                .GreaterThan(0);//TODO: Inny walidator

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(60);
        }

    }

}
