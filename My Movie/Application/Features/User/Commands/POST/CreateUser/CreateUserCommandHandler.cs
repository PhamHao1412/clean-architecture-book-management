using System.Security.Claims;
using MediatR;
using My_Movie.Application.UserFeatures.Commands;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.UserFeatures.CommandHandlers;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ILogger<CreateUserCommandHandler> logger
) : IRequestHandler<CreateUserCommand, ApiResponse<UserResponse>>
{
    public async Task<ApiResponse<UserResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = userRepository.GetAuthenticatedUser();
        var user_id = int.Parse(user.FindFirst(u => u.Type == "user_id").Value);
        var user_role = user.FindFirst(ClaimTypes.Role).Value;

        logger.LogInformation("User with ID {user_id} and Role is {role} is creating a user", user_id, user_role);
        var encryptPassword = BCrypt.Net.BCrypt.HashPassword(command.password);
        var newUser = new User
        {
            LoginName = command.username,
            Name = command.fullname,
            Password = encryptPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var response = await userRepository.AddUserAsync(newUser);
        logger.LogInformation("User with ID {user_id} and Role is {role} has created a user", user_id, user_role);

        await roleRepository.AddRoleForUser(newUser.Id, command.role_name);

        return new ApiResponse<UserResponse>(200, "success", response);
    }
}