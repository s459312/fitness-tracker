using System;
using FitnessTracker.Contracts.Request.Exercise;
using FitnessTracker.Services.Interfaces;
using FluentValidation;

namespace FitnessTracker.Validators.Exercise
{
    public class CreateExerciseRequestValidator : AbstractValidator<CreateExerciseRequest>
    {
        private readonly string _errorMsg = "Podaj poprawną kombinację parametrów ćwiczenia";
        public CreateExerciseRequestValidator(IGoalService goalService)
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

            When(x => x.Powtorzenia > 0 || x.Czas > 0 || x.Obciazenie > 0 || x.Serie > 0 || x.Dystans > 0, () =>
            {
                When(x => x.Powtorzenia >= 1, () =>
                {
                    RuleFor(x => x.Obciazenie).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Serie).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Dystans).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Czas).Equal(0).WithMessage(_errorMsg);
                });
            
                When(x => x.Serie >= 1, () =>
                {
                    RuleFor(x => x.Obciazenie).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Powtorzenia).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Dystans).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Czas).Equal(0).WithMessage(_errorMsg);
                });
            
                When(x => x.Obciazenie >= 1, () =>
                {
                    RuleFor(x => x.Serie).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Powtorzenia).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Dystans).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Czas).Equal(0).WithMessage(_errorMsg);
                });
            
                When(x => x.Czas >= 1, () =>
                {
                    RuleFor(x => x.Obciazenie).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Serie).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Powtorzenia).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Dystans).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                });
            
                When(x => x.Dystans >= 1, () =>
                {
                    RuleFor(x => x.Obciazenie).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Serie).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Powtorzenia).Equal(0).WithMessage(_errorMsg);
                    RuleFor(x => x.Czas).GreaterThanOrEqualTo(0).WithMessage(_errorMsg);
                });
            })
            .Otherwise(() =>
            {
                RuleFor(x => x.Obciazenie).GreaterThanOrEqualTo(1).WithMessage(_errorMsg);
                RuleFor(x => x.Serie).GreaterThanOrEqualTo(1).WithMessage(_errorMsg);
                RuleFor(x => x.Powtorzenia).GreaterThanOrEqualTo(1).WithMessage(_errorMsg);
                RuleFor(x => x.Dystans).GreaterThanOrEqualTo(1).WithMessage(_errorMsg);
                RuleFor(x => x.Czas).GreaterThanOrEqualTo(1).WithMessage(_errorMsg);
            });
                
            
            
        }
    }
}