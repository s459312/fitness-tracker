using System;
using FitnessTracker.Contracts.Request.Exercise;
using FitnessTracker.Services.Interfaces;
using FluentValidation;

namespace FitnessTracker.Validators.Exercise
{
    public class UpdateExerciseRequestValidator : AbstractValidator<UpdateExerciseRequest>
    {
        public UpdateExerciseRequestValidator(IGoalService goalService)
        {
            RuleFor(x => x.GoalId)
                .Must(roleId => goalService.GoalExists(roleId))
                .WithMessage("Cel o podanym id nie istnieje");
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => x.Description != String.Empty || x.Description != null);

            RuleFor(x => x.Serie)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Powtorzenia)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Dystans)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Obciazenie)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Czas)
                .GreaterThanOrEqualTo(0);
        }
    }
}