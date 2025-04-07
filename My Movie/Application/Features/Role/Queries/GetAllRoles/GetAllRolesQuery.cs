using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.RoleFeatures.Queries
{
    public record GetAllRolesQuery : IRequest<ApiResponse<List<RoleResponse>>>;

}
