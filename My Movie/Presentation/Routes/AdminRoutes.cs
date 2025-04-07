using Carter;
using MediatR;
using My_Movie.Application.BookFeatures.Commands;
using My_Movie.Application.Exceptions;
using My_Movie.Application.RoleFeatures.Commands;
using My_Movie.Application.RoleFeatures.Queries;
using My_Movie.Application.UserFeatures.Commands;

namespace My_Movie.Presentation.Routes
{
    public class AdminRoutes : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/admin/CreateBook",
                async (CreateBookCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);

            }).RequireAuthorization("Admin_HR");

            app.MapPut(
                "/admin/UpdateBook/{id}",
                async (UpdateBookCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);

            }).RequireAuthorization("Admin_HR");

            app.MapDelete(
                "/admin/Delete/{id}",
                async (int id, IMediator mediator) =>
                {
                    var command = new DeleteBookCommand(id);
                    return Results.Ok(await mediator.Send(command));

                }).RequireAuthorization("Admin_HR");

            app.MapPost(
                "/admin/CreateUser",
                async (CreateUserCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);

            }).RequireAuthorization("Admin_HR");

            app.MapGet(
                "/admin/GetAllRoles",
                async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetAllRolesQuery());
                return Results.Ok(response);

            }).RequireAuthorization("Admin_HR");
            app.MapPost(
                "/admin/CreateRole",
                async (CreateRoleCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);

            }).RequireAuthorization("Admin_HR");
        }
    }
}
