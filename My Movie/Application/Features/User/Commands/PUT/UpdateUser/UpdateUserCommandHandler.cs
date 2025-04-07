using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.UserFeatures.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,ApiResponse<UserResponse>>
{
    public async Task<ApiResponse<UserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new System.Exception();
    }
}