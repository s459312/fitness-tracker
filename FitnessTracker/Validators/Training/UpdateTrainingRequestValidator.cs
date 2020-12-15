using System;
using FitnessTracker.Contracts.Request.Training;
using FluentValidation;

namespace FitnessTracker.Validators.Training
{
    public class UpdateTrainingRequestValidator : AbstractValidator<UpdateTrainingRequest>
    {
        public UpdateTrainingRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            
            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => x.Description != String.Empty || x.Description != null);
        }
    }
}