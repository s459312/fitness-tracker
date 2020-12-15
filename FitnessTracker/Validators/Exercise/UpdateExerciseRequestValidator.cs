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
                .NotEmpty().WithMessage("Podaj poprwany cel ćwiczenia")
                .DependentRules(() =>
                {
                    RuleFor(x => x.GoalId)
                        .Must(roleId => goalService.GoalExists(roleId))
                        .WithMessage("Podany cel nie istnieje");
                });
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => x.Description != String.Empty || x.Description != null);

            RuleFor(x => x)
                .Custom((request, context) =>
                {
                    if (request.Czas <= 0 && request.Dystans <= 0 && request.Obciazenie <= 0 && request.Powtorzenia <= 0 && request.Serie <= 0)
                        context.AddFailure("Błędne prametry ćwiczenia");
                    if (request.Obciazenie > 0)
                        if (request.Czas > 0 || request.Dystans > 0)
                            context.AddFailure("Błędne prametry ćwiczenia");
                    if (request.Powtorzenia > 0)
                        if (request.Czas > 0 || request.Dystans > 0)
                            context.AddFailure("Błędne prametry ćwiczenia");
                    if (request.Serie > 0)
                        if (request.Czas > 0 || request.Dystans > 0)
                            context.AddFailure("Błędne prametry ćwiczenia");
                    if (request.Czas > 0)
                        if (request.Powtorzenia > 0 || request.Serie > 0 || request.Obciazenie > 0)
                            context.AddFailure("Błędne prametry ćwiczenia");
                    if (request.Dystans > 0)
                        if (request.Powtorzenia > 0 || request.Serie > 0 || request.Obciazenie > 0)
                            context.AddFailure("Błędne prametry ćwiczenia");
                });
        }
    }
}