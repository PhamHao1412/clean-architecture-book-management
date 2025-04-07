using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.IRepository
{
    public interface IRoleRepository
    {
        Task<RoleResponse> GetRoleByNameAsync(string role_name);
        Task<RoleResponse> AddRoleAsync(Role newRole);
        Task AddRoleForUser(int user_id, string role_name);
        Task<List<RoleResponse>> GetAllRoleAsync();
    }
}
