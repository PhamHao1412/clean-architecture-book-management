using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.UserFeatures.Commands
{
    public record SignInCommand(string username, string password) : IRequest<ApiResponse<string>>;
}
