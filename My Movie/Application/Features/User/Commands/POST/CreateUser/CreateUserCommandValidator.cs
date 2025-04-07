using FluentValidation;
using My_Movie.Application.UserFeatures.Commands;
using My_Movie.DTO;

namespace My_Movie.Validation
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.fullname).NotEmpty().WithMessage("Full name is required.");

            RuleFor(x => x.username)
                .NotEmpty().WithMessage("username is required.")
                .MaximumLength(50).WithMessage("LoginName cannot exceed 50 characters");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters");

            RuleFor(x => x.role_name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters");
        }

    }
}
