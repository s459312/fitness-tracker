using FitnessTracker.Contracts.Request.User;
using FitnessTracker.Services.Interfaces;
using FluentValidation;

namespace FitnessTracker.Validators.User
{
    public class UpdateUserRoleRequestValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleRequestValidator(IRoleService roleService)
        {
            RuleFor(x => x.RoleId)
                .Must( roleId => roleService.RoleExists(roleId))
                    .WithMessage("Role does not not exists");
        }
    }
}