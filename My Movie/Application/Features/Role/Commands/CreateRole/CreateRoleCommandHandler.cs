using MediatR;
using My_Movie.Application.RoleFeatures.Commands;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.RoleFeatures.CommandHandlers;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    ILogger<CreateRoleCommandHandler> logger)
    : IRequestHandler<CreateRoleCommand, ApiResponse<RoleResponse>>
{
    public async Task<ApiResponse<RoleResponse>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var user = userRepository.GetAuthenticatedUser();
        var user_id = int.Parse(user.FindFirst(u => u.Type == "user_id").Value);
        logger.LogInformation("User with ID {UserId} is creating a role", user_id);

        var newRole = new Role
        {
            Name = command.role_name,
            createAt = DateTime.UtcNow,
            updatedAt = DateTime.UtcNow
        };
        var response = await roleRepository.AddRoleAsync(newRole);
        logger.LogInformation("User with ID {UserId} has created the role with name {}.",
            user_id, command.role_name);
        return new ApiResponse<RoleResponse>(200, "success", response);
    }
}