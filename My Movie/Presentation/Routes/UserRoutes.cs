using Carter;
using MediatR;
using My_Movie.Application.Features.User.Commands.CreateRandomIDCommand;
using My_Movie.Application.Features.User.Commands.PUT.ForgetPassword;
using My_Movie.Application.UserFeatures.Commands;

namespace My_Movie.Modules
{
    public class UserRoutes : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost(
                "/signup",
                async (IMediator mediator, SignUpCommand command) =>
           {
               var response = await mediator.Send(command);
               return Results.Ok(response);
           });

            app.MapPost(
                "/signin", 
                async (IMediator mediator, SignInCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            });
            
            app.MapGet(
                "/GenerateID",
                async (IMediator mediator) =>
            {
                await mediator.Send(new CreateRandomIDCommand());
                return Results.Ok();
            });
            
            app.MapPut(
                "/forgetPassword", 
                async (IMediator mediator, ForgetPassowordCommand command) =>
            {
                var response = await mediator.Send(command);
                return Results.Ok(response);
            });
        }
    }

}
