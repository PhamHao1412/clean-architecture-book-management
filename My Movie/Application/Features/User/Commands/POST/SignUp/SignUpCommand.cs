using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.UserFeatures.Commands;

public record SignUpCommand(string fullname, string username, string password, string confirmPassword)
    : IRequest<ApiResponse<UserResponse>>;