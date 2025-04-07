using MediatR;
using My_Movie.Model;

namespace My_Movie.Application.Features.User.Commands.PUT.ForgetPassword;

public record ForgetPassowordCommand(string username, string new_password) : IRequest<ApiResponse<Unit>>;
