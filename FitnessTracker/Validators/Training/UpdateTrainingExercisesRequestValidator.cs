using FitnessTracker.Contracts.Request.Training;
using FitnessTracker.Services.Interfaces;
using FluentValidation;

namespace FitnessTracker.Validators.Training
{
    public class UpdateTrainingExercisesRequestValidator : AbstractValidator<UpdateTrainingExercisesRequest>
    {
        public UpdateTrainingExercisesRequestValidator(IExerciseService exerciseService)
        {
            RuleForEach(x => x.Exercises)
                .GreaterThan(0);

            RuleFor(x => x.Exercises)
                .ForEach(arrayRule => {
                    arrayRule.Must(element => element > 0).WithMessage("Podano błędną listę ćwiczeń");
                })
                .DependentRules(() => {
                    RuleFor(x => x.Exercises)
                        .Must(ids => exerciseService.AllExercisesExists(ids))
                        .WithMessage("Podano błędną listę ćwiczeń");
                });
        }
    }
}