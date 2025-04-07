using FluentValidation;

namespace My_Movie.Application.Features.User.Commands.PUT.ForgetPassword;

public class ForgetPasswordValidatior : AbstractValidator<ForgetPassowordCommand>
{
    public ForgetPasswordValidatior()
    {
        RuleFor(x => x.username).NotEmpty().WithMessage("Username is required.");
    }
}