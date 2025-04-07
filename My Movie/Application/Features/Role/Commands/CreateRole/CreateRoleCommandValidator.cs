using FluentValidation;
using My_Movie.Application.RoleFeatures.Commands;
using My_Movie.DTO;

namespace My_Movie.Validation
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.role_name).NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters")
                .Matches("^[^0-9]*$").WithMessage("Role name cannot contain numbers");

        }
    }

}
