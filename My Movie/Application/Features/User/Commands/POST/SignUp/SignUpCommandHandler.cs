using MediatR;
using My_Movie.Application.UserFeatures.Commands;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.Features.User.Commands.POST.SignUp;

public class SignUpCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository)
    : IRequestHandler<SignUpCommand, ApiResponse<UserResponse>>
{
    public async Task<ApiResponse<UserResponse>> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var encryptPassword = BCrypt.Net.BCrypt.HashPassword(command.password);
        var firstName = command.fullname.Split(' ')[0];
        var random = await userRepository.GetRandomID();

        if (random == null)
        {
            await userRepository.InsertShuffleIDAsync();
            random = await userRepository.GetRandomID();
        }

        var newUser = new Model.User
        {
            Name = command.fullname,
            LoginName = command.username,
            Password = encryptPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            user_id = $"{firstName}_{random!.id:D5}"
        };

        var response = await userRepository.AddUserAsync(newUser);
        await roleRepository.AddRoleForUser(newUser.Id, "User");
        return new ApiResponse<UserResponse>(200, "success", response);
    }
}