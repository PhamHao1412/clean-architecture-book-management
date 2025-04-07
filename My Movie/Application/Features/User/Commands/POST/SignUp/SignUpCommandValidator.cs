using FluentValidation;
using My_Movie.Application.UserFeatures.Commands;

namespace My_Movie.Application.Validation;

public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.fullname).NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.username)
            .NotEmpty().WithMessage("LoginName is required.")   
            .MaximumLength(50).WithMessage("LoginName cannot exceed 50 characters");

        RuleFor(x => x.password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters");

        RuleFor(x => x.confirmPassword)
            .NotEmpty().WithMessage("Password confirmation is required.")
            .Equal(x => x.password).WithMessage("The password and confirmation password do not match.");
    }
}