using My_Movie.Domain.Model;
using My_Movie.DTO;
using My_Movie.Model;
using System.Security.Claims;

namespace My_Movie.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByLoginName(string login_name);
        Task<User> CheckUserLogin(string login_name, string password);
        Task<User> GetUserByIdAsync(int id);
        List<string> GetRoleByUserId(int user_id);
        Task<UserResponse> AddUserAsync(User user);
        string GenerateJwtToken(User user);
        ClaimsPrincipal? GetAuthenticatedUser();
        Task InsertShuffleIDAsync();
        Task<RandomID?> GetRandomID();
        Task SaveChange();
    }
}
