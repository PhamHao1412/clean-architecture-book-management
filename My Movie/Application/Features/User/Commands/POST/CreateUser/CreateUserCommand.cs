﻿using MediatR;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.Application.UserFeatures.Commands
{
    public record CreateUserCommand(string fullname, string username, string password, string role_name) : IRequest<ApiResponse<UserResponse>>;

}
