using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.UserFeatures.Commands.UpdateUserCommand;

public record UpdateUserCommand(UpdateUserRequest UpdateUserRequest) : IRequest<ApiResponse<UserResponse>>;