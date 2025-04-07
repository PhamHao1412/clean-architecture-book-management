using FluentValidation;

namespace My_Movie.Application.UserFeatures.Commands;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public  SignInCommandValidator()
    {
        RuleFor(x => x.username).NotEmpty().WithMessage("username is required.");
        RuleFor(x => x.password).NotEmpty().WithMessage("password is required.");
    }
}