using MediatR;
using My_Movie.Application.RoleFeatures.Queries;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.Model;

namespace My_Movie.Application.RoleFeatures.QueryHandlers;

public class GetAllRolesQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetAllRolesQuery, ApiResponse<List<RoleResponse>>>
{
    public async Task<ApiResponse<List<RoleResponse>>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetAllRoleAsync();
        if (roles == null) return new ApiResponse<List<RoleResponse>>(404, "Not found");
        return new ApiResponse<List<RoleResponse>>(200, "success", roles);
        
    }
}