using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.RoleFeatures.Commands
{
    public record CreateRoleCommand(string role_name) : IRequest<ApiResponse<RoleResponse>>;


}
