using MediatR;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.User.Commands.PUT.ForgetPassword;

public class ForgetPasswordCommandHandler(IUserRepository userRepository)
    : IRequestHandler<ForgetPassowordCommand, ApiResponse<Unit>>
{
    public async Task<ApiResponse<Unit>> Handle(ForgetPassowordCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByLoginName(command.username);

        var encryptPassword = BCrypt.Net.BCrypt.HashPassword(command.new_password);
        user.Password = encryptPassword;
        await userRepository.SaveChange();
        
        return new ApiResponse<Unit>(200, "success");
    }
}