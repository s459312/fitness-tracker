using FitnessTracker.Contracts.Request.User;
using FluentValidation;

namespace FitnessTracker.Validators.User
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(60);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(60);
        }
    }
}