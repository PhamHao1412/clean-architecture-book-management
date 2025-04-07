using MediatR;
using My_Movie.Application.UserFeatures.Commands;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.User.Commands.POST.SignIn;

public class SigInCommandHandler(IUserRepository userRepository) : IRequestHandler<SignInCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.CheckUserLogin(command.username,command.password);
        var token = userRepository.GenerateJwtToken(user);
        return new ApiResponse<string>(200, "success", token);
    }
}   